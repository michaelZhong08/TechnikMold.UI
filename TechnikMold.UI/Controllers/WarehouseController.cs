using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridViewModel;
using MoldManager.WebUI.Models.Helpers;
using TechnikSys.MoldManager.Domain.Status;
using System.Linq.Expressions;
using MoldManager.WebUI.Tools;
using MoldManager.WebUI.Models.EditModel;

namespace MoldManager.WebUI.Controllers
{
    public class WarehouseController : Controller
    {
        private IWarehouseRepository _warehouseRepository;
        private IWarehouseStockRepository _warehouseStockRepository;
        private IWarehouseRecordRepository _warehouseRecordRepository;
        private IPurchaseOrderRepository _poRepository;
        private IPOContentRepository _poContentRepository;
        private IPurchaseRequestRepository _prRepository;
        private IPRContentRepository _prContentRepository;
        private IUserRepository _userRepository;
        private ISequenceRepository _sequenceRepository;
        private IWarehouseRequestRepository _warehouseRequestRepository;
        private IWarehouseRequestItemRepository _warehouseRequestItemRepository;
        private IOutStockFormRepository _outStockFormRepository;
        private IOutStockItemRepository _outStockItemRepository;
        private IPurchaseItemRepository _purchaseItemRepository;
        private IPurchaseTypeRepository _purchaseTypeRepository;
        private IStockTypeRepository _stockTypeRepository;
        private IWarehousePositionRepository _warehousePositionRepository;
        private IReturnItemRepository _returnItemRepository;
        private IReturnRequestRepository _returnRequestRepository;
        private IProjectRepository _projectRepository;
        private IProjectPhaseRepository _projectPhaseRepository;
        private ISupplierRepository _supplierRepository;
        private IWHPartRepository _whPartRepository;
        private IWHStockRepository _whStockRepository;

        public WarehouseController(IWarehouseRepository WarehouseRepository,
            IWarehouseStockRepository WarehouseStockRepository,
            IPurchaseOrderRepository PurchaseOrderRepository,
            IPOContentRepository POContentRepository,
            IPurchaseRequestRepository PurchaseRequestRepository,
            IPRContentRepository PRContentRepository,
            IUserRepository UserRepository, 
            IWarehouseRecordRepository WarehouseRecordRepository, 
            ISequenceRepository SequenceRepository, 
            IWarehouseRequestRepository WarehouseRequestRepository,
            IWarehouseRequestItemRepository WarehouseRequestItemRepository, 
            IOutStockFormRepository OutStockFormRepository, 
            IOutStockItemRepository OutStockItemRepository, 
            IPurchaseItemRepository PurchaseItemRepository, 
            IPurchaseTypeRepository PurchaseTypeRepository, 
            IStockTypeRepository StockTypeRepository, 
            IWarehousePositionRepository WarehousePositionRepository, 
            IReturnRequestRepository ReturnRequestRepository, 
            IReturnItemRepository ReturnItemRepository,
            IProjectRepository ProjectRepository,
            IProjectPhaseRepository ProjectPhaseRepository,
            ISupplierRepository SupplierRepository,
            IWHPartRepository WHPartRepository,
            IWHStockRepository WHStockRepository)
        {
            _warehouseRepository = WarehouseRepository;
            _warehouseStockRepository = WarehouseStockRepository;
            _poRepository = PurchaseOrderRepository;
            _poContentRepository = POContentRepository;
            _prRepository = PurchaseRequestRepository;
            _prContentRepository = PRContentRepository;
            _userRepository = UserRepository;
            _warehouseRecordRepository = WarehouseRecordRepository;
            _sequenceRepository = SequenceRepository;
            _warehouseRequestRepository = WarehouseRequestRepository;
            _warehouseRequestItemRepository = WarehouseRequestItemRepository;
            _outStockFormRepository = OutStockFormRepository;
            _outStockItemRepository = OutStockItemRepository;
            _purchaseItemRepository = PurchaseItemRepository;
            _purchaseTypeRepository = PurchaseTypeRepository;
            _stockTypeRepository = StockTypeRepository;
            _warehousePositionRepository = WarehousePositionRepository;
            _returnRequestRepository = ReturnRequestRepository;
            _returnItemRepository = ReturnItemRepository;
            _projectRepository = ProjectRepository;
            _projectPhaseRepository = ProjectPhaseRepository;
            _supplierRepository = SupplierRepository;
            _whPartRepository = WHPartRepository;
            _whStockRepository = WHStockRepository;
        }

        // GET: Warehouse content list
        // Data:JsonWarehouseStock
        public ActionResult Index(string Keyword = "",string MoldNumber="", int PurchaseType=0,string Parent= "")
        {
            ViewBag.Keyword = Keyword;
            ViewBag.MoldNumber = MoldNumber;
            ViewBag.PurchaseType = PurchaseType;
            ViewBag.Parent = Parent;

            switch (PurchaseType)
            {
                case 1:
                    ViewBag.Title = "在库零件信息";
                    break;
                case 6:
                    ViewBag.Title = "在库材料查询";
                    break;
                case 0:
                    ViewBag.Title = "库存信息";
                    break;
                case 2:
                    if(Parent == "生产耗材")
                    {
                        ViewBag.Title = "生产耗材查询";
                    }
                    if(Parent == "备库查询")
                    {
                        ViewBag.Title = "备库查询";
                    }
                    break;
                //case 2:
                //    ViewBag.Title = "备库信息";
                //    break;
                default:
                    ViewBag.Title = "库存信息";
                    break;
                
            }
            switch (Parent)
            {
                case "模具耗材备库":
                    ViewBag.Title = "备库信息";
                    break;
                case "生产耗材":
                    ViewBag.Title = "在库生产耗材信息";
                    break;
            }

            return View();
        }

        public ActionResult WHPOList(int ProjectID = 0, int State =1)
        {
            ViewBag.ProjectID = ProjectID;
            ViewBag.State = State;
            return View();
        }

        public ActionResult POContentList(int PurchaseOrderID, int ReturnRequestID=0)
        {
            PurchaseOrder _order = _poRepository.QueryByID(PurchaseOrderID);
            ViewBag.PONumber = _poRepository.QueryByID(PurchaseOrderID).PurchaseOrderNumber;
            ViewBag.State = _order.State;
            ViewBag.PurchaseOrderID = PurchaseOrderID;
            ViewBag.ReturnRequestID = ReturnRequestID;
            return View();
        }


        public ActionResult ClosePurchaseOrder(int PurchaseOrderID)
        {
            try
            {
                //int _userID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
                //_poRepository.ClosePurchaseOrder(PurchaseOrderID);
                //int _requestID = _poRepository.QueryByID(PurchaseOrderID).PurchaseRequestID;
                //_prRepository.Submit(_requestID, (int)PurchaseRequestStatus.完成, "所有零件已入库，申请单返回", _userID);
                return RedirectToAction("WHPOList", "Warehouse");
            }
            catch
            {
                return RedirectToAction("WHPOList","Warehouse");
            }          
       }


        public string StockChange(int WarehouseStockID, int Quantity)
        {
            WHStock _stock = _whStockRepository.QueryByID(WarehouseStockID);
            if (_stock.Qty + Quantity >=0)
            {
                _warehouseStockRepository.UpdateQuantity(WarehouseStockID, Quantity);
                if (Quantity > 0)
                {
                    return "入库完成";
                }
                else
                {
                    return "出库完成";
                }
                
            }
            else
            {
                return "出库数量不能大于库存数量";
            }
        }

        //public string NewPartInstock(string Name, string Specification, int Quantity)
        //{
        //    WarehouseStock _stock = new WarehouseStock();
            
