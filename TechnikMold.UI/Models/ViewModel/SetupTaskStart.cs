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
        public decimal TotalTime { get; set; }
    }
}