
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
using TechnikSys.MoldManager.Domain.Output;

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

        #region added by felix 版本管控

        /// <summary>
        /// 获取某project的所有版本清单
        /// </summary>
        /// <param name="MoldNumber"></param>
        /// <returns></returns>
        List<MoldVersionInfo> GetProjectVerList(string MoldNumber);

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="MoldNumber"></param>
        /// <param name="ver"></param>
        /// <returns></returns>
        Project GetProjectByMoldNumberVer(string MoldNumber, int ver = -1);

        #endregion
        IQueryable<Project> GetProjectsByDep(int Department);
    }
}
