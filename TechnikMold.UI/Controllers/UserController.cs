using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridViewModel;
using System.Text;
using MoldManager.WebUI.Models.EditModel;

namespace MoldManager.WebUI.Controllers
{
    public class UserController : Controller
    {
        //private static int _pageCount = 20;
        private IUserRepository _userRepository;
        private IDepartmentRepository _departmentRepository;
        private IPositionRepository _posRepository;
        private IUserRoleRepository _userRoleRepository;

        public UserController(IUserRepository UserRepository,
            IDepartmentRepository DepartmentRepository, 
            IPositionRepository PositionRepository, 
            IUserRoleRepository UserRoleRepository)
        {
            _userRepository = UserRepository;
            _departmentRepository = DepartmentRepository;
            _posRepository = PositionRepository;
            _userRoleRepository = UserRoleRepository;
        }
        //
        // GET: /User/
        public ActionResult Index(string Keyword="")
        {
            IEnumerable<User> _user = _userRepository.Users;
            ViewBag.Keyword = Keyword;
            return View(_user);
        }

        /// <summary>
        /// Validate the current user is in the system database.
        /// If the cookie is not set, create the cookies;
        /// </summary>
        /// <param name="UserName">User name and doman in "domain\user" format</param>
        /// <returns>The full name of current logged on user</returns>
        public string  Validate(string UserName)
        
        {
            //UserName = "sinno-tech\\administrator";
            string _userName;
            if (UserName.IndexOf('\\') > 0)
            {
                string[] _info = UserName.Split('\\');
                _userName = _info[1];
            }
            else
            {
                _userName = UserName;
            }
            
            string _displayName;
            if (Request.Cookies["User"]==null)
            {
                User _user = null;
                try
                {
                    _user = _userRepository.GetUserByName(_userName);
                }
                catch
                {
                    _user = null;
                }
                
                
                if (_user != null)
                {
                    Department _dept = _departmentRepository.GetByID(_user.DepartmentID);
                    HttpCookie _cookie = new HttpCookie("User");
                    Position _pos = _posRepository.QueryByID(_user.PositionID);

                    _cookie.Values.Add("UserID", _user.UserID.ToString());
                    _cookie.Values.Add("FullName", HttpUtility.UrlEncode(_user.FullName, Encoding.GetEncoding("UTF-8")));
                                      
                    _cookie.Values.Add("Department", _user.DepartmentID.ToString());
                    _cookie.Values.Add("DepartmentName", HttpUtility.UrlEncode(_dept.Name, Encoding.GetEncoding("UTF-8")));

                    _cookie.Values.Add("Position", _user.PositionID.ToString());
                    _cookie.Values.Add("PositionName", HttpUtility.UrlEncode(_pos.Name, Encoding.GetEncoding("UTF-8")));

                    Response.Cookies.Add(_cookie);
                    //_displayName = _user.FullName;
                    _displayName = Request.Cookies["User"]["FullName"]
                    + "(" + Request.Cookies["User"]["DepartmentName"]
                    + "-" + Request.Cookies["User"]["PositionName"] + ")";
                }
                else
                {
                    string[] _info = UserName.Split('\\');
                    Response.Redirect("/User/NoRegister?UserName=" + _info[1] + "---" + UserName);
                    _displayName = "";
                }
            }
            else
            {
                _displayName = Request.Cookies["User"]["FullName"]
                    +"("+Request.Cookies["User"]["DepartmentName"]
                    +"-"+Request.Cookies["User"]["PositionName"]+")";
            }    
            return HttpUtility.UrlDecode(_displayName, Encoding.GetEncoding("UTF-8"));
        }




