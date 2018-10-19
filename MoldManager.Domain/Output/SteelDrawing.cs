using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Output
{
    public class SteelDrawing
    {
        public string GroupName { get; set; }
        public double Time { get; set; }
        public DateTime IssueDate { get; set; }

        public bool DrawLock { get; set; }

        public bool Lastest { get; set; }
        public int GroupProgramID { get; set; }
    }
}
