using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models;
using TechnikMold.UI.Models.GridViewModel;
using TechnikMold.UI.Models;

namespace MoldManager.WebUI.Controllers
{
    public class AdministratorController : Controller
    {
        private ITaskHourCostRepository _taskHourCostRepository;
        private IDepartmentRepository _departmentRepository;
        private IUserRepository _userRepository;
        private IPositionRepository _posRepository;
        private IMaterialRepository _materialRepository;
        private IHardnessRepository _hardnessRepository;
        private IBrandRepository _brandRepository;
        private IPhaseRepository _phaseRepository;
        private IDepPhaseRepository _depphaseRepository;
        private IMachinesInfoRepository _machinesinfoRepository;
        private IMachineRepository _machinesRepository;

        public AdministratorController(ITaskHourCostRepository  TaskHourCostRepository, 
            IDepartmentRepository DepartmentRepository, 
            IUserRepository UserRepository, 
            IPositionRepository PositionRepository, 
            IMaterialRepository MaterialRepository, 
            IHardnessRepository HardnessRepository, 
            IBrandRepository BrandRepository,
            IPhaseRepository PhaseRepository,
            IDepPhaseRepository DepPhaseRepository,
            IMachinesInfoRepository MachinesInfoRepository,
            IMachineRepository MachinesRepository)
        {
            _taskHourCostRepository = TaskHourCostRepository;
            _departmentRepository =DepartmentRepository ;
            _userRepository = UserRepository;
            _posRepository = PositionRepository;
            _materialRepository = MaterialRepository;
            _hardnessRepository = HardnessRepository;
            _brandRepository = BrandRepository;
            _phaseRepository = PhaseRepository;
            _depphaseRepository = DepPhaseRepository;
            _machinesinfoRepository = MachinesInfoRepository;
            _machinesRepository = MachinesRepository;
        }
        // GET: Administrator
        public ActionResult Index()
        {
            return View();
        }
        #region 列表管理
        /// <summary>
        /// 列表管理首页,显示列表类型和列表内容
        /// </summary>
        /// <param name="ListTypeID"></param>
        /// <returns></returns>
        public ActionResult ListManagement(int ListTypeID=0)
        {

            return View();
        }


        #endregion

        #region 工时费率管理
        public ActionResult TaskHourCost()
        {
            TaskHourCostEditModel _model = new TaskHourCostEditModel();
            _model.TaskHourCosts = _taskHourCostRepository.TaskHourCosts;
            _model.Departments = _departmentRepository.Departments.Where(d=>d.Enabled==true);
            return View(_model);
        }

        [HttpPost]
        public ActionResult TaskHourCost(IEnumerable<TaskHourCost> TaskHourCosts)
        {
            if (TaskHourCosts != null) { 
                foreach (TaskHourCost _costItem in TaskHourCosts)
                {
                    _taskHourCostRepository.Save(_costItem);
                }
            }
            return RedirectToAction("TaskHourCost", "Administrator");
        }
        #endregion

        #region 用户管理
        public ActionResult UserManagement()
        {
            return RedirectToAction("Index", "User");
        }

