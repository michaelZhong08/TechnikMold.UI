using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class QCTaskGridViewModel
    {
        public List<QCTaskGridRowModel> rows;
        public int Page;
        public int Total;
        public int Records;

        public QCTaskGridViewModel(IEnumerable<QCTask> QCTasks)
        {
            rows = new List<QCTaskGridRowModel>();
            foreach (QCTask _task in QCTasks)
            {
                QCTaskGridRowModel _row = new QCTaskGridRowModel(_task);
                rows.Add(_row);
            }
        }
    }
}