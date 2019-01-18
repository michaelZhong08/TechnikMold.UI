/*
 * Create By:lechun1
 * 
 * Description:
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class WarehouseStockRepository:IWarehouseStockRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<WarehouseStock> WarehouseStocks
        {
            get 
            {
                return _context.WarehouseStocks;
            }
        }

        public int Save(WarehouseStock WarehouseStock)
        {
            WarehouseStock _dbEntry;
            bool _isNew = false;
            if (WarehouseStock.WarehouseStockID==0)
            {
                _dbEntry = _context.WarehouseStocks.Where(w => w.Name == WarehouseStock.Name)
                    .Where(w => w.MaterialNumber == WarehouseStock.MaterialNumber)
                    .Where(w=>w.Specification==WarehouseStock.Specification)
                    .Where(w=>w.SupplierName ==WarehouseStock.SupplierName)
                    .FirstOrDefault();

                if (_dbEntry == null)
                {
                    WarehouseStock.Material = WarehouseStock.Material == null ? "" : WarehouseStock.Material;
                    _context.WarehouseStocks.Add(WarehouseStock);
                    _isNew = true;
                }
                else
                {
                    _dbEntry.Name = WarehouseStock.Name;
                    _dbEntry.Specification = WarehouseStock.Specification;
                    _dbEntry.SafeQuantity = WarehouseStock.SafeQuantity;
                    _dbEntry.WarehouseID = WarehouseStock.WarehouseID;
                    _dbEntry.Enabled = WarehouseStock.Enabled;
                    _dbEntry.MaterialNumber = WarehouseStock.MaterialNumber;
                    _dbEntry.SupplierID = WarehouseStock.SupplierID;
                    _dbEntry.SupplierName = WarehouseStock.SupplierName;
                    _dbEntry.PurchaseItemID = WarehouseStock.PurchaseItemID;
                    _dbEntry.PurchaseType = WarehouseStock.PurchaseType;
                    _dbEntry.StockType = WarehouseStock.StockType;
                    _dbEntry.InStockTime = WarehouseStock.InStockTime;
                    _dbEntry.InStockQty = WarehouseStock.InStockQty;
                    _dbEntry.WarehouseUserID = WarehouseStock.WarehouseUserID;
                    _dbEntry.Material = WarehouseStock.Material == null ? "" : WarehouseStock.Material;
                    _dbEntry.MoldNumber = WarehouseStock.MoldNumber;
                    _dbEntry.PlanQty = WarehouseStock.PlanQty;
                    _dbEntry.WarehousePositionID = WarehouseStock.WarehousePositionID;
                    _dbEntry.OutStockQty = WarehouseStock.OutStockQty;
                }
                
                
            }
            else
            {                
                _dbEntry = _context.WarehouseStocks.Find(WarehouseStock.WarehouseStockID);
                _dbEntry.Name = WarehouseStock.Name;
                _dbEntry.Specification = WarehouseStock.Specification;
                _dbEntry.SafeQuantity = WarehouseStock.SafeQuantity;
                _dbEntry.WarehouseID = WarehouseStock.WarehouseID;
                _dbEntry.Enabled = WarehouseStock.Enabled;
                _dbEntry.MaterialNumber = WarehouseStock.MaterialNumber;
                _dbEntry.SupplierID = WarehouseStock.SupplierID;
                _dbEntry.SupplierName = WarehouseStock.SupplierName;
                _dbEntry.PurchaseItemID = WarehouseStock.PurchaseItemID;
                _dbEntry.PurchaseType = WarehouseStock.PurchaseType;
                _dbEntry.StockType = WarehouseStock.StockType;
                _dbEntry.InStockTime = WarehouseStock.InStockTime;
                _dbEntry.InStockQty = WarehouseStock.InStockQty;
                _dbEntry.WarehouseUserID = WarehouseStock.WarehouseUserID;
                _dbEntry.Material = WarehouseStock.Material;
                _dbEntry.MoldNumber = WarehouseStock.MoldNumber;
                _dbEntry.PlanQty = WarehouseStock.PlanQty;
                _dbEntry.WarehousePositionID = WarehouseStock.WarehousePositionID;
                _dbEntry.OutStockQty = WarehouseStock.OutStockQty;
            }
            _context.SaveChanges();

            if (_isNew)
            {
                return WarehouseStock.WarehouseStockID;
            }
            else
            {
                return _dbEntry.WarehouseStockID;
            }
            
        }

        public IEnumerable<WarehouseStock> StockWarning(int WarehouseID = 0)
        {
            IEnumerable<WarehouseStock> _warningItems;
            if (WarehouseID==0){
                _warningItems = _context.WarehouseStocks.Where(w => w.Quantity < w.SafeQuantity);
            }else{
                _warningItems = _context.WarehouseStocks.Where(w=>w.WarehouseID==WarehouseID).Where(w => w.Quantity < w.SafeQuantity);
            }
            return _warningItems;
        }

        /// <summary>
        /// Stock item quantity modification
        /// </summary>
        /// <param name="WarehouseStockID"></param>
        /// <param name="Quantity"></param>
        /// <param name="WarehouseID"></param>
        /// <param name="Type"> Update Operation Type
        /// 1:入库
        /// 2:出库
        /// </param>
        /// <returns></returns>
        public int UpdateQuantity(int WarehouseStockID, int Quantity, int WarehouseID = 1)
        {
            WHStock _dbEntry = _context.WHStocks.Find(WarehouseStockID);
            if (_dbEntry != null)
            {
                _dbEntry.InStockQty = _dbEntry.InStockQty + Quantity;
                if (Quantity < 0)
                {
                    _dbEntry.OutStockQty = _dbEntry.OutStockQty+( - Quantity);
                }
                _dbEntry.Qty = _dbEntry.Qty + Quantity;
            }
            _dbEntry.LInStockDate = DateTime.Now;
            _context.SaveChanges();
            return _dbEntry.ID;
        }

        private WarehouseStock GetStock(string Specification, int WarehouseID)
        {
            WarehouseStock _dbEntry = _context.WarehouseStocks.Where(w => w.Specification == Specification).Where(w => w.WarehouseID == WarehouseID).FirstOrDefault();
            return _dbEntry;
        }


        public int EleInStock(string Name)
        {
            WarehouseStock _stock = new WarehouseStock();
            _stock.Name = Name;
            _stock.Quantity = 1;
            _stock.Specification = "";
            _stock.Enabled = true;
            _context.WarehouseStocks.Add(_stock);
            _context.SaveChanges();
            return _stock.WarehouseStockID;
        }


        public int GetTotalStock(string Specification)
        {
            try
            {
                return _context.WarehouseStocks.Where(w => w.Specification == Specification).Sum(w => w.Quantity);
            }
            catch
            {
                return 0;
            }
           
        }


        public WarehouseStock QueryByID(int WarehouseStockID)
        {
            return _context.WarehouseStocks.Find(WarehouseStockID);
        }


        public IEnumerable<WarehouseStock> QueryByMoldNumber(string MoldNumber)
        {
            IEnumerable<WarehouseStock> _stocks = _context.WarehouseStocks.Where(w => w.MoldNumber.ToUpper().Contains(MoldNumber.ToUpper()) && w.Enabled==true);
            return _stocks;
        }


        public void SetSafeQty(string WarehouseStockIDs, int Quantity)
        {
            string[] _stockIDs = WarehouseStockIDs.Split(',');
            WarehouseStock _stock;

            for (int i = 0; i < _stockIDs.Length; i++)
            {
                _stock = _context.WarehouseStocks.Find(Convert.ToInt32(_stockIDs[i]));
                if (_stock != null)
                {
                    _stock.SafeQuantity = Quantity;
                }
            }
            _context.SaveChanges();
        }


        public void DeleteStock(int WarehouseStockID)
        {
            WarehouseStock _dbEntry = _context.WarehouseStocks.Find(WarehouseStockID);
            _dbEntry.Enabled = false;
            _context.SaveChanges();
        }
    }
}
