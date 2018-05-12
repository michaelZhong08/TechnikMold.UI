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
using System.Net;



namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class UserRepository : IUserRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<User> Users
        {
            get
            {
                return _context.Users;
            }
        }

        public int Save(User User)
        {
            if (User.UserID == 0)
            {
                _context.Users.Add(User);
            }
            else
            {
                User _dbEntry = _context.Users.Where(u => u.UserID == User.UserID).FirstOrDefault();
                if (_dbEntry != null)
                {
                    _dbEntry.FullName = User.FullName;
                    _dbEntry.LogonName = User.LogonName;
                    _dbEntry.DepartmentID = User.DepartmentID;
                    _dbEntry.Email = User.Email;
                    _dbEntry.Extension = User.Extension;
                    _dbEntry.Mobile = User.Mobile;
                    _dbEntry.Enabled = User.Enabled;
                    _dbEntry.PositionID = User.PositionID;
                }
            }
            _context.SaveChanges();
            return User.UserID;
        }

        public void Delete(int UserID)
        {
            User _dbEntry = _context.Users.Find(UserID);
            if (_dbEntry != null)
            {
                _dbEntry.Enabled = false;
                _context.SaveChanges();
            }

        }


        public User GetUserByID(int UserID)
        {
            return _context.Users.Find(UserID);

        }

        public User GetUserByName(string UserName)
        {
            User _user;
            try { 
                _user = _context.Users.Where(u => u.LogonName.ToLower()
                    .Trim()== UserName.ToLower().Trim()).Where(u=>u.Enabled==true).FirstOrDefault();
            }
            catch
            {
                _user = null;
            }
            return _user;
        }


        public IEnumerable<User> FilterUser(string UserName = "")
        {
            IEnumerable<User> _users = _context.Users.Where(u => u.FullName.Contains(UserName)).Where(u=>u.Enabled==true);
            return _users;
        }




        public NetworkCredential MailCredential(int UserID)
        {
            User _user =GetUserByID(UserID);
            return new NetworkCredential(_user.EmailUser, _user.EmailPassword);
        }
    }
}
