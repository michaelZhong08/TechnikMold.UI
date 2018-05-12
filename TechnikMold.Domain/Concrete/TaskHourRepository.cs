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
    public class TaskHourRepository:ITaskHourRepository
    {
        public IQueryable<TaskHour> TaskHours
        {
            get { throw new NotImplementedException(); }
        }

        public int Save(TaskHour TaskHour)
        {
            throw new NotImplementedException();
        }
    }
}
