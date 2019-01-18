using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class PurchaseOrderItemGridViewModel
    {
        public List<PurchaseOrderItemGridRowModel> rows = new List<PurchaseOrderItemGridRowModel>();
        public int Page;
        public int Total;
        public int Records;

        public PurchaseOrderItemGridViewModel(List<PurchaseItem> PurchaseItems, IPurchaseTypeRepository PurchaseTypeRepo)
        {
            foreach (PurchaseItem _item in PurchaseItems)
            {
                string purType = (PurchaseTypeRepo.QueryByID(_item.PurchaseType) ?? new PurchaseType()).Name;
                PurchaseOrderItemGridRowModel _row = new PurchaseOrderItemGridRowModel(_item, purType);
                rows.Add(_row);
            }
        }
    }
}