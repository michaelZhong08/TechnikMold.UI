using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class QCCmmFileSettingRepository:IQCCmmFileSettingRepository
    {

        private EFDbContext _context = new EFDbContext();

        public IQueryable<QCCmmFileSetting> QCCmmFileSettings
        {
            get { return _context.QCCmmFileSettings; }
        }

        public int Save(QCCmmFileSetting QCCmmFileSetting)
        {
            bool _isNew = false;
            QCCmmFileSetting _dbEntry = null;
            if (QCCmmFileSetting.QCCmmFileSettingID == 0)
            {
                _dbEntry = QueryByComputer(QCCmmFileSetting.ComputerName);
                if (_dbEntry == null)
                {
                    _isNew = true;
                    _context.QCCmmFileSettings.Add(QCCmmFileSetting);
                }
                else
                {
                    _dbEntry.FileAddress = QCCmmFileSetting.FileAddress;
                    _dbEntry.BackupDir = QCCmmFileSetting.BackupDir;
                    _dbEntry.TemplatePath = QCCmmFileSetting.TemplatePath;
                    _dbEntry.SteelTemplatePath = QCCmmFileSetting.SteelTemplatePath;
                    _dbEntry.COMIndex = QCCmmFileSetting.COMIndex;
                }
            }
            else
            {
                _dbEntry.FileAddress = QCCmmFileSetting.FileAddress;
                _dbEntry.BackupDir = QCCmmFileSetting.BackupDir;
                _dbEntry.TemplatePath = QCCmmFileSetting.TemplatePath;
                _dbEntry.SteelTemplatePath = QCCmmFileSetting.SteelTemplatePath;
                _dbEntry.COMIndex = QCCmmFileSetting.COMIndex;
            }
            _context.SaveChanges();
            if (_isNew)
            {
                return QCCmmFileSetting.QCCmmFileSettingID;
            }
            else
            {
                return _dbEntry.QCCmmFileSettingID;
            }
        }

        public QCCmmFileSetting QueryByComputer(string ComputerName)
        {
            return _context.QCCmmFileSettings.Where(q => q.ComputerName == ComputerName).FirstOrDefault();
        }
    }
}
