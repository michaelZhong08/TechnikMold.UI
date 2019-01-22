using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class CNCItemGridViewModel
    {

        public List<CNCItemGridRowModel> rows = new List<CNCItemGridRowModel>();
        public int Page;
        public int Total;
        public int Records;
        public CNCItemGridViewModel(List<Task> Tasks,ICNCItemRepository _cncItemRepo)
        {
            //string _raw="";
            //foreach (CNCItem _item in CNCItems)
            //{
            //    _raw = Tasks.Where(t=>t.TaskID==_item.TaskID).Select(t=>t.Raw).FirstOrDefault();
            //    rows.Add(new CNCItemGridRowModel(_item, _raw));
            //}
            foreach(var t in Tasks)
            {
                List<CNCItem> _item = _cncItemRepo.QueryByTaskID(t.TaskID).ToList();
                foreach(var c in _item)
                {
                    rows.Add(new CNCItemGridRowModel(t,c));
                }
            }
        }
    }
}