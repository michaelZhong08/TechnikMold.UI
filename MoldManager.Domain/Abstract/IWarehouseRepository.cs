using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IWarehouseRepository
    {
        IQueryable<Warehouse> Warehouses { get; }

        int Save(Warehouse Warehouse);

        void Delete(int WarehouseID);

        Warehouse QueryByID(int WarehouseID);
    }
}
