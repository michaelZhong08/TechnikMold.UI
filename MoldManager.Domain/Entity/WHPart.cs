using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class WHPart
    {
        public int ID { get; set; }
        //[Key]
        public string PartNum { get; set; }
        public string PartName { get; set; }
        public string Specification { get; set;}
        public int SafeQuantity { get; set; }
        public string Materials { get; set; }
        public string StockTypes { get; set; }
        public string PurchaseType { get; set; }
        public string MoldNumber { get; set; }
        public bool Enable { get; set; }
        public int CreateUserID { get; set; }
        public DateTime CreDate { get; set; }
        public int TaskID { get; set; }
        public int PartID { get; set; }
        public decimal PlanQty { get; set; }
    }
}
