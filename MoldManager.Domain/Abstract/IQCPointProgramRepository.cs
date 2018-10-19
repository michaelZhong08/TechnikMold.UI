using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IQCPointProgramRepository
    {
        IQueryable<QCPointProgram> QCPointPrograms { get; }

        QCPointProgram QueryByPart3D(string ELE3DName, int Version);

        int Save(QCPointProgram QCPointProgram);
    }
}
