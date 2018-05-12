using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class PartGridViewModel
    {
        public List<PartGridRowModel> rows;
        public int Page;
        public int Total;
        public int Records;

        public PartGridViewModel(IEnumerable<Part> Parts, IWarehouseStockRepository WarehouseRepository)
        {
            rows = new List<PartGridRowModel>();
            int i =1;
            foreach (Part _part in Parts)
            {
                string _partSpec = _part.Specification;
                string _partMaterial = _part.MaterialName;
                int _stock = WarehouseRepository.GetTotalStock(_partSpec);
                rows.Add(new PartGridRowModel(i, _part, _stock));
                i++;
            }
            Page = 1;
            Total = 200;
            Records = 10;
        }
    }
}