using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Status;

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
                MoldNumber = ProjectRepository.GetByID(_part.ProjectID).MoldNumber;
                rows.Add(new PurchaseContentGridRowModel(_part, MoldNumber));
            }
            Page = 1;
            Total = Parts.Count() ;
            Records = 500;
        }

        public PurchaseContentGridViewModel(IEnumerable<Task> Tasks, 
            IProjectPhaseRepository ProjectPhaseRepository, 
            ISteelCAMDrawingRepository SteelDrawingRepo)
        {
            rows = new List<PurchaseContentGridRowModel>();
            foreach (Task _task in Tasks){
                rows.Add(new PurchaseContentGridRowModel(_task, ProjectPhaseRepository, SteelDrawingRepo));
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
            IPurchaseTypeRepository PurchaseTypeRepository)
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
                if (_purchaseItem.CostCenterID>0){
                    _costcenter = CostCenterRepository.QueryByID(_purchaseItem.CostCenterID).Name;
                }else{
                    _costcenter="";
                }

                string _purchaseType="";
                try
                {
                    _purchaseType = PurchaseTypeRepository.QueryByID(_content.PurchaseTypeID).Name;
                }
                catch
                {

                }
                
                rows.Add(new PurchaseContentGridRowModel(_content, state, _costcenter, _purchaseType));
            }
            Page = 1;
            Total = PRContents.Count();
            Records = 500;
        }
    }
}