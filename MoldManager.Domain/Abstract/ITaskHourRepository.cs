
/*
 * Create By:lechun1
 * 
 * Description: public interface for repository of TechnikSys.MoldManager.Domain.Entity.TaskHour
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ITaskHourRepository
    {
        IQueryable<TaskHour> TaskHours { get; }

        int Save(TaskHour TaskHour);
        TaskHour GetCurTHByTaskID(int TaskID);
        decimal GetTotalHourByTaskID(int TaskID);
        string GetOperaterByTaskID(int TaskID);
        string GetMachineByTask(int TaskID);
    }
}
