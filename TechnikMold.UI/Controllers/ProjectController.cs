using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridViewModel;
using MoldManager.WebUI.Models.ViewModel;
using MoldManager.WebUI.Models.EditModel;
using System.Text;
using System.IO;
using MoldManager.WebUI.Tools;
using System.Linq.Expressions;
using TechnikSys.MoldManager.UI.Models.ViewModel;
using TechnikSys.MoldManager.Domain.Output;
using TechnikMold.UI.Models;

namespace MoldManager.WebUI.Controllers
{
    public class ProjectController : Controller
    {
        private IProjectRepository _projectRepository;
        private IProjectPhaseRepository _projectPhaseRepository;
        private IPhaseRepository _phasesRepository;
        private IRoleRepository _roleRepository;
        private IProjectRoleRepository _projectRoleRepository;
        private ICustomerRepository _customerRepository;
        private IPhaseModificationRepository _phaseModificationRepository;
        private ICostCenterRepository _costCenterRepository;
        private IDepartmentRepository _deptRepository;
        private IPartListRepository _partListRepository;

        private IProjectRecordRepository _projectRecordRepository;
        private IDepPhaseRepository _depphaseRepository;
        private ITaskTypeRepository _taskTypeRepository;
        private IPhaseTaskTypeRepository _phaseTasktypeRepository;

        public ProjectController(IProjectRepository ProjectRepository,
            IProjectPhaseRepository ProjectPhaseRepository, 
            IPhaseRepository PhaseRepository,
            IRoleRepository RoleRepository, 
            IProjectRoleRepository ProjectRoleRepository, 
            ICustomerRepository CustomerRepository, 
            IPhaseModificationRepository PhaseModificationRepository,
            ICostCenterRepository CostCenterRepository, 
            IDepartmentRepository DepartmentRepository,
            IPartListRepository PartListRepository,
            IProjectRecordRepository ProjectRecordRepository,
            IDepPhaseRepository DepPhaseRepository,
            ITaskTypeRepository TaskTypeRepository,
            IPhaseTaskTypeRepository PhaseTaskTypeRepository)
        {

            _projectRepository = ProjectRepository;
            _projectPhaseRepository = ProjectPhaseRepository;
            _phasesRepository = PhaseRepository;
            _roleRepository = RoleRepository;
            _projectRoleRepository = ProjectRoleRepository;
            _customerRepository = CustomerRepository;
            _phaseModificationRepository = PhaseModificationRepository;
            _costCenterRepository = CostCenterRepository;
            _deptRepository = DepartmentRepository;
            _partListRepository = PartListRepository;
            _projectRecordRepository = ProjectRecordRepository;
            _depphaseRepository = DepPhaseRepository;
            _taskTypeRepository = TaskTypeRepository;
            _phaseTasktypeRepository = PhaseTaskTypeRepository;
        }
        // GET: Project
        public ActionResult Index(string Keyword = "", int State = 1, int Type=1,bool IsDepFinish=true)
        {
            ViewBag.Keyword = Keyword;
            ViewBag.State = State;
            ViewBag.Type = Type;
            ViewBag.IsDepFinish = IsDepFinish;
            return View();
        }

        #region ProjectEdit
        /// <summary>
        /// Display page for project create/edit.
        /// For different parameters there are three different situations.
        /// 1. No parameter provided: Create a main project;
        /// 2. Only ProjectID: Edit main or sub project;
        /// 3. Only ParentID:　Create sub project under project specified by ParentID
        /// </summary>
        /// <param name="ProjectID">Primary key for project.</param>
        /// <param name="ParentID">Primary key for main project.</param>
        /// <returns></returns>
        //public ActionResult Edit(int ProjectID = 0, int ParentID = 0, int ProjectType = 1)
        //{
        //    int _projectID = ProjectID;
        //    int _parentID = ParentID;
        //    ProjectViewModel _viewModel;
        //    IEnumerable<Phase> _phases = _phasesRepository.Phases.Where(p => p.Enabled == true).OrderBy(p => p.Sequence);
        //    IEnumerable<Role> _roles = _roleRepository.Roles.Where(r => r.ProjectBased == true).OrderBy(r => r.Name);
        //    Project _project = new Project();
        //    ViewBag.ProjectID = ProjectID;
        //    ViewBag.ParentID = ParentID;
        //    ViewBag.ParentName = _projectRepository.Projects.Where(p => p.ProjectID == _parentID).Select(p => p.ProjectNumber).FirstOrDefault();
        //    ViewBag.Version = GetFixVersion(ParentID);

        //    if ((ProjectID == 0) && (ParentID != 0))
        //    {
        //        //Create a new mold project
        //        _projectID = _parentID;
        //    }
        //    if (_projectID > 0)
        //    {
        //        //Retrive the main project information
        //        _project = _projectRepository.Projects.Where(p => p.ProjectID == _projectID).FirstOrDefault();

        //        //Get the main project if the selected project is a sub project
        //        if ((_project.ParentID != 0) && (ProjectID == 0) && (ProjectType != 2))
        //        {
        //            _project = _projectRepository.Projects.Where(p => p.ProjectID == _project.ParentID).First();
        //            _projectID = _project.ProjectID;
        //            ViewBag.ParentID = _project.ProjectID;
        //        }
        //        IEnumerable<ProjectPhase> _projectPhases = _projectPhaseRepository.ProjectPhases.Where(p => p.ProjectID == _projectID);
        //        IEnumerable<ProjectRole> _projectRoles = _projectRoleRepository.ProjectRoles.Where(p => p.ProjectID == _projectID);
        //        _viewModel = new ProjectViewModel(_phases, _roles, _project, _projectPhases, _projectRoles);
        //        return View(_viewModel);
        //    }
        //    else
        //    {
        //        //Create a new main project
        //        _viewModel = new ProjectViewModel(_phases, _roles, _project);
        //        return View(_viewModel);
        //    }
        //}


