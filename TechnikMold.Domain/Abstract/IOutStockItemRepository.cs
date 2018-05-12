using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IOutStockItemRepository
    {
        IQueryable<OutStockItem> OutStockItems { get; }

        int Save(OutStockItem Item);

        IEnumerable<OutStockItem> QueryByFormID(int FormID);

        IEnumerable<OutStockItem> QueryByRequestID(int RequestID);
    }
}
