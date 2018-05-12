using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ISteelCAMDrawingRepository
    {
        IEnumerable<SteelCAMDrawing> SteelCAMDrawings { get; }

        SteelCAMDrawing QueryByNameVersion(string Name, int Version);

        IEnumerable<int> GetNCIDs(string Name, int Version);

        IEnumerable<SteelCAMDrawing> QueryOldVersion(string Name, int Version);

        IEnumerable<SteelCAMDrawing> QueryNewVersion(string Name, int Version);

        int Save(SteelCAMDrawing CAMDrawing);

        SteelCAMDrawing QueryByNCID(int NCID);

        int GetNextID();

        SteelCAMDrawing QueryByFullName(string FullName);
    }
}
