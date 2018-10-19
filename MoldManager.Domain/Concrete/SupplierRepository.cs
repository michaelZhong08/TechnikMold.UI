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
    public class SupplierRepository:ISupplierRepository
    {
        private EFDbContext _context = new EFDbContext();


        public IQueryable<Supplier> Suppliers
        {
            get
            {
                return _context.Suppliers;
            }
        }

        public int Save(Supplier Supplier)
        {
            if (Supplier.SupplierID == 0)
            {
                Supplier.Enabled = true;
                _context.Suppliers.Add(Supplier);
            }
            else
            {
                Supplier _dbEntry = _context.Suppliers.Find(Supplier.SupplierID);
                if (_dbEntry != null)
                {
                    _dbEntry.Code = Supplier.Code;
                    _dbEntry.Name = Supplier.Name;
                    _dbEntry.FullName = Supplier.FullName;
                    _dbEntry.Contact = Supplier.Contact;
                    _dbEntry.Email = Supplier.Email;
                    _dbEntry.Type = Supplier.Type;
                    _dbEntry.Enabled = true;
                    _dbEntry.FirstSupply = Supplier.FirstSupply;
                    _dbEntry.Address = Supplier.Address;
                    _dbEntry.Settlement = Supplier.Settlement;
                    _dbEntry.TaxNo = Supplier.TaxNo;
                    _dbEntry.Bank = Supplier.Bank;
                    _dbEntry.Account = Supplier.Account;
                    _dbEntry.TaxRate = Supplier.TaxRate;
                    _dbEntry.JianSuo = Supplier.JianSuo;
                }
            }
            _context.SaveChanges();
            return Supplier.SupplierID;
        }

        public int Delete(int SupplierID)
        {
            Supplier _dbEntry = _context.Suppliers.Find(SupplierID);
            _dbEntry.Enabled = !_dbEntry.Enabled;
            _context.SaveChanges();
            return SupplierID;
        }

        public Supplier QueryByID(int SupplierID)
        {
            return _context.Suppliers.Find(SupplierID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="js">检索</param>
        /// <returns></returns>
        public List<Supplier> GetSuppliersByJS(string js)
        {
            List<Supplier> _suppliers;
            if (!string.IsNullOrEmpty(js))
            {
                _suppliers = _context.Suppliers.Where(s => s.JianSuo.Contains(js) || s.Name.Contains(js)).Where(s => s.Enabled == true).ToList();// || s.Name.Contains(js)
            }
            else
            {
                _suppliers = _context.Suppliers.OrderBy(s=>s.SupplierID).ToList();
            }
            return _suppliers;
        }
    }
}
