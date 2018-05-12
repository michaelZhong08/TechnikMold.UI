using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Entity;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class CNCItemGridViewModel
    {

        public List<CNCItemGridRowModel> rows = new List<CNCItemGridRowModel>();
        public int Page;
        public int Total;
        public int Records;
        public CNCItemGridViewModel(List<CNCItem> CNCItems, List<Task> Tasks)
        {
            string _raw="";
            foreach (CNCItem _item in CNCItems)
            {
                _raw = Tasks.Where(t=>t.TaskID==_item.TaskID).Select(t=>t.Raw).FirstOrDefault();
                rows.Add(new CNCItemGridRowModel(_item, _raw));
            }
        }
    }
}