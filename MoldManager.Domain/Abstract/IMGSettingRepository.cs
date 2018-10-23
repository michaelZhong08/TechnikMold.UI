using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IMGSettingRepository
    {
        IQueryable<MGSetting> MGSettings { get; }

        MGSetting QueryByTaskID(int TaskID);

        int Save(MGSetting entity, bool ForUG = true);
        List<MGTypeName> GetMGTypeName();
        bool DeleteSettingByName(string partname, int rev);
        int ReleaseMGDrawing(int DrawIndex, string ReleaseBy,string TaskName);
        List<MGSetting> GetMGPartListByMold(string MoldNo, bool bRelease);
        string GetDrawFileByDrawName(string DrawName, bool IsContain2D, string DrawType = "CAM");
        bool IsLatestDrawFile(string DrawName, bool IsContain2D, string DrawType = "CAM");
    }
}
