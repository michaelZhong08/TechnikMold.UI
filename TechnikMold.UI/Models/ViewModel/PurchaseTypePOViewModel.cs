using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.ViewModel
{
    public class PurchaseTypePOViewModel
    {
        public PurchaseType PurchaseType;
        public IEnumerable<PurchaseOrder> PurchaseOrders;
    }
}