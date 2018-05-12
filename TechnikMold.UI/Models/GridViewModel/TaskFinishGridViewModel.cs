using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Entity;

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
                TaskFinishGridRowModel _row = new TaskFinishGridRowModel(_task);
                rows.Add(_row);
            }
        }

        public TaskFinishGridViewModel(IEnumerable<CNCItem> CNCItems)
        {
            foreach (CNCItem _item in CNCItems)
            {
                TaskFinishGridRowModel _row = new TaskFinishGridRowModel(_item);
                rows.Add(_row);
            }
        }
    }
}