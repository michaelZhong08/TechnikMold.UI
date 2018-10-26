using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IWH_TaskPeriodRecordRepository
    {
        IQueryable<WH_TaskPeriodRecord> WH_TaskPeriodRecords { get; }
        int Save(WH_TaskPeriodRecord model);
    }
}
