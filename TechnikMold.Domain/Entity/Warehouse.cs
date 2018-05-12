/*
 * Create By:lechun1
 * 
 * Description:
 * Data table manages the warehouse information
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public  class Warehouse
    {
        public int WarehouseID { get; set; }
        public string Name { get; set; }
        public string Memo { get; set; }
        public bool Enabled { get; set; }
    }
}
