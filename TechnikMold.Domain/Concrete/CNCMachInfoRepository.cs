using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class CNCMachInfoRepository:ICNCMachInfoRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IQueryable<CNCMachInfo> CNCMachInfoes
        {
            get { return _context.CNCMachInfoes; }
        }

        public int Save(CNCMachInfo CNCMachInfo)
        {
            CNCMachInfo _dbEntry = null;
            bool _isNew = false;
            if (CNCMachInfo.MachInfoID == 0)
            {
                _dbEntry = _context.CNCMachInfoes
                    .Where(m => m.Model == CNCMachInfo.Model)
                    .Where(m => m.Version == CNCMachInfo.Version)
                    .FirstOrDefault() ;
                if (_dbEntry == null)
                {
                    _isNew = true;
                    CNCMachInfo.CreateDate = DateTime.Now;
                    CNCMachInfo.IssueDate = new DateTime(1900, 1, 1);
                    CNCMachInfo.PosCheck = false;
                    _context.CNCMachInfoes.Add(CNCMachInfo);
                }
                else
                {
                    _dbEntry.Model = CNCMachInfo.Model;
                    _dbEntry.Version = CNCMachInfo.Version;
                    _dbEntry.Position = CNCMachInfo.Position;
                    _dbEntry.RoughName = CNCMachInfo.RoughName;
                    _dbEntry.FinishName = CNCMachInfo.FinishName;
                    _dbEntry.RoughGap = CNCMachInfo.RoughGap;
                    _dbEntry.FinishGap = CNCMachInfo.FinishGap;
                    _dbEntry.RoughTime = CNCMachInfo.RoughTime;
                    _dbEntry.FinishTime = CNCMachInfo.FinishTime;
                    _dbEntry.RoughCount = CNCMachInfo.RoughCount;
                    _dbEntry.FinishCount = CNCMachInfo.FinishCount;
                    _dbEntry.DrawIndex = CNCMachInfo.DrawIndex;
                    _dbEntry.Surface = CNCMachInfo.Surface;
                    _dbEntry.ObitType = CNCMachInfo.ObitType;
                    _dbEntry.MachType = CNCMachInfo.MachType;
                    _dbEntry.EDMStock = CNCMachInfo.EDMStock;
                    _dbEntry.CNCMethod = CNCMachInfo.CNCMethod;
                    _dbEntry.EDMMethod = CNCMachInfo.EDMMethod;
                    _dbEntry.AppendMethod = CNCMachInfo.AppendMethod;
                    _dbEntry.SafetyHeight = CNCMachInfo.SafetyHeight;
                    _dbEntry.QCPoint = CNCMachInfo.QCPoint;
                    _dbEntry.PosCheck = CNCMachInfo.PosCheck;
                }
                
            }
            else
            {
                _dbEntry = _context.CNCMachInfoes.Find(CNCMachInfo.MachInfoID);
                if (_dbEntry != null)
                {
                    _dbEntry.Model = CNCMachInfo.Model;
                    _dbEntry.Version = CNCMachInfo.Version;
                    _dbEntry.Position = CNCMachInfo.Position;
                    _dbEntry.RoughName = CNCMachInfo.RoughName;
                    _dbEntry.FinishName = CNCMachInfo.FinishName;
                    _dbEntry.RoughGap = CNCMachInfo.RoughGap;
                    _dbEntry.FinishGap = CNCMachInfo.FinishGap;
                    _dbEntry.RoughTime = CNCMachInfo.RoughTime;
                    _dbEntry.FinishTime = CNCMachInfo.FinishTime;
                    _dbEntry.RoughCount = CNCMachInfo.RoughCount;
                    _dbEntry.FinishCount = CNCMachInfo.FinishCount;
                    _dbEntry.DrawIndex = CNCMachInfo.DrawIndex;
                    _dbEntry.Surface = CNCMachInfo.Surface;
                    _dbEntry.ObitType = CNCMachInfo.ObitType;
                    _dbEntry.MachType = CNCMachInfo.MachType;
                    _dbEntry.EDMStock = CNCMachInfo.EDMStock;
                    _dbEntry.CNCMethod = CNCMachInfo.CNCMethod;
                    _dbEntry.EDMMethod = CNCMachInfo.EDMMethod;
                    _dbEntry.AppendMethod = CNCMachInfo.AppendMethod;
                    _dbEntry.SafetyHeight = CNCMachInfo.SafetyHeight;
                    _dbEntry.QCPoint = CNCMachInfo.QCPoint;
                    _dbEntry.PosCheck = CNCMachInfo.PosCheck;
                }
            }
            _context.SaveChanges();
            if (_isNew)
            {
                return CNCMachInfo.MachInfoID;
            }
            else
            {
                return _dbEntry.MachInfoID;
            }
            
        }

        public CNCMachInfo QueryByELEIndex(int ELEIndex)
        {
            return _context.CNCMachInfoes.Where(c => c.DrawIndex == ELEIndex).FirstOrDefault() ;
        }

        public CNCMachInfo QueryByNameVersion(string EleName, int Version)
        {
            return _context.CNCMachInfoes.Where(c => c.Model == EleName).Where(c => c.Version == Version).FirstOrDefault();
        }


        public int GetNextDrawIndex()
        {
            return _context.CNCMachInfoes.Select(m => m.DrawIndex).Max()+1;
        }
    }
}
