using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;


namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class EDMDetailRepository : IEDMDetailRepository
    {
        private EFDbContext _context = new EFDbContext();



        public IQueryable<EDMDetail> EDMDetails
        {
            get { return _context.EDMDetails; }
        }

        public int Save(EDMDetail EDMDetail)
        {
            EDMDetail _dbEntry = null;
            bool _isNew = false;

            if (EDMDetail.EDMDetailID == 0)
            {
                _dbEntry = QueryBySetting(EDMDetail.SettingName, EDMDetail.Version);
                if (_dbEntry == null)
                {
                    _isNew = true;
                    _context.EDMDetails.Add(EDMDetail);
                }
                else
                {
                    _dbEntry.EleDetail = EDMDetail.EleDetail;
                    _dbEntry.TaskID = EDMDetail.TaskID;
                    _dbEntry.CADDetail = EDMDetail.CADDetail;
                    _dbEntry.SettingName = EDMDetail.SettingName;
                    _dbEntry.Version = EDMDetail.Version;
                    _dbEntry.ModifyName = EDMDetail.ModifyName;
                    _dbEntry.ModifyCount = EDMDetail.ModifyCount;
                    _dbEntry.CADCount = EDMDetail.CADCount;
                    _dbEntry.CreateDate = EDMDetail.CreateDate;
                    _dbEntry.Designer = EDMDetail.Designer;
                    _dbEntry.Lock = EDMDetail.Lock;
                    _dbEntry.Expire = EDMDetail.Expire;
                    _dbEntry.MoldName = EDMDetail.MoldName;
                    _dbEntry.EleCount = EDMDetail.EleCount;
                    _dbEntry.QCPoint = EDMDetail.QCPoint;
                    _dbEntry.ProcessName = EDMDetail.ProcessName??"";
                }
            }
            else
            {
                _dbEntry = _context.EDMDetails.Find(EDMDetail.EDMDetailID);
                if (_dbEntry != null)
                {
                    _dbEntry.EleDetail = EDMDetail.EleDetail;
                    _dbEntry.TaskID = EDMDetail.TaskID;
                    _dbEntry.CADDetail = EDMDetail.CADDetail;
                    _dbEntry.SettingName = EDMDetail.SettingName;
                    _dbEntry.Version = EDMDetail.Version;
                    _dbEntry.ModifyName = EDMDetail.ModifyName;
                    _dbEntry.ModifyCount = EDMDetail.ModifyCount;
                    _dbEntry.CADCount = EDMDetail.CADCount;
                    _dbEntry.CreateDate = EDMDetail.CreateDate;
                    _dbEntry.Designer = EDMDetail.Designer;
                    _dbEntry.Lock = EDMDetail.Lock;
                    _dbEntry.Expire = EDMDetail.Expire;
                    _dbEntry.MoldName = EDMDetail.MoldName;
                    _dbEntry.EleCount = EDMDetail.EleCount;
                    _dbEntry.QCPoint = EDMDetail.QCPoint;
                    _dbEntry.ProcessName = EDMDetail.ProcessName ?? "";
                }
            }

            _context.SaveChanges();
            if (_isNew)
            {
                return EDMDetail.EDMDetailID;
            }
            else
            {
                return _dbEntry.EDMDetailID;
            }
        }

        public EDMDetail QueryByTaskID(int TaskID)
        {
            return _context.EDMDetails.Where(t => t.TaskID == TaskID).Where(t => t.Expire == false).FirstOrDefault();
        }

        public EDMDetail QueryBySetting(string SettingName, int Version)
        {
            return _context.EDMDetails.Where(t => t.SettingName == SettingName).Where(t => t.Version == Version).FirstOrDefault();
        }




        public int AddEDMDetail(EDMDetail EDMDetail)
        {
            _context.EDMDetails.Add(EDMDetail);
            _context.SaveChanges();
            return EDMDetail.EDMDetailID;
        }





        public void Lock(int TaskID, string TaskName, int Version)
        {
            List<EDMDetail> _details = _context.EDMDetails.Where(t => t.SettingName == TaskName).OrderByDescending(t => t.EDMDetailID).ToList();
            EDMDetail _curVersion = _details.First();
            _curVersion.Lock = 1;
            _details.Remove(_curVersion);
            foreach (EDMDetail _oldVersion in _details)
            {
                _oldVersion.Expire = true;
            }

            _context.SaveChanges();
        }
    }
}
