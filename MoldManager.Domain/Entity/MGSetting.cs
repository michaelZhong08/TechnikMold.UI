using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class MGSetting
    {
        public int ID { get; set; }
        public string DrawName { get; set; }
        public int Rev { get; set; }
        public string CADNames { get; set; }
        public string MoldName { get; set; }
        public int Qty { get; set; }
        public int ProcessType { get; set; }
        public decimal Time { get; set; }
        public string FeatureNote { get; set; }
        public bool LastestFlag { get; set; }
        public bool ReleaseFlag { get; set; }
        public string ItemNO { get; set; }
        public string Material { get; set; }
        public string HRC { get; set; }
        public string RawSize { get; set; }
        public string ReleaseBy { get; set; }
        public DateTime ReleaseDate { get; set; }

        public string DeleteBy { get; set; }
        public DateTime DeleteDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public bool active { get; set; }
        //State -100 新建 -99 已发布但任务未发布  0 生成任务
        public int State { get; set; }
    }
}
