using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IPartImportRecordRepository
    {
        IQueryable<PartImportRecord> PartImportRecords { get; }

        int Save(String  Data, int PartID);
    }
}
