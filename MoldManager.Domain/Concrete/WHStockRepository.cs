using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class WHStockRepository:IWHStockRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<WHStock> WHStocks { get { return _context.WHStocks; } }
        public int Save(WHStock model)
        {
            //WHStock _stock = _context.WHStocks.Where(s =>s.PartID==model.PartID && s.PartNum == model.PartNum && s.WarehouseID == model.WarehouseID && s.WarehousePositionID==model.WarehousePositionID && s.PurchaseType==model.PurchaseType).FirstOrDefault();
            
            if (model.ID == 0)
            {
                model.Enable = true;
                _context.WHStocks.Add(model);
            }
            else
            {
                WHStock _stock = _context.WHStocks.Where(s => s.ID == model.ID).FirstOrDefault();
                _stock.LInStockDate = model.LInStockDate;
            }
            _context.SaveChanges();
            return 1;
            //else
            //{

            //    _stock.Qty = model.Qty;
            //}
        }
        public int StockIncrease(WHStock model,decimal _qty)
        {
            //WHStock _stock = _context.WHStocks.Where(s =>s.PartID == model.PartID && s.PartNum == model.PartNum && s.WarehouseID == model.WarehouseID && s.WarehousePositionID == model.WarehousePositionID && s.PurchaseType == model.PurchaseType).FirstOrDefault();
            WHStock _stock = _context.WHStocks.Where(s => s.ID == model.ID).FirstOrDefault();
            if (_stock != null)
            {
                _stock.Qty = _stock.Qty + _qty;
                if (_qty > 0)//入
                {
                    _stock.InStockQty = _stock.InStockQty + _qty;
                }
                else//出
                {
                    _stock.OutStockQty = _stock.OutStockQty + _qty;
                }
                _context.SaveChanges();
                return 1;               
            }
            else
            {
                Save(model);
                StockIncrease(model, _qty);
            }
            return -99;
        }
        public List<WHStock> GetWHStocks()
        {
            List<WHStock> _stocks = _context.WHStocks.Where(s => s.Enable == true && s.Qty > 0).ToList();
            return _stocks;
        }
        /// <summary>
        /// 获取库存列表
        /// </summary>
        /// <param name="_type">模具直接材料/模具备库耗材/生产耗材</param>
        /// <returns></returns>
        public List<WHStock> GetWHStocksByType(string _type)
        {
            List<WHStock> _stocks = _context.WHStocks.Where(s => s.Enable == true && s.Qty>0).ToList();
            if (_type== "模具直接材料")
            {
                PurchaseType _type1 = _context.PurchaseTypes.Where(t => t.Name == "模具直接材料").FirstOrDefault();
                List<int> _types = _context.PurchaseTypes.Where(t => t.ParentTypeID == _type1.PurchaseTypeID).Select(t=>t.PurchaseTypeID).ToList();
                _stocks = _stocks.Where(s => _types.Contains(s.PurchaseType)).ToList();
            }
            else
            {
                List<int> _stypeids = _context.StockTypes.Where(t => t.Parent == _type).Select(t => t.StockTypeID).ToList();
                List<string> _partnums = _context.WHParts.ToList().Where(p => _stypeids.Contains(Convert.ToInt32(p.StockTypes))).Select(p=>p.PartNum).ToList();
                _stocks = _stocks.Where(s => _partnums.Contains(s.PartNum)).ToList();
            }
            return _stocks;
        }

        public decimal GetStockQtyByPart(string PartNum,int PartID = 0)
        {
            WHStock _stock = _context.WHStocks.Where(s => s.PartNum == PartNum && s.PartID == PartID).FirstOrDefault();
            if (_stock != null)
            {
                return _stock.Qty;
            }
            return 0;
        }
    }
}
