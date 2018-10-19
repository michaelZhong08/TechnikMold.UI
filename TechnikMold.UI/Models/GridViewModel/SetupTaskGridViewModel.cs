using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikMold.UI.Models.GridRowModel;

namespace TechnikMold.UI.Models.GridViewModel
{
    public class SetupTaskGridViewModel
    {
        public List<SetupTaskGridRowModel> rows = new List<SetupTaskGridRowModel>();
        public int Page;
        public int Total;
        public int Records;
        public SetupTaskGridViewModel(List<Task> _tasks)
        {
            if (_tasks.Count > 0)
            {
                foreach (var t in _tasks)
                {
                    rows.Add(new SetupTaskGridRowModel(t));
                }
            }
            else
                rows = null;
        }
    }
}