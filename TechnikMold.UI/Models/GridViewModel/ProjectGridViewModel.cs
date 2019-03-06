using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;
using System.Data;

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
            IProjectRepository ProjectRepository,
            List<Phase> Phases)
        {
            //int TotalProjects = 3000;
            ////int PageNo = 1,
            //int PageCount = 300;
            ////page = PageNo;
            //total = TotalProjects / PageCount + 1;
            //records = TotalProjects * 3;

            rows = new List<ProjectGridRowModel>();
            List<ProjectRole> _role;
            string _flitter;
            foreach (Project _project in Projects)
            {
                _role = ProjectRoleRepository.ProjectRoles.Where(f => f.ProjectID == _project.ProjectID).OrderBy(p=>p.RoleID).ToList();
                    //.Where(f => f.RoleID == 3).FirstOrDefault();
                //if (_role == null)
                //{
                //    _flitter = "";
                //}
                //else
                //{
                //    _flitter = _role.UserName;
                //}
                int _attQty = AttachFileInfoRepository.GetAttachByObj(_project.ProjectID.ToString(), "Projects").Count();
                string _mainProJName;
                if (_project.ParentID == 0)
                {
                    _mainProJName = _project.Name;
                }
                else
                {
                    Project _mainProJ = ProjectRepository.GetByID(_project.ParentID);
                    _mainProJName = _mainProJ.Name;
                }
                rows.AddRange(new ProjectGridRowModels(_project, ProjectPhaseRepository.GetProjectPhases(_project.ProjectID), _role, Phases, _attQty, _mainProJName).ProjectRows);
            }
        }
        public ProjectGridViewModel(DataTable dt)
        {
            rows = new List<ProjectGridRowModel>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    rows.Add(new ProjectGridRowModel(dr));
                }
            }
        }
    }
}