        #region 用户
        public JsonResult Users(string Keyword="")
        {
            
            IEnumerable<User> _users = _userRepository.Users.Where(u => u.Enabled == true).OrderBy(u => u.FullName);
            if (Keyword != "") {
                _users = _users.Where(u => u.FullName.ToLower().Contains(Keyword.ToLower())).Union(_users.Where(u => u.LogonName.ToLower().Contains(Keyword.ToLower())));
            }
            IEnumerable<Department> _departments = _departmentRepository.Departments.Where(d => d.Enabled == true);
            IEnumerable<Position> _positions = _posRepository.Positions.Where(p => p.Enabled == true);
            UserGridViewModel _viewModel = new UserGridViewModel(_users, _departments, _positions, _users.Count());
            return Json(_viewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserByID(int UserID)
        {
            User _user = _userRepository.GetUserByID(UserID);
            return Json(_user, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserByName(string UserName)
        {
            User _user = _userRepository.GetUserByName(UserName);
            return Json(_user, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 项目编辑页面 人员编辑列表数据源
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="_proJRole"></param>
        /// <returns></returns>
        public JsonResult FilterUser(string UserName = "",int _proJRole=1)
        {
            List<int> _depList = new List<int>();
            switch(_proJRole){
                case 1:
                    _depList.Add(21);//项目
                    break;
                case 2:
                    _depList.Add(2);//CAD
                    break;
                case 3:
                    _depList.Add(24);//钳工
                    break;
            }
            IEnumerable<User> _users = _userRepository.FilterUser(_depList,UserName) ;
            return Json(_users, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditUser(User User)
        {
            int _userID = _userRepository.Save(User);
            UserRole _userRole = _userRoleRepository.GetUserRoles(_userID).Where(u => u.DepartmentID == User.DepartmentID)
                .Where(u => u.PositionID == User.PositionID).FirstOrDefault();
            if (_userRole == null)
            {
                _userRole = new UserRole(User.UserID, User.DepartmentID, User.PositionID);
                int _userRoleID = _userRoleRepository.Save(_userRole);
                DefaultUserRole(_userRoleID);
            }
            else
            {
                DefaultUserRole(_userRole.UserRoleID);
            }
            return RedirectToAction("Index", "User", new { UserID = _userID });
        }

        public ActionResult DeleteUser(string UserIDs)
        {
            string[] _userID = UserIDs.Split(',');
            for (int i = 0; i < _userID.Length - 1; i++)
            {
                try
                {
                    int _id = Convert.ToInt32(_userID[i]);
                    _userRepository.Delete(_id);
                }
                catch
                {

                }
            }
            return RedirectToAction("Index", "User");
        }

        public ActionResult UserEmail()
        {
            User _user=null;
            try
            {
                int _userID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
                _user = _userRepository.GetUserByID(_userID);

            }
            catch
            {

            }
            return PartialView(_user);
        }

        public ActionResult NoRegister()
        {
            return View();
        }
        #endregion
        #region 部门
        public JsonResult Departments()
        {
            IEnumerable<Department> _departments = _departmentRepository.Departments.Where(d => d.Enabled == true);
            return Json(_departments, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveDepartment(Department Department)
        {
            int _departmentID = _departmentRepository.Save(Department);
            return RedirectToAction("Index", "User");
        }
        #endregion

        public ActionResult JsonPositions()
        {
            IEnumerable<Position> _positions = _posRepository.Positions.Where(p => p.Enabled == true);
            return Json(_positions, JsonRequestBehavior.AllowGet);
        }

        public bool ValidateUserExist(string UserName)
        {
            User _user = _userRepository.GetUserByName(UserName);
            if (_user != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public ActionResult GetUsersByDepartment(int DepartmentID)
        {
            IEnumerable<User> _users = _userRepository.Users.Where(u => u.Enabled == true).Where(u => u.DepartmentID == DepartmentID);
            return Json(_users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonUserRoleList(int UserID)
        {
            IEnumerable<UserRole> _userRoles = _userRoleRepository.GetUserRoles(UserID);
            List<UserRoleEditModel> _viewModel= new List<UserRoleEditModel>();
            foreach (UserRole _role in _userRoles)
            {
                string DisplayName = "";
                DisplayName = _departmentRepository.GetByID(_role.DepartmentID).Name + "-"
                    + _posRepository.QueryByID(_role.PositionID).Name;
                if (_role.DefaultRole)
                {
                    DisplayName = DisplayName + "(默认)";
                }

                _viewModel.Add( new UserRoleEditModel(_role.UserRoleID, DisplayName));
            }
            return Json(_viewModel.OrderBy(u=>u.DisplayName), JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonUserRole(int UserRoleID)
        {
            UserRole _userRole = _userRoleRepository.QueryByID(UserRoleID);
            return Json(_userRole, JsonRequestBehavior.AllowGet);
        }

        public string SaveRole(int UserRoleID, int UserID, int DepartmentID, int PositionID)
        {
            UserRole _userRole;
            if (UserRoleID == 0)
            {
                _userRole = new UserRole(UserID, DepartmentID, PositionID);
            }
            else
            {
                _userRole = _userRoleRepository.QueryByID(UserRoleID);
                _userRole.DepartmentID = DepartmentID;
                _userRole.PositionID = PositionID;
            }
            
            try
            {
                _userRoleRepository.Save(_userRole);
                return "";
            }
            catch
            {
                return "fail";   
            }            
        }

        public string DeleteRole(int UserRoleID)
        {
            try
            {
                _userRoleRepository.Delete(UserRoleID);
                return "";
            }
            catch
            {
                return "fail";
            }
        }

        public string DefaultUserRole(int UserRoleID){
            try
            {
                UserRole _userRole = _userRoleRepository.QueryByID(UserRoleID);
                User _user = _userRepository.GetUserByID(_userRole.UserID);
                _user.DepartmentID = _userRole.DepartmentID;
                _user.PositionID = _userRole.PositionID;
                _userRepository.Save(_user);
                _userRoleRepository.SetDefault(UserRoleID);
                return "";
            }
            catch
            {
                return "fail";
            }
        }

        public void ReloadCookie(int UserRoleID)
        {
            HttpCookie _cookie = new HttpCookie("User");
            _cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(_cookie);

            UserRole _userRole = _userRoleRepository.QueryByID(UserRoleID);
            string _user = _userRepository.GetUserByID(_userRole.UserID).FullName;
            string _department = _departmentRepository.GetByID(_userRole.DepartmentID).Name;
            string _position = _posRepository.QueryByID(_userRole.PositionID).Name;

            _cookie = new HttpCookie("User");
            _cookie.Values.Add("UserID", _userRole.UserID.ToString());
            _cookie.Values.Add("FullName", HttpUtility.UrlEncode(_user, Encoding.GetEncoding("UTF-8")));


            _cookie.Values.Add("Department", _userRole.DepartmentID.ToString());
            _cookie.Values.Add("DepartmentName", HttpUtility.UrlEncode(_department, Encoding.GetEncoding("UTF-8")));

            _cookie.Values.Add("Position", _userRole.PositionID.ToString());
            _cookie.Values.Add("PositionName", HttpUtility.UrlEncode(_position, Encoding.GetEncoding("UTF-8")));

            Response.Cookies.Add(_cookie);
        }

        public ActionResult SwitchRole()
        {
            return View();
        }

    }
}