        //    _stock.Name = Name;
        //    _stock.Specification = Specification;
        //    _stock.Quantity = Quantity;
        //    try
        //    {
        //        int _id = _warehouseStockRepository.Save(_stock);
        //        _warehouseStockRepository.UpdateQuantity(_id, Quantity);
        //        return "";
        //    }
        //    catch
        //    {
        //        return Name + "入库失败,请重试";
        //    }
        //}

        #region Json

        /// <summary>
        /// Provide data for purchase order list
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        public ActionResult JsonPurchaseOrder(int ProjectID = 0, 
            int State = 0, 
            string MoldNumber = "",
            string Keyword = "",
            string StartDate = "",
            string EndDate = "",
            int Supplier = 0,
            int PurchaseType = 0)
        {
            IEnumerable<PurchaseOrder> _orders;
            Expression<Func<PurchaseOrder, bool>> _exp;

            switch (State)
            {
                case 0:
                    int[] _statesAll = { (int)PurchaseOrderStatus.发布, (int)PurchaseOrderStatus.部分收货, (int)PurchaseOrderStatus.完成, (int)PurchaseOrderStatus.外发待出库 };
                    _exp = p => (_statesAll.Contains(p.State));
                    break;
                case 1:
                    int[] _statesUnFinish = { (int)PurchaseOrderStatus.发布, (int)PurchaseOrderStatus.部分收货, (int)PurchaseOrderStatus.外发待出库 };
                    _exp = p => (_statesUnFinish.Contains(p.State));
                    break;
                case 2:
                    _exp = p => p.State == (int)PurchaseOrderStatus.完成;
                    break;
                default:
                    _exp = p => p.State == State;
                    break;
            }


            List<int> _purchaseOrderIDs = QueryPurchaseOrderIDs(MoldNumber, Keyword, StartDate, EndDate, Supplier);

            _exp = PredicateBuilder.And(_exp, p=>(_purchaseOrderIDs.Contains(p.PurchaseOrderID)));

            if (ProjectID > 0)
            {
                _exp = PredicateBuilder.And(_exp, p=>p.ProjectID==ProjectID);
            }

            _orders = _poRepository.PurchaseOrders.Where(_exp);

            List<User> _users = new List<User>();
            foreach (PurchaseOrder _order in _orders)
            {
                int _userid = _order.UserID;
                try
                {
                    User _user = _userRepository.GetUserByID(_order.Responsible);

                    if (!_users.Contains(_user))
                    {
                        _users.Add(_user);
                    }
                }
                catch
                {
                    _users.Add(_userRepository.GetUserByID(_order.Responsible));
                }
            }


            _orders = _orders.OrderByDescending(r => r.ReleaseDate);
            WHPOListGridViewModel _viewModel = new WHPOListGridViewModel(_orders, _users, _supplierRepository);
            return Json(_viewModel, JsonRequestBehavior.AllowGet);
        }

        private List<int> QueryPurchaseOrderIDs(string MoldNumber = "",
            string Keyword = "",
            string StartDate = "",
            string EndDate = "",
            int Supplier = 0)
        {
            Expression<Func<PurchaseItem, bool>> _expItem = p => p.MoldNumber != "";
            IEnumerable<int> _purchaseOrderIDs;


            if (MoldNumber != "")
            {
                _expItem = PredicateBuilder.And(_expItem, p => p.MoldNumber == MoldNumber);
            }
            if (Keyword != "")
            {
                _expItem = PredicateBuilder.And(_expItem, p => p.Name.Contains(Keyword));
            }

            if (Supplier > 0)
            {
                _expItem = PredicateBuilder.And(_expItem, p => p.SupplierID == Supplier);
            }

            if (StartDate != "")
            {
                DateTime _start = Convert.ToDateTime(StartDate);
                _expItem = PredicateBuilder.And(_expItem, p => p.CreateTime > _start);
            }
            if (EndDate != "")
            {
                DateTime _end = Convert.ToDateTime(EndDate + " 23:59:59");
                _expItem = PredicateBuilder.And(_expItem, p => p.CreateTime < _end);
            }

            _purchaseOrderIDs = _purchaseItemRepository.PurchaseItems.Where(_expItem).Select(p => p.PurchaseOrderID).Distinct();
            return _purchaseOrderIDs.ToList();
        }

