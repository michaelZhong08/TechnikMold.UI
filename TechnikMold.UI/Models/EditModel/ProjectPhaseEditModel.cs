using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoldManager.WebUI.Models.EditModel
{
    public class ProjectPhaseEditModel
    {
        public int ProjectID { get; set; }
        public int PhaseID { get; set; }
        public int Reason { get; set; }
        public string Description { get; set; }
        public DateTime PlanCFinish { get; set; }
        public DateTime PlanFinish { get; set; }
        public string UserName { get; set; }
    }
}