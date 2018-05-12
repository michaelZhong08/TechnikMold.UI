/*
 * Create By:lechun1
 * 
 * Description:
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Status;



namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class TaskRepository:ITaskRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<Task> Tasks
        {
            get { return _context.Tasks; }
        }

        public int Save(Task Task)
        {
            Task _dbEntry = null;
            bool _isNew = false;
            
            if (Task.TaskID == 0)
            {
                _dbEntry = QueryByNameVersion(Task.TaskName, Task.Version);

                if (_dbEntry == null)
                {
                    _isNew = true;
                    Task.DrawingFile = Task.DrawingFile == null ? "" : Task.DrawingFile;
                    Task.ProcessName = Task.ProcessName == null ? "" : Task.ProcessName;
                    Task.Model = Task.Model == null ? "" : Task.Model;
                    Task.HRC = Task.HRC == null ? "" : Task.HRC;
                    Task.Material = Task.Material == null ? "" : Task.Material;
                    Task.State = Task.State == 0 ? 0 : Task.State;
                    Task.Raw = Task.Raw == null ? "" : Task.Raw;

                    Task.Quantity = Task.Quantity == 0 ? 1 : Task.Quantity;
                    Task.ForecastTime = Task.ForecastTime == null ? new DateTime() : Task.ForecastTime;
                    Task.AcceptTime = Task.AcceptTime == null ? new DateTime() : Task.AcceptTime;
                    Task.PlanTime = Task.PlanTime == null ? new DateTime() : Task.PlanTime;
                    Task.StartTime = Task.StartTime == null ? new DateTime() : Task.StartTime;
                    Task.Memo = Task.Memo == null ? "" : Task.Memo;
                    Task.StateMemo = Task.StateMemo == null ? "" : Task.StateMemo;
                    Task.State = -99;///If this is a new task, set the task in "Not Released" state;
                    Task.PrevState = -99;
                    _context.Tasks.Add(Task);
                }
                else
                {
                    
                    _dbEntry.DrawingFile = Task.DrawingFile == null ? "" : Task.DrawingFile;
                    _dbEntry.TaskName = Task.TaskName;
                    _dbEntry.Version = Task.Version;
                    _dbEntry.ProcessName = Task.ProcessName == null ? "" : Task.ProcessName;
                    _dbEntry.Model = Task.Model == null ? "" : Task.Model;
                    _dbEntry.R = Task.R;
                    _dbEntry.F = Task.F;
                    _dbEntry.HRC = Task.HRC == null ? "" : Task.HRC;
                    _dbEntry.Material = Task.Material == null ? "" : Task.Material;
                    _dbEntry.Time = Task.Time;
                    _dbEntry.Raw = Task.Raw == null ? "" : Task.Raw;
                    _dbEntry.Prepared = Task.Prepared;
                    _dbEntry.Priority = Task.Priority;
                    _dbEntry.Quantity = Task.Quantity;
                    //_dbEntry.ForecastTime=Task.ForecastTime;
                    //_dbEntry.AcceptTime=Task.AcceptTime;
                    //_dbEntry.CreateTime=Task.CreateTime;
                    //_dbEntry.PlanTime=Task.PlanTime;
                    //_dbEntry.StartTime=Task.StartTime;
                    //_dbEntry.ReleaseTime = Task.ReleaseTime;
                    //_dbEntry.FinishTime = Task.FinishTime;
                    _dbEntry.Memo = Task.Memo == null ? "" : Task.Memo;
                    _dbEntry.CreateTime = DateTime.Now;
                    _dbEntry.StateMemo = Task.StateMemo == null ? "" : Task.StateMemo;
                    _dbEntry.State = Task.State;
                    _dbEntry.ProjectID = Task.ProjectID;
                    //_dbEntry.Creator=Task.Creator;
                    _dbEntry.TaskType = Task.TaskType;
                    _dbEntry.ProgramID = Task.ProgramID;
                    _dbEntry.CADUser = Task.CADUser;
                    _dbEntry.CAMUser = Task.CAMUser;
                    //_dbEntry.WorkshopUser = Task.WorkshopUser;
                    //_dbEntry.QCUser = Task.QCUser;
                    _dbEntry.Enabled = Task.Enabled;
                    _dbEntry.QCInfoFinish = Task.QCInfoFinish;
                    _dbEntry.PositionFinish = Task.PositionFinish;
                    _dbEntry.State = -99;
                    _dbEntry.PrevState = -99;
                }
            }
            else
            {
                _dbEntry = _context.Tasks.Find(Task.TaskID);
                if (_dbEntry != null)
                {
                    _dbEntry.DrawingFile=Task.DrawingFile==null?"":Task.DrawingFile;
                    _dbEntry.TaskName=Task.TaskName;
                    _dbEntry.Version=Task.Version;
                    _dbEntry.ProcessName=Task.ProcessName==null?"":Task.ProcessName;
                    _dbEntry.Model = Task.Model == null ? "" : Task.Model;
                    _dbEntry.R=Task.R;
                    _dbEntry.F=Task.F;
                    _dbEntry.HRC = Task.HRC == null ? "" : Task.HRC;
                    _dbEntry.Material = Task.Material == null ? "" : Task.Material;
                    _dbEntry.Time=Task.Time;
                    _dbEntry.Raw = Task.Raw == null ? "" : Task.Raw;
                    _dbEntry.Prepared=Task.Prepared;
                    _dbEntry.Priority=Task.Priority;
                    _dbEntry.Quantity=Task.Quantity;
                    
                    _dbEntry.Memo=Task.Memo==null?"":Task.Memo;

                    _dbEntry.StateMemo = Task.StateMemo == null ? "" : Task.StateMemo;
                    _dbEntry.State=Task.State;
                    _dbEntry.ProjectID=Task.ProjectID;
                   
                    _dbEntry.TaskType = Task.TaskType;
                    _dbEntry.ProgramID = Task.ProgramID;
                    _dbEntry.ProjectID = Task.ProjectID;
                    
                    _dbEntry.Enabled = Task.Enabled;
                    _dbEntry.QCInfoFinish = Task.QCInfoFinish;
                    _dbEntry.PositionFinish = Task.PositionFinish;
                    _dbEntry.PrevState = Task.State;
                }
            }
            _context.SaveChanges();

            if (Task.Version > 0)
            {
                DisableOldVersion(Task.TaskName, Task.Version);
            }
            
            if (_isNew)
            {
                return Task.TaskID;
            }
            else
            {
                return _dbEntry.TaskID;
            }
            
        }
        
        public IEnumerable<Task> QueryByProject(int ProjectID, int TaskType,bool  Enabled=true)
        {
            IEnumerable<Task> _tasks = _context.Tasks.Where(t => t.ProjectID == ProjectID).Where(t => t.TaskType == TaskType).Where(t=>t.Enabled==Enabled);
            return _tasks;
        }
        
        public Task QueryByTaskID(int TaskID)
        {
            Task _task = _context.Tasks.Find(TaskID);
            return _task;
        }
        
        /// <summary>
        /// CAM user accept the cam design task
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="UserID"></param>
        public void Claim(int TaskID, int UserID)
        {
            Task _task = QueryByTaskID(TaskID);
            _task.CAMUser = UserID;
            _task.State = 2;
            _task.AcceptTime = DateTime.Now;
            _context.SaveChanges();
        }

        /// <summary>
        /// Set the task to state 2(released) and set the releasetime field
        /// Task no longer displayed in CAM user task list
        /// Task will appear in workshop operator task list
        /// </summary>
        /// <param name="TaskID"></param>
        public void Release(int TaskID)
        {
            Task _task = QueryByTaskID(TaskID);
            _task.State = 0;
            _task.ReleaseTime = DateTime.Now;
            _context.SaveChanges();
        }

        /// <summary>
        /// Workshop operator add task to queue
        /// </summary>
        /// <param name="TaskID"></param>
        public void Queue(int TaskID)
        {
            Task _task = QueryByTaskID(TaskID);
            _task.State = 4;
            _context.SaveChanges();
        }

        /// <summary>
        /// Workshop operator start the task
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="UserID"></param>
        public void Start(int TaskID, int UserID)
        {
            Task _task = QueryByTaskID(TaskID);
            _task.State = 5;
            _task.WorkshopUser = UserID;
            _task.StartTime = DateTime.Now;
            _context.SaveChanges();
        }

        public void Pause(int TaskID)
        {
            Task _task = QueryByTaskID(TaskID);
            _task.PrevState = _task.State;
            _task.State = -1;
            _context.SaveChanges();
        }

        public void UnPause(int TaskID)
        {
            Task _task = QueryByTaskID(TaskID);
            _task.State = _task.PrevState;
            _context.SaveChanges();
        }
        
        public void OutSource(int TaskID)
        {
            Task _task = QueryByTaskID(TaskID);
            if (_task.TaskType == 1)
            {
                _task.State = (int)CNCStatus.外发;
            }
            else if (_task.TaskType == 4)
            {
                _task.State = (int)SteelStatus.外发;
            }
            
            _context.SaveChanges();
        }

        public void CancelOutSource(int TaskID)
        {
            Task _task = QueryByTaskID(TaskID);
            _task.State = 4;
            _context.SaveChanges();
        }


        public void Priority(int TaskID, int Level)
        {
            Task _task = QueryByTaskID(TaskID);
            _task.Priority = Level;
            _context.SaveChanges();
        }

        public void Finish(int TaskID)
        {
            Task _task = QueryByTaskID(TaskID);
            _task.State = 90;
            _task.FinishTime = DateTime.Now;
            _context.SaveChanges();
        }


        public IEnumerable<Task> QueryByState(int TaskType, int State = 1, int ProjectID = 0)
        {
            IEnumerable<Task> _tasks = _context.Tasks.Where(t => t.TaskType == TaskType);
            if (ProjectID != 0)
            {
                _tasks = _tasks.Where(t => t.ProjectID == ProjectID);
            }
            switch (State){
                case 0:
                    _tasks=_tasks.Where(t=>t.State<=2);
                    break;
                case 1:
                    _tasks=_tasks.Where(t=>t.State>2).Where(t=>t.State<90);
                    break;
                case 2:
                    _tasks=_tasks.Where(t=>t.State==90);
                    break;
            }
            return _tasks;
        }


        public void PositonFinish(int TaskID)
        {
            Task _task = QueryByTaskID(TaskID);
            _task.PositionFinish = true;
            _context.SaveChanges();
        }

        public void QCInfoFinish(int TaskID)
        {
            Task _task = QueryByTaskID(TaskID);
            _task.QCInfoFinish = true;
            _context.SaveChanges();
        }


        public void Stop(int TaskID)
        {
            Task _task = QueryByTaskID(TaskID);
            _task.State = 99;
            _context.SaveChanges();
        }


        public void AcceptItem(int TaskID)
        {
            Task _task = QueryByTaskID(TaskID);
            _task.State = (int)SteelStatus.已接收;
            _context.SaveChanges();
        }

        //Used in PointCheck
        public void FinishTask(int TaskID, int TargetState, int WorkshopUser)
        {
            Task _task = QueryByTaskID(TaskID);
            _task.State = TargetState;
            _task.FinishTime = DateTime.Now;
            _task.WorkshopUser = WorkshopUser;
            _context.SaveChanges();
        }


        public IEnumerable<Task> QueryByProgramNumber(IEnumerable<int> ProgramIDs)
        {
            return _context.Tasks.Where(t => t.TaskType == 4).Where(t => (ProgramIDs.Contains(t.ProgramID)));
        }


        public IEnumerable<Task> QueryByGroupID(int GroupID)
        {
            return _context.Tasks.Where(t => t.ProgramID == GroupID).Where(t => t.Enabled == true).Where(t => t.State < 2).Where(t=>t.TaskType==4);
        }


        public Task QueryByNameVersion(string Name, int Version, int TaskType=-1, bool Enabled=true)
        {
            IEnumerable<Task> _task = _context.Tasks
                .Where(t => t.TaskName == Name)
                .Where(t => t.Version == Version)
                .Where(t => t.Enabled == true);
            if (TaskType > 0)
            {
                _task = _task.Where(t => t.TaskType == TaskType);
            }
            else
            {
                _task = _task.Where(t => t.TaskType < 5);
            }
            Task _returnTask = _task.FirstOrDefault();
            return _returnTask;
        }


        public void UpdateDrawing(string DrawName)
        {
            string _taskName = DrawName.Substring(0, DrawName.LastIndexOf('_'));
            int _version = Convert.ToInt16(DrawName.Substring(DrawName.LastIndexOf('_') + 2, 2));

            Task _task = QueryByNameVersion(_taskName, _version);
            if (_task != null)
            {
                _task.DrawingFile = DrawName + ".pdf";
            }
            _context.SaveChanges();
        }

        public IEnumerable<Task> QueryByMoldNumber(string MoldNumber, int TaskType, bool Enabled = true)
        {
            IEnumerable<Task> _tasks = _context.Tasks
                .Where(t => t.MoldNumber == MoldNumber)
                .Where(t => t.TaskType == TaskType)
                .Where(t => t.Enabled == Enabled);
            return _tasks;
        }


        public IEnumerable<Task> QueryByName(string TaskName)
        {
            return _context.Tasks.Where(t => t.TaskName == TaskName).Where(t => t.Enabled == true);
        }

        public void DisableOldVersion(string TaskName, int Version)
        {
            IEnumerable<Task> _tasks = _context.Tasks.Where(t => t.TaskName == TaskName).Where(t => t.Version < Version).Where(t => t.Enabled == false);
            foreach (Task _task in _tasks)
            {
                _task.Enabled = false;
            }
            _context.SaveChanges();
        }


        public Task QueryByModelVersion(string Model, int Version, int TaskType = -1, bool Enabled=true)
        {
            IEnumerable<Task> _task = _context.Tasks.Where(t => t.Model == Model).Where(t => t.Version == Version).Where(t=>t.Enabled==Enabled);
            if (TaskType > 0)
            {
                _task = _task.Where(t => t.TaskType == TaskType);
            }
            else
            {
                _task = _task.Where(t => t.TaskType < 5);
            }
            return _task.FirstOrDefault();
        }


        public void Delete(int TaskID)
        {
            Task _dbEntry = _context.Tasks.Find(TaskID);
            if (_dbEntry != null)
            {
                _dbEntry.Enabled = false;
                _context.SaveChanges();
            }
        }

        public int AddNewTask(Task Task)
        {
            _context.Tasks.Add(Task);
            _context.SaveChanges();
            return Task.TaskID;
        }




        public Task QueryByFullName(string FullName, int TaskType = 1)
        {
            string _name = FullName.Substring(0, FullName.LastIndexOf("_"));
            int _version = Convert.ToInt16(FullName.Substring(FullName.LastIndexOf('_')+1));
            return QueryByNameVersion(_name, _version, TaskType);
        }
    }
}
