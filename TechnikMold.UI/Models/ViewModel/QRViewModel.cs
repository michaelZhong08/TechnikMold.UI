using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.ViewModel
{
    public class QRViewModel
    {
        public IEnumerable<QRContent> QRContents;
        public User PuUser;
        public QuotationRequest QuotationRequest;

        public QRViewModel(IEnumerable<QRContent> Contents, 
            User Contact,
            QuotationRequest RequestInfo)
        {
                QRContents = Contents;
                PuUser = Contact;
                QuotationRequest = RequestInfo;
        }

    }
}