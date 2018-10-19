using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IUserRoleRepository
    {
        IQueryable<UserRole> UserRoles { get; }

        int Save(UserRole UserRole);

        void Delete(int UserRoleID);

        void DeleteAll(int UserID);

        IEnumerable<UserRole> GetUserRoles(int UserID);

        void SetDefault(int UserRoleID);

        UserRole QueryByID(int UserRoleID);
    }
}
