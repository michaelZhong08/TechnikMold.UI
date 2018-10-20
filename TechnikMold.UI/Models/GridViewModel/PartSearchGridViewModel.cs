﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikMold.UI.Models.GridRowModel;
using MoldManager.WebUI.Models.GridRowModel;
using MoldManager.WebUI.Models.Helpers;

namespace TechnikMold.UI.Models.GridViewModel
{
    public class PartSearchGridViewModel
    {
        public List<PartSearchGridRowModel> rows = new List<PartSearchGridRowModel>();
        public int Page;
        public int Total;
        public int Records;
        public PartSearchGridViewModel(List<Part> _parts)
        {
            foreach(var p in _parts)
            {
                rows.Add(new PartSearchGridRowModel(p));
            }
        }
    }
}