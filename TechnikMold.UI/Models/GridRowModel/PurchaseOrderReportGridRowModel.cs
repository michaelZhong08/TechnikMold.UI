using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikMold.UI.Models.ViewModel;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class PurchaseOrderReportGridRowModel
    {
        public string[] cell;
        #region region
        public PurchaseOrderReportGridRowModel(PurchaseItem Item, 
            string MainType, 
            string SubType, 
            string SupplierName, 
            string CostCenter, 
            string PurchaseOrder)
        {
            cell = new string[15];
            cell[0] = Item.PurchaseItemID.ToString();
            cell[1] = Item.DeliveryTime.ToString("yyyy-MM");
            cell[2] = CostCenter;
            switch (Item.ProcedureType)
            {
                case 0:
                    cell[3] = "";
                    break;
                case 10:
                    cell[3] = "制模";
                    break;
                case 20:
                    cell[3] = "修模";
                    break;
                case 30:
                    cell[3] = "耗材";
                    break;
            }
            cell[4] = MainType;
            cell[5] = SubType;
            cell[6] = SupplierName;
            cell[7] = Item.PartNumber;
            cell[8] = Item.Specification;
            cell[9] = Item.Quantity.ToString();
            cell[10] = Item.UnitPrice.ToString();
            cell[11] = Item.TotalPrice.ToString();
            cell[12] = Item.TaxRate.ToString() + "%";
            cell[13] = PurchaseOrder;
        }
        #endregion
        public PurchaseOrderReportGridRowModel(PDReportViewModel _item)
        {
            cell = new string[20];
            cell[0] = _item.PurchaseItemID.ToString();
            cell[1] = _item.事业部;
            cell[2] = _item.成本中心;
            cell[3] = _item.一级分类;
            cell[4] = _item.二级分类;
            cell[5] = _item.供应商;
            cell[6] = _item.模号;
            cell[7] = _item.规格;
            cell[8] = _item.数量.ToString();
            cell[9] = _item.单位;

            cell[10] = _item.含税单价.ToString();
            cell[11] = _item.含税总价.ToString();
            cell[12] = _item.税率.ToString()+"%";
            cell[13] = _item.未税总价.ToString();


            cell[14] = _item.订单;
            cell[15] = _item.请购单;
            cell[16] = _item.生成时间.ToString("yyyy-MM-dd");
            cell[17] = _item.预计交货日期.ToString("yyyy-MM-dd");
            cell[18] = _item.实际到达日期.ToString("yyyy-MM-dd");
            cell[19] = _item.备注;
            
        }
    }
}