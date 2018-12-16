using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class QuotationRequestRepository:IQuotationRequestRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IQueryable<QuotationRequest> QuotationRequests
        {
            get { return _context.QuotationRequests; }
        }

        public int Save(QuotationRequest QuotationRequest)
        {
            if (QuotationRequest.QuotationRequestID == 0)
            {
                _context.QuotationRequests.Add(QuotationRequest);
            }
            else
            {
                QuotationRequest _dbEntry = _context.QuotationRequests.Find(QuotationRequest.QuotationRequestID);
                if (_dbEntry != null)
                {
                    _dbEntry.QuotationNumber = QuotationRequest.QuotationNumber;
                    _dbEntry.ProjectID = QuotationRequest.ProjectID;
                    _dbEntry.CreateDate = QuotationRequest.CreateDate;
                    _dbEntry.RequestDate = QuotationRequest.RequestDate;
                    _dbEntry.PurchaseUserID = QuotationRequest.PurchaseUserID;
                    _dbEntry.DueDate = QuotationRequest.DueDate;
                    _dbEntry.Enabled = QuotationRequest.Enabled;
                    _dbEntry.State = QuotationRequest.State;
                    _dbEntry.PurchaseRequestID = QuotationRequest.PurchaseRequestID;
                    _dbEntry.Memo = QuotationRequest.Memo;
                    _dbEntry.QuotationGroupIDs = QuotationRequest.QuotationGroupIDs;
                }
            }
            _context.SaveChanges();
            return QuotationRequest.QuotationRequestID;
        }

        public QuotationRequest GetByID(int QuotationID)
        {
            QuotationRequest _dbEntry = _context.QuotationRequests.Find(QuotationID);
            return _dbEntry;
        }

        public void Delete(int QuotationRequestID)
        {
            QuotationRequest _dbEntry = GetByID(QuotationRequestID);
            _dbEntry.Enabled = false;
            _context.SaveChanges();
        }

        
        public void ChangeStatus(int QuotationID, int State)
        {
            QuotationRequest _dbEntry = GetByID(QuotationID);
            _dbEntry.State = State;
            _context.SaveChanges();
        }
    }
}
