using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class CAMDrawingRepository:ICAMDrawingRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<CAMDrawing> CAMDrawings
        {
            get { return _context.CAMDrawings; }
        }

        public int Save(CAMDrawing CAMDrawing)
        {
            CAMDrawing _dbEntry = null;
            bool _isNew=false;
            if (CAMDrawing.CAMDrawingID == 0)
            {
                _dbEntry = QueryByName(CAMDrawing.DrawingName);
                if (_dbEntry == null)
                {
                    _isNew=true;
                    CAMDrawing.ReleaseBy = "";
                    CAMDrawing.ReleaseDate = new DateTime(1900, 1, 1);
                    _context.CAMDrawings.Add(CAMDrawing);
                }
                else
                {
                    _dbEntry.DrawingName = CAMDrawing.DrawingName;
                    _dbEntry.MoldName = CAMDrawing.MoldName;
                    _dbEntry.Lock = CAMDrawing.Lock;
                    _dbEntry.CreateDate = CAMDrawing.CreateDate;
                    _dbEntry.CreateBy = CAMDrawing.CreateBy;
                    _dbEntry.ReleaseDate = CAMDrawing.ReleaseDate;
                    _dbEntry.ReleaseBy = CAMDrawing.ReleaseBy;
                    _dbEntry.active = CAMDrawing.active;

                }
            }else
            {
                _dbEntry = _context.CAMDrawings.Find(CAMDrawing.CAMDrawingID);
                _dbEntry.DrawingName = CAMDrawing.DrawingName;
                _dbEntry.MoldName = CAMDrawing.MoldName;
                _dbEntry.Lock = CAMDrawing.Lock;
                _dbEntry.CreateDate = CAMDrawing.CreateDate;
                _dbEntry.CreateBy = CAMDrawing.CreateBy;
                _dbEntry.ReleaseDate = CAMDrawing.ReleaseDate;
                _dbEntry.ReleaseBy = CAMDrawing.ReleaseBy;
                _dbEntry.active = CAMDrawing.active;
            }
            _context.SaveChanges();
            if (_isNew){
                return CAMDrawing.CAMDrawingID;
            }
            else
            {
                return _dbEntry.CAMDrawingID;
            }
            
        }

        public CAMDrawing QueryByName(string DrawingName)
        {
            //CAMDrawing _camDrawing = _context.CAMDrawings.Where(d => d.DrawingName == DrawingName).FirstOrDefault();

            CAMDrawing _camDrawing = (from c in _context.CAMDrawings
                                                  where c.DrawingName == DrawingName
                                                   select c).FirstOrDefault() ;


            return _camDrawing;
        }


        
    }
}
