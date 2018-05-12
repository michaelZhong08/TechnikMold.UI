
/*
 * Create By:lechun1
 * 
 * Description: public interface for repository of TechnikSys.MoldManager.Domain.Entity.PurchaseRequest 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IPurchaseRequestRepository
    {

        IQueryable<PurchaseRequest> PurchaseRequests { get; }

        int Save(PurchaseRequest PurchaseRequest);

        PurchaseRequest GetByID(int PurchaseRequestID);

        void AssignSupplier(int PurchaseRequestID, int SupplierID, double TotalPrice, string Memo);

        void StatePromote(int PurchaseRequestID, bool Response = true);

        void Accept(int PurchaseRequestID, int UserID);

        void Refuse(int PurchaseRequestID, string Memo);

        void UpdateMemo(int PurchaseRequestID, string Memo);

        void Submit(int PurchaseRequestID, int State, string Memo, int UserID);

        void Restart(int PurchaseRequestID, string Memo);

        void Cancel(int PurchaseRequestID);
    }
}
