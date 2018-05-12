using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IQCInfoRepository
    {
        IQueryable<QCInfo> QCInfoes { get; }

        int Save(QCInfo QCInfo);

        QCInfo QueryByID(int QCInfoID);

        QCInfo QueryByTaskID(int TaskID);
    }
}
