using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class Machine
    {
        public int MachineID { get; set; }
        public string Name { get; set; }
        public string IPAddress { get; set; }
        public int System_3R { get; set; }
        public int Pallet { get; set; }

        public string PointDescribe { get; set; }
        public int SystemType { get; set; }
        public bool Enabled { get; set; }
    }
}
