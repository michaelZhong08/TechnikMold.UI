using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Entity
{
    public class TaskHourCost
    {
        public int TaskHourCostID { get; set; }
        public int DepartmentID { get; set; }
        public double PeopleCost { get; set; }
        public double DeviceCost { get; set; }
    }
}
