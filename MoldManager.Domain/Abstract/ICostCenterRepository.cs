using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ICostCenterRepository
    {
        IQueryable<CostCenter> CostCenters { get; }

        int Save(CostCenter CostCenter);

        CostCenter QueryByID(int CostCenterID);

        IEnumerable<CostCenter> Query(string Keyword);

        void Delete(int CostCenterID);


    }
}
