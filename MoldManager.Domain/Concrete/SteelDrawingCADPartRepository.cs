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
    public class SteelDrawingCADPartRepository:ISteelDrawingCADPartRepository
    {

        private EFDbContext _context = new EFDbContext();


        public IQueryable<SteelDrawingCADPart> SteelDrawingCADPart
        {
            get { return _context.SteelDrawingCADParts; }
        }


        public IEnumerable<SteelDrawingCADPart> QueryByDrawingID(int DrawingID)
        {
            IEnumerable<SteelDrawingCADPart> _cadPart = _context.SteelDrawingCADParts.Where(s => s.SteelDrawingID == DrawingID);
            return _cadPart;
        }


        public int Save(SteelDrawingCADPart SteelDrawingCADPart)
        {
            bool _isNew = false;
            SteelDrawingCADPart _dbEntry=null;
            if (SteelDrawingCADPart.SteelDrawingCADPartID == 0)
            {
                _isNew = true;
                _context.SteelDrawingCADParts.Add(SteelDrawingCADPart);
            }
            else
            {
                _dbEntry = _context.SteelDrawingCADParts.Find(SteelDrawingCADPart.SteelDrawingCADPartID);
                if (_dbEntry != null)
                {
                    _dbEntry.CADPartName = SteelDrawingCADPart.CADPartName;
                    _dbEntry.SteelDrawingID = SteelDrawingCADPart.SteelDrawingID;
                    _dbEntry.active = SteelDrawingCADPart.active;
                }
                else
                {
                    SteelDrawingCADPart.SteelDrawingCADPartID = 0;
                    _context.SteelDrawingCADParts.Add(SteelDrawingCADPart);
                }
            }
            _context.SaveChanges();
            return _isNew ? SteelDrawingCADPart.SteelDrawingCADPartID : _dbEntry.SteelDrawingCADPartID;
        }
    }
}
