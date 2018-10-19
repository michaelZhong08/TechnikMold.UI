using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class WEDMSetting
    {
        public int ID { get; set; }
        public string DrawName { get; set; }
        public string MoldName { get; set; }
        public string CADPartName { get; set; }
        public int Rev { get; set; }
        public string Precision { get; set; }
        public string Note { get; set; }
        public int FeatureCount { get; set; }
        public decimal Length { get; set; }
        public decimal Thickness { get; set; }
        public decimal Time { get; set; }
        public bool LastestFlag { get; set; }
        public bool ReleaseFlag { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime DeleteDate { get; set; }
        public bool active { get; set; }
        public bool QcPoint { get; set; }
        public string ThreeDPartName { get; set; }
    }
}
