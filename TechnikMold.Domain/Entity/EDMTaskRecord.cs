using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class EDMTaskRecord
    {
        public int EDMTaskRecordID { get; set; }
        public int TaskID { get; set; }
        public string ElectrodeName { get; set; }
        public string EDMPartName { get; set; }
        public bool Finished { get; set; }

        public EDMTaskRecord()
        {
            EDMTaskRecordID = 0;
            TaskID = 0;
            ElectrodeName = "";
            EDMPartName = "";
            Finished = true;
        }
    }
}
