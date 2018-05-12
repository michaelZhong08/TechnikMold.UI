using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoldManager.WebUI.Models.GridRowModel;
using MoldManager.WebUI.Models.Helpers;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;


namespace MoldManager.WebUI.Models.GridViewModel
{
    public class WHPOContentGridViewModel
    {
        public List<WHPOContentGridRowModel> rows;

        public int Page;
        public int Total;
        public int Records;

        public WHPOContentGridViewModel(IEnumerable<PurchaseItem> PurchaseItems)
        {
            rows = new List<WHPOContentGridRowModel>();

            Page = 1;
            Total = PurchaseItems.Count();
            foreach (PurchaseItem _item in PurchaseItems)
            {
                rows.Add(new WHPOContentGridRowModel(_item));
            }
        }
    }
}