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
    public class DepartmentRepository:IDepartmentRepository
    {
        private EFDbContext _context = new EFDbContext();
        public IQueryable<Department> Departments
        {
            get {
                return _context.Departments;
            }
        }

        public int Save(Department Department)
        {
            if (Department.DepartmentID == 0)
            {
                _context.Departments.Add(Department);
            }
            else
            {
                Department _dbEntry = _context.Departments.Find(Department.DepartmentID);
                if (_dbEntry != null)
                {
                    _dbEntry.Name = Department.Name;
                    _dbEntry.Enabled = Department.Enabled;
                }
            }
            _context.SaveChanges();
            return Department.DepartmentID;
        }

        public int Delete(int DepartmentID)
        {
            Department _dbEntry = _context.Departments.Find(DepartmentID);
            if (_dbEntry != null)
            {
                _dbEntry.Enabled = !_dbEntry.Enabled;
            }
            _context.SaveChanges();
            return DepartmentID;
        }


        public Department GetByID(int DepartmentID)
        {
            return _context.Departments.Find(DepartmentID);
        }
    }
}
