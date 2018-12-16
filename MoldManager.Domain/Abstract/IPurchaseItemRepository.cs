using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IPurchaseItemRepository
    {
        IQueryable<PurchaseItem> PurchaseItems { get; }

        int Save(PurchaseItem PurchaseItem);

        void Delete(int PurchaseItemID);

        void ChangeState(int PurchaseItemID, int State);

        void ChangeState(int PurchaseRequestID, int QuotationRequestID, int PurchaseOrderID, int State);

        PurchaseItem QueryByID(int PurchaseItemID);

        IEnumerable<PurchaseItem> QueryByKeyword(string Keyword, int State = 100);

        IEnumerable<PurchaseItem> QueryBySupplier(int SupplierID);

        IEnumerable<PurchaseItem> QueryByPurchaseRequestID(int PurchaseRequestID);

        IEnumerable<PurchaseItem> QueryByQuotationRequestID(int QuotationRequestID);

        IEnumerable<PurchaseItem> QueryByPurchaseOrderID(int PurchaseOrderID);
        void PlanDateAdjust(int purchaseitemID, DateTime planDate);
        void PlanDateAdjustRecordSave(PurItemChangeDateRecord model);
        List<PurItemChangeDateRecord> GetPurItemChangeDateRecords(int PurchaseRequestID);
    }
}
