using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class SupplierBrandRepository:ISupplierBrandRepository
    {
        private EFDbContext _context = new EFDbContext();


        public IQueryable<SupplierBrand> SupplierBrands
        {
            get { return _context.SupplierBrands; }
        }

        public int Save(SupplierBrand SupplierBrand)
        {
            //bool _isNew = true;
            SupplierBrand _dbEntry;
            if (SupplierBrand.SupplierBrandID == 0)
            {
                _dbEntry = Query(SupplierBrand.SupplierID, SupplierBrand.BrandID);
                if (_dbEntry == null)
                {
                    _context.SupplierBrands.Add(SupplierBrand);
                    
                }
                else
                {
                    return _dbEntry.SupplierBrandID;
                }
            }
            else
            {
                _dbEntry = _context.SupplierBrands.Find(SupplierBrand.SupplierBrandID);
                if (_dbEntry != null)
                {
                    _dbEntry.SupplierID = SupplierBrand.SupplierID;
                    _dbEntry.BrandID = SupplierBrand.BrandID;
                }
                else
                {
                    SupplierBrand.SupplierBrandID = 0;
                    _context.SupplierBrands.Add(SupplierBrand);
                }
            }
            _context.SaveChanges();
            return SupplierBrand.SupplierBrandID;
        }

        public void Delete(int SupplierID, int BrandID)
        {
            SupplierBrand _dbEntry = _context.SupplierBrands.Where(s => s.SupplierID == SupplierID)
                .Where(s => s.BrandID == BrandID)
                .Where(s => s.Enabled == true).FirstOrDefault();
            if (_dbEntry != null)
            {
                _dbEntry.Enabled = false;
                _context.SaveChanges();
            }
        }

        public void Delete(int SupplierBrandID)
        {
            SupplierBrand _dbEntry = _context.SupplierBrands.Find(SupplierBrandID);
            if (_dbEntry != null)
            {
                _dbEntry.Enabled = false;
                _context.SaveChanges();
            }
        }

        public IEnumerable<SupplierBrand> QueryBySupplier(int SupplierID)
        {
            IEnumerable<SupplierBrand> _data = _context.SupplierBrands
                .Where(s => s.SupplierID == SupplierID)
                .Where(s => s.Enabled == true);
            return _data;

        }


        public IEnumerable<SupplierBrand> QueryByBrand(int BrandID)
        {
            IEnumerable<SupplierBrand> _data = _context.SupplierBrands
                .Where(s => s.BrandID == BrandID)
                .Where(s => s.Enabled == true);
            return _data;
        }


        public SupplierBrand Query(int SupplierID, int BrandID)
        {
            SupplierBrand _data = _context.SupplierBrands
                .Where(s => s.BrandID == BrandID)
                .Where(s=>s.SupplierID== SupplierID)
                .Where(s => s.Enabled == true).FirstOrDefault() ;
            return _data;
        }
    }
}
