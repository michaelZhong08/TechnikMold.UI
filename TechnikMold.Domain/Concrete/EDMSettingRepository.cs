using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;
namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class EDMSettingRepository:IEDMSettingRepository
    {

        private EFDbContext _context = new EFDbContext();
        public IQueryable<EDMSetting> EDMSettings
        {
            get { return _context.EDMSettings; }
        }

        public IEnumerable<EDMSetting> QueryByTaskID(int TaskID)
        {
            return _context.EDMSettings.Where(e => e.TaskID == TaskID);
        }

        public int Save(EDMSetting EDMSetting)
        {
            if (EDMSetting.EDMSettingID == 0)
            {
                _context.EDMSettings.Add(EDMSetting);
            }
            else
            {
                EDMSetting _dbEntry = _context.EDMSettings.Find(EDMSetting.EDMSettingID);
                if (_dbEntry != null)
                {
                    _dbEntry.TaskID = EDMSetting.TaskID;
                    _dbEntry.MoldNumber = EDMSetting.MoldNumber;
                    _dbEntry.EleName = EDMSetting.EleName;
                    _dbEntry.EleState = EDMSetting.EleState;
                    _dbEntry.CreateDate = EDMSetting.CreateDate;
                    _dbEntry.EDMOperator = EDMSetting.EDMOperator;
                    _dbEntry.Flag = EDMSetting.Flag;
                    _dbEntry.FinishDate = EDMSetting.FinishDate;
                    _dbEntry.Version = EDMSetting.Version;
                }
            }
            _context.SaveChanges();
            return EDMSetting.EDMSettingID;
        }
    }
}
