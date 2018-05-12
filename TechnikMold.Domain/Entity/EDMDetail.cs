using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class EDMDetail
    {

        public int EDMDetailID { get; set; }
        public string EleDetail { get; set; }
        public int TaskID { get; set; }
        public string CADDetail { get; set; }
        public string SettingName { get; set; }
        public int Version { get; set; }
        public string ModifyName { get; set; }
        public int ModifyCount { get; set; }
        public int CADCount { get; set; }
        public DateTime CreateDate { get; set; }
        public string Designer { get; set; }
        public int Lock { get; set; }
        public bool Expire { get; set; }
        public string MoldName { get; set; }
        public int EleCount { get; set; }
        public bool QCPoint { get; set; }

    }

}
