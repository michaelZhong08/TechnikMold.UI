using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    [Table("TaskHoursEmp")]
    public class TaskHoursEmp
    {
        public int Id { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public int DepID { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Enable { get; set; }
        public string MoldNumber { get; set; }
        public string WorkType { get; set; }
        public string BC { get; set; }
        public string MachineCode { get; set; }
        public int Status { get; set; }
        public double Time { get; set; }
        public int ApprovalUser { get; set; }
        public DateTime ApprovalTime { get; set; }
    }
}
