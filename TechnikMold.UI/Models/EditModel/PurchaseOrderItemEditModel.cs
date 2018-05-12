using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoldManager.WebUI.Models.EditModel
{
    public class PurchaseOrderItemEditModel
    {
        public int PurchaseItemID { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double SubTotal { get; set; }
        public DateTime PlanTime { get; set; }
    }
}