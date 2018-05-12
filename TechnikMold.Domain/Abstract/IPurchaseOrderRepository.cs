using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IPurchaseOrderRepository
    {
        IQueryable<PurchaseOrder> PurchaseOrders { get; }
        int Save(PurchaseOrder PurchaseOrder);
        

        PurchaseOrder QueryByPRID(int PurchaseRequestID);

        PurchaseOrder QueryByID(int PurchaseOrderID);

        void ClosePurchaseOrder(int PurchaseOrderID);

        void PartialClosePO(int PurchaseOrderID);

        void Submit(int PurchaseOrderID, int State, string Memo="");
    }
}
