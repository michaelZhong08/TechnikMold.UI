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
            ,ITaskTypeRepository TasktypeRepo)
        {
            ProjectPhase _phase;
            string _cad, _cam, _workshop, _qc, _planDate;
            int _phaseID=0;
            rows = new List<TaskGridRowModel>();
           
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
                        wedmsetting = WEDMSettingRepository.QueryByTaskID(_task.TaskID);
                        _phaseID = 10;
                        break;
                    case 4:
                        _phaseID = 8;
                        break;
                    case 6:
                        mgsetting = MGSettingRepository.QueryByTaskID(_task.TaskID);
                        _phaseID = 7;
                        break;
                }
                _cad = _task.CADUser > 0 ? UserRepository.GetUserByID(_task.CADUser).FullName : "";
                _cam = _task.CAMUser>0?UserRepository.GetUserByID(_task.CAMUser).FullName:"";
                _workshop = _task.WorkshopUser>0?UserRepository.GetUserByID(_task.WorkshopUser).FullName:"";
                _qc = _task.QCUser > 0 ? UserRepository.GetUserByID(_task.QCUser).FullName : "";
                CNCMachInfo _machinfo=GetCNCMachinfo(_task,MachInfoRepository, TaskRepository, EDMDetailRepository);
                decimal TaskHour = 0;
                try
                {
                    _phase = ProjectPhaseRepository.GetProjectPhases(_task.ProjectID).Where(p => p.PhaseID == _phaseID).FirstOrDefault();
                    _planDate = _phase.PlanCFinish == new DateTime(1, 1, 1) ? _phase.PlanFinish.ToString("yyyy-MM-dd") :
                    _phase.PlanCFinish.ToString("yyyy-MM-dd");                   
                }
                catch
                {
                    _planDate = "-";                    
                }
                try
                {
                    TaskHour = TaskHourRepository.GetTotalHourByTaskID(_task.TaskID);
                }
                catch
                {
                    TaskHour = 0;
                }
                string _machineCode = TaskHourRepository.GetMachineByTask(_task.TaskID) ?? "";
                string Operater = TaskHourRepository.GetOperaterByTaskID(_task.TaskID) ?? "";
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
                int _mgtype = (TasktypeRepo.TaskTypes.Where(t => t.ShortName == "MG" && t.Enable).FirstOrDefault() ?? new TaskType()).TaskID;//6
                if (_task.TaskType != _mgtype)
                {
                    taskType = TasktypeRepo.TaskTypes.Where(t => t.TaskID == _task.TaskType).Select(t => t.Name).FirstOrDefault();
                }
                else
                {
                    string _typeID = (_mgtype.ToString() + _task.OldID.ToString());
                    taskType = TasktypeRepo.TaskTypes.ToList().Where(t => t.TaskID == Convert.ToInt32(_typeID)).Select(t => t.Name).FirstOrDefault();
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
            ICNCMachInfoRepository MachInfoRepository, 
            ITaskRepository TaskRepository, 
            IEDMDetailRepository EDMDetailRepository)
        {
            CNCMachInfo _machinfo = null;
            switch (_task.TaskType){
                case 1:
                    
                    _machinfo = MachInfoRepository.QueryByELEIndex(_task.TaskID);
                    return _machinfo;
                case 2:
                    try
                    {
                        EDMDetail _detail = EDMDetailRepository.QueryByTaskID(_task.TaskID);
                        string[] _eleList = _detail.EleDetail.Split(';');
                        string _eleItem = _eleList[0].Substring(0, _eleList[0].Length - 4);
                        int _ver = Convert.ToInt16(_eleList[0].Substring(_eleList[0].IndexOf("_V") + 2));
                        Task _cnctask = TaskRepository.QueryByNameVersion(_eleItem, _ver);
                        _machinfo = MachInfoRepository.QueryByELEIndex(_cnctask.TaskID);
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

    }
}