﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikMold.UI.Models.ViewModel;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class PurchaseOrderReportGridViewModel
    {
        public List<PurchaseOrderReportGridRowModel> rows = new List<PurchaseOrderReportGridRowModel>();
        public int Page;
        public int Total;
        public int Records;

        public PurchaseOrderReportGridViewModel(List<PDReportViewModel> Items, 
            IPurchaseTypeRepository TypeRepo, 
            ISupplierRepository SupplierRepo, 
            IPurchaseOrderRepository PORepo, 
            ICostCenterRepository CostCenterRepo)
        {
            //Total = Items.Count();
            //string _subtype = "", _maintype = "";
            //string _supplier = "";
            //string _costcenter = "";
            //string _purchaseOrder = "";
            foreach (var _item in Items)
            {
                //PurchaseType _type = TypeRepo.QueryByID(_item.PurchaseType);
                //if (_type.ParentTypeID == 0)
                //{
                //    _maintype = _type.Name;
                //    _subtype = "";
                //}else{
                //    _maintype = TypeRepo.QueryByID(_type.ParentTypeID).Name;
                //    _subtype=_type.Name;
                //}
                //if(SupplierRepo.QueryByID(_item.SupplierID)!=null)
                //    _supplier = SupplierRepo.QueryByID(_item.SupplierID).FullName;
                //_supplier = "";
                //if (CostCenterRepo.QueryByID(1) != null)
                //    _costcenter = CostCenterRepo.QueryByID(1).Name;
                //_costcenter = "";
                //_purchaseOrder = PORepo.QueryByID(_item.PurchaseOrderID).PurchaseOrderNumber;
                rows.Add(new PurchaseOrderReportGridRowModel(_item));
            }
        }
    }
}