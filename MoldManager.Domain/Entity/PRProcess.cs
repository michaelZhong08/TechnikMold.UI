/*
 * Create By:lechun1
 * 
 * Description:
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class PRProcess
    {
        public int PRProcessID { get; set; }
        public int PurchaseRequestID { get; set; }
        public int UserID { get; set; }
        public int ResponseType { get; set; }
        public string Memo { get; set; }
        public DateTime ProcessDate { get; set; }
    }
}
