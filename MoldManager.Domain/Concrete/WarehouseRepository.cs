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
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class WarehouseRepository:IWarehouseRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IQueryable<Warehouse> Warehouses
        {
            get 
            {
                return _context.Warehouses;
            }
        }

        public int Save(Warehouse Warehouse)
        {
            if (Warehouse.WarehouseID == 0)
            {
                _context.Warehouses.Add(Warehouse);
            }
            else
            {
                Warehouse _dbEntry = _context.Warehouses.Find(Warehouse.WarehouseID);
                if (_dbEntry != null)
                {
                    _dbEntry.Name = Warehouse.Name;
                    _dbEntry.Memo = Warehouse.Memo;
                    _dbEntry.Enabled = Warehouse.Enabled;
                }
            }
            _context.SaveChanges();
            return Warehouse.WarehouseID;
        }

        public void Delete(int WarehouseID)
        {
            Warehouse _dbEntry = _context.Warehouses.Find(WarehouseID);
            _dbEntry.Enabled = !_dbEntry.Enabled;
            _context.SaveChanges();
        }


        public Warehouse QueryByID(int WarehouseID)
        {
            return _context.Warehouses.Find(WarehouseID);
        }
    }
}
