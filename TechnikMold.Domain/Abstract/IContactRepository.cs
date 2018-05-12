using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IContactRepository
    {
        IQueryable<Contact> Contacts { get; }

        int Save(Contact Contact);

        void Delete(int ContactID);

        IEnumerable<Contact> QueryByOrganization(int OrganizationID, int ContactType=1);

        Contact QueryByID(int ContactID);

        void DeleteByOrganization(int OrganizationID, int ContactType=1);
    }
}
