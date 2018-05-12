using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain;
using MoldManager.WebUI.Models.GridRowModel;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class ElectrodeStockGridViewModel
    {
        public List<ElectrodeStockGridRowModel> rows = new List<ElectrodeStockGridRowModel>();
        public int Page;
        public int Total;
        public int Records;

        public ElectrodeStockGridViewModel(IEnumerable<CNCItem> CNCItems)
        {
            foreach (CNCItem _cncitem in CNCItems)
            {
                rows.Add(new ElectrodeStockGridRowModel(_cncitem));
            }
        }
    }
}