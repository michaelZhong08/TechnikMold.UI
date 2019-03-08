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
        TaskHour GetCurTHBySemiTaskFlag(string SemiTaskFlag);
        List<TaskHour> GetCurTHsBySemiTaskFlag(string SemiTaskFlag,int TaskType=0);
        string GetOperaterBySemiTaskFlag(string SemiTaskFlag);
        string GetMachineBySemiTaskFlag(string SemiTaskFlag);
        List<TaskHour> GetCurTHsByMCode(string MCode);
    }
}
