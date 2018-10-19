
/*
 * Create By:lechun1
 * 
 * Description: public interface for repository of TechnikSys.MoldManager.Domain.Entity.Material 
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
    public interface IMaterialRepository
    {
        IQueryable<Material> Materials { get; }

        int Save(Material Material);

        IQueryable<Material> QueryByName(string Name);

        int Delete(int MaterialID);

        Material GetMaterial(int MaterialID);

        Material GetMaterialByName(string Name);
    }
}
