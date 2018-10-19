using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class StockTypeRepository:IStockTypeRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IEnumerable<StockType> StockTypes
        {
            get { return _context.StockTypes; }
        }

        public StockType QueryByID(int StockTypeID)
        {
            return _context.StockTypes.Find(StockTypeID);
        }


        public StockType QueryByName(string Name)
        {
            return _context.StockTypes.Where(s => s.Name == Name).Where(s=>s.Enabled==true).FirstOrDefault();
        }


        public int Save(string Name)
        {
            StockType _stockType = new StockType();
            _stockType.Name = Name;
            _stockType.Enabled = true;
            _context.StockTypes.Add(_stockType);
            _context.SaveChanges();
            return _stockType.StockTypeID;
        }


        public void Delete(int StockTypeID)
        {
            StockType _stockType = QueryByID(StockTypeID);
            if (_stockType != null)
            {
                _stockType.Enabled = false;
            }
            _context.SaveChanges();
        }
    }
}
