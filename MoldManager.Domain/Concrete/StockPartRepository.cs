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
    public class StockPartRepository:IStockPartRepository
    {
        public IQueryable<StockPart> StockParts
        {
            get { throw new NotImplementedException(); }
        }

        public int Save(StockPart StockPart)
        {
            throw new NotImplementedException();
        }
    }
}
