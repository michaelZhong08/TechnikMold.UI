using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class ReturnRequestRepository:IReturnRequestRepository
    {

        private EFDbContext _context = new EFDbContext();
        public IQueryable<ReturnRequest> ReturnRequests
        {
            get { return _context.ReturnRequests.Where(w=>w.Enabled==true); }
        }

        public int Save(ReturnRequest ReturnRequest)
        {
            ReturnRequest _dbEntry;
            if (ReturnRequest.ReturnRequestID == 0)
            {
                _context.ReturnRequests.Add(ReturnRequest);
            }
            else
            {
                _dbEntry = QueryByID(ReturnRequest.ReturnRequestID);
                _dbEntry.ReturnRequestID = ReturnRequest.ReturnRequestID;
                _dbEntry.PurchaseOrderID = ReturnRequest.PurchaseOrderID;
                _dbEntry.CreateDate = ReturnRequest.CreateDate;
                _dbEntry.ApprovalDate = ReturnRequest.ApprovalDate;
                _dbEntry.ReturnDate = ReturnRequest.ReturnDate;
                _dbEntry.WarehouseUserID = ReturnRequest.WarehouseUserID;
                _dbEntry.ApprovalUserID = ReturnRequest.ApprovalUserID;
                _dbEntry.State = ReturnRequest.State;
                _dbEntry.Enabled = ReturnRequest.Enabled;
                _dbEntry.SupplierID = ReturnRequest.SupplierID;
                _dbEntry.SupplierName = ReturnRequest.SupplierName;

            }
            _context.SaveChanges();
            return ReturnRequest.ReturnRequestID;
        }

        public void Delete(int ReturnRequestID)
        {
            ReturnRequest _request = QueryByID(ReturnRequestID);
            _request.Enabled = false;
            _context.SaveChanges();
        }



        public ReturnRequest QueryByID(int ReturnRequestID)
        {
            return _context.ReturnRequests.Find(ReturnRequestID);
        }

        public void ChangeState(int ReturnRequestID, int State)
        {
            ReturnRequest _request= QueryByID(ReturnRequestID);


            if (_request != null)
            {
                _request.State = State;
                _context.SaveChanges();
            }
        }
    }
}
