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
    public class CostCenterRepository:ICostCenterRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<CostCenter> CostCenters
        {
            get {
                return _context.CostCenters.OrderBy(c=>c.Name);
            }
        }

        public int Save(CostCenter CostCenter)
        {
            if (CostCenter.CostCenterID == 0)
            {
                _context.CostCenters.Add(CostCenter);
            }
            else
            {
                CostCenter _dbEntry = _context.CostCenters.Find(CostCenter.CostCenterID);
                if (_dbEntry != null)
                {
                    _dbEntry.DepCode = CostCenter.DepCode;
                    _dbEntry.Name = CostCenter.Name;
                    _dbEntry.Enabled = CostCenter.Enabled;
                }
            }
            _context.SaveChanges();
            return CostCenter.CostCenterID;
        }

        public void Delete(int CostCenterID)
        {
            CostCenter _dbEntry = _context.CostCenters.Find(CostCenterID);
            if (_dbEntry != null)
            {
                _dbEntry.Enabled = false;
            }
            _context.SaveChanges();
        }


        public CostCenter QueryByID(int CostCenterID)
        {
            CostCenter _costcenter = _context.CostCenters.Find(CostCenterID);
            return _costcenter;
        }


        public IEnumerable<CostCenter> Query(string Keyword)
        {
            return _context.CostCenters.Where(c=>c.Enabled==true).Where(c => c.Name.Contains(Keyword));
        }
    }
}
