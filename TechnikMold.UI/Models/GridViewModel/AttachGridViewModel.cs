using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikMold.UI.Models.GridRowModel;

namespace TechnikMold.UI.Models.GridViewModel
{
    public class AttachGridViewModel
    {
        public List<AttachGridRowModel> rows = new List<AttachGridRowModel>();
        public int Page;
        public int Total;
        public int Records;
        public AttachGridViewModel(IQueryable<AttachFileInfo> _models)
        {
            foreach(var a in _models)
            {
                rows.Add(new AttachGridRowModel(a));
            }
        }
    }
}