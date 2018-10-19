using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class CNCMachInfo
    {
        [Key]
        public int MachInfoID { get; set; }
        public string Model { get; set; }
        public int Version { get; set; }
        public string Position { get; set; }
        public string RoughName { get; set; }
        public string FinishName { get; set; }
        public double RoughGap { get; set; }
        public double FinishGap { get; set; }
        public double RoughTime { get; set; }
        public double FinishTime { get; set; }
        public int RoughCount { get; set; }
        public int FinishCount { get; set; }
        public int DrawIndex { get; set; }

        public string Surface { get; set; }
        public string ObitType { get; set; }
        public string MachType { get; set; }
        public string EDMStock { get; set; }
        public int CNCMethod { get; set; }
        public int EDMMethod { get; set; }

        public int AppendMethod { get; set; }
        public double SafetyHeight { get; set; }
        public bool QCPoint { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime IssueDate { get; set; }
        public int Lock { get; set; }
        public int DrawRev { get; set; }
        public bool PosCheck { get; set; }
    }
}
