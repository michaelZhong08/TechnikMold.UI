using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IQuotationRequestRepository
    {
        IQueryable<QuotationRequest> QuotationRequests { get; }

        int Save(QuotationRequest QuotationRequest);

        void Delete(int QuotationRequestID);

        QuotationRequest GetByID(int QuotationID);

        void ChangeStatus(int QuotationID, int State);
    }
}
