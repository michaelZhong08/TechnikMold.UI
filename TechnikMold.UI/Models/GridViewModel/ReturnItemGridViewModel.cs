using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridRowModel;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class ReturnItemGridViewModel
    {
        public List<ReturnItemGridRowModel> rows;
        public int Page;
        public int Total;
        public int Records; 

        public ReturnItemGridViewModel(List<ReturnItem> ReturnItems, 
            IPurchaseItemRepository POItemRepository, 
            IWarehouseStockRepository StockRepository, 
            int RequestState)
        {
            rows = new List<ReturnItemGridRowModel>();
            foreach (ReturnItem _item in ReturnItems){
                WarehouseStock _stock =StockRepository.QueryByID(_item.WarehouseItemID);
                PurchaseItem _poItem = POItemRepository.QueryByID(_item.PurchaseItemID);
                rows.Add(new ReturnItemGridRowModel(_item,_poItem, _stock, RequestState));
            }
            Page = 1;
            Total = ReturnItems.Count();
            Records = ReturnItems.Count();
        }
    }
}