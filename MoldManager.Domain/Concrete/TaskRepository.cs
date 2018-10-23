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
                    Task.Raw = Task.Raw == null ? "" : Task.Raw;
                    Task.Quantity = Task.Quantity == 0 ? 1 : Task.Quantity;
                    Task.ForecastTime = Task.ForecastTime == null ? new DateTime() : Task.ForecastTime;
                    Task.AcceptTime = Task.AcceptTime == null ? new DateTime() : Task.AcceptTime;
                    Task.PlanTime = Task.PlanTime == null ? new DateTime() : Task.PlanTime;
                    Task.StartTime = Task.StartTime == null ? new DateTime() : Task.StartTime;
                    Task.Memo = Task.Memo == null ? "" : Task.Memo;
                    Task.StateMemo = Task.StateMemo == null ? "" : Task.StateMemo;
                    Task.State = (int)CNCStatus.未发布;///If this is a new task, set the task in "Not Released" state;
                    Task.PrevState = (int)CNCStatus.未发布;
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
                    _dbEntry.Memo = Task.Memo == null ? "" : Task.Memo;
                    _dbEntry.StateMemo = Task.StateMemo == null ? "" : Task.StateMemo;
                    _dbEntry.PrevState = _dbEntry.State;
                    _dbEntry.State = Task.State;
                    _dbEntry.ProjectID = Task.ProjectID;
                    _dbEntry.TaskType = Task.TaskType;
                    _dbEntry.ProgramID = Task.ProgramID;
                    _dbEntry.CADUser = Task.CADUser;
                    _dbEntry.CAMUser = Task.CAMUser;
                    _dbEntry.Enabled = Task.Enabled;
                    _dbEntry.QCInfoFinish = Task.QCInfoFinish;
                    _dbEntry.PositionFinish = Task.PositionFinish;
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
                    _dbEntry.PrevState = _dbEntry.State;
                    _dbEntry.State=Task.State;
                    _dbEntry.ProjectID=Task.ProjectID;
                   
                    _dbEntry.TaskType = Task.TaskType;
                    _dbEntry.ProgramID = Task.ProgramID;
                    _dbEntry.ProjectID = Task.ProjectID;
                    
                    _dbEntry.Enabled = Task.Enabled;
                    _dbEntry.QCInfoFinish = Task.QCInfoFinish;
                    _dbEntry.PositionFinish = Task.PositionFinish;
                    
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
            _task.State = (int)CNCStatus.已接收;
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
            _task.State = (int)CNCStatus.暂停;
            _context.SaveChanges();
        }
        public void UnPause(int TaskID)
        {
            Task _task = QueryByTaskID(TaskID);
            _task.State = _task.PrevState;
            _context.SaveChanges();
        }
        /// <summary>
        /// Task外发 更新Task状态 外发
        /// </summary>
        /// <param name="TaskID"></param>
        public void OutSource(int TaskID)
        {
            Task _task = QueryByTaskID(TaskID);
            _task.State = (int)CNCStatus.外发;
            _task.StartTime = DateTime.Now;
            //if (_task.TaskType == 1)
            //{
            //    _task.State = (int)CNCStatus.外发;
            //}
            //else if (_task.TaskType == 2)
            //{
            //    _task.State = (int)EDMStatus.外发;
            //}
            //else if (_task.TaskType == 3)
            //{
            //    _task.State = (int)WEDMStatus.外发;
            //}
            //else if (_task.TaskType == 4)
            //{
            //    _task.State = (int)SteelStatus.外发;
            //}
            //else if (_task.TaskType == 6)
            //{
            //    _task.State = (int)GrindStatus.外发;
            //}
            _context.SaveChanges();
        }
        /// <summary>
        /// Task 取消外发 更新Task状态 等待
        /// </summary>
        /// <param name="TaskID"></param>
        public void CancelOutSource(int TaskID)
        {
            Task _task = QueryByTaskID(TaskID);
            _task.State = (int)CNCStatus.等待;
            _context.SaveChanges();
        }


        public void Priority(int TaskID, int Level)
        {
            Task _task = QueryByTaskID(TaskID);
            _task.Priority = Level;
            _context.SaveChanges();
        }
        /// <summary>
        /// Task 完成
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="FinishBy"></param>
        public void Finish(int TaskID,string FinishBy)
        {
            Task _task = QueryByTaskID(TaskID);
            //以前任务结束 _task.State = 90;
            _task.State = (int)CNCStatus.完成;
            _task.FinishTime = DateTime.Now;
            User _user = _context.Users.Where(u => u.FullName == FinishBy).FirstOrDefault() ?? new User();
            _task.FinishBy = _user.UserID;
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

        /// <summary>
        /// CNC任务取消
        /// </summary>
        /// <param name="TaskID"></param>
        public void Stop(int TaskID)
        {
            Task _task = QueryByTaskID(TaskID);
            switch (_task.TaskType)
            {
                case 1:
                    _task.State = (int)CNCStatus.任务取消;
                    break;
            }           
            _context.SaveChanges();
        }


        public void AcceptItem(int TaskID)
        {
            Task _task = QueryByTaskID(TaskID);
            _task.State = (int)SteelStatus.已接收;
            _task.AcceptTime = DateTime.Now;
            _context.SaveChanges();
        }

        /// <summary>
        /// 任务点检
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="TargetState"></param>
        /// <param name="WorkshopUser"></param>
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


        public void UpdateDrawing(string DrawName,bool IsContain2D, string DrawType)
        {
            string _taskName;
            if (IsContain2D)
            {
                //111111_MG_2D_V00
                _taskName = DrawName.Substring(0, DrawName.LastIndexOf('_'));
                _taskName = _taskName.Substring(0, _taskName.LastIndexOf('_'));
            }
            else
            {
                //111111_MG_V00
                _taskName = DrawName.Substring(0, DrawName.LastIndexOf('_'));
            }
            int _version = Convert.ToInt16(DrawName.Substring(DrawName.LastIndexOf('_') + 2, 2));

            List<Task> _tasks=new List<Task>();
            if (DrawType == "CAM")
            {
                _tasks = _context.Tasks.Where(t => t.TaskName == _taskName && t.Version == _version && t.Enabled == true).ToList() ?? new List<Task>();
            }
            else if (DrawType == "CAD")
            {
                _tasks = _context.Tasks.Where(t => t.TaskName == _taskName && t.Enabled == true).ToList() ?? new List<Task>();
            }

            if (_tasks.Count > 0)
            {
                foreach(var t in _tasks)
                {
                    t.DrawingFile = DrawName;// + ".pdf";
                }
            }
            _context.SaveChanges();
        }

        public IEnumerable<Task> QueryByMoldNumber(string MoldNumber, int TaskType, bool Enabled = true)
        {
            IEnumerable<Task> _tasks;
            if (!string.IsNullOrEmpty(MoldNumber))
            {
                _tasks = _context.Tasks
                .Where(t => t.MoldNumber == MoldNumber)
                .Where(t => t.TaskType == TaskType)
                .Where(t => t.Enabled == Enabled);
            }
            else
            {
                _tasks = _context.Tasks
                .Where(t => t.TaskType == TaskType)
                .Where(t => t.Enabled == Enabled);
            }
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
        /// <summary>
        /// 获取最大版本图纸
        /// </summary>
        /// <param name="TaskName">任务名</param>
        /// <returns></returns>
        public string GetMaxVerDrawFile(string TaskName)
        {
            List<CAMDrawing> _draws = _context.CAMDrawings.Where(d => d.DrawingName.Contains(TaskName) && d.active == true && d.Lock == false).ToList();
            #region 排序，找到最高版本对应DrawID
            Dictionary<int, int> _dics = new Dictionary<int, int>();
            foreach(var d in _draws)
            {
                int _ver = Convert.ToInt32(d.DrawingName.Substring(d.DrawingName.LastIndexOf('_') + 2, 2));
                _dics.Add(d.CAMDrawingID, _ver);
            }
            Dictionary<int, int> _dics_SortByKey = _dics.OrderByDescending(d => d.Value).ToDictionary(d => d.Key, v => v.Value);
            #endregion
            if (_dics_SortByKey.Count > 0)
            {
                int _fkey = _dics_SortByKey.FirstOrDefault().Key;
                CAMDrawing _draw = _context.CAMDrawings.Where(d => d.CAMDrawingID == _fkey).FirstOrDefault();
                return _draw.DrawingName ?? "";
            }
            return "";
        }
        /// <summary>
        /// 获取任务最大版本
        /// </summary>
        /// <param name="TaskName">任务名</param>
        /// <returns></returns>
        public int GetMaxVerMGTask(string TaskName, int ProcessType)
        {
            List<Task> _tasks = new List<Task>();
            MGTypeName mgty = _context.MGTypeNames.Where(m => m.ID == ProcessType).FirstOrDefault();
            string typename = mgty.Name ?? "";
            _tasks = _context.Tasks.Where(t => t.TaskName == TaskName&& t.ProcessName== typename).ToList();
            int MaxVer = 0;
            if (_tasks.Count>0)
                MaxVer = _tasks.Max(v => v.Version);
            return MaxVer;
        }
    }
}
