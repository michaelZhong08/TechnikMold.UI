using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class SupplierBrand
    {
        public int SupplierBrandID{get;set;}
        public int SupplierID { get; set; }
        public int BrandID { get; set; }
        public bool Enabled { get; set; }

        public SupplierBrand()
        {
            SupplierBrandID = 0;
            SupplierID = 0;
            BrandID = 0;
            Enabled = true;
        }

        public SupplierBrand(int Supplier_ID, int Brand_ID)
        {
            SupplierBrandID = 0;
            SupplierID = Supplier_ID;
            BrandID = Brand_ID;
            Enabled = true;

        }
    }
}
