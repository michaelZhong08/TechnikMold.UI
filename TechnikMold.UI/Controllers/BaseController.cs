using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnikMold.UI.Models;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikMold.UI.Controllers
{
    public class BaseController: Controller
    {
        public static string constr = ConfigurationManager.ConnectionStrings["EFDbContext"].ToString();
        //public DataAccess db = new DataAccess(constr, CommandType.StoredProcedure);

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
        public IAttachFileInfoRepository _attachFileInfoRepository;
        public IPurchaseItemRepository _purchaseItemRepository;
        public ITaskTypeRepository _taskTyprRepository;
        public IWH_WorkTypeRepository _workTyprRepository;
        #endregion


        #region 日志记录
        public void LogRecord(string filename, string content)
        {
            string logPath = Server.MapPath("~/Log/") + filename + "_" + DateTime.Now.ToString("yyMMddHHmmss") + ".txt";
            Toolkits.WriteLog(logPath, content);
        }
        #endregion
        public string GetCurrentUser()
        {
            try
            {
                //int _userID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
                //User _user = _userRepository.GetUserByID(_userID) ?? new User();
                //return _user.FullName;
                string _name = HttpUtility.UrlDecode(Request.Cookies["User"]["FullName"])??"";
                return _name;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp(System.DateTime time, int length = 13)
        {
            long ts = ConvertDateTimeToInt(time);
            return ts.ToString().Substring(0, length);
        }
        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }
    }
}