using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.ViewModel
{
    public class TaskHourEmpViewModel
    {
        public int Id { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public int DepID { get; set; }
        public string DepName { get; set; }
        public string DocDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool Enable { get; set; }
        public string MoldNumber { get; set; }
        public string WorkType { get; set; }
        public string WorkTypeName { get; set; }
        public string BC { get; set; }
        public string MachineCode { get; set; }
        public int INTStatus { get; set; }
        public string Status { get; set; }
        public double Time { get; set; }
        public int ApprovalUser { get; set; }
        public string ApprovalTime { get; set; }
        public string CreateTime { get; set; }
    }
}
