using System;
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
        public PurchaseItemGridViewModel(List<PurchaseItem> PurchaseItems,
            IPurchaseRequestRepository PurchaseRequestRepo,
            IQuotationRequestRepository QuotationRequestRepo,
            IPurchaseOrderRepository PurchaseOrderRepo,
            IUserRepository UserRepo,
            IPurchaseTypeRepository TypeRepo,
            IPurchaseItemRepository PurchaseItemRepo)
        {
            rows = new List<PurchaseItemGridRowModel>();
            List<PurchaseRequest> _preqs = PurchaseRequestRepo.PurchaseRequests.ToList();
            List<QuotationRequest> _quoreqs = QuotationRequestRepo.QuotationRequests.ToList();
            List<PurchaseOrder> _pos = PurchaseOrderRepo.PurchaseOrders.ToList();
            List<User> _users = UserRepo.Users.ToList();
            List<PurchaseType> _types = TypeRepo.PurchaseTypes.ToList();
            List<PurItemChangeDateRecord> _itemCDs = PurchaseItemRepo.PurItemChangeDateRecords.ToList();
            //List<PurchaseItem> _items = PurchaseItemRepo.PurchaseItems.ToList();

            foreach (PurchaseItem _item in PurchaseItems)
            {
                string _prNo, _qrNo, _poNo, _pocreateDate,_prcreDate;
                string _purchaseUser;
                #region region
                try
                {
                    if (_item.PurchaseRequestID > 0)
                    {
                        PurchaseRequest purRequest = _preqs.Where(p=>p.PurchaseRequestID==_item.PurchaseRequestID).FirstOrDefault()??new PurchaseRequest(); //PurchaseRequestRepo.GetByID(_item.PurchaseRequestID);
                        _prNo = purRequest.PurchaseRequestNumber;
                        _prcreDate = purRequest.CreateDate.ToString("yyyy-MM-dd HH:mm");
                    }
                    else
                    {
                        _prNo = "";
                        _prcreDate = "";
                    }
                }
                catch
                {
                    _prNo = "";
                    _prcreDate = "";
                }


                try
                {
                    if (_item.QuotationRequestID > 0)
                    {
                        _qrNo =(_quoreqs.Where(p => p.QuotationRequestID == _item.QuotationRequestID).FirstOrDefault() ?? new QuotationRequest()).QuotationNumber;//QuotationRequestRepo.GetByID(_item.QuotationRequestID).QuotationNumber;
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
                        _poNo = (_pos.Where(p => p.PurchaseOrderID == _item.PurchaseOrderID).FirstOrDefault() ?? new PurchaseOrder()).PurchaseOrderNumber;//PurchaseOrderRepo.QueryByID(_item.PurchaseOrderID).PurchaseOrderNumber;
                        _pocreateDate = PurchaseOrderRepo.QueryByID(_item.PurchaseOrderID).CreateDate.ToString("yyyy-MM-dd HH:mm");
                    }
                    else
                    {
                        _poNo = "";
                        _pocreateDate = "";
                    }
                }
                catch
                {
                    _poNo = "";
                    _pocreateDate = "";
                }


                try
                {
                    if (_item.PurchaseUserID > 0)
                    {
                        _purchaseUser = (_users.Where(u => u.UserID == _item.PurchaseUserID).FirstOrDefault() ?? new User()).FullName;//UserRepo.GetUserByID(_item.PurchaseUserID).FullName;
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
                    _purchaseType = (_types.Where(t => t.PurchaseTypeID == _item.PurchaseType && t.Enabled).FirstOrDefault() ?? new PurchaseType()).Name;//TypeRepo.QueryByID(_item.PurchaseType).Name;
                }
                catch
                {
                    _purchaseType = "";
                }

                string _requestUser;
                try
                {
                    if (_item.RequestUserID > 0)
                    {
                        _requestUser = (_users.Where(u => u.UserID == _item.RequestUserID).FirstOrDefault() ?? new User()).FullName;//UserRepo.GetUserByID(_item.RequestUserID).FullName;
                    }
                    else
                    {
                        _requestUser = "";
                    }
                }
                catch
                {
                    _requestUser = "";
                }
                #endregion
                string _htmlTitle = "";
                List<PurItemChangeDateRecord> _puritems = PurchaseItemRepo.GetPurItemChangeDateRecords(_itemCDs,_item.PurchaseItemID);
                if (_puritems.Count > 0)
                {
                    _htmlTitle = "<table><tr><th>调整后计划</th><th>调整人</th><th>调整时间</th></tr>";
                    foreach (var r in _puritems)
                    {
                        _htmlTitle = _htmlTitle + "<tr>";
                        _htmlTitle = _htmlTitle + "<td>" + ((r.PlanAJDate == new DateTime(1900, 1, 1) ? "-" : r.PlanAJDate.ToString("yyyy-MM-dd"))) + "</td>";
                        _htmlTitle = _htmlTitle + "<td>" + r.UserName.ToString() + "</td>";
                        _htmlTitle = _htmlTitle + "<td>" + ((r.CreDate == new DateTime(1900, 1, 1) ? "-" : r.CreDate.ToString("yyyy-MM-dd HH:mm"))) + "</td>";
                        _htmlTitle = _htmlTitle + "</tr>";
                    }
                    _htmlTitle = _htmlTitle + "</table>";
                }
                else
                {
                    _htmlTitle = "";
                }

                PurchaseItemGridRowModel _row = new PurchaseItemGridRowModel(_item, _prNo, _qrNo, _poNo, _purchaseUser, _purchaseType, _requestUser, _htmlTitle, _pocreateDate, _prcreDate);
                rows.Add(_row);
            }
            Records = PurchaseItems.Count();
        }
    }
}