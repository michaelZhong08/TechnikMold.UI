using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class ProjectRecordRepository:IProjectRecordRepository
    {
        private EFDbContext _context = new EFDbContext();


        public IQueryable<ProjectRecord> ProjectRecords
        {
            get { return _context.ProjectRecords; }
        }

        public IEnumerable<ProjectRecord> QueryByProjectID(int ProjectID)
        {
            return _context.ProjectRecords.Where(p => p.ProjectID == ProjectID).OrderBy(p => p.RecordDate);
        }

        public IEnumerable<ProjectRecord> QueryByMoldNumber(int ProjectID)
        {
            throw new NotImplementedException();
        }


        public int Save(int ProjectID, string RecordContent, string MoldNumber)
        {
            ProjectRecord _dbEntry = new ProjectRecord();
            _dbEntry.ProjectID = ProjectID;
            _dbEntry.RecordDate = DateTime.Now;
            _dbEntry.RecordContent =ReplaceHtmlTag( RecordContent);
            _dbEntry.MoldNumber = MoldNumber;
            _context.ProjectRecords.Add(_dbEntry);
            _context.SaveChanges();
            return _dbEntry.ProjectRecordID;
        }

        /// <summary>
        /// Remove  html tags if record content contains any
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private string ReplaceHtmlTag(string html)
        {
            string strText = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
            strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");
            return strText;
        }
    }
}
