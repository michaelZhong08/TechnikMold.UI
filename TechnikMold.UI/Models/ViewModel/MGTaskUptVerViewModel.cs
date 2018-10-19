using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnikMold.UI.Models.ViewModel
{
    public class MGTaskUptVerViewModel
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string CAM { get; set; }
        public string Technology { get; set; }
        public int Qty { get; set; }
        public string DrawingFile { get; set; }
    }
}