using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class SteelProgramRepository:ISteelProgramRepository
    {
        private EFDbContext _context = new EFDbContext();



        public IQueryable<SteelProgram> SteelPrograms
        {
            get 
            { 
                return _context.SteelPrograms;
            }
        }

        //public IEnumerable<SteelProgram> QueryByID(int ID)
        //{
        //    return _context.SteelPrograms.Where(s => s.SteelProgramID == ID);
        //}

        public int Save(SteelProgram Program)
        {
            SteelProgram _dbEntry = null;
            //bool _isNew = false;
            if (Program.SteelProgramID == 0)
            {
                _dbEntry = _context.SteelPrograms
                    .Where(p => p.ProgramName == Program.ProgramName)
                    .Where(p => p.GroupID == Program.GroupID).FirstOrDefault();
                if (_dbEntry == null)
                {
                    _context.SteelPrograms.Add(Program);
                }
                else
                {
                    _dbEntry.GroupID = Program.GroupID;
                    _dbEntry.ProgramName = Program.ProgramName;
                    _dbEntry.FileName = Program.FileName;
                    _dbEntry.ToolName = Program.ToolName;
                    _dbEntry.Time = Program.Time;
                    _dbEntry.Depth = Program.Depth;
                    _dbEntry.HaveFile = Program.HaveFile;
                    _dbEntry.Sequence = Program.Sequence;
                    _dbEntry.Enabled = Program.Enabled;
                }
                
            }
            else
            {
                _dbEntry = _context.SteelPrograms.Find(Program.SteelProgramID);
                if (_dbEntry != null)
                {
                    
                    _dbEntry.GroupID = Program.GroupID;
                    _dbEntry.ProgramName = Program.ProgramName;
                    _dbEntry.FileName = Program.FileName;
                    _dbEntry.ToolName = Program.ToolName;
                    _dbEntry.Time = Program.Time;
                    _dbEntry.Depth = Program.Depth;
                    _dbEntry.HaveFile = Program.HaveFile;
                    _dbEntry.Sequence = Program.Sequence;
                    _dbEntry.Enabled = Program.Enabled;
                }
            }
            _context.SaveChanges();
            return Program.SteelProgramID;
        }


        public IEnumerable<SteelProgram> QueryByGroupID(int GroupID)
        {
            return _context.SteelPrograms.Where(s => s.GroupID == GroupID);
        }


        public SteelProgram QueryByID(int ID)
        {
            return _context.SteelPrograms.Find(ID);
        }
    }
}
