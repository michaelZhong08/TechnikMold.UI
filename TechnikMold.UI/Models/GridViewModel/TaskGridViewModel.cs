using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikMold.UI.Models.ViewModel;
using TechnikSys.MoldManager.Domain.Status;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class TaskGridViewModel
    {
        public List<TaskGridRowModel> rows;
        public int Page;
        public int Total;
        public int Records;
        

        public TaskGridViewModel(IEnumerable<Task> Tasks, 
            IUserRepository UserRepository, 
            string CAMDrawingPath, 
            ICNCMachInfoRepository MachInfoRepository, 
            IEDMDetailRepository EDMDetailRepository, 
            ITaskRepository TaskRepository, IProjectPhaseRepository ProjectPhaseRepository,
            IMGSettingRepository MGSettingRepository,
            IWEDMSettingRepository WEDMSettingRepository,
            ITaskHourRepository TaskHourRepository
            ,ISystemConfigRepository SystemConfigRepo
            ,ITaskTypeRepository TasktypeRepo
            ,IMachinesInfoRepository MachInfoRepo)
        {
            ProjectPhase _phase;
            string _cad, _cam, _workshop, _qc, _planDate;
            int _phaseID=0;
            rows = new List<TaskGridRowModel>();

            List<WEDMSetting> _wedmSettings = WEDMSettingRepository.WEDMSettings.ToList();
            List<MGSetting> _mgSettings = MGSettingRepository.MGSettings.ToList();
            List<User> _users = UserRepository.Users.ToList();
            List<ProjectPhase> _pjPhases = ProjectPhaseRepository.ProjectPhases.ToList();
            List<TaskHour> _taskhours = TaskHourRepository.TaskHours.ToList();
            List<MachinesInfo> _machineInfos = MachInfoRepo.MachinesInfo.ToList();
            List<TaskType> _tasktypes = TasktypeRepo.TaskTypes.ToList();
            List<CNCMachInfo> _cncmachInfos = MachInfoRepository.CNCMachInfoes.ToList();
            List<Task> _tasks = TaskRepository.Tasks.ToList();
            List<EDMDetail> _edmDetails = EDMDetailRepository.EDMDetails.ToList();

            foreach (Task _task in Tasks)
            {
                WEDMSetting wedmsetting=new WEDMSetting();
                MGSetting mgsetting=new MGSetting();
                switch (_task.TaskType)
                {
                    case 1:
                        _phaseID = 8;
                        break;
                    case 2:
                        _phaseID = 9;
                        break;
                    case 3:
                        wedmsetting = _wedmSettings.Where(s => s.ID == _task.ProgramID).FirstOrDefault()??new WEDMSetting();//WEDMSettingRepository.QueryByTaskID(_task.TaskID);
                        _phaseID = 10;
                        break;
                    case 4:
                        _phaseID = 8;
                        break;
                    case 6:
                        mgsetting = _mgSettings.Where(s => s.ID == _task.ProgramID).FirstOrDefault()??new MGSetting();//MGSettingRepository.QueryByTaskID(_task.TaskID);
                        _phaseID = 7;
                        break;
                }
                _cad = _task.CADUser > 0 ? (_users.Where(u => u.UserID == _task.CADUser).FirstOrDefault() ?? new User()).FullName : "";//UserRepository.GetUserByID(_task.CADUser).FullName : "";
                _cam = _task.CAMUser>0? (_users.Where(u => u.UserID == _task.CAMUser).FirstOrDefault() ?? new User()).FullName : "";//UserRepository.GetUserByID(_task.CAMUser).FullName:"";
                _workshop = _task.WorkshopUser>0? (_users.Where(u => u.UserID == _task.WorkshopUser).FirstOrDefault() ?? new User()).FullName : "";//UserRepository.GetUserByID(_task.WorkshopUser).FullName:"";
                _qc = _task.QCUser > 0 ? (_users.Where(u => u.UserID == _task.QCUser).FirstOrDefault() ?? new User()).FullName : "";//UserRepository.GetUserByID(_task.QCUser).FullName : "";
                CNCMachInfo _machinfo = GetCNCMachinfo(_task, _cncmachInfos, _tasks, _edmDetails);//GetCNCMachinfo(_task,MachInfoRepository, TaskRepository, EDMDetailRepository);
                decimal TaskHour = 0;
                try
                {
                    _phase = _pjPhases.Where(p => p.ProjectID == _task.ProjectID && p.PhaseID == _phaseID).FirstOrDefault() ?? new ProjectPhase();//ProjectPhaseRepository.GetProjectPhases(_task.ProjectID).Where(p => p.PhaseID == _phaseID).FirstOrDefault();
                    _planDate = _phase.PlanCFinish == new DateTime(1, 1, 1) ? _phase.PlanFinish.ToString("yyyy-MM-dd") :
                    _phase.PlanCFinish.ToString("yyyy-MM-dd");                   
                }
                catch
                {
                    _planDate = "-";                    
                }
                try
                {
                    TaskHour = GetTotalHourByTaskID(_taskhours, _task.TaskID);//TaskHourRepository.GetTotalHourByTaskID(_task.TaskID);
                }
                catch
                {
                    TaskHour = 0;
                }
                string _machineCode = GetMachineByTask(_taskhours, _machineInfos, _task.TaskID);//TaskHourRepository.GetMachineByTask(_task.TaskID) ?? "";
                string Operater = GetOperaterByTaskID(_taskhours, _task.TaskID);//TaskHourRepository.GetOperaterByTaskID(_task.TaskID) ?? "";
                SetupTaskStart _setupTask = new SetupTaskStart
                {
                    TaskID = _task.TaskID,
                    TaskName = _task.TaskName,
                    State = Enum.GetName(typeof(TaskStatus), _task.State),
                    MachinesCode = "",
                    MachinesName = _machineCode,
                    TotalTime = Convert.ToInt32(TaskHour),
                    UserID = 0,
                    UserName = Operater,
                };
                //string _camDrawingPath = string.Empty;
                if (string.IsNullOrEmpty(CAMDrawingPath))
                {
                    if (_task.TaskType == 6)
                        CAMDrawingPath = SystemConfigRepo.GetTaskDrawingPath("CAD");
                    else
                        CAMDrawingPath = SystemConfigRepo.GetTaskDrawingPath();
                }
                string taskType = string.Empty;
                int _mgtype = (_tasktypes.Where(t => t.ShortName == "MG" && t.Enable).FirstOrDefault() ?? new TaskType()).TaskID;//(TasktypeRepo.TaskTypes.Where(t => t.ShortName == "MG" && t.Enable).FirstOrDefault() ?? new TaskType()).TaskID;//6
                if (_task.TaskType != _mgtype)
                {
                    taskType = (_tasktypes.Where(t => t.TaskID == _task.TaskType && t.Enable).FirstOrDefault() ?? new TaskType()).Name;//TasktypeRepo.TaskTypes.Where(t => t.TaskID == _task.TaskType).Select(t => t.Name).FirstOrDefault();
                }
                else
                {
                    string _typeID = (_mgtype.ToString() + _task.OldID.ToString());
                    taskType = (_tasktypes.Where(t => t.TaskID == Convert.ToInt32(_typeID) && t.Enable).FirstOrDefault() ?? new TaskType()).Name;//TasktypeRepo.TaskTypes.ToList().Where(t => t.TaskID == Convert.ToInt32(_typeID)).Select(t => t.Name).FirstOrDefault();
                }
                rows.Add( new TaskGridRowModel(_task, _cad, _cam, _workshop, _qc, CAMDrawingPath, _planDate, _setupTask, _machinfo,wedmsetting,mgsetting, taskType));
            }
        }

        public TaskGridViewModel(IEnumerable<Task> Tasks,
            IUserRepository UserRepository,
            string CAMDrawingPath)
        {
            //ProjectPhase _phase;
            //string _cad, _cam, _workshop, _qc, _planDate;
            //int _phaseID = 0;
            rows = new List<TaskGridRowModel>();
            foreach (Task _task in Tasks)
            {
                #region
                //WEDMSetting wedmsetting = new WEDMSetting();
                //MGSetting mgsetting = new MGSetting();
                //switch (_task.TaskType)
                //{
                //    case 1:
                //        _phaseID = 8;
                //        break;
                //    case 2:
                //        _phaseID = 9;
                //        break;
                //    case 3:
                //        wedmsetting = WEDMSettingRepository.QueryByTaskID(_task.TaskID);
                //        _phaseID = 10;
                //        break;
                //    case 4:
                //        _phaseID = 8;
                //        break;
                //    case 6:
                //        mgsetting = MGSettingRepository.QueryByTaskID(_task.TaskID);
                //        _phaseID = 7;
                //        break;
                //}
                //_cad = _task.CADUser > 0 ? UserRepository.GetUserByID(_task.CADUser).FullName : "";
                //_cam = _task.CAMUser > 0 ? UserRepository.GetUserByID(_task.CAMUser).FullName : "";
                //_workshop = _task.WorkshopUser > 0 ? UserRepository.GetUserByID(_task.WorkshopUser).FullName : "";
                //_qc = _task.QCUser > 0 ? UserRepository.GetUserByID(_task.QCUser).FullName : "";
                //CNCMachInfo _machinfo = GetCNCMachinfo(_task, MachInfoRepository, TaskRepository, EDMDetailRepository);
                //decimal TaskHour = 0;
                //try
                //{
                //    _phase = ProjectPhaseRepository.GetProjectPhases(_task.ProjectID).Where(p => p.PhaseID == _phaseID).FirstOrDefault();
                //    _planDate = _phase.PlanCFinish == new DateTime(1, 1, 1) ? _phase.PlanFinish.ToString("yyyy-MM-dd") :
                //    _phase.PlanCFinish.ToString("yyyy-MM-dd");
                //}
                //catch
                //{
                //    _planDate = "-";
                //}
                //try
                //{
                //    TaskHour = TaskHourRepository.GetTotalHourByTaskID(_task.TaskID);
                //}
                //catch
                //{
                //    TaskHour = 0;
                //}
                //string _machineCode = TaskHourRepository.GetMachineByTask(_task.TaskID) ?? "";
                //string Operater = TaskHourRepository.GetOperaterByTaskID(_task.TaskID) ?? "";
                //SetupTaskStart _setupTask = new SetupTaskStart
                //{
                //    TaskID = _task.TaskID,
                //    TaskName = _task.TaskName,
                //    State = Enum.GetName(typeof(TaskStatus), _task.State),
                //    MachinesCode = "",
                //    MachinesName = _machineCode,
                //    TotalTime = Convert.ToInt32(TaskHour),
                //    UserID = 0,
                //    UserName = Operater,
                //};
                #endregion
                string Creator = _task.Creator;//(UserRepository.GetUserByID(_task.Creator) ?? new User()).FullName;
                rows.Add(new TaskGridRowModel(_task, CAMDrawingPath, Creator));
            }
        }

        private CNCMachInfo GetCNCMachinfo(Task _task,
            //ICNCMachInfoRepository MachInfoRepository,
            //ITaskRepository TaskRepository,
            //IEDMDetailRepository EDMDetailRepository,
            List<CNCMachInfo> cncmacInfos,//
            List<Task> _tasks,//
            List<EDMDetail> _edmDetails//
            )
        {
            CNCMachInfo _machinfo = null;
            switch (_task.TaskType){
                case 1:

                    _machinfo = cncmacInfos.Where(m => m.DrawIndex == _task.TaskID).FirstOrDefault();//MachInfoRepository.QueryByELEIndex(_task.TaskID);
                    return _machinfo;
                case 2:
                    try
                    {
                        EDMDetail _detail = _edmDetails.Where(t => t.TaskID == _task.TaskID).Where(t => t.Expire == false).FirstOrDefault();//EDMDetailRepository.QueryByTaskID(_task.TaskID);
                        string[] _eleList = _detail.EleDetail.Split(';');
                        string _eleItem = _eleList[0].Substring(0, _eleList[0].Length - 4);
                        int _ver = Convert.ToInt16(_eleList[0].Substring(_eleList[0].IndexOf("_V") + 2));
                        Task _cnctask = QueryByNameVersion(_tasks, _eleItem, _ver);//TaskRepository.QueryByNameVersion(_eleItem, _ver);
                        _machinfo = cncmacInfos.Where(m => m.DrawIndex == _cnctask.TaskID).FirstOrDefault();//MachInfoRepository.QueryByELEIndex(_cnctask.TaskID);
                        return _machinfo;
                    }
                    catch
                    {
                        return null;
                    }
                default:
                    return null;
            }
        }
        public decimal GetTotalHourByTaskID(List<TaskHour> _ths,int TaskID)
        {
            decimal ClosedTime = 0;
            decimal OpenTime = 0;
            decimal TotalTiem = 0;
            List<int> _FStatelist = new List<int>
            {
                (int)TaskHourStatus.任务等待,
                (int)TaskHourStatus.完成记录,
                (int)TaskHourStatus.暂停,
            };
            #region 获取关闭工时
            List<TaskHour> _ClosedTHs = _ths.Where(h => _FStatelist.Contains(h.State) && h.TaskID == TaskID).Where(h => h.TaskType != 5).ToList();
            if (_ClosedTHs != null)
            {
                foreach (var cth in _ClosedTHs)
                {
                    ClosedTime = ClosedTime + cth.Time;
                }
            }
            #endregion
            #region 获取Open工时
            List<TaskHour> _OpenTHs = _ths.Where(h => h.State == (int)TaskHourStatus.开始 && h.TaskID == TaskID).Where(h => h.TaskType != 5).ToList();
            if (_OpenTHs != null)
            {
                TimeSpan _timespan;
                foreach (var cth in _OpenTHs)
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

        public string GetMachineByTask(List<TaskHour> _ths, List<MachinesInfo> _mis, int TaskID)
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
            var _th = _ths.Where(t => t.TaskID == TaskID && _FStatelist.Contains(t.State)).Where(h => h.TaskType != 5).OrderByDescending(h => h.TaskHourID).FirstOrDefault();
            if (_th != null)
            {
                MachinesInfo _mInfo = _mis.Where(m => m.MachineCode == _th.MachineCode).FirstOrDefault();
                if (_mInfo != null)
                    return _mInfo.MachineName + "_" + _mInfo.MachineCode;
            }
            return "";
        }

        public string GetOperaterByTaskID(List<TaskHour> _taskhours,int TaskID)
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
            TaskHour _taskhour = _taskhours.Where(h => h.TaskID == TaskID && _FStatelist.Contains(h.State)).Where(h => h.TaskType != 5).OrderByDescending(h => h.TaskHourID).FirstOrDefault() ?? new TaskHour();
            string _operater = _taskhour.TaskHourID > 0 ? _taskhour.Operater != null ? _taskhour.Operater : "" : "";
            return _operater;
        }

        public Task QueryByNameVersion(List<Task> _tasks,string Name, int Version, int TaskType = -1, bool Enabled = true)
        {
            IEnumerable<Task> _task = _tasks
                .Where(t => t.TaskName == Name)
                .Where(t => t.Version == Version)
                .Where(t => t.Enabled == true);
            if (TaskType > 0)
            {
                _task = _task.Where(t => t.TaskType == TaskType);
            }
            else
            {
                _task = _task.Where(t => t.TaskType < 10);
            }
            Task _returnTask = _task.FirstOrDefault();
            return _returnTask;
        }
    }
}