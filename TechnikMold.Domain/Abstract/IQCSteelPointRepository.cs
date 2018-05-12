using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IQCSteelPointRepository
    {
        IQueryable<QCSteelPoint> QCSteelPoints { get; }

        IEnumerable<QCSteelPoint> QueryByName(string Name, int Version);

        QCSteelPoint QueryStatus(string Name, int Version);

        IEnumerable<QCSteelPoint> QueryByFullPartName(string FullPartName);

        int Save(QCSteelPoint QCSteelPoint);

        QCSteelPoint QueryByNameVersion(string PartName, int Version);
    }
}
