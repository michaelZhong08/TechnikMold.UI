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
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class ProjectRoleRepository:IProjectRoleRepository
    {
        private EFDbContext _context = new EFDbContext();


        public IQueryable<ProjectRole> ProjectRoles
        {
            get {
                return _context.ProjectRoles;
            }
        }

        public int Save(ProjectRole ProjectRole)
        {
            ProjectRole _role = _context.ProjectRoles.Where(p => p.ProjectID == ProjectRole.ProjectID).Where(p => p.RoleID == ProjectRole.RoleID).FirstOrDefault();
            if (_role == null)
            {
                _context.ProjectRoles.Add(ProjectRole);
            }
            else
            {
                _role.UserID = ProjectRole.UserID;
                _role.UserName = ProjectRole.UserName;
            }
            //if (ProjectRole.ProjectRoleID == 0)
            //{
                
            //}
            //else
            //{
            //    ProjectRole _dbEntry = _context.ProjectRoles.Find(ProjectRole.ProjectRoleID);
            //    if (_dbEntry!=null)
            //    {
            //        _dbEntry.ProjectID = ProjectRole.ProjectID;
            //        _dbEntry.RoleID = ProjectRole.RoleID;
            //        _dbEntry.UserID = ProjectRole.UserID;
            //        _dbEntry.UserName = ProjectRole.UserName;
            //    }
            //}
            _context.SaveChanges();
            return ProjectRole.ProjectRoleID;
        }


        public IEnumerable<ProjectRole> QueryByProjectID(int ProjectID)
        {
            return _context.ProjectRoles.Where(p => p.ProjectID == ProjectID);
        }
    }
}
