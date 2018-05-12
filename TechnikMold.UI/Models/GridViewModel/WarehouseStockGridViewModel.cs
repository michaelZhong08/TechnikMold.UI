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

        public WarehouseStockGridViewModel(IEnumerable<WarehouseStock> StockItems,
            IUserRepository UserRepo, 
            IPurchaseItemRepository PurchaseItemRepo, 
            IPurchaseTypeRepository PurchaseTypeRepo, 
            IStockTypeRepository StockTypeRepo, 
            IWarehouseRepository WarehouseRepo, 
            IWarehousePositionRepository WarehousePositionRepo)
        {
            string UserName, PurchaseUserName, WarehouseUserName, PurchaseType, StockType, Warehouse, WarehousePosition;
            foreach (WarehouseStock _item in StockItems)
            {
                PurchaseItem _purchaseItem = PurchaseItemRepo.QueryByID(_item.PurchaseItemID);
                try
                {
                    UserName = UserRepo.GetUserByID(_purchaseItem.RequestUserID).FullName;
                }
                catch
                {
                    UserName = "";
                }
                try
                {
                    PurchaseUserName = UserRepo.GetUserByID(_purchaseItem.PurchaseUserID).FullName;

                }
                catch
                {
                    PurchaseUserName = "";
                }
                try
                {
                    WarehouseUserName = UserRepo.GetUserByID(_item.WarehouseUserID).FullName;
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
                    StockType = StockTypeRepo.QueryByID(_item.StockType).Name;
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
                
                rows.Add(new WarehouseStockGridRowModel(_item,
                    _purchaseItem,
                    UserName,
                    PurchaseUserName,
                    WarehouseUserName,
                    PurchaseType,
                    StockType, 
                    Warehouse, 
                    WarehousePosition));
            }
        }
    }
}