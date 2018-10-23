using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Output;
using System.Drawing;
using System.IO;
using MoldManager.WebUI.Models.GridViewModel;
using MoldManager.WebUI.Models.Helpers;
using TechnikSys.MoldManager.Domain.Status;
using CNCPgm;
using System.Threading;
using EDMPgm;
using MoldManager.WebUI.Models.ViewModel;
using Newtonsoft.Json;
using System.Text;
using TechnikMold.UI.Models.GridViewModel;
using TechnikMold.UI.Models.ViewModel;
using TechnikMold.UI.Models;

namespace MoldManager.WebUI.Controllers
{
    public class TaskController : Controller
    {
        #region 参数定义
        private ITaskRepository _taskRepository;
        private IPartRepository _partRepository;
        //private ICAMTasksRepository _camTaskRepository;
        private IProjectRepository _projectRepository;
        private IUserRepository _userRepository;
        private ICNCItemRepository _cncItemRepository;
        private IQCInfoRepository _qcInfoRepository;
        private IWarehouseStockRepository _whStockRepository;
        private IMachineRepository _machineRepository;
        private ICNCMachInfoRepository _machInfoRepository;
        private ISteelGroupProgramRepository _steelGroupProgramRepository;
        private ISteelProgramRepository _steelProgramRepository;
        private IEDMDetailRepository _edmDetailRepository;
        private IEDMSettingRepository _edmSettingRepository;
        private IQCPointProgramRepository _qcPointProgramRepository;
        private ISystemConfigRepository _systemConfigRepository;
        private IQCSteelPointRepository _qcSteelPointRepository;
        private ISteelCAMDrawingRepository _steelCAMDrawingRepository;
        private ISteelDrawingCADPartRepository _steelDrawingCADPartRepository;
        private ICAMDrawingRepository _camDrawingRepository;
        private IProjectPhaseRepository _projectPhaseRepository;
        private IPOContentRepository _poContentRepository;
        private IQCTaskRepository _qcTaskRepository;
        private IQCCmmReportRepository _qcCmmReportRepository;
        private IQCCmmFileSettingRepository _qcCmmFileSettingRepository;
        private IPRContentRepository _prContentRepository;
        private IPurchaseRequestRepository _prRepository;
        private ICharmillRepository _charmillRepository;
        private IEDMTaskRecordRepository _edmRecordRepository;
        private IDepartmentRepository _departmentRepository;
        private IMGSettingRepository _mgSettingRepository;
        private IWEDMSettingRepository _wedmSettingRepository;
        private ITaskHourRepository _taskHourRepository;
        private IMachinesInfoRepository _machinesinfoRepository;
        #endregion
        #region 构造
        public TaskController(ITaskRepository TaskRepository,
            IPartRepository PartRepository,
            //ICAMTasksRepository CAMTaskRepository,
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

        // GET: Task
        public ActionResult Index(string MoldNumber = "", int TaskType = 1, int State = 0)
        {


            //switch (TaskType)
            //{
            //    case 1:
            //        return RedirectToAction("CAMTaskList", new { ProjectID = ProjectID, State = State });
            //    case 7:
            //        return RedirectToAction("QCTaskList", new { ProjectID = ProjectID, State = State });
            //    default:
            //        return RedirectToAction("MachineTaskList", new { ProjectID = ProjectID, TaskType = TaskType, State = State });
            //}

            try
            {
                int Department = Convert.ToInt32(Request.Cookies["User"]["Department"]);
                switch (Department)
                {
                    case 7:
                        return RedirectToAction("MachineTaskList", new { TaskType = 1, State = 0 });
                    case 8:
                        return RedirectToAction("MachineTaskList", new { TaskType = 2, State = 0 });
                    case 9:
                        return RedirectToAction("MachineTaskList", new { TaskType = 3, State = 0 });
                    default:
                        return RedirectToAction("MachineTaskList");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #region TaskList
        public ActionResult CAMTaskList(string MoldNumber = "", int State = -99, int TaskType = 1, int CAM = 1)
        {
            ViewBag.MoldNumber = MoldNumber==null?"":MoldNumber;
            ViewBag.TaskType = TaskType;
            ViewBag.State = State;
            try
            {
                ViewBag.Department = Request.Cookies["User"]["Department"];
            }
            catch
            {
                ViewBag.Department = 0;
            }
            TaskType _type = new TaskType();
            string _title = new TaskType().GetTypeName(TaskType);
            ViewBag.TaskTypeName = _title;
            if (CAM == 1)
            {
                if (State == -99)
                {
                    ViewBag.Title = _title + "待发布";
                }
                else
                {
                    ViewBag.Title = _title + "发布历史";
                }
            }
            else
            {
                if (State == 0)
                {
                    ViewBag.Title = _title + "加工状态";
                }
                else
                {
                    ViewBag.Title = _title + "加工历史";
                }
            }

            //ViewBag.Title = _type.GetTypeName(TaskType);



            ViewBag.CAM = CAM;
            return View();
        }

        /// <summary>
        /// Set the task enable tag
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public int DeleteTask(int TaskID)
        {
            try
            {
                _taskRepository.Delete(TaskID);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int CheckTaskExists(string TaskName)
        {
            IEnumerable<Task> _tasks = _taskRepository.QueryByName(TaskName);

            if (_tasks.Count() > 0)
            {
                return _tasks.Select(t => t.Version).Max();
            }
            else
            {
                return -1;
            }
        }

        public ActionResult MachineTaskList(string MoldNumber = "", int TaskType = 1, int State = 0)
        {
            ViewBag.MoldNumber = MoldNumber;
            ViewBag.TaskType = TaskType;
            ViewBag.State = State;
            TaskType _type = new TaskType();
            ViewBag.Title = _type.GetTypeName(TaskType);
            return View();
        }


        public ActionResult ProcessTaskList(int State = 1)
        {
            try
            {
                int _dept = Convert.ToInt32(Request.Cookies["User"]["Department"]);
                switch (_dept)
                {
                    case 7:
                        return RedirectToAction("CNCProcessList");

                    case 8:
                        return RedirectToAction("EDMProcessList");
                    case 9:
                        return RedirectToAction("WEDMProcessList");
                }
            }
            catch
            {

            }
            return RedirectToAction("Index", "Home");
        }


        public ActionResult CNCProcessList(int TaskType = 1)
        {
            return View();
        }

        public ActionResult EDMProcessList(string TaskID)
        {
            List<Task> _eleList = new List<Task>();

            //_eleList.AddRange(_edmSettingRepository.QueryByTaskID(Convert.ToInt32(_taskIDs[i])).ToList());
            try
            {
                string _detail = _edmDetailRepository.QueryByTaskID(Convert.ToInt32(TaskID)).EleDetail;
                string[] _eleInfo = _detail.Split(';');
                for (int j = 0; j < _eleInfo.Length; j++)
                {
                    Task _task = GetTask(_eleInfo[j]);
                    if (_task != null)
                    {
                        _eleList.Add(_task);
                    }
                }

                ViewBag.TaskID = TaskID;
                return View(_eleList.OrderBy(t => t.TaskName));
            }
            catch
            {
                return View();
            }

        }

        private Task GetTask(string EleDetail)
        {
            string _eleName = EleDetail.Substring(0, EleDetail.LastIndexOf('_'));

            int _eleVer = Convert.ToInt32(EleDetail.Substring(EleDetail.LastIndexOf('_') + 2, EleDetail.Length - EleDetail.LastIndexOf('_') - 2));

            Task _task = _taskRepository.Tasks.Where(t => t.TaskType == 1)
                .Where(t => t.TaskName == _eleName)
                .Where(t => t.Version == _eleVer)
                //.Where(t=>t.State==0)
                .FirstOrDefault();
            return _task;
        }

        public string EDMProgram(List<EDMItemViewModel> Items,
            string MachineName,
            double RaiseHeight,
            int PieceCount,
            bool SelectFirst,
            List<string> EleList = null,
            List<string> PosList = null)
        //List<SpecElePos> SpecElePosList=null)
        {
            List<EDMItem> _EDMItems = new List<EDMItem>();
            List<SpecElePos> SpecElePosList = null;
            if (EleList != null)
            {
                SpecElePosList = GetPositionList(EleList, PosList);
            }
            foreach (EDMItemViewModel _viewItem in Items)
            {
                EDMItem _edmItem = new EDMItem();
                _edmItem.CNCMachMethod = _viewItem.CNCMachMethod;
                _edmItem.ELEName = _viewItem.ELEName;
                _edmItem.ElePoints = _viewItem.ElePoints;
                _edmItem.EleType = _viewItem.EleType;
                _edmItem.Gap = _viewItem.Gap;
                _edmItem.GapCompensate = _viewItem.GapCompensate;
                _edmItem.LableName = _viewItem.LableName;
                _edmItem.Material = _viewItem.Material;
                _edmItem.Obit = _viewItem.Obit;
                _edmItem.OffsetC = _viewItem.OffsetC;
                _edmItem.OffsetX = _viewItem.OffsetX;
                _edmItem.OffsetY = _viewItem.OffsetY;
                _edmItem.OffsetZ = _viewItem.OffsetZ;
                _edmItem.Position = Convert.ToInt32(_viewItem.Position);
                _edmItem.StockGap = _viewItem.StockGap;
                _edmItem.Surface = _viewItem.Surface;
                _edmItem.ZCompensate = _viewItem.ZCompensate;
                _edmItem.SValue = GetSValue(_viewItem, MachineName);
                _EDMItems.Add(_edmItem);
            }
            string _deviceType = "";
            EDMProgram _p = null;
            switch (MachineName)
            {
                case "夏米尔350":
                    _deviceType = "350_WithC";
                    _p = new EDMProgram(_EDMItems, _deviceType, RaiseHeight, PieceCount, SelectFirst);
                    _p.GetProgram();
                    break;
                case "夏米尔35P":
                    _deviceType = "35P_WithC";
                    _p = new EDMProgram(_EDMItems, _deviceType, RaiseHeight, PieceCount, SelectFirst);
                    _p.GetProgram();
                    break;
                case "夏米尔350 没有C":
                    _deviceType = "350_WithoutC";
                    _p = new EDMProgram(_EDMItems, _deviceType, RaiseHeight, PieceCount, SelectFirst);
                    _p.GetProgram();
                    break;
                case "夏米尔35P 没有C":
                    _deviceType = "35P_WithoutC";
                    _p = new EDMProgram(_EDMItems, _deviceType, RaiseHeight, PieceCount, SelectFirst);
                    _p.GetProgram();
                    break;
                case "夏米尔350选择跑位":
                    _deviceType = "350_WithC";
                    _p = new EDMProgram(_EDMItems, _deviceType, RaiseHeight, PieceCount, SpecElePosList, SelectFirst);
                    _p.GetProgram();
                    break;
                case "夏米尔23 选择跑位":
                    _deviceType = "35P_WithC";
                    _p = new EDMProgram(_EDMItems, _deviceType, RaiseHeight, PieceCount, SpecElePosList, SelectFirst);
                    _p.GetProgram();
                    break;
            }
            return _p.str_Programme.Replace("\\r\\n", "\r\n");
        }


        public List<SpecElePos> GetPositionList(List<string> EleList = null,
            List<string> PosList = null)
        {
            List<SpecElePos> _elePos = new List<SpecElePos>();
            List<string> _eleList = EleList.Distinct().ToList();

            foreach (string _eleName in _eleList)
            {
                SpecElePos _pos = new SpecElePos();
                _pos.ELEName = _eleName;
                _pos.ELESpecPosList = new List<string>();
                for (int i = 0; i < EleList.Count; i++)
                {
                    if (EleList[i] == _eleName)
                    {
                        _pos.ELESpecPosList.Add(PosList[i]);
                    }
                }
                _elePos.Add(_pos);
            }
            return _elePos;
        }

        private int GetSValue(EDMItemViewModel _viewItem, string MachineName)
        {

            string _machine;

            if (MachineName.IndexOf("350") >= 0)
            {
                _machine = "350";
            }
            else
            {
                _machine = "35";
            }

            double _gap;
            string _type;

            if (_viewItem.EleType == 0)
            {
                _gap = _viewItem.Gap + _viewItem.GapCompensate - 0.02;
                _type = "R";
            }
            else
            {
                _gap = _viewItem.Gap + _viewItem.GapCompensate;
                _type = "F";
            }
            Charmill _charmill = _charmillRepository.Charmills.Where(c => c.Material == _viewItem.Material)
                .Where(c => c.Max_Gap >= _gap).Where(c => c.Min_Gap <= _gap).Where(c => c.Surface == _viewItem.Surface)
                .Where(c => c.Name == _machine).Where(c => c.Type.Contains(_type)).FirstOrDefault();
            if (_charmill != null)
            {
                return _charmill.Program_Number;
            }
            else
            {
                return 0;
            }
        }

        //public ActionResult JsonTasks(string TaskIDs)
        //{
        //    string[] _taskIDs = TaskIDs.Split(',');
        //    List<Task> _tasks = new List<Task>();
        //    List<EDMDetail> _edmDetail = new List<EDMDetail>();
        //    for (int i = 0; i < _taskIDs.Length; i++)
        //    {
        //        _tasks.Add(_taskRepository.QueryByTaskID(Convert.ToInt32(_taskIDs[i])));
        //    }


        //}

        public string EDMTaskStart(int TaskID)
        {
            try
            {
                Task _task = _taskRepository.QueryByTaskID(TaskID);
                _task.State = (int)CNCStatus.正在加工;
                _task.StartTime = DateTime.Now;
                _taskRepository.Save(_task);
                return "";
            }
            catch
            {
                return "任务开始失败";
            }
        }

        /// <summary>
        /// Create a new cookie value
        /// </summary>
        /// <param name="TaskCookie"></param>
        /// <param name="CookieValue"></param>
        private void AddEDMTaskCookie(HttpCookie TaskCookie, string CookieValue)
        {
            TaskCookie = new HttpCookie("EDMTasks");
            TaskCookie.Values.Add("TaskIDs", CookieValue);
            Response.Cookies.Add(TaskCookie);
        }

        /// <summary>
        /// Update the cookie value
        /// </summary>
        /// <param name="TaskCookie"></param>
        /// <param name="CookieValue"></param>
        private void UpdateEDMTaskCookie(HttpCookie TaskCookie, string CookieValue)
        {
            TaskCookie.Expires = DateTime.Now.AddMinutes(-1);
            AddEDMTaskCookie(TaskCookie, CookieValue);
        }

        /// <summary>
        /// Cookie value add/remove
        /// </summary>
        /// <param name="ExistingValue"></param>
        /// <param name="NewValue"></param>
        /// <param name="Type">
        /// true:Add value
        /// false:Remove value
        /// </param>
        /// <returns></returns>
        private string CookValue(string ExistingValue, string NewValue, bool Type = true)
        {
            List<string> _eValue = new List<string>();
            _eValue.AddRange(ExistingValue.Split(','));
            string[] _nValue = NewValue.Split(',');
            foreach (string _i in _nValue)
            {
                if (!_eValue.Contains(_i))
                {
                    if (Type)
                    {
                        _eValue.Add(_i);
                    }
                    else
                    {
                        _eValue.Remove(_i);
                    }
                }
            }
            string _result = "";
            foreach (string _i in _eValue)
            {
                _result = _result == "" ? _i : _result + "," + _i;
            }
            return _result;
        }


        public ActionResult WEDMProcessList()
        {
            return View();
        }


        #endregion

        #region Data Write


        ///// <summary>
        ///// Create new CAM task
        ///// </summary>
        ///// <param name="CAMTask"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public int SaveCAMTask(CAMTask CAMTask)
        //{
        //    int _camTaskID;

        //    CAMTask.UserID = 0;
        //    CAMTask.DrawingFile = "";
        //    CAMTask.CreateDate = DateTime.Now;
        //    CAMTask.MoldNumber = _projectRepository.Projects.Where(p => p.ProjectID == CAMTask.ProjectID).Select(p => p.MoldNumber).FirstOrDefault();
        //    try
        //    {
        //        _camTaskID = _camTaskRepository.Save(CAMTask);
        //    }
        //    catch
        //    {
        //        _camTaskID = 0;
        //    }

        //    return _camTaskID;
        //}

        ///// <summary>
        ///// Accept the CAM Tasks
        ///// </summary>
        ///// <param name="TaskID"></param>
        ///// <returns></returns>
        //public int AcceptCAMTask(int TaskID)
        //{
        //    int _userID;
        //    try
        //    {
        //        _userID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
        //        int _currentTask = _camTaskRepository.CAMTasks.Where(c => c.UserID == _userID).Where(c => c.State != 2).Count();
        //        if (_currentTask > 0)
        //        {
        //            //Current user has a task in work
        //            return 2;
        //        }
        //        else
        //        {
        //            CAMTask _task = _camTaskRepository.QueryByID(TaskID);
        //            //Check whether the task is accepted
        //            if (_task.UserID == 0)
        //            {
        //                //No one has claimed current task
        //                //Accept the task
        //                _camTaskRepository.Claim(TaskID, _userID);


        //                return 1;
        //            }
        //            else
        //            {
        //                //Task has been claimed already
        //                return 3;
        //            }

        //        }
        //    }
        //    catch
        //    {
        //        //Failed to get current user cookie, so user is a visitor who cannot accept the task
        //        return 99;
        //    }
        //}

        ///// <summary>
        ///// Release the current CAM task
        ///// </summary>
        ///// <param name="TaskID"></param>
        ///// <returns></returns>
        //public int ReleaseCAMTask(int TaskID)
        //{
        //    int _userID;
        //    try
        //    {
        //        _userID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);

        //        _camTaskRepository.Release(TaskID, _userID);
        //        return 1;
        //    }
        //    catch
        //    {
        //        return 99;
        //    }
        //}


        //public int SaveCncPara(CNCParameter CNCPara)
        //{
        //    int _cncParaID = _cncParaRepository.Save(CNCPara);
        //    int _taskID = CNCPara.TaskID;
        //    _taskRepository.PositonFinish(_taskID);
        //    return _cncParaID;
        //}

        //public int SaveEDMPara(EDMItem EDMPara)
        //{
        //    return 0;// _edmItemRepository.Save(EDMPara);
        //}

        public int SaveQCInfo(QCInfo QCInfo)
        {
            int _qcInfoID = _qcInfoRepository.Save(QCInfo);
            int _taskID = QCInfo.TaskID;
            _taskRepository.QCInfoFinish(_taskID);
            return _qcInfoID;

        }



        /// <summary>
        /// Provide one label at a time for printer deamon can get the lable   Updated By michael
        /// </summary>
        /// <returns></returns>
        public string LabelToPrint()
        {
            CNCItem _item = _cncItemRepository.CNCItems.Where(c => c.LabelToPrint == true).Where(c => c.LabelPrinted == false).FirstOrDefault();
            if (_item != null)
            {
                _cncItemRepository.SetPrinted(_item.CNCItemID);
                string LabContent = GetLabelContent(_item.CNCItemID) ?? "";
                LabContent = "\""+LabContent + "\""+ "," + "\""+ _item.LabelName+ "\"";
                return LabContent;
            }
            else
            {
                return "";
            }
        }
        #endregion


        #region Task state change
        /// <summary>
        /// Create Machining task
        /// </summary>
        /// <param name="Task"></param>
        /// <returns></returns>
        [HttpPost]
        public int SaveTask(Task Task)
        {
            int _userID;
            try
            {
                _userID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
            }
            catch
            {
                _userID = 0;
            }
            Task.Creator = _userID;
            Task.CAMUser = _userID;
            Task.CreateTime = DateTime.Now;
            int _taskID = _taskRepository.Save(Task);

            return _taskID;
        }

        /// <summary>
        /// CAM user accept the task 
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public int AcceptTask(int TaskID)
        {
            int _userID;
            try
            {
                _userID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
                int _currentTask = _taskRepository.Tasks.Where(t => t.CAMUser == _userID).Where(c => c.State == 2).Count();
                if (_currentTask > 5)
                {
                    //Current user has a task in work
                    return 2;
                }
                else
                {
                    Task _task = _taskRepository.QueryByTaskID(TaskID);
                    //Check whether the task is accepted
                    if (_task.CAMUser == 0)
                    {
                        //No one has claimed current task
                        //Accept the task
                        _taskRepository.Claim(TaskID, _userID);
                        switch (_task.TaskType)
                        {
                            case 1:
                                //CreateCNCItems(_task);
                                ////if (_task.R > 0)
                                ////{
                                ////    CreateCNCItems(_task.TaskID, _task.R, _task.TaskName, "R", _task.Version, _task.Raw);
                                ////}
                                ////if (_task.F > 0)
                                ////{
                                ////    CreateCNCItems(_task.TaskID, _task.F, _task.TaskName, "F", _task.Version, _task.Raw);
                                ////}
                                //QCInfo _qcInfo = new QCInfo();
                                //_qcInfo.TaskID = TaskID;
                                //_qcInfo.QCPoints = "";
                                //_qcInfoRepository.Save(_qcInfo);
                                break;
                            case 2:
                                //CreateEDMItems(_task.TaskID, _task.Quantity, _task.TaskName);

                                break;
                        }
                        return 1;
                    }
                    else
                    {
                        //Task has been claimed already
                        return 3;
                    }

                }
            }
            catch
            {
                //Failed to get current user cookie, so user is a visitor who cannot accept the task
                return 99;
            }
        }

        #region 电极标签逻辑更新 michael
        /// <summary>
        /// updated by michael 粗电极个位 1，2，3 精电极 4......9
        /// </summary>
        /// <param name="Task"></param>
        public void CreateCNCItems(Task Task)
        {

            CNCItem _cncItem;
            string _seq = "";
            string _ver = Task.Version > 9 ? Task.Version.ToString() : "0" + Task.Version.ToString();
            int _itemID;
            int R_Number = 0;
            int F_Number = 3;
            for (int i = 0; i < Task.R; i++)
            {
                if ((R_Number % 10) == 3)
                {
                    //_seq= (i % 4 + (i / 4) * 10 + 1).ToString();
                    R_Number = R_Number + 8;
                    _seq = R_Number.ToString();
                }
                else
                {
                    //_seq = (i % 4 + (i / 4) * 10 ).ToString();
                    R_Number = R_Number + 1;
                    _seq = R_Number.ToString();
                }
                _cncItem = new CNCItem();
                //_seq = i > 9 ? i.ToString() : "0" + i.ToString();
                string res1 = "0" + _seq;
                res1 = res1.Substring(res1.Length - 2);
                _cncItem.LabelName = Task.TaskName + "/" + _ver + "/" + res1 + "-" + "R";
                _cncItem.Material = Task.Material;

                //_cncItem.Raw = Task.Raw;
                _cncItem.TaskID = Task.TaskID;
                //待测试
                _cncItem.ELE_INDEX = Convert.ToInt32((Task.TaskID.ToString() + _seq));
                //_cncItem.Required = true;
                _cncItem.SafetyHeight = GetSafetyHeight(Task.Raw);
                _cncItem.MoldNumber = Task.MoldNumber;
                _itemID = _cncItemRepository.Save(_cncItem);
            }

            for (int i = 0; i < Task.F; i++)
            {
                if ((F_Number % 10) == 9)
                {
                    //_seq = (i % 7 + (i / 7) * 10 + 1).ToString();
                    F_Number = F_Number + 5;
                    _seq = F_Number.ToString();
                }
                else
                {
                    //_seq = (i % 7 + (i / 7) * 10).ToString();
                    F_Number = F_Number + 1;
                    _seq = F_Number.ToString();
                }
                _cncItem = new CNCItem();
                //_seq = i + 4 > 9 ? (i + 4).ToString() : "0" + (i + 4).ToString();
                string res1 = "0" + _seq;
                res1 = res1.Substring(res1.Length - 2);
                _cncItem.LabelName = Task.TaskName + "/" + _ver + "/" + res1 + "-" + "F";
                _cncItem.Material = Task.Material;
                //_cncItem.Raw = Task.Raw;
                _cncItem.TaskID = Task.TaskID;
                //待测试
                _cncItem.ELE_INDEX = Convert.ToInt32((Task.TaskID.ToString() + _seq));
                //_cncItem.Required = true;
                _cncItem.SafetyHeight = GetSafetyHeight(Task.Raw);
                _cncItem.MoldNumber = Task.MoldNumber;
                _itemID = _cncItemRepository.Save(_cncItem);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LabelName"></param>
        /// <returns></returns>
        public ActionResult GetEleDrawingByELE_Index(string ELE_IndexCode)
        {
            CNCItem _cncItem = _cncItemRepository.QueryByELE_IndexCode(ELE_IndexCode) ?? new CNCItem();
            QCTask _qctask = _qcTaskRepository.QCTasks.Where(q => q.TaskName == _cncItem.LabelName).FirstOrDefault() ?? new QCTask();
            return Json(_qctask, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 扫描标签获得cncItem对象
        /// </summary>
        public JsonResult GetCNCItemsByELE_Index(string ELE_IndexCode)
        {
            //string ELE_IndexCode = "*EI0000018013*";
            CNCItem _cncItem = _cncItemRepository.QueryByELE_IndexCode(ELE_IndexCode) ?? new CNCItem();
            return Json(_cncItem, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 牛哥用
        /// </summary>
        /// <param name="ELE_IndexCode"></param>
        /// <returns></returns>
        public string GetLabelNameByELE_Index(string ELE_IndexCode)
        {
            CNCItem _cncItem = _cncItemRepository.QueryByELE_IndexCode(ELE_IndexCode) ?? new CNCItem();
            return _cncItem.LabelName;
        }
        /// <summary>
        /// 标签内容格式化
        /// </summary>
        public string GetLabelContent(int CncItemID)
        {
            CNCItem _item = _cncItemRepository.QueryByID(CncItemID);
            string ele_IndexStr = _item.ELE_INDEX.ToString();
            string middle = "0000000000" + ele_IndexStr;
            try
            {
                middle = middle.Substring(middle.Length - 10, 10) ?? "";
            }
            catch
            {
                middle = "";
            }
            middle = "*EI" + middle + "*";
            return middle;
        }
        #endregion

        private double GetSafetyHeight(string RawSize)
        {
            try
            {
                string _height = RawSize.Substring(RawSize.IndexOf('(') + 1, RawSize.IndexOf(')') - RawSize.IndexOf('(') - 1);

                return Convert.ToDouble(_height);
            }
            catch
            {
                return 0;
            }

        }


        public void CreateEDMItems(int TaskID, int Count, string TaskName)
        {
            //string _seq = "";
            //for (int i = 1; i < Count + 1; i++)
            //{
            //    EDMItem _edmItem = new EDMItem();
            //    _seq = i.ToString().Length == 1 ? "0" + i.ToString() : i.ToString();

            //    _edmItem.LabelName = TaskName + "/" + _seq;
            //    _edmItem.TaskID = TaskID;
            //    _edmItem.LabelName = TaskName;
            //    _edmItemRepository.Save(_edmItem);
            //}
        }

        //public int SaveEDMItems(EDMItem EDMItem)
        //{
        //    try
        //    {
        //        //_edmItemRepository.Save(EDMItem);
        //        return 0;
        //    }
        //    catch
        //    {
        //        return 1;
        //    }
        //}

        public void CreateQCInfo(int TaskID, int ItemID)
        {
            QCInfo _qcInfo = new QCInfo();
            _qcInfo.TaskID = TaskID;
            _qcInfo.ItemID = ItemID;
            _qcInfo.QCPoints = "";
            _qcInfoRepository.Save(_qcInfo);
        }



        /// <summary>
        /// Release machining task
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public int ReleaseTask(string TaskID)
        {
            string[] _taskIDs = TaskID.Split(',');
            int _result = 1;
            for (int i = 0; i < _taskIDs.Length; i++)
            {


                try
                {
                    int _userID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);

                    Task _task = _taskRepository.QueryByTaskID(Convert.ToInt32(_taskIDs[i]));
                    //CAMUser is not current user
                    //if (_userID != _task.CAMUser)
                    //{
                    //    return 10;
                    //}

                    //Task positon information is not filled
                    if ((_task.PositionFinish == false) && (_task.TaskType == 1))
                    {
                        return 11;
                    }

                    //Task QC information is not filled
                    if ((_task.TaskType == 1) || (_task.TaskType == 2))
                    {
                        if (_task.QCInfoFinish == false)
                        {
                            return 12;
                        }
                    }

                    if (_task.DrawingFile == "")
                    {
                        return 13;
                    }
                    #region Release Task
                    //铣铁任务 发布 更新MGsetting State
                    if (_task.TaskType == 4)
                    {
                        MGSetting _mgsetting = _mgSettingRepository.QueryByTaskID(_task.TaskID);
                        if (_mgsetting != null)
                        {
                            _mgsetting.State = (int)MGSettingStatus.任务发布;
                            _mgSettingRepository.Save(_mgsetting);
                        }
                    }
                    _taskRepository.Release(Convert.ToInt32(_taskIDs[i]));
                    #endregion
                    #region Create CNCItems
                    if (_task.R > 0 || _task.F > 0)
                    {
                        CreateCNCItems(_task);
                    }
                    #endregion
                    #region 电极任务 创建qc任务、CNCMachInfo
                    if (_task.TaskType == 1)
                    {
                        IEnumerable<CNCItem> _items = _cncItemRepository.QueryByTaskID(_task.TaskID);
                        foreach (CNCItem _item in _items)
                        {
                            QCTask _qctask = new QCTask();
                            _qctask.TaskID = _task.TaskID;
                            _qctask.DrawingFile = _task.DrawingFile+".pdf";
                            _qctask.TaskName = _item.LabelName;
                            _qctask.TaskType = 1;
                            _qctask.Version = _task.Version;
                            _qctask.Memo = _task.Memo;
                            _qctask.State = 0;
                            _qctask.ProjectID = _task.ProjectID;
                            _qctask.MoldNumber = _task.MoldNumber;
                            _qcTaskRepository.Save(_qctask);
                        }
                        CNCMachInfo _machinfo = _machInfoRepository.QueryByNameVersion(_task.TaskName, _task.Version);
                        if (_machinfo != null)
                        {
                            _machinfo.Lock = 1;
                            _machInfoRepository.Save(_machinfo);
                        }
                    }
                    #endregion
                    else
                    {
                        #region EDM任务 锁定EDMDetail数据 
                        if (_task.TaskType == 2)
                        {
                            _edmDetailRepository.Lock(_task.TaskID, _task.TaskName, _task.Version);
                        }
                        #endregion
                        try
                        {
                            if (_qcTaskRepository.QueryByTaskID(_task.TaskID) != null)
                            {
                                QCTask _qctask = _qcTaskRepository.QueryByTaskID(_task.TaskID);
                                _qctask.State = 0;
                                _qcTaskRepository.Save(_qctask);
                            }
                            
                        }
                        catch
                        {

                        }

                        #region 铣铁CNC 锁定SteelCAMDrawing
                        if (_task.TaskType == 4)
                        {
                            SteelCAMDrawing _camdrawing = _steelCAMDrawingRepository.QueryByNameVersion(_task.TaskName, _task.Version);
                            if (_camdrawing != null)
                            {
                                _camdrawing.DrawLock = true;
                                _steelCAMDrawingRepository.Save(_camdrawing);
                            }
                        }
                        #endregion
                    }
                }
                catch(Exception ex)
                {
                    _result= - 1;
                }
            }
            return _result;
        }

        public bool SetTaskPriority(string TaskIDs, int Level)
        {
            bool _succ = true;
            try
            {
                if (TaskIDs.IndexOf(',') > 0)
                {
                    string[] _ids = TaskIDs.Split(',');
                    for (int i = 0; i < _ids.Length; i++)
                    {
                        _taskRepository.Priority(Convert.ToInt32(_ids[i]), Level);
                    }
                }
                else
                {
                    _taskRepository.Priority(Convert.ToInt32(TaskIDs), Level);
                }
            }
            catch
            {
                _succ = false;
            }
            return _succ;
        }

        /// <summary>
        /// Add current project to prepare queue
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public string QueueTask(string TaskIDs)
        {
            string[] _ids;
            if (TaskIDs.IndexOf(",") > 0)
            {
                _ids = TaskIDs.Split(',');
            }
            else
            {
                _ids = new string[1];
                _ids[0] = TaskIDs;
            }
            string _msg = "";
            for (int i = 0; i < _ids.Length; i++)
            {
                try
                {
                    Task _task = _taskRepository.QueryByTaskID(Convert.ToInt32(_ids[i]));
                    if (_task.State == 3)
                    {
                        _taskRepository.Queue(Convert.ToInt32(_ids[i]));
                    }
                    else
                    {
                        _msg = _msg + _task.TaskName + ",";
                    }
                }
                catch
                {

                }

            }
            if (_msg != "")
            {
                _msg = "非发布状态的任务无法设置为等待加工:" + _msg;
            }
            return _msg;
        }

        /// <summary>
        /// Start the task
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public int StartTask(int TaskID)
        {
            return 0;
        }

        /// <summary>
        /// Stop the task and set the state to accepted
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public int StopTask(int TaskID)
        {
            try
            {
                _taskRepository.Stop(TaskID);
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public string PauseTask(int TaskID)
        {

            Task _task = _taskRepository.QueryByTaskID(TaskID);
            int _type = _task.TaskType;
            int _state = _task.State;
            if (_type == 2)
            {
                return "无法停止EDM任务";
            }
            else
            {
                if (_state >= 0)
                {
                    #region 暂停任务 更新状态
                    _taskRepository.Pause(TaskID);
                    #endregion
                    #region 结束工时
                    EndTaskHour(TaskID);
                    #endregion
                    return "任务暂停成功";
                }
                else
                {
                    if (_state == -1)
                    {
                        #region 重启任务工时记录
                        //目前只有正在加工(10)是重启记录 
                        if (_task.PrevState==10)
                        {
                            CreateTaskHour(_task, 1,_taskHourRepository.GetMachineByTask(_task.TaskID));
                        }
                        #endregion
                        _taskRepository.UnPause(TaskID);
                        return "任务开始成功";
                    }
                    else
                    {
                        return "无法暂停已开始的任务";
                    }

                }
            }         
        }

        public string DeleteTaskByCAM(int TaskID)
        {
            try
            {
                Task _task = _taskRepository.QueryByTaskID(TaskID);

                if (OutSourceState(TaskID))
                {
                    _task.State = (int)CNCStatus.CAM取消;
                    string memo = GetCurrentUser() + "取消任务，取消时间 " + DateTime.Now.ToString("yyMMddHHmm");
                    _task.StateMemo = _task.StateMemo == null ? memo : _task.StateMemo + ";" + memo;
                    _task.Enabled = false;
                    _taskRepository.Save(_task);
                    #region 取消 任务工时
                    CancelTaskHour(_task.TaskID);
                    #endregion
                    return "任务已取消";
                }
                else
                {
                    return "任务正在外发中，无法取消";
                }

            }
            catch
            {
                return "任务取消失败，请重试";
            }
        }


        public string DeleteTaskByCNC(string TaskIDs, string Memo)
        {
            string[] _taskIDs = TaskIDs.Split(',');
            string _msg = "";
            string _userName = GetCurrentUser();

            for (int i = 0; i < _taskIDs.Length; i++)
            {
                Task _task = _taskRepository.QueryByTaskID(Convert.ToInt32(_taskIDs[i]));
                if (_task != null)
                {
                    //处于等待状态的任务才允许CAM删除
                    bool _canDel = false;
                    int _nextState = 0;
                    switch (_task.TaskType)
                    {
                        case 1:
                            if (_task.State == (int)CNCStatus.等待)
                            {
                                _nextState = (int)CNCStatus.CNC删除;
                                _canDel = true;
                            }
                            break;
                        case 4:
                            if (_task.State == (int)SteelStatus.等待)
                            {
                                _nextState = (int)SteelStatus.CNC删除;
                                _canDel = true;
                            }
                            break;
                        default:
                            break;
                    }
                    if (_canDel)
                    {
                        #region 删除任务
                        _task.State = _nextState;
                        _task.Enabled = false;
                        _task.StateMemo = _userName + "删除于" + DateTime.Now.ToString("yyyy-MM-dd hh:mm") + "\r\n" + _task.StateMemo;
                        _taskRepository.Save(_task);
                        #endregion
                        //#region 取消任务工时  等待状态的任务不存在工时记录
                        //CancelTaskHour(_task.TaskID);
                        //#endregion
                    }
                    else
                    {
                        _msg = _msg == "" ? _task.TaskName : _msg + "," + _task.TaskName;
                    }
                }
            }
            return _msg;
        }


        public string DeleteTaskByEDM(string TaskIDs)
        {
            string[] _taskIDs = TaskIDs.Split(',');
            string _msg = "";
            string _userName = GetCurrentUser();

            for (int i = 0; i < _taskIDs.Length; i++)
            {
                Task _task = _taskRepository.QueryByTaskID(Convert.ToInt32(_taskIDs[i]));
                if (_task != null)
                {

                    //处于等待状态的任务才允许删除
                    bool _canDel = false;
                    switch (_task.TaskType)
                    {
                        case 2:
                            if ((_task.State == (int)EDMStatus.等待) || (_task.State == (int)EDMStatus.等待中))
                            {
                                _canDel = true;
                            }
                            break;
                    }
                    if (_canDel)
                    {
                        #region 删除EDM任务
                        _task.Enabled = false;
                        _task.State = (int)EDMStatus.EDM删除;
                        _task.StateMemo = _userName + "删除于" + DateTime.Now.ToString("yyyy-MM-dd hh:mm") + "\r\n" + _task.StateMemo;
                        _taskRepository.Save(_task);
                        #endregion
                    }
                    else
                    {
                        _msg = _msg == "" ? _task.TaskName : _msg + "," + _task.TaskName;
                    }
                }
            }
            return _msg;
        }


        public string GetCurrentUser()
        {
            try
            {
                int _userID = Convert.ToInt16(Request.Cookies["User"]["UserID"]);
                User _user = _userRepository.GetUserByID(_userID) ?? new User();
                return _user.FullName;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Set the task to finish state;
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public int FinishTask(int TaskID)
        {
            try
            {
                _taskRepository.Finish(TaskID, GetCurrentUser());
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public string AddToPrintQueue(string ItemIDs)
        {
            string _result = "";
            string _item = "";
            int _pos = Convert.ToInt16(Request.Cookies["User"]["Position"]);
            int _dept = Convert.ToInt16(Request.Cookies["User"]["Department"]);
            bool _reprint = (_dept == 7) && (_pos == 3);
            
            try
            {
                if (ItemIDs.IndexOf(",") > 0)
                {
                    string[] _id = ItemIDs.Split(',');
                    for (int i = 0; i < _id.Length; i++)
                    {
                        _item = _cncItemRepository.SetToPrint(Convert.ToInt32(_id[i]), _reprint);
                        if (_item != "")
                        {
                            _result = _result == "" ? _item : _result + "," + _item;
                        }
                    }
                }
                else
                {
                    _item = _cncItemRepository.SetToPrint(Convert.ToInt32(ItemIDs), _reprint);
                    _result = _item;
                }

                return _result;
            }
            catch
            {
                return "打印失败";
            }
        }

        public string OutSource(string TaskIDs)
        {
            string msg = "";
            //string _memo = "";
            try
            {
                string[] _ids;
                if (TaskIDs.IndexOf(",") > 0)
                {
                    _ids = TaskIDs.Split(',');

                }
                else
                {
                    _ids = new string[1];
                    _ids[0] = TaskIDs;
                }

                for (int i = 0; i < _ids.Length; i++)
                {
                    int _taskID = Convert.ToInt32(_ids[i]);
                    Task _task = _taskRepository.QueryByTaskID(_taskID);
                    bool _canOutSource = false;
                    List<int> _types = new List<int> { (int)CNCStatus.等待, (int)CNCStatus.等待中, (int)CNCStatus.已接收 };
                    //switch (_task.TaskType)
                    //{
                    //    case 1:
                    //        if (_types.Contains(_task.State ))
                    //        {
                    //            _canOutSource = true;
                    //        }
                    //        break;
                    //    case 2:
                    //        if (_task.State == (int)CNCStatus.等待)
                    //        {
                    //            _canOutSource = true;
                    //        }
                    //        break;
                    //    case 3:
                    //        if ((_task.State == (int)WEDMStatus.已接收) || (_task.State == (int)WEDMStatus.等待))
                    //        {
                    //            _canOutSource = true;
                    //        }
                    //        break;
                    //    case 4:
                    //        if ((_task.State == (int)SteelStatus.等待)||(_task.State==(int)SteelStatus.已接收))
                    //        {
                    //            _canOutSource = true;
                    //        }
                    //        break;
                    //    case 6:
                    //        if (_task.State == (int)GrindStatus.等待)
                    //        {
                    //            _canOutSource = true;
                    //        }
                    //        break;
                    //    default:
                    //        break;
                    //}
                    if (_types.Contains(_task.State))
                    {
                        _canOutSource = true;
                    }
                    if (!_canOutSource)
                    {
                        msg = msg + _task.TaskName + ",";
                    }
                }
            }
            catch
            {

            }
            if (msg != "")
            {
                msg = "以下任务不处于等待状态，无法进行外发" + msg;
            }
            return msg;
        }
        public string StartWEDMTask(string TaskIDs)
        {
            int[] _ids = Array.ConvertAll<string, int>(TaskIDs.Split(), s => int.Parse(s));
            try
            {
                foreach (int _id in _ids)
                {
                    Task _task = _taskRepository.QueryByTaskID(_id);
                    _task.State = (int)CNCStatus.正在加工;
                    _task.StartTime = DateTime.Now;
                    _taskRepository.Save(_task);
                }
                return "";
            }
            catch
            {
                return "部分任务开始失败,请重试";
            }
        }
        public string StartGrindTask(string TaskIDs)
        {
            int[] _ids = Array.ConvertAll<string, int>(TaskIDs.Split(), s => int.Parse(s));
            try
            {
                foreach (int _id in _ids)
                {
                    Task _task = _taskRepository.QueryByTaskID(_id);
                    _task.State = (int)CNCStatus.正在加工;
                    _task.StartTime = DateTime.Now;
                    _taskRepository.Save(_task);
                }
                return "";
            }
            catch
            {
                return "部分任务开始失败,请重试";
            }
            
        }
        [HttpPost]
        public string Service_StartTask(List<SetupTaskStart> _setupTS)
        {
            //int[] _ids;
            //if (TaskIDs.IndexOf(',')>0)
            //    _ids = Array.ConvertAll<string, int>(TaskIDs.Split(','), s => int.Parse(s));
            //else
            //    _ids = new int[] { Convert.ToInt32(TaskIDs) };
            if (_setupTS != null)
            {
                DateTime _iniTime = DateTime.Parse("1900/1/1");
                try
                {
                    //Task _task = _taskRepository.QueryByTaskID(_id);
                    //if (_task.State == (int)CNCStatus.正在加工)
                    //{
                    //    return "已有任务处于加工状态！";
                    //}
                    foreach (var s in _setupTS)
                    {
                        Task _task = _taskRepository.QueryByTaskID(s.TaskID);
                        _task.State = (int)CNCStatus.正在加工;
                        _task.StartTime = DateTime.Now;
                        _taskRepository.Save(_task);
                        #region 任务工时
                        CreateTaskHour(_task, 0, s.MachinesCode, s.UserName);
                        #endregion
                    }
                    return "";
                }
                catch
                {
                    return "部分任务开始失败,请重试";
                }               
            }
            return "系统没有接收到任何数据，请重新选择需要开始的任务！";
        }

        public string CancelOutSource(string TaskIDs)
        {
            string msg = "";
            string msg1 = "";
            try
            {
                string[] _id;
                if (TaskIDs.IndexOf(",") > 0)
                {
                    _id = TaskIDs.Split(',');
                }
                else
                {
                    _id = new string[1];
                    _id[0] = TaskIDs;
                }
                for (int i = 0; i < _id.Length; i++)
                {
                    bool _cancelOutsource = false;
                    int _taskID = Convert.ToInt32(_id[i]);
                    Task _task = _taskRepository.QueryByTaskID(_taskID);
                    if (OutSourceState(_task.TaskID))
                    {
                        #region 更新Flag _cancelOutsource
                        switch (_task.TaskType)
                        {
                            case 1:
                                if (_task.State == (int)CNCStatus.外发)
                                {
                                    _task.State = (int)CNCStatus.等待;
                                    _cancelOutsource = true;
                                }
                                break;
                            case 2:
                                if (_task.State == (int)EDMStatus.外发)
                                {
                                    _task.State = (int)EDMStatus.等待;
                                    _cancelOutsource = true;
                                }
                                break;
                            case 3:
                                if (_task.State == (int)WEDMStatus.外发)
                                {
                                    _task.State = (int)WEDMStatus.等待;
                                    _cancelOutsource = true;
                                }
                                break;
                            case 4:
                                if (_task.State == (int)SteelStatus.外发)
                                {
                                    _task.State = (int)SteelStatus.等待;
                                    _cancelOutsource = true;
                                }
                                break;
                            case 6:
                                if (_task.State == (int)GrindStatus.外发)
                                {
                                    _task.State = (int)GrindStatus.等待;
                                    _cancelOutsource = true;
                                }
                                break;
                            default:
                                break;
                        }
                        #endregion
                        if (_cancelOutsource)
                        {
                            _taskRepository.Save(_task);
                        }
                        else
                        {
                            msg = msg + _task.TaskName + ",";
                        }
                    }
                    else
                    {
                        msg1 = msg1 == "" ? _task.TaskName : msg1 + "," + _task.TaskName;
                    }
                }

            }
            catch
            {

            }
            if (msg != "")
            {
                msg = "以下任务不处于外发状态：" + msg;
            }

            if (msg1 != "")
            {
                msg1 = "以下任务已经处在采购流程中" + msg1;
            }
            return msg + msg1;
        }

        public bool OutSourceState(int TaskID)
        {
            PRContent _prContent = _prContentRepository.PRContents
                .Where(p => p.TaskID == TaskID)
                .Where(p => p.Enabled == true)
                .FirstOrDefault();
            if (_prContent != null)
            {
                PurchaseRequest _pr = _prRepository.GetByID(_prContent.PurchaseRequestID);
                //PR未批准情况下对PR内容和PR进行处理
                if (_pr.State < 3)
                {
                    //删除PR内容
                    _prContent.Enabled = false;
                    _prContentRepository.Save(_prContent);

                    //PR没有内容时删除PR
                    if (_prContentRepository.QueryByRequestID(_prContent.PurchaseRequestID).Count() == 0)
                    {
                        _pr.Enabled = false;
                        _prRepository.Save(_pr);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public bool PrintedItem(int ItemID)
        {
            try
            {
                _cncItemRepository.SetPrinted(ItemID);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Json
        //TODO: 加工界面Table Json数据源
        //Retrieve the json task list
        public ActionResult JsonMachineTasks(string MoldNumber = "", int TaskType = 1, int State = 1, int CAM = 0, string Keyword = "")
        {

            IEnumerable<Task> _tasks = _taskRepository.QueryByMoldNumber(MoldNumber, TaskType);
            string _camDrawingPath = "";
            //ProjectPhase _projectPhase = null;
            //string PlanDate = "";
            #region 图纸 历史；加工 当前
            if (State == 0)
            {
                #region 加工当前
                if (CAM == 0)
                {
                    switch (TaskType)
                    {
                        case 1:
                            //List<int> _states = new List<int>();
                            //_states.Add((int)CNCStatus.暂停);
                            //_states.Add((int)CNCStatus.等待);
                            //_states.Add((int)CNCStatus.返工);
                            //_states.Add((int)CNCStatus.正在加工);
                            //_states.Add((int)CNCStatus.外发);
                            //_tasks = _tasks.Where(t => (_states.Contains(t.State)));
                            _tasks = _tasks.Where(t => t.State >= (int)CNCStatus.暂停 && t.State < (int)CNCStatus.CNC结束);
                            break;
                        case 4:
                            //_tasks = _tasks.Where(t => t.State == (int)SteelStatus.等待)
                            //    .Union(_tasks.Where(t => t.State == (int)SteelStatus.正在加工))
                            //    .Union(_tasks.Where(t => t.State == (int)SteelStatus.外发))
                            //    .Union(_tasks.Where(t => t.State == (int)SteelStatus.已接收))
                            //    .Union(_tasks.Where(t => t.State == (int)SteelStatus.暂停))
                            //    .Union(_tasks.Where(t => t.State == (int)SteelStatus.CNC结束));
                            _tasks = _tasks.Where(t => t.State >= (int)CNCStatus.暂停 && t.State < (int)CNCStatus.CNC结束);
                            break;
                        default:
                            _tasks = _tasks.Where(t => t.State >= (int)CNCStatus.暂停 && t.State < (int)CNCStatus.完成);
                            break;
                    }
                }
                #endregion
                #region 图纸历史
                else
                {
                    _tasks = _tasks.Where(t => t.State > (int)CNCStatus.未发布 &&  t.State < (int)CNCStatus.完成);
                }
                #endregion
            }
            #endregion
            #region 加工 历史
            else if (State > 0)
            {
                
                switch (TaskType)
                {
                    case 1:
                        _tasks = _tasks.Where(t => t.State >= (int)CNCStatus.CNC结束);
                        break;
                    case 4:
                        _tasks = _tasks.Where(t => t.State >= (int)CNCStatus.CNC结束);
                        break;
                    default:
                        _tasks = _tasks.Where(t => t.State >= (int)CNCStatus.完成);
                        break;
                }
            }
            #endregion
            #region 图纸 当前
            else
            {
                _tasks = _tasks.Where(t => t.State == (int)CNCStatus.未发布);
            }
            #endregion
            if(TaskType==6)
                _camDrawingPath= GetTaskDrawingPath("CAD");
            else
                _camDrawingPath= GetTaskDrawingPath();
            if (Keyword != "")
            {
                _tasks = _tasks.Where(t => t.TaskName.ToUpper().Contains(Keyword.ToUpper()));
            }
            _tasks = _tasks.OrderBy(t => t.Priority);
            TaskGridViewModel _viewModel = new TaskGridViewModel(_tasks.OrderBy(t => t.Priority), _userRepository, _camDrawingPath,
                _machInfoRepository, _edmDetailRepository, _taskRepository, _projectPhaseRepository,_mgSettingRepository,_wedmSettingRepository,_taskHourRepository);
            return Json(_viewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonTaskBasic(int TaskID)
        {
            Task _task = _taskRepository.QueryByTaskID(TaskID);
            return Json(_task, JsonRequestBehavior.AllowGet);
        }
        

        public ActionResult JsonCNCTaskItems(string TaskIDs)
        {
            List<CNCItem> _items = new List<CNCItem>();
            List<Task> _tasks = new List<Task>();

            if (TaskIDs.IndexOf(',') > 0)
            {
                string[] _ids = TaskIDs.Split(',');
                int _taskID = 0;
                for (int i = 0; i < _ids.Length; i++)
                {
                    _taskID = Convert.ToInt32(_ids[i]);
                    IEnumerable<CNCItem> _cncItems = _cncItemRepository
                        .QueryByTaskID(_taskID);
                    _items.AddRange(_cncItems);
                }
                _tasks.Add(_taskRepository.QueryByTaskID(_taskID));
            }
            else
            {
                IEnumerable<CNCItem> _cncItems = _cncItemRepository.QueryByTaskID(Convert.ToInt32(TaskIDs));
                _items.AddRange(_cncItems);
                Task _task = _taskRepository.QueryByTaskID(Convert.ToInt32(TaskIDs));
                _tasks.Add(_task);
            }

            CNCItemGridViewModel _model = new CNCItemGridViewModel(_items, _tasks);


            return Json(_model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonCNCItems(int TaskID)
        {
            IEnumerable<CNCItem> _cncItems = _cncItemRepository.QueryByTaskID(TaskID);
            return Json(_cncItems, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Public Class QcImportResultForm
        /// Private Sub Search_Scaner_Ele(ByVal strID As String)
        /// </summary>
        /// <param name="CNCItemID"></param>
        /// <returns></returns>
        public ActionResult JsonCNCItem(int CNCItemID)
        {
            CNCItem _cncItem = _cncItemRepository.QueryByID(CNCItemID);
            return Json(_cncItem, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonCNCItemLabel(string LabelName)
        {
            CNCItem _cncitem = _cncItemRepository.QueryByLabel(LabelName);
            return Json(_cncitem, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonCNCPara(int TaskID)
        {
            //CNCParameter _cncpara = _cncParaRepository.QueryByTaskID(TaskID);
            //if (_cncpara == null)
            //{
            //    _cncpara = new CNCParameter();
            //}
            //return Json(_cncpara, JsonRequestBehavior.AllowGet);
            return null;
        }


        //public ActionResult JsonEDMPara(int TaskID)
        //{
        //    EDMItem _edmPara = _edmItemRepository.QueryByTaskID(TaskID);
        //    return Json(_edmPara, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult JsonEDMItems(int TaskID)
        {
            Task _task = _taskRepository.QueryByTaskID(TaskID);
            IEnumerable<CNCItem> _eleItems = _cncItemRepository.QueryByTaskID(TaskID);
            CNCMachInfo _eleMachInfoes = _machInfoRepository.QueryByNameVersion(_task.Model, _task.Version);
            //IEnumerable<EDMItem> _items = _edmItemRepository.QueryByTaskID(TaskID);
            List<EDMItemViewModel> _edmItems = new List<EDMItemViewModel>();
            foreach (CNCItem _item in _eleItems)
            {
                EDMItemViewModel _edmItem = new EDMItemViewModel();
                _edmItem.ID = _item.CNCItemID;
                _edmItem.ELEName = _eleMachInfoes.Model;
                _edmItem.LableName = _item.LabelName;
                //_edmItem.Position = _eleMachInfoes.Position;
                _edmItem.Gap = _item.Gap;
                _edmItem.OffsetX = _item.OffsetX;
                _edmItem.OffsetY = _item.OffsetY;
                _edmItem.OffsetZ = _item.OffsetZ;
                _edmItem.OffsetC = _item.OffsetC;
                _edmItem.GapCompensate = _item.GapCompensation;
                _edmItem.ZCompensate = _item.ZCompensation;
                _edmItem.Surface = _eleMachInfoes.Surface;
                _edmItem.Obit = _eleMachInfoes.ObitType;
                _edmItem.Material = _item.Material;
                _edmItem.ElePoints = _eleMachInfoes.Position;
                _edmItem.EleType = Convert.ToInt32(_item.EleType);
                _edmItem.StockGap = Convert.ToDouble(_eleMachInfoes.EDMStock);
                _edmItem.CNCMachMethod = _eleMachInfoes.CNCMethod;
                _edmItems.Add(_edmItem);
            }
            return Json(_edmItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonEDMItem(int EDMItemID)
        {

            //EDMItem _item = _edmItemRepository.QueryByID(EDMItemID);
            return View();//Json(_item, JsonRequestBehavior.AllowGet);
        }

        //Get all EDMItems if the related EDM task in in waiting state(4)
        public ActionResult JsonEDMItemList()
        {
            IEnumerable<int> _taskIDs = _taskRepository.QueryByState(2, 4, 0).Select(t => t.TaskID);
            List<EDMItem> _currentTasks = new List<EDMItem>();
            foreach (int _id in _taskIDs)
            {
                //_currentTasks.AddRange(_edmItemRepository.QueryByTaskID(_id));
            }

            return View();// Json(_currentTasks, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonQCInfo(int TaskID)
        {
            QCInfo _qcInfo = _qcInfoRepository.QueryByTaskID(TaskID);
            return Json(_qcInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonProcesses(int TaskType)
        {
            IEnumerable<Task> _tasks = _taskRepository.QueryByState(TaskType);
            return Json(_tasks, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonEmpty()
        {
            IEnumerable<EDMItem> _items = null;
            return Json(_items, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Dialogs
        public ActionResult CncTaskDialog(int TaskID = 0)
        {
            if (TaskID == 0)
            {
                return PartialView();
            }
            else
            {
                Task _task = _taskRepository.QueryByTaskID(TaskID);
                return PartialView(_task);
            }
        }

        public ActionResult ScanBarCodeDialog()
        {
            return PartialView();
        }

        public ActionResult EdmTaskDialog(int TaskID = 0)
        {
            if (TaskID == 0)
            {
                return PartialView();
            }
            else
            {
                Task _task = _taskRepository.QueryByTaskID(TaskID);
                return PartialView(_task);
            }
        }

        public ActionResult ELECompensationDialog()
        {

            return PartialView();
        }

        public ActionResult WedmTaskDialog(int TaskID = 0)
        {
            if (TaskID == 0)
            {
                return PartialView();
            }
            else
            {
                Task _task = _taskRepository.QueryByTaskID(TaskID);
                return PartialView(_task);
            }
        }

        public ActionResult GrindTaskDialog(int TaskID = 0)
        {
            if (TaskID == 0)
            {
                return PartialView();
            }
            else
            {
                Task _task = _taskRepository.QueryByTaskID(TaskID);
                return PartialView(_task);
            }
        }


        public ActionResult MillingTaskDialog(int TaskID = 0)
        {
            if (TaskID == 0)
            {
                return PartialView();
            }
            else
            {
                Task _task = _taskRepository.QueryByTaskID(TaskID);
                return PartialView(_task);
            }
        }

        public ActionResult CNCParaDialog()
        {
            return PartialView();
        }

        public ActionResult EDMParaDialog()
        {

            return PartialView();
        }

        public ActionResult QCInfoDialog()
        {
            return PartialView();
        }

        public ActionResult CNCItemList(string TaskIDs, int Type = 0)
        {
            ViewBag.TaskIDs = TaskIDs;
            ViewBag.Type = 0;
            return View();
        }

        public string CNCItemReady(string CNCItemIDs)
        {
            string[] _ids;
            if (CNCItemIDs.IndexOf(',') > 0)
            {
                _ids = CNCItemIDs.Split(',');
            }
            else
            {
                _ids = new string[1];
                _ids[0] = CNCItemIDs;
            }
            for (int i = 0; i < _ids.Length; i++)
            {
                _cncItemRepository.SetReady(Convert.ToInt32(_ids[i]));
            }
            return "";
        }

        public string CNCItemRequired(string CNCItemIDs)
        {
            string[] _ids;
            if (CNCItemIDs.IndexOf(',') > 0)
            {
                _ids = CNCItemIDs.Split(',');
            }
            else
            {
                _ids = new string[1];
                _ids[0] = CNCItemIDs;
            }
            for (int i = 0; i < _ids.Length; i++)
            {
                _cncItemRepository.SetRequired(Convert.ToInt32(_ids[i]));
            }
            return "";
        }


        public string CNCItemNotRequired(string CNCItemIDs)
        {
            string[] _ids;
            if (CNCItemIDs.IndexOf(',') > 0)
            {
                _ids = CNCItemIDs.Split(',');
            }
            else
            {
                _ids = new string[1];
                _ids[0] = CNCItemIDs;
            }
            for (int i = 0; i < _ids.Length; i++)
            {
                _cncItemRepository.SetUnRequired(Convert.ToInt32(_ids[i]));
            }
            return "";
        }


        public bool ValidateTaskUser(int TaskID)
        {
            int currentUserID = 0;
            try
            {
                currentUserID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
            }
            catch
            {

            }

            Task _task = _taskRepository.QueryByTaskID(TaskID);
            if (_task.CAMUser == currentUserID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateTaskAvaliable(int TaskID)
        {
            int _taskCAMUser = _taskRepository.QueryByTaskID(TaskID).CAMUser;
            if (_taskCAMUser == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// 零件入库
        /// </summary>
        /// <param name="TaskID"></param>
        public void TaskInStock(int TaskID)
        {
            Task _task = _taskRepository.QueryByTaskID(TaskID);
            switch (_task.TaskType)
            {
                case 1://电极任务
                    IEnumerable<CNCItem> _items = _cncItemRepository.QueryByTaskID(TaskID);
                    foreach (CNCItem _cncItem in _items)
                    {
                        //WarehouseStock _stock =new WarehouseStock();
                        //_stock.Name=_cncItem.LabelName;
                        //_stock.Enabled=true;
                        //_stock.Quantity = 1;
                        //_stock.SafeQuantity = 0;
                        //_stock.Specification = _cncItem.Raw;
                        _whStockRepository.EleInStock(_cncItem.LabelName);
                    }
                    break;
                case 3://WEDM任务
                    break;
                case 4://铣铁任务
                    break;
            }
        }





        #endregion

        #region fun
        public string SetCNCFinish(string CNCTaskIDs)
        {
            string msg = "";
            string fail = "";
            string[] _taskIDs;
            if (CNCTaskIDs.IndexOf(',') > 0)
            {
                _taskIDs = CNCTaskIDs.Split(',');
            }
            else
            {
                _taskIDs = new string[1];
                _taskIDs[0] = CNCTaskIDs;
            }

            for (int i = 0; i < _taskIDs.Length; i++)
            {
                int _taskid = Convert.ToInt32(_taskIDs[i]);
                try
                {
                    Task _task = _taskRepository.QueryByTaskID(_taskid);
                    if ((_task.State > 3) && (_task.State < 20))
                    {
                        _taskRepository.Finish(_taskid, GetCurrentUser());
                    }
                    else
                    {
                        msg = msg + _task.TaskName;
                    }

                }
                catch
                {
                    fail = fail + "  任务结束失败";
                }
            }

            for (int i = 0; i < _taskIDs.Length; i++)
            {
                int _taskid = Convert.ToInt32(_taskIDs[i]);
                try
                {
                    TaskInStock(_taskid);
                }
                catch
                {
                    fail = fail + "  电极入库失败";
                }
            }
            if (msg != "")
            {
                msg = "以下任务不是处于加工状态，无法设置为结束：" + msg;
            }
            return msg;
        }

        public string SetTaskFinish(string TaskIDs)
        {
            string msg = "";
            string[] _taskIDs;
            if (TaskIDs.IndexOf(',') > 0)
            {
                _taskIDs = TaskIDs.Split(',');
            }
            else
            {
                _taskIDs = new string[1];
                _taskIDs[0] = TaskIDs;
            }

            for (int i = 0; i < _taskIDs.Length; i++)
            {
                int _taskid = Convert.ToInt32(_taskIDs[i]);
                try
                {
                    #region 工时结束
                    EndTaskHour(_taskid);
                    #endregion
                    _taskRepository.Finish(_taskid, GetCurrentUser());
                }
                catch
                {
                    msg = msg + "  任务结束失败";
                }
            }
            return msg;
        }
        /// <summary>
        /// 外发任务结束
        /// </summary>
        /// <param name="TaskID">任务ID</param>
        /// <param name="Time">工时</param>
        /// <returns></returns>
        public string SetWFTaskFinish(List<SetupTaskStart> _viewmodel)
        {
            string msg = "";
            if (_viewmodel!=null && _viewmodel.Count > 0)
            {
                foreach (var t in _viewmodel)
                {
                    try
                    {
                        decimal _time = 0;
                        if (t.State == "外发")
                            _time = t.TotalTime;
                        #region 工时结束
                        EndTaskHour(t.TaskID, _time);
                        #endregion
                        //FinishBy 暂时记录当前系统用户
                        _taskRepository.Finish(t.TaskID, GetCurrentUser());
                    }
                    catch
                    {
                        Task _task = _taskRepository.QueryByTaskID(t.TaskID);
                        msg = _task.TaskName;
                    }
                }
            }
            return msg;
        }
        public string AcceptMachTask(string TaskIDs)
        {
            string msg = "";
            string[] _taskIDs;
            if (TaskIDs.IndexOf(',') > 0)
            {
                _taskIDs = TaskIDs.Split(',');
            }
            else
            {
                _taskIDs = new string[1];
                _taskIDs[0] = TaskIDs;
            }

            for (int i = 0; i < _taskIDs.Length; i++)
            {
                int _taskid = Convert.ToInt32(_taskIDs[i]);
                try
                {
                    _taskRepository.AcceptItem(_taskid);
                }
                catch
                {
                    msg = msg + "操作失败";
                }
            }
            return msg;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ELE_IndexCode"></param>
        /// <returns></returns>
        public JsonResult ValidateCNCItem(string ELE_IndexCode)
        {
            CNCItem _cncItem = _cncItemRepository.QueryByELE_IndexCode(ELE_IndexCode) ?? new CNCItem();            
            return Json(_cncItem, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CNC加工程序传输
        public string CreateCNCProgram(string EleIds, string Positions, int Device, string Path)
        {
            //new Thread(delegate(){threadHand1_Run(timeStart,timeEnd);});

            try
            {
                if (EleIds != "")
                {

                    string result = "";
                    Thread th1 = new Thread(delegate() { result = SendProgram(EleIds, Positions, Device, Path); });
                    try
                    {

                        
                        th1.SetApartmentState(ApartmentState.STA);
                        th1.IsBackground = true;
                        th1.Start();
                        th1.Join();
                        th1.Abort();
                    }
                    catch
                    {
                        th1.Abort();
                    }
                    return result;

                    //Thread th1 = new Thread(delegate() { SendProgram(EleIds, Positions, Device, Path); });
                    //th1.SetApartmentState(ApartmentState.STA);
                    //th1.Start();
                    //return true;
                }
                else
                {
                    return "Error";
                }
            }
            catch
            {
                return "Error";
            }
        }

        public ActionResult CNCMachines()
        {
            return Json(_machineRepository.Machines.Where(m => m.Enabled == true), JsonRequestBehavior.AllowGet);
        }

        private string SendProgram(string EleIds, string Positions, int Device, string Path)
        {

            ClsCNCProgramme _program = new ClsCNCProgramme();

            CncMachine _machine = CncMachine(Device);

            List<CncEleItem> _items = CNCItems(EleIds, Positions);

            string _taskName = GetTaskNameByEleID(EleIds);

            _program.Create_EleCncPgm(_items, _machine, _taskName);

            string Program = _program.str_Programme.Replace("\\r\\n", "\r\n");

            string DevicdAddr = _machineRepository.QueryByID(Device).IPAddress;

            string ProgramPath = _systemConfigRepository.GetConfigValue("ELEPATH");

            if (ProgramPath.IndexOf("T:") == 0)
            {
                ProgramPath = ProgramPath.Replace("T:", _systemConfigRepository.GetConfigValue("T:"));
            }
            ViewBag.Program = Program;

            //string _item = JsonConvert.SerializeObject(_items);

            _program.Transfer_EleCncPgm("c:\\temp", _items, DevicdAddr, _program.str_Programme, ProgramPath, Path, _taskName);
            Machine _device = _machineRepository.QueryByID(Device);
            if (_program.str_Err == "")
            {
                SetEleStart(EleIds, _device.MachineCode);
            }
            return _program.str_Err.Replace("\\r\\n", "\r\n");
        }

        private void SetEleStart(string EleIDs, string MachineCode)
        {
            string[] _itemIDs = EleIDs.Split(',');
            for (int i = 0; i < _itemIDs.Length; i++)
            {
                CNCItem _cncItem = _cncItemRepository.QueryByID(Convert.ToInt32(_itemIDs[i]));
                _cncItem.Status = (int)CNCStatus.正在加工;
                _cncItem.CNCMachine = MachineCode;
                _cncItem.CNCStartTime = DateTime.Now;
                _cncItemRepository.Save(_cncItem);

                Task _task = _taskRepository.QueryByTaskID(_cncItem.TaskID);
                _task.StartTime = DateTime.Now;
                _task.State = (int)CNCStatus.正在加工;
                _taskRepository.Save(_task);
                #region 任务工时
                CreateTaskHour(_task,0, MachineCode);
                #endregion
            }
        }

        private string GetTaskNameByEleID(string EleIDs)
        {
            string[] _ids = EleIDs.Split(',');
            int _taskID = _cncItemRepository.QueryByID(Convert.ToInt32(_ids[0])).TaskID;
            Task _task = _taskRepository.QueryByTaskID(_taskID);
            return _task.TaskName;
        }

        private CncMachine CncMachine(int MachineID)
        {

            CncMachine _cncMachine = new CncMachine();

            //switch (MachineID)
            //{
            //    case 1:
            //        _cncMachine.input_strIPAddress = "192.168.23.52";
            //        _cncMachine.input_intSystem3R = 1;
            //        _cncMachine.input_intPallet = 2;
            //        break;
            //    case 2:
            //        break;
            //}

            Machine _machine = _machineRepository.QueryByID(MachineID);
            _cncMachine.input_strIPAddress = _machine.IPAddress;
            _cncMachine.input_intSystem3R = _machine.System_3R;
            _cncMachine.input_intPallet = _machine.Pallet;

            return _cncMachine;
        }

        private List<CncEleItem> CNCItems(string EleIds, string Positions)
        {
            string[] ids = EleIds.Split(',');
            string[] pos = Positions.Split(',');
            List<CncEleItem> EleItems = new List<CncEleItem>();
            CncEleItem _eleItem;
            CNCItem _item;
            CNCMachInfo _machInfo;
            for (int i = 0; i < ids.Length; i++)
            {
                _item = _cncItemRepository.QueryByID(Convert.ToInt32(ids[i]));
                Task _task = _taskRepository.QueryByTaskID(_item.TaskID);
                _machInfo = _machInfoRepository.QueryByNameVersion(_task.TaskName, _task.Version);
                _eleItem = new CncEleItem();
                _eleItem.input_strWholelName = _item.LabelName;
                _eleItem.input_strPosition = pos[i];
                if (_item.LabelName.EndsWith("R"))
                {
                    _eleItem.input_strNcNames = _machInfo.RoughName;
                }
                else if (_item.LabelName.EndsWith("F"))
                {
                    _eleItem.input_strNcNames = _machInfo.FinishName;
                }
                EleItems.Add(_eleItem);
            }
            return EleItems;
        }

        #endregion

        #region 铣铁加工程序传输

        public string CreateSteelProgram(int TaskID, string ProgramIDs, int DeviceID, string PathName)
        {
            Task _steelTask = _taskRepository.QueryByTaskID(TaskID);
            _steelTask.State = (int)CNCStatus.正在加工;
            _steelTask.StartTime = DateTime.Now;
            _taskRepository.Save(_steelTask);
            DateTime _iniTime = DateTime.Parse("1900/1/1");
            Machine _machine = _machineRepository.QueryByID(DeviceID) ?? new Machine();
            string result = "";
            Thread th1 = new Thread(delegate() { result = SteelProgramSend(TaskID, ProgramIDs, DeviceID, PathName); });
            try
            {
                
                
                th1.SetApartmentState(ApartmentState.STA);
                th1.IsBackground = true;
                th1.Start();
                th1.Join();
                th1.Abort();
            }
            catch
            {
                th1.Abort();
                result= "Error";
            }
            #region 任务工时
            if (string.IsNullOrEmpty(result))
            {
                CreateTaskHour(_steelTask, 0, _machine.MachineCode);
            }
            #endregion
            return result;
        }

        public ActionResult SteelTaskDialog(int TaskID)
        {
            int GroupID = _taskRepository.QueryByTaskID(TaskID).ProgramID;

            IEnumerable<SteelProgram> _programs = _steelProgramRepository.QueryByGroupID(GroupID);

            return Json(_programs, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Partial view for steel program select
        /// </summary>
        /// <returns></returns>
        public ActionResult SteelProgramSelect()
        {
            return PartialView();
        }

        /// <summary>
        /// Json data for program transfer
        /// </summary>
        /// <param name="TaskIDs"></param>
        /// <returns></returns>
        public ActionResult JsonSteelPrograms(int TaskID)
        {

            int _programid = _taskRepository.QueryByTaskID(TaskID).ProgramID;
            List<int> _programgroupIDs = _steelGroupProgramRepository.QueryByNCID(_programid).Select(s=>s.SteelGroupProgramID).ToList();
            
            List<SteelProgram> _programs = new List<SteelProgram>();

            foreach (int _groupid in _programgroupIDs)
            {
                _programs.AddRange(_steelProgramRepository.QueryByGroupID(_groupid));
            }
            return Json(_programs, JsonRequestBehavior.AllowGet);
        }

        public string SteelProgramSend(int TaskID, string ProgramIDs, int DeviceID, string PathName)
        {
            ClsCNCProgramme _program = new ClsCNCProgramme();
            List<string> _programs = GetSteelProgram(ProgramIDs);
            Task _task = _taskRepository.QueryByTaskID(TaskID);

            string ProgramPath = GetProgramPath(_task.TaskName);

            if (ProgramPath.IndexOf("T:") == 0)
            {
                ProgramPath = ProgramPath.Replace("T:", _systemConfigRepository.GetConfigValue("T:"));
            }

            string PartName = GetPartName(_task.TaskName, _task.Version);

            string DrawName = GetDrawName(_task.TaskName);

            string DevicdAddr = _machineRepository.QueryByID(DeviceID).IPAddress;

            //_program.Transfer_SteelCncPgm(_programs, DevicdAddr, ProgramPath, PathName, PartName, DrawName, _task.TaskName);
            _program.Transfer_SteelCncPgm("c:\\temp", _programs, DevicdAddr, ProgramPath, PathName, PartName, DrawName, _task.TaskName);
            if (_program.str_Err == "")
            {
                return "";
            }
            else
            {
                return _program.str_Err;
            }
        }

        /// <summary>
        /// Return the program name list
        /// </summary>
        /// <param name="ProgramIDs"></param>
        /// <returns></returns>
        private List<string> GetSteelProgram(string ProgramIDs)
        {
            string[] _programID = ProgramIDs.Split(',');
            List<string> _programName = new List<string>();
            for (int i = 0; i < _programID.Length; i++)
            {
                SteelProgram _program = _steelProgramRepository.QueryByID(Convert.ToInt32(_programID[i]));
                _programName.Add(_program.FileName);
            }
            return _programName;
        }

        /// <summary>
        /// Retrive moldnumber from task name and return the program path
        /// </summary>
        /// <param name="TaskName"></param>
        /// <returns></returns>
        private string GetProgramPath(string TaskName)
        {
            string _path = _systemConfigRepository.SystemConfigs.Where(s => s.SettingName == "STEELPATH").Select(t => t.Value).FirstOrDefault();



            string _mold = TaskName.Substring(0, TaskName.IndexOf('_'));

            if (_path.EndsWith("\\"))
            {
                return _path + _mold;
            }
            else
            {
                return _path + "\\" + _mold;
            }

        }

        private string GetPartName(string TaskName, int Version)
        {
            string _partName;
            try
            {
                _partName = TaskName.Substring(0, TaskName.LastIndexOf('('));
            }
            catch
            {
                _partName = TaskName;
            }
            string _version = Version > 9 ? "V" + Version.ToString() : "V0" + Version.ToString();


            return TaskName + "_" + _version;
        }

        private string GetDrawName(string TaskName)
        {
            string _drawName;
            try
            {
                _drawName = TaskName.Substring(0, TaskName.LastIndexOf('(') - 4);
            }
            catch
            {
                _drawName = TaskName;
            }

            return _drawName;
        }




        //}
        #endregion

        #region EDM程序生成

        public ActionResult GetEDMEleInfo(string EleIDs)
        {
            string[] _ids = EleIDs.Split(',');
            List<Task> _tasks = new List<Task>();
            for (int i = 0; i < _ids.Length; i++)
            {
                CNCItem _item = _cncItemRepository.QueryByID(Convert.ToInt32(_ids[i]));
                IEnumerable<Task> _tempTask = _tasks.Where(t => t.TaskID == _item.TaskID);
                int j = _tempTask.Count();
                if (_tempTask.Count() == 0)
                {
                    Task _task = _taskRepository.QueryByTaskID(_item.TaskID);
                    _tasks.Add(_task);
                }
            }
            return Json(_tasks, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetElePosition(int TaskID)
        {
            Task _task = _taskRepository.QueryByTaskID(TaskID);
            string _pos = _machInfoRepository.QueryByNameVersion(_task.TaskName, _task.Version).Position;
            string[] _positions = _pos.Split(';');
            return Json(_positions, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region UG CAM inteface
        /// <summary>
        /// Get QCPointProgram by 3dName and version
        /// </summary>
        /// <param name="Part3DName"></param>
        /// <param name="Version"></param>
        /// <returns></returns>
        public ActionResult GetQCProgram(string Part3DName, int Version)
        {
            QCPointProgram _program = _qcPointProgramRepository.QueryByPart3D(Part3DName, Version);
            return Json(_program, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveCNCMachInfo(string MachInfo)
        {
            CNCMachInfo _machInfo = JsonConvert.DeserializeObject<CNCMachInfo>(MachInfo);
            if ((_machInfo.RoughName == "") && (_machInfo.FinishName == "") && (_machInfo.Version > 0))
            {
                //Get rough and finish info from older version
                CNCMachInfo _machinfo = _machInfoRepository.QueryByNameVersion(_machInfo.Model, _machInfo.Version - 1);
                if (_machinfo != null)
                {
                    _machInfo.RoughName = _machinfo.RoughName;
                    _machInfo.FinishName = _machinfo.FinishName;
                }
            }



            //Generate the drawing index
            if (_machInfo.DrawIndex == 0)
            {
                _machInfo.DrawIndex = _taskRepository.QueryByModelVersion(_machInfo.Model, _machInfo.Version).TaskID;
            }

            //Save or update MachInfo 
            int _machinfoID = _machInfoRepository.Save(_machInfo);

            return Json(_machInfo, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="Version"></param>
        /// <param name="DrawRev"></param>
        /// <returns>
        /// 0--没有锁定 
        /// 1--已锁定 
        /// 2--此版本没有上一版本 
        /// 3--此版本有另外版本但没有发行 
        /// 4-此版本有高版本已发行,
        ///</returns>
        public int GetLockStatus(string Model, int Version, int DrawRev)
        {
            IEnumerable<CNCMachInfo> _machInfoes = _machInfoRepository.CNCMachInfoes.Where(m => m.Model == Model);

            int _lock = _machInfoes.Where(m => m.DrawRev == DrawRev).Select(m => m.Lock).LastOrDefault();
            if (_lock == 1)
            {
                return 1;
            }
            else
            {
                int _older = _machInfoes.Where(m => m.DrawRev != DrawRev).Where(m => m.Lock == 0).Count();
                if (_older > 0)
                {
                    return 3;
                }
                _older = _machInfoes.Where(m => m.Lock < 2)
                    .Where(m => ((m.Version > Version) || (m.DrawRev > DrawRev))).Count();

                if (_older > 0)
                {
                    return 4;
                }

                return 0;
            }
        }
        /// <summary>
        /// Return program pathes by type name
        /// </summary>
        /// <param name="Name">Name of the path setting</param>
        /// <returns></returns>
        public string GetSetting(string Name)
        {
            return _systemConfigRepository.GetConfigValue(Name);
        }

        public int SaveSetting(string Name, string Value)
        {
            return _systemConfigRepository.Save(Name, Value);
        }


        public string GetTaskDrawingPath(string type="CAM")
        {
            string _pathSetting = type == "CAM" ? "CAMDrawingPath" : type == "CAD" ? "CADDrawingPath" : "CAMDrawingPath";
            string _path = GetSetting(_pathSetting);
            _path = _path.Substring(2, _path.Length - 2).Replace("\\", "/") + "/";
            return _path;
        }

        /// <summary>
        /// Save the QC Steel Point information
        /// </summary>
        /// <param name="SteelPoint"></param>
        /// <returns></returns>
        [HttpPost]
        public int SaveSteelPoint(string SteelPoint)
        {
            QCSteelPoint _qcSteelPoint = JsonConvert.DeserializeObject<QCSteelPoint>(SteelPoint);

            QCSteelPoint _tmp = _qcSteelPointRepository.QueryStatus(_qcSteelPoint.PartName, _qcSteelPoint.Rev);

            if (_tmp == null)
            {
                _qcSteelPointRepository.Save(_qcSteelPoint);
            }
            else
            {
                if (_tmp.Status == 2)
                {
                    return 1;
                }
                else
                {
                    _qcSteelPointRepository.Save(_qcSteelPoint);
                }
            }


            UpdateQCSteelPointFlag(_qcSteelPoint.FullPartName, _qcSteelPoint.PartName, _qcSteelPoint.Rev);

            UpdateTaskQCSteelPointFlag(_qcSteelPoint.PartName, _qcSteelPoint.Rev);
            return 0;

        }

        /// <summary>
        /// Update qcpoint flag in other tables
        /// </summary>
        /// <param name="FullName"></param>
        /// <param name="PartName"></param>
        /// <param name="Version"></param>
        private void UpdateQCSteelPointFlag(string FullName, string PartName, int Version)
        {
            SteelCAMDrawing _camDrawing = _steelCAMDrawingRepository.QueryByFullName(FullName);
            if (_camDrawing != null)
            {
                _camDrawing.QCPoint = true;
                _steelCAMDrawingRepository.Save(_camDrawing);
            }
            EDMDetail _edmDetail = _edmDetailRepository.QueryBySetting(PartName, Version);
            if (_edmDetail != null)
            {
                _edmDetail.QCPoint = true;
                _edmDetailRepository.Save(_edmDetail);
            }
            //WEDM Update is ignored here

        }



        /// <summary>
        /// Update Task &QCTask qcpoint flag
        /// </summary>
        /// <param name="PartName"></param>
        /// <param name="Version"></param>
        private void UpdateTaskQCSteelPointFlag(string PartName, int Version)
        {
            ///Update Task QCPoint Flag
            Task _task = _taskRepository.QueryByNameVersion(PartName, Version);

            if (_task != null)
            {
                _task.QCInfoFinish = true;

                int _taskID = _taskRepository.Save(_task);

                //Add QC Task
                CreateQCTaskWithTask(_task);
            }
        }


        public int UpdatePosFinishFlag(string EleName, int Version)
        {
            CNCMachInfo _machinfo = _machInfoRepository.QueryByNameVersion(EleName, Version);

            if (_machinfo != null)
            {
                _machinfo.PosCheck = true;
                _machInfoRepository.Save(_machinfo);

                UpdateTaskQCPosition(EleName, Version);
                return 0;
            }
            else
            {
                return -1;
            }
        }

        private void UpdateTaskQCPosition(string EleName, int Version)
        {
            Task _task = _taskRepository.QueryByModelVersion(EleName, Version);
            _task.PositionFinish = true;
            _taskRepository.Save(_task);
        }


        /// <summary>
        /// Save the QC Point Program Information and update the task qcpoint flag
        /// </summary>
        /// <param name="QCPointProgram"></param>
        /// <returns></returns>
        [HttpPost]
        public int SaveQCPointProgram(string QCPointProgram)
        {
            QCPointProgram _program = JsonConvert.DeserializeObject<QCPointProgram>(QCPointProgram);
            _program.Enabled = true;
            int _id = _qcPointProgramRepository.Save(_program);

            UpdateTaskQCSteelPointFlag(_program.ElectrodeName, _program.Rev);

            return _id;
        }

        /// <summary>
        /// Create QC Task after qc point program is saved;
        /// Called by TaskController.SaveQCPointProgram
        /// !!!!!Not used
        /// </summary>
        /// <returns></returns>
        //private int CreateQCTasks(QCPointProgram QCPointProgram)
        //{
        //    Task _task = new Task();
        //    _task.TaskType = 5;//QCTask
        //    _task.TaskName = QCPointProgram.ElectrodeName;
        //    _task.Version = QCPointProgram.Rev;
        //    _task.CreateTime = DateTime.Now;
        //    _task.DrawingFile = QCPointProgram.Part3D;
        //    _task.ProjectID = 0;//tobe finished
        //    _task.CAMUser = 0;//QCPointProgram.CreateBy
        //    _task.MoldNumber = GetMoldNumber(QCPointProgram.ElectrodeName);
        //    int _taskID = _taskRepository.Save(_task);
        //    return _taskID;
        //}

        /// <summary>
        /// Create QC task(tasktype=5) while creating machine task
        /// UnFinished!!!
        /// </summary>
        /// <param name="Task"></param>
        public void CreateQCTaskWithTask(Task Task)
        {
            Task _task = new Task();
            _task.TaskType = 5;

            if (Task.TaskType == 1)
            {
                CreateEleQCTask(Task);
            }
            else
            {
                CreateQCTask(Task);
            }

        }

        /// <summary>
        /// Create QCTask
        /// </summary>
        /// <param name="MachineTask"></param>
        /// <param name="LabelName"></param>
        public void CreateQCTask(Task MachineTask, string LabelName = "")
        {
            QCTask _qcTask = new QCTask();
            _qcTask.TaskID = MachineTask.TaskID;
            _qcTask.TaskType = MachineTask.TaskType;
            _qcTask.TaskName = LabelName == "" ? MachineTask.TaskName : LabelName;
            _qcTask.CreateTime = DateTime.Now;
            _qcTask.DrawingFile = MachineTask.DrawingFile+".pdf";
            _qcTask.Version = MachineTask.Version;
            _qcTask.Memo = MachineTask.Memo;
            _qcTask.State = MachineTask.State;
            _qcTask.ProjectID = MachineTask.ProjectID;
            _qcTask.MoldNumber = MachineTask.MoldNumber;
            _qcTask.Enabled = MachineTask.Enabled;
            _qcTaskRepository.Save(_qcTask);
        }

        /// <summary>
        /// Create QC Task for each electrode item
        /// </summary>
        /// <param name="MachineTask"></param>
        public void CreateEleQCTask(Task MachineTask)
        {
            IEnumerable<CNCItem> _items = _cncItemRepository.QueryByTaskID(MachineTask.TaskID);
            foreach (CNCItem _item in _items)
            {
                CreateQCTask(MachineTask, _item.LabelName);
            }
        }



        public ActionResult GetQCSteelPoints(string PartName, int Version)
        {
            IEnumerable<QCSteelPoint> _points = _qcSteelPointRepository.QCSteelPoints.Where(q => q.PartName == PartName)
                .Where(q => q.Rev <= Version).Where(q => q.Enabled == true).OrderByDescending(q => q.Rev);
            return Json(_points, JsonRequestBehavior.AllowGet);
        }

        public string GetXYZFile(string PartName3D, int Version)
        {
            QCPointProgram _program = _qcPointProgramRepository.QCPointPrograms.Where(q => q.Part3D == PartName3D)
                .Where(q => q.Part3DRev == Version).FirstOrDefault();
            return _program.XYZFlieName;
        }

        public ActionResult GetMachInfo(string EleName, int Version)
        {
            IEnumerable<CNCMachInfo> _data = _machInfoRepository.CNCMachInfoes.Where(c => c.Model == EleName).Where(c => c.Version == Version);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public int LastSteelFinished(string DrawName, int Version)
        {
            try
            {
                List<int> _ncids = _steelCAMDrawingRepository.GetNCIDs(DrawName, Version).ToList<int>();

                List<int> _groupPrograms = _steelGroupProgramRepository.SteelGroupPrograms
                    .Where(s => s.Enabled == true)
                    .Where(s => (_ncids.Contains(s.NCID)))
                    .Select(s => s.SteelGroupProgramID).ToList<int>();

                int _taskCount = _taskRepository.QueryByProgramNumber(_groupPrograms).Where(t => t.State < 2).Count();
                if (_taskCount == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch
            {
                return -1;
            }
        }




        public string UpdateSteelDrawing(string FullPartName, string DrawName, int Version,
            string CADPartName, string MoldName, string Programmer, bool UpdateProgram)
        {
            int _id = 0;
            bool _drawLock = false;
            SteelCAMDrawing _camDrawing = _steelCAMDrawingRepository.QueryByNameVersion(DrawName, Version);
            if (_camDrawing != null)
            {
                _id = _camDrawing.SteelCAMDrawingID;
                _drawLock = _camDrawing.DrawLock;
            }

            //已锁定
            if (_drawLock == true)
            {
                return "1," + _id;
            }

            //有更高的版本
            int _temp = _steelCAMDrawingRepository.QueryNewVersion(DrawName, Version).Count();
            if (_temp > 0)
            {
                return "2," + _id;
            }

            //上一个版本没有发行
            _temp = _steelCAMDrawingRepository.QueryOldVersion(DrawName, Version).Count();
            if (_temp > 0)
            {
                return "3," + _id;
            }

            try
            {


                bool _qcPoint = false;
                _temp = _qcSteelPointRepository.QueryByFullPartName(FullPartName).Count();
                if (_temp > 0)
                {
                    _qcPoint = true;
                }


                int _ncid = 0;
                if (_id > 0)
                {
                    //Update SteelCAMDrawing
                    if (UpdateProgram)
                    {
                        _ncid = _id;
                    }

                    _camDrawing.FullPartName = FullPartName;
                    _camDrawing.CADPartName = CADPartName;
                    _camDrawing.MoldName = MoldName;
                    _camDrawing.CreateDate = DateTime.Now;
                    _camDrawing.Programmer = Programmer;
                    _camDrawing.NCID = _ncid;
                    _camDrawing.active = true;
                    _camDrawing.QCPoint = _qcPoint;

                    _steelCAMDrawingRepository.Save(_camDrawing);

                }
                else
                {
                    //Create new SteelCAMDrawing record

                    _id = _steelCAMDrawingRepository.GetNextID();

                    _camDrawing = new SteelCAMDrawing();

                    if (!UpdateProgram)
                    {
                        _ncid = _id;
                    }

                    //_camDrawing.SteelCAMDrawingID = _id == null ? 1 : _id;
                    _camDrawing.FullPartName = FullPartName;
                    _camDrawing.DrawName = DrawName;
                    _camDrawing.DrawREV = Version;
                    _camDrawing.CADPartName = CADPartName;
                    _camDrawing.MoldName = MoldName;
                    _camDrawing.CreateDate = DateTime.Now;
                    _camDrawing.DrawLock = false;
                    _camDrawing.LastestFlag = true;
                    _camDrawing.Programmer = Programmer;
                    _camDrawing.NCID = _ncid;
                    _camDrawing.active = true;
                    _camDrawing.QCPoint = _qcPoint;
                    _camDrawing.Undo_date = new DateTime(1900, 1, 1);
                    _camDrawing.IssueDate = new DateTime(1900, 1, 1);
                    _camDrawing.Delete_time = new DateTime(1900, 1, 1);
                    _camDrawing.Undo_person = "";
                    _camDrawing.Delete_person = "";
                    _camDrawing.IssuePerson = "";
                    _steelCAMDrawingRepository.Save(_camDrawing);

                    _id = _camDrawing.SteelCAMDrawingID;
                    _drawLock = _camDrawing.DrawLock;
                }

                int _groupID = 0;
                if (UpdateProgram)
                {
                    SteelGroupProgram _groupProgram = _steelGroupProgramRepository.SteelGroupPrograms.Where(g => g.NCID == _id).FirstOrDefault();
                    if (_groupProgram != null)
                    {
                        _groupProgram.Enabled = false;
                        _groupID = _steelGroupProgramRepository.Save(_groupProgram);
                    }
                    //else
                    //{
                    //    _groupProgram = new SteelGroupProgram();
                    //    _groupProgram.NCID = _camDrawing.SteelCAMDrawingID;
                    //    _groupProgram.GroupName = "";

                    //}
                }
                SaveSteelCADPart(_id, CADPartName);


                SaveSteelTask(_camDrawing, _groupID);


                return "0," + _id;
            }
            catch
            {
                return "4," + _id;
            }
        }


        private void SaveSteelCADPart(int ID, string CADPartName)
        {
            try
            {
                List<SteelDrawingCADPart> _cadPart = _steelDrawingCADPartRepository.QueryByDrawingID(ID).ToList<SteelDrawingCADPart>();
                if (_cadPart != null)
                {
                    foreach (SteelDrawingCADPart _part in _cadPart)
                    {
                        _part.active = false;
                        _steelDrawingCADPartRepository.Save(_part);
                    }
                }

                string[] subCADPart = CADPartName.Split(';');

                for (int i = 0; i < subCADPart.Length; i++)
                {
                    SteelDrawingCADPart _part = _steelDrawingCADPartRepository.QueryByDrawingID(ID).Where(s => s.CADPartName == subCADPart[i]).FirstOrDefault();
                    if (_part != null)
                    {
                        _part.active = true;
                    }
                    else
                    {
                        _part = new SteelDrawingCADPart();
                        _part.CADPartName = subCADPart[i];
                        _part.SteelDrawingID = ID;
                        _part.active = true;
                    }
                    _steelDrawingCADPartRepository.Save(_part);
                }
            }
            catch
            {

            }
        }


        public int SaveSteelGroupProgram(int DrawingID, string GroupName, double Time)
        {
            SteelGroupProgram _program = _steelGroupProgramRepository.QueryByGroupName(GroupName, DrawingID);
            if (_program != null)
            {
                _program.Enabled = true;
                _program.Time = Time;
            }
            else
            {
                _program = new SteelGroupProgram();
                _program.NCID = DrawingID;
                _program.GroupName = GroupName;
                _program.Time = Time;
                _program.Enabled = true;
            }

            int _id = _steelGroupProgramRepository.Save(_program);

            return _id;
        }

        public int SaveSteelProgram(int GroupID, string ProgramName, string FileName,
            string ToolName, double Time, double Depth, int Sequence, bool HaveFile)
        {
            SteelProgram _program = new SteelProgram();
            _program.GroupID = GroupID;
            _program.ProgramName = ProgramName;
            _program.FileName = FileName;
            _program.ToolName = ToolName;
            _program.Time = Time;
            _program.Depth = Depth;
            _program.Sequence = Sequence;
            _program.HaveFile = HaveFile;
            int _id = _steelProgramRepository.Save(_program);
            return _id;
        }


        public int SaveSteelTask(SteelCAMDrawing SteelDrawing, int GroupID)
        {
            Task _task = new Task();
            Project _project = _projectRepository.QueryByMoldNumber(SteelDrawing.MoldName);
            ProjectPhase _phase;
            if (_project != null)
            {
                _phase = _projectPhaseRepository.ProjectPhases.Where(p => p.ProjectID == _project.ProjectID)
                .Where(p => p.PhaseID == 8).FirstOrDefault();
            }else{
                _phase = null;
            }

            SteelGroupProgram _program = _steelGroupProgramRepository.QueryByID(GroupID).FirstOrDefault();
            if (_program != null)
            {
                _task.ProcessName = _program.GroupName;
            }
            else
            {
                _task.ProcessName = "";
            }

            _task.TaskName = SteelDrawing.FullPartName;
            _task.Model = SteelDrawing.FullPartName;
            _task.MoldNumber = GetMoldNumber(SteelDrawing.FullPartName);
            _task.Version = SteelDrawing.DrawREV;
            _task.Quantity = 1;
            _task.CAMUser = GetUserID(SteelDrawing.Programmer);
            _task.CreateTime = DateTime.Now;
            _task.Enabled = true;
            _task.TaskType = 4;
            _task.Memo = "Created by CAM";
            _task.ProjectID =_project ==null?0: _project.ProjectID;
            _task.ProgramID = SteelDrawing.SteelCAMDrawingID;
            if (_task.ProjectID == 0)
            {
                try
                {
                    _task.ProjectID = _projectRepository.GetLatestActiveProject(_task.MoldNumber).ProjectID;
                }
                catch
                {
                    _task.ProjectID = 0;
                }
                
            }
            _task.PlanTime =_phase==null?new DateTime(1900,1,1):( _phase.PlanCFinish == new DateTime(1, 1, 1) ? _phase.PlanFinish : _phase.PlanCFinish);
            int _taskID = _taskRepository.Save(_task);
            _task.TaskID = _taskID;
            //Add QC Task
            CreateQCTaskWithTask(_task);
            return _taskID;
        }

        public ActionResult GetSteelDrawing(string DrawName, int DrawRev)
        {
            IEnumerable<SteelCAMDrawing> _camDrawings;
            if (DrawRev >= 0)
            {
                _camDrawings = _steelCAMDrawingRepository.SteelCAMDrawings.Where(s => s.DrawName == DrawName)
                .Where(s => s.DrawREV == DrawRev).Where(s => s.active == true);
            }
            else
            {
                _camDrawings = _steelCAMDrawingRepository.SteelCAMDrawings.Where(s => s.DrawName == DrawName)
                .Where(s => s.active == true);
            }
            

            return Json(_camDrawings, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetTaskByGroupID(int GroupID)
        {
            IEnumerable<Task> _tasks = _taskRepository.QueryByGroupID(GroupID);
            return Json(_tasks, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Steel program information
        /// </summary>
        /// <param name="DrawName"></param>
        /// <param name="DrawRev"></param>
        /// <returns></returns>
        public ActionResult SteelProgramInfo(string DrawName, int DrawRev)
        {
            SteelCAMDrawing _steelCAMDrawing = _steelCAMDrawingRepository.QueryByNameVersion(DrawName, DrawRev);
            IEnumerable<SteelGroupProgram> _steelGroup = _steelGroupProgramRepository.QueryByNCID(_steelCAMDrawing.NCID).Where(s => s.Time > 0);
            List<SteelDrawing> _steelDrawings = new List<SteelDrawing>();
            SteelDrawing _steelDrawing;
            foreach (SteelGroupProgram _program in _steelGroup)
            {
                _steelDrawing = new SteelDrawing();
                _steelDrawing.GroupName = _program.GroupName;
                _steelDrawing.Time = _program.Time;
                _steelDrawing.IssueDate = _steelCAMDrawing.IssueDate;
                _steelDrawing.DrawLock = _steelCAMDrawing.DrawLock;
                _steelDrawing.Lastest = _steelCAMDrawing.LastestFlag;
                _steelDrawing.GroupProgramID = _program.SteelGroupProgramID;
                _steelDrawings.Add(_steelDrawing);
            }
            return Json(_steelDrawings, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Save the 
        /// </summary>
        /// <param name="DrawName"></param>
        /// <param name="MoldName"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public int SaveDrawing(string DrawName, string MoldName, string UserName, string DrawType,bool IsContain2D=false)
        {
            CAMDrawing _camDrawing = _camDrawingRepository.QueryByName(DrawName);
            if (_camDrawing == null)
            {
                _camDrawing = new CAMDrawing();
                _camDrawing.DrawingName = DrawName;
                _camDrawing.MoldName = MoldName;
                _camDrawing.CreateBy = UserName;
                _camDrawing.Lock = false;
                _camDrawing.CreateDate = DateTime.Now;
                _camDrawing.active = true;
                _camDrawingRepository.Save(_camDrawing);
                _taskRepository.UpdateDrawing(DrawName, IsContain2D, DrawType);
                return 0;
            }
            else
            {
                if (_camDrawing.Lock)
                {
                    return 1;
                }
                else
                {
                    _camDrawing.MoldName = MoldName;
                    _camDrawing.CreateBy = UserName;
                    _camDrawing.CreateDate = DateTime.Now;
                    _camDrawingRepository.Save(_camDrawing);
                    _taskRepository.UpdateDrawing(DrawName, IsContain2D, DrawType);
                    return 0;
                }
            }
        }


        private void UpdateTaskPDF(string DrawName)
        {
            string _taskName = GetTaskName(DrawName);
            int _version = GetTaskVersion(DrawName);

            Task _task = _taskRepository.QueryByNameVersion(_taskName, _version);
            if (_task != null)
            {

            }
        }

        public ActionResult GetCAMDrawing(string DrawName)
        {
            CAMDrawing _camDrawing = _camDrawingRepository.QueryByName(DrawName);
            if ((_camDrawing == null) || (!_camDrawing.Lock))
            {
                return null;
            }
            else
            {
                return Json(_camDrawing, JsonRequestBehavior.AllowGet);
            }
            //return Json(_camDrawing, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int SaveUGTask(string CAMTask)
        {
            int _camTaskID;
            Task _camTask = JsonConvert.DeserializeObject<Task>(CAMTask);

            _camTask.DrawingFile = "";
            _camTask.CreateTime = DateTime.Now;
            try
            {
                _camTask.ProjectID = _projectRepository.QueryByMoldNumber(_camTask.MoldNumber).ProjectID;
            }
            catch
            {
                _camTask.ProjectID = -1;
            }

            try
            {
                _camTaskID = _taskRepository.Save(_camTask);
            }
            catch
            {
                _camTaskID = 0;
            }

            

            return _camTaskID;
        }


        public int CreateSteelTask(int GroupID, string Note, int CreateBy)
        {
            int _tempCount = _taskRepository.QueryByGroupID(GroupID)
                .Where(t => t.TaskType == 4)
                .Where(t => t.State < 2)
                .Where(t => t.Enabled == true)
                .Count();
            string MoldName = "";
            if (_tempCount > 0)
            {
                return 1;
            }
            else
            {
                SteelGroupProgram _groupProgram = _steelGroupProgramRepository.QueryByNCID(GroupID).FirstOrDefault();

                SteelCAMDrawing _camDrawing = _steelCAMDrawingRepository.QueryByNCID(GroupID);

                if ((_groupProgram == null) || (_camDrawing == null))
                {
                    return 2;
                }
                else
                {
                    string _cadName = _camDrawing.CADPartName.Trim();
                    string _mainPart;
                    MoldName = _camDrawing.MoldName;

                    int _quantity = 0;
                    if (_cadName.IndexOf(';') > 0)
                    {
                        _mainPart = _cadName.Substring(0, _cadName.IndexOf(';') - 2);
                    }
                    else
                    {
                        _mainPart = _cadName.Substring(0, _cadName.Length - 2);
                    }
                    Part _part = _partRepository.Parts.Where(p => p.Name.Contains(_mainPart)).Where(p => p.Enabled == true).FirstOrDefault();
                    if (_part == null)
                    {
                        _quantity = 1;
                    }
                    else
                    {
                        _quantity = _part.Quantity + _part.AppendQty;
                    }

                    int ProjectID = 0;
                    DateTime _dueDate = GetPlanDate(MoldName, ref ProjectID);

                    Task _task = new Task();
                    _task.TaskName = _camDrawing.FullPartName; //+ "(" + _groupProgram.GroupName + ")";
                    _task.ProgramID = GroupID;
                    _task.Quantity = _quantity;
                    _task.Creator = 0;//Need to be added
                    _task.Enabled = true;
                    _task.Memo = Note;
                    _task.DrawingFile = _camDrawing.DrawName; //+ ".pdf";
                    _task.ForecastTime = _dueDate;
                    _task.PlanTime = _dueDate;
                    _task.ProjectID = ProjectID;
                    _task.TaskType = 4;
                    _task.Time = _groupProgram.Time;
                    _task.CAMUser = CreateBy;
                    _task.ProcessName = _groupProgram.GroupName;
                    if (_task.ProjectID == 0)
                    {
                        _task.ProjectID = _projectRepository.GetLatestActiveProject(_task.MoldNumber).ProjectID;
                    }
                    int _taskID = _taskRepository.Save(_task);
                    _task.TaskID = _taskID;
                    //Add QC Task
                    CreateQCTaskWithTask(_task);
                    return 0;
                }
            }

        }

        private DateTime GetPlanDate(string MoldName, ref int ProjectID)
        {
            Project _project = _projectRepository.QueryByMoldNumber(MoldName);
            ProjectPhase _projectPhase = _projectPhaseRepository.GetProjectPhases(_project.ProjectID).Where(p => p.PhaseID == 8).FirstOrDefault();
            ProjectID = _project.ProjectID;
            return _projectPhase.PlanCFinish == new DateTime(0000, 1, 1) ? _projectPhase.PlanCFinish : _projectPhase.PlanFinish;
        }

        public ActionResult GetEDMDetail(string SettingName, int Version)
        {
            EDMDetail _setting = _edmDetailRepository.QueryBySetting(SettingName, Version);

            return Json(_setting, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public int SaveEDMDetail(string SettingData)
        {
            EDMDetail _setting = JsonConvert.DeserializeObject<EDMDetail>(SettingData);

            _setting.CreateDate = DateTime.Now;
            Task _task = _taskRepository.QueryByNameVersion(_setting.SettingName, _setting.Version);
            int _taskID;
            if (_task != null)
            {
                _setting.TaskID = _task.TaskID;
                _task.TaskName = _setting.SettingName;
                _task.Version = _setting.Version;
                _task.CreateTime = DateTime.Now;
                _task.Memo = "Create by CAM";
                _task.MoldNumber = _setting.MoldName == "" ? GetMoldNumber(_setting.SettingName) : _setting.MoldName;
                //EDM Task
                _task.TaskType = 2;
                try
                {
                    _task.ProjectID = _projectRepository.QueryByMoldNumber(GetMoldNumber(_setting.SettingName)).ProjectID;
                }
                catch
                {
                    _task.ProjectID = 0;
                }
                

                int _userID;
                try
                {
                    _userID = _userRepository.GetUserByName(_setting.Designer).UserID;
                }
                catch
                {
                    _userID = 0;
                }
                _task.ProcessName = _setting.ProcessName;
                _task.CAMUser = _userID;
                _taskRepository.Save(_task);
            }
            else
            {
                _task = new Task();
                _task.TaskName = _setting.SettingName;
                _task.Version = _setting.Version;
                _task.CreateTime = DateTime.Now;
                _task.Memo = "Create by CAM";
                _task.MoldNumber = _setting.MoldName == "" ? GetMoldNumber(_setting.SettingName) : _setting.MoldName;
                //EDM Task
                _task.TaskType = 2;
                try
                {
                    _task.ProjectID = _projectRepository.GetLatestActiveProject(_task.MoldNumber).ProjectID;
                }
                catch
                {
                    _task.ProjectID = 0;
                }
                

                int _userID;
                try
                {
                    _userID = _userRepository.GetUserByName(_setting.Designer).UserID;
                }
                catch
                {
                    _userID = 0;
                }

                _task.CAMUser = _userID;
                _task.ProcessName = _setting.ProcessName;
                _taskID = _taskRepository.Save(_task);
                _setting.TaskID = _taskID;
                _task.TaskID = _taskID;
            }
            //Add QC Task
            CreateQCTaskWithTask(_task);

            int _id = _edmDetailRepository.Save(_setting);
            return _id;
        }


        /// <summary>
        /// Get mold number from all string begins with moldnumber
        /// Eg: "102821_E4102001" returns "102821"
        /// </summary>
        /// <param name="ItemName"></param>
        /// <returns>MoldNumber</returns>
        private string GetMoldNumber(string ItemName)
        {

            int _index = ItemName.IndexOf('_');

            if (_index > 0)
            {
                return ItemName.Substring(0, _index);
            }
            else
            {
                return "";
            }

        }

        private int GetUserID(string UserName)
        {
            User _user = _userRepository.GetUserByName(UserName);

            if (_user != null)
            {
                return _user.UserID;
            }
            else
            {
                return 0;
            }
        }

        private string GetTaskName(string ItemName)
        {
            string _taskName = "";
            try
            {
                _taskName = ItemName.Substring(0, ItemName.LastIndexOf('_'));
            }
            catch
            {

            }
            return _taskName;
        }

        private int GetTaskVersion(string ItemName)
        {
            int _Version = 0;
            int _pos = ItemName.LastIndexOf('V') + 1;
            try
            {
                _Version = Convert.ToInt32(ItemName.Substring(_pos, ItemName.Length - _pos));
                return _Version;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 任务信息显示
        public string GetTaskInfo(int TaskID)
        {
            Task _task = _taskRepository.QueryByTaskID(TaskID);
            string _content = "";
            switch (_task.TaskType)
            {
                case 1:
                    _content = CNCTaskInfo(_task);
                    break;
                case 2:
                    _content = EDMTaskInfo(_task);
                    break;
                case 3:
                    _content = WEDMTaskInfo(_task);
                    break;
                case 4:
                    _content = SteelTaskInfo(_task);
                    break;
                default:
                    _content = "";
                    break;
            }
            return _content;
        }

        private string CNCTaskInfo(Task Task)
        {
            StringBuilder _info = new StringBuilder("<table class=\"table-stripped\">");
            _info.Append(AppendLabel("电极名称"));
            _info.Append(AppendData(Task.TaskName));
            _info.Append(AppendLabel("版本"));
            _info.Append(AppendData(Task.Version.ToString()));
            _info.Append(AppendLabel("电极设计"));
            _info.Append(AppendData(_userRepository.GetUserByID(Task.CADUser).FullName));
            _info.Append(AppendLabel("CAM编程"));
            _info.Append(AppendData(_userRepository.GetUserByID(Task.CAMUser).FullName));
            _info.Append(AppendLabel("毛坯"));
            _info.Append(AppendData(Task.Raw));
            _info.Append(AppendLabel("材料"));
            _info.Append(AppendData(Task.Material));

            CNCMachInfo _machInfo = _machInfoRepository.QueryByNameVersion(Task.TaskName, Task.Version);
            if (_machInfo != null)
            {
                _info.Append(AppendLabel("表面要求"));
                _info.Append(AppendData(_machInfo.Surface));
                _info.Append(AppendLabel("粗电极个数"));
                _info.Append(AppendData(_machInfo.RoughCount.ToString()));
                _info.Append(AppendLabel("精电极个数"));
                _info.Append(AppendData(_machInfo.FinishCount.ToString()));
                //_info.Append(AppendLabel("预留量"));
                _info.Append(AppendLabel("ROUGH_GAP"));
                _info.Append(AppendData(_machInfo.RoughGap.ToString()));
                _info.Append(AppendLabel("FINISH_GAP"));
                _info.Append(AppendData(_machInfo.FinishGap.ToString()));
                _info.Append(AppendLabel("跑位"));
                _info.Append(AppendData(_machInfo.Position.Replace(";", "<br/>")));
                _info.Append(AppendLabel("OBIT_TYPE"));
                _info.Append(AppendData(_machInfo.ObitType));
                _info.Append(AppendLabel("NC_ROUGH_NAME"));
                _info.Append(AppendData(_machInfo.RoughName.Replace(";", "<br/>")));
                _info.Append(AppendLabel("NC_FINISH_NAME"));
                _info.Append(AppendData(_machInfo.FinishName.Replace(";", "<br/>")));
            }

            _info.Append("</table>");
            return _info.ToString();
        }

        private string EDMTaskInfo(Task Task)
        {
            return "";
        }
        private string WEDMTaskInfo(Task Task)
        {
            return "";
        }
        private string SteelTaskInfo(Task Task)
        {
            return "";
        }

        private string AppendLabel(string LabelName)
        {
            StringBuilder _line = new StringBuilder();
            _line.AppendFormat("<tr><td><label>{0}</label></td></tr>", LabelName);
            return _line.ToString();
        }
        private string AppendData(string Data)
        {
            StringBuilder _line = new StringBuilder();
            _line.AppendFormat("<tr><td>{0}</td></tr>", Data);
            return _line.ToString();
        }

        #endregion

        #region 任务结束
        public ActionResult TaskFinishList(int TaskType = 1)
        {
            ViewBag.TaskType = TaskType;
            return View();
        }

        public ActionResult JsonFinishTasks(int TaskType)
        {
            int _status = (int)CNCStatus.未发布;
            TaskFinishGridViewModel _model;
            _status = (int)CNCStatus.正在加工;
            //switch (TaskType)
            //{
            //    case 1:
            //        _status = (int)CNCStatus.正在加工;
            //        break;
            //    case 2:
            //        _status = (int)EDMStatus.正在加工;
            //        break;
            //    case 3:
            //        _status = (int)WEDMStatus.正在加工;
            //        break;
            //    case 4:
            //        _status = (int)SteelStatus.正在加工;
            //        break;
            //}
            if (TaskType > 1)
            {
                IEnumerable<Task> _tasks = _taskRepository.Tasks
                .Where(t => t.TaskType == TaskType)
                .Where(t => t.State == _status)
                .Where(t => t.Enabled == true);
                _model = new TaskFinishGridViewModel(_tasks);
            }
            else
            {
                IEnumerable<CNCItem> _items = _cncItemRepository.CNCItems
                    .Where(c => c.Status == _status);
                _model = new TaskFinishGridViewModel(_items);
            }

            return Json(_model, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 任务点检
        /// </summary>
        /// <param name="IDs"></param>
        /// <param name="TaskType"></param>
        /// <returns></returns>

        public int TaskFinish(string IDs, int TaskType)
        {
            string[] _ids = IDs.Split(',');
            int _targetState;
            switch (TaskType)
            {
                case 1:
                    _targetState = (int)CNCStatus.已入库;
                    break;
                case 4:
                    _targetState = (int)SteelStatus.完成;
                    break;
                default:
                    _targetState = -1;
                    break;
            }
            if (_targetState > 0)
            {
                try
                {
                    for (int i = 0; i < _ids.Length; i++)
                    {
                        _taskRepository.FinishTask(Convert.ToInt32(_ids[i]), _targetState, Convert.ToInt32(Request.Cookies["User"]["UserID"]));
                    }
                    return 0;
                }
                catch
                {
                    return 1;
                }
            }
            else
            {
                return 1;
            }
        }
        #endregion

        #region 加工设备设定

        public ActionResult MachineSetting(int TaskType = 0)
        {
            List<Machine> _machines;
            _machines = _machineRepository.GetMachinesByTaskType(TaskType);
            ViewBag.TaskType = TaskType;
            return View(_machines);
        }
        public JsonResult Service_Json_GetMachincesByType(int TaskType)
        {
            List<Machine> _machines;
            _machines = _machineRepository.GetMachinesByTaskType(TaskType);
            return Json(_machines, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Service_Get_MInfoByTaskType(string TaskType="")
        {
            List<string> _existcodes=new List<string>();
            List<Machine> _machines= _machineRepository.GetMachinesByTaskType(Convert.ToInt32(TaskType));
            if (_machines != null)
            {
                foreach(var m in _machines)
                {
                    _existcodes.Add(m.MachineCode);
                }
            }
            List<MachinesInfo> _minfos = _machinesinfoRepository.GetMInfoByTaskType(Convert.ToInt32(TaskType)).Where(m=> !_existcodes.Contains(m.MachineCode)).ToList();
            return Json(_minfos, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Service_Get_TaskMInfoByTaskType(string TaskType,bool isWF=false)
        {
            List<MachinesInfo> _minfos;
            //虚拟委外
            if (isWF)
            {
                _minfos = _machinesinfoRepository.GetMInfoByTaskType(Convert.ToInt32(TaskType)).Where(m => m.EquipBrand == "委外").ToList();
            }
            //
            else
            {
                _minfos = _machinesinfoRepository.GetMInfoByTaskType(Convert.ToInt32(TaskType)).Where(m => m.EquipBrand != "委外").ToList();
            }
            return Json(_minfos,JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMachineInfo(int MachineID)
        {
            Machine _machine = _machineRepository.QueryByID(MachineID);
            return Json(_machine, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMachine(Machine _machine,string TaskType="")
        {
            _machine.Enabled = true;
            _machineRepository.Save(_machine);
            return RedirectToAction("MachineSetting", "Task",new { TaskType = TaskType });
        }

        public int DeleteMachine(int MachineID)
        {
            int _id = _machineRepository.Delete(MachineID);
            return _id;
        }
        #region michael
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TaskIDs"></param>
        /// <param name="type">1 等待 2 正在加工</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Service_Json_GetTaskByIDs(string TaskIDs,int type)
        {
            try
            {
                List<SetupTaskStart> _setupTasks = new List<SetupTaskStart>();
                List<int> _typelists = new List<int>();
                switch (type)
                {
                    case 1:
                        _typelists.Add((int)CNCStatus.等待);
                        _typelists.Add((int)CNCStatus.等待中);
                        _typelists.Add((int)CNCStatus.已接收);
                        break;
                    case 2:
                        _typelists.Add((int)CNCStatus.外发);
                        _typelists.Add((int)CNCStatus.正在加工);
                        break;
                }
                if (TaskIDs != null)
                {
                    #region region
                    if (TaskIDs.IndexOf(',') > 0)
                    {
                        string[] _taskids = TaskIDs.Split(',');
                        foreach (var _tid in _taskids)                            
                        {                            
                            var _task= _taskRepository.QueryByTaskID(Convert.ToInt32(_tid));
                            var _setuptask = _taskHourRepository.GetCurTHByTaskID(Convert.ToInt32(_tid)) ?? new TaskHour();
                            decimal _totalTime = 0;
                            if (_task.State == (int)CNCStatus.正在加工)
                            {
                                _totalTime = _taskHourRepository.GetTotalHourByTaskID(Convert.ToInt32(_tid));
                            }
                            SetupTaskStart _setupTask = new SetupTaskStart() {
                                TaskID = _task.TaskID,
                                TaskName = _task.TaskName,
                                State = Enum.GetName(typeof(CNCStatus), _task.State),
                                MachinesCode = _setuptask.MachineCode??"",
                                MachinesName = _taskHourRepository.GetMachineByTask(Convert.ToInt32(_tid))??"",
                                UserID = (_userRepository.GetUserByName(_setuptask.Operater)??new User()).UserID,
                                UserName = _setuptask.Operater??"",
                                TotalTime=_totalTime
                            };
                            if (_typelists.Contains(_task.State))
                                _setupTasks.Add(_setupTask);
                        }
                    }
                    else
                    {
                        var _task = _taskRepository.QueryByTaskID(Convert.ToInt32(TaskIDs));
                        var _setuptask = _taskHourRepository.GetCurTHByTaskID(Convert.ToInt32(TaskIDs)) ?? new TaskHour();
                        decimal _totalTime = 0;
                        if (_task.State == (int)CNCStatus.正在加工)
                        {
                            _totalTime = _taskHourRepository.GetTotalHourByTaskID(Convert.ToInt32(TaskIDs));
                        }
                        SetupTaskStart _setupTask = new SetupTaskStart()
                        {
                            TaskID = _task.TaskID,
                            TaskName = _task.TaskName,
                            State = Enum.GetName(typeof(CNCStatus), _task.State),
                            MachinesCode = _setuptask.MachineCode ?? "",
                            MachinesName = _taskHourRepository.GetMachineByTask(Convert.ToInt32(TaskIDs)) ?? "",
                            UserID = (_userRepository.GetUserByName(_setuptask.Operater) ?? new User()).UserID,
                            UserName = _setuptask.Operater ?? "",
                            TotalTime = _totalTime
                        };
                        if (_typelists.Contains(_task.State))
                            _setupTasks.Add(_setupTask);
                    }
                    #endregion
                    SetupTaskGridViewModel _viewmodel = new SetupTaskGridViewModel(_setupTasks);
                    return Json(_viewmodel, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
            }
            return null;
        }
        #endregion
        #endregion

        #region 添加手工任务
        public ActionResult AddEDMTask()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReaddEDMTask(int OldTaskID, string TaskName, string Version,
            string EleDetail, string CADDetail, string Memo, int Priority)
        {
            Task _oldtask = _taskRepository.QueryByTaskID(OldTaskID);
            Task _task = new Task();
            _task.TaskName = TaskName;

            try
            {
                _task.Version = Convert.ToInt32(Version.Substring(1));
            }
            catch
            {
                _task.Version = Convert.ToInt32(Version);
            }

            _task.DrawingFile = _oldtask.DrawingFile;
            _task.Quantity = _oldtask.Quantity;
            _task.ProjectID = _oldtask.ProjectID;
            _task.CADUser = _oldtask.CADUser;
            _task.CAMUser = _oldtask.CAMUser;
            _task.Creator = _oldtask.Creator;
            _task.ReleaseTime = DateTime.Now;
            _task.CreateTime = DateTime.Now;
            _task.PositionFinish = _oldtask.PositionFinish;
            _task.QCInfoFinish = _oldtask.QCInfoFinish;

            _task.Memo = Memo;
            _task.Priority = Priority;
            _task.TaskType = 2;
            _task.Enabled = true;
            _task.MoldNumber = _oldtask.MoldNumber;

            _task.State = 0;

            int _taskID = _taskRepository.AddNewTask(_task);
            _task.TaskID = _taskID;

            ///Add QC Task
            CreateQCTaskWithTask(_task);

            EDMDetail _edmDetail = new EDMDetail();
            string _designer = _edmDetailRepository.QueryByTaskID(OldTaskID).Designer;
            _edmDetail.SettingName = TaskName;
            _edmDetail.Version = _task.Version;
            _edmDetail.EleDetail = EleDetail;
            _edmDetail.CADDetail = CADDetail;
            _edmDetail.TaskID = _taskID;
            _edmDetail.ModifyName = "";
            _edmDetail.ModifyCount = CADDetail.Split(';').Length;
            _edmDetail.Designer = _designer;
            _edmDetail.Expire = false;
            _edmDetail.Lock = 1;
            _edmDetail.CreateDate = DateTime.Now;

            _edmDetailRepository.AddEDMDetail(_edmDetail);



            return RedirectToAction("MachineTaskList", "Task", new { TaskType = 2 });
        }


        public ActionResult AddCNCTask(int TaskType = 1)
        {
            return View();
        }


        public string ReaddCNCTask(string TaskIDs, int RCount, int FCount)
        {
            string[] _taskid = TaskIDs.Split(',');
            string _deptName = Request.Cookies["User"]["DepartmentName"];
            string _msg = "";
            for (int i = 0; i < _taskid.Length; i++)
            {
                Task _newTask = DuplicateTask(Convert.ToInt32(_taskid[i]));
                _newTask.ReleaseTime = DateTime.Now;
                _newTask.CreateTime = DateTime.Now;
                if (_newTask.R > 0)
                {
                    _newTask.R = RCount;
                }
                if (_newTask.F > 0)
                {
                    _newTask.F = FCount;
                }

                _newTask.Memo = "Created By " + _deptName;
                int _id = _taskRepository.AddNewTask(_newTask);
                _newTask.TaskID = _id;
                double _fgap ;
                double _rgap;
                try
                {
                    _fgap = _cncItemRepository.QueryByTaskID(Convert.ToInt32(_taskid[i])).Where(c => c.LabelName.EndsWith("F")).FirstOrDefault().Gap;
                }
                catch
                {
                    _fgap = 0;
                }

                try
                {
                    _rgap = _cncItemRepository.QueryByTaskID(Convert.ToInt32(_taskid[i])).Where(c => c.LabelName.EndsWith("R")).FirstOrDefault().Gap;

                }
                catch
                {
                    _rgap = 0;
                }
                if (_newTask.TaskType == 1)
                {
                    ReaddCNCItem(_newTask, _rgap, _fgap);
                }

                if ((_newTask.F == 0) && (_newTask.R == 0))
                {
                    _msg = _msg == "" ? _newTask.TaskName : _msg + "," + _newTask.TaskName;
                }
                else
                {
                    CreateQCTaskWithTask(_newTask);
                }

            }
            return _msg;
        }

        public void ReaddCNCItem(Task Task, double RGap, double FGap)
        {
            int _seq = GetCNCItemSequence(Task.TaskName);
            
            for (int i = 0; i < Task.R; i++)
            {
                CNCItem _item = new CNCItem();
                _item.LabelName = GetLabel(Task.TaskName, Task.Version, _seq, "R");
                _item.TaskID = Task.TaskID;
                _item.Material = Task.Material;
                _item.SafetyHeight = GetSafetyHeight(Task.Raw);
                _item.Gap = RGap;
                _seq = _seq + 1;
                _item.MoldNumber = Task.MoldNumber;
                _cncItemRepository.Save(_item);

            }
            for (int i = 0; i < Task.F; i++)
            {
                CNCItem _item = new CNCItem();
                _item.LabelName = GetLabel(Task.TaskName, Task.Version, _seq, "F");
                _item.TaskID = Task.TaskID;
                _item.Material = Task.Material;
                _item.SafetyHeight = GetSafetyHeight(Task.Raw);
                _item.Gap = FGap;
                _seq = _seq == 0 ? 4 : _seq + 1;
                _item.MoldNumber = Task.MoldNumber;
                _cncItemRepository.Save(_item);
            }
        }

        private int GetCNCItemSequence(string TaskName)
        {
            int _seq = 0;
            CNCItem _item = _cncItemRepository.CNCItems
                .Where(c => c.LabelName.Contains(TaskName))
                .OrderByDescending(c => c.LabelName).FirstOrDefault();
            if (_item != null)
            {
                string _itemName = _item.LabelName;
                _itemName = _itemName.Substring(_itemName.LastIndexOf('/') + 1);
                _itemName = _itemName.Substring(0, _itemName.LastIndexOf('-'));
                _seq = Convert.ToInt32(_itemName);
            }

            return _seq + 1;
        }

        private string GetLabel(string TaskName, int Version, int Sequence, string ItemType)
        {
            string _version = Version > 9 ? Version.ToString() : "0" + Version.ToString();

            return TaskName + "/" + _version + "/" + Sequence + "-" + ItemType;
        }

        public string JsonEleDetail(int TaskID)
        {
            EDMDetail _detail = _edmDetailRepository.QueryByTaskID(TaskID);
            return _detail.EleDetail;
        }

        public string JsonCADDetail(int TaskID)
        {
            EDMDetail _detail = _edmDetailRepository.QueryByTaskID(TaskID);
            return _detail.CADDetail;
        }

        private Task DuplicateTask(int OldTaskID)
        {
            Task _oldtask = _taskRepository.QueryByTaskID(OldTaskID);
            Task _task = new Task();
            _task.TaskName = _oldtask.TaskName;
            _task.Version = _oldtask.Version;
            _task.DrawingFile = _oldtask.DrawingFile;
            _task.Quantity = _oldtask.Quantity;
            _task.ProjectID = _oldtask.ProjectID;
            _task.CADUser = _oldtask.CADUser;
            _task.CAMUser = _oldtask.CAMUser;
            _task.Creator = _oldtask.Creator;
            _task.ReleaseTime = DateTime.Now;
            _task.CreateTime = DateTime.Now;
            _task.R = _oldtask.R;
            _task.F = _oldtask.F;
            _task.PositionFinish = _oldtask.PositionFinish;
            _task.QCInfoFinish = _oldtask.QCInfoFinish;
            _task.TaskType = _oldtask.TaskType;
            _task.Enabled = true;
            _task.MoldNumber = _oldtask.MoldNumber;
            _task.TaskID = 0;
            _task.Raw = _oldtask.Raw;
            _task.ProcessName = _oldtask.ProcessName;
            _task.Model = _oldtask.ProcessName;
            _task.HRC = _oldtask.HRC;
            _task.Material = _oldtask.Material;
            _task.Time = _oldtask.Time;
            _task.ProgramID = _oldtask.ProgramID;
            _task.State = 0;
            return _task;

        }


        public string CloseTask(string TaskIDs, int UserID=0, string Memo="")
        {
            string[] _taskIDs = TaskIDs.Split(',');
            string _result = "";
            for (int i = 0; i < _taskIDs.Length; i++)
            {
                Task _task = _taskRepository.QueryByTaskID(Convert.ToInt32(_taskIDs[i]));
                if (_task != null)
                {
                    try
                    {
                        _task.State = (int)CNCStatus.完成;
                        _task.WorkshopUser = UserID;
                        _task.Memo = Memo;
                        _task.FinishTime = DateTime.Now;
                        _taskRepository.Save(_task);
                    }
                    catch
                    {
                        _result = _result == "" ? _task.TaskName : _result + "," + _task.TaskName;
                    }
                }
            }
            return _result;
        }
        #endregion

        #region QCTask

        public ActionResult QCTaskList(int State = 0, int ProjectID = 0)
        {
            ViewBag.Title = "质检任务";
            ViewBag.ProjectID = ProjectID;
            ViewBag.State = State;
            return View();
        }

        public ActionResult JsonQCTasks(int TaskType, int State = 0, int ProjectID = 0)
        {
            IEnumerable<QCTask> _qcTasks = _qcTaskRepository.QueryByProjectID(ProjectID, State);

            QCTaskGridViewModel _model = new QCTaskGridViewModel(_qcTasks);
            return Json(_model, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// Return distinct mold numbers in qctask table
        /// Called by MoldManager.QC.QCinformation.GetMoldNumber
        /// </summary>
        /// <param name="TaskType"></param>
        /// <param name="Keyword"></param>
        /// <returns></returns>
        public ActionResult GetQCMoldNumber(int TaskType, string Keyword, string States = "")
        {
            IEnumerable<QCTask> _qcTasks = _qcTaskRepository.QCTasks.Where(q => q.Enabled == true);
            if (TaskType == 1)
            {
                _qcTasks = _qcTasks.Where(q => q.TaskType == 1);
            }
            else
            {
                _qcTasks = _qcTasks.Where(q => q.TaskType > 1);
            }
            if (Keyword != "")
            {
                _qcTasks = _qcTasks.Where(q => q.TaskName.Contains(Keyword));
            }

            if (States != "")
            {
                string[] _states = States.Split(',');
                List<int> _state = new List<int>();
                for (int i = 0; i < _states.Length; i++)
                {
                    _state.Add(Convert.ToInt32(_states[i]));
                }
                _qcTasks = _qcTasks.Where(q => (_state.Contains(q.State)));
            }
            IEnumerable<string> _moldNumbers = _qcTasks.Select(q => q.MoldNumber).Distinct();
            return Json(_moldNumbers, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Get qc task by moldnumber, tasktype and keyword
        /// </summary>
        /// <param name="MoldNumber">QCTask.MoldNumber</param>
        /// <param name="TaskType">
        /// 1:ELETask
        /// 2:Steel and EDM task
        /// </param>
        /// <param name="Keyword"></param>
        /// <returns></returns>
        public ActionResult GetQCTasks(string MoldNumber, int TaskType, string Keyword, string State = "0")
        {
            IEnumerable<QCTask> _qcTasks = _qcTaskRepository.QCTasks
                .Where(q => q.MoldNumber == MoldNumber)
                .Where(q => q.Enabled == true);
            if (TaskType == 1)
            {
                _qcTasks = _qcTasks.Where(q => q.TaskType == 1);
            }
            else
            {
                _qcTasks = _qcTasks.Where(q => q.TaskType > 1);
            }
            if (Keyword != "")
            {
                _qcTasks = _qcTasks.Where(q => q.TaskName.Contains(Keyword));
            }

            string[] _states = State.Split(',');
            List<QCTask> _tasks = new List<QCTask>();
            List<QCTask> _temp;
            for (int i = 0; i < _states.Length; i++)
            {
                int _state = Convert.ToInt16(_states[i]);
                _temp = _qcTasks.Where(q => q.State == _state).ToList();
                _tasks.AddRange(_temp);
            }
            _tasks = _tasks.OrderBy(t => t.TaskName).ToList();

            return Json(_tasks, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Finished Task filtered by moldnumber and tasktype
        /// </summary>
        /// <param name="MoldNumber"></param>
        /// <param name="TaskType"></param>
        /// <returns></returns>
        public ActionResult GetFinishedQCTasks(string MoldNumber, int State, string Keyword = "")
        {
            IEnumerable<QCTask> _qcTasks = _qcTaskRepository.QCTasks
                .Where(q => q.MoldNumber == MoldNumber)
                .Where(q => q.Enabled == true)
                .Where(q => q.State == State)
                .Where(q => q.TaskType == 1);
            if (Keyword != "")
            {
                _qcTasks = _qcTasks.Where(q => q.TaskName.Contains(Keyword));
            }
            List<CNCItem> _cncItems = new List<CNCItem>();
            foreach (QCTask _qctask in _qcTasks)
            {

                CNCItem _cncItem = _cncItemRepository.QueryByLabel(_qctask.TaskName);
                if (_cncItem != null)
                {
                    _cncItems.Add(_cncItem);
                }

            }

            return Json(_cncItems, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Private Sub DianjiGridView_CellContentClick
        /// 
        /// 获取qc加工任务信息
        /// </summary>
        /// <param name="QCTaskID"></param>
        /// <returns></returns>
        public ActionResult GetTaskByQCTaskID(int QCTaskID)
        {
            QCTask _qctask = _qcTaskRepository.QueryByID(QCTaskID);
            //Task _task = _taskRepository.QueryByTaskID(_taskID);
            return Json(_qctask, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Private Sub ELECheckGridView_CellContentClick
        /// 获取qc加工任务信息
        /// </summary>
        /// <param name="LabelName"></param>
        /// <returns></returns>
        public ActionResult GetTaskByLabelName(string LabelName)
        {
            QCTask _qctask = _qcTaskRepository.QCTasks.Where(q => q.TaskName == LabelName).FirstOrDefault();
            return Json(_qctask, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Private Sub ReELELabelHistory(ByVal MoldName As String, ByVal FilterStr As String)
        /// 搜索电极
        /// </summary>
        /// <param name="MoldNumber"></param>
        /// <param name="Keyword"></param>
        /// <returns></returns>
        public ActionResult GetEleInfo(string MoldNumber, string Keyword)
        {
            IEnumerable<int> _taskIDs = _taskRepository.Tasks
                .Where(t => t.MoldNumber == MoldNumber)
                .Where(t => t.Enabled == true).Select(t => t.TaskID);

            IEnumerable<CNCItem> _items = _cncItemRepository.CNCItems
                .Where(c => (_taskIDs.Contains(c.TaskID)))
                .Where(c => c.LabelName.Contains(Keyword));
            return Json(_items, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        ///  更新qc任务优先级 
        /// </summary>
        /// <param name="QCTaskIDs"></param>
        /// <returns>QCTaskID which failed to update priority</returns>
        public string UpdatePriority(string QCTaskIDs, int Priority)
        {
            string[] _qcTaskIDs = QCTaskIDs.Split(',');
            string _error = "";
            for (int i = 0; i < _qcTaskIDs.Length; i++)
            {
                try
                {
                    QCTask _qcTask = _qcTaskRepository.QueryByID(Convert.ToInt32(_qcTaskIDs[i]));

                    _qcTask.Priority = Priority;
                    _qcTaskRepository.Save(_qcTask);
                }
                catch
                {
                    _error = _error == "" ? _qcTaskIDs[i] : "," + _qcTaskIDs[i];
                }

            }
            return _error;
        }


        /// <summary>
        /// 设置铣铁/放电QC任务结束
        /// </summary>
        /// <param name="QCTaskIDs"></param>
        /// <param name="QCUserName"></param>
        /// <returns></returns>
        public string FinishQCTask(string QCTaskIDs, string QCUserName)
        {
            string[] _qcTaskIDs = QCTaskIDs.Split(',');
            int _qcUserID;
            try
            {
                _qcUserID = _userRepository.GetUserByName(QCUserName).UserID;
            }
            catch
            {
                _qcUserID = 0;
            }

            string _error = "";
            for (int i = 0; i < _qcTaskIDs.Length; i++)
            {
                try
                {
                    QCTask _qcTask = _qcTaskRepository.QueryByID(Convert.ToInt32(_qcTaskIDs[i]));
                    if (_qcTask != null)
                    {
                        if (_qcTask.State < (int)QCStatus.确认)
                        {
                            _qcTask.State = (int)QCStatus.确认;
                            _qcTask.QCUser = _qcUserID;
                            _qcTask.FinishTime = DateTime.Now;
                            _qcTaskRepository.Save(_qcTask);
                        }

                        QCSteelPoint _steelPoint = _qcSteelPointRepository
                            .QueryByName(_qcTask.TaskName, _qcTask.Version)
                            .Where(q => q.Status < (int)QCStatus.确认)
                            .FirstOrDefault();
                        if (_steelPoint != null)
                        {
                            _steelPoint.Status = (int)QCStatus.确认;
                            _qcSteelPointRepository.Save(_steelPoint);
                        }
                    }
                }
                catch
                {
                    _error = _error == "" ? _qcTaskIDs[i] : "," + _qcTaskIDs;
                }
            }
            return _error;
        }

        /// <summary>
        /// 设置电极QC任务确认， 并设置电极标记
        /// </summary>
        /// <param name="LabelName"></param>
        /// <param name="UserName"></param>
        /// <param name="Status"></param>
        public void ConfirmEleQCTask(string LabelName, string UserName, int Status)
        {

            int _userID;
            try
            {
                _userID = _userRepository.GetUserByName(UserName).UserID;
            }
            catch
            {
                _userID = 0;
            }
            //QC确认
            try
            {
                //设置QC任务完成
                QCTask _qcTask = _qcTaskRepository.QCTasks
                    .Where(t => t.TaskName == LabelName)
                    .Where(t => t.Enabled == true)
                    .FirstOrDefault();
                if (_qcTask != null)
                {
                    _qcTask.State = (int)QCStatus.确认;
                    _qcTask.FinishTime = DateTime.Now;
                    _qcTask.QCUser = _userID;
                    _qcTaskRepository.Save(_qcTask);
                }

                CNCItem _item = _cncItemRepository.QueryByLabel(LabelName);
                if (_item != null)
                {
                    //设置电极状态
                    _item.Status = Status;
                    _cncItemRepository.Save(_item);

                    //如果电极需要返工，设置CNC加工任务返工状态
                    if (Status == (int)QCStatus.返工)
                    {
                        Task _task = _taskRepository.QueryByTaskID(_item.TaskID);
                        _task.State = (int)CNCStatus.返工;
                        _task.QCUser = _userID;
                        _taskRepository.Save(_task);
                    }

                }
            }
            catch
            {
            }
        }

        public string SaveQCResult(string LabelName,
            double X,
            double Y,
            double Z,
            double C,
            double HeightMax,
            double HeightMin,
            double GapMax,
            double GapMin,
            double GapCompensation,
            double ZCompensation)
        {
            CNCItem _item = _cncItemRepository.QueryByLabel(LabelName);
            _item.OffsetX = X;
            _item.OffsetY = Y;
            _item.OffsetZ = Z;
            _item.OffsetC = C;
            _item.HeightMax = HeightMax;
            _item.HeightMin = HeightMin;
            _item.GapMax = GapMax;
            _item.GapMin = GapMin;
            _item.GapCompensation = GapCompensation;
            _item.ZCompensation = ZCompensation;
            _item.QCFinishTime = DateTime.Now;
            _cncItemRepository.Save(_item);

            QCTask _qctask = _qcTaskRepository.QCTasks.Where(q => q.TaskName == LabelName).FirstOrDefault();
            _qctask.State = (int)QCStatus.测量结束;
            _qctask.FinishTime = DateTime.Now;
            _qcTaskRepository.Save(_qctask);
            return _item.LabelName;
        }

        public ActionResult GetPointInformation(int CNCItemID)
        {
            CNCItem _item = _cncItemRepository.QueryByID(CNCItemID);
            Task _task = _taskRepository.QueryByTaskID(_item.TaskID);
            QCPointProgram _qcPointPgm = _qcPointProgramRepository.QCPointPrograms.Where(q => q.ElectrodeName == _task.TaskName)
                .Where(q => q.Rev == _task.Version).Where(q => q.Enabled == true).FirstOrDefault();

            EleQCInformation _info = new EleQCInformation();
            _info.LabelName = _item.LabelName;
            _info.Clearance = _qcPointPgm == null ? 0 : _qcPointPgm.Clearance;
            _info.CNCMachMethod = _item.CNCMachMethod;
            _info.ElectrodeName = _task.TaskName;
            _info.PartName3D = _qcPointPgm == null ? "" : _qcPointPgm.PartName3D;
            _info.PartPath = _qcPointPgm == null ? "" : _qcPointPgm.PartPath;
            _info.X = _qcPointPgm == null ? 0 : _qcPointPgm.X;
            _info.Y = _qcPointPgm == null ? 0 : _qcPointPgm.Y;
            _info.XYZFileName = _qcPointPgm == null ? "" : _qcPointPgm.XYZFlieName;
            _info.Gap = _item.Gap;
            return Json(_info, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetQCSteelPoint(int QCTaskID)
        {
            QCTask _qcTask = _qcTaskRepository.QueryByID(QCTaskID);

            QCSteelPoint _qcSteelPoint = _qcSteelPointRepository.QueryByNameVersion(_qcTask.TaskName, _qcTask.Version);
            return Json(_qcSteelPoint, JsonRequestBehavior.AllowGet);
        }


        public void DeleteElectrode(int QCTaskID)
        {
            QCTask _task = _qcTaskRepository.QueryByID(QCTaskID);
            _task.State = 3;
            _qcTaskRepository.Save(_task);

            CNCItem _item = _cncItemRepository.QueryByLabel(_task.TaskName);
            _item.Status = 6;
            _cncItemRepository.Save(_item);
        }

        public int RestartTask(int QCTaskID, string UserName)
        {
            QCTask _qcTask = _qcTaskRepository.QueryByID(QCTaskID);

            if (_qcTask.State != 2)
            {
                return 1;
            }

            //int UserID = _userRepository.GetUserByName(UserName).UserID;
            QCTask _newTask = new QCTask();
            _newTask.TaskID = _qcTask.TaskID;
            _newTask.DrawingFile = _qcTask.DrawingFile;
            _newTask.TaskName = _qcTask.TaskName;
            _newTask.TaskType = _qcTask.TaskType;
            _newTask.Version = _qcTask.Version;
            _newTask.Priority = 0;
            _newTask.Quantity = _qcTask.Quantity;
            _newTask.ForecastTime = _qcTask.ForecastTime;
            _newTask.CreateTime = DateTime.Now;
            _newTask.StartTime = _qcTask.StartTime;
            _newTask.FinishTime = _qcTask.FinishTime;
            _newTask.Memo = _qcTask.Memo;
            _newTask.StateMemo = _qcTask.StateMemo;
            _newTask.State = 0;
            _newTask.ProjectID = _qcTask.ProjectID;
            _newTask.QCUser = _qcTask.QCUser;
            _newTask.MoldNumber = _qcTask.MoldNumber;
            _newTask.Enabled = true;

            _qcTaskRepository.Save(_newTask);

            return 0;


        }

        public ActionResult GetQCSetting(string ComputerName)
        {
            QCCmmFileSetting _setting = _qcCmmFileSettingRepository.QueryByComputer(ComputerName);
            return Json(_setting, JsonRequestBehavior.AllowGet);
        }

        public void SaveQCSetting(string ComputerName,
            string CMMFile,
            string BackDir,
            string ReportsServer,
            string TemplatePath,
            string SteelTemplatePath)
        {
            QCCmmFileSetting _setting = new QCCmmFileSetting();
            _setting.ComputerName = ComputerName;
            _setting.FileAddress = CMMFile;
            _setting.BackupDir = BackDir;
            _setting.TemplatePath = TemplatePath;
            _setting.SteelTemplatePath = SteelTemplatePath;
            _qcCmmFileSettingRepository.Save(_setting);
        }


        public string GetCADList(int QCTaskID)
        {
            QCTask _qcTask = _qcTaskRepository.QueryByID(QCTaskID);
            EDMDetail _edmDetail = _edmDetailRepository.EDMDetails.Where(e => e.SettingName == _qcTask.TaskName)
                .Where(e => e.Version == _qcTask.Version).FirstOrDefault();

            return _edmDetail.CADDetail;
        }


        public void SetQCTaskStart(int QCTaskID)
        {
            QCTask _qcTask = _qcTaskRepository.QueryByID(QCTaskID);
            _qcTask.State = (int)QCStatus.测量结束;
            _qcTask.StartTime = DateTime.Now;
            _qcTaskRepository.Save(_qcTask);
        }


        public void SaveSteelReport(int QCTaskID, string ReportName, string UserName, string ComputerName, int ReportType = 0)
        {

            QCCmmReport _report = new QCCmmReport();
            _report.QCTaskID = QCTaskID;
            _report.ReportName = ReportName;
            _report.CreateBy = UserName;
            _report.CreateComputer = ComputerName;
            _report.CreateDate = DateTime.Now;
            _report.ReportType = ReportType;
            _qcCmmReportRepository.Save(_report);
        }


        public ActionResult GetQCReport(int QCTaskID, int ReportType = 0)
        {
            IEnumerable<QCCmmReport> _report = _qcCmmReportRepository.QueryByQCTaskID(QCTaskID, ReportType);
            return Json(_report, JsonRequestBehavior.AllowGet);
        }

        public void DeleteQCReport(int QCCmmReportID)
        {
            _qcCmmReportRepository.Delete(QCCmmReportID);
        }
        #endregion

        #region 电极确认
        public ActionResult GetEleQCResult(int Status = 10, string Keyword = "")
        {
            ViewBag.Status = Status;
            ViewBag.Keyword = Keyword;
            return View();
        }

        public ActionResult JsonEleInfo(int Status = 10, string Keyword = "")
        {

            IEnumerable<CNCItem> _cncItems;

            if (Keyword != "")
            {
                _cncItems = _cncItemRepository.CNCItems.Where(c => c.Status == Status).Where(c => c.LabelName.Contains(Keyword)).Take(50);
            }
            else
            {
                _cncItems = _cncItemRepository.CNCItems.Where(c => c.Status == Status).Where(c => c.LabelName.Contains(Keyword)).Take(50);
            }
            //_cncItems.Take(50);
            ElectrodeGridViewModel _model = new ElectrodeGridViewModel(_cncItems, _taskRepository, _qcTaskRepository);
            return Json(_model, JsonRequestBehavior.AllowGet);
        }


        public void ElectrodeConfirm(int CNCItemID, double ZCompensation, double GapCompensation, bool Result)
        {
            CNCItem _item = _cncItemRepository.QueryByID(CNCItemID);
            _item.ZCompensation = ZCompensation;
            _item.GapCompensation = GapCompensation;

            if (Result)
            {
                _item.Status = (int)CNCStatus.在EDM部门;
            }
            else
            {
                _item.Status = (int)CNCStatus.返工;
            }
            _cncItemRepository.Save(_item);
        }



        #endregion

        #region 电极仓库操作

        #region 页面显示
        /// <summary>
        /// 电极仓库
        /// </summary>
        /// <returns></returns>
        public ActionResult EleInStock(string MoldNumber = "")
        {
            ViewBag.MoldNumber = MoldNumber;
            return View();
        }



        #endregion

        #region Josn Data
        public ActionResult JsonEleInStock(string MoldNumber)
        {
            IEnumerable<CNCItem> _cncItems = _cncItemRepository.CNCItems.Where(c => c.Status == (int)CNCStatus.已入库)
                .Where(c => c.MoldNumber == MoldNumber);
            ElectrodeStockGridViewModel _viewModel = new ElectrodeStockGridViewModel(_cncItems);
            return Json(_viewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonMoldNumber(string Keyword = "")
        {
            IEnumerable<CNCItem> _moldNumbers = _cncItemRepository.CNCItems.Where(c => c.MoldNumber != "")
                .Where(c => c.Status == (int)CNCStatus.已入库);

            if (Keyword != "")
            {
                _moldNumbers = _moldNumbers.Where(m => m.MoldNumber.Contains(Keyword));
            }

            IEnumerable<string> _molds = _moldNumbers.Select(c => c.MoldNumber).Distinct().OrderBy(c => c);
            return Json(_molds, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 数据操作
        public string EleStockChange(string LabelNames, int OperationType)
        {
            string[] _labels = LabelNames.Split(',');
            string _fails = "";
            string _errorMsg = "";
            int _state = 0;
            if (OperationType == 1)
            {
                _state = (int)CNCStatus.已入库;
                _errorMsg = "入库";
            }
            else if (OperationType == 2)
            {
                _state = (int)CNCStatus.在EDM部门;
                _errorMsg = "出库";
            }
            if (_state > 0)
            {
                for (int i = 0; i < _labels.Length; i++)
                {
                    //CNCItem _item = _cncItemRepository.QueryByLabel(_labels[i]);
                    CNCItem _item = _cncItemRepository.QueryByELE_IndexCode(_labels[i]);
                    if (_item != null)
                    {
                        if (_item.Status != _state)
                        {
                            _item.Status = _state;
                            try
                            {
                                _cncItemRepository.Save(_item);
                            }
                            catch
                            {
                                _fails = _fails == "" ? _labels[i] + _errorMsg + "失败" : _fails + "," + _labels[i] + _errorMsg + "失败";
                            }
                        }
                        else
                        {
                            _fails = _fails == "" ? _labels[i] + "已经处于" + _errorMsg + "状态" : _fails + "," + "已经处于" + _errorMsg + "状态";
                        }

                    }
                    else
                    {
                        _fails = _fails == "" ? _labels[i] + ":系统中未找到对应电极" : _fails + "," + _labels[i] + ":系统中未找到对应电极";
                    }
                }
            }


            return _fails;
        }

        public string ElectrodeDestroy(string CNCItemIDs)
        {
            string _fails = "";

            string[] _itemIDs = CNCItemIDs.Split(',');
            for (int i = 0; i < _itemIDs.Length; i++)
            {
                CNCItem _item = _cncItemRepository.QueryByID(Convert.ToInt32(_itemIDs[i]));
                if (_item != null)
                {
                    try
                    {
                        _item.Status = (int)CNCStatus.已销毁;
                        _item.DestroyTime = DateTime.Now;
                        _item.Destroy = true;
                        _cncItemRepository.Save(_item);
                    }
                    catch
                    {
                        _fails = _fails == "" ? _item.LabelName + "销毁失败" : _fails + "," + _item.LabelName + "销毁失败";
                    }

                }
                else
                {
                    //_fails = _fails == "" ? _labels[i] + ":系统中未找到对应电极" : _fails + "," + _labels[i] + ":系统中未找到对应电极";
                }
            }
            return _fails;
        }
        #endregion
        #endregion

        #region fun
        public string ElectrodeFinish(string CNCItemIDs, int UserID)
        {
            string _fails = "";

            string[] _itemIDs = CNCItemIDs.Split(',');
            for (int i = 0; i < _itemIDs.Length; i++)
            {
                CNCItem _item = _cncItemRepository.QueryByID(Convert.ToInt32(_itemIDs[i]));
                if (_item != null)
                {
                    try
                    {
                        _item.Status = (int)CNCStatus.CNC结束;
                        _item.CNCFinishTime = DateTime.Now;
                        _cncItemRepository.Save(_item);
                    }
                    catch
                    {
                        _fails = _fails == "" ? _item.LabelName + "点检失败" : _fails + "," + _item.LabelName + "点检失败";
                    }

                    try
                    {
                        int _count = _cncItemRepository.CNCItems
                            .Where(c => c.TaskID == _item.TaskID)
                            .Where(c => c.Status != (int)CNCStatus.CNC结束)
                            .Count();

                        if (_count == 0)
                        {
                            Task _task = _taskRepository.QueryByTaskID(_item.TaskID);
                            _task.State = (int)CNCStatus.CNC结束;
                            _task.FinishTime = DateTime.Now;
                            _task.WorkshopUser = UserID;
                            _taskRepository.Save(_task);
                            #region 电极点检 工时结束
                            User _user = _userRepository.GetUserByID(UserID) ?? new User();
                            EndTaskHour(_task.TaskID,0, _user.FullName);
                            #endregion
                        }
                    }
                    catch
                    {

                    }

                }
                else
                {
                    //_fails = _fails == "" ? _labels[i] + ":系统中未找到对应电极" : _fails + "," + _labels[i] + ":系统中未找到对应电极";
                }
            }
            return _fails;
        }

        public string SteelFinish(string TaskIDs, int UserID)
        {
            string[] _tasks = TaskIDs.Split(',');
            string _fails = "";
            for (int i = 0; i < _tasks.Length; i++)
            {
                Task _task = _taskRepository.QueryByTaskID(Convert.ToInt32(_tasks[i]));
                try
                {
                    _task.State = (int)SteelStatus.CNC结束;
                    _task.FinishTime = DateTime.Now;
                    _task.WorkshopUser = UserID;
                    _taskRepository.Save(_task);
                    #region CNC任务 工时结束
                    User _user = _userRepository.GetUserByID(UserID) ?? new User();
                    EndTaskHour(_task.TaskID,0, _user.FullName);                 
                    #endregion
                }
                catch
                {
                    _fails = _fails == "" ? _task.TaskName : _fails + "," + _task.TaskName;
                }
            }
            return _fails;
        }


        public string CheckEleExist(int TaskID, int Type)
        {
            Task _task = _taskRepository.QueryByTaskID(TaskID);
            if (Type == 1)
            {
                if (_task.R > 0)
                {
                    return "";
                }
                else
                {
                    return "No";
                }
            }
            else
            {
                if (_task.F > 0)
                {
                    return "";
                }
                else
                {
                    return "No";
                }
            }
        }


        public bool TaskReleased(string TaskName, int Version)
        {
            Task _task = _taskRepository.QueryByNameVersion(TaskName, Version);
            if (_task != null)
            {
                if (_task.State == (int)CNCStatus.未发布)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        [HttpPost]
        public ActionResult DownloadProgram(string Program, int TaskID, string FileName)
        {

            string _fileName = "";
            if (FileName == "O0001.iso")
            {
                _fileName = FileName;
            }
            else
            {
                Task _task = _taskRepository.QueryByTaskID(TaskID);
                if (_task != null)
                {
                    _fileName = _task.TaskName + FileName;
                }
            }

            byte[] _pgm = System.Text.Encoding.Default.GetBytes(Program);
            return File(_pgm, "application/octet-stream", _fileName);
        }

        // TODO: 加工界面 模号列表 数据源
        /// <summary>
        /// 模具号列表
        /// </summary>
        /// <param name="State"></param>
        /// <param name="TaskType"></param>
        /// <param name="CAM"></param>
        /// <param name="Keyword"></param>
        /// <returns></returns>
        public ActionResult GetMoldNumberList(int State = 0, int TaskType = 1, int CAM = 0, string Keyword = "")
        {
            IEnumerable<string> _moldNumbers;
            //图纸
            if (CAM == 1)
            {
                #region 当前
                if (State == -99)
                {
                    ///CAM current tasks
                    _moldNumbers = _taskRepository.Tasks
                        .Where(t => t.TaskType == TaskType)
                        .Where(t => t.State == (int)CNCStatus.未发布)
                        .Where(t => t.Enabled == true)
                        .Select(t => t.MoldNumber.Trim()).Distinct();
                }
                #endregion
                #region 历史
                else
                {
                    ///CAM history tasks
                    _moldNumbers = _taskRepository.Tasks
                        .Where(t => t.TaskType == TaskType)
                        .Where(t => t.State > (int)CNCStatus.未发布 && t.State < (int)CNCStatus.完成)
                        .Where(t => t.Enabled == true)
                        .Select(t => t.MoldNumber.Trim()).Distinct();
                }
                #endregion
            }
            //加工
            else
            {
                #region 当前
                if (State == 0)
                {
                    //Machine current tasks
                    switch (TaskType)
                    {
                        case 1:
                            _moldNumbers = _taskRepository.Tasks
                                .Where(t => t.TaskType == TaskType)
                                .Where(t => t.State >= (int)CNCStatus.暂停 && t.State < (int)CNCStatus.CNC结束)
                                .Where(t => t.Enabled == true)
                                .Select(t => t.MoldNumber.Trim()).Distinct();
                            break;
                        case 4:
                            _moldNumbers = _taskRepository.Tasks
                                .Where(t => t.TaskType == TaskType)
                                .Where(t => t.State >= (int)CNCStatus.暂停 && t.State < (int)CNCStatus.CNC结束)
                                .Where(t => t.Enabled == true)
                                .Select(t => t.MoldNumber.Trim()).Distinct();
                            break;
                        default:
                            _moldNumbers = _taskRepository.Tasks
                                .Where(t => t.TaskType == TaskType)
                                .Where(t => t.State >= (int)CNCStatus.暂停 && t.State < (int)CNCStatus.完成)
                                .Where(t => t.Enabled == true)
                                .Select(t => t.MoldNumber.Trim()).Distinct();
                            break;
                    }
                    //if (TaskType == 3)
                    //{
                    //    List<int> _state = new List<int>();
                    //    _state.Add((int)WEDMStatus.已接收);
                    //    _state.Add((int)WEDMStatus.等待);
                    //    _state.Add((int)WEDMStatus.暂停);
                    //    _moldNumbers = _taskRepository.Tasks
                    //    .Where(t => _state.Contains(t.State)).Where(t=>t.TaskType==3)
                    //    .Where(t => t.Enabled == true)
                    //    .Select(t => t.MoldNumber.Trim()).Distinct();
                    //}
                    //else
                    //{
                    //    if (TaskType == 4)
                    //    {
                    //        _moldNumbers = _taskRepository.Tasks
                    //            .Where(t => t.TaskType == TaskType)
                    //            .Where(t => t.State >= (int)CNCStatus.等待).Where(t => t.State < (int)CNCStatus.完成)
                    //            .Where(t => t.Enabled == true)
                    //            .Select(t => t.MoldNumber.Trim()).Distinct();
                    //    }
                    //    else
                    //    {
                    //        _moldNumbers = _taskRepository.Tasks
                    //            .Where(t => t.TaskType == TaskType)
                    //            .Where(t => t.State >= (int)CNCStatus.等待).Where(t => t.State < (int)CNCStatus.完成)
                    //            .Where(t => t.Enabled == true)
                    //            .Select(t => t.MoldNumber.Trim()).Distinct();
                    //    }

                    //}
                }
                #endregion
                #region 历史
                else
                {
                    //Machine history tasks
                    switch (TaskType)
                    {
                        case 1:
                            _moldNumbers = _taskRepository.Tasks
                        .Where(t => t.TaskType == TaskType)
                        .Where(t => t.State >= (int)CNCStatus.CNC结束)
                        .Where(t => t.Enabled == true)
                        .Select(t => t.MoldNumber.Trim()).Distinct();
                            break;
                        case 4:
                            _moldNumbers = _taskRepository.Tasks
                        .Where(t => t.TaskType == TaskType)
                        .Where(t => t.State >= (int)CNCStatus.CNC结束)
                        .Where(t => t.Enabled == true)
                        .Select(t => t.MoldNumber.Trim()).Distinct();
                            break;
                        default:
                            _moldNumbers = _taskRepository.Tasks
                        .Where(t => t.TaskType == TaskType)
                        .Where(t => t.State >= (int)CNCStatus.完成)
                        .Where(t => t.Enabled == true)
                        .Select(t => t.MoldNumber.Trim()).Distinct();
                            break;

                    }
                }
                #endregion
            }

            _moldNumbers = _moldNumbers.Where(m => m != null);

            if (_moldNumbers != null)
            {

                _moldNumbers = _moldNumbers.Where(m => m.Contains(Keyword)).OrderBy(m => m);
            }


            return Json(_moldNumbers, JsonRequestBehavior.AllowGet);
        }

        public string JsonQCReport(int CNCItemID)
        {
            CNCItem _item = _cncItemRepository.QueryByID(CNCItemID);
            string _moldNumber = _item.MoldNumber;
            string _itemName = _item.LabelName;
            QCTask _qcTask = _qcTaskRepository.QCTasks
                .Where(q => q.TaskName == _itemName)
                .Where(q => q.Enabled == true)
                .FirstOrDefault();
            try
            {
                string _picPath = _item.LabelName.Replace('/', '_');
                _picPath = "/Tooling exp01/05Quality/QC CMM Reports/EleReports/" + _moldNumber + "/" + _picPath + ".bmp";
                return _picPath;
            }
            catch
            {
                return "";
            }
            
        }

        public string ReaddManualTask(int TaskID, string Department)
        {
            Task _task = _taskRepository.QueryByTaskID(TaskID);
            Task _newTask = new Task();
            _newTask.DrawingFile = _task.DrawingFile;
            _newTask.TaskName = _task.TaskName;
            _newTask.Version = _task.Version;
            _newTask.ProcessName = _task.ProcessName;
            _newTask.Model = _task.Model;
            _newTask.R = _task.R;
            _newTask.F = _task.F;
            _newTask.HRC = _task.HRC;
            _newTask.Material = _task.Material;
            _newTask.Time = _task.Time;
            _newTask.Raw = _task.Raw;
            _newTask.Prepared = _task.Prepared;
            _newTask.Priority = _task.Priority;
            _newTask.Quantity = _task.Quantity;
            _newTask.ForecastTime = _task.ForecastTime;
            _newTask.AcceptTime = _task.AcceptTime;
            _newTask.CreateTime = _task.CreateTime;
            _newTask.PlanTime = _task.PlanTime;
            _newTask.StartTime = _task.StartTime;
            _newTask.Memo = "Created by" + Department;
            _newTask.StateMemo = _task.StateMemo;
            _newTask.State = 0;
            _newTask.ProjectID = _task.ProjectID;
            _newTask.CADUser = _task.CADUser;
            _newTask.Creator = _task.Creator;
            _newTask.CAMUser = _task.CAMUser;
            _newTask.WorkshopUser = _task.WorkshopUser;
            _newTask.QCUser = _task.QCUser;
            _newTask.TaskType = _task.TaskType;
            _newTask.ReleaseTime = _task.ReleaseTime;
            _newTask.FinishTime = _task.FinishTime;
            _newTask.PositionFinish = _task.PositionFinish;
            _newTask.QCInfoFinish = _task.QCInfoFinish;
            _newTask.ProgramID = _task.ProgramID;
            _newTask.OldID = _task.OldID;
            _newTask.MoldNumber = _task.MoldNumber;
            _newTask.Enabled = _task.Enabled;
            _newTask.PrevState = _task.PrevState;
            _taskRepository.Save(_newTask);
            return "";
        }

        public string CreateNewTaskByCAM(string TaskIDs)
        {
            string[] _taskIDs = TaskIDs.Split(',');
            string _dept = "";
            try
            {
                _dept = Request.Cookies["User"]["DepartmentName"];
            }
            catch
            {
                _dept = "";
            }
            for (int i = 0; i < _taskIDs.Length; i++)
            {
                Task _task = _taskRepository.QueryByTaskID(Convert.ToInt32(_taskIDs[i]));
                Task _newTask = new Task();
                switch (_task.TaskType)
                {
                    case 1:
                        ReaddCNCTask(_taskIDs[i], _task.R, _task.F);
                        break;
                    case 2:
                        EDMDetail _detail = _edmDetailRepository.QueryByTaskID(_task.TaskID);
                        ReaddEDMTask(_task.TaskID, _task.TaskName, _task.Version.ToString(), _detail.EleDetail, _detail.CADDetail, "Created by CAM", 0);
                        break;
                    case 3:
                        ReaddManualTask(_task.TaskID, _dept);
                        break;
                    case 4:
                        ReaddManualTask(_task.TaskID, _dept);
                        break;
                    case 6:
                        ReaddManualTask(_task.TaskID, _dept);
                        break;
                }
            }
            return "";
        }

        public ActionResult EDMEleList(int TaskID)
        {
            List<string> _eleList = new List<string>();
            string[] _detail = _edmDetailRepository.QueryByTaskID(TaskID).EleDetail.Split(';');
            _eleList.AddRange(_detail);
            return Json(_eleList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EDMItemList(int TaskID)
        {
            List<string> _eleList = new List<string>();
            string[] _detail = _edmDetailRepository.QueryByTaskID(TaskID).CADDetail.Split(';');
            _eleList.AddRange(_detail);
            return Json(_eleList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NextDepartment(int DepartmentID)
        {
            List<int> _nextDepts = new List<int>();
            IEnumerable<Department> _departments = _departmentRepository.Departments.Where(d => d.Enabled == true)
                .Where(d => d.DepartmentID >= 5).Where(d => d.DepartmentID <= 11).Where(d => d.DepartmentID != 10);
            return Json(_departments, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonUsers(int DepartmentID)
        {
            IEnumerable<User> _users = _userRepository.Users.Where(u => u.Enabled == true).Where(u => u.DepartmentID == DepartmentID);
            return Json(_users, JsonRequestBehavior.AllowGet);
        }


        public string UpdateProgramID()
        {
            IEnumerable<SteelCAMDrawing> _camdrawings = _steelCAMDrawingRepository.SteelCAMDrawings;
            foreach (SteelCAMDrawing _drawing in _camdrawings){
                Task _task = _taskRepository.QueryByNameVersion(_drawing.FullPartName, _drawing.DrawREV, 4, true);
                try
                {
                    _task.ProgramID = _drawing.SteelCAMDrawingID;
                    _taskRepository.Save(_task);
                }
                catch
                {

                }
            }
            return "Success";
        }

        /// <summary>
        /// Provide data for EDM tasks to check electrode status
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public ActionResult EleStateCheck(int TaskID)
        {
            EDMDetail _edmDetail = _edmDetailRepository.QueryByTaskID(TaskID);
            string[] _eles = _edmDetail.EleDetail.Split(';');
            List<Task> _cnctasks = new List<Task>();
            List<string> _val = new List<string>();
            string state="";
            for (int i = 0; i < _eles.Length; i++)
            {
                string _cncname = _eles[i].Substring(0, _eles[i].IndexOf("_V"));
                int _version = Convert.ToInt16(_eles[i].Substring(_eles[i].IndexOf("_V") + 2));
                _cnctasks.Add(_taskRepository.QueryByNameVersion(_cncname, _version, 1));
            }
            foreach (Task _cnctask in _cnctasks)
            {
                _val.Add(_cnctask.TaskName);
                List<CNCItem> _cncitems = _cncItemRepository.QueryByTaskID(_cnctask.TaskID).ToList();
                foreach (CNCItem _item in _cncitems)
                {
                    switch (_item.Status){
                        case 0:
                            state = "在CNC";
                            break;
                        case 1:
                            state = "在CNC";
                            break;
                        case 2:
                            state = "CNC结束";
                            break;
                        case 4:
                            state = "入库";
                            break;
                        case 5:
                            state = "返工";
                            break;
                        case 6:
                            state = "EDM待确认";
                            break;
                    }
                    _val.Add("    " + _item.LabelName + "=====" + state);
                }
            }
            return Json(_val, JsonRequestBehavior.AllowGet);
        }

        public string GetTaskPDF(int TaskID)
        {
            Task _task = _taskRepository.QueryByTaskID(TaskID);
            string _moldNumber = _task.MoldNumber;
            string _fileName = _taskRepository.QueryByTaskID(TaskID).DrawingFile;
            string _path = GetTaskDrawingPath();
            
            string _port = Request.ServerVariables["SERVER_PORT"] == "80" ? "" : ":" + Request.ServerVariables["SERVER_PORT"];
            string _server = Request.ServerVariables["SERVER_NAME"]+_port;

            return "http://"+_server+"/File" + _path + _moldNumber + "/" + _fileName+ ".pdf";

        }
        #endregion

        #region MG、WEDM INterface
        [HttpPost]
        public int SaveService_MGCAMSetting(String entity)
        {
            MGSetting _entity = JsonConvert.DeserializeObject<MGSetting>(entity);
            return _mgSettingRepository.Save(_entity);
        }
        public ActionResult GetService_MGTypeName()//List<MGTypeName>
        {
            return Json(_mgSettingRepository.GetMGTypeName(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DelByNameService_MGCAMSetting(string partname, int rev)//bool
        {
            return Json(_mgSettingRepository.DeleteSettingByName(partname, rev), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReleaseMGDrawingService(int DrawIndex, string ReleaseBy,string TaskName)//int
        {
            return Json(_mgSettingRepository.ReleaseMGDrawing(DrawIndex, ReleaseBy, TaskName), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetService_MGTypeMold(string MoldNo, bool bRelease)//List<MGSetting>
        {
            return Json(_mgSettingRepository.GetMGPartListByMold(MoldNo, bRelease), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDrawFileByDrawName(string DrawName, bool IsContain2D, string DrawType)//string
        {
            return Json(_mgSettingRepository.GetDrawFileByDrawName(DrawName, IsContain2D, DrawType), JsonRequestBehavior.AllowGet);
        }
        public ActionResult IsLatestDrawFile(string DrawName, bool IsContain2D)//bool
        {
            return Json(_mgSettingRepository.IsLatestDrawFile(DrawName, IsContain2D), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int SaveService_WEDMCAMSetting(string entity)
        {
            WEDMSetting _entity = JsonConvert.DeserializeObject<WEDMSetting>(entity);
            return _wedmSettingRepository.Save(_entity);
        }
        public ActionResult DelByNameService_WEDMCAMSetting(string partname, int rev)//bool
        {
            return Json(_wedmSettingRepository.DeleteSettingByName(partname, rev), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReleaseWEDMDrawingService(int DrawIndex, string ReleaseBy, int Qty = 1)//int
        {
            return Json(_wedmSettingRepository.ReleaseWEDMDrawing(DrawIndex, ReleaseBy, Qty), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetService_WEDMTaskByMoldAndStatus(string MoldNo, int Status = -2, int PlanID = 0)//List<Task>
        {
            return Json(_wedmSettingRepository.GetWEDMTaskInfoByMoldAndStatus(MoldNo, Status, PlanID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetService_WDMCutSpeed(double Thickness, int CutTypeID)//WEDMCutSpeed
        {
            return Json(_wedmSettingRepository.GetWDMCutSpeed(Thickness, CutTypeID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetService_Precision()//List<WEDMPrecision>
        {
            return Json(_wedmSettingRepository.GetWEDMPrecision(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetService_3DDrawingServerPath()//string
        {
            return Json(_wedmSettingRepository.Get3DDrawingServerPath(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region MG升版老任务
        public ActionResult AddMGTask()
        {
            return View();
        }
        [HttpPost]
        public JsonResult JsonMGUptTaskList(string TaskIDs)
        {
            MGUptVerGridViewModel _list = new MGUptVerGridViewModel(TaskIDs, _userRepository, _taskRepository);
            return Json(_list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Service_MGTaskUptVer(List<MGTaskUptVerViewModel> _viewmodel)
        {
            string res = "";
            string _taskIDs = "";
            try
            {
                if (_viewmodel.Count > 0)
                {
                    Task _duptask = new Task();
                    int dbMaxVer = 0;                  
                    foreach (var t in _viewmodel)
                    {                      
                        _taskIDs = _taskIDs + t.TaskID.ToString() + " ;";
                        int type = t.Technology == "铣床" ? 1 : t.Technology == "磨床" ? 2 : t.Technology == "铣磨" ? 3 : 4;
                        _duptask = DuplicateTask(t.TaskID);
                        string _taskname = _duptask.TaskName;
                        string FullName = HttpUtility.UrlDecode(Request.Cookies["User"]["FullName"], Encoding.GetEncoding("UTF-8"));
                        #region 检查数据合法性
                        //是否存在正在进行（State 5 进行中；7 外发）的任务
                        Task _task1 = _taskRepository.Tasks.Where(z => z.TaskName == _taskname && z.Enabled == true && z.TaskType == 6 && z.State >= (int)CNCStatus.暂停 && z.State<(int)CNCStatus.完成).FirstOrDefault() ?? new Task();
                        if (_task1.TaskID > 0)
                        {
                            res = res + _task1.TaskName+"; ";
                            break;
                        }
                        #endregion

                        #region 升版任务
                        #region 铣磨
                        if (type == 3)
                        {
                            for(int i = 1; i < 3; i++)
                            {                              
                                dbMaxVer = _taskRepository.GetMaxVerMGTask(_duptask.TaskName,i);
                                _duptask.DrawingFile = t.DrawingFile ?? "";
                                _duptask.CreateTime = DateTime.Now;
                                _duptask.ReleaseTime = DateTime.Now;
                                _duptask.Creator = (_userRepository.GetUserByName(FullName ?? "")??new User()).UserID;
                                _duptask.Version = dbMaxVer + 1;//////
                                _duptask.ProcessName = i == 1 ? "铣床" : "磨床";
                                _duptask.OldID= i == 1 ? 1 :2;
                                _duptask.TaskType = 6;
                                _duptask.Memo = "升版任务(" + t.TaskID.ToString() + ")";
                                //状态 等待
                                _duptask.State = (int)CNCStatus.等待;
                                _taskRepository.AddNewTask(_duptask);
                            }                          
                        }
                        #endregion
                        #region others
                        else
                        {
                            dbMaxVer = _taskRepository.GetMaxVerMGTask(_duptask.TaskName,type);
                            _duptask.DrawingFile = t.DrawingFile ?? "";
                            _duptask.CreateTime = DateTime.Now;
                            _duptask.ReleaseTime = DateTime.Now;
                            _duptask.Creator = (_userRepository.GetUserByName(FullName ?? "") ?? new User()).UserID;
                            _duptask.Version = dbMaxVer + 1;//////
                            _duptask.ProcessName = type == 1 ? "铣床" : type == 2 ? "磨床" : "车";
                            _duptask.OldID = type == 1 ? 1 : 2;
                            _duptask.TaskType = 6;
                            _duptask.Memo = "升版任务(" + t.TaskID.ToString() + ")";
                            //状态 等待
                            _duptask.State = (int)CNCStatus.等待;
                            _taskRepository.AddNewTask(_duptask);
                        }
                        #endregion
                        #endregion
                        #region
                        Task _oldtask = _taskRepository.QueryByTaskID(t.TaskID);
                        _oldtask.State = (int)CNCStatus.已升版;
                        string memo = GetCurrentUser() + "升版任务，升版时间 " + DateTime.Now.ToString("yyMMddHHmm");
                        _oldtask.StateMemo = _oldtask.StateMemo == null ? memo : _oldtask.StateMemo + ";" + memo;
                        _taskRepository.Save(_oldtask);
                        #endregion
                    }
                }
                if (res == "")
                {
                    return Json(new { Code = 0, Message = res }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Code = -1, Message = res }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogRecord("MG任务升版错误日志", "异常任务:" + _taskIDs + "/r/n程序异常原因——" + ex.Message);
                //string logPath = Server.MapPath("~/Log/") + "MG任务升版错误日志" + "_" + DateTime.Now.ToString("yyMMddHHmmss") + ".txt";
                //Toolkits.WriteLog(logPath,"异常任务:"+ _taskIDs + "程序异常原因——" +ex.Message);
                return Json(new { Code = -2, Message = _taskIDs, Ex=ex.Message }, JsonRequestBehavior.AllowGet); 
            }
        }
        #endregion
        #region 日志记录
        public void LogRecord(string filename,string content)
        {
            string logPath = Server.MapPath("~/Log/") + filename + "_" + DateTime.Now.ToString("yyMMddHHmmss") + ".txt";
            Toolkits.WriteLog(logPath, content);
        }
        #endregion
        #region 任务工时
        /// <summary>
        /// 创建工时记录
        /// </summary>
        /// <param name="_task">任务对象</param>
        /// <param name="RecordType">0 正常开始 1 重启 2外发</param>
        public void CreateTaskHour(Task _task,int RecordType,string MachineCode,string wsUserName="")
        {
            DateTime _iniTime = DateTime.Parse("1900/1/1");
            try
            {
                string _operater= wsUserName == "" ? GetCurrentUser() ?? "" : wsUserName;
                TaskHour _taskhour = new TaskHour();
                _taskhour.TaskID = _task.TaskID;
                _taskhour.Enabled = true;
                _taskhour.StartTime = DateTime.Now;
                _taskhour.FinishTime = _iniTime;
                _taskhour.TaskType = _task.TaskType;
                _taskhour.MoldNumber = _task.MoldNumber;
                _taskhour.Time = 0;
                _taskhour.RecordType = RecordType;
                _taskhour.State = (int)TaskHourStatus.开始;
                _taskhour.Operater = _operater;
                _taskhour.MachineCode = MachineCode ?? "";
                _taskhour.Memo = "记录创建于：" + DateTime.Now.ToString("yyMMddHHmm") + "；操作者：" + _operater + "/r/n";
                _taskHourRepository.Save(_taskhour);
            }
            catch (Exception ex)
            {
                LogRecord("任务工时创建错误日志", "异常任务:" + _task.TaskID.ToString() + "/r/n程序异常原因——" + ex.Message);
            }
        }
        /// <summary>
        /// 结束工时记录
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="Time">外发工时</param>
        /// <param name="djUserName">点检人员</param>
        public void EndTaskHour(int TaskID, decimal Time =0,string djUserName="")
        {
            try
            {
                //当前活动的记录
                TaskHour _taskhour = _taskHourRepository.GetCurTHByTaskID(TaskID);
                string _operater = string.IsNullOrEmpty(_taskhour.Operater)? string.IsNullOrEmpty(djUserName)?GetCurrentUser(): djUserName : _taskhour.Operater;
                if (_taskhour.TaskHourID > 0)
                {
                    _taskhour.FinishTime = DateTime.Now;
                    _taskhour.Enabled = false;
                    _taskhour.State = (int)TaskHourStatus.完成;
                    _taskhour.Operater = _operater;
                    //外发
                    if (_taskhour.RecordType == 2)
                        _taskhour.Time = Time;
                    _taskhour.Memo=_taskhour.Memo+ " 记录结束于：" + DateTime.Now.ToString("yyMMddHHmm") + "；操作者：" + (string.IsNullOrEmpty(djUserName) ? GetCurrentUser() : djUserName) + "/r/n";
                    _taskHourRepository.Save(_taskhour);
                }
            }
            catch(Exception ex)
            {
                LogRecord("任务工时结束错误日志", "异常任务:" + TaskID.ToString() + "/r/n程序异常原因——" + ex.Message);
            }
        }
        /// <summary>
        /// 取消工时记录
        /// </summary>
        /// <param name="TaskID"></param>
        public void CancelTaskHour(int TaskID)
        {
            try
            {
                //当前活动的记录
                TaskHour _taskhour = _taskHourRepository.GetCurTHByTaskID(TaskID);
                if (_taskhour.TaskHourID > 0)
                {
                    _taskhour.Enabled = false;
                    _taskhour.RecordType = -1;
                    _taskhour.State = (int)TaskHourStatus.取消;
                    _taskhour.FinishTime = DateTime.Now;
                    _taskhour.Memo = _taskhour.Memo + " 记录取消于：" + DateTime.Now.ToString("yyMMddHHmm") + "；操作者：" + GetCurrentUser() + "/r/n";
                    _taskHourRepository.Save(_taskhour);
                }
            }
            catch (Exception ex)
            {
                LogRecord("任务工时取消错误日志", "异常任务:" + TaskID.ToString() + "/r/n程序异常原因——" + ex.Message);
            }
        }
        #endregion
        #region 提交 任务(外发、正常)结束 统计工时
        public string Service_Save_wfTaskHour()
        {
            string res = "";
            List<SetupTaskStart> _viewmodel = new List<SetupTaskStart>();
            if (Session["setupTask"] != null)
            {
                _viewmodel = Session["setupTask"] as List<SetupTaskStart>;
            }
            if (_viewmodel.Count > 0)
            {
                foreach(var t in _viewmodel)
                {
                    try
                    {
                        Task _task = _taskRepository.QueryByTaskID(t.TaskID);
                        CreateTaskHour(_task, 2, t.MachinesCode, t.UserName);
                    }
                    catch
                    {
                        res = res + t.TaskID.ToString() + ";";
                    }
                }
                #region 清空 Session
                Session["setupTask"] = null;
                #endregion
            }
            else
            {
                res = "-";
            }
            return res;
        }
        #endregion
    }
}
