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
    public class HardnessRepository:IHardnessRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<Hardness> Hardnesses
        {
            get 
            {
                return _context.Hardnesses;
            }
        }

        public int Save(Hardness Hardness)
        {
            if (Hardness.HardnessID == 0)
            {
                _context.Hardnesses.Add(Hardness);
            }
            else
            {
                Hardness _dbEntry = _context.Hardnesses.Find(Hardness.HardnessID);
                if (_dbEntry != null)
                {
                    _dbEntry.Value = Hardness.Value;
                    _dbEntry.MaterialID = Hardness.MaterialID;
                    _dbEntry.Enabled = Hardness.Enabled;
                }
            }
            _context.SaveChanges();
            return Hardness.HardnessID;
        }

        public IEnumerable<Hardness> QueryByMaterial(int MaterialID)
        {
            IEnumerable<Hardness> _hardnesses = _context.Hardnesses.Where(h => h.MaterialID == MaterialID).Where(h=>h.Enabled==true);
            return _hardnesses;
        }


        public void Delete(int HardnessID)
        {
            Hardness _dbEntry = _context.Hardnesses.Find(HardnessID);
            _dbEntry.Enabled = false;
            _context.SaveChanges();
        }
    }
}
