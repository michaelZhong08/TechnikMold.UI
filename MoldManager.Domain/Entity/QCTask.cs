using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class QCTask
    {
        public int QCTaskID { get; set; }
        public int TaskID { get; set; }
        public string DrawingFile { get; set; }
        public string TaskName { get; set; }
        public int TaskType { get; set; }
        public int Version { get; set; }
        public int Priority { get; set; }
        public int Quantity { get; set; }
        public DateTime ForecastTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string Memo { get; set; }
        public string StateMemo { get; set; }
        public int State { get; set; }
        public int ProjectID { get; set; }
        public int QCUser { get; set; }
        public string MoldNumber { get; set; }
        public bool Enabled { get; set; }

        public string Creator { get; set; }
        public string Finisher { get; set; }
        public string Deletor { get; set; }
        public DateTime DeleteTime { get; set; }

        public QCTask()
        {
            TaskID = 0;
            DrawingFile = "";
            TaskName = "";
            TaskType = 0;
            Version = 0;
            Priority = 0;
            Quantity = 1;
            ForecastTime = new DateTime(1900, 1, 1);
            CreateTime = DateTime.Now;
            StartTime = new DateTime(1900, 1, 1);
            FinishTime = new DateTime(1900, 1, 1);
            DeleteTime = new DateTime(1900, 1, 1);
            Memo = "";
            StateMemo = "";
            State = -99;
            QCUser = 0;
            MoldNumber = "";
            Enabled = true;;
        }
    }
}
