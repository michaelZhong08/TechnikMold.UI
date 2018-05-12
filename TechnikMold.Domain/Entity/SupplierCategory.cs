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
    public class SupplierCategory
    {
        public int SupplierCategoryID { get; set; }
        public int SupplierID { get; set; }
        public int PartTypeID { get; set; }
        public bool Enabled { get; set; }
    }
}
