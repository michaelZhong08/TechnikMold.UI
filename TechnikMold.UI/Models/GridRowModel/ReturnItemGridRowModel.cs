using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class ReturnItemGridRowModel
    {
        public string[] cell;
        public ReturnItemGridRowModel(ReturnItem Item, PurchaseItem POItem, WHStock Stock, int RequestState)
        {
            cell= new string[12];
            cell[0]= Item.ReturnItemID.ToString();
            cell[1] = Item.PurchaseItemID.ToString();
            cell[2] = Item.WarehouseItemID.ToString();
            cell[3] = Item.Name;
            cell[4] = Item.MaterialNumber;
            cell[5] = Item.Specification;
            cell[6] = POItem.Quantity.ToString();
            cell[7] = POItem.InStockQty.ToString();
            if (RequestState != 0)
            {
                cell[8] = Stock.Qty.ToString();
            }
            else    
            {
                cell[8] = (Stock.Qty + Item.Quantity).ToString();
            }
            
            
            if (Item.Quantity == 0)
            {
                if (Stock.Qty > POItem.InStockQty)
                {
                    cell[9] = POItem.InStockQty.ToString();
                }
                else
                {
                    cell[9] = Stock.Qty.ToString();
                }
            }
            else
            {
                cell[9] = Item.Quantity.ToString();
            }
            
            
            cell[10] = Item.Memo;

            cell[11] = Item.Enabled.ToString();
        }
    }
}