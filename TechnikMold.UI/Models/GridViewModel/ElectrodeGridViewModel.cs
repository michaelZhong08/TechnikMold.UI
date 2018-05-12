using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class ElectrodeGridViewModel
    {
        public List<ElectrodeGridRowModel> rows = new List<ElectrodeGridRowModel>();
        public int Page;
        public int Total;
        public int Records;

        public ElectrodeGridViewModel(IEnumerable<CNCItem> CNCItems,
            ITaskRepository TaskRepository, 
            IQCTaskRepository QCTaskRepository)
        {
            foreach (CNCItem _item in CNCItems)
            {
                string _taskName = TaskRepository.QueryByTaskID(_item.TaskID).TaskName;
                DateTime _qcDate;
                try
                {
                    _qcDate = QCTaskRepository.QueryByTaskID(_item.TaskID).FinishTime;
                }
                catch
                {
                    _qcDate = new DateTime(1900, 1, 1);
                }
                
                 rows.Add( new ElectrodeGridRowModel(_item, _taskName, _qcDate));

            }
        }
    }
}