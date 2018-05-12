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
    public class StockChangeRepository :IStockChangeRepository
    {
        public IQueryable<StockChange> StockChanges
        {
            get { throw new NotImplementedException(); }
        }

        public int Save(StockChange StockChange)
        {
            throw new NotImplementedException();
        }
    }
}
