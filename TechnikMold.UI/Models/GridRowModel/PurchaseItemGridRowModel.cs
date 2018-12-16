using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Status;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class PurchaseItemGridRowModel
    {
        public string[] cell;
        public PurchaseItemGridRowModel(PurchaseItem PurchaseItem, 
            string PRNO, 
            string QRNO, 
            string PONO, 
            string PurchaseUser, 
            string PurchaseType,
            string RequestUser,
            string htmlTitle)
        {
            cell = new string[24];

            cell[0] = PurchaseItem.PurchaseItemID.ToString();
            cell[1] = PurchaseItem.Name;
            cell[2] = PurchaseItem.PartNumber;
            cell[3] = PurchaseItem.Specification;
            cell[4] = PurchaseItem.Quantity.ToString();
            cell[5] = Enum.GetName(typeof(PurchaseItemStatus), PurchaseItem.State);
            cell[6] = PurchaseType;

            if (PurchaseItem.PurchaseRequestID > 0)
            {
                cell[7] = "<a href='/Purchase/PRDetail?PurchaseRequestID=" + PurchaseItem.PurchaseRequestID + "'>"+PRNO+"</a>";
            }
            else
            {
                cell[7] = "-";
            }
            if (PurchaseItem.QuotationRequestID > 0)
            {
                cell[8] = "<a href='/Purchase/QRDetail?QuotationRequestID=" + PurchaseItem.QuotationRequestID + "'>"+QRNO+"</a>";
            }
            else
            {
                cell[8] = "-";
            }

            if (PurchaseItem.PurchaseOrderID > 0)
            {
                cell[9] = "<a href='/Purchase/PODetail?PurchaseOrderID=" + PurchaseItem.PurchaseOrderID + "'>"+PONO+"</a>";
            }
            else
            {
                cell[9] = "-";
            }
 
            cell[10] = PurchaseItem.SupplierName;
            cell[11] = PurchaseUser;
            cell[12] = PurchaseItem.UnitPrice.ToString();
            cell[13] = PurchaseItem.TotalPrice.ToString();
          
            cell[14] = PurchaseItem.OutStockQty.ToString();
            
            cell[15] = PurchaseItem.TotalPriceWT==0?"":PurchaseItem.TotalPriceWT.ToString();
            cell[16] = PurchaseItem.RequireTime == new DateTime(1900, 1, 1) ? "-" : PurchaseItem.RequireTime.ToString("yyyy-MM-dd");
            cell[17] = PurchaseItem.PlanTime == new DateTime(1900, 1, 1) ? "-" : PurchaseItem.PlanTime.ToString("yyyy-MM-dd");
            cell[18] = "<label class='Lab_PlanAJDate' title='"+htmlTitle+"'>" +(PurchaseItem.PlanAJTime == new DateTime(1900, 1, 1) ? "-" : PurchaseItem.PlanAJTime.ToString("yyyy-MM-dd"))+"</label>";

            cell[19] = RequestUser;
            cell[20] = PurchaseItem.DeliveryTime == new DateTime(1900, 1, 1) ? "-" : PurchaseItem.DeliveryTime.ToString("yyyy-MM-dd");
            cell[21] = PurchaseItem.InStockQty.ToString();
            cell[22] = PurchaseItem.UnitPriceWT.ToString();
            cell[23] = PurchaseItem.Memo;
        }
    }
}