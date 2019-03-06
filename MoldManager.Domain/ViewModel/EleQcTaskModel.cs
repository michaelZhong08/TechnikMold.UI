using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.ViewModel
{
    public class EleQcTaskModel
    {
        public QCTask QcTask { get; set; }
        public string Material { get; set; }
        public DateTime CNCFinishTime { get; set; }
        public double Gap { get; set; }
    }
}
