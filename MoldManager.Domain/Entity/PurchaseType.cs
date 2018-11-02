using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class PurchaseType
    {
        public int PurchaseTypeID { get; set; }
        public string Name { get; set; }
        public int ParentTypeID { get; set; }
        public string ShortName { get; set; }
        public string TaskType { get; set; }
        public int DefaultPeriod { get; set; }
        public bool Enabled { get; set; }
        public string DepID { get; set; }
    }
}
