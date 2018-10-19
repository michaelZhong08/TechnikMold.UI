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

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class Contact
    {
        public int ContactID { get; set; }
        public string FullName { get; set; }
        public int ContactType { get; set; }
        public int OrganizationID { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Memo { get; set; }
        public bool Enabled { get; set; }
    }
}
