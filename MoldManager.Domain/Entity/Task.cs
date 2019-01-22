/*
 * Create By:lechun1
 * 
 * Description:data of a task
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class Task
    {
        public int TaskID { get; set; }
        public string DrawingFile { get; set; }
        public string TaskName { get; set; }
        public int Version { get; set; }
        public string ProcessName { get; set; }
        public string Model { get; set; }
        public int R { get; set; }
        public int F { get; set; }
        public string HRC { get; set; }
        public string Material { get; set; }
        public double Time { get; set; }
        public string Raw { get; set; }
        public bool Prepared { get; set; }
        public int Priority { get; set; }
        public int Quantity { get; set; }
        public DateTime ForecastTime { get; set; }
        public DateTime AcceptTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime PlanTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ReleaseTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string Memo { get; set; }
        public string StateMemo { get; set; }
        public int State { get; set; }
        public int ProjectID { get; set; }
        public int TaskType { get; set; }
        public string Creator { get; set; }
        public int CADUser { get; set; }
        public int CAMUser { get; set; }
        public int WorkshopUser { get; set; }
        public int QCUser { get; set; }
        public bool PositionFinish { get; set; }
        public bool QCInfoFinish { get; set; }
        public int ProgramID { get; set; }
        public int OldID { get; set; }
        public string MoldNumber { get; set; }
        public bool Enabled { get; set; }
        public int PrevState { get; set; }
        public int FinishBy { get; set; }

        public Task()
        {
            TaskType = 0;
            TaskName = "";
            Version = 0;
            CreateTime = DateTime.Now;
            DrawingFile = "";
            ProcessName = "";
            Model = "";
            Material = "";
            HRC = "";
            Time = 0;
            Raw = "";
            Prepared = false;
            Priority = 0;
            Quantity = 1;
            ForecastTime = new DateTime(1900, 1, 1);
            PlanTime = new DateTime(1900, 1, 1);
            AcceptTime = new DateTime(1900, 1, 1);
            StartTime = new DateTime(1900, 1, 1);
            Memo = "";
            StateMemo = "";
            State = 0;
            ProjectID = 0;
            CADUser = 0;
            CAMUser = 0;
            WorkshopUser = 0;
            QCUser = 0;
            ReleaseTime = new DateTime(1900, 1, 1);
            FinishTime = new DateTime(1900, 1, 1);
            PositionFinish = false;
            QCInfoFinish = false;
            ProgramID = 0;
            OldID = 0;
            MoldNumber = "";
            Enabled = true;
            PrevState = 0;
            Creator = string.Empty;
        }
    }
}
