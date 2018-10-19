using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class WEDMCutSpeed
    {
        public int ID { get; set; }
        public int TypeID { get; set; }
        public int Thickness { get; set; }
        public decimal CutSpeed { get; set; }
        public decimal Rate { get; set; }
        public decimal Stop { get; set; }
        public bool active { get; set; }
    }
}
