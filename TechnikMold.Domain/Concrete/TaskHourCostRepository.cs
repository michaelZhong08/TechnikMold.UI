using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class TaskHourCostRepository:ITaskHourCostRepository 
    {

        private EFDbContext _context = new EFDbContext();
        public IQueryable<TaskHourCost> TaskHourCosts
        {
            get {
                return _context.TaskHourCosts;
            }
        }

        public int Save(TaskHourCost TaskHourCost)
        {
            if (TaskHourCost.TaskHourCostID == 0)
            {
                _context.TaskHourCosts.Add(TaskHourCost);
            }
            else
            {
                TaskHourCost _dbEntry = _context.TaskHourCosts.Where(t => t.TaskHourCostID == TaskHourCost.TaskHourCostID).FirstOrDefault ();
                if (_dbEntry != null)
                {
                    _dbEntry.DepartmentID = TaskHourCost.DepartmentID;
                    _dbEntry.PeopleCost = TaskHourCost.PeopleCost;
                    _dbEntry.DeviceCost = TaskHourCost.DeviceCost;
                }
                
            }
            _context.SaveChanges();
            return TaskHourCost.TaskHourCostID;
        }
    }
}
