using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ISupplierBrandRepository
    {
        IQueryable<SupplierBrand> SupplierBrands { get; }

        int Save(SupplierBrand SuppleirBrand);

        void Delete(int SupplierID, int BrandID);

        void Delete(int SupplierBrandID);

        IEnumerable<SupplierBrand> QueryBySupplier(int SupplierID);

        IEnumerable<SupplierBrand> QueryByBrand(int BrandID);

        SupplierBrand Query(int SupplierID, int BrandID);
    }
}
