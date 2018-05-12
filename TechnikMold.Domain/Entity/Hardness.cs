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
    public class Hardness
    {
        public int HardnessID { get; set; }
        public string Value { get; set; }
        public int MaterialID { get; set; }
        public bool Enabled { get; set; }
    }
}
