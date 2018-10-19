using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class PartListRepository:IPartListRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<PartList> PartLists
        {
            get { return _context.PartLists; }
        }

        public int Save(PartList PartList)
        {
            if (PartList.PartListID == 0)
            {
                //PartList _lastVersion = QueryByMoldNumber(PartList.MoldNumber, true).FirstOrDefault();
                //if (_lastVersion != null)
                //{
                //    _lastVersion.Latest = false;
                //    PartList.Version = _lastVersion.Version + 1;
                //    PartList.PrevVersion = _lastVersion.Version;
                //}
                //else
                //{
                //    PartList.Version = 1;
                //    PartList.PrevVersion = 0;
                //}

                PartList.Latest = true;
                //PartList.Released = false;
                _context.PartLists.Add(PartList);
            }
            else
            {
                PartList _dbEntry = _context.PartLists.Find(PartList.PartListID);
                if (_dbEntry != null)
                {
                    _dbEntry.MoldNumber = PartList.MoldNumber;
                    _dbEntry.Version = PartList.Version;
                    _dbEntry.Released = PartList.Released;
                    _dbEntry.Enabled = PartList.Enabled;
                    _dbEntry.PrevVersion = PartList.PrevVersion;
                    _dbEntry.Latest = PartList.Latest;
                    _dbEntry.ProjectID = PartList.ProjectID;
                    _dbEntry.CreateDate = PartList.CreateDate;
                    _dbEntry.ReleaseDate = PartList.ReleaseDate;
                }
            }
            _context.SaveChanges();
            return PartList.PartListID;
        }

        public PartList Query(int PartListID)
        {
            return _context.PartLists.Find(PartListID);
        }

        /// <summary>
        /// Query BOM versions of Molds
        /// </summary>
        /// <param name="MoldNumber">MoldNumber</param>
        /// <param name="Latest">
        /// true: get the latest version partlist of Moldnumber
        /// false:get specified version of part list
        /// </param>
        /// <param name="Version">
        /// Only valid when Latest is false!
        /// <=0:Get all version of partlist from current moldnumber
        /// >0:Get the version of partlist from current moldnumber
        /// </param>
        /// <returns></returns>
        public IEnumerable<PartList> QueryByMoldNumber(string MoldNumber, bool Latest = false, int Version = -1)
        {
            if (Latest)
            {
                return _context.PartLists.Where(p => p.Enabled == true).Where(p => p.MoldNumber == MoldNumber).Where(p=>p.Latest==true);
            }
            else
            {
                if (Version > 0)
                {
                    return _context.PartLists.Where(p => p.Enabled == true).Where(p => p.MoldNumber == MoldNumber).Where(p => p.Version == Version);
                }
                else
                {
                    return _context.PartLists.Where(p => p.Enabled == true).Where(p => p.MoldNumber == MoldNumber).OrderByDescending(p => p.Version);
                }
            }
        }



    }
}
