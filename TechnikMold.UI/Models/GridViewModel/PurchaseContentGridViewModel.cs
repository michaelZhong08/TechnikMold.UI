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

        public PurchaseContentGridViewModel(IEnumerable<Part> Parts, IProjectRepository ProjectRepository)
        {
            rows = new List<PurchaseContentGridRowModel>();
            string MoldNumber;
            foreach (Part _part in Parts)
            {
                MoldNumber= _part.Name.Split(new char[] { '_' })[0]??"";
                rows.Add(new PurchaseContentGridRowModel(_part, MoldNumber));
            }
            Page = 1;
            Total = Parts.Count() ;
            Records = 500;
        }

        public PurchaseContentGridViewModel(IEnumerable<Task> Tasks,
            List<SetupTaskStart> _viewmodel,
            IProjectPhaseRepository ProjectPhaseRepository, 
            ISteelCAMDrawingRepository SteelDrawingRepo,
            ITaskRepository TaskRepository
            )
        {
            rows = new List<PurchaseContentGridRowModel>();
            foreach (var m in _viewmodel)
            {
                Task _task = TaskRepository.QueryByTaskID(m.TaskID) ?? new Task();
                rows.Add(new PurchaseContentGridRowModel(_task, m, ProjectPhaseRepository, SteelDrawingRepo));
            }
            Page=1;
            Total=Tasks.Count();
            Records=500;
        }

        public PurchaseContentGridViewModel(IEnumerable<WarehouseStock> WarehouseStocks)
        {
            rows = new List<PurchaseContentGridRowModel>();
            foreach (WarehouseStock _stock in WarehouseStocks)
            {
                rows.Add(new PurchaseContentGridRowModel(_stock));
            }
            Page = 1;
            Total = WarehouseStocks.Count();
            Records = 500;
        }

        public PurchaseContentGridViewModel(List<PRContent> PRContents,
           IPurchaseItemRepository PurchaseItemRepository,
           ICostCenterRepository CostCenterRepository,
           IPartRepository PartRepository)
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
                if (_purchaseItem.CostCenterID > 0)
                {
                    _costcenter = CostCenterRepository.QueryByID(_purchaseItem.CostCenterID).Name;
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
                rows.Add(new PurchaseContentGridRowModel(_content, state, _costcenter, ERPNo));
            }
            Page = 1;
            Total = PRContents.Count();
            Records = 500;
        }
    }
}