
/*
 * Create By:lechun1
 * 
 * Description: public interface for repository of TechnikSys.MoldManager.Domain.Entity.Brand 
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
    public  interface IBrandRepository
    {
        IQueryable<Brand> Brands { get; }

        int Save(Brand Brand);

        IQueryable<Brand> QueryByName(string Name);


        Brand QueryByID(int BrandID);

        int Delete(int BrandID);
    }
}
