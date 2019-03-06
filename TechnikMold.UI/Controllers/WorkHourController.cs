using Common;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnikMold.UI.Models.GridViewModel;
using TechnikMold.UI.Models.ViewModel;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.ViewModel;

namespace TechnikMold.UI.Controllers
{
    public class WorkHourController : BaseController
    {
        #region 构造
        public WorkHourController(ITaskRepository TaskRepository,
            IPartRepository PartRepository,
            IProjectRepository ProjectRepository,
            IUserRepository UserRepository,
            ICNCItemRepository CNCItemRepository,
            IQCInfoRepository QCInfoRepository,
            IWarehouseStockRepository WarehouseStockRepository,
            IMachineRepository MachineRepository,
            ICNCMachInfoRepository CNCMachInfoRepository,
            ISteelGroupProgramRepository SteelGroupProgramRepository,
            ISteelProgramRepository SteelProgramRepository,
            IEDMSettingRepository EDMSettingRepository,
            IEDMDetailRepository EDMDetailRepository,
            IQCPointProgramRepository QCPointProgramRepository,
            ISystemConfigRepository SystemConfigRepository,
            IQCSteelPointRepository QCSteelPointRepository,
            ISteelCAMDrawingRepository SteelCAMDrawingRepository,
            ISteelDrawingCADPartRepository SteelDrawingCADPartRepository,
            ICAMDrawingRepository CAMDrawingRepository,
            IProjectPhaseRepository ProjectPhaseRepository,
            IPOContentRepository POContentRepository,
            IQCTaskRepository QCTaskRepository,
            IQCCmmReportRepository QCCmmReportRepository,
            IQCCmmFileSettingRepository QCCmmFileSettingRepository,
            IPRContentRepository PRContentRepository,
            IPurchaseRequestRepository PurchanseRequestRepository,
            ICharmillRepository CharmillRepository,
            IEDMTaskRecordRepository EDMTaskRepository,
            IDepartmentRepository DepartmentRepository,
            IMGSettingRepository MGSettingRepository,
            IWEDMSettingRepository WEDMSettingRepository,
            ITaskHourRepository TaskHourRepository,
            IMachinesInfoRepository MachinesInfoRepository,
            ITaskTypeRepository TaskTypeRepositroy,
            IWH_WorkTypeRepository WorkTypeRepository)
        {
            _taskRepository = TaskRepository;
            _partRepository = PartRepository;
            _projectRepository = ProjectRepository;
            _userRepository = UserRepository;
            _cncItemRepository = CNCItemRepository;
            _qcInfoRepository = QCInfoRepository;
            _whStockRepository = WarehouseStockRepository;
            _machineRepository = MachineRepository;
            _machInfoRepository = CNCMachInfoRepository;
            _steelGroupProgramRepository = SteelGroupProgramRepository;
            _steelProgramRepository = SteelProgramRepository;
            _edmSettingRepository = EDMSettingRepository;
            _edmDetailRepository = EDMDetailRepository;
            _qcPointProgramRepository = QCPointProgramRepository;
            _systemConfigRepository = SystemConfigRepository;
            _qcSteelPointRepository = QCSteelPointRepository;
            _steelCAMDrawingRepository = SteelCAMDrawingRepository;
            _steelDrawingCADPartRepository = SteelDrawingCADPartRepository;
            _camDrawingRepository = CAMDrawingRepository;
            _projectPhaseRepository = ProjectPhaseRepository;
            _poContentRepository = POContentRepository;
            _qcTaskRepository = QCTaskRepository;
            _qcCmmReportRepository = QCCmmReportRepository;
            _qcCmmFileSettingRepository = QCCmmFileSettingRepository;
            _prContentRepository = PRContentRepository;
            _prRepository = PurchanseRequestRepository;
            _charmillRepository = CharmillRepository;
            _edmRecordRepository = EDMTaskRepository;
            _departmentRepository = DepartmentRepository;
            _mgSettingRepository = MGSettingRepository;
            _wedmSettingRepository = WEDMSettingRepository;
            _taskHourRepository = TaskHourRepository;
            _machinesinfoRepository = MachinesInfoRepository;
            _taskTyprRepository = TaskTypeRepositroy;
            _workTyprRepository = WorkTypeRepository;
        }
        #endregion
        WorkHourDal dal = new WorkHourDal(constr);
        // GET: WorkHour
        public ActionResult Index()
        {
            return View();
        }
        #region 工时报表 按模号
        public ViewResult WHReportWithMold()
        {
            return View();
        }
        public JsonResult Service_WHReport_GetDataByMold(string MoldNum)
        {

            return null;
        }
        #endregion

        #region 工时报表 按机器
        public ViewResult WHReportWithMachine()
        {
            return View();
        }

