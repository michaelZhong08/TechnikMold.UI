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
    public class RoleRepository:IRoleRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<Role> Roles
        {
            
            get {
                return _context.Roles;
            }
        }

        public int Save(Role Role)
        {
            throw new NotImplementedException();
        }

        public int Delete(int RoleID)
        {
            throw new NotImplementedException();
        }
    }
}
