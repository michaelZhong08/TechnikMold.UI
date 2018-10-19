using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class PositionRepository:IPositionRepository
    {
        private EFDbContext _context = new EFDbContext();


        public int Save(Position Position)
        {
            if (Position.PositionID == 0)
            {
                _context.Positions.Add(Position);
            }
            else
            {
                Position _dbEntry = _context.Positions.Find(Position.PositionID);
                if (_dbEntry != null)
                {
                    _dbEntry.Name = Position.Name;
                    _dbEntry.Enabled = Position.Enabled;
                }
            }
            _context.SaveChanges();
            return Position.PositionID;
        }

        public void Delete(int PositionID)
        {
            Position _dbEntry = _context.Positions.Find(PositionID);
            if (_dbEntry != null)
            {
                _dbEntry.Enabled = false;
            }
            _context.SaveChanges();
        }

        public Position QueryByID(int PositionID)
        {
            Position _dbEntry = _context.Positions.Find(PositionID);
            return _dbEntry;
        }

        public IEnumerable<Position> QueryByName(string Keyword)
        {
            IEnumerable<Position> _positions = _context.Positions.Where(p => p.Name.Contains(Keyword));
            return _positions;
        }

        public IQueryable<Position> Positions
        {
            get { return _context.Positions; }
        }
    }
}
