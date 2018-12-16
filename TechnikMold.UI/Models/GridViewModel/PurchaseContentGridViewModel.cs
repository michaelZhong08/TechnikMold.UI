using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Status;
using TechnikMold.UI.Models.ViewModel;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class PurchaseContentGridViewModel
    {
        
        public List<PurchaseContentGridRowModel> rows;
        public int Page;
        public int Total;
        public int Records;
        /// <summary>
        /// 数据源：设计清单
        /// </summary>
        /// <param name="Parts"></param>
        /// <param name="ProjectRepository"></param>
        public PurchaseContentGridViewModel(IEnumerable<Part> Parts, IProjectRepository ProjectRepository, string mrPurDate = "")
        {
            rows = new List<PurchaseContentGridRowModel>();
            string MoldNumber;
            foreach (Part _part in Parts)
            {
                MoldNumber= _part.Name.Split(new char[] { '_' })[0]??"";
                rows.Add(new PurchaseContentGridRowModel(_part, mrPurDate, MoldNumber));
            }
            Page = 1;
            Total = Parts.Count() ;
            Records = 500;
        }
        /// <summary>
        /// 数据源：外发
        /// </summary>
        /// <param name="Tasks"></param>
        /// <param name="_viewmodel"></param>
        /// <param name="ProjectPhaseRepository"></param>
        /// <param name="SteelDrawingRepo"></param>
        /// <param name="TaskRepository"></param>
        public PurchaseContentGridViewModel(IEnumerable<Task> Tasks,
            List<SetupTaskStart> _viewmodel,            
            IProjectPhaseRepository ProjectPhaseRepository, 
            ISteelCAMDrawingRepository SteelDrawingRepo,
            ITaskRepository TaskRepository,
            IWHPartRepository WHPartRepository,
            IPurchaseTypeRepository PurchaseTypeRepository,
            string mrPurDate = ""
            )
        {
            rows = new List<PurchaseContentGridRowModel>();
            foreach (var m in _viewmodel)
            {
                Task _task = TaskRepository.QueryByTaskID(m.TaskID) ?? new Task();
                rows.Add(new PurchaseContentGridRowModel(_task, m, mrPurDate, ProjectPhaseRepository, SteelDrawingRepo, WHPartRepository, PurchaseTypeRepository));
            }
            Page=1;
            Total=Tasks.Count();
            Records=500;
        }
        /// <summary>
        /// 数据源：备库
        /// </summary>
        /// <param name="WarehouseStocks"></param>
        public PurchaseContentGridViewModel(IEnumerable<WarehouseStock> WarehouseStocks, string mrPurDate = "")
        {
            rows = new List<PurchaseContentGridRowModel>();
            foreach (WarehouseStock _stock in WarehouseStocks)
            {
                rows.Add(new PurchaseContentGridRowModel(mrPurDate,_stock));
            }
            Page = 1;
            Total = WarehouseStocks.Count();
            Records = 500;
        }
        /// <summary>
        /// 数据源：申请单
        /// </summary>
        /// <param name="PRContents"></param>
        /// <param name="PurchaseItemRepository"></param>
        /// <param name="CostCenterRepository"></param>
        /// <param name="PartRepository"></param>
        public PurchaseContentGridViewModel(List<PRContent> PRContents,
           IPurchaseItemRepository PurchaseItemRepository,
           ICostCenterRepository CostCenterRepository,
           IPartRepository PartRepository,
           ITaskHourRepository TaskHourRepository)
        {
            rows = new List<PurchaseContentGridRowModel>();

            string state = "";
            foreach (PRContent _content in PRContents)
            {
                PurchaseItem _purchaseItem = PurchaseItemRepository.QueryByID(_content.PurchaseItemID);
                try
                {
                    state = Enum.GetName(typeof(PurchaseItemStatus), _purchaseItem.State);
                }
                catch
                {
                    state = "";
                }

                String _costcenter;
                CostCenter _centerObj= CostCenterRepository.QueryByID(_purchaseItem.CostCenterID);
                if (_centerObj!=null)
                {
                    _costcenter = _centerObj.Name;
                }
                else
                {
                    _costcenter = "";
                }
                string ERPNo = string.Empty;
                //if (_purchaseItem.PartID > 0)
                //{
                //    Part _part = PartRepository.QueryByID(_purchaseItem.PartID);
                //    ERPNo = _part.ERPPartID;
                //}
                ERPNo = _content.ERPPartID;
                SetupTaskStart _setupTask=new SetupTaskStart();
                #region 外发内容
                if (_content.TaskID > 0)
                {
                    TaskHour _taskhour = TaskHourRepository.TaskHours.Where(h => h.TaskID == _content.TaskID).OrderByDescending(h => h.TaskHourID).FirstOrDefault();
                    if (_taskhour != null)
                    {
                        _setupTask.TaskID = _taskhour.TaskID;
                        _setupTask.MachinesName = TaskHourRepository.GetMachineByTask(_taskhour.TaskID) ?? "";
                        _setupTask.TotalTime = TaskHourRepository.GetTotalHourByTaskID(_taskhour.TaskID);
                        _setupTask.UserName = _taskhour.Operater;
                        _setupTask.MachinesCode = _taskhour.MachineCode;
                    }

                }               
                #endregion
                rows.Add(new PurchaseContentGridRowModel(_content, state, _costcenter, ERPNo, _setupTask));
            }
            Page = 1;
            Total = PRContents.Count();
            Records = 500;
        }
    }
}