﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;
using MoldManager.WebUI.Models.GridRowModel;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class PurchaseItemGridViewModel
    {
        public List<PurchaseItemGridRowModel> rows;
        public int Page;
        public int Total;
        public int Records;
        public PurchaseItemGridViewModel(IEnumerable<PurchaseItem> PurchaseItems, 
            IPurchaseRequestRepository PurchaseRequestRepo, 
            IQuotationRequestRepository QuotationRequestRepo, 
            IPurchaseOrderRepository PurchaseOrderRepo,
            IUserRepository UserRepo, 
            IPurchaseTypeRepository TypeRepo,
            IPurchaseItemRepository PurchaseItemRepo)
        {
            rows = new List<PurchaseItemGridRowModel>();
            foreach (PurchaseItem _item in PurchaseItems)
            {
                string _prNo, _qrNo, _poNo;
                string _purchaseUser;
                #region region
                try
                {
                    if (_item.PurchaseRequestID > 0)
                    {
                        _prNo = PurchaseRequestRepo.GetByID(_item.PurchaseRequestID).PurchaseRequestNumber;
                    }
                    else
                    {
                        _prNo = "";
                    }
                }
                catch
                {
                    _prNo = "";
                }
                

                try
                {
                    if (_item.QuotationRequestID > 0)
                    {
                        _qrNo = QuotationRequestRepo.GetByID(_item.QuotationRequestID).QuotationNumber;
                    }
                    else
                    {
                        _qrNo = "";
                    }
                }
                catch
                {
                    _qrNo = "";
                }
                

                try
                {
                    if (_item.PurchaseOrderID > 0)
                    {
                        _poNo = PurchaseOrderRepo.QueryByID(_item.PurchaseOrderID).PurchaseOrderNumber;
                    }
                    else
                    {
                        _poNo = "";
                    }
                    }
                catch
                {
                    _poNo = "";
                }


                try
                {
                    if (_item.PurchaseUserID > 0)
                    {
                        _purchaseUser = UserRepo.GetUserByID(_item.PurchaseUserID).FullName;
                    }
                    else
                    {
                        _purchaseUser = "";
                    }
                }
                catch
                {
                    _purchaseUser = "";
                }

                string _purchaseType;
                try
                {
                    _purchaseType = TypeRepo.QueryByID(_item.PurchaseType).Name;
                }catch{
                    _purchaseType = "";
                }

                string _requestUser;
                try
                {
                    if (_item.RequestUserID > 0)
                    {
                        _requestUser = UserRepo.GetUserByID(_item.RequestUserID).FullName;
                    }else{
                        _requestUser = "";
                    }
                }
                catch
                {
                    _requestUser = "";
                }
                #endregion
                string _htmlTitle = "";
                List<PurItemChangeDateRecord> _puritems = PurchaseItemRepo.GetPurItemChangeDateRecords(_item.PurchaseItemID);
                if (_puritems.Count > 0)
                {
                    _htmlTitle = "<table><tr><th>调整后计划</th><th>调整人</th><th>调整时间</th></tr>";
                    foreach (var r in _puritems)
                    {
                        _htmlTitle = _htmlTitle + "<tr>";
                        _htmlTitle = _htmlTitle + "<td>" + ((r.PlanAJDate == new DateTime(1900, 1, 1) ? "-" : r.PlanAJDate.ToString("yyyy-MM-dd"))) + "</td>";
                        _htmlTitle = _htmlTitle + "<td>"+r.UserName.ToString()+"</td>";                        
                        _htmlTitle = _htmlTitle + "<td>" + ((r.CreDate == new DateTime(1900, 1, 1) ? "-" : r.CreDate.ToString("yyyy-MM-dd HH:mm"))) + "</td>";
                        _htmlTitle = _htmlTitle + "</tr>";
                    }
                    _htmlTitle = _htmlTitle + "</table>";
                }
                else
                {
                    _htmlTitle = "";
                }
                
                PurchaseItemGridRowModel _row = new PurchaseItemGridRowModel(_item, _prNo, _qrNo, _poNo, _purchaseUser, _purchaseType, _requestUser, _htmlTitle);
                rows.Add(_row);                
            }
            Records = PurchaseItems.Count();
        }
    }
}