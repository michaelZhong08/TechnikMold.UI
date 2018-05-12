using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class Charmill
    {
        public int CharmillID { get; set; }
        public string Name { get; set; }
        public string Material { get; set; }
        public string Surface { get; set; }
        public string Obit { get; set; }
        public double Max_Gap { get; set; }
        public double Min_Gap { get; set; }
        public int Program_Number { get; set; }
        public string Type { get; set; }
    }
}