        public ActionResult Edit(int ProjectID = 0, int ParentID = 0, int ProjectType = 1)
        {
            ProjectViewModel _viewModel;
            IEnumerable<Phase> _phases = _phasesRepository.Phases.Where(p => p.Enabled == true).OrderBy(p => p.Sequence);
            IEnumerable<Role> _roles = _roleRepository.Roles.Where(r => r.ProjectBased == true).OrderBy(r => r.Name);
            Project _project=new Project();
            IEnumerable<ProjectPhase> _projectPhases=null;
            IEnumerable<ProjectRole> _projectRoles=null;

            if (ProjectID > 0)
            {
                _project = _projectRepository.GetByID(ProjectID);
            }
            else if (ParentID>0)
            {
                _project = _projectRepository.GetByID(ParentID);
                _project.ParentID = _project.ProjectID;
                _project.ProjectID = 0;
                _project.Type = ProjectType;
            }

            if (ProjectID == 0)
            {
                switch (ProjectType)
                {
                    case 0:
                        _project.FixMoldType = "主项目";
                        break;
                    case 1:
                        _project.FixMoldType = "新模项目";
                        break;
                    case 2:
                        _project.FixMoldType = "";
                        break;
                }
            }
            

            if (_project.ProjectID>0)
            {
                _projectPhases = _projectPhaseRepository.GetProjectPhases(_project.ProjectID).OrderBy(p=>p.PhaseID);
                _projectRoles = _projectRoleRepository.QueryByProjectID(_project.ProjectID);
            }
            else if (_project.ParentID>0)
            {
                _projectPhases = _projectPhaseRepository.GetProjectPhases(_project.ParentID);
                _projectRoles = _projectRoleRepository.QueryByProjectID(_project.ParentID);
            }




            _viewModel = new ProjectViewModel(ProjectID, ParentID, ProjectType, _phases, _roles, _project, _projectPhases, _projectRoles);
            return View(_viewModel);
        }

        public int GetFixVersion(string MoldNumber)
        {
            IEnumerable<Project> _projects = _projectRepository.Projects.Where(p=>p.Type==2).Where(p => p.MoldNumber == MoldNumber);
            int _count = _projects.Count();
            if (_projects.Count()==0)
            {
                return 1;
            }
            else
            {
                int _version = _projects.Select(p => p.Version).Max() + 1;
                return _version;
            }
        }

