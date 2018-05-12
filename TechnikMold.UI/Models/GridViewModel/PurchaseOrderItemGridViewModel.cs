using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class PurchaseOrderItemGridViewModel
    {
        public List<PurchaseOrderItemGridRowModel> rows = new List<PurchaseOrderItemGridRowModel>();
        public int Page;
        public int Total;
        public int Records;

        public PurchaseOrderItemGridViewModel(List<PurchaseItem> PurchaseItems)
        {
            foreach (PurchaseItem _item in PurchaseItems)
            {
                PurchaseOrderItemGridRowModel _row = new PurchaseOrderItemGridRowModel(_item);
                rows.Add(_row);
            }
        }
    }
}