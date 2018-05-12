using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class QuotationRequest
    {
        public int QuotationRequestID { get; set; }
        public string QuotationNumber { get; set; }
        public int ProjectID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime RequestDate { get; set; }
        public int PurchaseUserID { get; set; }
        public DateTime DueDate { get; set; }
        public bool Enabled { get; set; }
        public int State { get; set; }
        public int PurchaseRequestID { get; set; }

    }
}
