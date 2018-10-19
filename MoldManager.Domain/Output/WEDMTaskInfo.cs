using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Output
{
    public class WEDMTaskInfo
    {
        #region WEDM_Task
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public int rev { get; set; }
        public int Priority { get; set; }
        public string Note { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int Qty { get; set; }
        public string FinishBy { get; set; }
        public bool HaveISO { get; set; }
        public string ISOName { get; set; }
        public int SettingID { get; set; }
        public DateTime PlanDate { get; set; }
        public DateTime PlanDate1 { get; set; }
        public DateTime PlanDate2 { get; set; }
        public string PlanNote { get; set; }
        public int MachineID { get; set; }
        public DateTime RecieveDate { get; set; }
        public string MoldNumber { get; set; }
        public int State { get; set; }
        public int Plan { get; set; }
        #endregion
        #region WEDM_Status
        public string StatusName { get; set; }
        #endregion
        #region WEDM_CAMSetting
        public string CADPartName { get; set; }
        public decimal? Time { get; set; }
        public string ThreeDPartName { get; set; }
        #endregion
    }
}
