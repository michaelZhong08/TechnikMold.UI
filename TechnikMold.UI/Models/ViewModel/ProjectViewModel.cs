using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.ViewModel
{
    public class ProjectViewModel
    {
        public int ProjectID;
        public int ParentID;
        public int ProjectType;
        public Project Project;
        public IEnumerable<ProjectPhase> ProjectPhases;
        public IEnumerable<ProjectRole> ProjectRoles;
        public IEnumerable<Phase> Phases;
        public IEnumerable<Role> Roles;
        public ProjectViewModel(int Project_ID, int Parent, int Type, 
            IEnumerable<Phase> PhasesList, 
            IEnumerable<Role> RoleList, 
            Project ProjectTarget=null, 
            IEnumerable<ProjectPhase> ProjectPhasesList = null,
            IEnumerable<ProjectRole> ProjectRoleList = null)
        {
            ProjectID = Project_ID;
            ParentID = Parent;
            ProjectType = Type;
            Project = ProjectTarget;
            ProjectPhases = ProjectPhasesList;
            Phases = PhasesList;
            Roles = RoleList;
            ProjectRoles = ProjectRoleList;            
        }
    }
}