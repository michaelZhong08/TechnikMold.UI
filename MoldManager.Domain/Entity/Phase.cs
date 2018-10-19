/*
 * Create By:lechun1
 * 
 * Description:data represent a project phase in template
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class Phase
    {
        public int PhaseID { get; set; }
        public string Name { get; set; }
        public int Sequence { get; set; }
        public bool Enabled { get; set; }
    }
}
