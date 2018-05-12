using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class PartImportRecord
    {
        public int PartImportRecordID { get; set; }
        public string DataContent { get; set; }
        public DateTime ImportDate { get; set; }
        public int PartID { get; set; }
    }
}
