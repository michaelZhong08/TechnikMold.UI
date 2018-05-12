﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Abstract;


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
            ITaskRepository TaskRepository, IProjectPhaseRepository ProjectPhaseRepository)
        {
            ProjectPhase _phase;
            string _cad, _cam, _workshop, _qc, _planDate;
            int _phaseID=0;
            rows = new List<TaskGridRowModel>();
           
            foreach (Task _task in Tasks)
            {
                switch (_task.TaskType)
                {
                    case 1:
                        _phaseID = 8;
                        break;
                    case 2:
                        _phaseID = 9;
                        break;
                    case 3:
                        _phaseID = 10;
                        break;
                    case 4:
                        _phaseID = 8;
                        break;
                    case 6:
                        _phaseID = 7;
                        break;
                }
                _cad = _task.CADUser > 0 ? UserRepository.GetUserByID(_task.CADUser).FullName : "";
                _cam = _task.CAMUser>0?UserRepository.GetUserByID(_task.CAMUser).FullName:"";
                _workshop = _task.WorkshopUser>0?UserRepository.GetUserByID(_task.WorkshopUser).FullName:"";
                _qc = _task.QCUser > 0 ? UserRepository.GetUserByID(_task.QCUser).FullName : "";
                CNCMachInfo _machinfo=GetCNCMachinfo(_task,MachInfoRepository, TaskRepository, EDMDetailRepository);
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
                
                
                rows.Add( new TaskGridRowModel(_task, _cad, _cam, _workshop, _qc, CAMDrawingPath, _planDate, _machinfo));
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