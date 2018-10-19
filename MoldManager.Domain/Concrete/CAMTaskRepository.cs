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
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class CAMTaskRepository:ICAMTasksRepository
    {
        private EFDbContext _context = new EFDbContext();



        public IQueryable<CAMTask> CAMTasks
        {
            get { return _context.CAMTasks; }
        }

        public int Save(CAMTask CAMTask)
        {
            if (CAMTask.CAMTaskID == 0)
            {
                CAMTask.Version = 1;
                _context.CAMTasks.Add(CAMTask);
            }
            else
            {
                CAMTask _dbEntry = _context.CAMTasks.Find(CAMTask.CAMTaskID);
                if (_dbEntry != null)
                {
                    _dbEntry.DrawingFile = CAMTask.DrawingFile;
                    _dbEntry.TaskName = CAMTask.TaskName;
                    _dbEntry.Version = CAMTask.Version;
                    _dbEntry.UserID = CAMTask.UserID;
                    _dbEntry.CreateDate = CAMTask.CreateDate;
                    _dbEntry.ReleaseDate = CAMTask.ReleaseDate;
                    _dbEntry.ProjectID = CAMTask.ProjectID;
                    _dbEntry.MoldNumber = CAMTask.MoldNumber;
                    _dbEntry.CADUserID = CAMTask.CADUserID;
                    _dbEntry.State = CAMTask.State;
                    _dbEntry.ReleaseUserID = CAMTask.ReleaseUserID;
                    _dbEntry.AcceptDate = CAMTask.AcceptDate;
                    _dbEntry.TaskType = CAMTask.TaskType;
                }
            }
            _context.SaveChanges();
            return CAMTask.CAMTaskID;

        }

        public int Upgrade(int CAMTaskID)
        {
            CAMTask _dbEntry = _context.CAMTasks.Find(CAMTaskID);
            _dbEntry.State = 9;

            CAMTask _newTask = new CAMTask();
            _newTask.DrawingFile = _dbEntry.DrawingFile;
            _newTask.TaskName = _dbEntry.TaskName;
            _newTask.Version = _dbEntry.Version+1;
            _newTask.UserID = _dbEntry.UserID;
            _newTask.CreateDate = DateTime.Now;
            _newTask.AcceptDate = new DateTime();

            _newTask.ReleaseDate = new DateTime();
            _newTask.ProjectID = _dbEntry.ProjectID;
            _newTask.MoldNumber = _dbEntry.MoldNumber;
            _newTask.CADUserID = _dbEntry.CADUserID;
            _newTask.State = 1;

            _context.CAMTasks.Add(_newTask);
            
            _context.SaveChanges();
            return _newTask.CAMTaskID;
        }

        public bool Claim(int CAMTaskID, int UserID)
        {

            CAMTask _dbEntry = _context.CAMTasks.Find(CAMTaskID);
            _dbEntry.UserID = UserID;
            _dbEntry.AcceptDate = DateTime.Now;
            _dbEntry.State = 1;
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
           
        }

        public bool Release(int CAMTaskID, int UserID)
        {
            CAMTask _dbEntry = _context.CAMTasks.Find(CAMTaskID);
            _dbEntry.ReleaseUserID = UserID;
            _dbEntry.ReleaseDate = DateTime.Now;
            _dbEntry.State = 2;
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool Pause(int CAMTaskID)
        {
            CAMTask _dbEntry = _context.CAMTasks.Find(CAMTaskID);
            _dbEntry.State = 3;
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region Query
        public IEnumerable<CAMTask> QueryByProjectID(int ProjectID)
        {
            return _context.CAMTasks.Where(t=>t.ProjectID==ProjectID);
        }

        public IEnumerable<CAMTask> QueryByUser(int UserID)
        {
            return _context.CAMTasks.Where(t => t.UserID == UserID);
        }

        public CAMTask QueryByID(int TaskID)
        {
            return _context.CAMTasks.Find(TaskID);
        }

        #endregion
       
    }
}
