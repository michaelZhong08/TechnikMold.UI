/*
 * Create By:lechun1
 * 
 * Description:
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;



namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class PurchaseRequestRepository:IPurchaseRequestRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<PurchaseRequest> PurchaseRequests
        {
            get 
            {
                return _context.PurchaseRequests;
            }
        }

        public int Save(PurchaseRequest PurchaseRequest)
        {
            if (PurchaseRequest.PurchaseRequestID == 0)
            {
                PurchaseRequest.State = 1;
                _context.PurchaseRequests.Add(PurchaseRequest);
            }
            else
            {
                PurchaseRequest _dbEntry = _context.PurchaseRequests.Find(PurchaseRequest.PurchaseRequestID);
                if (_dbEntry != null)
                {
                    _dbEntry.PurchaseRequestNumber = PurchaseRequest.PurchaseRequestNumber;
                    _dbEntry.State = PurchaseRequest.State;
                    _dbEntry.Responsible = PurchaseRequest.Responsible;
                    _dbEntry.Approval = PurchaseRequest.Approval;
                    _dbEntry.ProjectID = PurchaseRequest.ProjectID;
                    _dbEntry.CreateDate = PurchaseRequest.CreateDate;
                    _dbEntry.AcceptDate = PurchaseRequest.AcceptDate;
                    _dbEntry.ApprovalDate = PurchaseRequest.ApprovalDate;
                    _dbEntry.TotalPrice = PurchaseRequest.TotalPrice;
                    _dbEntry.SupplierID = PurchaseRequest.SupplierID;
                    _dbEntry.Memo = PurchaseRequest.Memo;
                    _dbEntry.DueDate = PurchaseRequest.DueDate;
                    _dbEntry.PurchaseType = PurchaseRequest.PurchaseType;
                    _dbEntry.ApprovalERPUserID = PurchaseRequest.ApprovalERPUserID;
                    _dbEntry.Enabled = PurchaseRequest.Enabled;
                }
            }
            _context.SaveChanges();
            return PurchaseRequest.PurchaseRequestID;
        }

        /// <summary>
        /// Get the purchase order by PurchaseRequestID
        /// </summary>
        /// <param name="PurchaseRequestID">Primary key of purchase request</param>
        /// <returns></returns>
        public PurchaseRequest GetByID(int PurchaseRequestID)
        {
            return _context.PurchaseRequests.Find(PurchaseRequestID);
        }

        /// <summary>
        /// Assign the supplier for the purchase request
        /// </summary>
        /// <param name="PurchaseID"></param>
        /// <param name="SupplierID"></param>
        public void AssignSupplier(int PurchaseRequestID, int SupplierID, double TotalPrice, string Memo)
        {
            PurchaseRequest _dbEntry = _context.PurchaseRequests.Find(PurchaseRequestID);
            if (_dbEntry != null) { 
                _dbEntry.SupplierID = SupplierID;
                _dbEntry.TotalPrice = TotalPrice;
                _dbEntry.Memo = Memo;
            }
            _context.SaveChanges();
        }




        /// <summary>
        /// Changes the state field of purchase request
        /// </summary>
        /// <param name="PurchaseRequestID"></param>
        /// <param name="ResponseType"></param>
        public void StatePromote(int PurchaseRequestID, bool ResponseType = true)
        {
            PurchaseRequest _dbEntry = _context.PurchaseRequests.Find(PurchaseRequestID);
            if (_dbEntry != null)
            {
                if (ResponseType)
                {
                    _dbEntry.State = _dbEntry.State + 1;
                }
                else
                {
                    _dbEntry.State = _dbEntry.State - 1;
                    _dbEntry.SupplierID = 0;
                    _dbEntry.TotalPrice = 0;
                }
            }
            _context.SaveChanges();
        }

        //Assign the PU user of the purchase request
        public void Accept(int PurchaseRequestID, int UserID)
        {
            PurchaseRequest _dbEntry = _context.PurchaseRequests.Find(PurchaseRequestID);
            if (_dbEntry != null)
            {
                _dbEntry.Responsible = UserID;
            }
            _context.SaveChanges();
        }

        public void Refuse(int PurchaseRequestID, string Memo)
        {
            PurchaseRequest _dbEntry = _context.PurchaseRequests.Find(PurchaseRequestID); 
            if (_dbEntry != null)
            {
                _dbEntry.State = -99;
                _dbEntry.Memo = Memo;
            }
            _context.SaveChanges();
        }


        public void UpdateMemo(int PurchaseRequestID, string Memo)
        {
            PurchaseRequest _dbEntry = _context.PurchaseRequests.Find(PurchaseRequestID);
            if (_dbEntry != null)
            {
                _dbEntry.Memo = Memo;
            }
            _context.SaveChanges();
        }


        



        public void Restart(int PurchaseRequestID, string Memo)
        {
            PurchaseRequest _dbEntry = _context.PurchaseRequests.Find(PurchaseRequestID);
            if (_dbEntry != null)
            {
                _dbEntry.Memo = Memo;
                _dbEntry.State = 1;
            }
            _context.SaveChanges();
        }

        public void Submit(int PurchaseRequestID, int State, string Memo, int UserID)
        {
            PurchaseRequest _dbEntry = GetByID(PurchaseRequestID);
            if (_dbEntry != null)
            {
                _dbEntry.State = State;
                if (Memo != "")
                {
                    _dbEntry.Memo = _dbEntry.Memo+" "+Memo;
                }
                switch (State){
                    case 5:
                        _dbEntry.SubmitUserID=UserID;
                        break;
                    case 10:
                        _dbEntry.ReviewUserID=UserID;
                        _dbEntry.ApprovalDate = DateTime.Now;
                        break;
                    case -99:
                        _dbEntry.ReviewUserID=UserID;
                        _dbEntry.ApprovalDate = DateTime.Now;
                        break;
                }
            }
            _context.SaveChanges();
        }


        public void Cancel(int PurchaseRequestID)
        {
            PurchaseRequest _dbEntry = GetByID(PurchaseRequestID);
            if (_dbEntry != null)
            {
                _dbEntry.Enabled = false;
            }
            _context.SaveChanges();
        }


    }
}
