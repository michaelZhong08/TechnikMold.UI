using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikMold.UI.Models.GridRowModel;
using TechnikMold.UI.Models.ViewModel;

namespace TechnikMold.UI.Models.GridViewModel
{
    public class SetupTaskGridViewModel
    {
        public List<SetupTaskGridRowModel> rows = new List<SetupTaskGridRowModel>();
        public int Page;
        public int Total;
        public int Records;
        public SetupTaskGridViewModel(List<SetupTaskStart> _setupTasks)
        {
            if (_setupTasks.Count > 0)
            {
                foreach (var t in _setupTasks)
                {
                    rows.Add(new SetupTaskGridRowModel(t));
                }
            }
            else
                rows = null;
        }
    }
}