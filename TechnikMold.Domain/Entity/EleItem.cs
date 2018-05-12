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
    public class EleItem
    {
        public int ELEItemID { get; set; }
        public int TaskID { get; set; }
        public int EDMItemID { get; set; }
        public string LabelName { get; set; }
        public string Raw { get; set; }
        public bool Ready { get; set; }
        public bool Required { get; set; }
        public string PartPosition { get; set; }
        public bool Finished { get; set; }

    }
}
