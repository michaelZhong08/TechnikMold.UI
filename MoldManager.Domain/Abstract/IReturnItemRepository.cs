using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IReturnItemRepository
    {
        IQueryable<ReturnItem> ReturnItems { get; }

        int Save(ReturnItem ReturnItem);

        void Delete(int ReturnItemID);

        ReturnItem QueryByPOItem(int PurchaseItemID);

        ReturnItem QueryByID(int ReturnItemID);

        IEnumerable<ReturnItem> QueryByRequest(int ReturnRequestID);



    }
}
