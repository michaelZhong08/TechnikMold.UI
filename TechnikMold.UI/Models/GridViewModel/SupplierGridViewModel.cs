using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class SupplierGridViewModel
    {
        public List<SupplierGridRowModel> rows;
        public int Page;
        public int Total;
        public int Records;

        public SupplierGridViewModel(IEnumerable<Supplier> Suppliers)
        {
            rows = new List<SupplierGridRowModel>();
            Page = 1;
            Total = 200;
            Records = 20;
            foreach(Supplier _supplier in Suppliers){
                SupplierGridRowModel _row = new SupplierGridRowModel(_supplier);
                rows.Add(_row);
            }
        }
    }
}