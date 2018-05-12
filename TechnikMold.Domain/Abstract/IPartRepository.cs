
/*
 * Create By:lechun1
 * 
 * Description: public interface for repository of TechnikSys.MoldManager.Domain.Entity.Part 
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
    public interface IPartRepository
    {
        IQueryable<Part> Parts { get; }

        int Save(Part Part);

        Part QueryByName(string Name);

        IQueryable<Part> Query(string Keyword);

        IQueryable<Part> QueryByProject(int ProjectID);

        Part QueryByID(int PartID);

        int Delete(int PartID);

        void DeleteExisting(string MoldNumber);

        IEnumerable<Part> QueryByMoldNumber(string MoldNumber);

        IEnumerable<Part> QueryBySpecification(string Keyword);
    }
}
