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
    public class ListTypeRepository:IListTypeRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<ListType> ListTypes
        {
            get {
                return _context.ListTypes;
            }
        }

        public int Save(ListType ListType)
        {
            if (ListType.ListTypeID == 0)
            {
                _context.ListTypes.Add(ListType);
            }
            else
            {
                ListType _dbEntry = _context.ListTypes.Find(ListType.ListTypeID);
                if (_dbEntry != null)
                {
                    _dbEntry.Name = ListType.Name;
                    _dbEntry.Enabled = ListType.Enabled;
                }
            }
            _context.SaveChanges();
            return ListType.ListTypeID;
        }

        public int Delete(int ListTypeID)
        {
            ListType _dbEntry = _context.ListTypes.Find(ListTypeID);
            if (_dbEntry != null)
            {
                _dbEntry.Enabled = !_dbEntry.Enabled;
            }
            _context.SaveChanges();
            return ListTypeID;
        }
    }
}
