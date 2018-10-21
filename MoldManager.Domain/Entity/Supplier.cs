/*
 * Create By:lechun1
 * 
 * Description:data of supplier
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public int Type { get; set; }
        public bool Enabled { get; set; }
        public DateTime FirstSupply { get; set; }

        public string Address { get; set; }
        public string Settlement { get; set; }
        public string TaxNo { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
        public string TaxRate { get; set; }
        public string JianSuo { get; set; }
        public string MachineCode { get; set; }
    }
}
