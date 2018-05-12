using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class WarehousePositionRepository:IWarehousePositionRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<WarehousePosition> WarehousePositions
        {
            get { return _context.WarehousePositions; }
        }

        public int Save(WarehousePosition Position)
        {
            if (Position.WarehousePositionID == 0)
            {
                _context.WarehousePositions.Add(Position);
            }
            else
            {
                WarehousePosition _dbEntry = _context.WarehousePositions.Find( Position.WarehousePositionID);

                if (_dbEntry != null)
                {
                    _dbEntry.WarehouseID = Position.WarehouseID;
                    _dbEntry.Name = Position.Name;
                    _dbEntry.Enabled = Position.Enabled;
                }

            }
            _context.SaveChanges();
            return Position.WarehousePositionID;
        }

        public void Delete(int WarehousePositionID)
        {
            WarehousePosition _dbEntry = _context.WarehousePositions.Find(WarehousePositionID);
            if (_dbEntry != null)
            {
                _dbEntry.Enabled = false;
            }
            _context.SaveChanges();
        }


        public IEnumerable<WarehousePosition> QueryByWarehouse(int WarehouseID)
        {
            return _context.WarehousePositions.Where(w => w.WarehouseID == WarehouseID).Where(w=>w.Enabled==true);
        }


        public WarehousePosition QueryByID(int WarehousePositionID)
        {
            return _context.WarehousePositions.Find(WarehousePositionID);
        }
    }
}
