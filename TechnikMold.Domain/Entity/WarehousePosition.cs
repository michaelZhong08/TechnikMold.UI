using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class WarehousePosition
    {
        public int WarehousePositionID { get; set; }
        public int WarehouseID { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
