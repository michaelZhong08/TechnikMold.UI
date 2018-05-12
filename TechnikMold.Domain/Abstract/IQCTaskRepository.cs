using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IQCTaskRepository
    {
        IQueryable<QCTask> QCTasks { get; }

        int Save(QCTask QCTask, int Mode=0);

        QCTask QueryByTaskID(int TaskID);

        IEnumerable<QCTask> QueryByProjectID(int ProjectID, int State);

        QCTask QueryByID(int QCTaskID);

    }
}
