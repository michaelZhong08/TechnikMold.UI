using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IPRProcessRepository
    {
        IQueryable<PRProcess> PrProcesses { get; }

        int Save(PRProcess PRProcess);

        IEnumerable<PRProcess> QueryByPRID(int PurchaseRequestID);
    }
}
