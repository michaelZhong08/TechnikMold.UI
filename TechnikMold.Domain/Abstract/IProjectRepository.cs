
/*
 * Create By:lechun1
 * 
 * Description: public interface for repository of TechnikSys.MoldManager.Domain.Entity.Project 
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
    public  interface IProjectRepository
    {
        IQueryable<Project> Projects { get; }

        int Save(Project Project);

        IQueryable<Project> Query(string Keyword);

        Project QueryByMoldNumber(string MoldNumber, int Type=-1);

        Project QueryByProjectNumber(string ProjectNumber);

        IEnumerable<Project> QueryByMainProject(int ProjectID);

        void AddMemo(int ProjectID, String Memo);

        int PauseProject(int ProjectID, string Memo, bool PauseSubs);

        int DeleteProject(int ProjectID, string Memo);

        Project GetByID(int ProjectID);

        void AddAttachment(int ProjectID, string Attachment);

        Project QueryActiveByMoldNumber(string MoldNumber, int Type = 1, int Status = 0);

        Project GetLatestActiveProject(string MoldNumber);
    }
}
