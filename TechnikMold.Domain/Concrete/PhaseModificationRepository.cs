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
    public class PhaseModificationRepository:IPhaseModificationRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<PhaseModification> PhaseModifications
        {
            
            get {
                return _context.PhaseModifications;
            }
        }

        public int Save(PhaseModification PhaseModification)
        {
            if (PhaseModification.Description == null)
            {
                PhaseModification.Description = "";
            }
            _context.PhaseModifications.Add(PhaseModification);
            _context.SaveChanges();
            return PhaseModification.PhaseModificationID;
        }
    }
}
