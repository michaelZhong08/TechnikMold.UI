using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class OutStockForm
    {
        public int OutStockFormID { get; set; }
        public int RequestID { get; set; }
        public int WHUserID { get; set; }
        public string FormName { get; set; }
        public DateTime CreateTime { get; set; }
        public int UserID { get; set; }
    }
}
