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
    public class PhaseRepository:IPhaseRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<Phase> Phases
        {
            get {
                return _context.Phases;
            }
        }

        public int Save(Phase Phase)
        {
            if (Phase.PhaseID == 0)
            {
                _context.Phases.Add(Phase);
            }
            else
            {
                Phase _dbEntry = _context.Phases.Find(Phase.PhaseID);
                if (_dbEntry != null)
                {
                    _dbEntry.Name = Phase.Name;
                    _dbEntry.Sequence = Phase.Sequence;
                    _dbEntry.Enabled = Phase.Enabled;
                }
            }
            _context.SaveChanges();
            return Phase.PhaseID;
        }

        public int Delete(int PhaseID)
        {
            Phase _dbEntry = _context.Phases.Find(PhaseID);
            if (_dbEntry != null)
            {
                _dbEntry.Enabled =!_dbEntry.Enabled;
            }
            _context.SaveChanges();
            return PhaseID;
        }
    }
}
