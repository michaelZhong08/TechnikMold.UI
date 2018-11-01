using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ITaskTypeRepository
    {
        IQueryable<TaskType> TaskTypes { get; }
        int Save(TaskType model);
    }
}