        public JsonResult Service_WH_GetMachineInfoList(string keyword = "")
        {
            List<string> _mCodeList = _taskHourRepository.TaskHours.Select(h => h.MachineCode).Distinct().ToList();
            List<MachinesInfo> _mInfos = _machinesinfoRepository.MachinesInfo.Where(m => _mCodeList.Contains(m.MachineCode)).ToList();
            List<MachinesInfo> _mInfos1 = new List<MachinesInfo>();
            _mInfos1.Add(new MachinesInfo { MachineCode = "All", MachineName = "-" });
            if (!string.IsNullOrEmpty(keyword.Trim()))
            {
                keyword = keyword.ToUpper();
                _mInfos = _mInfos.Where(m => m.MachineCode.ToUpper().Contains(keyword) || m.MachineName.ToUpper().Contains(keyword)).ToList();
            }

            //if (_mCodeList != null)
            //{
            //    foreach(var m in _mCodeList)
            //    {
            //        MachinesInfo minfo = _machinesinfoRepository.GetMInfoByCode(m);
            //    }
            //}
            _mInfos1.AddRange(_mInfos);
            return Json(_mInfos1, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Service_WH_GetWHReportDataAByProc(string mCode, int taskType, DateTime? startTimeFr, DateTime? startTimeTo, DateTime? finishTimeFr, DateTime? finishTimeTo)
        {
            DataTable dt = dal.Get_MacWHReportDataA(mCode, taskType, startTimeFr, startTimeTo, finishTimeFr, finishTimeTo);
            string json = string.Empty;
            if (dt != null)
            {
                json = Common.JsonHelper.DataTableToJSON(dt);
            }
            return Json(json, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 工时报表 按任务类型

        #endregion

        #region 人员工时
        public ViewResult EmpWorkHourSubmit()
        {
            return View();
        }
        public ViewResult EmpWorkHourSubmitA()
        {
            return View();
        }
        /// <summary>
        /// 人工工时保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public string Service_WH_SaveEmpWHRecords(TaskHoursEmp model)
        {
            User user = (_userRepository.GetUserByCode(model.EmpCode) ?? new TechnikSys.MoldManager.Domain.Entity.User());
            model.DepID = user.DepartmentID;
            string res = dal.Chk_EmpWHRecords(model);
            if (string.IsNullOrEmpty(res))
            {
                try
                {
                    dal.Save_EmpWHRecords(model);
                }
                catch { }
            }
            return res;
        }
        public JsonResult Service_WH_GetEmpWHRecordsByDay(string EmpCode)
        {
            DataTable dt = dal.Get_EmpWHRecordsByDay(EmpCode);
            string json = string.Empty;
            if (dt != null)
            {
                json = Common.JsonHelper.DataTableToJSON(dt);
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// TODO:更新人工工时记录状态
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int Service_WH_UptEmpRecordState(int type, int id)
        {
            int status = 0;
            switch (type)
            {
                case 1:
                    status = 5;//待审核
                    break;
                case 2:
                    status = -100;//取消
                    break;
                case 3:
                    status = 100;//通过
                    break;
                case 4:
                    status = -99;//拒绝
                    break;
            }
            try
            {
                dal.Upt_EmpRecordState(id, status);
            }
            catch { }
            return 0;
        }
        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="ids"></param>
        public void Service_WH_SubmitEmpWHRecord(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var idArry = ids.Split(',');
                foreach (var _id in idArry)
                {
                    var _intid = Convert.ToInt32(_id);
                    TaskHourEmpViewModel dbEntry = dal.Get_EmpRecordById(_intid);
                    if (new List<int> { 0, -99 }.Contains(dbEntry.INTStatus))
                    {
                        Service_WH_UptEmpRecordState(1, _intid);
                    }
                }
            }
        }
        /// <summary>
        /// 取消工时
        /// </summary>
        /// <param name="ids"></param>
        public void Service_WH_CancelEmpWHRecord(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var idArry = ids.Split(',');
                foreach (var _id in idArry)
                {
                    var _intid = Convert.ToInt32(_id);
                    TaskHourEmpViewModel dbEntry = dal.Get_EmpRecordById(_intid);
                    if (new List<int> { 0, -99 }.Contains(dbEntry.INTStatus))
                    {
                        Service_WH_UptEmpRecordState(2, _intid);
                    }
                }
            }
        }
        #endregion

        #region 人工工时报表
        public ViewResult EmpWorkHourRecord()
        {
            return View();
        }
        public JsonResult Service_WH_GetEmpWHReportDataAByProc(int Status, string WorkType, DateTime? startTimeFr, DateTime? startTimeTo, DateTime? finishTimeFr, DateTime? finishTimeTo, int Depid = 0)
        {
            Depid = Convert.ToInt32(Request.Cookies["User"]["Department"]);
            DataTable dt = dal.Get_EmpWHReportDataA(Status, WorkType, startTimeFr, startTimeTo, finishTimeFr, finishTimeTo, Depid);
            string json=string.Empty;
            if (dt != null)
            {
                json = Common.JsonHelper.DataTableToJSON(dt);
            }
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 审核工时
        /// </summary>
        /// <param name="ids"></param>
        public void Service_WH_ApproEmpWHRecord(string ids, int type)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var idArry = ids.Split(',');
                foreach (var _id in idArry)
                {
                    int _intid = Convert.ToInt32(_id);
                    TaskHourEmpViewModel dbEntry = dal.Get_EmpRecordById(_intid);
                    if (new List<int> { 5 }.Contains(dbEntry.INTStatus))
                    {
                        Service_WH_UptEmpRecordState(type, _intid);
                    }
                }
            }
        }
        public JsonResult Service_WH_GetEmpWHById(int id)
        {
            try
            {
                TaskHourEmpViewModel model = dal.Get_EmpRecordById(id);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch { return null; }
        }

        #endregion

        #region 其它功能
        public JsonResult Service_WH_GetWorkType()
        {
            List<WH_WorkType> _worktypeList = _workTyprRepository.WH_WorkTypes.Where(t => t.Enabled).ToList();
            return Json(_worktypeList, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}