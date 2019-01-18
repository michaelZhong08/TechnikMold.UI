
/*
 * Create By:lechun1
 * 
 * Description: public interface for repository of TechnikSys.MoldManager.Domain.Entity.Task 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ITaskRepository
    {
        IQueryable<TechnikSys.MoldManager.Domain.Entity.Task> Tasks { get; }

        int Save(TechnikSys.MoldManager.Domain.Entity.Task Task);

        IEnumerable<Task> QueryByProject(int ProjectID, int TaskType, bool Enabled=true);

        Task QueryByTaskID(int TaskID);

        void Claim(int TaskID, int UserID);

        void Release(int TaskID);

        void DeleteByCAM(int TaskID);

        void Queue(int TaskID);

        void Start(int TaskID, int UserID);

        void Pause(int TaskID);

        void UnPause(int TaskID);

        void CancelOutSource(int TaskID);

        void OutSource(int TaskID);

        void Stop(int TaskID);

        void AcceptItem(int TaskID);

        void Finish(int TaskID,string FinishBy);

        void PositonFinish(int TaskID);

        void QCInfoFinish(int TaskID);

        void Priority(int TaskID, int Level);
        
        IEnumerable<Task> QueryByState(int TaskType, int ProjectID = 0, int State = 1);

        IEnumerable<Task> QueryByProgramNumber(IEnumerable<int> ProgramIDs);

        IEnumerable<Task> QueryByGroupID(int GroupID);

        Task QueryByNameVersion(string Name, int Version, int TaskType=-1, bool Enabled=true);

        void UpdateDrawing(string DrawName,bool IsContain2D=false, string DrawType="CAM");

        IEnumerable<Task> QueryByName(string TaskName);

        Task QueryByModelVersion(string Model, int Version, int TaskType = -1, bool Enabled = true);

        void FinishTask(int TaskID, int TargetState, int WorkshopUser);

        int AddNewTask(Task Task);

        IEnumerable<Task> QueryByMoldNumber(string MoldNumber, int TaskType, bool Enabled = true);
        string GetMaxVerDrawFile(string TaskName);
        int GetMaxVerMGTask(string TaskName,int ProcessType=0);
        void UpdateTaskTime(int taskID, double time);
        void DeleteTask(int TaskID, string StateMemo);

    }
}
