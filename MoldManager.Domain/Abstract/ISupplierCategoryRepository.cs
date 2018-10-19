using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;
namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ISupplierCategoryRepository
    {
        IQueryable<SupplierCategory> SupplierCategories { get; }
        int Save(SupplierCategory SupplierCategory);
        void Delete(int SupplierCategoryID);

        IEnumerable<SupplierCategory> QueryByCategory(int PartTypeID);
        IEnumerable<SupplierCategory> QueryBySupplier(int SupplierID);
    }
}
