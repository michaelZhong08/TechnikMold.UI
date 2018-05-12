using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IWarehousePositionRepository
    {
        IQueryable<WarehousePosition> WarehousePositions { get; }

        int Save(WarehousePosition Position);

        void Delete(int WarehousePositonID);

        IEnumerable<WarehousePosition> QueryByWarehouse(int WarehouseID);

        WarehousePosition QueryByID(int WarehousePositionID);
    }
}
