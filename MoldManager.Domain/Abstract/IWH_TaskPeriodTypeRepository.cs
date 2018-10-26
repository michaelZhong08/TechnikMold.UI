using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IWH_TaskPeriodTypeRepository
    {
        IQueryable<WH_TaskPeriodType> WH_TaskPeriodTypes { get; }
        int Save(WH_TaskPeriodType model);
    }
}
