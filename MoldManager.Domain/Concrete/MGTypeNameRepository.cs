using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class MGTypeNameRepository: IMGTypeNameRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<MGTypeName> MGTypeNames
        {
            get
            {
                return _context.MGTypeNames.Where(m=>m.active);
            }
        }
        public int Save(MGTypeName model)
        {
            return 0;
        }
    }
}
