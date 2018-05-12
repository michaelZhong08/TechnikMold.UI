using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.EditModel
{
    public class ProjectEditModel
    {
        public Project Project { get; set; }
        public IEnumerable<ProjectPhase> ProjectPhases { get; set; }
        public IEnumerable<ProjectRole> ProjectRoles { get; set; }
        public int Type;
    }
}