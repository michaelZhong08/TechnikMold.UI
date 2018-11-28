using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IStockTypeRepository
    {
        IEnumerable<StockType> StockTypes { get; }

        int Save(StockType model);

        StockType QueryByID(int StockTypeID);

        StockType QueryByName(string Name);

        void Delete(int StockTypeID);
        List<StockType> GetTypeList(string Parent);
    }
}
