using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class QCTaskRepository :IQCTaskRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<QCTask> QCTasks
        {
            get { return _context.QCTasks; }
        }

        public int Save(QCTask QCTask, int Mode = 0)
        {
            bool _isNew = false;
            QCTask _dbEntry=null;
            if (Mode == 0)
            {
                if (QCTask.QCTaskID == 0)
                {
                    _dbEntry = _context.QCTasks.Where(q => q.TaskName == QCTask.TaskName).Where(q => q.Version == QCTask.Version).FirstOrDefault();
                    if (_dbEntry == null)
                    {
                        _isNew = true;
                        _context.QCTasks.Add(QCTask);
                    }
                    else
                    {
                        _dbEntry.TaskID = QCTask.TaskID;
                        _dbEntry.DrawingFile = QCTask.DrawingFile;
                        _dbEntry.TaskName = QCTask.TaskName;
                        _dbEntry.TaskType = QCTask.TaskType;
                        _dbEntry.Version = QCTask.Version;
                        _dbEntry.Priority = QCTask.Priority;
                        _dbEntry.Quantity = QCTask.Quantity;
                        _dbEntry.ForecastTime = QCTask.ForecastTime;
                        _dbEntry.CreateTime = QCTask.CreateTime;
                        _dbEntry.StartTime = QCTask.StartTime;
                        _dbEntry.FinishTime = QCTask.FinishTime;
                        _dbEntry.Memo = QCTask.Memo;
                        _dbEntry.StateMemo = QCTask.StateMemo;
                        _dbEntry.State = QCTask.State;
                        _dbEntry.ProjectID = QCTask.ProjectID;
                        _dbEntry.QCUser = QCTask.QCUser;
                        _dbEntry.MoldNumber = QCTask.MoldNumber;
                        _dbEntry.Enabled = QCTask.Enabled;
                    }
                    
                }
                else
                {
                    _dbEntry = _context.QCTasks.Find(QCTask.QCTaskID);
                    if (_dbEntry != null)
                    {
                        _dbEntry.TaskID = QCTask.TaskID;
                        _dbEntry.DrawingFile = QCTask.DrawingFile;
                        _dbEntry.TaskName = QCTask.TaskName;
                        _dbEntry.TaskType = QCTask.TaskType;
                        _dbEntry.Version = QCTask.Version;
                        _dbEntry.Priority = QCTask.Priority;
                        _dbEntry.Quantity = QCTask.Quantity;
                        _dbEntry.ForecastTime = QCTask.ForecastTime;
                        _dbEntry.CreateTime = QCTask.CreateTime;
                        _dbEntry.StartTime = QCTask.StartTime;
                        _dbEntry.FinishTime = QCTask.FinishTime;
                        _dbEntry.Memo = QCTask.Memo;
                        _dbEntry.StateMemo = QCTask.StateMemo;
                        _dbEntry.State = QCTask.State;
                        _dbEntry.ProjectID = QCTask.ProjectID;
                        _dbEntry.QCUser = QCTask.QCUser;
                        _dbEntry.MoldNumber = QCTask.MoldNumber;
                        _dbEntry.Enabled = QCTask.Enabled;
                    }
                }
            }
            else
            {
                _isNew = true;
                _context.QCTasks.Add(QCTask);
            }
            _context.SaveChanges();
            if (_isNew)
            {
                return QCTask.QCTaskID;
            }
            else
            {
                return _dbEntry.QCTaskID;
            }
        }


        public QCTask QueryByTaskID(int TaskID)
        {
            QCTask _dbEntry = _context.QCTasks.Where(q => q.TaskID == TaskID).Where(q=>q.Enabled==true).FirstOrDefault();
            return _dbEntry;
        }




        public IEnumerable<QCTask> QueryByProjectID(int ProjectID, int State=0)
        {
            IEnumerable<QCTask> _tasks = _context.QCTasks.Where(t => t.Enabled == true);
            if (ProjectID > 0)
            {
                _tasks = _tasks.Where(t => t.ProjectID == ProjectID);
            }
            
            if (State == 0)
            {
                _tasks = _tasks.Where(t => t.State == 0);
            }
            else
            {
                _tasks = _tasks.Where(t => t.State != 0);
            }
            return _tasks;
        }


        public QCTask QueryByID(int QCTaskID)
        {
            QCTask _qcTask = _context.QCTasks.Find(QCTaskID);
            return _qcTask;
        }
    }
}
