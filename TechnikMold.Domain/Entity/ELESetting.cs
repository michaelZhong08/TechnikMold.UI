using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class ELESetting
    {
        public string edm_setting_name { get; set; }
        public string MOLD_NAME { get; set; }
        public string ELE_NAMES { get; set; }
        public int SETING_REV { get; set; }
        public int ELE_COUNT { get; set; }
        public string ELE_MODIFY_NAMES { get; set; }
        public int ELE_MODIFY_COUNT { get; set; }
        public string PROJECT_NAME { get; set; }
        public int DRAWING_ID { get; set; }
        public int DrawingType { get; set; }
        public string CAD_NAMES { get; set; }
        public int CAD_COUNT { get; set; }
        public string DESIGNER { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public int ELE_SETTING_LOCK { get; set; }
        public int DRAW_OLD_STATUS { get; set; }
        public DateTime Delete_time { get; set; }
        public string Delete_person { get; set; }
        public DateTime Issue_date { get; set; }
        public string Issue_person { get; set; }
        public string Undo_person { get; set; }
        public DateTime Undo_date { get; set; }
        public bool QCPoint { get; set; }
        public bool PosCheck { get; set; }	
    }
}
