using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class Base_DepPhase
    {
        public int Id { get; set; }
        public int DepId { get; set; }
        public int PhaseId { get; set; }
        public bool Enable { get; set; }
    }
}
