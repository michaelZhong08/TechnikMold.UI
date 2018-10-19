
/*
 * Create By:lechun1
 * 
 * Description: public interface for repository of TechnikSys.MoldManager.Domain.Entity.PartCode 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;


namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IPartCodeRepository
    {
        IQueryable<PartCode> PartCodes { get; }

        int Save(PartCode PartCode);

        IQueryable<PartCode> QueryByName(string Name);

    }
}