        public ActionResult Department()
        {
            IEnumerable<Department> _depts = _departmentRepository.Departments.Where(d => d.Enabled == true).Where(d => d.DepartmentID != 1);
            return View(_depts);
        }
        public JsonResult JsonPhases(int DepId)
        {
            #region depPhaseList-->PhaseList
            IEnumerable<Base_DepPhase> _depphaselist = _depphaseRepository.QueryByDepID(DepId);
            List<int> PhaseIDList = new List<int>();
            foreach (var depPhase in _depphaselist)
            {
                Phase pha = _phaseRepository.Phases.Where(p => p.PhaseID == depPhase.PhaseId).FirstOrDefault();
                PhaseIDList.Add(pha.PhaseID);
            }
            #endregion
            IEnumerable<Phase> _phases = _phaseRepository.Phases.Where(d => d.Enabled == true).Where(d=> !PhaseIDList.Contains(d.PhaseID));
            return Json(_phases, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JsonDepPhaseList(int DepId)
        {
            IEnumerable<Base_DepPhase> _depphaselist = _depphaseRepository.QueryByDepID(DepId);
            List<Phase> PhaseList = new List<Phase>();
            foreach(var depPhase in _depphaselist)
            {
                Phase pha = _phaseRepository.Phases.Where(p => p.PhaseID == depPhase.PhaseId).FirstOrDefault();
                PhaseList.Add(pha);
            }
            return Json(PhaseList, JsonRequestBehavior.AllowGet);
        }
        public void SaveDepPhases(int DepId, string PhaseIds)
        {
            List<int> _PhaseIds = Array.ConvertAll(PhaseIds.Split(','), id => Convert.ToInt32(id)).ToList<int>();

            List<int> _removedepPhaseIds = _depphaseRepository.DepPhases.Where(d => d.DepId == DepId).Select(d => d.Id).ToList();

            foreach (int id in _removedepPhaseIds)
            {
                _depphaseRepository.Delete(id);
            }

            foreach (int _PhaseId in _PhaseIds)
            {
                Base_DepPhase depPhase = new Base_DepPhase() { DepId = DepId, PhaseId = _PhaseId, Enable = true };
                _depphaseRepository.Save(depPhase);
            }

        }

        public string  DeleteDepartment(int DepartmentID)
        {
            int _userCount = _userRepository.Users.Where(u => u.DepartmentID == DepartmentID).Where(u=>u.Enabled==true).Count();
            string msg = "";
            if (_userCount == 0)
            {
                _departmentRepository.Delete(DepartmentID);
            }else{
                msg = "系统中还有属于该部门的员工， 无法删除";
            }
            return msg;

        }


        [HttpPost]
        public ActionResult DepartmentSave(Department Department)
        {
            _departmentRepository.Save(Department);
            return RedirectToAction("Department", "Administrator");
        }

        public bool ValidateDeptExist(string DepartmentName)
        {
            int _count = _departmentRepository.Departments.Where(d => d.Name == DepartmentName).Where(d=>d.Enabled==true).Count();
            if (_count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public ActionResult Position()
        {
            IEnumerable<Position> _positions = _posRepository.Positions.Where(p => p.Enabled == true);
            return View(_positions);
        }

        [HttpPost]
        public ActionResult PositionSave(Position Position)
        {
            _posRepository.Save(Position);
            return RedirectToAction("Position", "Administrator");
        }

        public ActionResult JsonPosition(int PositionID)
        {
            Position _poistion = _posRepository.QueryByID(PositionID);
            return Json(_posRepository, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonPositions()
        {
            IEnumerable<Position> _positions = _posRepository.Positions.Where(p => p.Enabled == true);
            return Json(_posRepository, JsonRequestBehavior.AllowGet);
        }

        public string DeletePosition(int PositionID)
        {
            string _msg = "";
            int _count = _userRepository.Users.Where(u => u.PositionID == PositionID).Count();
            if (_count > 0)
            {
                _msg = "系统中还存在属于该岗位的用户，无法删除";
            }else{
                _posRepository.Delete(PositionID);
                _msg="";
            }
            return _msg;
        }

        public JsonResult Service_Json_Department()
        {
            IEnumerable<Department> _depts = _departmentRepository.Departments.Where(d => d.Enabled == true).Where(d => d.DepartmentID != 1);
            return Json(_depts,JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 设备管理

        public ActionResult EquipmentList()
        {
            return View();
        }

        public ActionResult JsonEquipmentType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EquipmentSave(Equipment Equipment)
        {
            return View();
        }
        [HttpPost]
        public JsonResult Service_Json_GetMachinesInfo(string KeyWord = "")
        {
            List<MachinesInfo> _machinesInfos;
            _machinesInfos = _machinesinfoRepository.MachinesInfo.Where(m=>m.IsActive==true).OrderBy(m=>m.MachineCode).ToList();
            try
            {
                if (!string.IsNullOrEmpty(KeyWord))
                {
                    _machinesInfos = _machinesInfos.Where(m => m.MachineCode.ToUpper().Contains(KeyWord.ToUpper())).ToList()
                                     .Union(_machinesInfos.Where(m => m.MachineName.ToUpper().Contains(KeyWord.ToUpper())))
                                     .Union(_machinesInfos.Where(m => m.EquipBrand.ToUpper().Contains(KeyWord.ToUpper()))).ToList();
                }
                    
            }
            catch { }
            if (_machinesInfos == null)
                _machinesInfos = new List<MachinesInfo>();
            MachinesInfoGridViewModel _viewmodel = new MachinesInfoGridViewModel(_machinesInfos,_departmentRepository, _machinesinfoRepository);
            return Json(_viewmodel, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Service_Save_MachinesInfo(MachinesInfo model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.MachineCode) && !string.IsNullOrEmpty(model.TaskType))
                {
                    string _firstletter = "";
                    if (model.EquipBrand == "委外")
                        _firstletter = "Y";
                    model.MachineCode = _machinesinfoRepository.GenerateCode(model.TaskType, _firstletter);
                }                    
                if (_machinesinfoRepository.IsNullMachinesInfo(model) < 0)
                    return Json(new { Code = -1, Message = "关键信息(名称/工艺)不能为空！" });
                int r = _machinesinfoRepository.Save(model);
                if (r == 0)
                {
                    #region 更新设备配置表 Machines
                    List<Machine> _machines = _machinesRepository.Machines.Where(m => m.MachineCode == model.MachineCode).ToList();
                    if (_machines != null)
                    {
                        foreach (var m in _machines)
                        {
                            m.Name = model.MachineName;
                            _machinesRepository.Save(m);
                        }
                    }                   
                    #endregion
                    return Json(new { Code = 0, Message = "" });
                }                   
                else if (r == -1)
                    return Json(new { Code = -99, Message = "设备代码 " + model.MachineCode + " 重复！" });
                else if (r == -2)
                    return Json(new { Code = -99, Message = "设备名称 " + model.MachineName + " 重复！" });
                else if (r == -99)
                    return Json(new { Code = -99, Message = "设备代码 " + model.MachineCode + " 已失效！" });
            }
            catch(Exception ex) { LogRecord("设备保存异常信息", "异常信息——" + ex.Message); }
            return Json(new { Code = -99, Message = "设备保存时发生异常，详情请见日志文件！" });
        }
        public JsonResult Service_Get_MachinesInfo(string Code="")
        {
            MachinesInfo _machinesinfo = _machinesinfoRepository.GetMInfoByCode(Code);
            if (_machinesinfo != null)
                return Json(_machinesinfo, JsonRequestBehavior.AllowGet);
            return null;
        }
        public int Service_Del_MachinesInfo(string MCode)
        {
            MachinesInfo dbEntry = _machinesinfoRepository.GetMInfoByCode(MCode);
            if (dbEntry != null)
            {
                dbEntry.IsActive = false;
                dbEntry.Status = -99;
                _machinesinfoRepository.Save(dbEntry);
                return 0;
            }
            return -1;
        }
        public string Service_Get_GenerateCode(string TaskType = "1")
        {
            return _machinesinfoRepository.GenerateCode(TaskType);
        }
        #endregion

        #region 仓库信息
        public ActionResult WarehouseList()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Warehouse(Warehouse Warehouse)
        {
            return View();
        }
        #endregion
        #region 客户信息

        public ActionResult CustomerList()
        {
            return View();
        }

        public ActionResult CustomerSave()
        {
            return View();
        }
        #endregion

        #region 杂项
        public ActionResult PathRecord()
        {
            return View();
        }
        #endregion

        #region 材料库
        public ActionResult MaterialManagement()
        {
            return View();
        }


        [HttpPost]
        public int SaveMaterial(Material Material)
        {
            int _materialID = _materialRepository.Save(Material);
            return _materialID;
        }

        [HttpPost]
        public int SaveHardness(Hardness Hardness)
        {
            int _hardnessID = _hardnessRepository.Save(Hardness);
            return _hardnessID;
        }

        public void DeleteMaterial(int MaterialID)
        {
            try
            {
                _materialRepository.Delete(MaterialID);
            }
            catch
            {

            }
            
        }

        public void DeleteHardness(int HardnessID)
        {
            try
            {
                _hardnessRepository.Delete(HardnessID);
            }
            catch
            {

            }
            
        }


        public ActionResult BrandManagement()
        {
            return View();
        }

        public int SaveBrand(Brand Brand)
        {
            Brand.Enabled = true;
            int _brandID= _brandRepository.Save(Brand);
            return _brandID;            
        }

        public void DeleteBrand(int BrandID)
        {
            _brandRepository.Delete(BrandID);
        }
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