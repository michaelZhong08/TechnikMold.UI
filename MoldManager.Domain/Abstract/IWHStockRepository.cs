using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IWHStockRepository
    {
        IQueryable<WHStock> WHStocks { get; }
        int Save(WHStock model);
        int StockIncrease(int stockID, decimal _qty);
        List<WHStock> GetWHStocks();
        List<WHStock> GetWHStocksByType(string _type);
        decimal GetStockQtyByPart(string PartNum, int PartID = 0);
        WHStock QueryByID(int stockID);
        int ChangeWHPosition(WHStock model);
        int StockReturn(int stockID, decimal _qty);
        WHStock GetStockByPartNum(string partNum, int partID);
    }
}
