using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class POContentGridViewModel
    {
        public List<POContentGridRowModel> rows;
        public int Page;
        public int Total;
        public int Records;

        public POContentGridViewModel(IEnumerable<POContent> POContents, 
            IPurchaseRequestRepository PRRepository, 
            IPurchaseItemRepository PurchaseItemRepository, 
            IPurchaseTypeRepository PurchaseTypeRepository)
        {
            rows = new List<POContentGridRowModel>();
            string _eta;
            string _prNumber;
            int _typeid;
            string _type;
            foreach (POContent _poContent in POContents)
            {
                try{
                    PurchaseItem _item = PurchaseItemRepository.QueryByID(_poContent.PurchaseItemID);
                    PurchaseRequest _pr = PRRepository.GetByID(_item.PurchaseRequestID);
                    _eta = _pr.DueDate.ToString("yyyy-MM-dd");
                    _prNumber = _pr.PurchaseRequestNumber;
                    _typeid = _item.PurchaseType;
                    _type = PurchaseTypeRepository.QueryByID(_typeid).Name;
                }
                catch
                {
                    _eta = "";
                    _prNumber = "";
                    _typeid = 0;
                    _type = "";
                }
                rows.Add(new POContentGridRowModel(_poContent, _eta, _prNumber, _typeid, _type));
            }
            Page = 1;
            Total = POContents.Count();
            Records = 500;
        }
    }
}