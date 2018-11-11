using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikMold.UI.Models.GridRowModel;
using TechnikSys.MoldManager.Domain.Abstract;

namespace TechnikMold.UI.Models.GridViewModel
{
    public class PurchaseAttachGridViewModel
    {
        public List<PurchaseAttachGridRowModel> rows = new List<PurchaseAttachGridRowModel>();
        public int Page;
        public int Total;
        public int Records;
        public PurchaseAttachGridViewModel(IQueryable<PurchaseItem> _models, IAttachFileInfoRepository _attachFileInfoRepository)
        {
            foreach (var p in _models)
            {
                AttachFileInfo _file = _attachFileInfoRepository.GetAttachByObj(p.AttachObjID, "PurchaseItems").FirstOrDefault() ?? new AttachFileInfo();
                rows.Add(new PurchaseAttachGridRowModel(p, _file));
            }
        }
    }
}