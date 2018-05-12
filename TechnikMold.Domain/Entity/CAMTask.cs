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
    public class CAMTask
    {
        public int CAMTaskID { get; set; }
        public string DrawingFile { get; set; }
        public string TaskName { get; set; }
        public int Version { get; set; }
        public int UserID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ProjectID { get; set; }
        public string MoldNumber { get; set; }
        public int CADUserID { get; set; }
        public int ReleaseUserID { get; set; }
        public DateTime AcceptDate { get; set; }
        //Represents the task status
        //1:in work
        //2:released
        //9:not latest version
        public int State { get; set; }
        public int TaskType { get; set; }
    }
}
