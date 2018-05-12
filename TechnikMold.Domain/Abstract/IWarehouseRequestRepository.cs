using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IWarehouseRequestRepository
    {
        IQueryable<WarehouseRequest> WarehouseRequests { get; }

        int Save(WarehouseRequest WarehouseRequest);

        void Delete(int WarehouseRequestID);

        void ChangeStatus(int WarehouseRequestID, int State);

        //IEnumerable<WarehouseRequest> QueryByMoldNumber(string MoldNumber);

        WarehouseRequest QueryByID(int WarehouseReqeustID);


    }
}
