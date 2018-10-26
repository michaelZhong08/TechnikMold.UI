using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnikMold.UI.Models;
using TechnikSys.MoldManager.Domain.Abstract;

namespace TechnikMold.UI.Controllers
{
    public class BaseController: Controller
    {
        #region 参数定义
        public ITaskRepository _taskRepository;
        public IPartRepository _partRepository;
        public IProjectRepository _projectRepository;
        public IUserRepository _userRepository;
        public ICNCItemRepository _cncItemRepository;
        public IQCInfoRepository _qcInfoRepository;
        public IWarehouseStockRepository _whStockRepository;
        public IMachineRepository _machineRepository;
        public ICNCMachInfoRepository _machInfoRepository;
        public ISteelGroupProgramRepository _steelGroupProgramRepository;
        public ISteelProgramRepository _steelProgramRepository;
        public IEDMDetailRepository _edmDetailRepository;
        public IEDMSettingRepository _edmSettingRepository;
        public IQCPointProgramRepository _qcPointProgramRepository;
        public ISystemConfigRepository _systemConfigRepository;
        public IQCSteelPointRepository _qcSteelPointRepository;
        public ISteelCAMDrawingRepository _steelCAMDrawingRepository;
        public ISteelDrawingCADPartRepository _steelDrawingCADPartRepository;
        public ICAMDrawingRepository _camDrawingRepository;
        public IProjectPhaseRepository _projectPhaseRepository;
        public IPOContentRepository _poContentRepository;
        public IQCTaskRepository _qcTaskRepository;
        public IQCCmmReportRepository _qcCmmReportRepository;
        public IQCCmmFileSettingRepository _qcCmmFileSettingRepository;
        public IPRContentRepository _prContentRepository;
        public IPurchaseRequestRepository _prRepository;
        public ICharmillRepository _charmillRepository;
        public IEDMTaskRecordRepository _edmRecordRepository;
        public IDepartmentRepository _departmentRepository;
        public IMGSettingRepository _mgSettingRepository;
        public IWEDMSettingRepository _wedmSettingRepository;
        public ITaskHourRepository _taskHourRepository;
        public IMachinesInfoRepository _machinesinfoRepository;
        #endregion


        #region 日志记录
        public void LogRecord(string filename, string content)
        {
            string logPath = Server.MapPath("~/Log/") + filename + "_" + DateTime.Now.ToString("yyMMddHHmmss") + ".txt";
            Toolkits.WriteLog(logPath, content);
        }
        #endregion
    }
}