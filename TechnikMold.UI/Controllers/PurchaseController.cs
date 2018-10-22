using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridViewModel;
using MoldManager.WebUI.Models.EditModel;
using MoldManager.WebUI.Models.ViewModel;
using MoldManager.WebUI.Models.Helpers;
using MoldManager.WebUI.Tools;
using System.IO;
using System.Net;
using TechnikSys.MoldManager.Domain.Status;
using System.Linq.Expressions;
using Aspose.Cells;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using TechnikSys.MoldManager.UI.Models.ViewModel;
using TechnikMold.UI.Models;
using TechnikMold.UI.Models.ViewModel;

namespace MoldManager.WebUI.Controllers
{
    public class PurchaseController : Controller
    {
        #region 定义参数
        private IPartRepository _partRepository;
        private IPRContentRepository _prContentRepository;
        private IPurchaseRequestRepository _purchaseRequestRepository;
        private ISequenceRepository _sequenceRepository;
        private ISupplierRepository _supplierRepository;
        private IQRSupplierRepository _qrSupplierRepository;
        private IQRQuotationRepository _qrQuotationRepository;
        private IUserRepository _userRepository;
        private PurchaseRequestStatus _status;
        private IPRProcessRepository _prProcessRepository;
        private IContactRepository _contactRepository;
        private IProcessRecordRepository _processRecordRepository;
        private IPurchaseOrderRepository _purchaseOrderRepository;
        private IPOContentRepository _poContentRepository;
        private IProjectRepository _projectRepository;
        private IQuotationRequestRepository _quotationRequestRepository;
        private IQRContentRepository _qrContentRepository;
        private ITaskRepository _taskRepository;
        private ICNCMachInfoRepository _machInfoRepository;
        private IPurchaseItemRepository _purchaseItemRepository;
        private IPurchaseTypeRepository _purchaseTypeRepository;
        private IWarehouseStockRepository _warehouseStockRepository;
        private IDepartmentRepository _departmentRepository;
        private IProjectPhaseRepository _projectPhaseRepository;
        private ISteelCAMDrawingRepository _steelDrawingRepository;
        private ISupplierBrandRepository _supplierBrandRepository;
        private IBrandRepository _brandRepository;
        private ICostCenterRepository _costCenterRepository;
        private IPartListRepository _partListRepository;
        private ITaskHourRepository _taskHourRepository;
        #endregion
        #region 构造
        public PurchaseController(IPartRepository PartRepository,
            IPRContentRepository PRContentRepository,
            IPurchaseRequestRepository PurchaseRequestRepository,
            ISequenceRepository SequenceRepository,
            ISupplierRepository SupplierRepository,
            IQRSupplierRepository QRSupplierRepository,
            IQRQuotationRepository QRQuotationRepository,
            IUserRepository UserRepository,
            IPRProcessRepository PRProcessRepository,
            IContactRepository ContactRepository,
            IProcessRecordRepository ProcessRecordRepository,
            IPurchaseOrderRepository PurchaseOrderRepository,
            IPOContentRepository POContentRepository,
            IProjectRepository ProjectRepository,
            IQuotationRequestRepository QuotationRequestRepository,
            IQRContentRepository QRContentRepository,
            ITaskRepository TaskRepository,
            ICNCMachInfoRepository CNCMachInfoRepository,
            IPurchaseItemRepository PurchaseItemRepository,
            IPurchaseTypeRepository PurchaseTypeRepository,
            IWarehouseStockRepository WarehouseStockRepository,
            IDepartmentRepository DepartmentRepository,
            IProjectPhaseRepository ProjectPhaseRepository,
            ISteelCAMDrawingRepository SteelCAMDrawingRepository,
            ISupplierBrandRepository SupplierBrandRepository,
            IBrandRepository BrandRepository,
            ICostCenterRepository CostCenterRepository,
            IPartListRepository partListRepository,
            ITaskHourRepository taskHourRepository)
        {
            _partRepository = PartRepository;
            _prContentRepository = PRContentRepository;
            _purchaseRequestRepository = PurchaseRequestRepository;
            _sequenceRepository = SequenceRepository;
            _supplierRepository = SupplierRepository;
            _qrSupplierRepository = QRSupplierRepository;
            _qrQuotationRepository = QRQuotationRepository;
            _userRepository = UserRepository;
            _prProcessRepository = PRProcessRepository;
            _contactRepository = ContactRepository;
            _processRecordRepository = ProcessRecordRepository;
            _purchaseOrderRepository = PurchaseOrderRepository;
            _poContentRepository = POContentRepository;
            _projectRepository = ProjectRepository;
            _quotationRequestRepository = QuotationRequestRepository;
            _qrContentRepository = QRContentRepository;
            _taskRepository = TaskRepository;
            _machInfoRepository = CNCMachInfoRepository;
            _purchaseItemRepository = PurchaseItemRepository;
            _purchaseTypeRepository = PurchaseTypeRepository;
            _warehouseStockRepository = WarehouseStockRepository;
            _departmentRepository = DepartmentRepository;
            _projectPhaseRepository = ProjectPhaseRepository;
            _steelDrawingRepository = SteelCAMDrawingRepository;
            _supplierBrandRepository = SupplierBrandRepository;
            _brandRepository = BrandRepository;
            _costCenterRepository = CostCenterRepository;
            _partListRepository = partListRepository;
            _status = new PurchaseRequestStatus();
            _taskHourRepository = taskHourRepository;
        }
        #endregion

        #region PageView
        // GET: Purchase
        public ActionResult Index(string MoldNumber = "", string PRKeyword = "", string StartDate = "",
            string FinishDate = "", int Supplier = 0, int PurchaseType = 0, int DepartmentID = 0, int State = 0)
        {
            int _dept;
            int _pos;
            try
            {
                _dept = Convert.ToInt16(Request.Cookies["User"]["Department"]);
            }
            catch
            {
                _dept = 0;
            }


            try
            {
                _pos = Convert.ToInt16(Request.Cookies["User"]["Position"]);
            }
            catch
            {
                _pos = 0;
            }

            if (_dept == 4)
            {
                if (State == 0)
                {
                    State = 3;
                }
            }
            else
            {
                if (_pos == 1)
                {
                    State = 1;
                }
                else
                {
                    State = 2;
                }
            }


            if (DepartmentID == 0)
            {
                ViewBag.Department = _dept;
            }
            else
            {
                ViewBag.Department = DepartmentID;
            }
            ViewBag.MoldNumber = MoldNumber;
            ViewBag.PRKeyword = PRKeyword;
            ViewBag.StartDate = StartDate;
            ViewBag.FinishDate = FinishDate;
            ViewBag.Supplier = Supplier;
            ViewBag.PurchaseType = PurchaseType;
            ViewBag.State = State;
            return View();
        }

        /// <summary>
        /// View/Edit detail PR information
        /// </summary>
        /// <param name="PurchaseRequestID"></param>
        /// <param name="ProjectID"></param>
        /// <param name="PartIDs"></param>
        /// <returns></returns>
        public ActionResult PRDetail(int PurchaseRequestID = 0,//List<SetupTaskStart> _viewmodel,
            string MoldNumber = "",
            string PartIDs = "",
            string TaskIDs = "",
            string WarehouseStockIDs = "",
            int TaskType = 0)
        {
            List<User> ApprovaluserList = _userRepository.Users.Where(u => !string.IsNullOrEmpty(u.UserCode)).ToList() ?? new List<TechnikSys.MoldManager.Domain.Entity.User>();
            //ViewBag.ApprovalUserIDList = new SelectList(ApprovaluserList, "UserCode", "FullName");
            ViewBag.ApprovalUserIDList = ApprovaluserList;
            ViewBag.TaskType = TaskType;
            #region 存在采购申请单号
            if (PurchaseRequestID != 0)
            {
                PurchaseRequest _request = _purchaseRequestRepository.GetByID(PurchaseRequestID);
                ViewBag.RequestNumber = _request.PurchaseRequestNumber;
                ViewBag.Title = "申请单详情";
                //ViewBag.ProjectID = _request.ProjectID;
                //ViewBag.MoldNumber = _projectRepository.GetByID(_request.ProjectID).MoldNumber;
                ViewBag.PartIDs = "";
                ViewBag.PurchaseRequestID = PurchaseRequestID;
                ViewBag.PRState = _request.State;
                ViewBag.TaskIDs = "";
                ViewBag.State = _request.State;
                string ApprolUserID = _request.ApprovalERPUserID ?? "";
                User ApprolUser = _userRepository.Users.Where(u => !string.IsNullOrEmpty(u.UserCode)).Where(u => u.UserCode == ApprolUserID).FirstOrDefault() ?? new TechnikSys.MoldManager.Domain.Entity.User();
                ViewBag.ApprovalUserName = ApprolUser.FullName ?? "";
                User CreUser = _userRepository.Users.Where(u => u.UserID == _request.UserID).FirstOrDefault() ?? new User();
                ViewBag.CreUserName = CreUser.FullName ?? "";
                //外发申请单 页面添加外发类型
                PRContent _prcontent = _prContentRepository.QueryByRequestID(PurchaseRequestID).FirstOrDefault() ?? new PRContent();
                if (_prcontent.TaskID > 0)
                {
                    Task _task = _taskRepository.QueryByTaskID(_prcontent.TaskID);
                    ViewBag.TaskType = _task.TaskType;
                }
                try
                {
                    PurchaseType _purchaseType = _purchaseTypeRepository.QueryByID(_request.PurchaseType);
                    ViewBag.PurchaseTypeID = _purchaseType.PurchaseTypeID;
                    ViewBag.PurchaseType = _purchaseType.Name;
                }
                catch
                {
                    ViewBag.PurchaseTypeID = 0;
                    ViewBag.PurchaseType = "";
                }                
                return View(_request);
            }
            #endregion
            #region 不存在PR单号、存在模具号
            else if (MoldNumber != "")
            {
                ViewBag.Title = "新建申请单";
                try
                {
                    ViewBag.ProjectID = _projectRepository.QueryByMoldNumber(MoldNumber).ProjectID;
                }
                catch
                {
                    ViewBag.ProjectID = 0;
                }
                ViewBag.PurchaseRequestID = 0;
                ViewBag.PRState = 0;
                ViewBag.MoldNumber = MoldNumber;
                ViewBag.WarehouseStockIDs = "";
                ViewBag.State = 0;
                #region 来源：Partlist
                if (PartIDs != "")
                {
                    ViewBag.PartIDs = PartIDs;
                    ViewBag.TaskIDs = "";
                    ViewBag.WarehouseStockIDs = "";
                }
                #endregion
                #region 来源：任务外发
                else if (TaskIDs != "")
                {
                    ViewBag.PartIDs = "";
                    ViewBag.TaskIDs = TaskIDs;
                    ViewBag.WarehouseStockIDs = "";
                    //ViewBag.setupTaskModel = _viewmodel;
                    //if(_viewmodel.Count>0)
                        //Session["setupTask"] = _viewmodel;
                }
                #endregion
                return View();
            }
            #endregion
            #region 不存在PR单号、不存在模具号
            else
            {
                ViewBag.Title = "新建申请单";
                //List<User> ApprovaluserList = _userRepository.Users.Where(u => !string.IsNullOrEmpty(u.UserCode)).ToList() ?? new List<TechnikSys.MoldManager.Domain.Entity.User>();
                //ViewBag.ApprovalUserIDList = new SelectList(ApprovaluserList, "UserCode", "FullName");
                ViewBag.ProjectID = 0;
                ViewBag.PartIDs = "";
                ViewBag.PurchaseRequestID = 0;
                ViewBag.PRState = 0;
                ViewBag.TaskIDs = "";
                ViewBag.State = 0;
                if (WarehouseStockIDs != "")
                {
                    ViewBag.WarehouseStockIDs = WarehouseStockIDs;
                }
                else
                {
                    ViewBag.WarehouseStockIDs = "";
                }
                return View();
            }
            #endregion
        }
        /// <summary>
        /// 接收外发任务机器、人员等信息 并设置到Session
        /// </summary>
        /// <param name="_viewmodel"></param>
        /// <returns></returns>
        [HttpPost]
        public string AccsetupTaskData(List<SetupTaskStart> _viewmodel)
        {
            if (Session["setupTask"] == null)
                Session["setupTask"] = null;
            if (_viewmodel.Count > 0)
                Session["setupTask"] = _viewmodel;
            return "";
        }
        //public ActionResult AccOutSourceData(List<SetupTaskStart> _viewmodel, string MoldNumber, string TaskIDs = "")
        //{
        //    return RedirectToAction("PRDetail", "Purchase", new { _viewmodel = _viewmodel, MoldNumber = MoldNumber, TaskIDs = TaskIDs });
        //}
        ///// <summary>
        ///// Display the Quotation input page
        ///// </summary>
        ///// <param name="PurchaseRequestID"></param>
        ///// <param name="PRSupplierID"></param>
        ///// <returns></returns>
        //public ActionResult PRQuotation(int PurchaseRequestID, int PRSupplierID = 0)
        //{
        //    List<QRQuotationEditModel> _prQuotationModels = new List<QRQuotationEditModel>();
        //    IEnumerable<QRContent> _prContents = _q.QueryByRequestID(PurchaseRequestID);
        //    //IEnumerable<PRContent> _prContents = _prContentRepository.PRContents.Where(p => p.PurchaseRequestID == PurchaseRequestID);
        //    if (PRSupplierID == 0)
        //    {

        //        foreach (PRContent _content in _prContents)
        //        {
        //            _prQuotationModels.Add(new QRQuotationEditModel(_content));
        //        }
        //    }
        //    else
        //    {
        //        IEnumerable<QRQuotation> _qrQuotations = _prQuotationRepository.QRQuotations.
        //            Where(p => p.QuotationRequestID == PurchaseRequestID).Where(p => p.SupplierID == PRSupplierID);
        //        if (_qrQuotations != null)
        //        {
        //            foreach (PRContent _content in _prContents)
        //            {
        //                QRQuotation _quotation = _qrQuotations.Where(p => p.PRContentID == _content.PRContentID).FirstOrDefault();
        //                _prQuotationModels.Add(new QRQuotationEditModel(_content, _quotation));
        //            }
        //        }
        //    }

        //    ViewBag.PRSupplierID = PRSupplierID;

        //    ViewBag.PurchaseRequestID = PurchaseRequestID;
        //    return View(_prQuotationModels);
        //}

        /// <summary>
        /// Supplier management page
        /// </summary>
        /// <returns></returns>
        public ActionResult Suppliers(int SupplierID = 0)
        {
            IEnumerable<Supplier> _suppliers = _supplierRepository.Suppliers.Where(s => s.Enabled == true);

            SupplierGridViewModel _model = new SupplierGridViewModel(_suppliers);
            return View();
        }


