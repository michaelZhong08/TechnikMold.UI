using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface ICAMDrawingRepository
    {
        IQueryable<CAMDrawing> CAMDrawings { get; }

        int Save(CAMDrawing CAMDrawing);

        CAMDrawing QueryByName(string DrawingName);
    }
}
