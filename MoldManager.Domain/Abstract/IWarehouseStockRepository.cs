using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IWarehouseStockRepository
    {
        IQueryable<WarehouseStock> WarehouseStocks { get; }

        int Save(WarehouseStock WarehouseStock);

        IEnumerable<WarehouseStock> StockWarning(int WarehouseID=0);

        int UpdateQuantity(int WarehouseStockID, int Quantity, int WarehouseID=1);

        int EleInStock(string Name);

        int GetTotalStock(string Specification);

        WarehouseStock QueryByID(int WarehouseStockID);

        IEnumerable<WarehouseStock> QueryByMoldNumber(string MoldNumber);

        void SetSafeQty(string WarehouseStockIDs, int Quantity);

        void DeleteStock(int WarehouseStockID);
    }
}
