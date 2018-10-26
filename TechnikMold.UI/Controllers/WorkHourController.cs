using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnikSys.MoldManager.Domain.Abstract;

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
            IMachinesInfoRepository MachinesInfoRepository)
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
        }
        #endregion
        // GET: WorkHour
        public ActionResult Index()
        {
            return View();
        }

    }
}