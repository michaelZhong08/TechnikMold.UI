
/*
 * Create By:lechun1
 * 
 * Description: public interface for repository of TechnikSys.MoldManager.Domain.Entity.Customer 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> Customers { get; }

        int Save(Customer Customer);

        IQueryable<Customer> QueryByName(string Name);

        Customer QueryByID(int CustomerID);

        int Delete(int CustomerID);
    }
}
