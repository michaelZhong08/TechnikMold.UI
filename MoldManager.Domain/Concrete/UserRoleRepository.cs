using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;


namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IQueryable<UserRole> UserRoles
        {
            get { return _context.UserRoles; }
        }

        public int Save(UserRole UserRole)
        {
            UserRole _dbEntry;
            bool _isNew = false;
            if (UserRole.UserRoleID == 0)
            {
                _dbEntry = _context.UserRoles.Where(u => u.UserID == UserRole.UserID)
                    .Where(u => u.DepartmentID == UserRole.DepartmentID)
                    .Where(u => u.PositionID == UserRole.PositionID)
                    .Where(u => u.Enabled == true).FirstOrDefault();
                if (_dbEntry == null)
                {
                    _isNew = true;
                    _context.UserRoles.Add(UserRole);
                }
                else
                {
                    _dbEntry.DefaultRole = UserRole.DefaultRole;
                }
            }
            else
            {
                _dbEntry = _context.UserRoles.Find(UserRole.UserRoleID);
                if (_dbEntry != null)
                {
                    _dbEntry.UserID = UserRole.UserID;
                    _dbEntry.DepartmentID = UserRole.DepartmentID;
                    _dbEntry.PositionID = UserRole.PositionID;
                    _dbEntry.DefaultRole = UserRole.DefaultRole;
                    _dbEntry.Enabled = UserRole.Enabled;
                }
            }
            _context.SaveChanges();
            if (_isNew)
            {
                return UserRole.UserRoleID;
            }
            else
            {
                return _dbEntry.UserRoleID;
            }
        }




        public void Delete(int UserRoleID)
        {
            UserRole _dbEntry = _context.UserRoles.Find(UserRoleID);
            _dbEntry.Enabled = false;
            _context.SaveChanges();
        }

        public void DeleteAll(int UserID)
        {
            IEnumerable<UserRole> _userRoles = GetUserRoles(UserID);
            foreach (UserRole _userRole in UserRoles)
            {
                Delete(_userRole.UserRoleID);
            }
        }

        public IEnumerable<UserRole> GetUserRoles(int UserID)
        {
            return _context.UserRoles.Where(u => u.UserID == UserID).Where(u => u.Enabled == true).OrderBy(u=>u.UserRoleID);
        }


        public void SetDefault(int UserRoleID)
        {
            UserRole _dbEntry = _context.UserRoles.Find(UserRoleID);
            IEnumerable<UserRole> _userRoles = GetUserRoles(_dbEntry.UserID);
            foreach (UserRole _userRole in _userRoles)
            {
                _userRole.DefaultRole = false;
            }
            _dbEntry.DefaultRole = true;
            _context.SaveChanges();
        }

        public UserRole QueryByID(int UserRoleID)
        {
            return _context.UserRoles.Find(UserRoleID);
        }
    }
}
