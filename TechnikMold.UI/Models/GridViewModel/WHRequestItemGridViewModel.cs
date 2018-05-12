using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridRowModel;


namespace MoldManager.WebUI.Models.GridViewModel
{
    public class WHRequestItemGridViewModel
    {
        public List<WHRequestItemGridRowModel> rows = new List<WHRequestItemGridRowModel>();
        public int Page;
        public int Total;
        public int Records;

        public WHRequestItemGridViewModel(IEnumerable<WarehouseRequestItem> Items)
        {
            foreach (WarehouseRequestItem _item in Items)
            {
                rows.Add(new WHRequestItemGridRowModel(_item));
            }
        }
    }
}