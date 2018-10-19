
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
using TechnikSys.MoldManager.Domain.Output;

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


        int SaveNew(Part Part);
        int SaveUpgrade(Part Part);

        /// <summary>
        /// 根据名称（包含版本号）获取Part对象
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        IQueryable<Part> GetPartsByName(string Name);
        /// <summary>
        /// 根据名称（包含版本号）获取Part对象
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        IQueryable<Part> GetPartsByName(string Name, int projectid);

        /// <summary>
        /// 判断是否存在发布的bom版本
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        IQueryable<Project> GetProjectsByNameIsPublish(string Name);
        /// <summary>
        /// 获取某project的所有版本清单
        /// </summary>
        /// <param name="MoldNumber"></param>
        /// <returns></returns>
        List<MoldVersionInfo> GetMoldVerList(string MoldNumber);
    }
}
