using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.ViewModel
{
    public class SimilarPurchaseItem
    {
        public string Name { get; set; }
        public string Supplier { get; set; }
        public double Price { get; set; }
        public string Date { get; set; }

        public SimilarPurchaseItem(PurchaseItem Item)
        {
            Name = Item.Name;
            Supplier = Item.SupplierName;
            Price = Item.UnitPrice;
            Date = Item.RequireTime.ToString("yyyy-MM-dd");
        }
    }
}