using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class QRListGridViewModel
    {
        public List<QRListGridRowModel> rows;

        public int Total;
        public int Page;
        public int Records;

        public QRListGridViewModel(IEnumerable<QuotationRequest> Requests,
            IUserRepository UserRepository,
            ISupplierRepository SupplierRepository, 
            IProjectRepository ProjectRepository, 
            IQRSupplierRepository QRSupplierRepository)
        {
            string MoldNumber="";
            string UserName="";
            string ProjectNumber="";
            rows = new List<QRListGridRowModel>();
            
            foreach (QuotationRequest _request in Requests)
            {
                Project _project = ProjectRepository.GetByID(_request.ProjectID);
                MoldNumber = _project==null?"":_project.MoldNumber;
                ProjectNumber = _project == null ? "" : _project.ProjectNumber;
                UserName = UserRepository.GetUserByID(_request.PurchaseUserID).FullName;


                rows.Add(new QRListGridRowModel(_request, ProjectNumber, MoldNumber, UserName
                    , GetQRSuppliers(_request.QuotationRequestID, QRSupplierRepository)
                    , GetQRSuppliers(_request.QuotationRequestID, QRSupplierRepository, 1)));
            }
            Page = 1;
            Total = Requests.Count();
            Records = 200;
        }

        private string GetQRSuppliers(int QuotationRequestID, IQRSupplierRepository QRSupplierRepo,int State=0)
        {
            IEnumerable<QRSupplier> QRSuppliers;
            if (State > 0)
            {
                QRSuppliers = QRSupplierRepo.QueryByQRID(QuotationRequestID).Where(s=>s.QuotationState==true);
            }else{
                QRSuppliers = QRSupplierRepo.QueryByQRID(QuotationRequestID);
            }
            string _result="";
            foreach (QRSupplier _supplier in QRSuppliers)
            {
                _result = _result == "" ? _supplier.SupplierName : _result+ "," + _supplier.SupplierName;
            }
            return _result;
        }
    }
}