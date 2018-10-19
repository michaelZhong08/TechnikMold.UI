using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class ProjectRecord
    {

        public int ProjectRecordID { get; set; }
        public int ProjectID { get; set; }
        public DateTime RecordDate { get; set; }
        public string RecordContent { get; set; }
        public string MoldNumber { get; set; }

    }
}
