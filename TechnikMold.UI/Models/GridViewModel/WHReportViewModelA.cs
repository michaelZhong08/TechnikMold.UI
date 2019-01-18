using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikMold.UI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikMold.UI.Models.GridViewModel
{
    public class WHReportViewModelA
    {
        public List<WHReportRowModelA> rows = new List<WHReportRowModelA>();
        public int Page;
        public int Total;
        public int Records;
        public WHReportViewModelA(List<TaskHour> _models,IMachinesInfoRepository MachinesInfoRepo
            ,ITaskTypeRepository TaskTypeRepo
            ,ITaskRepository TaskRepo)
        {
            foreach (var m in _models)
            {
                string mName = (MachinesInfoRepo.GetMInfoByCode(m.MachineCode) ?? new MachinesInfo()).MachineName;
                string tName = (TaskTypeRepo.TaskTypes.Where(t => t.TaskID == m.TaskType).FirstOrDefault() ?? new TaskType()).Name;
                string taskName = (TaskRepo.QueryByTaskID(m.TaskID) ?? new Task()).TaskName;
                rows.Add(new WHReportRowModelA(m, taskName,mName, tName));
            }
        }
    }
}