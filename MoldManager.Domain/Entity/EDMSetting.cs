using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class EDMSetting
    {
        public int EDMSettingID { get; set; }
        public int TaskID { get; set; }
        public string MoldNumber { get; set; }
        public string EleName { get; set; }
        public int EleState { get; set; }
        public DateTime CreateDate { get; set; }
        public string EDMOperator { get; set; }
        public int Flag { get; set; }
        public DateTime FinishDate { get; set; }
        public int Version { get; set; }

    }
}
