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
using TechnikSys.MoldManager.Domain.Status;


namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class TaskHourRepository:ITaskHourRepository
    {
        private EFDbContext _context=new EFDbContext();
        public IQueryable<TaskHour> TaskHours 
        {
            get { return _context.TaskHours.Where(h => h.TaskType != 5); }
        }
        /// <summary>
        /// RecordType:0正常开始  1暂停后重启  -1取消/删除任务 2 外发 3返工
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Save(TaskHour model)
        {
            TaskHour dbentry;
            //DateTime _iniTime = DateTime.Parse("1900/1/1");
            #region 新增
            if (model.TaskHourID== 0)
            {
                dbentry = new TaskHour();
                dbentry.TaskID = model.TaskID;
                dbentry.StartTime = model.StartTime;
                dbentry.FinishTime = model.FinishTime;
                dbentry.TaskType = model.TaskType;
                dbentry.Enabled = model.Enabled;
                dbentry.Time = model.Time ;
                // RecordType:0正常开始  1暂停后重启  -1取消/删除任务 2 外发 3返工
                dbentry.RecordType = model.RecordType;
                dbentry.Memo = model.Memo;
                dbentry.MoldNumber = model.MoldNumber;
                dbentry.MachineCode = model.MachineCode;
                dbentry.State = model.State;
                dbentry.Operater = model.Operater;
                dbentry.Qty = model.Qty;
                dbentry.Cost = model.Cost;
                dbentry.SemiTaskFlag = model.SemiTaskFlag;
                _context.TaskHours.Add(dbentry);
            }
            #endregion
            #region 更新
            else
            {
                dbentry = _context.TaskHours.Where(h => h.TaskHourID == model.TaskHourID).FirstOrDefault();
                if (dbentry != null)
                {
                    //if (model.State == (int)TaskHourStatus.完成)
                    //{
                    //    TimeSpan timeSpan;
                    //    timeSpan = dbentry.FinishTime - dbentry.StartTime;
                    //    dbentry.Time = Convert.ToDecimal(timeSpan.TotalMinutes);
                    //    dbentry.FinishTime = model.FinishTime;
                    //    dbentry.MachineCode = model.MachineCode;
                    //}   
                    //TimeSpan timeSpan;
                    dbentry.FinishTime = model.FinishTime;
                    //timeSpan = dbentry.FinishTime - dbentry.StartTime;
                    ////正常结束
                    //if (dbentry.RecordType==2)
                    //{
                    //    dbentry.Time = model.Time;                        
                    //}
                    ////外发结束
                    //else
                    //    dbentry.Time = Convert.ToDecimal(timeSpan.TotalMinutes);
                    dbentry.Time = model.Time;
                    dbentry.MachineCode = model.MachineCode;
                    dbentry.Enabled = model.Enabled;
                    dbentry.RecordType = model.RecordType;                   
                    dbentry.State = model.State;
                    dbentry.Qty = model.Qty;
                    dbentry.Cost = model.Cost;
                    dbentry.SemiTaskFlag = model.SemiTaskFlag;
                    dbentry.Memo = model.Memo + " 时间：" + dbentry.Time.ToString();
                }               
            }
            #endregion
            _context.SaveChanges();
            return dbentry.TaskHourID;
        }
        public TaskHour GetCurTHByTaskID(int TaskID)
        {
            TaskHour _taskhour = _context.TaskHours.Where(h => h.TaskID == TaskID && h.Enabled == true && h.State==(int)TaskHourStatus.开始).Where(h=>h.TaskType!=5).OrderByDescending(h=>h.TaskHourID).FirstOrDefault() ?? new TaskHour();
            return _taskhour;
        }
        public TaskHour GetCurTHBySemiTaskFlag(string SemiTaskFlag)
        {
            TaskHour _taskhour = _context.TaskHours.Where(h => h.SemiTaskFlag.Contains(SemiTaskFlag) && h.Enabled == true && h.State == (int)TaskHourStatus.开始).OrderByDescending(h => h.TaskHourID).FirstOrDefault() ?? new TaskHour();
            return _taskhour;
        }
        public decimal GetTotalHourByTaskID(int TaskID)
        {
            decimal ClosedTime=0;
            decimal OpenTime=0;
            decimal TotalTiem = 0;
            List<int> _FStatelist = new List<int>
            {
                (int)TaskHourStatus.任务等待,
                (int)TaskHourStatus.完成记录,
                (int)TaskHourStatus.暂停,
            };
            #region 获取关闭工时
            List<TaskHour> _ClosedTHs = _context.TaskHours.Where(h => _FStatelist.Contains(h.State) && h.TaskID == TaskID).Where(h => h.TaskType != 5).ToList();
            if (_ClosedTHs != null)
            {
                foreach(var cth in _ClosedTHs)
                {
                    ClosedTime = ClosedTime + cth.Time;
                }
            }
            #endregion
            #region 获取Open工时
            List<TaskHour> _OpenTHs = _context.TaskHours.Where(h => h.State == (int)TaskHourStatus.开始 && h.TaskID == TaskID).Where(h => h.TaskType != 5).ToList();
            if (_OpenTHs != null)
            {
                TimeSpan _timespan;
                foreach(var cth in _OpenTHs)
                {
                    _timespan = DateTime.Now - cth.StartTime;
                    decimal _time = Convert.ToDecimal(_timespan.TotalMinutes);
                    OpenTime = OpenTime + _time;
                }
            }
            #endregion
            TotalTiem = ClosedTime + OpenTime;
            return TotalTiem;
        }
        public string GetOperaterByTaskID(int TaskID)
        {
            List<int> _FStatelist = new List<int>
            {
                (int)TaskHourStatus.完成记录,
                (int)TaskHourStatus.完成,
                (int)TaskHourStatus.暂停,
                (int)TaskHourStatus.任务等待,
                (int)TaskHourStatus.开始,
                (int)TaskHourStatus.外发,
            };
            TaskHour _taskhour = _context.TaskHours.Where(h => h.TaskID == TaskID && _FStatelist.Contains(h.State)).Where(h => h.TaskType != 5).OrderByDescending(h=>h.TaskHourID).FirstOrDefault() ?? new TaskHour();
            string _operater = _taskhour.TaskHourID > 0 ? _taskhour.Operater != null ? _taskhour.Operater : "" : "";
            return _operater;
        }
        public string GetMachineByTask(int TaskID)
        {
            List<int> _FStatelist = new List<int>
            {
                (int)TaskHourStatus.完成记录,
                (int)TaskHourStatus.完成,
                (int)TaskHourStatus.暂停,
                (int)TaskHourStatus.任务等待,
                (int)TaskHourStatus.开始,
                (int)TaskHourStatus.外发,
            };
            var _th = _context.TaskHours.Where(t => t.TaskID == TaskID && _FStatelist.Contains(t.State)).Where(h => h.TaskType != 5).OrderByDescending(h=>h.TaskHourID).FirstOrDefault();
            if (_th != null)
            {
                MachinesInfo _mInfo = _context.MachinesInfo.Where(m => m.MachineCode == _th.MachineCode).FirstOrDefault();
                if (_mInfo != null)
                    return _mInfo.MachineName + "_" + _mInfo.MachineCode;
            }
            return "";
        }
        public string GetOperaterBySemiTaskFlag(string SemiTaskFlag)
        {
            List<int> _FStatelist = new List<int>
            {
                (int)TaskHourStatus.完成记录,
                (int)TaskHourStatus.完成,
                (int)TaskHourStatus.暂停,
                (int)TaskHourStatus.任务等待,
                (int)TaskHourStatus.开始,
                (int)TaskHourStatus.外发,
            };
            TaskHour _taskhour = _context.TaskHours.Where(h => h.SemiTaskFlag.Contains(SemiTaskFlag) && _FStatelist.Contains(h.State)).Where(h => h.TaskType != 5).OrderByDescending(h => h.TaskHourID).FirstOrDefault() ?? new TaskHour();
            string _operater = _taskhour.TaskHourID > 0 ? _taskhour.Operater != null ? _taskhour.Operater : "" : "";
            return _operater;
        }
        public string GetMachineBySemiTaskFlag(string SemiTaskFlag)
        {
            List<int> _FStatelist = new List<int>
            {
                (int)TaskHourStatus.完成记录,
                (int)TaskHourStatus.完成,
                (int)TaskHourStatus.暂停,
                (int)TaskHourStatus.任务等待,
                (int)TaskHourStatus.开始,
                (int)TaskHourStatus.外发,
            };
            TaskHour _taskhour = _context.TaskHours.Where(h => h.SemiTaskFlag.Contains(SemiTaskFlag) && _FStatelist.Contains(h.State)).Where(h => h.TaskType != 5).OrderByDescending(h => h.TaskHourID).FirstOrDefault() ?? new TaskHour();
            if (_taskhour != null)
            {
                MachinesInfo _mInfo = _context.MachinesInfo.Where(m => m.MachineCode == _taskhour.MachineCode).FirstOrDefault();
                if (_mInfo != null)
                    return _mInfo.MachineName + "_" + _mInfo.MachineCode;
            }
            return "";
        }


    }
}
