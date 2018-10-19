using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnikSys.MoldManager.UI.Models.ViewModel
{
    public class ProJPHViewModel
    {
        public int ProjectID { get; set; }
        public int? PhaseID { get; set; }
        public string Name { get; set; }
        public DateTime? ActualFinish { get; set; }
    }
}