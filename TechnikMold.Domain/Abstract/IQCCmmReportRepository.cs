using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IQCCmmReportRepository
    {
        IQueryable<QCCmmReport> QCCmmReports { get; }

        int Save(QCCmmReport Report);

        void Delete(int QCCmmReportID);

        IEnumerable<QCCmmReport> QueryByQCTaskID(int QCTaskID, int ReportType);

        
    }
}
