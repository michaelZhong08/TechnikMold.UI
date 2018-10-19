using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class PartStockRepository:IPartStockRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<PartStock> PartStocks
        {
            get { return _context.PartStocks; }
        }

        public PartStock QueryByRawNo(string RawNo)
        {
            PartStock _stock = _context.PartStocks.Where(p => p.RawNO == RawNo).FirstOrDefault();
            return _stock;
        }


    }
}