        /// <summary>
        /// Provide data for purchase order contents
        /// </summary>
        /// <param name="POID"></param>
        /// <returns></returns>
        public ActionResult JsonPOContents(int POID)
        {
            IEnumerable<PurchaseItem> _items = _purchaseItemRepository.QueryByPurchaseOrderID(POID);
            //IEnumerable<POContent> _items = _poContentRepository.QueryByPOID(POID);
            return Json(new WHPOContentGridViewModel(_items), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// TODO:备库信息
        /// </summary>
        /// <param name="Keyword"></param>
        /// <param name="MoldNumber"></param>
        /// <param name="PurchaseType"></param>
        /// <param name="StockType"></param>
        /// <param name="Exclude"></param>
        /// <returns></returns>
        public ActionResult JsonWarehouseStock(string Keyword="",string MoldNumber="", int PurchaseType=0, int StockType=0, string Exclude="",string Parent="")
        {
            List<WHStock> _stockItems;
            //Expression<Func<WHStock, bool>> _exp = w => w.Enable == true;
            //_exp = PredicateBuilder.And(_exp, w => w.Qty > 0);
            switch (PurchaseType)
            {
                case 1:
                    _stockItems = _whStockRepository.GetWHStocksByType("模具直接材料");
                    break;
                case 2:
                    _stockItems = _whStockRepository.GetWHStocksByType(Parent);
                    break;
                case 6:
                    _stockItems = _whStockRepository.GetWHStocks().Where(s => s.PurchaseType == PurchaseType).ToList();
                    break;
                default:
                    List<int> _typeids = GetPurchaseType(PurchaseType);
                    _stockItems = _whStockRepository.GetWHStocks().Where(s => _typeids.Contains(s.PurchaseType)).ToList();
                    break;
            }

            //if (PurchaseType == 0)
            //{
            //    //_exp = PredicateBuilder.And(_exp, w=>w.Qty>0);
            //}
            //else
            //{

            //    if (PurchaseType == 2)
            //    {
            //        if(Parent== "模具耗材备库")
            //        {

            //        }
            //        else if(Parent == "生产耗材")
            //        {

            //        }
            //        List<int> _typeids = GetPurchaseType(PurchaseType);
            //        _exp = PredicateBuilder.And(_exp, w => (_typeids.Contains(w.PurchaseType)));
            //        if (StockType > 0)
            //        {
            //            _exp = PredicateBuilder.And(_exp, w => w.StockType == StockType);
            //        }                       

            //    }else{
            //        List<int> _typeids = GetPurchaseType(PurchaseType);
            //        _exp = PredicateBuilder.And(_exp, w=>(_typeids.Contains(w.)));

            //    }
            //}
            if (StockType > 0)
            {
                _stockItems = _stockItems.Where(s=>s.StockType== StockType).ToList();
            }
            if (Exclude != "")
            {
                string[] _excludeIDs = Exclude.Split(',');
                List<int> _exclude = new List<int>();
                for (int i = 0; i < _excludeIDs.Length; i++)
                {
                    try
                    {
                        _exclude.Add(Convert.ToInt32(_excludeIDs[i]));
                    }
                    catch
                    {

                    }
                }
                if (_exclude.Count > 0)
                {
                    _stockItems = _stockItems.Where(w => !(_exclude.Contains(w.ID))).ToList();
                    //_exp = PredicateBuilder.And(_exp, w => !(_exclude.Contains(w.ID)));
                }
            }
            _stockItems = _stockItems.Where(s => s.MoldNumber.Contains(MoldNumber)).ToList();
            //_stockItems = _warehouseStockRepository.WarehouseStocks
            //       .Where(w => w.Name.Contains(MoldNumber))
            //       .Where(w => w.Name.Contains(Keyword))
            //       .Where(_exp);
            //_stockItems = _stockItems.Where(_exp);


            WarehouseStockGridViewModel _viewModel = new WarehouseStockGridViewModel(_stockItems,
                _userRepository, _purchaseItemRepository, _purchaseTypeRepository, _stockTypeRepository, 
                _warehouseRepository, _warehousePositionRepository,_whPartRepository);
            return Json(_viewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonWarehouseStockItem(int WarehouseStockID)
        {
            WarehouseStock _stock = _warehouseStockRepository.QueryByID(WarehouseStockID);
            return Json(_stock, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonStockByPO(int PurchaseItemID)
        {
            PurchaseItem _item = _purchaseItemRepository.QueryByID(PurchaseItemID);
            //WarehouseStock _stock = _warehouseStockRepository.WarehouseStocks
            //    .Where(w=>w.MaterialNumber==_item.PartNumber).Where(w=>w.Specification==_item.Specification)
            //    .Where(w=>w.Material==_item.Material).Where(w=>w.SupplierID==_item.SupplierID).FirstOrDefault();
            WHStock _stock = _whStockRepository.WHStocks.Where(s => s.PartNum == _item.PartNumber && s.Qty > 0 && s.Enable == true).FirstOrDefault();

            return Json(_stock, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Provide data for purchase order item detail
        /// </summary>
        /// <param name="POContentID"></param>
        /// <returns></returns>
        public ActionResult JsonPOContent(int PurchaseItemID)
        {
            //POContent _poItem = _poContentRepository.QueryByID(POContentID);
            PurchaseItem _item = _purchaseItemRepository.QueryByID(PurchaseItemID);
            return Json(_item, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonHistory(int PurchaseOrderID)
        {
            IEnumerable<WarehouseRecord> _items = _warehouseRecordRepository.WarehouseRecords
                .Where(w => w.PurchaseOrderID == PurchaseOrderID)
                .OrderBy(w=>w.Date);
            return Json(_items, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region HTTPost
        /// <summary>
        /// TODO:Update the purchase order item in stock record
        /// </summary>
        /// <param name="POContentID"></param>
        /// <param name="ReceiveQty"></param>
        /// <param name="Memo"></param>
        /// <returns></returns>
        [HttpPost]
        public int  POContentInStock(int PurchaseItemID, int ReceiveQty, string Memo, int WarehouseID, int WarehousePositionID)
        {
            PurchaseItem _purchaseItem = _purchaseItemRepository.QueryByID(PurchaseItemID);

            //Update the purchaseItem receive quantity and state   
            if (_purchaseItem.InStockQty + ReceiveQty == _purchaseItem.Quantity)
            {
                _purchaseItem.State = (int)PurchaseItemStatus.完成;
            }
            else if (_purchaseItem.InStockQty+ReceiveQty<_purchaseItem.Quantity)
            {
                _purchaseItem.State = (int)PurchaseItemStatus.部分收货;
            }
            else
            {
                return -1;
            }
            _purchaseItem.InStockQty = _purchaseItem.InStockQty+ReceiveQty;
            _purchaseItem.DeliveryTime = DateTime.Now;
            _purchaseItemRepository.Save(_purchaseItem);//采购结束
            #region 项目采购阶段结束
            if (!string.IsNullOrEmpty(_purchaseItem.MoldNumber))
            {
                IQueryable<PurchaseItem> _puritems = _purchaseItemRepository.PurchaseItems.Where(p => p.MoldNumber == _purchaseItem.MoldNumber);
                bool isPurPhaseFinished = true;
                foreach(var p in _puritems)
                {
                    if(p.State>=(int)PurchaseItemStatus.需求待审批 && p.State < (int)PurchaseItemStatus.完成)
                    {
                        isPurPhaseFinished = false;
                    }
                }
                if (isPurPhaseFinished)
                {
                    IQueryable<Project> _projects = _projectRepository.Projects.Where(p=>p.MoldNumber== _purchaseItem.MoldNumber);
                    foreach(var p in _projects)
                    {
                        ProjectPhase _proJPhase = _projectPhaseRepository.GetProjectPhase(p.ProjectID, 3);
                        if (_proJPhase != null)
                        {
                            if (p.Type == 0 || p.Type == 1)
                            {
                                if (_proJPhase.ActualFinish == new DateTime(1, 1, 1) || _proJPhase.ActualFinish == new DateTime(1900, 1, 1))
                                {
                                    _proJPhase.ActualFinish = DateTime.Now;
                                }
                            }
                            else
                            {
                                _proJPhase.ActualFinish = DateTime.Now;
                            }
                            _projectPhaseRepository.Save(_proJPhase);
                        }                      
                    }
                }
            }
            #endregion
            //Close Purchase Request
            if (_purchaseItem.State==(int)PurchaseItemStatus.完成)
            {
                List<int> _finishstate= new List<int>();
                _finishstate.Add((int)PurchaseItemStatus.待收货);
                _finishstate.Add((int)PurchaseItemStatus.部分收货);
                int _unfinish = _purchaseItemRepository.QueryByPurchaseRequestID(_purchaseItem.PurchaseRequestID)
                    .Where(p => _finishstate.Contains(p.State)).Count();
                if (_unfinish == 0)
                {
                    _prRepository.Submit(_purchaseItem.PurchaseRequestID, 
                        (int)PurchaseRequestStatus.完成, 
                        "全部收货完成,申请单关闭", 
                        Convert.ToInt16(Request.Cookies["User"]["UserID"]));
                }
            }

            //Update the POContent receive quantity and state
            POContent _item = _poContentRepository.POContents.Where(p => p.PurchaseItemID == PurchaseItemID).FirstOrDefault();
            _poContentRepository.Receive(_item.POContentID, ReceiveQty, Memo);
            #region 收货批次记录
            WarehouseRecord _record = new WarehouseRecord();
            _record.UserID=Convert.ToInt32( Request.Cookies["User"]["UserID"]);
            _record.Name = _item.PartName;
            _record.POContentID = _item.POContentID;
            _record.PurchaseOrderID = _item.PurchaseOrderID;
            _record.RecordType = 1;
            _record.Quantity = ReceiveQty;//收货数量
            _record.Memo = Memo;
            _record.Specification = _item.PartSpecification;
            _record.Date = DateTime.Now;
            _warehouseRecordRepository.Save(_record);
            #endregion

            //PurchaseOrder _po = _poRepository.QueryByID(_item.PurchaseOrderID);

            //WarehouseStock _stock=null;
            //if (_purchaseItem.WarehouseStockID > 0)
            //{
            //    _stock = _warehouseStockRepository.QueryByID(_purchaseItem.WarehouseStockID);
            //}
            //else
            //{
            //    _stock = _warehouseStockRepository.WarehouseStocks
            //        .Where(w => w.MaterialNumber == _purchaseItem.PartNumber)
            //        .Where(w => w.Material == _purchaseItem.Material)
            //        .Where(w => w.SupplierID == _purchaseItem.SupplierID)
            //        .Where(w => w.Specification == _purchaseItem.Specification)
            //        .FirstOrDefault();
            //}

            //if (_stock ==null){
            //    _stock = new WarehouseStock();
            //    _stock.Name = _item.PartName;
            //    _stock.Specification = _item.PartSpecification;
            //    _stock.Quantity = _item.ReceivedQty;
            //    _stock.Enabled = true;
            //    _stock.SafeQuantity = 1;
            //    _stock.WarehouseID = 1;
            //    _stock.PurchaseType = _purchaseItem.PurchaseType;
            //    _stock.StockType = 0;
            //    _stock.PurchaseItemID = _purchaseItem.PurchaseItemID;
            //    _stock.InStockTime = DateTime.Now;
            //    _stock.MoldNumber = _purchaseItem.MoldNumber;
            //    _stock.MaterialNumber = _purchaseItem.PartNumber;
            //    _stock.Material = _purchaseItem.Material;
            //    _stock.SupplierID = _purchaseItem.SupplierID;
            //    _stock.SupplierName = _purchaseItem.SupplierName;
            //    _stock.WarehouseID = WarehouseID;
            //    _stock.WarehousePositionID = WarehousePositionID;
            //    _stock.InStockQty = ReceiveQty;
            //    _warehouseStockRepository.Save(_stock);
            //}
            //else
            //{
            //    //_warehouseStockRepository.UpdateQuantity(_stock.WarehouseStockID, ReceiveQty);
            //}
            #region 更新库存
            WHPart _part1 = _whPartRepository.GetPart(_item.PartNumber) ?? new WHPart();
            WHStock _stock = new WHStock()
            {
                PartNum = _item.PartNumber,
                WarehouseID = WarehouseID,
                WarehousePositionID = WarehousePositionID,
                FInStockDate = DateTime.Now,
                LInStockDate = DateTime.Now,
                PurchaseType = _purchaseItem.PurchaseType,
                StockType= Convert.ToInt32(_part1.StockTypes),
                PartID = _purchaseItem.PartID,
                TaskID=_purchaseItem.TaskID,
                MoldNumber=_purchaseItem.MoldNumber,
            };
            var _id= _whStockRepository.Save(_stock);//若没有记录，则创建
            _whStockRepository.StockIncrease(_id, ReceiveQty);//更新库存
            #endregion

            int _unfinishedCount = _purchaseItemRepository.QueryByPurchaseOrderID(_purchaseItem.PurchaseOrderID)
                .Where(p => p.InStockQty < p.Quantity).Count();

            if (_unfinishedCount == 0)
            {
                _poRepository.ClosePurchaseOrder(_purchaseItem.PurchaseOrderID);//全部完成
            }
            else
            {
                //Update the Purchase Order State
                _poRepository.PartialClosePO(_purchaseItem.PurchaseOrderID);//部分完成
            }
            return _unfinishedCount;
        }
        #endregion

        #region 领料申请

        public ActionResult WarehouseRequestList()
        {
            return View();
        }

        public ActionResult JsonWarehouseRequests(string Keyword, string StartDate, string EndDate, string RequestKeyword)
        {
            Expression<Func<WarehouseRequest, bool>> _exp = r=>r.Enabled==true;

            if (StartDate != "")
            {
                try{
                    DateTime _start = Convert.ToDateTime(StartDate);
                    _exp = PredicateBuilder.And(_exp, r => r.CreateDate > _start);
                }catch{

                }
            }

            if (EndDate != "")
            {
                try
                {
                    DateTime _end = Convert.ToDateTime(EndDate + " 23:59:59");
                    _exp = PredicateBuilder.And(_exp, r => r.CreateDate < _end);
                }
                catch
                {

                }
            }

            if (Keyword != "")
            {
                List<int> _warehousestockIDs = _warehouseStockRepository.WarehouseStocks
                    .Where(w => w.Name.Contains(Keyword)).Select(w => w.WarehouseStockID).ToList();

                List<int> _warehouseRequestID = _warehouseRequestItemRepository.WarehouseRequestItems
                    .Where(w => (_warehousestockIDs.Contains(w.WarehouseStockID)))
                    .Select(w => w.WarehouseRequestID)
                    .Distinct().ToList();
                _exp = PredicateBuilder.And(_exp, r => (_warehouseRequestID.Contains(r.WarehouseRequestID)));
            }

            if (RequestKeyword != "")
            {
                _exp = PredicateBuilder.And(_exp, r => r.RequestNumber.Contains(RequestKeyword));
            }



            IEnumerable<WarehouseRequest> _requests = _warehouseRequestRepository.WarehouseRequests.Where(w => w.Enabled == true)
                .Where(_exp).Where(w=>w.State<(int)WarehouseRequestStatus.完成);
            WHRequestGridViewModel _viewModel = new WHRequestGridViewModel(_requests, _userRepository);
            return Json(_viewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonWarehouseRequestItems(int WarehouseRequestID)
        {
            WHRequestItemGridViewModel _items =new WHRequestItemGridViewModel( _warehouseRequestItemRepository.QueryByRequestID(WarehouseRequestID));

            return Json(_items, JsonRequestBehavior.AllowGet);
        }


        public ActionResult WHRequestDetail(int WHRequestID=0)
        {
            
            
            if (WHRequestID > 0)
            {
                WarehouseRequest _request = _warehouseRequestRepository.QueryByID(WHRequestID);
                if (_request != null)
                {
                    ViewBag.RequestNumber = _request.RequestNumber;
                }
                else
                {
                    string _seq = _sequenceRepository.GetNextNumber("WarehouseRequest");
                    ViewBag.RequestNumber = _seq;
                }
            }
            else
            {
                string _seq = _sequenceRepository.GetNextNumber("WarehouseRequest");
                ViewBag.RequestNumber = _seq;
            }
            
            
            ViewBag.WHRequestID = WHRequestID;
            return View();
        }

        //public ActionResult WHStockList(string Keyword)
        //{
        //    IEnumerable<WHStock> _stocks;
        //    if ((Keyword != "")&&(Keyword!="undefined"))
        //    {
        //        _stocks = _whStockRepository.WarehouseStocks.Where(w => w.Name.Contains(Keyword))
        //        .Union(_warehouseStockRepository.WarehouseStocks.Where(w => w.MaterialNumber.Contains(Keyword)))
        //        .Union(_warehouseStockRepository.WarehouseStocks.Where(w => w.Specification.Contains(Keyword)))
        //        .Where(w => w.Quantity > 0).Where(w=>w.Enabled==true).Take(100);
        //    }
        //    else
        //    {
        //        _stocks = _warehouseStockRepository.WarehouseStocks.Where(w=>w.Quantity>0).Where(w=>w.Enabled==true).Take(100);
        //    }
            
        //    return Json(_stocks, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult LoadItem(int ItemID)
        {
            WHStock _item = _whStockRepository.QueryByID(ItemID);
            return Json(_item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string SaveWHRequest(String RequestNumber, 
            IEnumerable<WarehouseRequestItem> Items)
        {
            WarehouseRequest WHRequest = new WarehouseRequest();
            int _userID = 0;
            
            try{
                _userID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
            }
            catch
            {
                _userID = 0;
            }
            WHRequest.RequestNumber = RequestNumber;
            WHRequest.RequestUserID = _userID;
            WHRequest.ApprovalDate = DateTime.Now;
            WHRequest.State = (int)WarehouseRequestStatus.待领;

            int _requestID = _warehouseRequestRepository.Save(WHRequest);

            foreach (WarehouseRequestItem _item in Items)
            {
                _item.WarehouseRequestID = _requestID;
                _warehouseRequestItemRepository.Save(_item);
            }
            return "";
        }
        #endregion

        #region 出库单
        public ActionResult ReceiveItem(string ItemIDs)
        {
            string[] _ids = ItemIDs.Split(',');
            List<WarehouseRequestItem> _items = new List<WarehouseRequestItem>();
            List<WHStock> _stocks = new List<WHStock>();
            for (int i = 0; i < _ids.Length; i++)
            {
                WarehouseRequestItem _item = _warehouseRequestItemRepository.QueryByID(Convert.ToInt32(_ids[i]));
                WHStock _stock = _whStockRepository.QueryByID(_item.WarehouseStockID);
                ViewBag.RequestID = _item.WarehouseRequestID;
                _items.Add(_item);
                _stocks.Add(_stock);
            }
            ViewBag.StockItems = _stocks;
            return View(_items);
        }

        [HttpPost]
        public ActionResult SaveReceiveForm(int WarehouseRequestID, IEnumerable<OutStockItem> Items)
        {
            WarehouseRequest _request = _warehouseRequestRepository.QueryByID(WarehouseRequestID);
            OutStockForm _form = new OutStockForm();
            _form.CreateTime = DateTime.Now;
            _form.RequestID = WarehouseRequestID;
            _form.FormName=_sequenceRepository.GetNextNumber("WarehouseOutStock");
            try
            {
                _form.WHUserID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
            }
            catch
            {
                _form.WHUserID = 0;
            }
            _form.UserID = _request.RequestUserID;
            int _formID = _outStockFormRepository.Save(_form);

            foreach (OutStockItem _item in Items)
            {
                _warehouseStockRepository.UpdateQuantity(_item.WHStockID, -1*_item.Quantity);
                _warehouseRequestItemRepository.Receive(_item.WHRequestID, _item.Quantity);
                WHStock _stock = _whStockRepository.QueryByID(_item.WHStockID);
                WHPart _part =( _whPartRepository.WHParts.Where(p => p.PartNum == _stock.PartNum && p.PartID == _stock.PartID).FirstOrDefault()??new WHPart());
                _item.PartName = _part.PartName;
                _item.PartNumber = _part.PartNum;
                _item.Specification = _part.Specification;
                _item.OutStockFormID = _formID;
                _item.MoldNumber = _stock.MoldNumber;
                _whStockRepository.StockIncrease(_stock.ID, -_item.Quantity);
                _outStockItemRepository.Save(_item);
            }
           
            int leftItemCount = _warehouseRequestItemRepository.QueryByRequestID(WarehouseRequestID).Where(w => w.ReceivedQuantity < w.Quantity).Count();
            if (leftItemCount > 0)
            {               
                _request.State = (int)WarehouseRequestStatus.部分领料;
            }
            else
            {
                _request.State = (int)WarehouseRequestStatus.完成;
            }
            _warehouseRequestRepository.Save(_request);
            return RedirectToAction("WarehouseRequestList", "Warehouse");
        }


        public ActionResult OutStockHistory(string MoldNumber="", string Keyword = "", string StartDate="", string EndDate="")
        {
            ViewBag.Keyword = Keyword;
            ViewBag.MoldNumber = MoldNumber;
            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;
            return View();
        }

        public ActionResult JsonOutStock(string MoldNumber = "", string Keyword = "", 
            string StartDate="",string EndDate="")
        {
            Expression<Func<OutStockItem, bool>> _exp1 = null;
            Expression<Func<OutStockItem, bool>> _exp2 = null;

            IEnumerable<OutStockItem> _outRecords;
            //if ((MoldNumber == "") && (Keyword == ""))
            //{
            //    _outRecords = _outStockItemRepository.OutStockItems.Take(50);
            //}else if ((MoldNumber!="")&&(Keyword==""))
            //{
            //    _outRecords = _outStockItemRepository.OutStockItems.Where(i => i.PartName.Contains(MoldNumber));
            //}
            //else if ((MoldNumber == "") && (Keyword != ""))
            //{
            //    _outRecords = _outStockItemRepository.OutStockItems.Where(i => i.PartName.Contains(Keyword));
            //}
            //else
            //{
            //    _outRecords = _outStockItemRepository.OutStockItems.Where(i => i.PartName.Contains(MoldNumber))
            //        .Union(_outRecords = _outStockItemRepository.OutStockItems.Where(i => i.PartName.Contains(MoldNumber)))
            //        .Take(50);
            //}


            if ((StartDate!="")||(EndDate!="")){            
                try
                {
                    DateTime _start = Convert.ToDateTime(StartDate);
                    _exp1 = i => i.OutDate >= _start;
                }
                catch
                {

                }

                try
                {
                    DateTime _end = Convert.ToDateTime(EndDate+" 23:59:59");
                    if (_exp1 == null)
                    {
                        _exp1 = i => i.OutDate <= _end;
                    }
                    else
                    {
                        _exp2 = i => i.OutDate <= _end;
                        _exp1 = PredicateBuilder.And(_exp1,_exp2);
                    }
                }
                catch
                {

                }

                _outRecords = _outStockItemRepository.OutStockItems.Where(i => i.PartName.Contains(MoldNumber))
                .Where(i => i.PartName.Contains(Keyword))
                .Where(_exp1).Take(50);
            }
            else
            {
                _outRecords = _outStockItemRepository.OutStockItems.Where(i => i.PartName.Contains(MoldNumber))
                .Where(i => i.PartName.Contains(Keyword)).Take(50);
            }


            



            OutStockGridViewModel _viewModel = new OutStockGridViewModel(_outRecords.OrderByDescending(o=>o.OutStockItemID), _outStockFormRepository, _userRepository);

            return Json(_viewModel, JsonRequestBehavior.AllowGet);
        }



        public ActionResult JsonStockType()
        {
            IEnumerable<StockType> _stockType = _stockTypeRepository.StockTypes.Where(s=>s.Enabled==true);
            return Json(_stockType, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PurchaseItemList()
        {
            return View();
        }



        #endregion

        #region 仓库库位管理
        public ActionResult WarehouseInformation()
        {
            return View();
        }

        public ActionResult JsonWarehouses()
        {
            IEnumerable<Warehouse> _warehouses = _warehouseRepository.Warehouses.Where(w => w.Enabled == true);
            return Json(_warehouses, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonWarehouse(int WarehouseID)
        {
            Warehouse _warehouse = _warehouseRepository.QueryByID(WarehouseID);
            return Json(_warehouse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonWarehousePositions(int WarehouseID=0)
        {
            IEnumerable<WarehousePosition> _warehousePositions = _warehousePositionRepository.QueryByWarehouse(WarehouseID);
            return Json(_warehousePositions, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonWarehousePosition(int WarehousePositionID)
        {
            WarehousePosition _warehousePosition = _warehousePositionRepository.QueryByID(WarehousePositionID);
            return Json(_warehousePosition, JsonRequestBehavior.AllowGet);
        }


        public string SaveWarehouse(Warehouse Warehouse)
        {
            int _warehouseID=0;
            try
            {
                _warehouseID = _warehouseRepository.Save(Warehouse);
                return "";
            }
            catch
            {
                return "fail";
            }           

        }

        public string SaveWarehousePosition(WarehousePosition Position)
        {
            try
            {
                _warehousePositionRepository.Save(Position);
                return "";
            }
            catch
            {
                return "fail";
            }
        }

        public string DeleteWarehouse(int WarehouseID)
        {
            int _whPositionCount = _warehousePositionRepository.QueryByWarehouse(WarehouseID).Count();
            if (_whPositionCount == 0)
            {
                try
                {
                    _warehouseRepository.Delete(WarehouseID);
                    return "";
                }
                catch
                {
                    return "仓库删除失败";
                }
            }
            else
            {
                return "请先删除仓库中的所有库位后再删除仓库";
            }
            
        }

        public string DeleteWarehousePosition(int WarehousePositionID)
        {
            try
            {
                int _count = _warehouseStockRepository.WarehouseStocks
                    .Where(w => w.WarehousePositionID == WarehousePositionID)
                    .Where(w=>w.Enabled==true)
                    .Count();
                _count = _count + _warehouseStockRepository.WarehouseStocks
                    .Where(w => w.WarehousePositionID == WarehousePositionID)
                    .Where(w => w.Quantity>0)
                    .Count();
                if (_count == 0)
                {
                    _warehousePositionRepository.Delete(WarehousePositionID);
                    return "";
                }
                else
                {
                    return "当前库位仍有库存件,请先将库存移出库位操作后再删除库位";
                }
                
            }
            catch
            {
                return "库位删除失败";
            }
        }

        public string ChangeStockPosition(string WarehouseStockIDs, int WarehouseID, int WarehousePositionID)
        {
            string[] _ids = WarehouseStockIDs.Split(',');
            string _error = "";
            for (int i = 0; i < _ids.Length; i++)
            {
                try
                {
                    WHStock _stock = _whStockRepository.QueryByID(Convert.ToInt32(_ids[i]));
                    _stock.WarehouseID = WarehouseID;
                    _stock.WarehousePositionID = WarehousePositionID;
                    _whStockRepository.ChangeWHPosition(_stock);
                }
                catch
                {
                    _error = _error == "" ? _ids[i] : _error + "," + _ids[i];
                }
                
            }
            return _error;
        }
        #endregion

        /// <summary>
        /// Left navigation bar for all warehouse pages
        /// </summary>
        /// <returns></returns>
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
            else
            {
                _purchaseTypeIDs = _purchaseTypeRepository.PurchaseTypes.Select(t=>t.PurchaseTypeID).ToList();
            }

            return _purchaseTypeIDs;
        }

        public ActionResult StockList(int PurchaseType = 0)
        {
            switch (PurchaseType)
            {
                case 0:
                    ViewBag.Title = "库存信息";
                    break;
                case 1:
                    ViewBag.Tilte = "在库零件信息";
                    break;
                case 2:
                    ViewBag.Title = "备库查询";
                    break;
                case 4:
                    ViewBag.Tilte = "在库材料信息";
                    break;
                

            }
            ViewBag.TypeID = PurchaseType;
            return View();
        }

        public ActionResult OutSource(int PurchaseType = 3)
        {
            ViewBag.Title = "外发出库";
            ViewBag.PurchaseType = PurchaseType;
            return View();
        }

        public ActionResult JsonStockTypes(string Parent)
        {
            IEnumerable<StockType> _types;
            if (Parent == "模具耗材备库")
            {
                _types = _stockTypeRepository.StockTypes.Where(s => s.Enabled == true && s.Parent== "模具耗材备库");
            }
            else
            {
                _types = _stockTypeRepository.StockTypes.Where(s => s.Enabled == true && s.Parent == "生产耗材");
            }
            
            return Json(_types, JsonRequestBehavior.AllowGet);
        }


        public string SetSafeQuantity(string StockItemIDs, int SafeQty)
        {
            try
            {
                _warehouseStockRepository.SetSafeQty(StockItemIDs, SafeQty);
                return "";
            }
            catch
            {
                return "设置错误";
            }
        }        

        public string DeleteStock(string WarehouseStockIDs)
        {
            string[] _ids = WarehouseStockIDs.Split(',');
            string _error = "";
            for (int i = 0; i < _ids.Length; i++)
            {
                //WarehouseStock _stock = _warehouseStockRepository.QueryByID(Convert.ToInt32(_ids[i]));
                WHStock _stock = _whStockRepository.QueryByID(Convert.ToInt32(_ids[i]));
                _stock.Enable = false;
                try
                {
                    _whStockRepository.Save(_stock);
                }
                catch
                {
                    _error = _error == "" ? _stock.PartNum : "," + _stock.PartNum;
                }
            }
            return _error;
        }
        /// <summary>
        /// TODO:库存MoldNum List
        /// </summary>
        /// <param name="PurchaseType">0 所有 1 零件 6 铁件</param>
        /// <param name="Keyword"></param>
        /// <returns></returns>
        public ActionResult JsonMoldNumber(int PurchaseType=0, string Keyword="") {
            List<int> _typeids = GetPurchaseType(PurchaseType);
            List<string> _moldNumbers;
            switch (PurchaseType)
            {
                case 0:
                    _moldNumbers = _whStockRepository.GetWHStocks().Where(s => s.MoldNumber.Contains(Keyword))
                    .Select(s => s.MoldNumber).Distinct().ToList();
                    break;
                case 6:
                    _moldNumbers = _whStockRepository.GetWHStocks().Where(s => PurchaseType==s.PurchaseType && s.MoldNumber.Contains(Keyword))
                    .Select(s => s.MoldNumber).Distinct().ToList();
                    break;
                default:
                    _moldNumbers = _whStockRepository.GetWHStocks().Where(s => _typeids.Contains(s.PurchaseType) && s.MoldNumber.Contains(Keyword))
                    .Select(s => s.MoldNumber).Distinct().ToList();
                    break;
            }
            //if (PurchaseType > 0)
            //{
            //    _moldNumbers = _whStockRepository.GetWHStocks().Where(s => _typeids.Contains(s.PurchaseType) && s.MoldNumber.Contains(Keyword))
            //        .Select(s => s.MoldNumber).Distinct().ToList();
            //    // _moldNumbers = _warehouseStockRepository.WarehouseStocks
            //    //.Where(w => w.MoldNumber.Contains(Keyword))
            //    //.Where(w => w.Quantity > 0)
            //    //.Where(w => w.Enabled == true)
            //    //.Where(w => (_typeids.Contains(w.PurchaseType)))
            //    //.Select(w => w.MoldNumber).Distinct();
            //}
            //else
            //{
            //    _moldNumbers = _whStockRepository.GetWHStocks().Where(s => s.MoldNumber.Contains(Keyword))
            //        .Select(s => s.MoldNumber).Distinct().ToList();
            //    //_moldNumbers = _warehouseStockRepository.WarehouseStocks
            //    //.Where(w => w.MoldNumber.Contains(Keyword))
            //    //.Where(w => w.Quantity > 0)
            //    //.Where(w => w.Enabled == true)
            //    ////.Where(w => (_typeids.Contains(w.PurchaseType)))
            //    //.Select(w => w.MoldNumber).Distinct();
            //}                           
            return Json(_moldNumbers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult JsonStockPositionInfo(string WarehouseStockIDs)
        {
            string[] _ids = WarehouseStockIDs.Split(',');
            List<WarehousePositionEditModel> _model= new List<WarehousePositionEditModel>();
            for (int i = 0; i < _ids.Length; i++)
            {
                WHStock _stock = _whStockRepository.QueryByID(Convert.ToInt32(_ids[i]));
                string _warehouseName = _warehouseRepository.QueryByID(_stock.WarehouseID).Name;
                string _positionName = _warehousePositionRepository.QueryByID(_stock.WarehousePositionID).Name;
                string _name = (_whPartRepository.GetPart(_stock.PartNum, _stock.PartID)??new WHPart()).PartName;
                WarehousePositionEditModel _row = new WarehousePositionEditModel(_stock, _name, _warehouseName, _positionName);
                _model.Add(_row);
            }
            return Json(_model, JsonRequestBehavior.AllowGet);
        }


        #region 退货单处理
        public ActionResult ReturnRequestList()
        {
            return View();
        }

        public ActionResult JsonReturnRequests(int State, string StartDate = "", string EndDate = "", string Keyword="")
        {
            IEnumerable<ReturnRequest> _data;
            Expression<Func<ReturnRequest, bool>> _exp =null;
            switch (State)
            {
                //Unsubmitted request
                case -1:
                    _exp = w => w.State == (int)ReturnRequestStatus.新建;
                    break;
                //Submitted request
                case 0:
                    _exp = w => w.State == (int)ReturnRequestStatus.待审批;
                    break;                
                case 1:
                    _exp =w => w.State == (int)ReturnRequestStatus.通过;
                    _exp=PredicateBuilder.Or(_exp, w=>w.State==(int)ReturnRequestStatus.拒绝);
                    break;
                case 2:
                    _exp = w => w.State == (int)ReturnRequestStatus.已关闭;
                    break;
                default:
                    _exp =  w=>w.Enabled==true;
                    break;
            }
            if (StartDate != "")
            {
                DateTime _startDateVal= Convert.ToDateTime( StartDate+" 00:00");
                _exp = PredicateBuilder.And(_exp, w => w.CreateDate >_startDateVal);
            }

            if (EndDate != "")
            {
                DateTime _endDateVal = Convert.ToDateTime(EndDate + " 23:59");
                _exp = PredicateBuilder.And(_exp, w => w.CreateDate < _endDateVal);
            }

            if (Keyword != "")
            {
                List<int> _requestIDs = QueryReturnRequestByKeyword(Keyword);
                _exp =  PredicateBuilder.And(_exp, w=>(_requestIDs.Contains(w.ReturnRequestID)));
            }
            _data = _returnRequestRepository.ReturnRequests.Where(_exp).Take(50).OrderByDescending(r => r.CreateDate);
            ReturnRequestGridViewModel _viewModel = new ReturnRequestGridViewModel(_data, _poRepository, _userRepository);
            return Json(_viewModel, JsonRequestBehavior.AllowGet);
        }

        public List<int> QueryReturnRequestByKeyword(string Keyword)
        {
            List<int> _returnRequestIDs = _returnItemRepository.ReturnItems.Where(r => r.Enabled == true)
                .Where(r => r.Name.Contains(Keyword)).Select(r => r.ReturnRequestID).Distinct().ToList();
            return _returnRequestIDs;
        }

        public ActionResult ReturnRequestDetail(int ReturnRequestID = 0, string PurchaseItemIDs = "", int PurchaseOrderID=0)
        {
            ViewBag.ReturnRequestID = ReturnRequestID;
            ViewBag.PurchaseItemIDs = PurchaseItemIDs;
            ReturnRequest _request;
            if (ReturnRequestID != 0)
            {
                _request = _returnRequestRepository.QueryByID(ReturnRequestID);
            }
            else
            {
                PurchaseOrder _order = _poRepository.QueryByID(PurchaseOrderID);
                _request = new ReturnRequest(_order);
            }
            return View(_request);
        }

        [HttpPost]
        public string SaveReturnRequest(ReturnRequest ReturnRequest, IEnumerable<ReturnItem> ReturnItems)
        {
            int _requestID = 0;
            if (ReturnRequest.ReturnRequestID == 0)
            {
                ReturnRequest.ReturnRequestNumber = _sequenceRepository.GetNextNumber("ReturnRequest");
                ReturnRequest.CreateDate = DateTime.Now;
                ReturnRequest.Enabled = true;
                ReturnRequest.WarehouseUserID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
            }
            _requestID = _returnRequestRepository.Save(ReturnRequest);
            foreach (ReturnItem _item in ReturnItems)
            {
                if (_item.ReturnItemID == 0)
                {
                    _item.Enabled = true;
                }
                _item.ReturnRequestID = ReturnRequest.ReturnRequestID;
                _item.ReturnRequestID = _requestID;
                _returnItemRepository.Save(_item);
            }
            return _requestID.ToString();
            
        }

        public string SubmitReturnRequest(int ReturnRequestID)
        {
            ReturnRequest _request = _returnRequestRepository.QueryByID(ReturnRequestID);
            _request.State = (int)ReturnRequestStatus.待审批;
            _returnRequestRepository.Save(_request);

            IEnumerable<ReturnItem> _items = _returnItemRepository.QueryByRequest(ReturnRequestID);
            foreach (ReturnItem _item in _items)
            {
                //WHStock _stock = _whStockRepository.QueryByID(_item.WarehouseItemID);
                //_stock.Quantity = _stock.Quantity - _item.Quantity;
                //_stock.InStockQty = _stock.InStockQty - _item.Quantity;
                //_warehouseStockRepository.Save(_stock);
                _whStockRepository.StockReturn(_item.WarehouseItemID, _item.Quantity);
            }
            return "";
        }

        public string ReviewReturnRequest(int ReturnRequestID, bool Approve)
        {
            ReturnRequest _request = _returnRequestRepository.QueryByID(ReturnRequestID);
            if (Approve)
            {
                _request.State = (int)ReturnRequestStatus.通过;
                
            }
            else
            {
                _request.State = (int)ReturnRequestStatus.拒绝;
            }
            _request.ApprovalDate = DateTime.Now;
            _request.ApprovalUserID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
            _returnRequestRepository.Save(_request);

            if (!Approve)
            {
                IEnumerable<ReturnItem> _items = _returnItemRepository.QueryByRequest(ReturnRequestID);
                foreach (ReturnItem _item in _items)
                {
                    //WarehouseStock _stock = _warehouseStockRepository.QueryByID(_item.WarehouseItemID);
                    //_stock.Quantity = _stock.Quantity + _item.Quantity;
                    //_stock.InStockQty = _stock.InStockQty + _item.Quantity;
                    //_warehouseStockRepository.Save(_stock);
                    _whStockRepository.StockIncrease(_item.WarehouseItemID, _item.Quantity);
                }
            }
            
            return "";
        }

        public string RestartReturnRequest(int ReturnRequestID)
        {
            ReturnRequest _request = _returnRequestRepository.QueryByID(ReturnRequestID);
            if (_request != null)
            {
                _request.State = (int)ReturnRequestStatus.新建;
                _request.WarehouseUserID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
                _returnRequestRepository.Save(_request);
            }
            return "";
        }

        public string CloseReturnRequest(int ReturnRequestID)
        {
            ReturnRequest _request = _returnRequestRepository.QueryByID(ReturnRequestID);
            if (_request != null)
            {
                if (_request.State == (int)ReturnRequestStatus.通过)
                {
                    PurchaseOrder _po = _poRepository.QueryByID(_request.PurchaseOrderID);
                    _po.State = (int)PurchaseOrderStatus.部分收货;
                    _poRepository.Save(_po);

                    IEnumerable<ReturnItem> _rrItems = _returnItemRepository.QueryByRequest(ReturnRequestID);
                    foreach (ReturnItem _rrItem in _rrItems)
                    {
                        PurchaseItem _purchaseItem = _purchaseItemRepository.QueryByID(_rrItem.PurchaseItemID);
                        _purchaseItem.InStockQty = _purchaseItem.InStockQty - _rrItem.Quantity;
                        _purchaseItemRepository.Save(_purchaseItem);

                        WarehouseRecord _record = new WarehouseRecord();

                        _record.UserID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
                        _record.Name = _rrItem.Name;
                        _record.POContentID =0;
                        _record.PurchaseOrderID = _purchaseItem.PurchaseOrderID;
                        _record.RecordType = 3;
                        _record.Quantity = _rrItem.Quantity;
                        _record.Memo = _rrItem.Memo;
                        _record.Specification = _rrItem.Specification;
                        _record.Date = DateTime.Now;
                        _warehouseRecordRepository.Save(_record);
                    }
                }
                _request.State = (int)ReturnRequestStatus.已关闭;
                _request.ReturnDate = DateTime.Now;
                _returnRequestRepository.Save(_request);
            }
            return "";
        }

        public ActionResult JsonReturnItems(int ReturnRequestID = 0, string PurchaseItemIDs = "")
        {
            int _state;
            if (ReturnRequestID>0){
                _state= _returnRequestRepository.QueryByID(ReturnRequestID).State;
            }else{
                _state=-1;
            }
            ReturnRequest _request = _returnRequestRepository.QueryByID(ReturnRequestID);
            List<ReturnItem> _returnItems = new List<ReturnItem>();
            
            if (ReturnRequestID > 0)
            {
                _returnItems.AddRange(_returnItemRepository.QueryByRequest(ReturnRequestID));
            }
            if (PurchaseItemIDs!="")
            {
                
                string[] _purchaseItems = PurchaseItemIDs.Split(',');
                for (int i = 0; i < _purchaseItems.Length; i++)
                {
                    try
                    {
                        int _count = _returnItems.Where(r => r.PurchaseItemID == Convert.ToInt32(_purchaseItems[i])).Count();
                        if (_count == 0)
                        {
                            PurchaseItem _purchaseItem = _purchaseItemRepository.QueryByID(Convert.ToInt32(_purchaseItems[i]));
                            //WarehouseStock _stock = _warehouseStockRepository.WarehouseStocks
                            //    .Where(w => w.PurchaseItemID == _purchaseItem.PurchaseItemID).FirstOrDefault();
                            WHStock _stock = _whStockRepository.WHStocks.Where(p => p.PartNum == _purchaseItem.PartNumber && p.PartID == _purchaseItem.PartID).FirstOrDefault();
                            ReturnItem _item = new ReturnItem(_purchaseItem, _stock.ID);
                            _returnItems.Add(_item);
                        }
                    }
                    catch
                    {

                    }
                    
                    
                }
            }
            ReturnItemGridViewModel _viewModel = new ReturnItemGridViewModel(_returnItems,
                _purchaseItemRepository, 
                _whStockRepository, 
                _state);
            return Json(_viewModel, JsonRequestBehavior.AllowGet);
        }

        public string ReturnItemDelete(string ReturnItemIDs)
        {
            string[] _ids = ReturnItemIDs.Split(',');
            string _failIDs = "";
            int ReturnRequestID = _returnItemRepository.QueryByID(Convert.ToInt32(_ids[0])).ReturnRequestID;
            for (int i = 0; i < _ids.Length; i++)
            {
                try
                {
                    _returnItemRepository.Delete(Convert.ToInt32(_ids[i]));
                }
                catch
                {
                    _failIDs = _failIDs == "" ? _ids[i] : _failIDs + "," + _ids[i];
                }
                
            }
            if (_failIDs == "")
            {
                int _count = _returnItemRepository.QueryByRequest(ReturnRequestID).Count();
                return _count.ToString();
            }
            else
            {
                return "fail";
            }
        }

        public int SaveStockType(string Name,string Code)
        {
            //StockType _stockType = _stockTypeRepository.QueryByName(Name);
            StockType _stockType = new StockType()
            {
                StockTypeID = 0,
                Name = Name,
                Code = Code,
                Enabled = true,
            };
            if (_stockType != null)
            {
                try
                {
                    int _stockTypeID = _stockTypeRepository.Save(_stockType);
                    return _stockTypeID;
                }
                catch(Exception ex)
                {
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }

        public int DeleteStockType(int StockTypeID)
        {
            try
            {
                _stockTypeRepository.Delete(StockTypeID);
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        public void DeleteReturnRequest(int ReturnRequestID)
        {
            try
            {
                _returnRequestRepository.Delete(ReturnRequestID);
            }
            catch
            {

            }
        }


        public ActionResult JsonOutStockMoldNumber(string Keyword="")
        {

            IEnumerable<string> _moldNumbers = _outStockItemRepository.OutStockItems.Where(o => o.MoldNumber.Contains(Keyword))
                .Where(o=>o.MoldNumber!="")
                .Select(o => o.MoldNumber).Distinct();
            return Json(_moldNumbers, JsonRequestBehavior.AllowGet);
        }
        #endregion


        public string DeleteWarehouseStocks(string WarehouseStockIDs)
        {
            string[] _ids = WarehouseStockIDs.Split(',');
            string _error = "";
            for (int i = 0; i < _ids.Length; i++)
            {
                try
                {
                    _warehouseStockRepository.DeleteStock(Convert.ToInt32(_ids[i]));
                }
                catch
                {
                    
                }                
            }
            return _error;
        }

        public ActionResult LoadPOMoldNumbers(string Keyword)
        {
            List<int> _states = new List<int>();

            _states.Add((int)PurchaseItemStatus.待收货);
            _states.Add((int)PurchaseItemStatus.部分收货);
            _states.Add((int)PurchaseItemStatus.完成);

            IEnumerable<string> _moldNumbers = _purchaseItemRepository.PurchaseItems
                .Where(p => (_states.Contains(p.State)))
                .Where(p => p.Name.Contains(Keyword))
                .Select(p => p.MoldNumber).Distinct();

            return Json(_moldNumbers, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult QueryStockParts(string Specification="", string Material="")
        //{
        //    Expression<Func<WarehouseStock, bool>> _exp = w => w.Enabled == true;
        //    if (Specification != "")
        //    {
        //        try
        //        {
        //            _exp = PredicateBuilder.And(_exp, w => w.Specification.Contains(Specification));
        //        }
        //        catch
        //        {

        //        }
        //    }
        //    if (Material != "")
        //    {
        //        try
        //        {
        //            _exp = PredicateBuilder.And(_exp, w => w.Material.Contains(Material));
        //        }
        //        catch
        //        {

        //        }
        //    }

        //    IEnumerable<WarehouseStock> _stocks = _warehouseStockRepository.WarehouseStocks.Where(w => w.StockType > 0)
        //        .Where(_exp);
        //    return Json(_stocks, JsonRequestBehavior.AllowGet);

        //}


        public ActionResult InStockHistory()
        {
            return View();
        }

        #region 库存零件管理
        [HttpPost]
        public void StockItemEdit(WHPart model)
        {
            //WHPart _part = _whPartRepository.GetPart(model.PartNum);
            //if (_part == null)
            //{
            //    WarehouseStock.Quantity = 0;
            //}
            model.CreateUserID = Convert.ToInt32(Request.Cookies["User"]["UserID"]);
            _whPartRepository.Save(model);
            //int _warehouseStockID = _warehouseStockRepository.Save(WarehouseStock);
        }
        public string Service_WH_GetPartNumByType(string _type)
        {
            string _partNum = _whPartRepository.GetPartNum(_type);
            return _partNum;
        }
        public string Service_WH_GetStockTypeCode(int _id)
        {
            StockType _type = _stockTypeRepository.QueryByID(_id);
            if (_type != null)
            {
                return _type.Code;
            }
            return null;
        }
        public JsonResult Service_GetHCClassList()
        {
            List<StockType> _hcTypes = _stockTypeRepository.GetTypeList("生产耗材");
            return Json(_hcTypes, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Service_GetBKClassList()
        {
            List<StockType> _hcTypes = _stockTypeRepository.GetTypeList("模具耗材备库");
            return Json(_hcTypes, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}