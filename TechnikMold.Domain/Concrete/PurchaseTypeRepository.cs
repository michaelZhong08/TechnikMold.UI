using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;


namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class PurchaseTypeRepository:IPurchaseTypeRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IQueryable<PurchaseType> PurchaseTypes
        {
            get { return _context.PurchaseTypes.Where(p=>p.Enabled==true); }
        }


        public int Save(PurchaseType PurchaseType)
        {
            if (PurchaseType.PurchaseTypeID == 0)
            {
                PurchaseType.ShortName = "";
                PurchaseType.Enabled = true;
                _context.PurchaseTypes.Add(PurchaseType);
            }
            else
            {
                PurchaseType _dbEntry = _context.PurchaseTypes.Find(PurchaseType.PurchaseTypeID);
                if (_dbEntry != null)
                {
                    _dbEntry.Name = PurchaseType.Name;
                    _dbEntry.Name = PurchaseType.Name;
                    _dbEntry.ParentTypeID = PurchaseType.ParentTypeID;
                    _dbEntry.ShortName = "";
                    _dbEntry.TaskType = PurchaseType.TaskType;
                    _dbEntry.DefaultPeriod = PurchaseType.DefaultPeriod;
                    _dbEntry.Enabled = PurchaseType.Enabled;
                }
            }
            _context.SaveChanges();
            return PurchaseType.PurchaseTypeID;
        }

        public List<PurchaseType> QueryByParentName(string ParentName, bool ContainParent=true)
        {
            List<PurchaseType> _purchaseTypes= new List<PurchaseType>();
            PurchaseType ParentType = PurchaseTypes.Where(p => p.Name == ParentName).Where(p=>p.Enabled==true).FirstOrDefault();
            if (ContainParent)
            {

                _purchaseTypes.Add(ParentType);
            }

            _purchaseTypes.AddRange(PurchaseTypes.Where(p => p.ParentTypeID == ParentType.PurchaseTypeID).OrderBy(p=>p.Name));
            
            
            return _purchaseTypes;
        }


        public List<PurchaseType> PurchaseTypeTree(int PurchaseTypeID=0)
        {
            List<PurchaseType> _types = new List<PurchaseType>();

            if (PurchaseTypeID > 0)
            {
                _types.Add(QueryByID(PurchaseTypeID));
            }
            
            List<PurchaseType> Parent = _context.PurchaseTypes.Where(p=>p.Enabled==true)
                .Where(p => p.ParentTypeID == PurchaseTypeID).ToList<PurchaseType>();
            foreach (PurchaseType _type in Parent)
            {
                _types.AddRange(PurchaseTypeTree(_type.PurchaseTypeID));
            }
            return _types;
        }

        public PurchaseType QueryByID(int PurchaseTypeID)
        {
            PurchaseType _type = _context.PurchaseTypes.Find(PurchaseTypeID);
            return _type;
        }

        public PurchaseType QueryByName(string Name)
        {
            PurchaseType _type = _context.PurchaseTypes.Where(p => p.Name == Name)
                .Where(p=>p.Enabled==true).FirstOrDefault();
            return _type;
        }



        public IEnumerable<PurchaseType> QueryByParentID(int ParentID)
        {
            IEnumerable<PurchaseType> _types = _context.PurchaseTypes.Where(p => p.ParentTypeID == ParentID).Where(p=>p.Enabled==true);
            return _types;
        }








        public void DeletePurchaseType(int PurchaseTypeID)
        {
            PurchaseType _purchaseType = _context.PurchaseTypes.Find(PurchaseTypeID);
            if (_purchaseType != null)
            {
                _purchaseType.Enabled = false;
            }
            _context.SaveChanges();
        }
    }
}
