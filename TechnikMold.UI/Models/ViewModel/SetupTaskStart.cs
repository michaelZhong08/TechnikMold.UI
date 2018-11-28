using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnikMold.UI.Models.ViewModel
{
    public class SetupTaskStart
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string State { get; set; }
        public string MachinesCode { get; set; }
        public string MachinesName { get; set; }
        public int UserID  { get; set; }
        public string UserName  { get; set; }
        public decimal TotalTime { get; set; }
        public int Qty { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public int TaskHourID { get; set; }
        public string SemiTaskFlag { get; set; }
    }
}