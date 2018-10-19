/*
 * Create By:lechun1
 * 
 * Description:
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class ProcessRecordRepository:IProcessRecordRepository
    {
        private EFDbContext _context = new EFDbContext();


        public IQueryable<ProcessRecord> ProcessRecords
        {
            get 
            {
                return _context.ProcessRecords;
            }
        }

        public int Save(ProcessRecord ProcessRecord)
        {
            if (ProcessRecord.ProcessRecordID == 0)
            {
                ProcessRecord.RecordDate = DateTime.Now;
                _context.ProcessRecords.Add(ProcessRecord);
            }
            else
            {
                ProcessRecord _dbEntry = _context.ProcessRecords.Find(ProcessRecord.ProcessRecordID);
                if (_dbEntry != null)
                {
                    _dbEntry.ProcessType = ProcessRecord.ProcessType;
                    _dbEntry.ProcessID = ProcessRecord.ProcessID;
                    _dbEntry.UserID = ProcessRecord.UserID;
                    _dbEntry.Message = ProcessRecord.Message;
                    _dbEntry.RecordDate = ProcessRecord.RecordDate;
                }
            }
            _context.SaveChanges();
            return ProcessRecord.ProcessRecordID;
        }

        public IEnumerable<ProcessRecord> Query(int ProcessType, int ProcessID)
        {
            return _context.ProcessRecords.Where(p => p.ProcessType == ProcessType).Where(p => p.ProcessID == ProcessID).OrderBy(p => p.RecordDate);
        }
    }
}
