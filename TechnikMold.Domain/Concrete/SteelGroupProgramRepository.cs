
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;
using TechnikSys.MoldManager.Domain.Abstract;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class SteelGroupProgramRepository:ISteelGroupProgramRepository
    {
        private EFDbContext _context = new EFDbContext();

        public IQueryable<SteelGroupProgram> SteelGroupPrograms
        {
            get 
            { 
                return _context.SteelGroupPrograms; 
            }
        }


        public IEnumerable<SteelGroupProgram> QueryByID(int ID)
        {
            return _context.SteelGroupPrograms.Where(s => s.SteelGroupProgramID == ID);
        }

        public int Save(SteelGroupProgram GroupProgram)
        {
            SteelGroupProgram _dbEntry = null;
            bool _isNew = false;
            if (GroupProgram.SteelGroupProgramID == 0)
            {
                _dbEntry = QueryByGroupName(GroupProgram.GroupName, GroupProgram.NCID);
                if (_dbEntry == null)
                {
                    _isNew = true;
                    _context.SteelGroupPrograms.Add(GroupProgram);
                }
                else
                {
                    _dbEntry.NCID = GroupProgram.NCID;
                    _dbEntry.GroupName = GroupProgram.GroupName;
                    _dbEntry.Time = GroupProgram.Time;
                    _dbEntry.Enabled = GroupProgram.Enabled;
                }
                
            }
            else
            {
                _dbEntry = _context.SteelGroupPrograms.Find(GroupProgram.SteelGroupProgramID);
                if (_dbEntry != null)
                {
                    _dbEntry.NCID = GroupProgram.NCID;
                    _dbEntry.GroupName = GroupProgram.GroupName;
                    _dbEntry.Time = GroupProgram.Time;
                    _dbEntry.Enabled = GroupProgram.Enabled;
                }
            }
            _context.SaveChanges();
            return _isNew?GroupProgram.SteelGroupProgramID:_dbEntry.SteelGroupProgramID;
        }

        public SteelGroupProgram QueryByGroupName(string GroupName, int DrawingID)
        {
            return _context.SteelGroupPrograms.Where(g => g.GroupName == GroupName).Where(g => g.NCID == DrawingID).FirstOrDefault();
        }




        public IEnumerable<SteelGroupProgram> QueryByNCID(int NCID)
        {
            return _context.SteelGroupPrograms.Where(s => s.NCID == NCID);
        }
    }
}
