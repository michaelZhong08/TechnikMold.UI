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
    public class PartCodeRepository:IPartCodeRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<PartCode> PartCodes
        {
            get 
            {
                return _context.PartCodes;
            }
        }

        public int Save(PartCode PartCode)
        {
            if (PartCode.PartCodeID == 0)
            {
                _context.PartCodes.Add(PartCode);
            }
            else
            {
                PartCode _dbEntry = _context.PartCodes.Find(PartCode.PartCodeID);
                if (_dbEntry != null)
                {
                    _dbEntry.Name = PartCode.Name;
                    _dbEntry.ShortName = PartCode.ShortName;
                    _dbEntry.Code = PartCode.Code;
                }
            }
            _context.SaveChanges();
            return PartCode.PartCodeID;
        }

        public IQueryable<PartCode> QueryByName(string Name)
        {
            IQueryable<PartCode> _dbEntry = _context.PartCodes.Where(p => p.Name.Contains(Name));
            return _dbEntry;
        }
    }
}
