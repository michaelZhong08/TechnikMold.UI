using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IProcessRecordRepository
    {
        IQueryable<ProcessRecord> ProcessRecords { get; }

        int Save(ProcessRecord ProcessRecord);

        IEnumerable<ProcessRecord> Query(int ProcessType, int ProcessID);
    }
}