        /// <summary>
        /// Project information save request processor
        /// </summary>
        /// <param name="Project"></param>
        /// <returns></returns>
        [HttpPost] 
        public ActionResult Edit(ProjectEditModel Project)
        {
            bool newProject = Project.Project.ProjectID == 0 ? true : false;
            if (newProject)
            {
                //Project.Project.ProjectStatus = 1;
                Project.Project.Enabled = true;
            }
            int _projectID = _projectRepository.Save(Project.Project);

            
            foreach (ProjectRole _projectRole in Project.ProjectRoles)
            {
                _projectRole.ProjectID = _projectID;
                _projectRoleRepository.Save(_projectRole);
            }
            foreach (ProjectPhase _projectPhase in Project.ProjectPhases)
            {
                _projectPhase.ProjectID = _projectID;
                _projectPhaseRepository.Save(_projectPhase);
            }
            //if (newProject)
            //{
            //    CostCenter _costCenter = new CostCenter();
            //    _costCenter.ProjectID = _projectID;
            //    _costCenter.Name = Project.Project.Name + "-" + Project.Project.MoldNumber;
            //}

            if (newProject)
            {
                string _userName = "";
                try
                {
                    _userName = HttpUtility.UrlDecode(Request.Cookies["User"]["FullName"], Encoding.GetEncoding("UTF-8"));
                }
                catch
                {
                    _userName = "";
                }
                string _projectType = "";
                switch (Project.Type)
                {
                    case 0:
                        _projectType = "项目";
                        break;
                    case 1:
                        _projectType = "模具项目";                       
                        break;
                    case 2:
                        _projectType = "修模项目";
                        break;
                    default:
                        _projectType = "项目";
                        break;
                }

                string _recordMemo = _userName + "创建" + _projectType;

                AddProjectRecord(_projectID, _recordMemo);
                #region by michael 创建新模项目 更新partlist模具号
                if (Project.Project.Type == 1)
                {
                    try
                    {
                        List<PartList> _partlists = _partListRepository.PartLists.Where(p => p.MoldNumber == Project.Project.MoldNumber).ToList() ?? new List<PartList>();
                        if (_partlists.Count > 0)
                        {
                            foreach (var _pl in _partlists)
                            {
                                _pl.ProjectID = _projectID;
                                _partListRepository.Save(_pl);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                #endregion
            }

            if (Project.Project.Type > 0)
            {
                return RedirectToAction("Index", "Project", new { Type = Project.Project.Type });
            }
            else
            {
                return RedirectToAction("Index", "Project");
            }
           
        }



        [HttpPost]
        public ActionResult Memo(ProjectMemoEditModel MemoEdit)
        {
            string UserName = HttpUtility.UrlDecode(Request.Cookies["User"]["FullName"], Encoding.GetEncoding("UTF-8"));

            string _memo = UserName +"添加备注:"+ MemoEdit.Memo;
            MemoEdit.Memo = UserName+ "<br>" + DateTime.Now.ToString("yyyy-MM-dd") +"<br>"+ MemoEdit.Memo; 



            _projectRepository.AddMemo(MemoEdit.MemoProject, MemoEdit.Memo);

            AddProjectRecord(MemoEdit.MemoProject, _memo);

            return RedirectToAction("Index", "Project");
        }


        public string CheckMainFOT(int ProjectID)
        {
            Project _project = _projectRepository.GetByID(ProjectID);
            if (_project.ParentID == 0)
            {
                return "只能基于模具项目创建修模项目";
            }

           
            if (!CheckProjectFOT(ProjectID))
            {
                return "模具项目FOT结束前不能创建修模项目";
            }
            else
            {
                IEnumerable<Project> _fixPrjs = _projectRepository.Projects
                    .Where(p => p.MoldNumber == _project.MoldNumber)
                    .Where(p => p.Type == 2)
                    .OrderByDescending(p=>p.ProjectID);

                if (_fixPrjs.Count() > 0)
                {
                    if (!CheckProjectFOT(_fixPrjs.First().ProjectID))
                    {
                        //Exist fix mold project not FOT
                        return "还有试模未结束的修模项目, 无法创建新的修模项目";
                    }
                    else
                    {
                        //All existing fix mold projects are FOT finished
                        return "";
                    }
                }
                else
                {
                    //No fix mold project exist
                    return "";
                }
                
            }
        }
        /// <summary>
        /// Check whether the project is already FOT
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns>
        /// true:Project FOT
        /// false:Project NOT FOT
        /// </returns>
        public bool CheckProjectFOT(int ProjectID)
        {
            Project _project = _projectRepository.GetByID(ProjectID);
            if (_project.Type == 1)
            {
                ProjectPhase _phase = _projectPhaseRepository.GetProjectPhases(ProjectID).Where(p => p.PhaseID == 12).FirstOrDefault();
                if (_phase != null)
                {
                    if (_phase.ActualFinish != null)
                    {
                        DateTime _finishTime = _phase.ActualFinish;
                        if (_finishTime != new DateTime(1, 1, 1))
                        {
                            return true;
                        }

                    }
                }
                
                return false;
            }
            else
            {
                return true;
            }
            
        }


        #endregion

        #region MilestoneModification

        #region 调整计划单元格编辑保存函数
        public JsonResult Service_Save_ProJPhaseCFDate(int ProJID,int PhaseID,DateTime CFDate)
        {
            ProjectPhase _projectPhase = _projectPhaseRepository.GetProjectPhase(ProJID, PhaseID);
            #region 获取当前项目主项目计划完成日期
            //Project _modifyProJ = _projectRepository.GetByID(ProJID);
            ////Project _mainProJ;
            //if (_modifyProJ.ParentID > 0)
            //{
                //_mainProJ = _projectRepository.GetByID(_modifyProJ.ParentID);
                //ProjectPhase _mainPhase = _projectPhaseRepository.GetProjectPhase(_mainProJ.ProjectID, PhaseID);
                //DateTime _mainPhaseDate = Toolkits.CheckZero(_mainPhase.PlanCFinish) ? _mainPhase.PlanFinish : _mainPhase.PlanCFinish;
                //子项目完成日期不能晚于主项目完成日期
                //if (_mainPhaseDate< CFDate)
                //{
                //    return Json(new { Code = -1 }, JsonRequestBehavior.AllowGet);
                //}
                //else if (!Toolkits.CheckZero(_mainPhase.ActualFinish))
                //{
                //    return Json(new { Code = -2 }, JsonRequestBehavior.AllowGet);
                //}
            //}
            #endregion
            int _prjPhaseID = 0;
            if (_projectPhase != null)
            {
                try
                {
                    _prjPhaseID = _projectPhaseRepository.Save(_projectPhase.ProjectPhaseID, CFDate);
                    if (_prjPhaseID > 0)
                    {
                        #region 更新ProJ状态为1
                        Project _project = _projectRepository.GetByID(ProJID) ?? new Project();
                        if(_project.ProjectID>0 && _project.ProjectStatus == 0)
                        {
                            IEnumerable<ProjectPhase> _proJPhases = _projectPhaseRepository.GetProjectPhases(ProJID);
                            var _isSetupAllPlan = true;
                            foreach(var p in _proJPhases)
                            {
                                if (Toolkits.CheckZero(p.PlanFinish))
                                    _isSetupAllPlan = false;
                            }
                            if (_isSetupAllPlan)
                            {
                                _project.ProjectStatus = 1;
                                _projectRepository.Save(_project);
                            }
                        }
                        #endregion
                        return Json(new { Code = 0 }, JsonRequestBehavior.AllowGet);
                    }                        
                }
                catch(Exception ex)
                {
                    _prjPhaseID = _projectPhaseRepository.Save(0, CFDate, ProJID, PhaseID);
                }
            }
            return Json(new { Code = -99 }, JsonRequestBehavior.AllowGet);
        }

        public string Service_Get_ProJPhaseAcDate(int ProJID, int PhaseID)
        {
            ProjectPhase _projectPhase = _projectPhaseRepository.GetProjectPhase(ProJID, PhaseID);
            if (_projectPhase != null)
            {
                //判断是否存在原计划
                if (Toolkits.CheckZero(_projectPhase.PlanFinish))
                {
                    return "当前项目未设置原计划";
                }
                int _depid = Convert.ToInt32(Request.Cookies["User"]["Department"]);
                Base_DepPhase _depphase = _depphaseRepository.DepPhases.Where(d => d.PhaseId == PhaseID && d.DepId == _depid && d.Enable == true).FirstOrDefault();
                if (_depphase != null)
                {
                    if (Toolkits.CheckZero(_projectPhase.ActualFinish))
                        return "";
                }
                else
                    return "只能调整本部门计划";         
                return _projectPhase.ActualFinish.ToString("yy/MM/dd");
            }
            #region 获取当前项目主项目并判断
            Project _modifyProJ = _projectRepository.GetByID(ProJID);
            Project _mainProJ;
            //新模计划
            //if (_modifyProJ.Type == 1)
            //{
                if (_modifyProJ.ParentID > 0)
                {
                //主项目异常判断
                    _mainProJ = _projectRepository.GetByID(_modifyProJ.ParentID);
                    ProjectPhase _mainPhase = _projectPhaseRepository.GetProjectPhase(_mainProJ.ProjectID, PhaseID);
                    if(_mainPhase==null)
                        return "主项目数据异常！";
                    DateTime _mainPhaseDate = Toolkits.CheckZero(_mainPhase.PlanCFinish) ? _mainPhase.PlanFinish : _mainPhase.PlanCFinish;
                    //if (!Toolkits.CheckZero(_mainPhase.ActualFinish))
                    //{
                    //    return "主项目阶段已结束";
                    //}
                    //if (Toolkits.CheckZero(_mainPhase.PlanFinish))
                    //{
                    //    return "主项目未设置原计划";
                    //}
                }
            //    else
            //    {
            //        return "该项目无对应主项目";
            //    }
            //}
            //else
            //{
            //    return "不允许在此更新主项目计划";
            //}
            #endregion
                     
            return "不存在项目阶段";
        }
        public string Service_Get_ProJPhaseYDate(int ProJID, int PhaseID)
        {
            ProjectPhase _projectPhase = _projectPhaseRepository.GetProjectPhase(ProJID, PhaseID);
            if (_projectPhase != null)
            {
                int _depid = Convert.ToInt32(Request.Cookies["User"]["Department"]);
                List<int> _camPhases = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
                if (_depid == 3 && _camPhases.Contains(PhaseID))
                {
                    if (Toolkits.CheckZero(_projectPhase.PlanFinish))
                    {
                        return "";
                    }
                }
                else if (new List<int> { 1, 2 }.Contains(_depid) && !_camPhases.Contains(PhaseID))
                {
                    if (Toolkits.CheckZero(_projectPhase.PlanFinish))
                    {
                        return "";
                    }
                }
                else
                {
                    return "非计划部门禁止修改原计划";
                }
                return _projectPhase.PlanFinish.ToString("yyyyMMdd");
            }
            return "项目阶段数据不存在";
        }
        #endregion


        /// <summary>
        /// Response for the project phase modification
        /// </summary>
        /// <param name="PhaseChange"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ModifyPhase(ProjectPhaseEditModel PhaseChange)
        {
            string UserName;
            PhaseModification _phaseModification = new PhaseModification();
            ProjectPhase _projectPhase = _projectPhaseRepository.GetProjectPhase(PhaseChange.ProjectID, PhaseChange.PhaseID);
            //Save the latest phase date
            //_projectPhase.PlanCFinish=PhaseChange.PlanCFinish;
            int _prjPhaseID = 0;
            try
            {
                _prjPhaseID = _projectPhaseRepository.Save(_projectPhase.ProjectPhaseID, PhaseChange.PlanCFinish);
            }
            catch
            {
                _prjPhaseID = _projectPhaseRepository.Save(0, PhaseChange.PlanCFinish, PhaseChange.ProjectID, PhaseChange.PhaseID);
            }

            //Record the phase date moidifcation
            #region Add phaseModification 阶段调整记录
            try
            {
                _projectPhase = _projectPhaseRepository.GetProjectPhase(_prjPhaseID);
                if (_projectPhase != null)
                {
                    _phaseModification.ProjectPhaseID = _projectPhase.ProjectPhaseID;
                    _phaseModification.BeforeModify = PhaseChange.PlanFinish;
                    _phaseModification.AfterModify = PhaseChange.PlanCFinish;
                    try
                    {
                        UserName = HttpUtility.UrlDecode(Request.Cookies["User"]["FullName"], Encoding.GetEncoding("UTF-8"));
                    }
                    catch
                    {
                        UserName = "";
                    }
                    _phaseModification.User = UserName;
                    _phaseModification.ModifyDate = DateTime.Now;
                    _phaseModification.Reason = PhaseChange.Reason;
                    string _desc = "用户" + UserName + "调整项目计划从" + _phaseModification.BeforeModify.ToString("yyyy-MM-dd") +
                        "为" + _phaseModification.AfterModify.ToString("yyyy-MM-dd");
                    _phaseModification.Description = PhaseChange.Description == null ? _desc : _desc + ", 备注:" + PhaseChange.Description;

                }
                _phaseModificationRepository.Save(_phaseModification);
            }
            catch
            {

            }
            #endregion

            Project _project = _projectRepository.Projects.Where(p => p.ProjectID == PhaseChange.ProjectID).FirstOrDefault();
            _project.Memo = PhaseChange.Description;
            _projectRepository.Save(_project);

            if (_project.ParentID == 0)
            {
                SetPhaseModifyTag(_project.ProjectID, PhaseChange.PhaseID);
            }

            AddProjectRecord(_project.ProjectID, _project.Memo);

            if (_project.Type == 0)
            {
                return RedirectToAction("Index", "Project");
            }
            else
            {
                return RedirectToAction("Index", "Project", new { Type = _project.Type });
            }
            
        }

        /// <summary>
        /// When modify phases of main project, set the phase change tag for sub projects
        /// </summary>
        /// <param name="ProjectID"></param>
        public  void SetPhaseModifyTag(int ProjectID, int PhaseID)
        {
            IEnumerable<int> _subs = _projectRepository.QueryByMainProject(ProjectID)
                .Where(p=>p.Type==1).Select(p=>p.ProjectID);
            
            //foreach (Project _sub in _subs)
            //{
            //    _sub.MainPhaseChange = true;
            //    _projectRepository.Save(_sub);
            //}

            foreach (int _projectid in _subs)
            {
                ProjectPhase _projectPhase = _projectPhaseRepository.GetProjectPhase(_projectid, PhaseID);
                _projectPhase.MainChange = true;
                _projectPhaseRepository.Save(_projectPhase);
            }
        }

        #endregion


        #region JSON
        /// <summary>
        /// Create Json data of existing customers
        /// </summary>
        /// <returns></returns>
        public JsonResult JsonCustomers(){
            IEnumerable<Customer> _customers = _customerRepository.Customers.Where(c=>c.Enabled==true);
            return Json(_customers, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Retrives json format project list
        /// </summary>
        /// <returns></returns>
        public JsonResult JsonProjects1(string Keyword="", int State=1, int Type=1)
        {
            IQueryable<Project> _projects;
            if (Type == 1)
            {
                _projects = _projectRepository.Projects
               .Where(p => p.Enabled == true)
               .Where(p => (p.Type == Type) || (p.Type == 0))
               .Where(p=>p.Version==0)
               .OrderBy(p => p.ProjectNumber)
               .ThenBy(p => p.MoldNumber);
            }
            else
            {
                _projects = _projectRepository.Projects
               .Where(p => p.Enabled == true)
               .Where(p => (p.Type == Type))
               .OrderBy(p => p.ProjectNumber)
               .ThenBy(p => p.MoldNumber);
            }


            if (Keyword != "")
            {
                IQueryable<Project> _a = _projects.Where(p => p.ProjectNumber.Contains(Keyword));
                IQueryable<Project> _b = _projects.Where(p => p.MoldNumber.Contains(Keyword));
                _projects = _projects.Where(p => p.ProjectNumber.Contains(Keyword))
                    .Union(_projects.Where(p => p.MoldNumber.Contains(Keyword)));
            }
            else
            {
                if (State == 1)
                {

                    _projects = _projects.Where(p => p.ProjectStatus <= State);
                }
                else
                {
                    _projects = _projects.Where(p => p.ProjectStatus == State);
                }          
            }
            List<Phase> _phases = _phasesRepository.Phases.OrderBy(p => p.Sequence).ToList();
          
            ProjectGridViewModel _gridViewModel = new ProjectGridViewModel(_projects, _projectPhaseRepository, _projectRoleRepository, _phases);
            return Json(_gridViewModel, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// TODO:Projects Json
        /// </summary>
        /// <param name="Keyword"></param>
        /// <param name="State"></param>
        /// <param name="Type"></param>
        /// <param name="DepID"></param>
        /// <param name="PageCount"></param>
        /// <param name="Page"></param>
        /// <param name="isDepFinish">true 部门未结案 false 项目未结案</param>
        /// <returns></returns>

        public ActionResult JsonProjects(string Keyword = "", int State = 1, int Type = 1, int DepID = 18, int PageCount = 18, int Page = 1,bool isDepFinish=true)
        {
            IQueryable<Project> _projects;
            List<Phase> _phases;
            ProjectGridViewModel _gridViewModel = null;
            Expression<Func<Project, bool>> _kwexp = null;
            Expression<Func<Project, bool>> _typeexp = null;
            List<Project> _projectsByDep;
            //部门/项目阶段 绑定判断 By Michael
            //Expression<Func<Project, bool>> _phaseexp = null;

            #region Prepare Expression
            if (Type == 1)
            {
                _typeexp = p => p.Type == 1;
                _typeexp = PredicateBuilder.Or(_typeexp, p => p.Type == 0);
                _typeexp = PredicateBuilder.And(_typeexp, p => p.Version == 0);
            }
            else
            {
                _typeexp = p => p.Type == Type;
            }

            _typeexp = PredicateBuilder.And(_typeexp, p => p.Enabled == true);

            if (Keyword != "")
            {
                _kwexp = p => p.ProjectNumber.Contains(Keyword);
                _kwexp = PredicateBuilder.Or(_kwexp, p => p.MoldNumber.Contains(Keyword));
            }
            else
            {
                if (State == 1)
                {
                    _kwexp = p => p.ProjectStatus <= State;
                }
                else
                {
                    _kwexp = p => p.ProjectStatus == State;
                }
            }
            #endregion
            int _takeCount = PageCount / 3;
            int _skipcount = (Page - 1) * _takeCount;
            _projects = _projectRepository.Projects
             .Where(_typeexp)
             .Where(_kwexp)
             .OrderBy(p => p.ProjectNumber).ThenBy(p => p.MoldNumber);

            int _totalprojects = 0;
            _phases = _phasesRepository.Phases.OrderBy(p => p.Sequence).ToList();
            if (Keyword == "")
            {
                if (isDepFinish)
                {
                    //_projectsByDep = GetProjectsByDep(_projects, DepID, _skipcount, _takeCount).ToList();
                    _projectsByDep = _projectRepository.GetProjectsByDep(DepID).Where(_kwexp).Where(_typeexp).OrderBy(p => p.ProjectNumber).ThenBy(p => p.MoldNumber).ToList();
                }
                else
                {
                    _projectsByDep = _projects.Where(p => p.Enabled == true && p.ProjectNumber != "Sinnotech").Where(p => p.ProjectStatus < 3).ToList();//新建 启动
                }
                //_projectsByDep = _projects.Where(p => p.ProjectNumber != "Sinnotech" && p.ProjectStatus >= 0);
                #region 分页   
                _totalprojects = _projectsByDep.Count();
                _projectsByDep = _projectsByDep.Skip(_skipcount).Take(_takeCount).ToList();
                #endregion
            }
            else
            {
                _projectsByDep = _projects.ToList();
                _totalprojects = _projects.Count();
                _projectsByDep = _projects.Skip(_skipcount).Take(_takeCount).ToList();
            }
            //_projects = GetProjectsByDep(_projects, DepID, _skipcount, _takeCount).OrderBy(p => p.ProjectNumber).ThenBy(p => p.MoldNumber).ToList();
            //IEnumerable<Project> _projectsByDepNow = _projectsByDep.Skip(_skipcount).Take(_takeCount);
                       
            

            _gridViewModel = new ProjectGridViewModel(_projectsByDep,
                _projectPhaseRepository,
                _projectRoleRepository,
                _phases,
                _totalprojects,
                Page,
                _takeCount);
            return Json(_gridViewModel, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 规划项目计划首页-划部门
        /// </summary>
        /// <param name="Projects"></param>
        /// <param name="DepartmentID"></param>
        /// <returns></returns>
        public IEnumerable<Project> GetProjectsByDep(IEnumerable<Project> Projects, int DepartmentID,int skipcount,int takeCount)
        {
            IEnumerable<Project> ProjectList;
            DateTime _datezero = new DateTime(1, 1, 1);
            List<ProJPHViewModel> lst = new List<ProJPHViewModel>();
            List<int> ProJIDList = new List<int>();            
            Projects = Projects.Where(p => p.ProjectNumber != "Sinnotech" && p.ProjectStatus >= 0);
            foreach (var Project in Projects)
            {
                var ProJPHList = from pp in _projectPhaseRepository.ProjectPhases
                                 where pp.ProjectID== Project.ProjectID
                                 select pp;
                foreach (var item in ProJPHList)
                {
                    var phase = from ph in _phasesRepository.Phases
                                where ph.PhaseID == item.PhaseID
                                select ph;
                    string name = "";
                    foreach (var ph in phase)
                    {
                        name = ph.Name;
                    }
                    lst.Add(new ProJPHViewModel
                    {
                        ProjectID = item.ProjectID,
                        PhaseID = item.PhaseID,
                        Name = name,
                        ActualFinish = item.ActualFinish
                    });
                }
            }                      
            try
            {
                #region 部门-项目阶段控制
                //string[] PhName;
                IEnumerable<int> ProjectID;
                //switch (DepartmentID)
                //{
                //    //管理
                //    case 1:
                //        break;
                //    //CAD
                //    case 2:
                //        PhName = new string[] { "CAD" };
                //        ProjectID = lst.Where(i => PhName.Contains(i.Name) & i.ActualFinish != _datezero).Select(i => i.ProjectID);
                //        ProJIDList = ProjectID.ToList();
                //        break;
                //    //CAM
                //    case 3:
                //        PhName = new string[] { "CAM" };
                //        ProjectID = lst.Where(i => PhName.Contains(i.Name) & i.ActualFinish != _datezero).Select(i => i.ProjectID);
                //        ProJIDList = ProjectID.ToList();
                //        break;
                //    //采购
                //    case 4:
                //        PhName = new string[] { "采购" };
                //        ProjectID = lst.Where(i => PhName.Contains(i.Name) & i.ActualFinish != _datezero).Select(i => i.ProjectID);
                //        ProJIDList = ProjectID.ToList();
                //        break;
                //    //CNC
                //    case 7:
                //        PhName = new string[] { "CNC", "CNC开粗" };
                //        ProjectID = lst.Where(i => PhName.Contains(i.Name) & i.ActualFinish != _datezero).Select(i => i.ProjectID);
                //        ProJIDList = ProjectID.ToList();
                //        break;
                //    //EDM
                //    case 8:
                //        PhName = new string[] { "EDM" };
                //        ProjectID = lst.Where(i => PhName.Contains(i.Name) & i.ActualFinish != _datezero).Select(i => i.ProjectID);
                //        ProJIDList = ProjectID.ToList();
                //        break;
                //    //WEDM
                //    case 9:
                //        PhName = new string[] { "WEDM" };
                //        ProjectID = lst.Where(i => PhName.Contains(i.Name) & i.ActualFinish != _datezero).Select(i => i.ProjectID);
                //        ProJIDList = ProjectID.ToList();
                //        break;
                //    //仓库
                //    case 20:
                //        PhName = new string[] { "热处理" };
                //        ProjectID = lst.Where(i => PhName.Contains(i.Name) & i.ActualFinish != _datezero).Select(i => i.ProjectID);
                //        ProJIDList = ProjectID.ToList();
                //        break;
                //    //MG 磨床
                //    case 5:
                //        PhName = new string[] { "开粗", "磨床" };
                //        ProjectID = lst.Where(i => PhName.Contains(i.Name) & i.ActualFinish != _datezero).Select(i => i.ProjectID);
                //        ProJIDList = ProjectID.ToList();
                //        break;
                //    //装配
                //    case 11:
                //        PhName = new string[] { "装配" };
                //        ProjectID = lst.Where(i => PhName.Contains(i.Name) & i.ActualFinish != _datezero).Select(i => i.ProjectID);
                //        ProJIDList = ProjectID.ToList();
                //        break;
                //    //异常
                //    case 18:
                //        Projects = new List<Project>();
                //        break;
                //}
                IEnumerable<Base_DepPhase> depPhases = _depphaseRepository.QueryByDepID(DepartmentID).Where(d=>d.DepId!=1);//管理部门可以看到所有项目
                List<int?> phaseIds = new List<int?>();
                if (DepartmentID != 1)
                {
                    foreach (var depPhase in depPhases)
                        phaseIds.Add(depPhase.PhaseId);
                }
                else
                {
                    foreach (var depPhase in _phasesRepository.Phases.ToList())
                        phaseIds.Add(depPhase.PhaseID);
                }
                ProjectID = phaseIds.Count == 0 ? new List<int>() : lst.Where(i => phaseIds.Contains(i.PhaseID) && _datezero.Equals(i.ActualFinish)).Select(i => i.ProjectID);
                ProJIDList = ProjectID.ToList();
                #endregion
            }
            catch (Exception ex)
            {

            }
            
            ProjectList = Projects.Where(p => ProJIDList.Contains(p.ProjectID)); //.Skip(skipcount).Take(takeCount)
            return ProjectList;
        }
        /// <summary>
        /// Retrives Json data for project phases
        /// </summary>
        /// <returns></returns>
        public JsonResult JsonPhases()
        {
            IEnumerable<Phase> _phases = _phasesRepository.Phases.Where(p => p.Enabled == true).OrderBy(p => p.Sequence);
            return Json(_phases, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonProjectPhase(int ProjectID, int PhaseID)
        {
            ProjectPhaseViewModel _phase =new ProjectPhaseViewModel();
            //s _mainPhase, _subPhase;
            int _mainProjectID = _projectRepository.Projects.Where(p=>p.ProjectID==ProjectID).Select(p=>p.ParentID).FirstOrDefault();
            if (_mainProjectID>0)
            {
                _phase.MainPhase = _projectPhaseRepository.GetCurrentPlan(_mainProjectID, PhaseID).ToString("yyyy-MM-dd");
                //_phase.MainPhase = _projectRepository
            }
            _phase.ProjectPhase = _projectPhaseRepository.GetCurrentPlan(ProjectID, PhaseID).ToString("yyyy-MM-dd");
            
            return Json(_phase, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonMoldNumber(string Keyword="", bool TakeAll=false)
        {


            IEnumerable<Project> _moldProjects;
            if (Keyword != "")
            {
                _moldProjects =_projectRepository.Projects
                .Where(p=>p.Name!="")
                .Where(p=>p.Enabled==true)
                .Where(p => p.ParentID > 0)
                .Where(p => p.MoldNumber.Contains(Keyword));
            }else{
                _moldProjects=_projectRepository.Projects
                .Where(p=>p.Name!="")
                .Where(p => p.ParentID > 0)
                .Where(p=>p.Enabled==true);
            }
            if (TakeAll != true)
            {
                _moldProjects = _moldProjects.Where(p => p.ProjectStatus <2);
            }
            IEnumerable<string> _moldNumbers = _moldProjects.OrderBy(p=>p.MoldNumber).Select(p => p.MoldNumber).Distinct();
            return Json(_moldNumbers, JsonRequestBehavior.AllowGet);
        }


        public int CheckMoldNumber(string MoldNumber)
        {
            Project _project = _projectRepository.QueryByMoldNumber(MoldNumber);
            if (_project != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int CheckProjectNumber(string ProjectNumber)
        {
            Project _project = _projectRepository.QueryByProjectNumber(ProjectNumber);
            if (_project != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public JsonResult JsonProject(string MoldNumber)
        {
            Project _project = _projectRepository.GetLatestActiveProject(MoldNumber);
            return Json(_project, JsonRequestBehavior.AllowGet);
        }

       

        #endregion


        #region CommonMethods
        public int PauseProject(int ProjectID, string Memo, bool PauseSubs)
        {

            string _memo;
            string _recordMemo;
            if (Memo != "")
            {
                _memo = " 备注信息: <br>" + Memo;
            }
            else
            {
                _memo = "";
            }
            string _userName;
            try
            {
                _userName = HttpUtility.UrlDecode(Request.Cookies["User"]["FullName"], Encoding.GetEncoding("UTF-8")); 
            }
            catch
            {
                _userName = "";
            }
            _memo = _userName+"<br>"+  _memo;
            int state = _projectRepository.PauseProject(ProjectID, _memo, PauseSubs);

            if (state==1){
                _recordMemo = "暂停";
            }else{
                _recordMemo = "继续";
            }

            _recordMemo = _userName+"设置项目"+_recordMemo+"。";
            if (Memo!=""){
                _recordMemo = _recordMemo + "备注信息:" + Memo;
            }
            AddProjectRecord(ProjectID, _recordMemo);
            
            return state;
        }

        public void DeleteProject(int ProjectID, string Memo)
        {
            _projectRepository.DeleteProject(ProjectID, Memo);
        }

        public int FinishPhase(int ProjectID, int PhaseID)
        {
            try
            {
                if (PhaseID == 14)
                {
                    int _parent = _projectRepository.GetByID(ProjectID).ParentID;
                    if (_parent == 0)
                    {
                        int _subUnfinish = _projectRepository.QueryByMainProject(ProjectID).Where(p => p.ProjectStatus != 90).Count();
                        if (_subUnfinish > 0)
                        {
                            return 2;
                        }
                    }
                }
                _projectPhaseRepository.FinishPhase(ProjectID, PhaseID);
                return 0;
            }
            catch
            {
                return 1;
            }            
        }

        public int SubProjectCount(int ProjectID)
        {
            int subCount = _projectRepository.Projects.Where(p => p.ParentID == ProjectID).Where(p=>p.Enabled==true).Count();
            return subCount;
        }
        #endregion


        public ActionResult CustomerManagement()
        {
            IEnumerable<Customer> _Customers = _customerRepository.Customers.Where(c => c.Enabled == true);
            return View(_Customers);
        }

        public ActionResult JsonCustomer(int CustomerID)
        {
            Customer _customer = _customerRepository.QueryByID(CustomerID);
            return Json(_customer, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CustomerSave(Customer Customer)
        {
            _customerRepository.Save(Customer);
            return RedirectToAction("CustomerManagement", "Project");
        }

        public string  DeleteCustomer(int CustomerID)
        {
            string msg = "";
            int _projectCount = _projectRepository.Projects.Where(p => p.CustomerID == CustomerID).Count();
            if (_projectCount == 0)
            {
                _customerRepository.Delete(CustomerID);
            }
            else
            {
                msg = "系统中有该客户相关项目，无法删除";
            }
            return msg;
        }

        public int UniqueCustomer(string Name)
        {
            int _count = _customerRepository.Customers.Where(c =>c.Name.ToLower() == Name.ToLower())
                .Where(c => c.Enabled == true).Count();
            return _count;
        }

        public bool PhaseDeptValidate(int PhaseID, int DepartmentID)
        {
            //string _phase = _phasesRepository.Phases.Where(p => p.PhaseID == PhaseID).Select(p=>p.Name).FirstOrDefault();
            string _dept = _deptRepository.GetByID(DepartmentID).Name;
            Base_DepPhase depPhase = _depphaseRepository.QueryByDepID(DepartmentID).Where(d => d.PhaseId == PhaseID).FirstOrDefault() ?? new Base_DepPhase();
            if ((depPhase.Id>0) ||(_dept=="管理"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        public string SaveProjectFile(int FileProjectID, HttpPostedFileBase Attachment)
        {

            Project _project = _projectRepository.GetByID(FileProjectID);

            if (Attachment == null)
            {
                return "文件不存在";
            }
            //string _fullname = "/Upload/" + _project.MoldNumber + "/";

            //string filePhysicalPath = Server.MapPath("~"+_fullname);
            //if (!Directory.Exists(filePhysicalPath))//判断上传文件夹是否存在，若不存在，则创建
            //{
            //    Directory.CreateDirectory(filePhysicalPath);//创建文件夹
            //}

            var physicsFileName = Server.MapPath(GetUploadPath(_project.MoldNumber)+Attachment.FileName);

            try
            {
                Attachment.SaveAs(physicsFileName);
                string _fileName = Attachment.FileName;
                _projectRepository.AddAttachment(FileProjectID, _fileName);
                return "保存成功";
            }
            catch
            {
                return "保存失败";
            }
            
        }

        private string GetUploadPath(string MoldNumber)
        {
            string _fullname = "/Upload/" + MoldNumber + "/";

            string filePhysicalPath = Server.MapPath("~" + _fullname);
            if (!Directory.Exists(filePhysicalPath))//判断上传文件夹是否存在，若不存在，则创建
            {
                Directory.CreateDirectory(filePhysicalPath);//创建文件夹
            }
            return _fullname;
        }

        public ActionResult ProjectFile(int ProjectID)
        {
            Project _project = _projectRepository.GetByID(ProjectID);
            string _fileName = _project.Attachment;
            string _path = "/Upload/" + _project.MoldNumber + "/";

            string _fullPath = _path + _fileName;

            return File(_fullPath, "application/vnd.ms-powerpoint", _fileName);
            
        }

        public string GetMoldName(string MoldNumber)
        {
            try
            {
                Project _project = _projectRepository.QueryByMoldNumber(MoldNumber, 1);
                return _project.Name;
            }
            catch
            {
                return "";
            }
            
        }


        public string GetProjectNumber(string MoldNumber)
        {
            try
            {
                Project _project = _projectRepository.QueryByMoldNumber(MoldNumber, 1);
                return _project.ProjectNumber;
            }
            catch
            {
                return "";
            }
        }

        public int GetProjectID(string MoldNumber)
        {
            
            Project _project = _projectRepository.GetLatestActiveProject(MoldNumber);
            if (_project != null)
            {
                return _project.ProjectID;
            }
            else
            {
                return 0;
            }           
        }

        public void AddProjectRecord(int ProjectID, string RecordContent)
        {
            string _moldNumber = _projectRepository.GetByID(ProjectID).MoldNumber;
            _projectRecordRepository.Save(ProjectID, RecordContent, _moldNumber);
        }

        public string GetMoldNumber(int ProjectID)
        {
            string _moldNumber = _projectRepository.GetByID(ProjectID).MoldNumber;
            return _moldNumber;
        }


        public ActionResult JsonProjectRecord(int ProjectID)
        {
            IEnumerable<ProjectRecord> _records = _projectRecordRepository.QueryByProjectID(ProjectID);
            return Json(_records, JsonRequestBehavior.AllowGet);
        }

        public string CheckFixMoldPhase(int ProjectID, int PhaseID, DateTime PlanCFinish)
        {
            ProjectPhase _ots = _projectPhaseRepository.GetProjectPhases(ProjectID).Where(p => p.PhaseID == 13).FirstOrDefault();
            ProjectPhase _ppap = _projectPhaseRepository.GetProjectPhases(ProjectID).Where(p => p.PhaseID == 14).FirstOrDefault();
            DateTime _otsDate = _ots.PlanCFinish == new DateTime(1, 1, 1) ? _ots.PlanFinish : _ots.PlanCFinish;
            DateTime _ppapDate = _ppap.PlanCFinish == new DateTime(1, 1, 1) ? _ppap.PlanFinish : _ppap.PlanCFinish;
            if(PhaseID<13)
            {
                if (PlanCFinish > _otsDate)
                {
                    if (_ots.ActualFinish > new DateTime(1, 1, 1))
                    {
                        return "计划完成日期不能晚于OTS日期";
                    }
                    else
                    {
                        if (PlanCFinish > _ppapDate)
                        {
                            return "计划完成日期不能晚于PPAP日期";
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }            
        }

        public ActionResult CostSummary(string MoldNumber)
        {
            return View();
        }

        #region   added by felix 20170729
        /// <summary>
        /// 获取某模具的版本清单（版本控制）  
        /// </summary>
        /// <param name="MoldNumber">模具编号</param>
        /// <returns></returns>
        public JsonResult GetMoldVerList(string MoldNumber)
        {
            try
            {
                List<MoldVersionInfo> _project =
                    _projectRepository.GetProjectVerList(MoldNumber);
                return Json(_project, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 判断是否已发布
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public int GetIsPublish(int ProjectID = 0)
        {
            try
            {
                Project prj = _projectRepository.GetByID(ProjectID);
                if (prj != null && prj.IsPublish == true)
                {
                    return 1;
                }
                else if (prj != null && prj.IsPublish == false)
                {
                    return 0;
                }
                else
                    return -1;
            }
            catch
            {
                return -1;
            }
        }
        public JsonResult GetProjectJson(int ProjectID = 0)
        {
            try
            {
                Project prj = _projectRepository.GetByID(ProjectID);
                return Json(prj, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return null;
            }
        }

        public int GetIsUpgrade(int ProjectID = 0)
        {
            try
            {
                Project prj = _projectRepository.GetByID(ProjectID);//当前项目
                Project _project = _projectRepository.QueryByMoldNumber(prj.MoldNumber);//最新版本的项目

                if (prj != null && prj.IsPublish == true && prj.Version == _project.Version)
                {//可以升级（最新的版本 ，已发布的）
                    return 1;
                }
                else if (prj != null && prj.Version != _project.Version)
                {
                    return 0;//不是最新版本
                }
                else if (prj != null && prj.Version == _project.Version && prj.IsPublish == false)
                {
                    return 2;//该最新版本未发布
                }
                else
                    return -1;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// publish project
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public int PublishProject(string moldNumber)
        {
            try
            {
                //获取模具最后一个版本的id
                int projectID = GetProjectID(moldNumber);
                int rs = 0;
                //修改该版本的ispublish
                if (projectID > 0)
                {
                    Project _project = _projectRepository.GetByID(projectID);
                    _project.IsPublish = true;
                    rs = _projectRepository.Save(_project);
                }
                if (rs > 0)
                    return 1;
                else return 0;
            }
            catch
            {
                return 0;
            }
        }

        #endregion
        #region added by michael 180708
        public bool IsMainPJ(string pjID)
        {
            if (!string.IsNullOrEmpty(pjID) && pjID!="undefined")
            {
                int pjIDint = Convert.ToInt32(pjID);
                Project pj = _projectRepository.GetByID(pjIDint);
                if (pj.ParentID == 0)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 项目-任务历史
        public string Service_GetMoldNoByProID(int projectid)
        {
            string _moldno = _projectRepository.GetByID(projectid).MoldNumber;
            return _moldno == "---" ? "" : _moldno;
        }
        public JsonResult Service_GetTaskTypeByPhaseID(int phaseid)
        {
            List<PhaseTaskType> _phaseTasktypes = _phaseTasktypeRepository.PhaseTaskTypes.Where(p => p.PhaseID == phaseid).ToList();
            List<TaskType> _tasktypes = new List<TaskType>();
            if (_phaseTasktypes != null)
            {
                foreach(var p in _phaseTasktypes)
                {
                    TaskType _type = _taskTypeRepository.TaskTypes.Where(t => t.TaskID == p.TaskID && t.Enable==true).FirstOrDefault();
                    if (_type != null)
                        _tasktypes.Add(_type);
                }
                return Json(_tasktypes, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        #endregion
    }
}