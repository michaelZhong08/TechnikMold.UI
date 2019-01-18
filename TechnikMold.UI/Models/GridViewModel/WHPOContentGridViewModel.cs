using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoldManager.WebUI.Models.GridRowModel;
using MoldManager.WebUI.Models.Helpers;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;


namespace MoldManager.WebUI.Models.GridViewModel
{
    public class WHPOContentGridViewModel
    {
        public List<WHPOContentGridRowModel> rows;

        public int Page;
        public int Total;
        public int Records;

        public WHPOContentGridViewModel(IEnumerable<PurchaseItem> PurchaseItems
            ,IWarehouseRepository _warehouseRepo
            ,IWarehousePositionRepository _warehousePositonRepo
            ,IWHStockRepository _whstockRepo)
        {
            rows = new List<WHPOContentGridRowModel>();
            Page = 1;
            Total = PurchaseItems.Count();
            foreach (PurchaseItem _item in PurchaseItems)
            {
                string warehoseListStr = string.Empty;
                string whpositionListStr = string.Empty;
                WHStock _stock = (_whstockRepo.WHStocks.Where(s => s.PartNum == _item.PartNumber && s.PartID == _item.PartID && s.Enable == true).FirstOrDefault()??new WHStock());
                if (_stock.ID == 0)
                {
                    warehoseListStr = "<select id='wh" + _item.PurchaseItemID.ToString() + "' class='form-control'>";
                    whpositionListStr = "<select id='whposi" + _item.PurchaseItemID.ToString() + "' class='form-control'>";
                    foreach (var w in _warehouseRepo.Warehouses.ToList())
                    {
                        if (_stock.WarehouseID == w.WarehouseID)
                        {
                            warehoseListStr = warehoseListStr + "<option value='" + w.WarehouseID.ToString() + "' selected>" + w.Name + "</option>";
                        }
                        else
                        {
                            warehoseListStr = warehoseListStr + "<option value='" + w.WarehouseID.ToString() + "'>" + w.Name + "</option>";
                        }
                    }
                    warehoseListStr = warehoseListStr + "</select>";
                    List<WarehousePosition> _whpositionList = _warehousePositonRepo.QueryByWarehouse(_warehouseRepo.Warehouses.FirstOrDefault().WarehouseID).ToList();
                    foreach (var p in _whpositionList)
                    {
                        if (_stock.WarehousePositionID == p.WarehousePositionID)
                        {
                            whpositionListStr = whpositionListStr + "<option value='" + p.WarehousePositionID.ToString() + "' selected>" + p.Name + "</option>";
                        }
                        else
                        {
                            whpositionListStr = whpositionListStr + "<option value='" + p.WarehousePositionID.ToString() + "'>" + p.Name + "</option>";
                        }
                    }
                }
                else
                {
                    warehoseListStr = "<input id='wh"+ _item.PurchaseItemID.ToString() + "' value='" + _stock.WarehouseID.ToString() + "' hidden/> " + _warehouseRepo.QueryByID(_stock.WarehouseID).Name;
                    whpositionListStr = "<input id='whposi" + _item.PurchaseItemID.ToString() + "' value='" + _stock.WarehousePositionID.ToString() + "' hidden/> " + _warehousePositonRepo.QueryByID(_stock.WarehousePositionID).Name;
                }
                
                whpositionListStr= whpositionListStr + "</select>";
                rows.Add(new WHPOContentGridRowModel(_item, warehoseListStr, whpositionListStr, _stock));
            }
        }
    }
}