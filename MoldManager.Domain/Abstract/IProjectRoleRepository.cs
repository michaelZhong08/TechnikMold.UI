using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IProjectRoleRepository
    {
        IQueryable<ProjectRole> ProjectRoles { get; }

        int Save(ProjectRole ProjectRole);

        IEnumerable<ProjectRole> QueryByProjectID(int ProjectID);

    }
}
