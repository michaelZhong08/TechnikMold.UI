using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridRowModel;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class WarehouseStockGridViewModel
    {
        public List<WarehouseStockGridRowModel> rows = new List<WarehouseStockGridRowModel>();
        public int Page;
        public int Total;
        public int Records;

        public WarehouseStockGridViewModel(List<WHStock> StockItems,
            IUserRepository UserRepo, 
            IPurchaseItemRepository PurchaseItemRepo, 
            IPurchaseTypeRepository PurchaseTypeRepo, 
            IStockTypeRepository StockTypeRepo, 
            IWarehouseRepository WarehouseRepo, 
            IWarehousePositionRepository WarehousePositionRepo,
            IWHPartRepository WHPartRepository)
        {
            string UserName, PurchaseUserName, WarehouseUserName, PurchaseType, StockType, Warehouse, WarehousePosition;
            foreach (var _item in StockItems)
            {
                //PurchaseItem _purchaseItem = PurchaseItemRepo.QueryByID(_item.PurchaseItemID);
                try
                {
                    //UserName = UserRepo.GetUserByID(_item.RequestUserID).FullName;
                    UserName = "";
                }
                catch
                {
                    UserName = "";
                }
                try
                {
                    //PurchaseUserName = UserRepo.GetUserByID(_purchaseItem.PurchaseUserID).FullName;
                    PurchaseUserName = "";

                }
                catch
                {
                    PurchaseUserName = "";
                }
                try
                {
                    //WarehouseUserName = UserRepo.GetUserByID(_item.WarehouseUserID).FullName;
                    WarehouseUserName = "";
                }
                catch
                {
                    WarehouseUserName = "";
                }

                try 
                { 
                    PurchaseType= PurchaseTypeRepo.QueryByID(_item.PurchaseType).Name;
                }
                catch
                {
                    PurchaseType = "";
                }
                try
                {
                    var _PartNumStrs = _item.PartNum.Split('-');
                    StockType = StockTypeRepo.StockTypes.Where(s => s.Code == _PartNumStrs[0]).FirstOrDefault().Name;//StockTypeRepo.QueryByID(_item.StockType).Name;
                }
                catch
                {
                    StockType = "";
                }
                try
                {
                    Warehouse = WarehouseRepo.QueryByID(_item.WarehouseID).Name;
                }
                catch
                {
                    Warehouse = "";
                }
                try
                {
                    WarehousePosition = WarehousePositionRepo.QueryByID(_item.WarehousePositionID).Name;
                }
                catch
                {
                    WarehousePosition = "";
                }
                WHPart _part1 = WHPartRepository.GetPart(_item.PartNum, _item.PartID) ?? new WHPart();
                rows.Add(new WarehouseStockGridRowModel(_item,
                    _part1,
                    //UserName,
                    //PurchaseUserName,
                    //WarehouseUserName,
                    PurchaseType,
                    StockType, 
                    Warehouse, 
                    WarehousePosition));
            }
        }
    }
}