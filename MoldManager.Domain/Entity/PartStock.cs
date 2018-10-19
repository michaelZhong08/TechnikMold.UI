using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class PartStock
    {
        public int PartStockID { get; set; }
        public string Name { get; set; }
        public string RawNO { get; set; }
        public int Qty { get; set; }
        public string Supplier { get; set; }
        public string Material { get; set; }
        public string Remark { get; set; }
        public string ItemNO { get; set; }
        public string Category { get; set; }
        public string PartNumber { get; set; }
        public bool Active { get; set; }

    }
}
