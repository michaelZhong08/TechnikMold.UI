
/*
 * Create By:lechun1
 * 
 * Description: public interface for repository of TechnikSys.MoldManager.Domain.Entity.Role 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IRoleRepository
    {
        IQueryable<Role> Roles { get; }

        int Save(Role Role);

        int Delete(int RoleID);
    }
}
