using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class TaskFinishGridViewModel
    {
        public List<TaskFinishGridRowModel> rows = new List<TaskFinishGridRowModel>();
        public int Page;
        public int Total;
        public int Records;
        public TaskFinishGridViewModel(IEnumerable<Task> Tasks)
        {
            foreach (Task _task in Tasks)
            {
                //string machineName= _machinesinfoRepository.GetMInfoByCode(_task.)
                TaskFinishGridRowModel _row = new TaskFinishGridRowModel(_task);
                rows.Add(_row);
            }
        }

        public TaskFinishGridViewModel(IEnumerable<CNCItem> CNCItems, IMachinesInfoRepository _machinesinfoRepository)
        {
            foreach (CNCItem _item in CNCItems)
            {
                string machineName = (_machinesinfoRepository.GetMInfoByCode(_item.CNCMachine) ?? new MachinesInfo()).MachineName;
                TaskFinishGridRowModel _row = new TaskFinishGridRowModel(_item, machineName);
                rows.Add(_row);
            }
        }
    }
}