using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class SupplierGroup
    {
        public int ID { get; set; }
        public string GroupName { get; set; }
        public string MailList { get; set; }
        public bool active { get; set; }
    }
}
