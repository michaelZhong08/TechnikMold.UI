using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IWH_WorkTypeRepository
    {
        IQueryable<WH_WorkType> WH_WorkTypes { get; }
        int Save(WH_WorkType model);
    }
}
