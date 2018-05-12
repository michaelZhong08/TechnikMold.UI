/*
 * Create By:lechun1
 * 
 * Description:data represent a stock change record
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class StockChange
    {
        public int StockChangeID { get; set; }
        public int StockPartID { get; set; }
        public int ChangeType { get; set; }
        public int Quantity { get; set; }
        public int UserID { get; set; }
        public int PurchaseRequestID { get; set; }
        public DateTime ChangeDate { get; set; }
        public int Operator { get; set; }
        public int ProjectID { get; set; }
        public string PartName { get; set; }
    }
}
