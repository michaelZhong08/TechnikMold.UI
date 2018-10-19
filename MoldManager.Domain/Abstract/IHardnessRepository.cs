using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IHardnessRepository
    {
        IQueryable<Hardness> Hardnesses { get; }

        int Save(Hardness Hardness);

        IEnumerable<Hardness> QueryByMaterial(int MaterialID);

        void Delete(int HardnessID);
    }
}
