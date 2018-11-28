using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class WarehouseStockGridRowModel
    {
        public string[] cell;
        public WarehouseStockGridRowModel(WHStock Stock, 
            WHPart _Part, 
            //string RequestUser, 
            //string PurchaseUser, 
            //string State, 
            string PurchaseType,
            string StockType, 
            string Warehouse, 
            string WarehousePosition)
        {
            
            //if (PurchaseItem == null)
            //{
            //    PurchaseItem = new PurchaseItem();
            //}
            cell = new string[13];
            cell[0] = Stock.ID.ToString();
            cell[1] = _Part.PartName;
            
            cell[2] = _Part.PartNum;
            cell[3] = _Part.Specification;
            cell[4] = _Part.Materials;

            if ((_Part.SafeQuantity>0)&&(Stock.Qty< _Part.SafeQuantity)){
                cell[5] = "<font color='#ff0000'>"+Stock.Qty.ToString()+"</font>";
            }else{
                cell[5] = Stock.Qty.ToString();
            }
            
            cell[6] = _Part.SafeQuantity.ToString();
            //cell[7] = PurchaseItem==null?"":PurchaseItem.SupplierName;
            cell[7] = PurchaseType;
            cell[8] = StockType;
            //cell[10] = PurchaseItem == null ? "-" : PurchaseItem.PlanTime.ToString("yy/MM/dd");
            //cell[11] = PurchaseItem == null ? "-" : PurchaseItem.RequireTime.ToString("yy/MM/dd");
            //cell[12] = PurchaseItem == null ? "-" : PurchaseItem.DeliveryTime.ToString("yy/MM/dd");
            //cell[13] = PurchaseItem == null ? "-" : PurchaseItem.CreateTime.ToString("yy/MM/dd");
            //cell[14] = Stock.InStockTime.ToString("yy/MM/dd");
            //cell[15] = PurchaseUser;
            //cell[16] = State;
            cell[9] = Stock.InStockQty.ToString();
            cell[10] = Stock.OutStockQty.ToString();
            //cell[19] = Stock.PlanQty.ToString();
            cell[11] = Warehouse;
            cell[12] = WarehousePosition;
         }
    }
}