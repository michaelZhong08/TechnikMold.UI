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
    public class PartTypeRepository:IPartTypeRepository
    {
        public IQueryable<PartType> PartTypes
        {
            get { throw new NotImplementedException(); }
        }

        public int Save(PartType PartType)
        {
            throw new NotImplementedException();
        }

        public int Delete(int PartTypeID)
        {
            throw new NotImplementedException();
        }
    }
}
