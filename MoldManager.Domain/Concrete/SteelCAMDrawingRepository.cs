using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class SteelCAMDrawingRepository:ISteelCAMDrawingRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IEnumerable<SteelCAMDrawing> SteelCAMDrawings
        {
            get { return _context.SteelCAMDrawngs; }
        }

        public IEnumerable<SteelCAMDrawing> QueryOlderVersion(string Name, int Version)
        {
            return _context.SteelCAMDrawngs.Where(s => s.DrawName == Name).Where(s => s.DrawREV<Version);
        }


        public SteelCAMDrawing QueryByNameVersion(string Name, int Version)
        {
            return _context.SteelCAMDrawngs
                .Where(s => s.DrawName == Name)
                .Where(s => s.DrawREV == Version)
                .Where(s=>s.active==true)
                .FirstOrDefault();
        }

        public SteelCAMDrawing QueryByFullName(string FullName)
        {
            return _context.SteelCAMDrawngs.Where(s => s.FullPartName == FullName).FirstOrDefault();
        }



        public IEnumerable<SteelCAMDrawing> QueryOldVersion(string Name, int Version)
        {
            return _context.SteelCAMDrawngs
                .Where(s => s.DrawName == Name)
                .Where(s => s.DrawREV < Version)
                .Where(s=>s.active==true)
                .Where(s=>s.DrawLock==true);
        }


        public IEnumerable<SteelCAMDrawing> QueryNewVersion(string Name, int Version)
        {
            return _context.SteelCAMDrawngs
                .Where(s => s.DrawName == Name)
                .Where(s => s.DrawREV > Version)
                .Where(s=>s.active==true)
                .Where(s=>s.DrawLock==false);
        }


        public int Save(SteelCAMDrawing CAMDrawing)
        {
            bool _isnew = false;
            SteelCAMDrawing _dbEntry=null;
            if (CAMDrawing.SteelCAMDrawingID == 0)
            {
                _isnew = true;
                _context.SteelCAMDrawngs.Add(CAMDrawing);
            }
            else
            {
                _dbEntry = _context.SteelCAMDrawngs.Find(CAMDrawing.SteelCAMDrawingID);
                if (_dbEntry != null)
                {
                    
                    _dbEntry.FullPartName = CAMDrawing.FullPartName;
                    _dbEntry.DrawName = CAMDrawing.DrawName;
                    _dbEntry.DrawREV = CAMDrawing.DrawREV;
                    _dbEntry.CADPartName = CAMDrawing.CADPartName;
                    _dbEntry.MoldName = CAMDrawing.MoldName;
                    _dbEntry.CreateDate = CAMDrawing.CreateDate;
                    _dbEntry.DrawLock = CAMDrawing.DrawLock;
                    _dbEntry.LastestFlag = CAMDrawing.LastestFlag;
                    _dbEntry.NCID = CAMDrawing.NCID;
                    _dbEntry.Programmer = CAMDrawing.Programmer;
                    _dbEntry.IssuePerson = CAMDrawing.IssuePerson;
                    _dbEntry.IssueDate = CAMDrawing.IssueDate;
                    _dbEntry.Undo_person = CAMDrawing.Undo_person;
                    _dbEntry.Undo_date = CAMDrawing.Undo_date;
                    _dbEntry.Delete_time = CAMDrawing.Delete_time;
                    _dbEntry.Delete_person = CAMDrawing.Delete_person;
                    _dbEntry.active = CAMDrawing.active;
                    _dbEntry.QCPoint = CAMDrawing.QCPoint;
                }
            }
            _context.SaveChanges();
            if (_isnew)
            {
                return CAMDrawing.SteelCAMDrawingID;
            }
            else
            {
                try
                {
                    return _dbEntry.SteelCAMDrawingID;
                }
                catch
                {
                    return -1;
                }
                
            }
        }

        /// <summary>
        /// Table primary key increment
        /// </summary>
        /// <returns></returns>
        public int GetNextID()
        {
            int _next = _context.SteelCAMDrawngs.Select(s => s.SteelCAMDrawingID).Max()+1;
            return _next;
        }


        public IEnumerable<int> GetNCIDs(string Name, int Version)
        {
            IEnumerable<int> _result = _context.SteelCAMDrawngs.Where(s => s.DrawName == Name).Where(s => s.DrawREV < Version).Select(s => s.NCID);
            return _result;
        }


        public SteelCAMDrawing QueryByNCID(int NCID)
        {
            SteelCAMDrawing _steelCAMDrawing = _context.SteelCAMDrawngs.Where(s => s.NCID == NCID).FirstOrDefault();
            return _steelCAMDrawing;
        }
    }
}
