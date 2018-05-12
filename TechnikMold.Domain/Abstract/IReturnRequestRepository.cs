using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IReturnRequestRepository
    {
        IQueryable<ReturnRequest> ReturnRequests { get; }

        int Save(ReturnRequest ReturnRequest);

        void Delete(int ReturnRequestID);

        ReturnRequest QueryByID(int ReturnRequestID);

        void ChangeState(int ReturnRequestID, int State);
    }
}
