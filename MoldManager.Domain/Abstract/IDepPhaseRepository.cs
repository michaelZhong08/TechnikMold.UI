using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IDepPhaseRepository
    {
        IQueryable<Base_DepPhase> DepPhases { get; }

        int Save(Base_DepPhase DepPhase);

        //IQueryable<Base_DepPhase> QueryByName(string Name);


        IQueryable<Base_DepPhase> QueryByDepID(int DepId);

        int Delete(int Id);
    }
}
