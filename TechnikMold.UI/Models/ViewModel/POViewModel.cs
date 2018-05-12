using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.ViewModel
{
    public class POViewModel
    {
        public IEnumerable<POContent> POContents;
        public User PuUser;
        public PurchaseOrder PurchaseOrder;
        public Supplier Supplier;
        public Contact Contact;
        public POViewModel(IEnumerable<POContent> Contents, 
            PurchaseOrder Order, 
            User Responisble, 
            Supplier POSupplier, 
            Contact POContact)
        {
            POContents = Contents;
            PurchaseOrder = Order;
            PuUser = Responisble;
            Supplier = POSupplier;
            Contact = POContact;
        }
    }
}