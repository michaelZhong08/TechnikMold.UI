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
    public class PRProcessRepository:IPRProcessRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<PRProcess> PrProcesses
        {
            get {
                return _context.PRProcesses;
            }
        }

        public int Save(PRProcess PRProcess)
        {
            if (PRProcess.PRProcessID == 0)
            {
                _context.PRProcesses.Add(PRProcess);
            }
            else
            {
                PRProcess _dbEntry = _context.PRProcesses.Find(PRProcess.PRProcessID);
                if (_dbEntry != null)
                {
                    _dbEntry.PurchaseRequestID=PRProcess.PurchaseRequestID;
                    _dbEntry.UserID=PRProcess.UserID;
                    _dbEntry.ResponseType=PRProcess.ResponseType;
                    _dbEntry.Memo = PRProcess.Memo;
                }
            }
            _context.SaveChanges();
            return PRProcess.PRProcessID;
        }

        public IEnumerable<PRProcess> QueryByPRID(int PurchaseRequestID)
        {
            return _context.PRProcesses.Where(p => p.PurchaseRequestID == PurchaseRequestID);
        }
    }
}
