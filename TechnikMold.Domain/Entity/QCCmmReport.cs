using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class QCCmmReport
    {
        public int QCCmmReportID { get; set; }
        public int QCTaskID { get; set; }
        public string ReportName { get; set; }
        public string CreateBy { get; set; }
        public string CreateComputer { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Enabled { get; set; }

        /// <summary>
        /// 0:CMM report
        /// 1:Picture report
        /// </summary>
        public int ReportType { get; set; }

        public QCCmmReport()
        {
            QCTaskID = 0;
            ReportName = "";
            CreateBy = "";
            CreateComputer = "";
            CreateDate = DateTime.Now;
            Enabled=true;
            ReportType = 0;
        }
    }
}
