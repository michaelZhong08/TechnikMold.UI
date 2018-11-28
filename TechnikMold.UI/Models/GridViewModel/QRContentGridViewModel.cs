using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using MoldManager.WebUI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Abstract;

namespace MoldManager.WebUI.Models.GridViewModel
{
    public class QRContentGridViewModel
    {
        public List<QRContentGridRowModel> rows;
        public int Page;
        public int Total;
        public int Records;

        public QRContentGridViewModel(IEnumerable<PRContent> PRContents,string prMemo)
        {
            rows = new List<QRContentGridRowModel>();
            foreach (PRContent _prContent in PRContents)
            {
                rows.Add(new QRContentGridRowModel(_prContent, prMemo));
            }
            Page = 1;
            Total = PRContents.Count();
            Records = 500;
        }

        public QRContentGridViewModel(IEnumerable<QRContent> QRContents)
        {
            rows = new List<QRContentGridRowModel>();
            foreach (QRContent _qrContent in QRContents)
            {
                rows.Add(new QRContentGridRowModel(_qrContent));
            }
            Page = 1;
            Total = QRContents.Count();
            Records = 500;
        }
        public QRContentGridViewModel(IEnumerable<PurchaseItem> PurchaseItems,IPRContentRepository PRContentRepo)
        {
            rows = new List<QRContentGridRowModel>();
            foreach (PurchaseItem _item in PurchaseItems)
            {
                PRContent _prcontent = PRContentRepo.PRContents.Where(p => p.PurchaseItemID == _item.PurchaseItemID).FirstOrDefault();
                rows.Add(new QRContentGridRowModel(_item, _prcontent));
            }
            Page = 1;
            Total = PurchaseItems.Count();
            Records = 500;
        }
    }
}