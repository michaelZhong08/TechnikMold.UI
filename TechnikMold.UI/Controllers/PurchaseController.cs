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

namespace MoldManager.WebUI.Controllers
{
    public class PurchaseController : Controller
    {
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
        private IQuotationFileRepository _quotationFileRepository;


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
            IQuotationFileRepository QuotationFileRepository)
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
            _quotationFileRepository = QuotationFileRepository;
            _status = new PurchaseRequestStatus();
        }


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
        public ActionResult PRDetail(int PurchaseRequestID = 0,
            string MoldNumber = "",
            string PartIDs = "",
            string TaskIDs = "",
            string WarehouseStockIDs = "",
            int TaskType = 0)
        {
            ViewBag.TaskType = TaskType;
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
                if (PartIDs != "")
                {

                    ViewBag.PartIDs = PartIDs;
                    ViewBag.TaskIDs = "";
                    ViewBag.WarehouseStockIDs = "";
                }
                else if (TaskIDs != "")
                {
                    ViewBag.PartIDs = "";
                    ViewBag.TaskIDs = TaskIDs;
                    ViewBag.WarehouseStockIDs = "";
                }


                return View();
            }


            else
            {
                ViewBag.Title = "新建申请单";
                ViewBag.ProjectID = 0;
                ViewBag.PartIDs = "";
                ViewBag.PurchaseRequestID = 0;
                ViewBag.PRState = 0;
                ViewBag.TaskIDs = "";
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

        }




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
        public int PRSave(List<PRContent> PRContents, int PurchaseType, int PurchaseRequestID = 0, int SupplierID = 0, string Memo = "")
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
                _requestID = _purchaseRequestRepository.Save(_request);
            }
            else
            {


                _requestID = PurchaseRequestID;
                _request = _purchaseRequestRepository.GetByID(PurchaseRequestID);


                _request.Memo = Memo;
                _request.SupplierID = SupplierID;


                _request.DueDate = _requireDate;
                _request.PurchaseType = PurchaseType;

                _purchaseRequestRepository.Save(_request);

            }

            //string MoldNumber = _projectRepository.GetByID(ProjectID).MoldNumber;
            //Create PR Contents
            foreach (PRContent _content in PRContents)
            {
                _content.PurchaseRequestID = _requestID;

                _content.RequireTime = _content.RequireTime == new DateTime(1, 1, 1) ? new DateTime(1900, 1, 1) : _content.RequireTime;

                PurchaseItem _item = new PurchaseItem(_content);

                _item.PurchaseType = _content.PurchaseTypeID;

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
                    _taskRepository.OutSource(_content.TaskID);
                }
                if (_content.PartID > 0)
                {
                    Part _part = _partRepository.QueryByID(_content.PartID);
                    _part.InPurchase = true;
                    _partRepository.Save(_part);
                }

                if (_content.SupplierName == null)
                {
                    _content.SupplierName = "";
                }
                _prContentRepository.Save(_content);
            }

            //Create PR operation record
            if (PurchaseRequestID == 0)
            {
                string _msg = "创建申请单" + _request.PurchaseRequestNumber;
                PRRecord(_requestID, _msg);
            }

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
                if ((Department > 0) && (_dept != 4))
                {
                    _prList = _prList.Where(p => p.DepartmentID == Department);
                }

            }
            else
            {
                Expression<Func<PurchaseItem, bool>> _exp1 = i => i.Quantity > 0;
                Expression<Func<PurchaseItem, bool>> _exp2 = null;

                if (MoldNumber != "")
                {
                    _exp1 = PredicateBuilder.And(_exp1, i => i.MoldNumber.Contains(MoldNumber));
                }

                if (StartDate != "")
                {
                    try
                    {
                        DateTime _start = Convert.ToDateTime(StartDate);
                        _exp1 = PredicateBuilder.And(_exp1, i => i.CreateTime > _start);
                    }
                    catch
                    {

                    }
                }

                if (FinishDate != "")
                {
                    try
                    {
                        FinishDate = FinishDate + " 23:59";
                        DateTime _end = Convert.ToDateTime(FinishDate);
                        _exp1 = PredicateBuilder.And(_exp1, i => i.CreateTime < _end);
                    }
                    catch
                    {

                    }
                }

                if (PRKeyword != "")
                {
                    _exp2 = i => i.PartNumber.Contains(PRKeyword);
                    _exp1 = PredicateBuilder.Or(_exp2, i => i.Name.Contains(PRKeyword));

                }

                if (Supplier > 0)
                {
                    _exp1 = PredicateBuilder.And(_exp1, i => i.SupplierID == Supplier);
                }


                List<int> _prIDs = _purchaseItemRepository.PurchaseItems.Where(_exp1)
                    .Select(i => i.PurchaseRequestID).Distinct().ToList();
                _prList = _purchaseRequestRepository.PurchaseRequests
                    .Where(p => (_prIDs.Contains(p.PurchaseRequestID)))
                    //.Where(p => p.State == State)
                    .Where(p => p.Enabled == true).OrderByDescending(p => p.CreateDate);
                if ((Department > 0) && (_dept != 4))
                {
                    _prList = _prList.Where(p => p.DepartmentID == Department);
                }
            }
            PRListGridViewModel _viewModel = new PRListGridViewModel(_prList,
                _userRepository,
                _status,
                _projectRepository,
                _purchaseTypeRepository,
                _departmentRepository);
            return Json(_viewModel, JsonRequestBehavior.AllowGet);
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
                List<Part> _partList = new List<Part>();
                for (int i = 0; i < _partID.Length; i++)
                {
                    _partList.Add(_partRepository.QueryByID(Convert.ToInt32(_partID[i])));
                }
                PurchaseContentGridViewModel _model = new PurchaseContentGridViewModel(_partList, _projectRepository);

                return Json(_model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsonPROutSource(string TaskIDs)
        {
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
                PurchaseContentGridViewModel _model = new PurchaseContentGridViewModel(_taskList, _projectPhaseRepository, _steelDrawingRepository);


                return Json(_model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult JsonPRWarehouseStock(string WarehouseStockIDs)
        {
            if (WarehouseStockIDs != "")
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

        public JsonResult JsonPRContent(int PRContentID)
        {
            PRContent _item = _prContentRepository.QueryByID(PRContentID);
            return Json(_item, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonPRContents(string Keyword)
        {
            IEnumerable<PRContent> _prcontents = _prContentRepository.QueryByName(Keyword);
            return Json(_prcontents, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JsonPRDetail(int PRID)
        {
            List<PRContent> _contents = _prContentRepository.QueryByRequestID(PRID).ToList();
            PurchaseContentGridViewModel _model = new PurchaseContentGridViewModel(_contents, _purchaseItemRepository, _costCenterRepository, _purchaseTypeRepository);
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

            //Michael 20180507
            QuotationRequest _qr = _quotationRequestRepository.GetByID(QuotationRequestID);
            List<PurchaseItem> PiList = _purchaseItemRepository.PurchaseItems.Where(p => p.QuotationRequestID == QuotationRequestID).ToList();

            if (_qr.State == (int)QuotationRequestStatus.新建)
            {
                try
                {
                    _quotationRequestRepository.ChangeStatus(QuotationRequestID, (int)QuotationRequestStatus.发出);
                    foreach (PurchaseItem pi in PiList)
                    {
                        pi.State = (int)PurchaseItemStatus.询价中;
                        _purchaseItemRepository.Save(pi);
                    }
                }
                catch { }
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
                _id = Convert.ToInt16(_ids[i]);
                PRContent _content = _prContentRepository.QueryByID(_id);
                PurchaseItem _item = _purchaseItemRepository.QueryByID(_content.PurchaseItemID);
                if (_item.State < (int)PurchaseItemStatus.待收货)
                {
                    _prContentRepository.Delete(_id);
                    _purchaseItemRepository.ChangeState(_content.PurchaseItemID, (int)PurchaseItemStatus.取消);

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
                int[] _purchaseItemIDs = Array.ConvertAll<string, int>(PurchaseItemIDs.Split(','), delegate(string s) { return int.Parse(s); });
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
                _poList = _purchaseOrderRepository.PurchaseOrders.Where(p=>p.UserID!=0);

                if (State != 0)
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
                _purchaseRequestRepository, _purchaseItemRepository, _purchaseTypeRepository);
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
                    _types = _purchaseTypeRepository.PurchaseTypes.Where(p => p.ParentTypeID > 0).ToList();
                }

            }



            return Json(_types, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonPurchaseTypeLevel(int ParentID = 0)
        {
            List<PurchaseType> _types = _purchaseTypeRepository.QueryByParentID(ParentID).ToList();
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

        public ActionResult JsonPurchaseItemMoldNumber(string Keyword = "")
        {
            IEnumerable<string> _moldNumbers = _purchaseItemRepository.PurchaseItems
                .Where(i => i.MoldNumber != "")
                .Where(i => i.MoldNumber.Contains(Keyword))
                .Select(i => i.MoldNumber).Distinct();
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

            int[] _purchaseItemID = Array.ConvertAll<string, int>(PurchaseItemIDs.Split(','), delegate(string s) { return int.Parse(s); });


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
            return _costCenterRepository.CostCenters.Where(c => c.Name == Name).Count();
        }

        [HttpPost]
        public int SaveCostCenter(CostCenter CostCenter)
        {
            CostCenter.Enabled = true;
            return _costCenterRepository.Save(CostCenter);
        }

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


        #region PO report for finacial dept

        public ActionResult POReport()
        {
            return View();
        }


        public ActionResult JsonPOReport(string StartDate, string EndDate = "")
        {
            DateTime _startDate = Convert.ToDateTime(StartDate + " 00:00");
            DateTime _endDate;
            if (EndDate == "")
            {
                EndDate = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59";
            }
            _endDate = Convert.ToDateTime(EndDate);
            List<PurchaseItem> _items = _purchaseItemRepository.PurchaseItems.Where(p => p.DeliveryTime > _startDate)
                .Where(p => p.DeliveryTime <= _endDate)//.Where(p => p.State == (int)PurchaseItemStatus.完成)
                .ToList();
            PurchaseOrderReportGridViewModel _viewModel = new PurchaseOrderReportGridViewModel(_items,
                _purchaseTypeRepository, _supplierRepository, _purchaseOrderRepository, _costCenterRepository);
            return Json(_viewModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public string UpdatePurchaseType(string POContentIDs, int PurchaseTypeID)
        {
            string[] _ids = POContentIDs.Split(',');
            try
            {
                for (int i = 0; i < _ids.Length; i++)
                {
                    int _itemid = _poContentRepository.QueryByID(Convert.ToInt32(_ids[i])).PurchaseItemID;
                    PurchaseItem _item = _purchaseItemRepository.QueryByID(_itemid);
                    _item.PurchaseType = PurchaseTypeID;
                    _purchaseItemRepository.Save(_item);
                }
                return "采购类型更新成功";
            }
            catch
            {
                return "保存失败,请刷新后重试";
            }

        }

        #region QuotationFile Upload

        [HttpPost]
        public string QuotationUpload(int RequestID, int UploadSupplier, HttpPostedFileBase File)
        {
            List<QuotationFile> _exist = _quotationFileRepository.QueryByQuotationRequest(RequestID, UploadSupplier);
            foreach (QuotationFile _file in _exist)
            {
                _file.Enabled = false;
                _quotationFileRepository.Save(_file);
            }

            try
            {
                string _path = Server.MapPath("~/Quotation/");
                QuotationFile _file = new QuotationFile(RequestID, UploadSupplier, File.FileName, File.ContentType);
                _quotationFileRepository.Save(_file);
                File.SaveAs(_path + _file.SysFileName);
                return "";
            }
            catch
            {
                return "error";
            }
        }


        public ActionResult JsonQuotationFiles(int QuotationRequestID)
        {
            List<QuotationFile> _files = _quotationFileRepository.QueryByQuotationRequest(QuotationRequestID);
            List<QuotationFileViewModel> _models = new List<QuotationFileViewModel>();
            foreach(QuotationFile _file in _files){
                string _supplierName = _supplierRepository.QueryByID(_file.SupplierID).Name;
                QuotationFileViewModel _model = new QuotationFileViewModel(_file, _supplierName);
                _models.Add(_model);
            }
            return Json(_models, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetQuotationFile(int QuotationFileID)
        {
            QuotationFile _file = _quotationFileRepository.QueryByID(QuotationFileID);
            string _filepath = Server.MapPath("~/Quotation/"+_file.SysFileName);
            return File(_filepath, _file.MimeType, _file.FileName);
        }

        public int QuotationFileCount(int QuotationRequestID, int SupplierID)
        {
            int _count = _quotationFileRepository.QueryByQuotationRequest(QuotationRequestID, SupplierID).Count();
            return _count;
        }
        #endregion
    }
}