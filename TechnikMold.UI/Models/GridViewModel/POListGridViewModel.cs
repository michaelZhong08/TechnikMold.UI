using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridRowModel;
using MoldManager.WebUI.Models.Helpers;
using TechnikSys.MoldManager.Domain.Abstract;


namespace MoldManager.WebUI.Models.GridViewModel
{
    public class POListGridViewModel
    {
        public List<POListGridRowModel> rows;
        public int Page;
        public int Total;
        public int Records;

        public POListGridViewModel(IEnumerable<PurchaseOrder> Orders,
            IProjectRepository ProjectRepository,
            ISupplierRepository SupplierRepository, 
            IPurchaseTypeRepository PurchaseTypeRepository, 
            IUserRepository UserRepository)
        {
            rows = new List<POListGridRowModel>();
            string _status;
            int count = Orders.Count();
            if (count > 0)
            {
                foreach (PurchaseOrder _order in Orders)
                {
                    string Supplier = "";
                    if (SupplierRepository.QueryByID(_order.SupplierID)!=null)
                        Supplier = SupplierRepository.QueryByID(_order.SupplierID).Name;
                    string PurchaseType;
                    try
                    {
                        PurchaseType = PurchaseTypeRepository.QueryByID(_order.PurchaseType).Name;
                    }
                    catch
                    {
                        PurchaseType = "";
                    }
                    _status = Enum.GetName(typeof(PurchaseOrderStatus), _order.State);
                    string _user = "";
                    try
                    {
                        _user = UserRepository.GetUserByID(_order.UserID).FullName;
                    }
                    catch
                    {
                        _user = "";
                    }
                    rows.Add(new POListGridRowModel(_order, Supplier, _status, PurchaseType, _user));
                }
            }
            
            Page = 1;
            Total = Orders.Count();
            Records = Orders.Count(); ;
        }
    }
}