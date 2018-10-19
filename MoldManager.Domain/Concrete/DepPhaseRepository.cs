using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class DepPhaseRepository:IDepPhaseRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<Base_DepPhase> DepPhases
        {
            get
            {
                return _context.Base_DepPhases;
            }
        }

        public int Save(Base_DepPhase DepPhase)
        {
            if (DepPhase.Id == 0)
            {
                _context.Base_DepPhases.Add(DepPhase);
            }
            else
            {
                Base_DepPhase _dbEntry = _context.Base_DepPhases.Find(DepPhase.Id);
                if (_dbEntry != null)
                {
                    _dbEntry.DepId = DepPhase.DepId;
                    _dbEntry.PhaseId = DepPhase.PhaseId;
                    _dbEntry.Enable = DepPhase.Enable;
                }
            }
            _context.SaveChanges();
            return DepPhase.Id;
        }

        public IQueryable<Base_DepPhase> QueryByDepID(int DepId)
        {
            IQueryable<Base_DepPhase> _dbEntry = _context.Base_DepPhases.Where(b => b.DepId== DepId).Where(b => b.Enable == true);
            return _dbEntry;
        }

        public int Delete(int Id)
        {
            Base_DepPhase _dbEntry = _context.Base_DepPhases.Find(Id);
            _dbEntry.Enable = false;
            _context.SaveChanges();
            return Id;
        }
    }
}
