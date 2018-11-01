using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class TaskTypeRepository:ITaskTypeRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<TaskType> TaskTypes
        {
            get { return _context.TaskTypes; }
        }
        public int Save(TaskType model)
        {
            return 1;
        }
    }
}
