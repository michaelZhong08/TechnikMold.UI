/*
 * Create By:lechun1
 * 
 * Description:data represent a phase of project 
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class ProjectPhase
    {
        public int ProjectPhaseID { get; set; }
        public int ProjectID { get; set; }
        public int PhaseID { get; set; }
        public DateTime PlanFinish { get; set; }
        public DateTime PlanCFinish { get; set; }
        public DateTime ActualFinish { get; set; }
        public bool MainChange { get; set; }
    }
}
