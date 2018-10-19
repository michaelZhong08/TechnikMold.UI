/*
 * Create By:lechun1
 * 
 * Description:data represents a phase modification record
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class PhaseModification
    {
        public int PhaseModificationID { get; set; }
        public int ProjectPhaseID  { get; set; }
        public DateTime ModifyDate { get; set; }
        public string User { get; set; }
        public DateTime BeforeModify { get; set; }
        public DateTime AfterModify { get; set; }
        public int Reason { get; set; }
        public string Description { get; set; }
    }
}
