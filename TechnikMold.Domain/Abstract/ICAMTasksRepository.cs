using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ICAMTasksRepository
    {
        IQueryable<CAMTask> CAMTasks { get; }

        int Save(CAMTask CAMTask);

        int Upgrade(int CAMTaskID);

        IEnumerable<CAMTask> QueryByProjectID(int ProjectID);

        IEnumerable<CAMTask> QueryByUser(int UserID);

        CAMTask QueryByID(int TaskID);

        bool Claim(int CAMTaskID, int UserID);

        bool Release(int CAMTaskID, int UserID);

        bool Pause(int CAMTaskID);
    }
}
