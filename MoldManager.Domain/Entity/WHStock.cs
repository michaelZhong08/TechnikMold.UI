using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class WHStock
    {
        public int ID { get; set; }
        //[Key, Column(Order = 0)]
        public string PartNum { get; set; }
        //[Key, Column(Order = 1)]
        public int WarehouseID { get; set; }
        //[Key, Column(Order = 2)]
        public int WarehousePositionID { get; set; }
        public decimal Qty { get; set; }
        public decimal InStockQty { get; set; }
        public decimal OutStockQty { get; set; }
        public DateTime FInStockDate { get; set; }
        public DateTime LInStockDate { get; set; }
        public bool Enable { get; set; }
        //[Key, Column(Order = 3)]
        public int PurchaseType { get; set; }
        //[Key, Column(Order = 4)]
        public int PartID { get; set; }
        public int TaskID { get; set; }
        public int StockType { get; set; }
        public string MoldNumber { get; set; }
        public string Memo { get; set; }
    }
}
