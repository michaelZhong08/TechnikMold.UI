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
    public class ContactRepository:IContactRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<Contact> Contacts
        {
            get 
            {
                return _context.Contacts;
            }
        }

        public int Save(Contact Contact)
        {
            if (Contact.ContactID == 0)
            {
                _context.Contacts.Add(Contact);
            }
            else
            {
                Contact _dbEntry = _context.Contacts.Find(Contact.ContactID);
                if (_dbEntry != null)
                {
                    _dbEntry.ContactID = Contact.ContactID;
                    _dbEntry.FullName = Contact.FullName;
                    _dbEntry.ContactType = Contact.ContactType;
                    _dbEntry.OrganizationID = Contact.OrganizationID;
                    _dbEntry.Telephone = Contact.Telephone;
                    _dbEntry.Mobile = Contact.Mobile;
                    _dbEntry.Email = Contact.Email;
                    _dbEntry.Memo = Contact.Memo;
                    _dbEntry.Enabled = true;
                }
            }
            _context.SaveChanges();
            return Contact.ContactID;
        }

        public void Delete(int ContactID)
        {
            Contact _dbEntry = _context.Contacts.Find(ContactID);
            _dbEntry.Enabled = false;
            _context.SaveChanges();
        }


        /// <summary>
        /// Return contacts from one organization
        /// </summary>
        /// <param name="OrganizationID">Primary key of the organization</param>
        /// <param name="ContactType">
        /// Type of the contact
        /// 1:Supplier Contact
        /// 2:Customer Contact
        /// </param>
        /// <returns></returns>
        public IEnumerable<Contact> QueryByOrganization(int OrganizationID, int ContactType=1)
        {
            IEnumerable<Contact> _contacts = _context.Contacts
                .Where(c => c.OrganizationID == OrganizationID)
                .Where(c=>c.ContactType==ContactType)
                .Where(c=>c.Enabled==true);
            return _contacts;
        }


        public Contact QueryByID(int ContactID)
        {
            Contact _dbEntry = _context.Contacts.Find(ContactID);
            return _dbEntry;
        }


        public void DeleteByOrganization(int OrganizationID, int ContactType = 1)
        {
            IEnumerable<Contact> _contacts = QueryByOrganization(OrganizationID, ContactType);
            if (_contacts.Count() > 0) { 
                foreach (Contact _contact in _contacts)
                {
                    _contact.Enabled = false;
                }
            _context.SaveChanges();
            }
        }


    }
}
