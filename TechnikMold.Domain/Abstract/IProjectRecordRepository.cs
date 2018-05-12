using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IProjectRecordRepository
    {
        IQueryable<ProjectRecord> ProjectRecords { get; }


        int Save(int ProjectID, string RecordContent, string MoldNumber);

        IEnumerable<ProjectRecord> QueryByProjectID(int ProjectID);

        IEnumerable<ProjectRecord> QueryByMoldNumber(int ProjectID);
    }
}
