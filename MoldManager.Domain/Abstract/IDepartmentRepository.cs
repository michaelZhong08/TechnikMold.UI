
/*
 * Create By:lechun1
 * 
 * Description: public interface for repository of TechnikSys.MoldManager.Domain.Entity.Department 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IDepartmentRepository
    {
        IQueryable<Department> Departments { get; }

        int Save(Department Department);

        int Delete(int DepartmentID);

        Department GetByID(int DepartmentID);
    }
}
