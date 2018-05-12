using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models ;

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

        public AdministratorController(ITaskHourCostRepository  TaskHourCostRepository, 
            IDepartmentRepository DepartmentRepository, 
            IUserRepository UserRepository, 
            IPositionRepository PositionRepository, 
            IMaterialRepository MaterialRepository, 
            IHardnessRepository HardnessRepository, 
            IBrandRepository BrandRepository)
        {
            _taskHourCostRepository = TaskHourCostRepository;
            _departmentRepository =DepartmentRepository ;
            _userRepository = UserRepository;
            _posRepository = PositionRepository;
            _materialRepository = MaterialRepository;
            _hardnessRepository = HardnessRepository;
            _brandRepository = BrandRepository;
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
            IEnumerable<Department> _depts = _departmentRepository.Departments.Where(d => d.Enabled == true);
            return View(_depts);
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
    }
}