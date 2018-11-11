using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;


namespace MoldManager.WebUI.Models.GridViewModel
{
    public class ProjectGridViewModel
    {         
        public int page;
        public int total;
        public int records;
        public List<ProjectGridRowModel> rows;
        public ProjectGridViewModel(IEnumerable<Project> Projects,
            IProjectPhaseRepository ProjectPhaseRepository, 
            IProjectRoleRepository ProjectRoleRepository,
            IAttachFileInfoRepository AttachFileInfoRepository,
            List<Phase> Phases, 
            int TotalProjects=0, 
            int PageNo=1, 
            int PageCount=20)
        {
            page = PageNo;
            total = TotalProjects / PageCount+1;
            records = TotalProjects*3;
            rows = new List<ProjectGridRowModel>();
             ProjectRole _role;
             string _flitter;
            foreach (Project _project in Projects)
            {
                _role= ProjectRoleRepository.ProjectRoles.Where(f=>f.ProjectID ==_project.ProjectID )
                    .Where(f=>f.RoleID ==3).FirstOrDefault();
                if (_role == null)
                {
                    _flitter = "";
                }
                else
                {
                    _flitter = _role.UserName;
                }
                int _attQty = AttachFileInfoRepository.GetAttachByObj(_project.ProjectID.ToString(), "Projects").Count();
                rows.AddRange(new ProjectGridRowModels(_project, ProjectPhaseRepository.GetProjectPhases(_project.ProjectID), _flitter, Phases, _attQty).ProjectRows);
            }         
        }       
    }
}