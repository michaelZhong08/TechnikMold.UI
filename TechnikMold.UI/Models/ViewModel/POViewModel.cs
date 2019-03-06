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
        public PurchaseOrder PurchaseOrder;
        public Supplier Supplier;
        public Contact Contact;
        public string Buyer, Chker, Approval;
        public User PuUser;
        public POViewModel(IEnumerable<POContent> Contents, 
            PurchaseOrder Order, 
            Supplier POSupplier, 
            Contact POContact,
            User puUser,
            string buyer,string chker,string approval)
        {
            POContents = Contents;
            PurchaseOrder = Order;
            Supplier = POSupplier;
            Contact = POContact;
            Buyer = buyer;
            Chker = chker;
            Approval = approval;
            PuUser = puUser;
        }
    }
}