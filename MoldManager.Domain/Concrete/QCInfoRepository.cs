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
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class QCInfoRepository:IQCInfoRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<QCInfo> QCInfoes
        {
            get { return _context.QCInfoes; }
        }

        public int Save(QCInfo QCInfo)
        {
            if (QCInfo.QCInfoID == 0)
            {
                _context.QCInfoes.Add(QCInfo);
            }
            else
            {
                QCInfo _dbEntry = _context.QCInfoes.Find(QCInfo.QCInfoID);
                if (_dbEntry != null)
                {
                    _dbEntry.TaskID = QCInfo.TaskID;
                    _dbEntry.ItemID = QCInfo.ItemID;
                    _dbEntry.QCPoints = QCInfo.QCPoints;
                }
            }
            _context.SaveChanges();
            return QCInfo.QCInfoID;
        }

        public QCInfo QueryByID(int QCInfoID)
        {
            return _context.QCInfoes.Find(QCInfoID);
        }

        public QCInfo QueryByTaskID(int TaskID)
        {
            return _context.QCInfoes.Where(q => q.TaskID == TaskID).FirstOrDefault();
        }
    }
}
