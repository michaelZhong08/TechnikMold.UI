using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class CharmillRepository:ICharmillRepository
    {
        private EFDbContext _context = new EFDbContext();



        public IQueryable<Charmill> Charmills
        {
            get { return _context.Charmills; }
        }




        public int QueryProgram(string Name, string Type, string Surface, string Material, double Gap, string Obit = "")
        {
            Charmill _charmill = _context.Charmills.Where(c => c.Name == Name).Where(c => c.Type == Type).Where(c => c.Surface == Surface)
                            .Where(c => c.Material == Material).FirstOrDefault();
            return _charmill.Program_Number;
        }
    }
}
