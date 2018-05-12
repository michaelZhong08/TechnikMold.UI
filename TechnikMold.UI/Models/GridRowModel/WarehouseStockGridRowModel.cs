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
        public WarehouseStockGridRowModel(WarehouseStock Stock, 
            PurchaseItem PurchaseItem, 
            string RequestUser, 
            string PurchaseUser, 
            string State, 
            string PurchaseType,
            string StockType, 
            string Warehouse, 
            string WarehousePosition)
        {
            
            //if (PurchaseItem == null)
            //{
            //    PurchaseItem = new PurchaseItem();
            //}
            cell = new string[22];
            cell[0] = Stock.WarehouseStockID.ToString();
            cell[1] = Stock.Name;
            
            cell[2] = Stock.MaterialNumber;
            cell[3] = Stock.Specification;
            cell[4] = Stock.Material;

            if ((Stock.SafeQuantity>0)&&(Stock.Quantity<Stock.SafeQuantity)){
                cell[5] = "<font color='#ff0000'>"+Stock.Quantity.ToString()+"</font>";
            }else{
                cell[5] = Stock.Quantity.ToString();
            }
            
            cell[6] = Stock.SafeQuantity.ToString();
            cell[7] = PurchaseItem==null?"":PurchaseItem.SupplierName;
            cell[8] = PurchaseType;
            cell[9] = StockType;
            cell[10] = PurchaseItem == null ? "-" : PurchaseItem.PlanTime.ToString("yy/MM/dd");
            cell[11] = PurchaseItem == null ? "-" : PurchaseItem.RequireTime.ToString("yy/MM/dd");
            cell[12] = PurchaseItem == null ? "-" : PurchaseItem.DeliveryTime.ToString("yy/MM/dd");
            cell[13] = PurchaseItem == null ? "-" : PurchaseItem.CreateTime.ToString("yy/MM/dd");
            cell[14] = Stock.InStockTime.ToString("yy/MM/dd");
            cell[15] = PurchaseUser;
            cell[16] = State;
            cell[17] = Stock.InStockQty.ToString();
            cell[18] = Stock.OutStockQty.ToString() ;
            cell[19] = Stock.PlanQty.ToString();
            cell[20] = Warehouse;
            cell[21] = WarehousePosition;
         }
    }
}