        public ActionResult JsonQuotation(int QuotationID, int SupplierID)
        {
            IEnumerable<QRQuotation> _result = _qrQuotationRepository.QRQuotations.Where(q => q.QuotationRequestID == QuotationID)
                .Where(q => q.SupplierID == SupplierID).Where(q => q.Enabled == true);
            return Json(_result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult QuotationSummary(int QuotationRequestID)
        {
            List<Supplier> _suppliers = new List<Supplier>();
            int _state = _quotationRequestRepository.GetByID(QuotationRequestID).State;
            IEnumerable<QRSupplier> _prSuppliers = _qrSupplierRepository.QueryByQRID(QuotationRequestID);
            foreach (QRSupplier _supplier in _prSuppliers)
            {
                _suppliers.Add(_supplierRepository.QueryByID(_supplier.SupplierID));
            }

            IEnumerable<QRContent> _qrContents = _qrContentRepository.QueryByQRID(QuotationRequestID).OrderBy(q => q.SupplierID);
            IEnumerable<QRQuotation> _qrQuotations = _qrQuotationRepository.QueryByQRID(QuotationRequestID);
            QRQuotationSummaryViewModel _viewModel = new QRQuotationSummaryViewModel(QuotationRequestID, _state, _qrContents, _qrQuotations, _suppliers, _prSuppliers);
            return View(_viewModel);
        }

        #endregion

        #region HttpPost Actions


        /// <summary>
        /// Stage 1:Create/Update PR
        /// 1. Create the purchase request(PR) 
        /// 2. Create purchase request contents
        /// 3. Update the parts information(in purchase progress or not)
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="PRContents"></param>
        /// <returns></returns>
        [HttpPost]
        public int PRSave(List<PRContent> PRContents, int PurchaseType, int PurchaseRequestID = 0, int SupplierID = 0, string Memo = "",string ApprovalERPUserID="",int wsUserID=0)
        {
            int _requestID;
            PurchaseRequest _request;
            DateTime _requireDate;
            int DepartmentID = 0;

            _requireDate = PRContents.Where(p => p.RequireTime != new DateTime(1900, 1, 1))
                            .OrderBy(p => p.RequireTime)
                            .Select(p => p.RequireTime)
                            .FirstOrDefault();
            try
            {
                DepartmentID = Convert.ToInt16(Request.Cookies["User"]["Department"]);
            }
            catch
            {
                DepartmentID = 0;
            }
            #region 新建PR
            if (PurchaseRequestID == 0)
            {
                _request = new PurchaseRequest();
                _request.ProjectID = 0;
                try
                {
                    _request.UserID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
                }
                catch
                {
                    _request.UserID = 0;
                }

                _request.CreateDate = DateTime.Now;
                _request.TotalPrice = 0;
                _request.PurchaseRequestNumber = _sequenceRepository.GetNextNumber("purchaserequest");
                _request.State = (int)PurchaseRequestStatus.新建;
                _request.Memo = Memo == "undefined" ? "" : Memo;
                _request.DueDate = new DateTime(1900, 1, 1);
                _request.Enabled = true;
                _request.SupplierID = SupplierID;
                _request.PurchaseType = PurchaseType;
                _request.DepartmentID = DepartmentID;
                _request.ApprovalERPUserID = ApprovalERPUserID;
                _requestID = _purchaseRequestRepository.Save(_request);
            }
            #endregion
            #region 更新PR
            else
            {


                _requestID = PurchaseRequestID;
                _request = _purchaseRequestRepository.GetByID(PurchaseRequestID);


                _request.Memo = Memo;
                _request.SupplierID = SupplierID;


                _request.DueDate = _requireDate;
                _request.PurchaseType = PurchaseType;
                _request.ApprovalERPUserID = ApprovalERPUserID;
                _purchaseRequestRepository.Save(_request);

            }
            #endregion
            #region 创建 PR明细、采购项明细
            //Create PR Contents
            foreach (PRContent _content in PRContents)
            {
                _content.PurchaseRequestID = _requestID;

                _content.RequireTime = _content.RequireTime == new DateTime(1, 1, 1) ? new DateTime(1900, 1, 1) : _content.RequireTime;

                PurchaseItem _item = new PurchaseItem(_content);

                _item.PurchaseType = PurchaseType;

                _item.MoldNumber = _content.MoldNumber;

                _item.RequestUserID = _request.UserID;

                _item.SupplierName = _item.SupplierName == null ? "" : _item.SupplierName;

                _item.RequireTime = _content.RequireTime;

                _item.CostCenterID = _content.CostCenterID;

                _item.Memo = _content.Memo;

                int _itemID = _purchaseItemRepository.Save(_item);



                _content.PurchaseItemID = _itemID;

                if (_content.PartNumber == null)
                {
                    _content.PartNumber = "";
                }
                if (_content.PRContentID < 0)
                {
                    _content.PRContentID = 0;
                }
                //Modify task outsource state
                if (_content.TaskID > 0)
                {
                    #region 外发任务
                    _taskRepository.OutSource(_content.TaskID);
                    Task _task = _taskRepository.QueryByTaskID(_content.TaskID);
                    DateTime _iniTime = DateTime.Parse("1900/1/1");
                    try
                    {
                        TaskHour _taskhour = new TaskHour();
                        _taskhour.TaskID = _task.TaskID;
                        _taskhour.Enabled = true;
                        _taskhour.StartTime = DateTime.Now;
                        _taskhour.FinishTime = _iniTime;
                        _taskhour.TaskType = _task.TaskType;
                        _taskhour.MoldNumber = _task.MoldNumber;
                        _taskhour.Time = 0;
                        _taskhour.RecordType = 2; //外发任务工时记录
                        _taskhour.State = (int)TaskHourStatus.外发;
                        try
                        {
                            User _user = _userRepository.GetUserByID(wsUserID) ?? new User();
                            _taskhour.Operater = _user.FullName;
                            Supplier _supplier = _supplierRepository.QueryByID(SupplierID) ?? new Supplier();
                            _taskhour.MachineCode = _supplier.MachineCode ?? "";
                        }
                        catch { }                        
                        _taskhour.Memo = "记录创建于：" + DateTime.Now.ToString("yyMMddHHmm") + "；操作者：" + GetCurrentUser() + "/r/n";
                        _taskHourRepository.Save(_taskhour);
                    }
                    catch (Exception ex)
                    {
                        
                    }
                    #endregion
                }
                if (_content.PartID > 0)
                {
                    Part _part = _partRepository.QueryByID(_content.PartID);
                    _part.InPurchase = true;
                    //零件上锁 michael
                    _part.Locked = true;
                    _partRepository.Save(_part);
                }

                if (_content.SupplierName == null)
                {
                    _content.SupplierName = "";
                }
                _prContentRepository.Save(_content);
            }
            #endregion
            #region Create PR operation record 具体用处不详
            //Create PR operation record
            if (PurchaseRequestID == 0)
            {
                string _msg = "创建申请单" + _request.PurchaseRequestNumber;
                PRRecord(_requestID, _msg);
            }
            #endregion
            return _requestID;

        }

        /// <summary>
        /// Stage 2: Purchase request accept
        /// Set the responsible user for the request 
        /// Promote the status from 1 to 2
        /// </summary>
        /// <param name="PurchaseRequestID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public ActionResult AcceptPurchaseRequest(int PurchaseRequestID, string Memo)
        {
            int PUUserID = GetCurrentUser();
            _purchaseRequestRepository.Accept(PurchaseRequestID, PUUserID);
            _purchaseRequestRepository.StatePromote(PurchaseRequestID);
            string _msg = "接受采购申请。备注信息:" + Memo;
            PRRecord(PurchaseRequestID, _msg);
            return RedirectToAction("PRDetail", "Purchase", new { PurchaseRequestID = PurchaseRequestID });
        }

        /// <summary>
        /// Stage 99: Purchase request reject
        /// </summary>
        /// <param name="PurchaseRequestID"></param>
        /// <returns></returns>
        public ActionResult RefusePurchaseRequest(int PurchaseRequestID, string Memo)
        {

            _purchaseRequestRepository.Refuse(PurchaseRequestID, Memo);
            string _msg = "拒绝了采购申请。原因：" + Memo;
            PRRecord(PurchaseRequestID, _msg);
            return RedirectToAction("PRDetail", "Purchase", new { PurchaseRequestID = PurchaseRequestID });
        }


        public int Restart(int PurchaseRequestID, string Memo)
        {
            try
            {


                _purchaseRequestRepository.Restart(PurchaseRequestID, Memo);
                string _msg = "重启了采购申请。原因：" + Memo;
                PRRecord(PurchaseRequestID, _msg);
                return 1;
            }
            catch
            {
                return 0;
            }
        }




        /// <summary>
        /// Stage3: Input Quotation of suppliers
        /// Save the quotation from the supplier
        /// Update the quotation state of PRSupplier
        /// </summary>
        /// <param name="PruchaseRequestID"></param>
        /// <param name="SupplierID"></param>
        /// <param name="Quotations"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveQuotation(int QuotationRequestID,
            int SupplierID,
            IEnumerable<QRQuotation> Quotations,
            DateTime QuotationDate,
            DateTime ValidDate,
            double TaxRate,
            int TaxInclude,
            int ContactID = 0)
        {
            _qrQuotationRepository.Disable(QuotationRequestID, SupplierID);
            foreach (QRQuotation _quotation in Quotations)
            {
                _quotation.QuotationRequestID = QuotationRequestID;
                _quotation.QuotationDate = QuotationDate;
                _quotation.Enabled = true;
                _quotation.SupplierID = SupplierID;
                _quotation.TaxRate = TaxRate;

                if (_quotation.UnitPrice == -1)
                {
                    _quotation.ShipDate = new DateTime(1900, 1, 1);
                }


                _qrQuotationRepository.Save(_quotation);
            }
            _qrSupplierRepository.Quotation(QuotationRequestID, SupplierID, QuotationDate, ValidDate, TaxRate, TaxInclude, ContactID);
            string _supplierName = _supplierRepository.QueryByID(SupplierID).Name;
            string _msg = "输入供应商" + _supplierName + "报价";
            PRRecord(QuotationRequestID, _msg);
            return RedirectToAction("QRDetail", "Purchase", new { QuotationRequestID = QuotationRequestID });
        }


        /// <summary>
        /// Stage 4: Supplier assign approval
        /// </summary>
        /// <param name="PurchaseID"></param>
        /// <param name="ResponseType"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PurchaseRequestReview(int PurchaseRequestID, bool ResponseType, string Memo = "")
        {
            _purchaseRequestRepository.UpdateMemo(PurchaseRequestID, Memo);
            _purchaseRequestRepository.StatePromote(PurchaseRequestID, ResponseType);

            string _msg;
            if (ResponseType)
            {
                _msg = "审批报价通过";
            }
            else
            {
                _msg = "审批报价拒绝。原因：" + Memo;
            }
            PRRecord(PurchaseRequestID, _msg);
            return RedirectToAction("Index", "Purchase", null);
        }

        /// <summary>
        /// Stage 5： PO sent to supplier
        /// </summary>
        /// <param name="PurchaseRequestID"></param>
        /// <returns></returns>

        public ActionResult PurchaseRequestAccepted(int PurchaseRequestID)
        {

            PurchaseRequest _pr = _purchaseRequestRepository.GetByID(PurchaseRequestID);
            PurchaseOrder _po = GetPurchaseOrder(_pr);
            _po.State = 1;
            int _poID = _purchaseOrderRepository.Save(_po);

            IEnumerable<PRContent> _prContents = _prContentRepository.QueryByRequestID(PurchaseRequestID);
            foreach (PRContent _prContent in _prContents)
            {
                POContent _poContent = GetPOContent(_prContent);
                _poContent.PurchaseOrderID = _poID;
                _poContentRepository.Save(_poContent);
            }
            _purchaseRequestRepository.StatePromote(PurchaseRequestID);
            string _msg = "发出订单";
            PRRecord(PurchaseRequestID, _msg);
            return RedirectToAction("Index", "Purchase", null);
        }

        /// <summary>
        /// Stage 6: PO content accepted by warehouse
        /// </summary>
        /// <param name="PurchaseRequestID"></param>
        /// <returns></returns>
        public ActionResult PurchaseRequestInStock(int PurchaseRequestID)
        {
            _purchaseRequestRepository.StatePromote(PurchaseRequestID);
            string _msg = "完成订单";
            PRRecord(PurchaseRequestID, _msg);
            return RedirectToAction("Index", "Purchase", null);
        }

        /// <summary>
        /// Supplier create/save
        /// </summary>
        /// <param name="Supplier"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Supplier(Supplier Supplier)
        {
            int id = _supplierRepository.Save(Supplier);
            return RedirectToAction("Suppliers", "Purchase");
        }

        [HttpPost]
        public ActionResult SupplierContact(Contact Contact)
        {
            int SupplierID = Contact.OrganizationID;
            Contact.ContactType = 1;
            Contact.Enabled = true;
            _contactRepository.Save(Contact);
            return RedirectToAction("Suppliers", "Purchase", new { SupplierID = SupplierID });
        }

        #endregion

        #region Json
        public JsonResult JsonPRList(string MoldNumber = "", string PRKeyword = "", string StartDate = "",
            string FinishDate = "", int Supplier = 0, int PurchaseType = 0, int Department = 0, int State = 0)
        {
            int _dept = Convert.ToInt16(Request.Cookies["User"]["Department"]);
            IEnumerable<PurchaseRequest> _prList;
            if ((MoldNumber == "") && (PRKeyword == "") && (StartDate == "") && (FinishDate == "") && (Supplier == 0) && (PurchaseType == 0))
            {
                if (State > 0)
                {
                    _prList = _purchaseRequestRepository.PurchaseRequests
                        .Where(p => p.State == State)
                        .Where(p => p.Enabled == true)
                        .OrderByDescending(p => p.CreateDate);
                }
                else
                {
                    _prList = _purchaseRequestRepository.PurchaseRequests.Where(p => p.Enabled == true).OrderByDescending(p => p.PurchaseRequestNumber);
                }

            }
            else
            {
                IEnumerable<PurchaseItem> _items = _purchaseItemRepository.PurchaseItems;
                if (MoldNumber != "")
                {
                    _items = _items.Where(p => p.Name.Contains(MoldNumber));
                }
                if (PRKeyword != "")
                {
                    _items = _items.Where(p => p.Name.Contains(PRKeyword));
                }
                //if (StartDate != "")
                //{
                //}
                //if (FinishDate != "")
                //{

                //}
                //if (Supplier > 0)
                //{
                //    _items = _items.Where(p => p.SupplierID == Supplier);
                //}

                //if (PurchaseType > 0)
                //{
                //    _items = _items.Where(p => p.PurchaseType == PurchaseType);
                //}

                //if (State > 0)
                //{
                //    _items = _items.Where(p => p.State == State);
                //}

                IEnumerable<int> _prIds = _items.Select(p => p.PurchaseRequestID).Distinct();

                _prList = _purchaseRequestRepository.PurchaseRequests
                    .Where(p => (_prIds.Contains(p.PurchaseRequestID)))
                    .Where(p => p.State == State)
                    .Where(p => p.Enabled == true).OrderByDescending(p => p.CreateDate); ;
            }

            if ((Department > 0) && (_dept != 4))
            {
                _prList = _prList.Where(p => p.DepartmentID == Department);
            }
            PRListGridViewModel _viewModel = new PRListGridViewModel(_prList,
                _userRepository,
                _status,
                _projectRepository,
                _purchaseTypeRepository,
                _departmentRepository,
                _prContentRepository);
            return Json(_viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetErpIDByPartID(string PartID="")
        {
            PartID = PartID == "" ? "0" : PartID;
            int int_PartID = Convert.ToInt32(PartID);
            if (int_PartID > 0)
            {
                string erpID = _partRepository.QueryByID(int_PartID).ERPPartID??"";
                return Json(new { Code = 0, Message = erpID });
            }
            return Json(new { Code = -1, Message = "" });
        }
        [HttpPost]
        public string GetErpIDByPrcID(string PrcID = "")
        {
            PrcID = PrcID == "" ? "0" : PrcID;
            int int_PrcID = Convert.ToInt32(PrcID);
            if (int_PrcID > 0)
            {
                string erpID = _prContentRepository.QueryByID(int_PrcID).ERPPartID ?? "";
                return erpID;
                //return Json(string.IsNullOrEmpty(erpID) ? new { Code = -1, Message = "" }: new { Code = 0, Message = erpID });
            }
            return "";
            //return Json(new { Code = -1, Message = "" });
        }
        /// <summary>
        /// Pass the partID string to create the PR
        /// </summary>
        /// <param name="PartIDs">PartID(split by ',')</param>
        /// <returns>Json of parts</returns>
        public JsonResult JsonPRNew(String PartIDs)
        {
            if (PartIDs != "")
            {
                string[] _partID = PartIDs.Split(',');
                List<Part> _parts = new List<Part>();
                for (int i = 0; i < _partID.Length; i++)
                {
                    if (!string.IsNullOrEmpty(_partID[i]))
                    {
                        _parts.Add(_partRepository.QueryByID(Convert.ToInt32(_partID[i])));
                    }

                }
                PurchaseContentGridViewModel _model;
                _model = new PurchaseContentGridViewModel(_parts, _projectRepository);

                return Json(_model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsonPROutSource(string TaskIDs)
        {
            List<SetupTaskStart> _viewmodel=new List<SetupTaskStart>();
            if (Session["setupTask"] != null)
            {
                _viewmodel = Session["setupTask"] as List<SetupTaskStart>;
            }
            if (TaskIDs != "")
            {
                string[] _taskId = TaskIDs.Split(',');
                List<Task> _taskList = new List<Task>();
                for (int i = 0; i < _taskId.Length; i++)
                {
                    _taskList.Add(_taskRepository.QueryByTaskID(Convert.ToInt32(_taskId[i])));
                }

                List<string> Memo = new List<string>();
                foreach (Task _task in _taskList)
                {
                    _task.Memo = GetOutSourceMemo(_task);
                }
                PurchaseContentGridViewModel _model = new PurchaseContentGridViewModel(_taskList, _viewmodel, _projectPhaseRepository, _steelDrawingRepository,_taskRepository );
               return Json(_model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsonPRWarehouseStock(string WarehouseStockIDs)
        {
            if (!string.IsNullOrEmpty(WarehouseStockIDs) && WarehouseStockIDs!="undefined")
            {
                string[] _whId = WarehouseStockIDs.Split(',');
                List<WarehouseStock> _stockItems = new List<WarehouseStock>();
                for (int i = 0; i < _whId.Length; i++)
                {
                    _stockItems.Add(_warehouseStockRepository.QueryByID(Convert.ToInt32(_whId[i])));
                }

                PurchaseContentGridViewModel _model = new PurchaseContentGridViewModel(_stockItems);
                return Json(_model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public string GetOutSourceMemo(Task Task)
        {
            string _memo = "";
            double _time = 0;
            if (Task.TaskType == 1)
            {
                CNCMachInfo _machInfo = _machInfoRepository.QueryByNameVersion(Task.TaskName, Task.Version);
                if (_machInfo != null)
                {
                    //_time = (_machInfo.RoughCount * _machInfo.RoughTime + _machInfo.FinishCount * _machInfo.FinishTime) * 1.7 / 60;
                    _time = (Task.R * _machInfo.RoughTime + Task.F * _machInfo.FinishTime) * 1.7 / 60;
                }
            }
            else if (Task.TaskType == 4)
            {
                _time = Task.Time * Task.Quantity * 1.8 / 60;
            }
            _memo = Task.R.ToString() + "R/" + Task.F.ToString() + "F(" + _time.ToString("0.00") + ")";
            return _memo;
        }




        public JsonResult JsonPurchaseRequest(int PurchaseRequestID)
        {
            PurchaseRequest _pr = _purchaseRequestRepository.GetByID(PurchaseRequestID);
            return Json(_pr, JsonRequestBehavior.AllowGet);
        }
        #region Upd By Michael 零件名更新为零件短名
        public JsonResult JsonPRContent(int PRContentID)
        {
            PRContent _item = _prContentRepository.QueryByID(PRContentID);
            //短名
            string name = _item.PartName??"";
            //name = name.Substring(name.IndexOf('_') + 1, name.LastIndexOf('_') - name.IndexOf('_') - 1);
            //name = name.Substring(0, name.LastIndexOf('_') - 1);
            _item.PartName = name;
            return Json(_item, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonPRContents(string Keyword)
        {
            IEnumerable<PRContent> _prcontents = _prContentRepository.QueryByName(Keyword);
            foreach(var prc in _prcontents)
            {
                //短名
                string name = prc.PartName;
                name = name.Substring(name.IndexOf('_') + 1, name.LastIndexOf('_') - name.IndexOf('_') - 1);
                name = name.Substring(0, name.LastIndexOf('_') - 1);
                prc.PartName = name;
            }            
            return Json(_prcontents, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public JsonResult JsonPRDetail(int PRID)
        {
            List<PRContent> _contents= new List<PRContent>();
            try
            {
                _contents = _prContentRepository.QueryByRequestID(PRID).ToList() ?? new List<PRContent>();
            }
            catch (Exception ex) { }
            PurchaseContentGridViewModel _model = new PurchaseContentGridViewModel(_contents, _purchaseItemRepository, _costCenterRepository, _partRepository);
            return Json(_model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Json data for displaying supplier data in grid view
        /// </summary>
        /// <returns></returns>
        public JsonResult JsonSupplierGrid()
        {
            IEnumerable<Supplier> _prSupplier = _supplierRepository.Suppliers;
            SupplierGridViewModel _supplier = new SupplierGridViewModel(_prSupplier);
            return Json(_supplier, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonSuppliers(string Keyword = "", int QuotationRequestID = 0)
        {
            IEnumerable<Supplier> _Supplier;
            if (Keyword != "")
            {
                _Supplier = _supplierRepository.Suppliers.Where(s => s.Name.ToLower().Contains(Keyword.ToLower()));
            }
            else
            {
                _Supplier = _supplierRepository.Suppliers;
            }
            _Supplier = _Supplier.Where(s => s.Enabled == true);

            if (QuotationRequestID > 0)
            {
                IEnumerable<QRSupplier> _qrSuppliers = _qrSupplierRepository.QueryByQRID(QuotationRequestID);
                foreach (QRSupplier _qrSupplier in _qrSuppliers)
                {
                    _Supplier = _Supplier.Where(s => s.SupplierID != _qrSupplier.SupplierID);
                }
            }

            return Json(_Supplier.OrderBy(s => s.Name), JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonSupplier(int SupplierID)
        {
            Supplier _supplier = _supplierRepository.QueryByID(SupplierID);
            return Json(_supplier, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonContacts(int SupplierID)
        {
            IEnumerable<Contact> _contacts = _contactRepository.QueryByOrganization(SupplierID);
            return Json(_contacts, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonContact(int ContactID)
        {
            Contact _contact = _contactRepository.QueryByID(ContactID);
            return Json(_contact, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonQRSupplier(int QuotationRequestID, int SupplierID = 0)
        {
            IEnumerable<QRSupplier> _qrSuppliers = _qrSupplierRepository.QueryByQRID(QuotationRequestID);
            if (SupplierID > 0)
            {
                _qrSuppliers = _qrSuppliers.Where(q => q.SupplierID == SupplierID);
            }
            return Json(_qrSuppliers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonHistory(int PurchaseRequestID)
        {
            IEnumerable<ProcessRecord> _history = _processRecordRepository.Query(1, PurchaseRequestID);
            return Json(_history, JsonRequestBehavior.AllowGet);
        }

        public int UniqueSupplier(String Name)
        {
            int _count = _supplierRepository.Suppliers.Where(s => s.Name.ToLower() == Name.ToLower())
                .Where(s => s.Enabled == true).Count();
            return _count;
        }

        public JsonResult JsonQRContent(int QRContentID)
        {
            QRContent _content = _qrContentRepository.GetByID(QRContentID);
            return Json(_content, JsonRequestBehavior.AllowGet);
        }


        #endregion




        private void PRRecord(int PurchaseRequestID, string Message)
        {
            int _userID;
            string _userName;
            try
            {
                _userID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
                _userName = HttpUtility.UrlDecode(Request.Cookies["User"]["FullName"], System.Text.Encoding.UTF8);
            }
            catch
            {
                _userID = 0;
                _userName = "";
            }
            ProcessRecord _record = new ProcessRecord();
            _record.ProcessType = 1;
            _record.ProcessID = PurchaseRequestID;
            _record.UserID = _userID;
            _record.Message = _userName + " " + Message;
            _processRecordRepository.Save(_record);
        }

        private PurchaseOrder GetPurchaseOrder(PurchaseRequest PurchaseRequest)
        {
            PurchaseOrder _po = _purchaseOrderRepository.QueryByPRID(PurchaseRequest.PurchaseRequestID);
            if (_po == null)
            {
                _po = new PurchaseOrder();
            }
            _po.PurchaseRequestID = PurchaseRequest.PurchaseRequestID;
            _po.PurchaseOrderNumber = PurchaseRequest.PurchaseRequestNumber;
            _po.State = PurchaseRequest.State;
            _po.UserID = PurchaseRequest.UserID;
            _po.Responsible = PurchaseRequest.Responsible;
            _po.Approval = PurchaseRequest.Approval;
            _po.ProjectID = PurchaseRequest.ProjectID;
            _po.ReleaseDate = DateTime.Now;
            _po.TotalPrice = PurchaseRequest.TotalPrice;
            _po.SupplierID = PurchaseRequest.SupplierID;
            _po.Memo = PurchaseRequest.Memo;
            return _po;
        }

        private POContent GetPOContent(PRContent PRContent)
        {

            POContent _poContent = _poContentRepository.QueryByPRContentID(PRContent.PRContentID);
            if (_poContent == null)
            {
                _poContent = new POContent();
            }
            _poContent.PRContentID = PRContent.PRContentID;
            _poContent.PartName = PRContent.PartName;
            _poContent.PartNumber = PRContent.PartNumber;
            _poContent.PartSpecification = PRContent.PartSpecification;
            _poContent.Quantity = PRContent.Quantity;
            _poContent.PurchaseOrderID = PRContent.PurchaseRequestID;
            _poContent.UnitPrice = PRContent.UnitPrice;
            _poContent.SubTotal = PRContent.SubTotal;
            _poContent.BrandName = PRContent.BrandName;
            _poContent.Memo = PRContent.Memo;
            _poContent.ReceivedQty = 0;
            _poContent.State = 1;
            return _poContent;
        }

        /// <summary>
        /// Calculate the total cost of purchase order, by unit price(from pr items) and received quantity(from po items)
        /// </summary>
        /// <param name="PurchaseOrderID"></param>
        /// <returns></returns>
        public double TotalCost(int PurchaseOrderID)
        {
            int _prID = _purchaseOrderRepository.QueryByID(PurchaseOrderID).PurchaseRequestID;
            IEnumerable<POContent> _orderItems = _poContentRepository.QueryByPOID(PurchaseOrderID);
            IEnumerable<PRContent> _requestItems = _prContentRepository.QueryByRequestID(_prID);
            double _totalCost = 0;
            foreach (POContent _item in _orderItems)
            {
                PRContent _reqeustItem = _requestItems.Where(r => r.PRContentID == _item.PRContentID).FirstOrDefault();
                _totalCost = _totalCost + _reqeustItem.UnitPrice * _item.ReceivedQty;
            }
            return _totalCost;
        }

        private int GetCurrentUser()
        {
            try
            {
                int _userID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
                return _userID;
            }
            catch
            {
                return 0;
            }
        }

        #region TestMethods

        public FileResult PDFTest(int PurchaseRequestID)
        {
            string _server = "http://" + Request.Url.Host + ":" + Request.Url.Port;
            byte[] file = PDFUtil.GetQR(_server, PurchaseRequestID);
            return File(file, "application/pdf");
        }

        public ActionResult QRForm(int QuotationRequestID)
        {


            QuotationRequest _qr = _quotationRequestRepository.GetByID(QuotationRequestID);

            if (_qr.State == (int)QuotationRequestStatus.新建)
            {

                _quotationRequestRepository.ChangeStatus(QuotationRequestID, (int)QuotationRequestStatus.发出);
            }
            User _user = _userRepository.GetUserByID(_qr.PurchaseUserID);
            IEnumerable<QRContent> _qrContents = _qrContentRepository.QueryByQRID(QuotationRequestID);
            QRViewModel _model = new QRViewModel(_qrContents, _user, _qr);
            return View(_model);
        }

        public FileResult QRFormPDF(int PurchaseRequestID)
        {
            string _server = "http://" + Request.Url.Host + ":" + Request.Url.Port;
            byte[] file = PDFUtil.GetQR(_server, PurchaseRequestID);
            return File(file, "application/pdf");
        }

        public ActionResult PRForm(int PurchaseOrderID)
        {
            PurchaseOrder _order = _purchaseOrderRepository.QueryByID(PurchaseOrderID);
            User _user = _userRepository.GetUserByID(_order.Responsible);
            IEnumerable<POContent> _poContents = _poContentRepository.QueryByPOID(PurchaseOrderID);
            Supplier _supplier = _supplierRepository.QueryByID(_order.SupplierID);
            Contact _contact = _contactRepository.QueryByOrganization(_order.SupplierID).FirstOrDefault();
            POViewModel _model = new POViewModel(_poContents, _order, _user, _supplier, _contact);
            return View(_model);
        }

        public FileResult POFormPDF(int PurchaseOrderID)
        {
            PurchaseOrder _po = _purchaseOrderRepository.QueryByID(PurchaseOrderID);




            if (IsOutSource(_po.PurchaseType))
            {
                _purchaseItemRepository.ChangeState(0, 0, PurchaseOrderID, (int)PurchaseItemStatus.外发项待出库);
            }
            else
            {
                _purchaseItemRepository.ChangeState(0, 0, PurchaseOrderID, (int)PurchaseItemStatus.待收货);
            }


            string _server = "http://" + Request.Url.Host + ":" + Request.Url.Port;
            byte[] file = PDFUtil.GetPO(_server, PurchaseOrderID);
            return File(file, "application/pdf");
        }

        #endregion

        public string DeletePRContent(string PRContentIDs, int PurchaseRequestID)
        {
            string[] _ids = PRContentIDs.Split(',');
            string _return = "";
            int _id;
            for (int i = 0; i < _ids.Length; i++)
            {
                _id = Convert.ToInt32(_ids[i]);
                PRContent _content = _prContentRepository.QueryByID(_id);
                PurchaseItem _item = _purchaseItemRepository.QueryByID(_content.PurchaseItemID);
                if (_item.State < (int)PurchaseItemStatus.待收货)
                {
                    _prContentRepository.Delete(_id);
                    _purchaseItemRepository.ChangeState(_content.PurchaseItemID, (int)PurchaseItemStatus.取消);
                    #region 更新 Part 采购状态
                    int _partID = _item.PartID;               
                    if (_partID > 0)
                    {
                        Part _part = _partRepository.QueryByID(_partID);
                        int _purchaseCount = _purchaseItemRepository.PurchaseItems
                            .Where(p => p.PartID == _partID)
                            .Where(p => p.PurchaseRequestID != _content.PurchaseRequestID)
                            .Where(p => p.State > 0)
                            .Count();
                        if (_purchaseCount == 0)
                        {
                            _part.InPurchase = false;
                            _partRepository.Save(_part);
                        }
                    }
                    #endregion
                    #region 更新 Task 状态 '等待'
                    int _taskid = _content.TaskID;
                    if (_taskid > 0)
                    {
                        Task _task = _taskRepository.QueryByTaskID(_taskid);
                        _task.State = (int)CNCStatus.等待;
                        _taskRepository.Save(_task);
                    }
                    #endregion
                }
                else
                {
                    _return = _return == "" ? _content.PartName : _return + "," + _content.PartName;
                }
            }
            int _count = _prContentRepository.QueryByRequestID(PurchaseRequestID).Where(p => p.Enabled == true).Count();
            if (_count == 0)
            {
                _purchaseRequestRepository.Cancel(PurchaseRequestID);
            }
            if (_return != "")
            {
                return _return;
            }
            else
            {
                return _count.ToString();
            }



        }

        public string DeleteSupplier(int SupplierID)
        {
            string msg = "";
            int _prCount = _purchaseRequestRepository.PurchaseRequests.Where(p => p.SupplierID == SupplierID).Count();
            int _poCount = _purchaseOrderRepository.PurchaseOrders.Where(p => p.SupplierID == SupplierID).Count();
            if (_prCount + _poCount == 0)
            {
                _supplierRepository.Delete(SupplierID);
                _contactRepository.DeleteByOrganization(SupplierID);
            }
            else
            {
                msg = "系统中有该供应商相关订单记录，无法删除供应商";
            }
            return msg;
        }

        public void DeleteContact(int ContactID)
        {
            _contactRepository.Delete(ContactID);
            //return RedirectToAction("Suppliers", "Purchase");
        }

        public string SubmitPR(int PurchaseRequestID)
        {
            string _msg = "";
            UpdatePurchaseRequestState(PurchaseRequestID, (int)PurchaseRequestStatus.待审批);

            PurchaseRequest _request = _purchaseRequestRepository.GetByID(PurchaseRequestID);
            string _processMsg = "提交申请单" + _request.PurchaseRequestNumber;
            PRRecord(PurchaseRequestID, _processMsg);

            _purchaseItemRepository.ChangeState(PurchaseRequestID, 0, 0, (int)PurchaseItemStatus.需求待审批);


            return _msg;
        }


        private void UpdatePurchaseRequestState(int PurchaseRequestID, int State, string Memo = "")
        {
            int _userID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
            _purchaseRequestRepository.Submit(PurchaseRequestID, State, Memo, _userID);
        }

        public string ReviewPR(int PurchaseRequestID, int ResponseType, string Memo)
        {
            string _msg = "";
            int _userID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
            _purchaseRequestRepository.Submit(PurchaseRequestID, ResponseType, Memo, _userID);

            PurchaseRequest _request = _purchaseRequestRepository.GetByID(PurchaseRequestID);
            string response = ResponseType == (int)PurchaseRequestStatus.审批通过 ? "批准" : "拒绝";
            string _processMsg = response + "申请单" + _request.PurchaseRequestNumber + "。备注：" + Memo;
            PRRecord(PurchaseRequestID, _processMsg);

            if (ResponseType == (int)PurchaseRequestStatus.审批通过)
            {
                _purchaseItemRepository.ChangeState(PurchaseRequestID, 0, 0, (int)PurchaseItemStatus.待询价);
            }
            else
            {
                _purchaseItemRepository.ChangeState(PurchaseRequestID, 0, 0, (int)PurchaseItemStatus.审批拒绝);
                //added by felix
                //修改parts 的inpurchase = 0
                List<PurchaseItem> items = _purchaseItemRepository.QueryByPurchaseRequestID(PurchaseRequestID).ToList<PurchaseItem>();
                foreach (PurchaseItem item in items)
                {
                    Part p = _partRepository.QueryByID(item.PartID);
                    p.InPurchase = false;
                    PartList partlist = _partListRepository.PartLists.Where(pl => pl.PartListID == p.PartListID).FirstOrDefault();
                    //零件(新建) bom(未发布)
                    //零件(新建并升级)
                    //零件(非新建且升级)
                    if ((p.Latest && !partlist.Released)||(p.Latest&&p.Status>1)||(!p.Latest&&p.Status>0))
                    {
                        p.Locked = false;
                    }
                    _partRepository.SaveNew(p);
                }
            }
            return _msg;
        }



        public bool BatchReviewPR(string PurchaseRequestIDs)
        {
            string[] _id;
            string _memo = DateTime.Now.ToString("yyyy-MM-dd") + "批量审批通过";
            int _userID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
            if (PurchaseRequestIDs.Contains(','))
            {
                _id = PurchaseRequestIDs.Split(',');
            }
            else
            {
                _id = new string[1];
                _id[0] = PurchaseRequestIDs;
            }
            for (int i = 0; i < _id.Length; i++)
            {
                PurchaseRequest _request = _purchaseRequestRepository.GetByID(Convert.ToInt32(_id[i]));
                if (_request.State == (int)PurchaseRequestStatus.待审批)
                {
                    _purchaseRequestRepository.Submit(_request.PurchaseRequestID, (int)PurchaseRequestStatus.审批通过, _memo, _userID);
                    string _processMsg = "批准申请单" + _request.PurchaseRequestNumber;
                    PRRecord(_request.PurchaseRequestID, _processMsg);

                    _purchaseItemRepository.ChangeState(_request.PurchaseRequestID, 0, 0, (int)PurchaseItemStatus.待询价);
                }
            }
            return true;
        }

        public string CancelPR(int PurchaseRequestID)
        {
            string _msg = "";
            _purchaseRequestRepository.Cancel(PurchaseRequestID);
            #region 取消 Part 采购状态
            List<PurchaseItem> items = _purchaseItemRepository.QueryByPurchaseRequestID(PurchaseRequestID).ToList<PurchaseItem>();
            foreach (PurchaseItem item in items)
            {               
                Part p = _partRepository.QueryByID(item.PartID) ?? new Part();                
                if (p.PartID > 0)
                {
                    p.InPurchase = false;
                    _partRepository.SaveNew(p);
                }                
            }
            #endregion
            #region 更新外发任务状态
            List<PRContent> _prcontents = _prContentRepository.QueryByRequestID(PurchaseRequestID).ToList();
            foreach(var r in _prcontents)
            {
                if (r.TaskID > 0)
                {
                    Task _task = _taskRepository.QueryByTaskID(r.TaskID);
                    _task.State = (int)CNCStatus.等待;
                    _taskRepository.Save(_task);
                }
            }
            #endregion
            return _msg;
        }

        #region 询价单

        /// <summary>
        /// Display a list of quotation requst
        /// </summary>
        /// <returns></returns>
        public ActionResult QuotationRequestList(int ProjectID = 0, int UserID = 0, int State = 0)
        {
            ViewBag.ProjectID = ProjectID;
            ViewBag.UserID = UserID;
            ViewBag.State = State;
            return View();
        }

        public JsonResult JsonQRList(int ProjectID = 0, int UserID = 0, int State = 0)
        {
            IEnumerable<QuotationRequest> _requests = _quotationRequestRepository.QuotationRequests.Where(q => q.Enabled == true);

            if (State > 0)
            {
                _requests = _requests.Where(r => r.State == State);
            }

            _requests = _requests.OrderByDescending(r => r.QuotationRequestID);
            QRListGridViewModel _model = new QRListGridViewModel(_requests,
                _userRepository,
                _supplierRepository,
                _projectRepository,
                _qrSupplierRepository);
            return Json(_model, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Display the quotation request detail page
        /// </summary>
        /// <param name="QuotationRequestID">
        /// Primary key of quotation request
        /// if the id is not provided, means this is a new quotation request.
        /// </param>
        /// <param name="PurchaseRequestID">
        /// Primary key of related purchase request
        /// </param>
        /// <param name="PRContentIDs"></param>
        /// <returns></returns>
        public ActionResult QRDetail(int QuotationRequestID = 0, int PurchaseRequestID = 0, string PRContentIDs = "", string PurchaseItemIDs = "")
        {
            Project _project;
            if (QuotationRequestID != 0)
            {
                QuotationRequest _request = _quotationRequestRepository.GetByID(QuotationRequestID);
                PurchaseRequest _pr = _purchaseRequestRepository.GetByID(_request.PurchaseRequestID);
                ViewBag.Title = "询价单详情";
                ViewBag.PurchaseRequestID = _request.PurchaseRequestID;
                ViewBag.PurchaseNumber = (_pr == null) ? "" : _pr.PurchaseRequestNumber;
                _project = _projectRepository.GetByID(_request.ProjectID);
                //ViewBag.MoldNumber = _project.MoldNumber;
                ViewBag.ProjectID = _request.ProjectID;
                ViewBag.PRContentIDs = "";
                ViewBag.QuotationRequestID = QuotationRequestID;
                ViewBag.Duedate = _request.DueDate.ToString("yyyy-MM-dd");
                ViewBag.QRState = _request.State;
                ViewBag.PurchaseItemIDs = "";
                return View(_request);
            }
            else if ((PurchaseRequestID != 0) || (PRContentIDs != ""))
            {
                PurchaseRequest _pr = _purchaseRequestRepository.GetByID(PurchaseRequestID);

                ViewBag.Title = "新建询价单";
                ViewBag.PurchaseRequestID = _pr.PurchaseRequestID;
                ViewBag.PurchaseNumber = _pr.PurchaseRequestNumber;
                _project = _projectRepository.GetByID(_pr.ProjectID);
                //ViewBag.MoldNumber = _project.MoldNumber;
                ViewBag.ProjectID = _pr.ProjectID;
                ViewBag.PRContentIDs = PRContentIDs;
                ViewBag.DueDate = _pr.DueDate.ToString("yyyy-MM-dd");
                ViewBag.QuotationRequestID = 0;
                ViewBag.QRState = 0;
                ViewBag.PurchaseItemIDs = "";
                return View();
            }
            else if (PurchaseItemIDs != "")
            {
                ViewBag.Title = "新建询价单";
                ViewBag.PurchaseRequestID = 0;
                ViewBag.PurchaseNumber = "";
                //ViewBag.MoldNumber = 0;
                ViewBag.ProjectID = 0;
                ViewBag.PRContentIDs = "";
                ViewBag.QuotationRequestID = 0;
                ViewBag.QRState = 0;
                ViewBag.PurchaseItemIDs = PurchaseItemIDs;
                return View();
            }
            else
            {
                ViewBag.Title = "新建询价单";
                ViewBag.PurchaseNumber = "";
                ViewBag.PurchaseRequestID = 0;
                ViewBag.ProjectID = 0;
                ViewBag.PRContentIDs = "";
                ViewBag.QuotationRequestID = 0;
                ViewBag.DueDate = null;
                ViewBag.QRState = 0;
                ViewBag.PurchaseItemIDs = "";
                return View();
            }
        }

        public JsonResult JsonQRNew(string PRContentIDs)
        {
            if (PRContentIDs != null)
            {
                string[] _prContentID = PRContentIDs.Split(',');
                List<PRContent> _prContentList = new List<PRContent>();
                for (int i = 0; i < _prContentID.Length; i++)
                {
                    _prContentList.Add(_prContentRepository.QueryByID(Convert.ToInt32(_prContentID[i])));
                }
                QRContentGridViewModel _model = new QRContentGridViewModel(_prContentList);
                return Json(_model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsonQRDetail(int QRID)
        {
            IEnumerable<QRContent> _contents = _qrContentRepository.QueryByQRID(QRID);
            QRContentGridViewModel _model = new QRContentGridViewModel(_contents);
            return Json(_model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonQRPurchaseItems(string PurchaseItemIDs)
        {
            List<PurchaseItem> _contents = new List<PurchaseItem>();

            if (PurchaseItemIDs != "undefined")
            {
                string[] _ids = PurchaseItemIDs.Split(',');
                for (int i = 0; i < _ids.Length; i++)
                {
                    PurchaseItem _item = _purchaseItemRepository.QueryByID(Convert.ToInt32(_ids[i]));
                    if (_item.State <= (int)PurchaseItemStatus.待询价)
                    {
                        _contents.Add(_item);
                    }
                }
            }
            QRContentGridViewModel _model = new QRContentGridViewModel(_contents, _prContentRepository);
            return Json(_model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int QRSave(List<QRContent> QRContents, DateTime DueDate, int PurchaseRequestID = 0, int QuotationRequestID = 0, int ProjectID = 0)
        {
            int _requestID = 0;
            QuotationRequest _request = null;
            int _supplier = 0;
            string MoldNumber = "";
            //Save Quotation Request
            if (QuotationRequestID == 0)
            {
                _request = new QuotationRequest();
                if (PurchaseRequestID > 0)
                {

                    _request.PurchaseRequestID = PurchaseRequestID;
                    PurchaseRequest _pr = _purchaseRequestRepository.GetByID(PurchaseRequestID);
                    _supplier = _pr.SupplierID;
                    _request.ProjectID = _pr.ProjectID;
                    //MoldNumber = _projectRepository.GetByID(_request.ProjectID).MoldNumber;
                }
                else
                {
                    _request.PurchaseRequestID = 0;
                    _request.ProjectID = ProjectID;
                }
                try
                {
                    _request.PurchaseUserID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
                }
                catch
                {
                    _request.PurchaseUserID = 0;
                }
                _request.CreateDate = DateTime.Now;
                _request.DueDate = DueDate;
                _request.QuotationNumber = _sequenceRepository.GetNextNumber("quotationrequest");
                _request.State = (int)QuotationRequestStatus.新建;
                _request.Enabled = true;
                _requestID = _quotationRequestRepository.Save(_request);

                if (_supplier > 0)
                {
                    Supplier _sup = _supplierRepository.QueryByID(_supplier);
                    QRSupplier _qrSupplier = new QRSupplier();
                    _qrSupplier.QuotationRequestID = _requestID;
                    _qrSupplier.SupplierID = _supplier;
                    _qrSupplier.SupplierName = _sup.Name;
                    _qrSupplierRepository.Save(_qrSupplier);
                }
            }
            else
            {
                _requestID = QuotationRequestID;
            }


            //Save QRContents
            foreach (QRContent _content in QRContents)
            {


                _content.QuotationRequestID = _requestID;



                if (_content.PartNumber == null)
                {
                    _content.PartNumber = "";
                }
                if (_content.QRContentID < 0)
                {
                    _content.QRContentID = 0;
                }
                _content.Enabled = true;


                PurchaseItem _item;
                int _purchaseItemID;
                if (_content.PurchaseItemID > 0)
                {
                    _item = _purchaseItemRepository.QueryByID(_content.PurchaseItemID);

                    _item.PurchaseUserID = _request == null ? 0 : _request.PurchaseUserID;
                    _item.QuotationRequestID = _requestID;
                    _purchaseItemID = _content.PurchaseItemID;

                    _purchaseItemRepository.Save(_item);
                }
                else
                {
                    _item = new PurchaseItem(_content);
                    _item.PurchaseUserID = _request == null ? 0 : _request.PurchaseUserID;
                    _item.MoldNumber = MoldNumber;
                    _purchaseItemID = _purchaseItemRepository.Save(_item);
                }
                if (_content.RequireDate == new DateTime(1, 1, 1))
                {
                    _content.RequireDate = new DateTime(1900, 1, 1);
                }
                _content.PurchaseItemID = _purchaseItemID;
                _qrContentRepository.Save(_content);
            }


            return _requestID;
        }


        public ActionResult CancelQR(int QuotationRequestID)
        {
            _quotationRequestRepository.Delete(QuotationRequestID);

            _qrContentRepository.DeleteByQRID(QuotationRequestID);
            return RedirectToAction("QuotationRequestList", "Purchase");
        }

        public void DeleteQRContent(int QRContentID)
        {
            _qrContentRepository.Delete(QRContentID);

        }


        [HttpPost]
        public string SelectQRSupplier(int QuotationID, IEnumerable<QRSupplier> Supplier)
        {
            string _suppliers = "";
            List<QRSupplier> _qrsuppliers = _qrSupplierRepository.QueryByQRID(QuotationID).ToList();
            QRSupplier _temp;
            foreach (QRSupplier _qrSupplier in Supplier)
            {
                _qrSupplier.TaxRate = -1;
                _qrSupplierRepository.Save(_qrSupplier);
                if (_suppliers == "")
                {
                    _suppliers = _qrSupplier.SupplierName;
                }
                else
                {
                    _suppliers = _suppliers + "," + _qrSupplier.SupplierName;
                }
                _temp = _qrsuppliers.Where(q => q.QuotationRequestID == _qrSupplier.QuotationRequestID)
                    .Where(q => q.SupplierID == _qrSupplier.SupplierID).FirstOrDefault();
                if (_temp != null)
                {
                    _qrsuppliers.Remove(_temp);
                }
            }
            foreach (QRSupplier _supp in _qrsuppliers)
            {
                _qrSupplierRepository.Delete(_supp.QRSupplierID);
            }
            string _msg = "选择供应商" + _suppliers + "进行报价";

            return QuotationID.ToString();
        }

        public string QRSupplierNames(int QuotationRequestID)
        {
            string _names = "";
            IEnumerable<QRSupplier> _suppliers = _qrSupplierRepository.QueryByQRID(QuotationRequestID);
            foreach (QRSupplier _supplier in _suppliers)
            {
                _names = _names == "" ? _supplier.SupplierName : _names + "," + _supplier.SupplierName;
            }
            return _names;
        }

        public string CloseQR(int QuotationRequestID)
        {
            string _msg = "";
            _quotationRequestRepository.ChangeStatus(QuotationRequestID, (int)QuotationRequestStatus.关闭);
            return _msg;
        }

        /// <summary>
        /// Stage2 Action:Send PR sheet
        /// Assign supplier for purchase requests for quotation
        /// </summary>
        /// <param name="PurchaseRequestID"></param>
        /// <param name="Supplier"></param>
        /// <returns></returns>
        public bool SendQR(int QuotationRequestID, string QRReceiver)
        {
            int UserID;
            NetworkCredential _mailCred;
            try
            {
                UserID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
            }
            catch
            {
                UserID = 0;
            }
            if (UserID > 0)
            {
                _mailCred = _userRepository.MailCredential(UserID);

                string _server = "http://" + Request.Url.Host + ":" + Request.Url.Port;
                bool _mailResult = SendMail.SendQR(QRReceiver, QuotationRequestID, _server, _mailCred);
                QuotationRequest _request = _quotationRequestRepository.GetByID(QuotationRequestID);
                if (_request.State == (int)QuotationRequestStatus.新建)
                {

                    _quotationRequestRepository.ChangeStatus(QuotationRequestID, (int)QuotationRequestStatus.发出);
                }
                return _mailResult;
            }
            else
            {
                return false;
            }
        }

        public ActionResult QuotationInput(int QuotationRequestID, int QRSupplierID = 0)
        {
            List<QRQuotationEditModel> _qrQuotationModels = new List<QRQuotationEditModel>();
            IEnumerable<QRContent> _qrContents = _qrContentRepository.QueryByQRID(QuotationRequestID);
            //IEnumerable<PRContent> _prContents = _prContentRepository.PRContents.Where(p => p.PurchaseRequestID == PurchaseRequestID);
            if (QRSupplierID == 0)
            {

                foreach (QRContent _content in _qrContents)
                {
                    _qrQuotationModels.Add(new QRQuotationEditModel(_content));
                }
            }
            else
            {
                IEnumerable<QRQuotation> _qrQuotations = _qrQuotationRepository.QRQuotations.
                    Where(p => p.QuotationRequestID == QuotationRequestID).Where(p => p.SupplierID == QRSupplierID);
                if (_qrQuotations != null)
                {
                    foreach (QRContent _content in _qrContents)
                    {
                        QRQuotation _quotation = _qrQuotations.Where(q => q.QRContentID == _content.QRContentID).FirstOrDefault();
                        _qrQuotationModels.Add(new QRQuotationEditModel(_content, _quotation));
                    }
                }
            }

            ViewBag.QRSupplierID = QRSupplierID;

            ViewBag.QuotationRequestID = QuotationRequestID;
            return View(_qrQuotationModels);
        }

        [HttpPost]
        public ActionResult AssignSupplier(int SupplierID, int QuotationRequestID, double TotalPrice, string PurchaseItemIDs, string Memo = "")
        {
            try
            {
                int[] _purchaseItemIDs = Array.ConvertAll<string, int>(PurchaseItemIDs.Split(','), delegate (string s) { return int.Parse(s); });
                List<QRContent> _qrContents = _qrContentRepository.QRContents.Where(q => q.QuotationRequestID == QuotationRequestID)
                    .Where(q => (_purchaseItemIDs.Contains(q.PurchaseItemID))).Where(q => q.Enabled == true).ToList();
                IEnumerable<QRQuotation> _qrQuotations = _qrQuotationRepository.QueryByQRID(QuotationRequestID);
                IEnumerable<PurchaseItem> _items = _purchaseItemRepository.QueryByQuotationRequestID(QuotationRequestID);

                foreach (QRContent _qrContent in _qrContents)
                {
                    PurchaseItem _item = _items.Where(p => p.PurchaseItemID == _qrContent.PurchaseItemID).FirstOrDefault();
                    QRQuotation _quotation = _qrQuotations.Where(q => q.QRContentID == _qrContent.QRContentID)
                        .Where(q => q.SupplierID == SupplierID).FirstOrDefault();

                    _item.UnitPrice = _quotation.UnitPrice;
                    _item.TotalPrice = _quotation.TotalPrice;
                    _item.UnitPriceWT = _quotation.UnitPriceWT;
                    _item.TotalPriceWT = _quotation.TotalPriceWT;
                    _item.PlanTime = _quotation.ShipDate;
                    _item.State = (int)PurchaseItemStatus.待采购;
                    _item.TaxRate = _quotation.TaxRate;
                    _item.SupplierID = _quotation.SupplierID;
                    _item.SupplierName = _supplierRepository.QueryByID(_quotation.SupplierID).Name;
                    _purchaseItemRepository.Save(_item);

                    //Update supplier information of QRContent object
                    _qrContent.SupplierID = SupplierID;
                    _qrContentRepository.Save(_qrContent);
                }

            }
            catch
            {

            }
            int _left = _qrContentRepository.QRContents.Where(q => q.QuotationRequestID == QuotationRequestID).Where(q => q.Enabled == true)
                .Where(q => q.SupplierID == 0).Count();

            if (_left == 0)
            {
                _quotationRequestRepository.ChangeStatus(QuotationRequestID, (int)QuotationRequestStatus.完成);
            }



            //return RedirectToAction("PODetail", "Purchase", new { PurchaseOrderID=_purchaseOrderID});
            return RedirectToAction("QuotationRequestList", "Purchase");
        }

        public ActionResult RestartQuotation(int QuotationRequestID)
        {
            _quotationRequestRepository.ChangeStatus(QuotationRequestID, (int)QuotationRequestStatus.新建);
            return RedirectToAction("QRDetail", "Purchase", new { QuotationRequestID = QuotationRequestID });

        }


        public string ModifyQRContentQty(int QRContentID, int Quantity)
        {
            string _msg = "";
            QRContent _content = _qrContentRepository.GetByID(QRContentID);
            _content.Quantity = Quantity;
            _qrContentRepository.Save(_content);
            return _msg;
        }
        #endregion

        #region Purchase Order

        //public ActionResult PurchaseOrderList(int ProjectID = 0, int UserID = 0, int State = 0)
        //{
        //    ViewBag.ProjectID = ProjectID;
        //    ViewBag.UserID = UserID;
        //    ViewBag.State = State;
        //    return View();
        //}

        public ActionResult PurchaseOrderList(string MoldNumber = "",
            string Keyword = "",
            String StartDate = "",
            String EndDate = "",
            int Supplier = 0,
            int State = 1,
            int PurchaseType = 0)
        {
            ViewBag.MoldNumber = MoldNumber;
            ViewBag.Keyword = Keyword;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            ViewBag.Supplier = Supplier;
            ViewBag.State = State;
            ViewBag.PurchaseType = PurchaseType;
            return View();
        }

        public ActionResult PurchaseOrderDetail(int PurchaseOrderID)
        {
            return View();
        }

        public ActionResult JsonPurchaseOrderList(string MoldNumber = "",
            string Keyword = "",
            string StartDate = "",
            string EndDate = "",
            int State = 0,
            int Supplier = 0,
            int PurchaseType = 0)
        {
            IEnumerable<PurchaseOrder> _poList;
            if ((MoldNumber == "") && (Keyword == "") && (StartDate == "") && (EndDate == "") && (Supplier == 0) && (PurchaseType == 0))
            {
                _poList = _purchaseOrderRepository.PurchaseOrders;

                if (State > 0)
                {
                    _poList = _poList.Where(p => p.State == State);
                }

                _poList = _poList.OrderByDescending(p => p.PurchaseOrderNumber);
            }
            else
            {
                IEnumerable<PurchaseItem> _items = _purchaseItemRepository.PurchaseItems;
                if (MoldNumber != "")
                {
                    _items = _items.Where(p => p.Name.Contains(MoldNumber));
                }
                if (Keyword != "")
                {
                    _items = _items.Where(p => p.Name.Contains(Keyword));
                }
                if (StartDate != "")
                {
                }
                if (EndDate != "")
                {

                }
                if (Supplier > 0)
                {
                    _items = _items.Where(p => p.SupplierID == Supplier);
                }

                if (PurchaseType > 0)
                {
                    _items = _items.Where(p => p.PurchaseType == PurchaseType);
                }

                IEnumerable<int> _poIds = _items.Select(p => p.PurchaseOrderID);

                _poList = _purchaseOrderRepository.PurchaseOrders.Where(p => (_poIds.Contains(p.PurchaseOrderID)));
            }


            POListGridViewModel _viewModel = new POListGridViewModel(_poList, _projectRepository, _supplierRepository, _purchaseTypeRepository,
                _userRepository);
            return Json(_viewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonPurchaseOrders(int PurchaseType)
        {
            PurchaseType _type = _purchaseTypeRepository.QueryByID(PurchaseType);
            List<int> PurchaseTypeIDs = new List<int>();
            PurchaseTypeIDs.Add(PurchaseType);
            if (_type.ParentTypeID == 0)
            {
                PurchaseTypeIDs.AddRange(_purchaseTypeRepository.QueryByParentID(PurchaseType).Select(p => p.PurchaseTypeID).Distinct());
            }
            IEnumerable<PurchaseOrder> _data = _purchaseOrderRepository.PurchaseOrders.Where(p => (PurchaseTypeIDs.Contains(p.PurchaseType)))
                .Where(p => p.State == (int)PurchaseOrderStatus.外发待出库);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult JsonOutSourceItems(int PurchaseOrderID)
        {
            IEnumerable<PurchaseItem> _outItems = _purchaseItemRepository.PurchaseItems
                .Where(p => p.PurchaseOrderID == PurchaseOrderID).Where(p => p.State == (int)PurchaseItemStatus.外发项待出库);
            PurchaseItemGridViewModel _viewModel = new PurchaseItemGridViewModel(_outItems, _purchaseRequestRepository, _quotationRequestRepository,
                _purchaseOrderRepository, _userRepository, _purchaseTypeRepository);
            return Json(_viewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PODetail(int PurchaseOrderID)
        {
            PurchaseOrder _po = _purchaseOrderRepository.QueryByID(PurchaseOrderID);
            ViewBag.PurchaseOrderID = PurchaseOrderID;
            ViewBag.Title = "采购单详情";
            return View(_po);
        }

        public ActionResult JosnPOContents(int PurchaseOrderID)
        {
            POContentGridViewModel _poContents = new POContentGridViewModel(_poContentRepository.QueryByPOID(PurchaseOrderID),
                _purchaseRequestRepository, _purchaseItemRepository);
            return Json(_poContents, JsonRequestBehavior.AllowGet);
        }

        public void DeletePOContent(int POContentID)
        {
            try
            {
                POContent _content = _poContentRepository.QueryByID(POContentID);
                PurchaseOrder _po = _purchaseOrderRepository.QueryByID(_content.PurchaseOrderID);

                _poContentRepository.Delete(POContentID);
                _po.TotalPrice = _poContentRepository.QueryByPOID(_po.PurchaseOrderID).Sum(p => p.SubTotal);
                _purchaseOrderRepository.Save(_po);

                PurchaseItem _purchaseItem = _purchaseItemRepository.QueryByID(_content.PurchaseItemID);
                _purchaseItem.PurchaseOrderID = 0;
                if (_purchaseItem.SupplierID > 0)
                {
                    _purchaseItem.State = (int)PurchaseItemStatus.待采购;
                }
                else
                {
                    _purchaseItem.State = (int)PurchaseItemStatus.待询价;
                }

                _purchaseItemRepository.Save(_purchaseItem);

            }
            catch
            {

            }
        }

        public void SubmitPO(int PurchaseOrderID)
        {
            try
            {
                _purchaseOrderRepository.Submit(PurchaseOrderID, (int)PurchaseRequestStatus.待审批);

                _purchaseItemRepository.ChangeState(0, 0, PurchaseOrderID, (int)PurchaseItemStatus.订单待审批);
            }
            catch
            {

            }
        }


        public string ReviewPO(int PurchaseOrderID, int ResponseType, string Memo = "")
        {
            string _msg = "";

            PurchaseOrder _po = _purchaseOrderRepository.QueryByID(PurchaseOrderID);


            if (ResponseType == (int)PurchaseOrderStatus.发布)
            {
                if (IsOutSource(_po.PurchaseType))
                {
                    _purchaseOrderRepository.Submit(PurchaseOrderID, (int)PurchaseOrderStatus.外发待出库, Memo);
                }
                else
                {
                    _purchaseOrderRepository.Submit(PurchaseOrderID, ResponseType, Memo);
                }
            }
            else
            {
                _purchaseOrderRepository.Submit(PurchaseOrderID, ResponseType, Memo);
            }



            PurchaseRequest _request = _purchaseRequestRepository.GetByID(PurchaseOrderID);



            string response = ResponseType == (int)PurchaseOrderStatus.发布 ? "批准" : "拒绝";

            if (ResponseType == (int)PurchaseOrderStatus.发布)
            {
                if (IsOutSource(_po.PurchaseType))
                {
                    _purchaseItemRepository.ChangeState(0, 0, PurchaseOrderID, (int)PurchaseItemStatus.外发项待出库);
                }
                else
                {
                    _purchaseItemRepository.ChangeState(0, 0, PurchaseOrderID, (int)PurchaseItemStatus.订单待发);
                }

            }
            else
            {
                _purchaseItemRepository.ChangeState(0, 0, PurchaseOrderID, (int)PurchaseItemStatus.订单审批拒绝);
            }

            return _msg;
        }


        private bool IsOutSource(int PurchaseType)
        {
            IEnumerable<int> _outsource = _purchaseTypeRepository.QueryByParentName("模具委外加工").Select(t => t.PurchaseTypeID);
            return _outsource.Contains(PurchaseType);

        }

        public ActionResult PurchaseItemList(int State = 0)
        {
            ViewBag.State = State;

            switch (State)
            {
                case 0:

                    ViewBag.Title = "采购项查询";
                    break;
                case 10:

                    ViewBag.Title = "在购零件清单";
                    break;
                case 20:

                    ViewBag.Title = "待收货零件";
                    break;
                case 30:
                    ViewBag.Title = "待处理采购项";
                    break;
                case 40:

                    ViewBag.Title = "零件采购清单";
                    break;
                case 50:

                    ViewBag.Title = "历史采购任务";
                    break;
                default:

                    ViewBag.Title = "采购项查询";
                    break;
            }
            return View();
        }

        public ActionResult OutSourceItemList(int State = 0)
        {
            ViewBag.State = State;
            ViewBag.PurchaseType = 3;
            ViewBag.Title = "外发任务清单";
            return View();
        }

        /// <summary>
        /// Search the purchaseItem to display in-purchase items
        /// </summary>
        /// <param name="Keyword">Item Keyword</param>
        /// <param name="MoldNumber">MoldNumber</param>
        /// <param name="State">PurchaseItem State
        /// 0: All PurchaseItems
        /// 10:Approved PR Items
        /// 20:In PO Items
        /// 50:FinishedItems
        /// </param>
        /// <returns></returns>
        public JsonResult JsonPurchaseItems(string Keyword = "", string MoldNumber = "", int State = 0, int PurchaseType = 1, string ExcluedIDs = "")
        {
            IEnumerable<PurchaseItem> _items;
            Expression<Func<PurchaseItem, bool>> _exp1 = null;
            Expression<Func<PurchaseItem, bool>> _exp2 = null;



            switch (State)
            {
                case 0:
                    //采购项查询：从pr提交通过到完成（包括完成） 	
                    _exp1 = p => p.State >= (int)PurchaseItemStatus.待询价;
                    break;
                case 10:
                    //在购零件清单：PO生成到完成之间（不包括完成）
                    _exp1 = i => i.State >= (int)PurchaseItemStatus.订单新建;
                    _exp2 = i => i.State < (int)PurchaseItemStatus.完成;
                    break;
                case 20:
                    //待收货零件：po审批通过到完成之间
                    _exp1 = i => i.State >= (int)PurchaseItemStatus.订单待发;
                    _exp2 = i => i.State < (int)PurchaseItemStatus.完成;
                    break;
                case 30:
                    //待处理采购项：pr审批通过到生成PO前（不含生成PO）
                    _exp1 = i => i.State >= (int)PurchaseItemStatus.待询价;
                    _exp2 = i => i.State <= (int)PurchaseItemStatus.待采购;
                    break;
                case 40:
                    //零件采购清单：PO生成到完成（包括完成）
                    _exp1 = i => i.State >= (int)PurchaseItemStatus.订单新建;
                    _exp2 = i => i.State <= (int)PurchaseItemStatus.完成;
                    break;
                case 50:
                    //历史采购任务
                    _exp1 = i => i.State == (int)PurchaseItemStatus.完成;
                    break;
                default:
                    //采购项查询：从pr提交通过到完成（包括完成） 	
                    _exp1 = p => p.State >= (int)PurchaseItemStatus.需求待审批;
                    break;
            }

            if (_exp2 != null)
            {
                _exp1 = PredicateBuilder.And(_exp1, _exp2);
            }


            IEnumerable<int> _type;
            if ((PurchaseType == 1) || (PurchaseType == 3))
            {
                _type = _purchaseTypeRepository.QueryByParentID(PurchaseType).Select(p => p.PurchaseTypeID);
                _exp2 = i => (_type.Contains(i.PurchaseType));
            }
            else
            {
                _exp2 = i => i.PurchaseType == PurchaseType;
            }
            _exp1 = PredicateBuilder.And(_exp1, _exp2);


            if (MoldNumber != "")
            {
                _exp1 = PredicateBuilder.And(_exp1, i => i.MoldNumber == MoldNumber);
            }


            if ((ExcluedIDs != "") && (ExcluedIDs != "undefined"))
            {
                string[] _exIDs = ExcluedIDs.Split(',');
                List<int> excludeIDs = new List<int>();
                for (int i = 0; i < _exIDs.Length; i++)
                {
                    excludeIDs.Add(Convert.ToInt32(_exIDs[i]));
                }
                _exp1 = PredicateBuilder.And(_exp1, i => (!excludeIDs.Contains(i.PurchaseItemID)));
            }

            Keyword = Keyword.ToUpper();



            _items = _purchaseItemRepository.PurchaseItems.Where(p => p.Name.ToUpper().Contains(Keyword)).Where(_exp1);

            PurchaseItemGridViewModel _viewModel = new PurchaseItemGridViewModel(_items,
                _purchaseRequestRepository,
                _quotationRequestRepository,
                _purchaseOrderRepository,
                _userRepository,
                _purchaseTypeRepository);

            return Json(_viewModel, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult POForm(int PurchaseOrderID)
        //{


        //    PurchaseOrder _pr = _purchaseOrderRepository.QueryByID(PurchaseOrderID);
        //    User _user =_userRepository.GetUserByID( _pr.Responsible
        //    IEnumerable<POContent> _poContents = _poContentRepository.QueryByPOID(PurchaseOrderID);
        //    POViewModel _model = new POViewModel(_poContents, _pr, );
        //    return View(_model);
        //}      


        public ActionResult SelectPOContent(string Keyword = "", int PurchaseType = 1, string PurchaseItemIDs = "")
        {
            if (Keyword != "null")
            {
                ViewBag.Keyword = Keyword;
            }
            else
            {
                ViewBag.Keyword = "";
            }

            ViewBag.PurchaseItemIDs = PurchaseItemIDs;

            int _purchaseType = GetPurchaseType(PurchaseItemIDs);
            if (GetPurchaseType(PurchaseItemIDs) == 0)
            {
                ViewBag.PurchaseType = PurchaseType;
            }
            else
            {
                ViewBag.PurchaseType = _purchaseType;
            }

            return View();
        }

        public int GetPurchaseType(string PurchaseItemIDs)
        {
            if (PurchaseItemIDs != "")
            {
                string[] _ids = PurchaseItemIDs.Split(',');
                List<int> _ItemIDs = new List<int>();
                for (int i = 0; i < _ids.Length; i++)
                {
                    _ItemIDs.Add(Convert.ToInt32(_ids[i]));
                }
                IEnumerable<int> _purchaseTypes = _purchaseItemRepository.PurchaseItems.Where(p => (_ItemIDs.Contains(p.PurchaseItemID))).Select(p => p.PurchaseType)
                    .Distinct();
                if (_purchaseTypes.Count() == 1)
                {
                    return _purchaseTypes.FirstOrDefault();
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }

        }


        public ActionResult AddPOItem(string ItemIDs)
        {
            List<PurchaseItem> _items = new List<PurchaseItem>();
            List<int> _exist = new List<int>();
            string[] _ids = ItemIDs.Split(',');
            for (int i = 0; i < _ids.Length; i++)
            {
                var _id = Convert.ToInt32(_ids[i]);
                _items.Add(_purchaseItemRepository.QueryByID(_id));
            }

            PurchaseOrderItemGridViewModel _viewModel = new PurchaseOrderItemGridViewModel(_items.Distinct().ToList());
            return Json(_viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string CreatePurchaseOrder(IEnumerable<PurchaseOrderItemEditModel> POContents,
            int Supplier, string Currency, string TaxRate, int PurchaseType, string SupplierName)
        {
            int _purchaseUserID;
            int _purchaseOrderID = 0;
            int _purchaseItemID;
            List<PurchaseItem> _purchaseItems = new List<PurchaseItem>();
            List<POContent> _poContents = new List<POContent>();
            string _error = "";
            try
            {
                _purchaseUserID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
            }
            catch
            {
                _purchaseUserID = 0;
            }

            try
            {
                PurchaseOrder _po = new PurchaseOrder();

                _po.SupplierID = Supplier;
                _po.SupplierName = SupplierName;
                _po.Responsible = _purchaseUserID;
                _po.State = (int)PurchaseOrderStatus.新建;
                _po.PurchaseOrderNumber = _sequenceRepository.GetNextNumber("purchaseorder");
                _po.PurchaseType = PurchaseType;
                _po.TotalPrice = POContents.Sum(p => p.SubTotal);
                _po.TaxRate = Convert.ToInt16(TaxRate);
                _po.Currency = Currency;
                _po.UserID = _purchaseUserID;

                _purchaseOrderID = _purchaseOrderRepository.Save(_po);


                foreach (PurchaseOrderItemEditModel _model in POContents)
                {
                    PurchaseItem _item = _purchaseItemRepository.QueryByID(_model.PurchaseItemID);
                    _item.Quantity = _model.Quantity;
                    _item.UnitPrice = _model.UnitPrice;
                    _item.TotalPrice = _model.SubTotal;
                    _item.RequireTime = _model.PlanTime;
                    _item.PurchaseOrderID = _purchaseOrderID;
                    _item.PurchaseUserID = _purchaseUserID;
                    _item.State = (int)PurchaseItemStatus.订单新建;
                    _item.SupplierID = Supplier;
                    _item.SupplierName = SupplierName;

                    _purchaseItems.Add(_item);
                    _purchaseItemID = _purchaseItemRepository.Save(_item);

                    _poContents.Add(new POContent(_item, _purchaseItemID));

                }

                _poContentRepository.BatchCreate(_poContents);

            }
            catch
            {
                _error = "订单保存失败";
            }



            return _error;
        }


        public ActionResult JsonQRSupplierDetail(int SupplierID, int QuotationID)
        {
            QRSupplier _qrSupplier = _qrSupplierRepository.QRSuppliers.Where(q => q.SupplierID == SupplierID)
                .Where(q => q.QuotationRequestID == QuotationID).Where(q => q.Enabled == true).FirstOrDefault();
            return Json(_qrSupplier, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonPurchaseTypes(string TypeName = "", bool ContainParent = true)
        {

            List<PurchaseType> _types;

            if (TypeName != "")
            {
                _types = _purchaseTypeRepository.QueryByParentName(TypeName, ContainParent).ToList();
            }
            else
            {
                if (ContainParent)
                {
                    _types = _purchaseTypeRepository.PurchaseTypes.ToList();
                }
                else
                {
                    //默认类型 去掉委外加工
                    _types = _purchaseTypeRepository.PurchaseTypes.Where(p => p.ParentTypeID > 0 && p.ParentTypeID!= 3).ToList();
                }

            }



            return Json(_types, JsonRequestBehavior.AllowGet);
        }


        #endregion

        public ActionResult SideBar()
        {
            return PartialView();
        }


        public List<int> GetPurchaseType(int ParentID = 0)
        {
            List<int> _purchaseTypeIDs = new List<int>();
            if (ParentID > 0)
            {
                _purchaseTypeIDs = _purchaseTypeRepository.QueryByParentID(ParentID).Select(t => t.PurchaseTypeID).ToList();
                _purchaseTypeIDs.Add(ParentID);
            }

            return _purchaseTypeIDs;
        }

        public ActionResult GetPurchaseTypeInfo(int PurchaseTypeID)
        {
            PurchaseType _type = _purchaseTypeRepository.QueryByID(PurchaseTypeID);
            return Json(_type, JsonRequestBehavior.AllowGet);
        }


        public ActionResult JsonOutSourcePO()
        {
            //PurchaseType _type = _purchaseTypeRepository.QueryByName("外发采购");
            IEnumerable<PurchaseType> _purchaseTypes = _purchaseTypeRepository.QueryByParentName("模具委外加工");
            List<PurchaseTypePOViewModel> _viewModel = new List<PurchaseTypePOViewModel>();
            PurchaseTypePOViewModel _model;
            foreach (PurchaseType _type in _purchaseTypes)
            {
                _model = new PurchaseTypePOViewModel();

            }
            return Json(_viewModel, JsonRequestBehavior.AllowGet);
        }

        public string StartOutSource(string OutSourceItemIDs)
        {
            string[] _ids = OutSourceItemIDs.Split(',');
            string _errItem = "";
            int _purchaseOrderID = 0;
            for (int i = 0; i < _ids.Length; i++)
            {

                PurchaseItem _item = _purchaseItemRepository.QueryByID(Convert.ToInt32(_ids[i]));
                _purchaseOrderID = _item.PurchaseOrderID;
                _item.State = (int)PurchaseItemStatus.待收货;
                try
                {
                    _purchaseItemRepository.Save(_item);
                }
                catch
                {
                    _errItem = _errItem == "" ? _item.Name : "," + _item.Name;
                }
            }

            int _left = _purchaseItemRepository.QueryByPurchaseOrderID(_purchaseOrderID)
                .Where(p => p.State == (int)PurchaseItemStatus.外发项待出库).Count();
            if (_left == 0)
            {
                PurchaseOrder _po = _purchaseOrderRepository.QueryByID(_purchaseOrderID);
                _po.State = (int)PurchaseOrderStatus.发布;
                _purchaseOrderRepository.Save(_po);
            }
            return _errItem;
        }

        public ActionResult JsonMoldNumber(string Keyword = "", int State = 0, int PurchaseType = 1)
        {
            IEnumerable<string> _moldNumbers;
            Expression<Func<PurchaseItem, bool>> _exp1 = null;
            Expression<Func<PurchaseItem, bool>> _exp2 = null;



            switch (State)
            {
                case 0:
                    //采购项查询：从pr提交通过到完成（包括完成） 	
                    _exp1 = p => p.State >= (int)PurchaseItemStatus.待询价;
                    break;
                case 10:
                    //在购零件清单：PO生成到完成之间（不包括完成）
                    _exp1 = i => i.State >= (int)PurchaseItemStatus.订单新建;
                    _exp2 = i => i.State < (int)PurchaseItemStatus.完成;
                    break;
                case 20:
                    //待收货零件：po审批通过到完成之间
                    _exp1 = i => i.State >= (int)PurchaseItemStatus.订单待发;
                    _exp2 = i => i.State < (int)PurchaseItemStatus.完成;
                    break;
                case 30:
                    //待处理采购项：pr审批通过到生成PO前（不含生成PO）
                    _exp1 = i => i.State >= (int)PurchaseItemStatus.待询价;
                    _exp2 = i => i.State <= (int)PurchaseItemStatus.待采购;
                    break;
                case 40:
                    //零件采购清单：PO生成到完成（包括完成）
                    _exp1 = i => i.State >= (int)PurchaseItemStatus.订单新建;
                    _exp2 = i => i.State <= (int)PurchaseItemStatus.完成;
                    break;
                case 50:
                    //历史采购任务
                    _exp1 = i => i.State == (int)PurchaseItemStatus.完成;
                    break;
                default:
                    //采购项查询：从pr提交通过到完成（包括完成） 	
                    _exp1 = p => p.State >= (int)PurchaseItemStatus.需求待审批;
                    break;
            }

            if (_exp2 != null)
            {
                _exp1 = PredicateBuilder.And(_exp1, _exp2);
            }

            IEnumerable<int> _type;
            if ((PurchaseType == 1) || (PurchaseType == 3))
            {
                _type = _purchaseTypeRepository.QueryByParentID(PurchaseType).Select(p => p.PurchaseTypeID);
                _exp2 = i => (_type.Contains(i.PurchaseType));
            }
            else
            {
                _exp2 = i => i.PurchaseType == PurchaseType;
            }
            _exp1 = PredicateBuilder.And(_exp1, _exp2);

            Keyword = Keyword.ToUpper();

            _moldNumbers = _purchaseItemRepository.PurchaseItems.Where(p => p.Name.ToUpper().Contains(Keyword))
                .Where(_exp1).Select(p => p.MoldNumber).Distinct();

            return Json(_moldNumbers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonPurchaseTypeTree(int PurchaseTypeID = 0)
        {
            List<PurchaseType> _types = _purchaseTypeRepository.PurchaseTypeTree(PurchaseTypeID);
            return Json(_types, JsonRequestBehavior.AllowGet);
        }

        public string VerifyMoldNumber(string PartNumber)
        {
            string _moldNumber = "";

            if (PartNumber.IndexOf('-') > 0)
            {
                try
                {
                    _moldNumber = PartNumber.Substring(0, PartNumber.IndexOf('-'));
                }
                catch
                {
                    _moldNumber = "";
                }
            }
            else if (PartNumber.IndexOf('_') > 0)
            {
                try
                {
                    _moldNumber = PartNumber.Substring(0, PartNumber.IndexOf('_'));
                }
                catch
                {
                    _moldNumber = "";
                }
            }




            Project _project = _projectRepository.GetLatestActiveProject(_moldNumber);

            if (_project != null)
            {
                return _project.MoldNumber;
            }
            else
            {
                return "";
            }
        }

        public ActionResult SupplierBrandList(int SupplierID)
        {
            List<int> _brandIDs = _supplierBrandRepository.QueryBySupplier(SupplierID).Select(s => s.BrandID).ToList();
            IEnumerable<Brand> _brands = _brandRepository.Brands.Where(b => (_brandIDs.Contains(b.BrandID)));
            return Json(_brands, JsonRequestBehavior.AllowGet);
        }

        public void SaveSupplierBrands(int SupplierID, string BrandIDs)
        {
            List<int> _brandIDs = Array.ConvertAll(BrandIDs.Split(','), id => Convert.ToInt32(id)).ToList<int>();

            List<int> _removeBrandIDs = _supplierBrandRepository.QueryBySupplier(SupplierID)
                .Where(s => (!_brandIDs.Contains(s.BrandID))).Select(s => s.SupplierBrandID).ToList();

            foreach (int _supplierBrandID in _removeBrandIDs)
            {
                _supplierBrandRepository.Delete(_supplierBrandID);
            }

            foreach (int _brandID in _brandIDs)
            {
                SupplierBrand _suppBrand = new SupplierBrand(SupplierID, _brandID);
                _supplierBrandRepository.Save(_suppBrand);
            }

        }

        public double QueryProjectCost(string MoldNumber, int PurchaseType, string BeginDate, string EndDate)
        {
            List<int> _purchaseTypeList = new List<int>();
            _purchaseTypeList.Add(PurchaseType);
            _purchaseTypeList.AddRange(_purchaseTypeRepository.QueryByParentID(PurchaseType).Select(p => p.PurchaseTypeID));
            IEnumerable<PurchaseItem> _purchaseItems = _purchaseItemRepository.PurchaseItems
                .Where(p => p.MoldNumber == MoldNumber)
                .Where(p => (_purchaseTypeList.Contains(p.PurchaseType)))
                .Where(p => p.State == (int)PurchaseItemStatus.完成);
            double _total = 0;
            foreach (PurchaseItem _item in _purchaseItems)
            {
                _total = _total + _item.TotalPrice;
            }
            return _total;
        }

        public double GetQuotationTotal(int QuotationRequestID, int SupplierID, string PurchaseItemIDs)
        {
            double _total = 0;

            int[] _purchaseItemID = Array.ConvertAll<string, int>(PurchaseItemIDs.Split(','), delegate (string s) { return int.Parse(s); });


            IEnumerable<QRQuotation> _quotations;

            List<int> _qrcontentIDs = _qrContentRepository.QRContents.Where(q => (_purchaseItemID.Contains(q.PurchaseItemID)))
                    .Select(q => q.QRContentID).ToList<int>();

            _quotations = _qrQuotationRepository.QRQuotations.Where(q => q.QuotationRequestID == QuotationRequestID)
                .Where(q => q.SupplierID == SupplierID)
                .Where(q => q.Enabled == true)
                .Where(q => (_qrcontentIDs.Contains(q.QRContentID)));


            _total = _quotations.Sum(q => q.TotalPriceWT);

            return _total;
        }
        public ActionResult PurchaseTypeManage()
        {
            IEnumerable<PurchaseType> _topTypes = _purchaseTypeRepository.QueryByParentID(0);
            return View(_topTypes);
        }

        [HttpPost]
        public string SavePurchaseType(PurchaseType PurchaseType)
        {
            try
            {
                _purchaseTypeRepository.Save(PurchaseType);
            }
            catch
            {
                return "采购类型保存失败,请重试";
            }
            return "";
        }

        public string DeletePurchaseType(int PurchaseTypeID)
        {
            int _count = _purchaseOrderRepository.PurchaseOrders.Where(p => p.PurchaseType == PurchaseTypeID).Count();
            if (_count == 0)
            {
                _purchaseTypeRepository.DeletePurchaseType(PurchaseTypeID);
                return "";
            }
            else
            {
                return "系统中存在该类型采购订单,无法删除类型";
            }
        }

        public bool OrderOutSource(int PurchaseTypeID)
        {
            PurchaseType _type = _purchaseTypeRepository.QueryByID(PurchaseTypeID);
            if ((PurchaseTypeID == 3) || (_type.ParentTypeID == 3))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult FindHistoryPart(int PurchaseItemID)
        {
            PurchaseItem _item = _purchaseItemRepository.QueryByID(PurchaseItemID);
            IEnumerable<PurchaseItem> _existing = _purchaseItemRepository.PurchaseItems
                .Where(p => p.Specification == _item.Specification)
                .Where(p => p.Material == _item.Material)
                .Where(p => p.State == (int)PurchaseItemStatus.完成)
                .OrderByDescending(p => p.DeliveryTime)
                .Take(10);
            List<SimilarPurchaseItem> _history = new List<SimilarPurchaseItem>();
            foreach (PurchaseItem _eItem in _existing)
            {
                _history.Add(new SimilarPurchaseItem(_eItem));
            }
            return Json(_history, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CostCenter()
        {
            return View();
        }

        public ActionResult JsonCostCenters(string Keyword = "")
        {
            return Json(_costCenterRepository.Query(Keyword), JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonCostCenter(int CostCenterID)
        {
            return Json(_costCenterRepository.QueryByID(CostCenterID));
        }

        public int GetCostCenter(string Name)
        {
            int count = _costCenterRepository.CostCenters.Where(c => c.Name == Name).Where(c => c.Enabled == true).Count();
            return count;
        }

        [HttpPost]
        public int SaveCostCenter(CostCenter CostCenter)
        {
            CostCenter.Enabled = true;
            return _costCenterRepository.Save(CostCenter);
        }
        #region added by michael
        /// <summary>
        /// Get部门代码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDepCode(string Name)
        {
            CostCenter center = _costCenterRepository.CostCenters.Where(c => c.Name == Name).Where(c => c.Enabled == true).FirstOrDefault() ?? new CostCenter();
            return Json(center, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public bool DeleteCostCenter(int CostCenterID)
        {
            try
            {
                _costCenterRepository.Delete(CostCenterID);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string CancelPO(int PurchaseOrderID)
        {
            try
            {
                _purchaseOrderRepository.Submit(PurchaseOrderID, (int)PurchaseOrderStatus.取消);
                List<PurchaseItem> _items = _purchaseItemRepository.QueryByPurchaseOrderID(PurchaseOrderID).ToList();
                foreach (PurchaseItem _item in _items)
                {
                    _item.State = (int)PurchaseItemStatus.订单取消;
                    _purchaseItemRepository.Save(_item);
                }
                return "";
            }
            catch
            {
                return "fail";
            }
        }

        /// <summary>
        /// Remove all items which are in 订单取消 state
        /// And if PR does not contain any in work contents, close the PR.
        /// </summary>
        /// <param name="PRContentIDs"></param>
        /// <param name="PurchaseRequestID"></param>
        /// <returns></returns>
        public string RemovePOCancelItems(string PRContentIDs, int PurchaseRequestID)
        {
            int[] ids = Array.ConvertAll<string, int>(PRContentIDs.Split(), s => int.Parse(s));
            List<int> _itemIDs = _prContentRepository.PRContents.Where(p => ids.Contains(p.PRContentID)).Select(p => p.PurchaseItemID).ToList();
            List<PurchaseItem> _purchaseItems = _purchaseItemRepository.PurchaseItems.Where(p => _itemIDs.Contains(p.PurchaseItemID)).ToList();
            foreach (PurchaseItem _item in _purchaseItems)
            {
                _item.State = (int)PurchaseItemStatus.取消;
                _purchaseItemRepository.Save(_item);
            }
            List<int> _states = new List<int>();
            _states.Add((int)PurchaseItemStatus.待收货);
            _states.Add((int)PurchaseItemStatus.部分收货);
            _states.Add((int)PurchaseItemStatus.待询价);
            _states.Add((int)PurchaseItemStatus.待采购);
            int _count = _purchaseItemRepository.QueryByPurchaseRequestID(PurchaseRequestID).Where(p => (_states.Contains(p.State))).Count();
            if (_count == 0)
            {
                _purchaseRequestRepository.Submit(PurchaseRequestID,
                    (int)PurchaseRequestStatus.完成,
                    "",
                    Convert.ToInt32(Request.Cookies["User"]["UserID"]));
                return "删除成功,申请单同时关闭";
            }
            else
            {
                return "删除成功";
            }
        }
        #region ExportExcelForPart
        /// <summary>
        /// 导出零件信息
        /// </summary>
        /// <returns></returns>
        public string ExportExcelForPart(string PartID="")
        {
            //return ExportExcel<PartViewForExport>(ps);
            Workbook workbook = new Workbook();
            workbook.Open(AppDomain.CurrentDomain.BaseDirectory + "\\images\\material.xlsx", FileFormatType.Excel2007Xlsx);

            Worksheet worksheet = workbook.Worksheets[0];
            //worksheet.Name = "Page1";
            Cells cells = worksheet.Cells;
            cells.InsertRow(0);
            Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];//新增样式
            style.HorizontalAlignment = TextAlignmentType.Center;//文字居中  
            style.Font.Size = 11;//文字大小  
            style.Font.IsBold = true;//粗体 
            cells.SetRowHeight(0, 20);              //设置行高 

            List<string> listHead = new List<string>();
            #region MyRegion
            listHead.Add("代码");
            listHead.Add("名称");
            listHead.Add("明细");
            listHead.Add("审核人_FName");
            listHead.Add("物料全名");
            listHead.Add("助记码");
            listHead.Add("规格型号");
            listHead.Add("辅助属性类别_FName");
            listHead.Add("辅助属性类别_FNumber");
            listHead.Add("模具号");
            listHead.Add("零件号");
            listHead.Add("物料属性_FName");
            listHead.Add("物料分类_FName");
            listHead.Add("计量单位组_FName");
            listHead.Add("基本计量单位_FName");
            listHead.Add("基本计量单位_FGroupName");
            listHead.Add("采购计量单位_FName");
            listHead.Add("采购计量单位_FGroupName");
            listHead.Add("销售计量单位_FName");
            listHead.Add("销售计量单位_FGroupName");
            listHead.Add("生产计量单位_FName");
            listHead.Add("生产计量单位_FGroupName");
            listHead.Add("库存计量单位_FName");
            listHead.Add("库存计量单位_FGroupName");
            listHead.Add("辅助计量单位_FName");
            listHead.Add("辅助计量单位_FGroupName");
            listHead.Add("辅助计量单位换算率");
            listHead.Add("默认仓库_FName");
            listHead.Add("默认仓库_FNumber");
            listHead.Add("默认仓位_FName");
            listHead.Add("默认仓位_FGroupName");
            listHead.Add("默认仓管员_FName");
            listHead.Add("默认仓管员_FNumber");
            listHead.Add("来源_FName");
            listHead.Add("来源_FNumber");
            listHead.Add("数量精度");
            listHead.Add("最低存量");
            listHead.Add("最高存量");
            listHead.Add("安全库存数量");
            listHead.Add("使用状态_FName");
            listHead.Add("是否为设备");
            listHead.Add("设备编码");
            listHead.Add("是否为备件");
            listHead.Add("批准文号");
            listHead.Add("别名");
            listHead.Add("物料对应特性");
            listHead.Add("默认待检仓库_FName");
            listHead.Add("默认待检仓库_FNumber");
            listHead.Add("默认待检仓位_FName");
            listHead.Add("默认待检仓位_FGroupName");
            listHead.Add("品牌");
            listHead.Add("材料");
            listHead.Add("采购最高价");
            listHead.Add("采购最高价币别_FName");
            listHead.Add("采购最高价币别_FNumber");
            listHead.Add("委外加工最高价");
            listHead.Add("委外加工最高价币别_FName");
            listHead.Add("委外加工最高价币别_FNumber");
            listHead.Add("销售最低价");
            listHead.Add("销售最低价币别_FName");
            listHead.Add("销售最低价币别_FNumber");
            listHead.Add("是否销售");
            listHead.Add("采购负责人_FName");
            listHead.Add("采购负责人_FNumber");
            listHead.Add("采购部门");
            listHead.Add("毛利率(%)");
            listHead.Add("采购单价");
            listHead.Add("销售单价");
            listHead.Add("是否农林计税");
            listHead.Add("是否进行保质期管理");
            listHead.Add("保质期(天)");
            listHead.Add("是否需要库龄管理");
            listHead.Add("是否采用业务批次管理");
            listHead.Add("是否需要进行订补货计划的运算");
            listHead.Add("失效提前期(天)");
            listHead.Add("盘点周期单位_FName");
            listHead.Add("盘点周期");
            listHead.Add("每周/月第()天");
            listHead.Add("上次盘点日期");
            listHead.Add("外购超收比例(%)");
            listHead.Add("外购欠收比例(%)");
            listHead.Add("销售超交比例(%)");
            listHead.Add("销售欠交比例(%)");
            listHead.Add("完工超收比例(%)");
            listHead.Add("完工欠收比例(%)");
            listHead.Add("领料超收比例(%)");
            listHead.Add("领料欠收比例(%)");
            listHead.Add("计价方法_FName");
            listHead.Add("计划单价");
            listHead.Add("单价精度");
            listHead.Add("存货科目代码_FNumber");
            listHead.Add("销售收入科目代码_FNumber");
            listHead.Add("销售成本科目代码_FNumber");
            listHead.Add("成本差异科目代码_FNumber");
            listHead.Add("代管物资科目_FNumber");
            listHead.Add("税目代码_FName");
            listHead.Add("税率(%)");
            listHead.Add("成本项目_FName");
            listHead.Add("成本项目_FNumber");
            listHead.Add("是否进行序列号管理");
            listHead.Add("参与结转式成本还原");
            listHead.Add("备注");
            listHead.Add("网店货品名");
            listHead.Add("商家编码");
            listHead.Add("严格进行二维码数量校验");
            listHead.Add("单位包装数量");
            listHead.Add("计划策略_FName");
            listHead.Add("计划模式_FName");
            listHead.Add("订货策略_FName");
            listHead.Add("固定提前期");
            listHead.Add("变动提前期");
            listHead.Add("累计提前期");
            listHead.Add("订货间隔期(天)");
            listHead.Add("最小订货量");
            listHead.Add("最大订货量");
            listHead.Add("批量增量");
            listHead.Add("设置为固定再订货点");
            listHead.Add("再订货点");
            listHead.Add("固定/经济批量");
            listHead.Add("变动提前期批量");
            listHead.Add("批量拆分间隔天数");
            listHead.Add("拆分批量");
            listHead.Add("需求时界(天)");
            listHead.Add("计划时界(天)");
            listHead.Add("默认工艺路线_FInterID");
            listHead.Add("默认工艺路线_FRoutingName");
            listHead.Add("默认生产类型_FName");
            listHead.Add("默认生产类型_FNumber");
            listHead.Add("生产负责人_FName");
            listHead.Add("生产负责人_FNumber");
            listHead.Add("计划员_FName");
            listHead.Add("计划员_FNumber");
            listHead.Add("是否倒冲");
            listHead.Add("倒冲仓库_FName");
            listHead.Add("倒冲仓库_FNumber");
            listHead.Add("倒冲仓位_FName");
            listHead.Add("倒冲仓位_FGroupName");
            listHead.Add("投料自动取整");
            listHead.Add("日消耗量");
            listHead.Add("MRP计算是否合并需求");
            listHead.Add("MRP计算是否产生采购申请");
            listHead.Add("控制类型_FName");
            listHead.Add("控制策略_FName");
            listHead.Add("容器名称");
            listHead.Add("看板容量");
            listHead.Add("辅助属性参与计划运算");
            listHead.Add("产品设计员_FName");
            listHead.Add("产品设计员_FNumber");
            listHead.Add("图号");
            listHead.Add("是否关键件");
            listHead.Add("毛重");
            listHead.Add("净重");
            listHead.Add("重量单位_FName");
            listHead.Add("重量单位_FGroupName");
            listHead.Add("长度");
            listHead.Add("宽度");
            listHead.Add("高度");
            listHead.Add("体积");
            listHead.Add("长度单位_FName");
            listHead.Add("长度单位_FGroupName");
            listHead.Add("版本号");
            listHead.Add("单位标准成本");
            listHead.Add("附加费率(%)");
            listHead.Add("附加费所属成本项目_FNumber");
            listHead.Add("成本BOM_FBOMNumber");
            listHead.Add("成本工艺路线_FInterID");
            listHead.Add("成本工艺路线_FRoutingName");
            listHead.Add("标准加工批量");
            listHead.Add("单位标准工时(小时)");
            listHead.Add("标准工资率");
            listHead.Add("变动制造费用分配率");
            listHead.Add("单位标准固定制造费用金额");
            listHead.Add("单位委外加工费");
            listHead.Add("委外加工费所属成本项目_FNumber");
            listHead.Add("单位计件工资");
            listHead.Add("采购订单差异科目代码_FNumber");
            listHead.Add("采购发票差异科目代码_FNumber");
            listHead.Add("材料成本差异科目代码_FNumber");
            listHead.Add("加工费差异科目代码_FNumber");
            listHead.Add("废品损失科目代码_FNumber");
            listHead.Add("标准成本调整差异科目代码_FNumber");
            listHead.Add("采购检验方式_FName");
            listHead.Add("产品检验方式_FName");
            listHead.Add("委外加工检验方式_FName");
            listHead.Add("发货检验方式_FName");
            listHead.Add("退货检验方式_FName");
            listHead.Add("库存检验方式_FName");
            listHead.Add("其他检验方式_FName");
            listHead.Add("抽样标准(致命)_FName");
            listHead.Add("抽样标准(致命)_FNumber");
            listHead.Add("抽样标准(严重)_FName");
            listHead.Add("抽样标准(严重)_FNumber");
            listHead.Add("抽样标准(轻微)_FName");
            listHead.Add("抽样标准(轻微)_FNumber");
            listHead.Add("库存检验周期(天)");
            listHead.Add("库存周期检验预警提前期(天)");
            listHead.Add("检验方案_FInterID");
            listHead.Add("检验方案_FBrNo");
            listHead.Add("检验员_FName");
            listHead.Add("检验员_FNumber");
            listHead.Add("英文名称");
            listHead.Add("英文规格");
            listHead.Add("HS编码_FHSCode");
            listHead.Add("HS编码_FNumber");
            listHead.Add("外销税率%");
            listHead.Add("HS第一法定单位");
            listHead.Add("HS第二法定单位");
            listHead.Add("进口关税率%");
            listHead.Add("进口消费税率%");
            listHead.Add("HS第一法定单位换算率");
            listHead.Add("HS第二法定单位换算率");
            listHead.Add("是否保税监管");
            listHead.Add("物料监管类型_FName");
            listHead.Add("物料监管类型_FNumber");
            listHead.Add("长度精度");
            listHead.Add("体积精度");
            listHead.Add("重量精度");
            listHead.Add("启用服务");
            listHead.Add("生成产品档案");
            listHead.Add("维修件");
            listHead.Add("保修期限（月）");
            listHead.Add("使用寿命（月）");
            listHead.Add("控制");
            listHead.Add("是否禁用");
            listHead.Add("全球唯一标识内码");

            #endregion
            List<string> listHead1 = new List<string>();
            #region MyRegion
            listHead1.Add("代码");
            listHead1.Add("名称");
            listHead1.Add("明细");
            listHead1.Add("审核人_FName");
            listHead1.Add("物料全名");
            listHead1.Add("助记码");
            listHead1.Add("规格型号");
            listHead1.Add("辅助属性类别_FName");
            listHead1.Add("辅助属性类别_FNumber");
            listHead1.Add("模具号");
            listHead1.Add("零件号");
            listHead1.Add("物料属性_FName");
            listHead1.Add("物料分类_FName");
            listHead1.Add("计量单位组_FName");
            listHead1.Add("基本计量单位_FName");
            listHead1.Add("基本计量单位_FGroupName");
            listHead1.Add("采购计量单位_FName");
            listHead1.Add("采购计量单位_FGroupName");
            listHead1.Add("销售计量单位_FName");
            listHead1.Add("销售计量单位_FGroupName");
            listHead1.Add("生产计量单位_FName");
            listHead1.Add("生产计量单位_FGroupName");
            listHead1.Add("库存计量单位_FName");
            listHead1.Add("库存计量单位_FGroupName");
            listHead1.Add("辅助计量单位_FName");
            listHead1.Add("辅助计量单位_FGroupName");
            listHead1.Add("辅助计量单位换算率");
            listHead1.Add("默认仓库_FName");
            listHead1.Add("默认仓库_FNumber");
            listHead1.Add("默认仓位_FName");
            listHead1.Add("默认仓位_FGroupName");
            listHead1.Add("默认仓管员_FName");
            listHead1.Add("默认仓管员_FNumber");
            listHead1.Add("来源_FName");
            listHead1.Add("来源_FNumber");
            listHead1.Add("数量精度");
            listHead1.Add("最低存量");
            listHead1.Add("最高存量");
            listHead1.Add("安全库存数量");
            listHead1.Add("使用状态_FName");
            listHead1.Add("是否为设备");
            listHead1.Add("设备编码");
            listHead1.Add("是否为备件");
            listHead1.Add("批准文号");
            listHead1.Add("别名");
            listHead1.Add("物料对应特性");
            listHead1.Add("默认待检仓库_FName");
            listHead1.Add("默认待检仓库_FNumber");
            listHead1.Add("默认待检仓位_FName");
            listHead1.Add("默认待检仓位_FGroupName");
            listHead1.Add("品牌");
            listHead1.Add("材料");

            listHead1.Add("采购最高价");
            listHead1.Add("采购最高价币别_FName");
            listHead1.Add("采购最高价币别_FNumber");
            listHead1.Add("委外加工最高价");
            listHead1.Add("委外加工最高价币别_FName");
            listHead1.Add("委外加工最高价币别_FNumber");
            listHead1.Add("销售最低价");
            listHead1.Add("销售最低价币别_FName");
            listHead1.Add("销售最低价币别_FNumber");
            listHead1.Add("是否销售");
            listHead1.Add("采购负责人_FName");
            listHead1.Add("采购负责人_FNumber");
            listHead1.Add("采购部门");
            listHead1.Add("毛利率");
            listHead1.Add("采购单价");
            listHead1.Add("销售单价");
            listHead1.Add("是否农林计税");
            listHead1.Add("是否进行保质期管理");
            listHead1.Add("保质期天");
            listHead1.Add("是否需要库龄管理");
            listHead1.Add("是否采用业务批次管理");
            listHead1.Add("是否需要进行订补货计划的运算");
            listHead1.Add("失效提前期天");
            listHead1.Add("盘点周期单位_FName");
            listHead1.Add("盘点周期");
            listHead1.Add("每周月第天");
            listHead1.Add("上次盘点日期");
            listHead1.Add("外购超收比例");
            listHead1.Add("外购欠收比例");
            listHead1.Add("销售超交比例");
            listHead1.Add("销售欠交比例");
            listHead1.Add("完工超收比例");
            listHead1.Add("完工欠收比例");
            listHead1.Add("领料超收比例");
            listHead1.Add("领料欠收比例");
            listHead1.Add("计价方法_FName");
            listHead1.Add("计划单价");
            listHead1.Add("单价精度");
            listHead1.Add("存货科目代码_FNumber");
            listHead1.Add("销售收入科目代码_FNumber");
            listHead1.Add("销售成本科目代码_FNumber");
            listHead1.Add("成本差异科目代码_FNumber");
            listHead1.Add("代管物资科目_FNumber");
            listHead1.Add("税目代码_FName");
            listHead1.Add("税率");
            listHead1.Add("成本项目_FName");
            listHead1.Add("成本项目_FNumber");
            listHead1.Add("是否进行序列号管理");
            listHead1.Add("参与结转式成本还原");
            listHead1.Add("备注");
            listHead1.Add("网店货品名");
            listHead1.Add("商家编码");
            listHead1.Add("严格进行二维码数量校验");
            listHead1.Add("单位包装数量");
            listHead1.Add("计划策略_FName");
            listHead1.Add("计划模式_FName");
            listHead1.Add("订货策略_FName");
            listHead1.Add("固定提前期");
            listHead1.Add("变动提前期");
            listHead1.Add("累计提前期");
            listHead1.Add("订货间隔期天");
            listHead1.Add("最小订货量");
            listHead1.Add("最大订货量");
            listHead1.Add("批量增量");
            listHead1.Add("设置为固定再订货点");
            listHead1.Add("再订货点");
            listHead1.Add("固定经济批量");
            listHead1.Add("变动提前期批量");
            listHead1.Add("批量拆分间隔天数");
            listHead1.Add("拆分批量");
            listHead1.Add("需求时界天");
            listHead1.Add("计划时界天");
            listHead1.Add("默认工艺路线_FInterID");
            listHead1.Add("默认工艺路线_FRoutingName");
            listHead1.Add("默认生产类型_FName");
            listHead1.Add("默认生产类型_FNumber");
            listHead1.Add("生产负责人_FName");
            listHead1.Add("生产负责人_FNumber");
            listHead1.Add("计划员_FName");
            listHead1.Add("计划员_FNumber");
            listHead1.Add("是否倒冲");
            listHead1.Add("倒冲仓库_FName");
            listHead1.Add("倒冲仓库_FNumber");
            listHead1.Add("倒冲仓位_FName");
            listHead1.Add("倒冲仓位_FGroupName");
            listHead1.Add("投料自动取整");
            listHead1.Add("日消耗量");
            listHead1.Add("MRP计算是否合并需求");
            listHead1.Add("MRP计算是否产生采购申请");
            listHead1.Add("控制类型_FName");
            listHead1.Add("控制策略_FName");
            listHead1.Add("容器名称");
            listHead1.Add("看板容量");
            listHead1.Add("辅助属性参与计划运算");
            listHead1.Add("产品设计员_FName");
            listHead1.Add("产品设计员_FNumber");
            listHead1.Add("图号");
            listHead1.Add("是否关键件");
            listHead1.Add("毛重");
            listHead1.Add("净重");
            listHead1.Add("重量单位_FName");
            listHead1.Add("重量单位_FGroupName");
            listHead1.Add("长度");
            listHead1.Add("宽度");
            listHead1.Add("高度");
            listHead1.Add("体积");
            listHead1.Add("长度单位_FName");
            listHead1.Add("长度单位_FGroupName");
            listHead1.Add("版本号");
            listHead1.Add("单位标准成本");
            listHead1.Add("附加费率");
            listHead1.Add("附加费所属成本项目_FNumber");
            listHead1.Add("成本BOM_FBOMNumber");
            listHead1.Add("成本工艺路线_FInterID");
            listHead1.Add("成本工艺路线_FRoutingName");
            listHead1.Add("标准加工批量");
            listHead1.Add("单位标准工时小时");
            listHead1.Add("标准工资率");
            listHead1.Add("变动制造费用分配率");
            listHead1.Add("单位标准固定制造费用金额");
            listHead1.Add("单位委外加工费");
            listHead1.Add("委外加工费所属成本项目_FNumber");
            listHead1.Add("单位计件工资");
            listHead1.Add("采购订单差异科目代码_FNumber");
            listHead1.Add("采购发票差异科目代码_FNumber");
            listHead1.Add("材料成本差异科目代码_FNumber");
            listHead1.Add("加工费差异科目代码_FNumber");
            listHead1.Add("废品损失科目代码_FNumber");
            listHead1.Add("标准成本调整差异科目代码_FNumber");
            listHead1.Add("采购检验方式_FName");
            listHead1.Add("产品检验方式_FName");
            listHead1.Add("委外加工检验方式_FName");
            listHead1.Add("发货检验方式_FName");
            listHead1.Add("退货检验方式_FName");
            listHead1.Add("库存检验方式_FName");
            listHead1.Add("其他检验方式_FName");
            listHead1.Add("抽样标准致命_FName");
            listHead1.Add("抽样标准致命_FNumber");
            listHead1.Add("抽样标准严重_FName");
            listHead1.Add("抽样标准严重_FNumber");
            listHead1.Add("抽样标准轻微_FName");
            listHead1.Add("抽样标准轻微_FNumber");
            listHead1.Add("库存检验周期天");
            listHead1.Add("库存周期检验预警提前期天");
            listHead1.Add("检验方案_FInterID");
            listHead1.Add("检验方案_FBrNo");
            listHead1.Add("检验员_FName");
            listHead1.Add("检验员_FNumber");
            listHead1.Add("英文名称");
            listHead1.Add("英文规格");
            listHead1.Add("HS编码_FHSCode");
            listHead1.Add("HS编码_FNumber");
            listHead1.Add("外销税率");
            listHead1.Add("HS第一法定单位");
            listHead1.Add("HS第二法定单位");
            listHead1.Add("进口关税率");
            listHead1.Add("进口消费税率");
            listHead1.Add("HS第一法定单位换算率");
            listHead1.Add("HS第二法定单位换算率");
            listHead1.Add("是否保税监管");
            listHead1.Add("物料监管类型_FName");
            listHead1.Add("物料监管类型_FNumber");
            listHead1.Add("长度精度");
            listHead1.Add("体积精度");
            listHead1.Add("重量精度");
            listHead1.Add("启用服务");
            listHead1.Add("生成产品档案");
            listHead1.Add("维修件");
            listHead1.Add("保修期限月");
            listHead1.Add("使用寿命月");
            listHead1.Add("控制");
            listHead1.Add("是否禁用");
            listHead1.Add("全球唯一标识内码");

            #endregion

            for (int i = 0; i < listHead.Count; i++)
            {
                cells[0, i].PutValue(listHead[i]);
                cells[0, i].Style = style;
                cells.SetColumnWidth(i, 30);
            }

            List<PartViewForExport> list = new List<PartViewForExport>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EFDbContext"].ToString());

            if (PartID.Contains(","))
            //if (PurchaseRequestID>0)
            {
                string[] strArr = PartID.Split(',');
                //构建dt
                //string sql1 = "execute Pro_RefreshERPData '" + "0" + "'";
                //SqlCommand comm1 = new SqlCommand(sql1, conn);
                //SqlDataAdapter da1= new SqlDataAdapter(comm1);
                //DataSet ds1 = new DataSet();
                //da1.Fill(ds1, "test");
                //DataTable Exceldt= ds1.Tables[0].Clone();
                foreach (string str in strArr)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        #region MyRegion 
                        string sql = "execute Pro_RefreshERPData '" + str + "'";
                        SqlCommand comm = new SqlCommand(sql, conn);
                        SqlDataAdapter da = new SqlDataAdapter(comm);
                        DataSet ds = new DataSet();
                        da.Fill(ds, "test");
                        #endregion
                        if (ds == null && ds.Tables.Count == 0)
                            return "false";
                        //Exceldt = FilterRepeatTable(Exceldt, "代码");
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            PartViewForExport pvfe = new PartViewForExport();
                            Type partInfo = typeof(PartViewForExport);
                            PropertyInfo[] pis = partInfo.GetProperties();
                            foreach (PropertyInfo pi in pis)
                            {
                                if (ds.Tables[0].Columns.Contains(pi.Name))
                                {
                                    pi.SetValue(pvfe, dr[pi.Name].ToString());
                                }
                            }
                            list.Add(pvfe);
                        }                        
                    }
                }
                //过滤重复行 michael
                List<PartViewForExport> list1 = new List<PartViewForExport>();
                foreach (var pv in list)
                {
                    int rowCount = (from PartViewForExport q in list1 where q.代码.ToString() == pv.代码.ToString() where pv.代码.ToString() != "未查询到物料分类，请至ERP创建" select q).ToList().Count();//where pv.代码.ToString() != "未查询到物料分类，请至ERP创建"
                    if (rowCount == 0)
                    {
                        list1.Add(pv);
                    }
                }
                if (list1.Count > 0)
                {
                    for (int i = 1; i <= list1.Count; i++)
                    {
                        PartViewForExport pv = list1[i - 1];

                        for (int j = 1; j <= listHead1.Count; j++)
                        {
                            string name = listHead1[j - 1];
                            PropertyInfo p = typeof(PartViewForExport).GetProperty(name);
                            string value = p.GetValue(pv, null) == null ? "" : p.GetValue(pv, null).ToString(); ;
                            cells[i, j - 1].PutValue(value);
                            cells.SetColumnWidth(i, 30);
                        }
                    }
                    workbook.Save("物料" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx", FileFormatType.Excel2007Xlsx, SaveType.OpenInExcel, System.Web.HttpContext.Current.Response);
                }                
            }
            return "ok";
        }
        #endregion
        #region ERP料号同步 michael
        /// <summary>
        /// 导出零件信息  michael 180608
        /// </summary>
        /// <returns></returns>
        public string ExportExcelForPartByPR(string PurchaseRequestID,string PartID = "")
        {            
            PurchaseRequestID = PurchaseRequestID == "" ? "0" : PurchaseRequestID;
            int int_PurchaseRequestID = Convert.ToInt32(PurchaseRequestID);
            if (int_PurchaseRequestID == 0)
                return "NG";
            //return ExportExcel<PartViewForExport>(ps);
            Workbook workbook = new Workbook();
            workbook.Open(AppDomain.CurrentDomain.BaseDirectory + "\\images\\material.xlsx", FileFormatType.Excel2007Xlsx);

            Worksheet worksheet = workbook.Worksheets[0];
            //worksheet.Name = "Page1";
            Cells cells = worksheet.Cells;
            cells.InsertRow(0);
            Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];//新增样式
            style.HorizontalAlignment = TextAlignmentType.Center;//文字居中  
            style.Font.Size = 11;//文字大小  
            style.Font.IsBold = true;//粗体 
            cells.SetRowHeight(0, 20);              //设置行高 

            List<string> listHead = new List<string>();
            #region MyRegion
            listHead.Add("代码");
            listHead.Add("名称");
            listHead.Add("明细");
            listHead.Add("审核人_FName");
            listHead.Add("物料全名");
            listHead.Add("助记码");
            listHead.Add("规格型号");
            listHead.Add("辅助属性类别_FName");
            listHead.Add("辅助属性类别_FNumber");
            listHead.Add("模具号");
            listHead.Add("零件号");
            listHead.Add("物料属性_FName");
            listHead.Add("物料分类_FName");
            listHead.Add("计量单位组_FName");
            listHead.Add("基本计量单位_FName");
            listHead.Add("基本计量单位_FGroupName");
            listHead.Add("采购计量单位_FName");
            listHead.Add("采购计量单位_FGroupName");
            listHead.Add("销售计量单位_FName");
            listHead.Add("销售计量单位_FGroupName");
            listHead.Add("生产计量单位_FName");
            listHead.Add("生产计量单位_FGroupName");
            listHead.Add("库存计量单位_FName");
            listHead.Add("库存计量单位_FGroupName");
            listHead.Add("辅助计量单位_FName");
            listHead.Add("辅助计量单位_FGroupName");
            listHead.Add("辅助计量单位换算率");
            listHead.Add("默认仓库_FName");
            listHead.Add("默认仓库_FNumber");
            listHead.Add("默认仓位_FName");
            listHead.Add("默认仓位_FGroupName");
            listHead.Add("默认仓管员_FName");
            listHead.Add("默认仓管员_FNumber");
            listHead.Add("来源_FName");
            listHead.Add("来源_FNumber");
            listHead.Add("数量精度");
            listHead.Add("最低存量");
            listHead.Add("最高存量");
            listHead.Add("安全库存数量");
            listHead.Add("使用状态_FName");
            listHead.Add("是否为设备");
            listHead.Add("设备编码");
            listHead.Add("是否为备件");
            listHead.Add("批准文号");
            listHead.Add("别名");
            listHead.Add("物料对应特性");
            listHead.Add("默认待检仓库_FName");
            listHead.Add("默认待检仓库_FNumber");
            listHead.Add("默认待检仓位_FName");
            listHead.Add("默认待检仓位_FGroupName");
            listHead.Add("品牌");
            listHead.Add("材料");
            listHead.Add("采购最高价");
            listHead.Add("采购最高价币别_FName");
            listHead.Add("采购最高价币别_FNumber");
            listHead.Add("委外加工最高价");
            listHead.Add("委外加工最高价币别_FName");
            listHead.Add("委外加工最高价币别_FNumber");
            listHead.Add("销售最低价");
            listHead.Add("销售最低价币别_FName");
            listHead.Add("销售最低价币别_FNumber");
            listHead.Add("是否销售");
            listHead.Add("采购负责人_FName");
            listHead.Add("采购负责人_FNumber");
            listHead.Add("采购部门");
            listHead.Add("毛利率(%)");
            listHead.Add("采购单价");
            listHead.Add("销售单价");
            listHead.Add("是否农林计税");
            listHead.Add("是否进行保质期管理");
            listHead.Add("保质期(天)");
            listHead.Add("是否需要库龄管理");
            listHead.Add("是否采用业务批次管理");
            listHead.Add("是否需要进行订补货计划的运算");
            listHead.Add("失效提前期(天)");
            listHead.Add("盘点周期单位_FName");
            listHead.Add("盘点周期");
            listHead.Add("每周/月第()天");
            listHead.Add("上次盘点日期");
            listHead.Add("外购超收比例(%)");
            listHead.Add("外购欠收比例(%)");
            listHead.Add("销售超交比例(%)");
            listHead.Add("销售欠交比例(%)");
            listHead.Add("完工超收比例(%)");
            listHead.Add("完工欠收比例(%)");
            listHead.Add("领料超收比例(%)");
            listHead.Add("领料欠收比例(%)");
            listHead.Add("计价方法_FName");
            listHead.Add("计划单价");
            listHead.Add("单价精度");
            listHead.Add("存货科目代码_FNumber");
            listHead.Add("销售收入科目代码_FNumber");
            listHead.Add("销售成本科目代码_FNumber");
            listHead.Add("成本差异科目代码_FNumber");
            listHead.Add("代管物资科目_FNumber");
            listHead.Add("税目代码_FName");
            listHead.Add("税率(%)");
            listHead.Add("成本项目_FName");
            listHead.Add("成本项目_FNumber");
            listHead.Add("是否进行序列号管理");
            listHead.Add("参与结转式成本还原");
            listHead.Add("备注");
            listHead.Add("网店货品名");
            listHead.Add("商家编码");
            listHead.Add("严格进行二维码数量校验");
            listHead.Add("单位包装数量");
            listHead.Add("计划策略_FName");
            listHead.Add("计划模式_FName");
            listHead.Add("订货策略_FName");
            listHead.Add("固定提前期");
            listHead.Add("变动提前期");
            listHead.Add("累计提前期");
            listHead.Add("订货间隔期(天)");
            listHead.Add("最小订货量");
            listHead.Add("最大订货量");
            listHead.Add("批量增量");
            listHead.Add("设置为固定再订货点");
            listHead.Add("再订货点");
            listHead.Add("固定/经济批量");
            listHead.Add("变动提前期批量");
            listHead.Add("批量拆分间隔天数");
            listHead.Add("拆分批量");
            listHead.Add("需求时界(天)");
            listHead.Add("计划时界(天)");
            listHead.Add("默认工艺路线_FInterID");
            listHead.Add("默认工艺路线_FRoutingName");
            listHead.Add("默认生产类型_FName");
            listHead.Add("默认生产类型_FNumber");
            listHead.Add("生产负责人_FName");
            listHead.Add("生产负责人_FNumber");
            listHead.Add("计划员_FName");
            listHead.Add("计划员_FNumber");
            listHead.Add("是否倒冲");
            listHead.Add("倒冲仓库_FName");
            listHead.Add("倒冲仓库_FNumber");
            listHead.Add("倒冲仓位_FName");
            listHead.Add("倒冲仓位_FGroupName");
            listHead.Add("投料自动取整");
            listHead.Add("日消耗量");
            listHead.Add("MRP计算是否合并需求");
            listHead.Add("MRP计算是否产生采购申请");
            listHead.Add("控制类型_FName");
            listHead.Add("控制策略_FName");
            listHead.Add("容器名称");
            listHead.Add("看板容量");
            listHead.Add("辅助属性参与计划运算");
            listHead.Add("产品设计员_FName");
            listHead.Add("产品设计员_FNumber");
            listHead.Add("图号");
            listHead.Add("是否关键件");
            listHead.Add("毛重");
            listHead.Add("净重");
            listHead.Add("重量单位_FName");
            listHead.Add("重量单位_FGroupName");
            listHead.Add("长度");
            listHead.Add("宽度");
            listHead.Add("高度");
            listHead.Add("体积");
            listHead.Add("长度单位_FName");
            listHead.Add("长度单位_FGroupName");
            listHead.Add("版本号");
            listHead.Add("单位标准成本");
            listHead.Add("附加费率(%)");
            listHead.Add("附加费所属成本项目_FNumber");
            listHead.Add("成本BOM_FBOMNumber");
            listHead.Add("成本工艺路线_FInterID");
            listHead.Add("成本工艺路线_FRoutingName");
            listHead.Add("标准加工批量");
            listHead.Add("单位标准工时(小时)");
            listHead.Add("标准工资率");
            listHead.Add("变动制造费用分配率");
            listHead.Add("单位标准固定制造费用金额");
            listHead.Add("单位委外加工费");
            listHead.Add("委外加工费所属成本项目_FNumber");
            listHead.Add("单位计件工资");
            listHead.Add("采购订单差异科目代码_FNumber");
            listHead.Add("采购发票差异科目代码_FNumber");
            listHead.Add("材料成本差异科目代码_FNumber");
            listHead.Add("加工费差异科目代码_FNumber");
            listHead.Add("废品损失科目代码_FNumber");
            listHead.Add("标准成本调整差异科目代码_FNumber");
            listHead.Add("采购检验方式_FName");
            listHead.Add("产品检验方式_FName");
            listHead.Add("委外加工检验方式_FName");
            listHead.Add("发货检验方式_FName");
            listHead.Add("退货检验方式_FName");
            listHead.Add("库存检验方式_FName");
            listHead.Add("其他检验方式_FName");
            listHead.Add("抽样标准(致命)_FName");
            listHead.Add("抽样标准(致命)_FNumber");
            listHead.Add("抽样标准(严重)_FName");
            listHead.Add("抽样标准(严重)_FNumber");
            listHead.Add("抽样标准(轻微)_FName");
            listHead.Add("抽样标准(轻微)_FNumber");
            listHead.Add("库存检验周期(天)");
            listHead.Add("库存周期检验预警提前期(天)");
            listHead.Add("检验方案_FInterID");
            listHead.Add("检验方案_FBrNo");
            listHead.Add("检验员_FName");
            listHead.Add("检验员_FNumber");
            listHead.Add("英文名称");
            listHead.Add("英文规格");
            listHead.Add("HS编码_FHSCode");
            listHead.Add("HS编码_FNumber");
            listHead.Add("外销税率%");
            listHead.Add("HS第一法定单位");
            listHead.Add("HS第二法定单位");
            listHead.Add("进口关税率%");
            listHead.Add("进口消费税率%");
            listHead.Add("HS第一法定单位换算率");
            listHead.Add("HS第二法定单位换算率");
            listHead.Add("是否保税监管");
            listHead.Add("物料监管类型_FName");
            listHead.Add("物料监管类型_FNumber");
            listHead.Add("长度精度");
            listHead.Add("体积精度");
            listHead.Add("重量精度");
            listHead.Add("启用服务");
            listHead.Add("生成产品档案");
            listHead.Add("维修件");
            listHead.Add("保修期限（月）");
            listHead.Add("使用寿命（月）");
            listHead.Add("控制");
            listHead.Add("是否禁用");
            listHead.Add("全球唯一标识内码");

            #endregion
            List<string> listHead1 = new List<string>();
            #region MyRegion
            listHead1.Add("代码");
            listHead1.Add("名称");
            listHead1.Add("明细");
            listHead1.Add("审核人_FName");
            listHead1.Add("物料全名");
            listHead1.Add("助记码");
            listHead1.Add("规格型号");
            listHead1.Add("辅助属性类别_FName");
            listHead1.Add("辅助属性类别_FNumber");
            listHead1.Add("模具号");
            listHead1.Add("零件号");
            listHead1.Add("物料属性_FName");
            listHead1.Add("物料分类_FName");
            listHead1.Add("计量单位组_FName");
            listHead1.Add("基本计量单位_FName");
            listHead1.Add("基本计量单位_FGroupName");
            listHead1.Add("采购计量单位_FName");
            listHead1.Add("采购计量单位_FGroupName");
            listHead1.Add("销售计量单位_FName");
            listHead1.Add("销售计量单位_FGroupName");
            listHead1.Add("生产计量单位_FName");
            listHead1.Add("生产计量单位_FGroupName");
            listHead1.Add("库存计量单位_FName");
            listHead1.Add("库存计量单位_FGroupName");
            listHead1.Add("辅助计量单位_FName");
            listHead1.Add("辅助计量单位_FGroupName");
            listHead1.Add("辅助计量单位换算率");
            listHead1.Add("默认仓库_FName");
            listHead1.Add("默认仓库_FNumber");
            listHead1.Add("默认仓位_FName");
            listHead1.Add("默认仓位_FGroupName");
            listHead1.Add("默认仓管员_FName");
            listHead1.Add("默认仓管员_FNumber");
            listHead1.Add("来源_FName");
            listHead1.Add("来源_FNumber");
            listHead1.Add("数量精度");
            listHead1.Add("最低存量");
            listHead1.Add("最高存量");
            listHead1.Add("安全库存数量");
            listHead1.Add("使用状态_FName");
            listHead1.Add("是否为设备");
            listHead1.Add("设备编码");
            listHead1.Add("是否为备件");
            listHead1.Add("批准文号");
            listHead1.Add("别名");
            listHead1.Add("物料对应特性");
            listHead1.Add("默认待检仓库_FName");
            listHead1.Add("默认待检仓库_FNumber");
            listHead1.Add("默认待检仓位_FName");
            listHead1.Add("默认待检仓位_FGroupName");
            listHead1.Add("品牌");
            listHead1.Add("材料");

            listHead1.Add("采购最高价");
            listHead1.Add("采购最高价币别_FName");
            listHead1.Add("采购最高价币别_FNumber");
            listHead1.Add("委外加工最高价");
            listHead1.Add("委外加工最高价币别_FName");
            listHead1.Add("委外加工最高价币别_FNumber");
            listHead1.Add("销售最低价");
            listHead1.Add("销售最低价币别_FName");
            listHead1.Add("销售最低价币别_FNumber");
            listHead1.Add("是否销售");
            listHead1.Add("采购负责人_FName");
            listHead1.Add("采购负责人_FNumber");
            listHead1.Add("采购部门");
            listHead1.Add("毛利率");
            listHead1.Add("采购单价");
            listHead1.Add("销售单价");
            listHead1.Add("是否农林计税");
            listHead1.Add("是否进行保质期管理");
            listHead1.Add("保质期天");
            listHead1.Add("是否需要库龄管理");
            listHead1.Add("是否采用业务批次管理");
            listHead1.Add("是否需要进行订补货计划的运算");
            listHead1.Add("失效提前期天");
            listHead1.Add("盘点周期单位_FName");
            listHead1.Add("盘点周期");
            listHead1.Add("每周月第天");
            listHead1.Add("上次盘点日期");
            listHead1.Add("外购超收比例");
            listHead1.Add("外购欠收比例");
            listHead1.Add("销售超交比例");
            listHead1.Add("销售欠交比例");
            listHead1.Add("完工超收比例");
            listHead1.Add("完工欠收比例");
            listHead1.Add("领料超收比例");
            listHead1.Add("领料欠收比例");
            listHead1.Add("计价方法_FName");
            listHead1.Add("计划单价");
            listHead1.Add("单价精度");
            listHead1.Add("存货科目代码_FNumber");
            listHead1.Add("销售收入科目代码_FNumber");
            listHead1.Add("销售成本科目代码_FNumber");
            listHead1.Add("成本差异科目代码_FNumber");
            listHead1.Add("代管物资科目_FNumber");
            listHead1.Add("税目代码_FName");
            listHead1.Add("税率");
            listHead1.Add("成本项目_FName");
            listHead1.Add("成本项目_FNumber");
            listHead1.Add("是否进行序列号管理");
            listHead1.Add("参与结转式成本还原");
            listHead1.Add("备注");
            listHead1.Add("网店货品名");
            listHead1.Add("商家编码");
            listHead1.Add("严格进行二维码数量校验");
            listHead1.Add("单位包装数量");
            listHead1.Add("计划策略_FName");
            listHead1.Add("计划模式_FName");
            listHead1.Add("订货策略_FName");
            listHead1.Add("固定提前期");
            listHead1.Add("变动提前期");
            listHead1.Add("累计提前期");
            listHead1.Add("订货间隔期天");
            listHead1.Add("最小订货量");
            listHead1.Add("最大订货量");
            listHead1.Add("批量增量");
            listHead1.Add("设置为固定再订货点");
            listHead1.Add("再订货点");
            listHead1.Add("固定经济批量");
            listHead1.Add("变动提前期批量");
            listHead1.Add("批量拆分间隔天数");
            listHead1.Add("拆分批量");
            listHead1.Add("需求时界天");
            listHead1.Add("计划时界天");
            listHead1.Add("默认工艺路线_FInterID");
            listHead1.Add("默认工艺路线_FRoutingName");
            listHead1.Add("默认生产类型_FName");
            listHead1.Add("默认生产类型_FNumber");
            listHead1.Add("生产负责人_FName");
            listHead1.Add("生产负责人_FNumber");
            listHead1.Add("计划员_FName");
            listHead1.Add("计划员_FNumber");
            listHead1.Add("是否倒冲");
            listHead1.Add("倒冲仓库_FName");
            listHead1.Add("倒冲仓库_FNumber");
            listHead1.Add("倒冲仓位_FName");
            listHead1.Add("倒冲仓位_FGroupName");
            listHead1.Add("投料自动取整");
            listHead1.Add("日消耗量");
            listHead1.Add("MRP计算是否合并需求");
            listHead1.Add("MRP计算是否产生采购申请");
            listHead1.Add("控制类型_FName");
            listHead1.Add("控制策略_FName");
            listHead1.Add("容器名称");
            listHead1.Add("看板容量");
            listHead1.Add("辅助属性参与计划运算");
            listHead1.Add("产品设计员_FName");
            listHead1.Add("产品设计员_FNumber");
            listHead1.Add("图号");
            listHead1.Add("是否关键件");
            listHead1.Add("毛重");
            listHead1.Add("净重");
            listHead1.Add("重量单位_FName");
            listHead1.Add("重量单位_FGroupName");
            listHead1.Add("长度");
            listHead1.Add("宽度");
            listHead1.Add("高度");
            listHead1.Add("体积");
            listHead1.Add("长度单位_FName");
            listHead1.Add("长度单位_FGroupName");
            listHead1.Add("版本号");
            listHead1.Add("单位标准成本");
            listHead1.Add("附加费率");
            listHead1.Add("附加费所属成本项目_FNumber");
            listHead1.Add("成本BOM_FBOMNumber");
            listHead1.Add("成本工艺路线_FInterID");
            listHead1.Add("成本工艺路线_FRoutingName");
            listHead1.Add("标准加工批量");
            listHead1.Add("单位标准工时小时");
            listHead1.Add("标准工资率");
            listHead1.Add("变动制造费用分配率");
            listHead1.Add("单位标准固定制造费用金额");
            listHead1.Add("单位委外加工费");
            listHead1.Add("委外加工费所属成本项目_FNumber");
            listHead1.Add("单位计件工资");
            listHead1.Add("采购订单差异科目代码_FNumber");
            listHead1.Add("采购发票差异科目代码_FNumber");
            listHead1.Add("材料成本差异科目代码_FNumber");
            listHead1.Add("加工费差异科目代码_FNumber");
            listHead1.Add("废品损失科目代码_FNumber");
            listHead1.Add("标准成本调整差异科目代码_FNumber");
            listHead1.Add("采购检验方式_FName");
            listHead1.Add("产品检验方式_FName");
            listHead1.Add("委外加工检验方式_FName");
            listHead1.Add("发货检验方式_FName");
            listHead1.Add("退货检验方式_FName");
            listHead1.Add("库存检验方式_FName");
            listHead1.Add("其他检验方式_FName");
            listHead1.Add("抽样标准致命_FName");
            listHead1.Add("抽样标准致命_FNumber");
            listHead1.Add("抽样标准严重_FName");
            listHead1.Add("抽样标准严重_FNumber");
            listHead1.Add("抽样标准轻微_FName");
            listHead1.Add("抽样标准轻微_FNumber");
            listHead1.Add("库存检验周期天");
            listHead1.Add("库存周期检验预警提前期天");
            listHead1.Add("检验方案_FInterID");
            listHead1.Add("检验方案_FBrNo");
            listHead1.Add("检验员_FName");
            listHead1.Add("检验员_FNumber");
            listHead1.Add("英文名称");
            listHead1.Add("英文规格");
            listHead1.Add("HS编码_FHSCode");
            listHead1.Add("HS编码_FNumber");
            listHead1.Add("外销税率");
            listHead1.Add("HS第一法定单位");
            listHead1.Add("HS第二法定单位");
            listHead1.Add("进口关税率");
            listHead1.Add("进口消费税率");
            listHead1.Add("HS第一法定单位换算率");
            listHead1.Add("HS第二法定单位换算率");
            listHead1.Add("是否保税监管");
            listHead1.Add("物料监管类型_FName");
            listHead1.Add("物料监管类型_FNumber");
            listHead1.Add("长度精度");
            listHead1.Add("体积精度");
            listHead1.Add("重量精度");
            listHead1.Add("启用服务");
            listHead1.Add("生成产品档案");
            listHead1.Add("维修件");
            listHead1.Add("保修期限月");
            listHead1.Add("使用寿命月");
            listHead1.Add("控制");
            listHead1.Add("是否禁用");
            listHead1.Add("全球唯一标识内码");

            #endregion

            for (int i = 0; i < listHead.Count; i++)
            {
                cells[0, i].PutValue(listHead[i]);
                cells[0, i].Style = style;
                cells.SetColumnWidth(i, 30);
            }

            List<PartViewForExport> list = new List<PartViewForExport>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EFDbContext"].ToString());

            List<PRContent> prcs = _prContentRepository.PRContents.Where(c => c.PurchaseRequestID == int_PurchaseRequestID).Where(c => c.Enabled == true).ToList() ?? new List<PRContent>();
            int z = 1;
            bool IsOver = false;
            try
            {
                foreach (var prc in prcs)
                {
                    if (z == prcs.Count)
                        IsOver = true;
                    z++;
                    #region 执行存储过程 
                    string sql = "execute Pro_RefreshERPDataByPR " + prc.PRContentID + ",'" + IsOver.ToString() + "'";
                    SqlCommand comm = new SqlCommand(sql, conn);
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "test");
                    #endregion
                    if (ds == null && ds.Tables.Count == 0)
                        return "false";
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            PartViewForExport pvfe = new PartViewForExport();
                            Type partInfo = typeof(PartViewForExport);
                            PropertyInfo[] pis = partInfo.GetProperties();
                            foreach (PropertyInfo pi in pis)
                            {
                                if (ds.Tables[0].Columns.Contains(pi.Name))
                                {
                                    pi.SetValue(pvfe, dr[pi.Name].ToString());
                                }
                            }
                            list.Add(pvfe);
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                string WebLogPath = Server.MapPath("~/Log/");
                if (!Directory.Exists(WebLogPath))
                {
                    DirectoryInfo dir = new DirectoryInfo(WebLogPath);
                    dir.Create();
                }
                string logPath = WebLogPath + "采购单号_"+ PurchaseRequestID.ToString() + "_" + DateTime.Now.ToString("yyMMddhhmm") + ".txt";
                Toolkits.WriteLog(logPath, ex.Message.ToString());
            }
            //过滤重复行 michael
            List<PartViewForExport> list1 = new List<PartViewForExport>();
            foreach (var pv in list)
            {
                int rowCount = (from PartViewForExport q in list1 where q.代码.ToString() == pv.代码.ToString() where pv.代码.ToString() != "未查询到物料分类，请至ERP创建" select q).ToList().Count();//where pv.代码.ToString() != "未查询到物料分类，请至ERP创建"
                if (rowCount == 0)
                {
                    list1.Add(pv);
                }
            }
            if (list1.Count > 0)
            {
                for (int i = 1; i <= list1.Count; i++)
                {
                    PartViewForExport pv = list1[i - 1];

                    for (int j = 1; j <= listHead1.Count; j++)
                    {
                        string name = listHead1[j - 1];
                        PropertyInfo p = typeof(PartViewForExport).GetProperty(name);
                        string value = p.GetValue(pv, null) == null ? "" : p.GetValue(pv, null).ToString(); ;
                        cells[i, j - 1].PutValue(value);
                        cells.SetColumnWidth(i, 30);
                    }
                }
                string prNum = _purchaseRequestRepository.GetByID(Convert.ToInt32(PurchaseRequestID)).PurchaseRequestNumber??"";
                workbook.Save(prNum + "_物料" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx", FileFormatType.Excel2007Xlsx,SaveType.OpenInExcel, System.Web.HttpContext.Current.Response);
            }
            return "ok";
        }
        #endregion
        /// <summary>
        /// 过滤dt重复行
        /// </summary>
        /// <param name="Stable"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public DataTable FilterRepeatTable(DataTable Stable,string colName)
        {
            DataTable _table = Stable.Clone();
            foreach (DataRow row in Stable.Rows)
            {
                int rowCount = (from DataRow q in _table.Rows where q[colName].ToString() == row[colName].ToString() where row[colName].ToString()!= "未查询到物料分类，请至ERP创建" select q).ToList().Count();
                if (rowCount == 0)
                {
                    DataRow _row = _table.NewRow();
                    _row.ItemArray = row.ItemArray;
                    _table.Rows.Add(_row);
                }
            }
            return _table;
        }
        /// <summary>
        /// 导出采购信息
        /// </summary>
        /// <returns></returns>
        public string ExportExcelForPurchase(string prNO)
        {

            //return ExportExcel<PartViewForExport>(ps);

            Workbook workbook = new Workbook();
            workbook.Open(AppDomain.CurrentDomain.BaseDirectory + "\\images\\purchase.xls");
            //workbook.Worksheets.Clear();
            //workbook.Worksheets.Add("Page1");
            //workbook.Worksheets.Add("Page2");

            Worksheet worksheet = workbook.Worksheets[1];
            Cells cells = worksheet.Cells;
            cells.InsertRow(0);
            Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];//新增样式
            style.HorizontalAlignment = TextAlignmentType.Center;//文字居中  
            style.Font.Size = 11;//文字大小  
            style.Font.IsBold = true;//粗体 
            cells.SetRowHeight(0, 20);              //设置行高 

            Worksheet worksheet2 = workbook.Worksheets[0];
            Cells cells2 = worksheet2.Cells;
            cells2.InsertRow(0);
            Aspose.Cells.Style style2 = workbook.Styles[workbook.Styles.Add()];//新增样式
            style2.HorizontalAlignment = TextAlignmentType.Center;//文字居中  
            style2.Font.Size = 11;//文字大小  
            style2.Font.IsBold = true;//粗体 
            cells2.SetRowHeight(0, 20);              //设置行高 

            List<string> listHead = new List<string>();
            #region MyRegion
            listHead.Add("行号");
            listHead.Add("单据号_FBillno");
            listHead.Add("单据号_FTrantype");
            listHead.Add("物料代码_FNumber");
            listHead.Add("物料代码_FName");
            listHead.Add("物料代码_FModel");
            listHead.Add("辅助属性_FNumber");
            listHead.Add("辅助属性_FName");
            listHead.Add("辅助属性_FClassName");
            listHead.Add("单位_FNumber");
            listHead.Add("单位_FName");
            listHead.Add("数量");
            listHead.Add("建议采购日期");
            listHead.Add("BOM编号");
            listHead.Add("用途");
            listHead.Add("基本单位数量");
            listHead.Add("供应商_FNumber");
            listHead.Add("供应商_FName");
            listHead.Add("到货日期");
            listHead.Add("计划订单号");
            listHead.Add("换算率");
            listHead.Add("辅助数量");
            listHead.Add("源单单号");
            listHead.Add("源单类型");
            listHead.Add("源单内码");
            listHead.Add("源单分录");
            listHead.Add("MRP计算标记");
            listHead.Add("计划模式_FID");
            listHead.Add("计划模式_FName");
            listHead.Add("计划模式_FTypeID");
            listHead.Add("计划跟踪号");
            listHead.Add("是否询价");
            listHead.Add("BOM类別_FID");
            listHead.Add("BOM类別_FName");
            listHead.Add("BOM类別_FTypeID");
            listHead.Add("模具号");
            listHead.Add("零件号");
            listHead.Add("订单BOM行号");
            listHead.Add("规格");
            listHead.Add("备注1");
            listHead.Add("件数");
            listHead.Add("到货日期2");


            #endregion
            for (int i = 0; i < listHead.Count; i++)
            {
                cells[0, i].PutValue(listHead[i]);
                cells[0, i].Style = style;
                cells.SetColumnWidth(i, 30);
            }

            List<string> listHead2 = new List<string>();
            #region MyRegion
            listHead2.Add("FCheckDate");
            listHead2.Add("日期");
            listHead2.Add("制单人_FName");
            listHead2.Add("编号");
            listHead2.Add("审核人_FName");
            listHead2.Add("FBrID_FNumber");
            listHead2.Add("FBrID_FName");
            listHead2.Add("事务类型");
            listHead2.Add("单据号");
            listHead2.Add("使用部门_FNumber");
            listHead2.Add("使用部门_FName");
            listHead2.Add("业务类型_FID");
            listHead2.Add("业务类型_FName");
            listHead2.Add("业务类型_FTypeID");
            listHead2.Add("备注");
            listHead2.Add("申请人_FNumber");
            listHead2.Add("申请人_FName");
            listHead2.Add("审批日期");
            listHead2.Add("源单类型");
            listHead2.Add("打印次数");
            listHead2.Add("计划确认");
            listHead2.Add("计划类别_FNumber");
            listHead2.Add("计划类别_FName");

            #endregion
            for (int i = 0; i < listHead2.Count; i++)
            {
                cells2[0, i].PutValue(listHead2[i]);
                cells2[0, i].Style = style;
                cells2.SetColumnWidth(i, 30);
            }

            List<PurchaseViewForExport> list = new List<PurchaseViewForExport>();
            List<Purchase2ViewForExport> list2 = new List<Purchase2ViewForExport>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EFDbContext"].ToString());

            if (prNO.Contains(","))
            {
                string[] strArr = prNO.Split(',');
                #region MyRegion
                foreach (string str in strArr)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        //PurchaseRequest pr = _purchaseRequestRepository.GetByID(Convert.ToInt32(str)) ?? new PurchaseRequest();
                        List<PRContent> prcs = _prContentRepository.QueryByRequestID(Convert.ToInt32(str)).ToList();
                        foreach (var prc in prcs)
                        {
                            PurchaseRequest pr = _purchaseRequestRepository.GetByID(prc.PurchaseRequestID) ?? new PurchaseRequest();
                            if (string.IsNullOrEmpty(prc.ERPPartID))
                                return "NG|" + pr.PurchaseRequestNumber.ToString() + "|" + prc.JobNo;
                        }
                    }                       
                }
                foreach (string str in strArr)
                {
                    if (!string.IsNullOrEmpty(str))
                    {

                        #region MyRegion 
                        string sql = "execute Pro_RefreshPR '" + str + "'";
                        SqlCommand comm = new SqlCommand(sql, conn);
                        SqlDataAdapter da = new SqlDataAdapter(comm);
                        DataSet ds = new DataSet();
                        da.Fill(ds, "test");
                        #endregion


                        if (ds == null && ds.Tables.Count == 0)
                            return "false";

                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            PurchaseViewForExport pvfe = new PurchaseViewForExport();
                            Type partInfo = typeof(PurchaseViewForExport);
                            PropertyInfo[] pis = partInfo.GetProperties();
                            foreach (PropertyInfo pi in pis)
                            {
                                if (ds.Tables[1].Columns.Contains(pi.Name))
                                {
                                    pi.SetValue(pvfe, dr[pi.Name].ToString());
                                }
                            }
                            list.Add(pvfe);
                        }
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Purchase2ViewForExport pvfe2 = new Purchase2ViewForExport();
                            Type partInfo = typeof(Purchase2ViewForExport);
                            PropertyInfo[] pis = partInfo.GetProperties();
                            foreach (PropertyInfo pi in pis)
                            {
                                if (ds.Tables[0].Columns.Contains(pi.Name))
                                {
                                    pi.SetValue(pvfe2, dr[pi.Name].ToString());
                                }
                            }
                            list2.Add(pvfe2);
                        }
                        for (int i = 1; i <= list.Count; i++)
                        {
                            PurchaseViewForExport pv = list[i - 1];

                            for (int j = 1; j <= listHead.Count; j++)
                            {
                                string name = listHead[j - 1];
                                PropertyInfo p = typeof(PurchaseViewForExport).GetProperty(name);
                                string value = p.GetValue(pv, null) == null ? "" : p.GetValue(pv, null).ToString();
                                cells[i, j - 1].PutValue(value);
                                cells.SetColumnWidth(i, 30);
                            }
                        }

                        for (int i = 1; i <= list2.Count; i++)
                        {
                            Purchase2ViewForExport pv = list2[i - 1];

                            for (int j = 1; j <= listHead2.Count; j++)
                            {
                                string name = listHead2[j - 1];
                                PropertyInfo p = typeof(Purchase2ViewForExport).GetProperty(name);
                                string value = p.GetValue(pv, null) == null ? "" : p.GetValue(pv, null).ToString();
                                cells2[i, j - 1].PutValue(value);
                                cells2.SetColumnWidth(i, 30);
                            }
                        }
                    }
                }

                #endregion
                workbook.Save("采购申请" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls", FileFormatType.Excel2003,
                    SaveType.OpenInExcel, System.Web.HttpContext.Current.Response);

            }
            return "ok";
        }
        /// <summary>
        /// 判断是否全部同步erp料号
        /// </summary>
        /// <returns></returns>
        public string IsErpPasrts(string prID)
        {
            List<PRContent> prcs = _prContentRepository.QueryByRequestID(Convert.ToInt32(prID)).ToList() ?? new List<PRContent>();
            foreach (var prc in prcs)
            {
                PurchaseRequest pr = _purchaseRequestRepository.GetByID(prc.PurchaseRequestID) ?? new PurchaseRequest();
                if (string.IsNullOrEmpty(prc.ERPPartID))
                    return "NG|" + pr.PurchaseRequestNumber.ToString() + "|" + prc.JobNo;
            }
            return "ok";
        }
        /// <summary>
        /// 检索供应商
        /// </summary>
        /// <param name="js"></param>
        /// <returns></returns>
        public string JsonSuppliersByJS(string js)
        {
            List<Supplier> _suppliers = _supplierRepository.GetSuppliersByJS(js)??new List<Supplier>();
            string _nameStrs = "";
            if (_suppliers.Count > 0)
            {
                foreach(var s in _suppliers)
                {
                    _nameStrs = _nameStrs + s.Name+"-"+s.JianSuo + ",";
                }
            }
            if(!string.IsNullOrEmpty(_nameStrs))
                _nameStrs = _nameStrs.Substring(0, _nameStrs.Length - 1);
            else
                _nameStrs = "";
          return _nameStrs;
        }
        public int GetSupplierID(string _sName)
        {
            Supplier _supplier = _supplierRepository.Suppliers.Where(s => s.Name == _sName).FirstOrDefault();
            if(_supplier!=null)
                return _supplier.SupplierID;
            return 0;
        }
    }
}