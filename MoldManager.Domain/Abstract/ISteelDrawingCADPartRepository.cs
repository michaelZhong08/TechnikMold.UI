using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ISteelDrawingCADPartRepository
    {
        IQueryable<SteelDrawingCADPart> SteelDrawingCADPart { get; }

        IEnumerable<SteelDrawingCADPart> QueryByDrawingID(int DrawingID);

        int Save(SteelDrawingCADPart SteelDrawingCADPart);
    }
}
