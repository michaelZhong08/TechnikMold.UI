using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IEDMTaskRecordRepository
    {
        IEnumerable<EDMTaskRecord> EDMTaskRecords { get;}


        int Save(EDMTaskRecord EDMTaskRecord);

        IEnumerable<EDMTaskRecord> QueryByTaskID(int TaskID);

    }
}
