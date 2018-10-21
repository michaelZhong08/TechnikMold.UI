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
            get { return _context.TaskHours; }
        }

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
                // RecordType:0正常开始  1暂停后重启  -1取消/删除任务 2 外发
                dbentry.RecordType = model.RecordType;
                //dbentry.Memo = "任务开始于：" + DateTime.Now.ToString("yyMMddhhmm") + "；操作者：" + operater + "/r/n";
                dbentry.Memo = model.Memo;
                dbentry.MoldNumber = model.MoldNumber;
                dbentry.MachineCode = model.MachineCode;
                dbentry.State = model.State;
                dbentry.Operater = model.Operater;
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
                    TimeSpan timeSpan;
                    dbentry.FinishTime = model.FinishTime;
                    timeSpan = dbentry.FinishTime - dbentry.StartTime;
                    //正常结束
                    if (model.Time == 0)
                    {
                        dbentry.Time = Convert.ToDecimal(timeSpan.TotalMinutes);
                    }
                    //外发结束
                    else
                        dbentry.Time = model.Time;
                    dbentry.MachineCode = model.MachineCode;
                    dbentry.Enabled = model.Enabled;
                    dbentry.RecordType = model.RecordType;                   
                    dbentry.State = model.State;
                    dbentry.Memo = model.Memo + " 时间：" + dbentry.Time.ToString();
                }               
            }
            #endregion
            _context.SaveChanges();
            return dbentry.TaskHourID;
        }
        public TaskHour GetCurTHByTaskID(int TaskID)
        {
            TaskHour _taskhour = _context.TaskHours.Where(h => h.TaskID == TaskID && h.Enabled == true && h.State==(int)TaskHourStatus.开始).FirstOrDefault() ?? new TaskHour();
            return _taskhour;
        }
        public decimal GetTotalHourByTaskID(int TaskID)
        {
            decimal ClosedTime=0;
            decimal OpenTime=0;
            decimal TotalTiem = 0;
            #region 获取关闭工时
            List<TaskHour> _ClosedTHs = _context.TaskHours.Where(h => h.State == (int)TaskHourStatus.完成 && h.TaskID == TaskID).ToList();
            if (_ClosedTHs != null)
            {
                foreach(var cth in _ClosedTHs)
                {
                    ClosedTime = ClosedTime + cth.Time;
                }
            }
            #endregion
            #region 获取Open工时
            List<TaskHour> _OpenTHs = _context.TaskHours.Where(h => h.State == (int)TaskHourStatus.开始 && h.TaskID == TaskID).ToList();
            if (_OpenTHs != null)
            {
                foreach(var cth in _OpenTHs)
                {
                    OpenTime = OpenTime + cth.Time;
                }
            }
            #endregion
            TotalTiem = ClosedTime + OpenTime;
            return TotalTiem;
        }
        public string GetOperaterByTaskID(int TaskID)
        {
            TaskHour _taskhour = _context.TaskHours.Where(h => h.TaskID == TaskID && h.State == (int)TaskHourStatus.完成).OrderByDescending(h=>h.TaskHourID).FirstOrDefault() ?? new TaskHour();
            string _operater = _taskhour.TaskHourID > 0 ? _taskhour.Operater != null ? _taskhour.Operater : "" : "";
            return _operater;
        }
        public string GetMachineByTask(int TaskID)
        {
            var _th = _context.TaskHours.Where(t => t.TaskID == TaskID).FirstOrDefault();
            if (_th != null)
            {
                MachinesInfo _mInfo = _context.MachinesInfo.Where(m => m.MachineCode == _th.MachineCode).FirstOrDefault();
                if (_mInfo != null)
                    return _mInfo.MachineName + "_" + _mInfo.MachineCode;
            }
            return null;
        }
    }
}
