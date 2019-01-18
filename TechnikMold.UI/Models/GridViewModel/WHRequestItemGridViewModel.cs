using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Abstract;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class WHRequestItemGridViewModel
    {
        public List<WHRequestItemGridRowModel> rows = new List<WHRequestItemGridRowModel>();
        public int Page;
        public int Total;
        public int Records;

        public WHRequestItemGridViewModel(IEnumerable<WarehouseRequestItem> Items,IWHStockRepository WHStockRepo)
        {
            foreach (WarehouseRequestItem _item in Items)
            {
                WHStock _stockItem = WHStockRepo.GetStockByPartNum(_item.PartNumber, _item.PartID);
                rows.Add(new WHRequestItemGridRowModel(_item, _stockItem));
            }
        }
    }
}