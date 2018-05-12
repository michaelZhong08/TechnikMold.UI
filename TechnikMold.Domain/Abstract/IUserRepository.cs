
/*
 * Create By:lechun1
 * 
 * Description: public interface for repository of TechnikSys.MoldManager.Domain.Entity.User 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;
using System.Net;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }

        int Save(User User);

        void Delete(int UserID);

        User GetUserByID(int UserID);

        User GetUserByName(string UserName);

        IEnumerable<User> FilterUser(string UserName="");

        NetworkCredential MailCredential(int UserID);


    }
}
