using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class PhaseTaskTypeRepository:IPhaseTaskTypeRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<PhaseTaskType> PhaseTaskTypes
        {
            get{ return _context.PhaseTaskTypes; }
        }
        public int Save(PhaseTaskType model)
        {
            return 1;
        }
    }
}
