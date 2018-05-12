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
    public class SupplierCategoryRepository:ISupplierCategoryRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<SupplierCategory> SupplierCategories
        {
	        get {
                return _context.SupplierCategories;
            }
        }

        public int Save(SupplierCategory SupplierCategory)
        {
            SupplierCategory _dbEntry;
            int id = 0;
            if (SupplierCategory.SupplierCategoryID == 0)
            {
                _dbEntry = _context.SupplierCategories.Where(s => s.SupplierID == SupplierCategory.SupplierID).Where(s => s.PartTypeID == SupplierCategory.PartTypeID).FirstOrDefault();
                if (_dbEntry == null)
                {
                    _context.SupplierCategories.Add(SupplierCategory);
                }
                else
                {
                    id = _dbEntry.SupplierCategoryID;
                    _dbEntry.Enabled=true;
                }
            }
            else
            {
                _dbEntry = _context.SupplierCategories.Find(SupplierCategory.SupplierCategoryID);
                if (_dbEntry != null)
                {
                    _dbEntry.SupplierID = SupplierCategory.SupplierID;
                    _dbEntry.PartTypeID = SupplierCategory.PartTypeID;
                    _dbEntry.Enabled = SupplierCategory.Enabled;
                }
            }
            if (id == 0)
            {
                _context.SaveChanges();
                id = SupplierCategory.SupplierCategoryID;
            }
            return id;
        }

        public void Delete(int SupplierCategoryID)
        {
            SupplierCategory _dbEntry = _context.SupplierCategories.Find(SupplierCategoryID);
            _dbEntry.Enabled = false;
        }


        public IEnumerable<SupplierCategory> QueryByCategory(int PartTypeID)
        {
            return _context.SupplierCategories.Where(s => s.PartTypeID == PartTypeID).Where(s => s.Enabled == true);
        }

        public IEnumerable<SupplierCategory> QueryBySupplier(int SupplierID)
        {
            return _context.SupplierCategories.Where(s => s.SupplierID == SupplierID).Where(s => s.Enabled == true);
        }
    }
}
