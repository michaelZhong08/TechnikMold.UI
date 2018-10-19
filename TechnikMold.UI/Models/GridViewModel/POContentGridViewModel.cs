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
            IPurchaseRequestRepository PRRepository, IPurchaseItemRepository PurchaseItemRepository)
        {
            rows = new List<POContentGridRowModel>();
            string _eta;
            string _prNumber;
            foreach (POContent _poContent in POContents)
            {
                try
                {
                    PurchaseItem _item = PurchaseItemRepository.QueryByID(_poContent.PurchaseItemID);
                    PurchaseRequest _pr = PRRepository.GetByID(_item.PurchaseRequestID);
                    _eta = _pr.DueDate.ToString("yyyy-MM-dd");
                    _prNumber = _pr.PurchaseRequestNumber;
                }
                catch
                {
                    _eta = "";
                    _prNumber = "";
                }
                rows.Add(new POContentGridRowModel(_poContent, _eta, _prNumber));
            }
            Page = 1;
            Total = POContents.Count();
            Records = 500;
        }
    }
}