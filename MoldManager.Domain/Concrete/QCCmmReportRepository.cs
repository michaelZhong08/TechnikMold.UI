using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class QCCmmReportRepository:IQCCmmReportRepository
    {

        private EFDbContext _context = new EFDbContext();


        public IQueryable<QCCmmReport> QCCmmReports
        {
            get { return _context.QCCmmReports; }
        }

        public int Save(QCCmmReport Report)
        {
            QCCmmReport _dbEntry = null;
            bool _isNew = false;
            if (Report.QCCmmReportID == 0)
            {
                _isNew = true;
                _context.QCCmmReports.Add(Report);
               
            }
            else
            {
                _dbEntry = QueryByReportID(Report.QCCmmReportID);
                if (_dbEntry != null)
                {
                    _dbEntry.QCTaskID = Report.QCTaskID;
                    _dbEntry.ReportName = Report.ReportName;
                    _dbEntry.CreateBy = Report.CreateBy;
                    _dbEntry.CreateComputer = Report.CreateComputer;
                    _dbEntry.CreateDate = Report.CreateDate;
                    _dbEntry.Enabled = Report.Enabled;
                    _dbEntry.ReportType = Report.ReportType;
                }
            }
            _context.SaveChanges();
            if (_isNew)
            {
                return Report.QCCmmReportID;
            }
            else
            {
                return _dbEntry.QCCmmReportID;
            }
        }

        public void Delete(int QCCmmReportID)
        {
            QCCmmReport _report = _context.QCCmmReports.Find(QCCmmReportID);
            _report.Enabled = false;
            _context.SaveChanges();
        }

        public IEnumerable<QCCmmReport> QueryByQCTaskID(int QCTaskID, int ReportType=0)
        {
            IEnumerable<QCCmmReport> _report = _context.QCCmmReports
                .Where(q => q.QCTaskID == QCTaskID)
                .Where(q => q.ReportType == ReportType)
                .Where(q => q.Enabled == true);
            return _report;
        }

        public QCCmmReport QueryByReportID(int ReportID)
        {
            QCCmmReport _report = _context.QCCmmReports.Find(ReportID);
            return _report;
        }

        
    }
}
