/*
 * Create By:lechun1
 * 
 * Description: data of task hour record
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class TaskHour
    {
        public int TaskHourID { get; set; }
        public int TaskID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public int RecordType { get; set; }
    }
}
