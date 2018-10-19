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
    public class ListValueRepository:IListValueRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IQueryable<ListValue> ListValues
        {
            get 
            {
                return _context.ListValues;
            }
        }

        public int Save(ListValue ListValue)
        {
            if (ListValue.ListValueID == 0)
            {
                _context.ListValues.Add(ListValue);
            }else{
                ListValue _dbEntry =_context.ListValues.Find(ListValue.ListValueID);
                if (_dbEntry!=null){
                    _dbEntry.ListTypeID=ListValue.ListTypeID;
                    _dbEntry.Name=ListValue.Name;
                    _dbEntry.Enabled=ListValue.Enabled;
                }
            }
            _context.SaveChanges();
            return ListValue.ListValueID;
        }

        public int Delete(int ListValueID)
        {
            ListValue _dbEntry = _context.ListValues.Find(ListValueID);
            _dbEntry.Enabled=!_dbEntry.Enabled;
            _context.SaveChanges();
            return _dbEntry.ListTypeID;
        }
    }
}
