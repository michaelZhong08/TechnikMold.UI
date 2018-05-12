using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class PartList
    {
        public int PartListID { get; set; }
        public string MoldNumber { get; set; }
        public int Version { get; set; }
        public bool Released { get; set; }
        public bool Enabled { get; set; }
        public int PrevVersion { get; set; }
        public bool Latest { get; set; }

        public PartList()
        {
            PartListID = 0;
            MoldNumber = "";
            Version = 1;
            Released = false;
            Enabled = true;
            PrevVersion = 0;
            Latest = true;
        }
    }
}
