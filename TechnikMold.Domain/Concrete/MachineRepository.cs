using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Abstract;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Concrete
{
    public class MachineRepository:IMachineRepository
    {
        private EFDbContext _context = new EFDbContext();



        public IQueryable<Machine> Machines
        {
            get { 
                return _context.Machines; 
            }
        }

        public int Save(Machine Machine)
        {
            if (Machine.MachineID == 0)
            {
                _context.Machines.Add(Machine);
            }
            else
            {
                Machine _dbEntry = _context.Machines.Find(Machine.MachineID);
                if (_dbEntry != null)
                {
                    _dbEntry.Name = Machine.Name;
                    _dbEntry.IPAddress = Machine.IPAddress;
                    _dbEntry.System_3R = Machine.System_3R;
                    _dbEntry.Pallet = Machine.Pallet;
                    _dbEntry.SystemType = Machine.SystemType;
                }
            }
            _context.SaveChanges();
            return Machine.MachineID;
        }

        public Machine QueryByName(string Name)
        {
            Machine _dbEntry = _context.Machines.Where(m => m.Name == Name).FirstOrDefault();
            return _dbEntry;
        }




        public Machine QueryByID(int MachineID)
        {
            Machine _dbEntry = _context.Machines.Find(MachineID);
            return _dbEntry;
        }


        public int Delete(int MachineID)
        {
            try
            {
                Machine _dbEntry = _context.Machines.Find(MachineID);
                _dbEntry.Enabled = false;
                _context.SaveChanges();
                return _dbEntry.MachineID;
            }
            catch
            {
                return -1;
            }
            
            
        }
    }
}
