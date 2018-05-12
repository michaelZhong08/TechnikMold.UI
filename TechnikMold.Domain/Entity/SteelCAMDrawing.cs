using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class SteelCAMDrawing
    {
        public int SteelCAMDrawingID { get; set; }
        public string FullPartName { get; set; }
        public string DrawName { get; set; }
        public int DrawREV { get; set; }
        public string CADPartName { get; set; }
        public string MoldName { get; set; }
        public DateTime CreateDate { get; set; }
        public bool DrawLock { get; set; }
        public bool LastestFlag { get; set; }
        public int NCID { get; set; }
        public string Programmer { get; set; }
        public string IssuePerson { get; set; }
        public DateTime IssueDate { get; set; }
        public string Undo_person { get; set; }
        public DateTime Undo_date { get; set; }
        public DateTime Delete_time { get; set; }
        public string Delete_person { get; set; }
        public bool active { get; set; }
        public bool QCPoint { get; set; }


    }
}
