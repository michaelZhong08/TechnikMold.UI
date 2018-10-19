using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class WEDMPrecision
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int TrimQty { get; set; }
        public bool active { get; set; }
    }
}
