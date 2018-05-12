using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class WarehouseRequestRepository:IWarehouseRequestRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<WarehouseRequest> WarehouseRequests
        {
            get { return _context.WarehouseRequests; }
        }

        public int Save(WarehouseRequest WarehouseRequest)
        {
            if (WarehouseRequest.WarehouseRequestID == 0)
            {
                _context.WarehouseRequests.Add(WarehouseRequest);
            }
            else
            {
                WarehouseRequest _dbEntry = QueryByID(WarehouseRequest.WarehouseRequestID);
                if (_dbEntry != null)
                {
                    _dbEntry.WarehouseRequestID = WarehouseRequest.WarehouseRequestID;
                    _dbEntry.RequestNumber = WarehouseRequest.RequestNumber;
                    _dbEntry.RequestUserID = WarehouseRequest.RequestUserID;
                    _dbEntry.WarehouseUserID = WarehouseRequest.WarehouseUserID;
                    _dbEntry.ApprovalUserID = WarehouseRequest.ApprovalUserID;
                    _dbEntry.CreateDate = WarehouseRequest.CreateDate;
                    _dbEntry.ApprovalDate = WarehouseRequest.ApprovalDate;
                    _dbEntry.WarehouseDate = WarehouseRequest.WarehouseDate;
                    _dbEntry.State = WarehouseRequest.State;
                    _dbEntry.Enabled = WarehouseRequest.Enabled;
                }
            }
            _context.SaveChanges();
            return WarehouseRequest.WarehouseRequestID;
        }

        public void Delete(int WarehouseRequestID)
        {
            WarehouseRequest _request = QueryByID(WarehouseRequestID);
            _request.Enabled =false;
            _context.SaveChanges();
        }

        public void ChangeStatus(int WarehouseRequestID, int State)
        {
            WarehouseRequest _request = QueryByID(WarehouseRequestID);
            _request.State = State;
            _context.SaveChanges();
        }

        //public IEnumerable<WarehouseRequest> QueryByMoldNumber(string MoldNumber)
        //{
        //    return _context.WarehouseRequests.Where(w => w.MoldNumber == MoldNumber);
        //}

        public WarehouseRequest QueryByID(int WarehouseReqeustID)
        {
            return _context.WarehouseRequests.Find(WarehouseReqeustID);
        }

        //private WarehouseRequest QueryByRequestNumber(string RequestNumber)
        //{
        //    return _context.WarehouseRequests.Where
        //}
    }
}
