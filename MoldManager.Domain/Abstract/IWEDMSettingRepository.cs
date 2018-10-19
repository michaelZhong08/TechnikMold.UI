using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Output;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IWEDMSettingRepository
    {
        IQueryable<WEDMSetting> WEDMSettings { get; }

        WEDMSetting QueryByTaskID(int TaskID);

        int Save(WEDMSetting entity);
        bool DeleteSettingByName(string partname, int rev);
        int ReleaseWEDMDrawing(int DrawIndex, string ReleaseBy, int Qty = 1);
        List<Task> GetWEDMTaskByMoldAndStatus(string MoldNo, int Status = -2, int PlanID = 0);
        WEDMCutSpeed GetWDMCutSpeed(double Thickness, int CutTypeID);
        List<WEDMPrecision> GetWEDMPrecision();
        string Get3DDrawingServerPath();
        List<WEDMTaskInfo> GetWEDMTaskInfoByMoldAndStatus(string MoldNo, int Status = -2, int PlanID = 0);
    }
}
