/*
 * Create By:lechun1
 * 
 * Description:
 * 
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class ELEItemRepository:IEleItemRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<EleItem> EleItems
        {
            get { return _context.EleItems; }
        }

        public int Save(EleItem EleItem)
        {
            if (EleItem.ELEItemID == 0)
            {
                _context.EleItems.Add(EleItem);
            }
            else
            {
                EleItem _dbEntry = _context.EleItems.Find(EleItem.ELEItemID);
                _dbEntry.TaskID = EleItem.TaskID;
                _dbEntry.EDMItemID = EleItem.EDMItemID;
                _dbEntry.LabelName = EleItem.LabelName;
                _dbEntry.Raw = EleItem.Raw;
                _dbEntry.Ready = EleItem.Ready;
                _dbEntry.Required = EleItem.Required;
                _dbEntry.PartPosition = EleItem.PartPosition;
                _dbEntry.Finished = EleItem.Finished;
            }
            _context.SaveChanges();
            return EleItem.ELEItemID;
        }

        public IEnumerable<EleItem> QueryByTaskID(int TaskID)
        {
            IEnumerable<EleItem> _items = _context.EleItems.Where(e => e.TaskID == TaskID);
            return _items;
        }

        public IEnumerable<EleItem> QueryByEDMItemID(int EDMItemID)
        {
            IEnumerable<EleItem> _items = _context.EleItems.Where(e => e.EDMItemID == EDMItemID);
            return _items;
        }

        public EleItem GetByID(int EleItemID)
        {
            EleItem _item = _context.EleItems.Find(EleItemID);
            return _item;
        }
    }
}
