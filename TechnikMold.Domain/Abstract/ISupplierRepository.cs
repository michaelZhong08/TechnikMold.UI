
/*
 * Create By:lechun1
 * 
 * Description: public interface for repository of TechnikSys.MoldManager.Domain.Entity.PRQuotation 
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
    public interface ISupplierRepository
    {
        IQueryable<Supplier> Suppliers { get; }

        int Save(Supplier Supplier);

        int Delete(int SupplierID);

        Supplier QueryByID(int SupplierID);
    }
}
