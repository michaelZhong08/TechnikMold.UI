/*
 * Create By:lechun1
 * 
 * Description:data represent a stock record
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class StockPart
    {
        public int StockPartID { get; set; }
        public int PartID { get; set; }
        public string PartName { get; set; }
        public string Specification { get; set; }
        public int InStockQty { get; set; }
        public int SafeQty { get; set; }
        public int ReserveQty { get; set; }
        public int PartTypeID { get; set; }
    }
}
