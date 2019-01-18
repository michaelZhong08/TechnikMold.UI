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
    public class CustomerRepository:ICustomerRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<Customer> Customers
        {
            get {
                return _context.Customers;
            }
        }

        public int Save(Customer Customer)
        {
            if (Customer.CustomerID == 0)
            {
                Customer.Address = Customer.Address ?? "";
                _context.Customers.Add(Customer);
            }
            else
            {
                Customer _dbEntry = _context.Customers.Find(Customer.CustomerID);
                if (_dbEntry != null)
                {
                    _dbEntry.Name = Customer.Name;
                    _dbEntry.Address = Customer.Address;
                    _dbEntry.Enabled = Customer.Enabled;
                }
            }
            _context.SaveChanges();
            return Customer.CustomerID;
        }

        public IQueryable<Customer> QueryByName(string Name)
        {
            throw new NotImplementedException();
        }

        public Customer QueryByID(int CustomerID)
        {
            return _context.Customers.Find(CustomerID);
        }


        public int Delete(int CustomerID)
        {
            Customer _dbEntry = _context.Customers.Find(CustomerID);
            if (_dbEntry != null)
            {
                _dbEntry.Enabled = !_dbEntry.Enabled;
            }
            _context.SaveChanges();
            return CustomerID;
        }
    }
}
