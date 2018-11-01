using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IPhaseTaskTypeRepository
    {
        IQueryable<PhaseTaskType> PhaseTaskTypes { get; }
        int Save(PhaseTaskType model);
    }
}
