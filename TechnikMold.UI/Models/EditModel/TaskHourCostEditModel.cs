using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models
{
    public class TaskHourCostEditModel
    {
        public IEnumerable<Department> Departments;
        public IEnumerable<TaskHourCost> TaskHourCosts;
    }
}