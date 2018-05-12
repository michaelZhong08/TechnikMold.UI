using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class OutStockItem
    {
        public int OutStockItemID { get; set; }
        public int WHRequestID { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public string Specification { get; set; }
        public int Quantity { get; set; }
        public int OutStockFormID { get; set; }
        public int WHStockID { get; set; }
        public DateTime OutDate { get; set; }
        public string MoldNumber { get; set; }
    }
}
