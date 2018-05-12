using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridRowModel
{
    public class SupplierGridRowModel
    {
        public string[] cell;

        public SupplierGridRowModel(Supplier Supplier)
        {
            cell = new string[9];
            cell[0] = Supplier.SupplierID.ToString();
            cell[1] = Supplier.Name;
            cell[2] = Supplier.FullName;
            cell[3] = Supplier.Address;
            cell[4] = Supplier.Bank;
            cell[5] = Supplier.Account;
            cell[6] = Supplier.TaxNo;
            cell[7] = Supplier.TaxRate;
            cell[8] = Supplier.Settlement;
        }
    }
}