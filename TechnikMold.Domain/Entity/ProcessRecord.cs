/*
 * Create By:lechun1
 * 
 * Description:
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class ProcessRecord
    {
        //Primary key of record
        public int ProcessRecordID { get; set; }
        //For general record usage, processtype defines the record type:
        //1:Purchase Record
        //2:Project Task Record
        public int ProcessType { get; set; }
        //Represents the primary key of different process types:
        //Purchase process: Purchase RequestID
        //Project Task: TaskID
        public int ProcessID { get; set; }
        //User who did the operation
        public int UserID { get; set; }
        //Process message, for example: UserA created purchase request T0000001
        public string Message { get; set; }
        //Date of operation
        public DateTime RecordDate { get; set; }
    }
}
