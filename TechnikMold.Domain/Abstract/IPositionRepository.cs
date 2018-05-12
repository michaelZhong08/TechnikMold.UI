using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IPositionRepository
    {
        IQueryable<Position> Positions{get;}

        int Save(Position Position);

        void Delete(int PositionID);

        Position QueryByID(int PositionID);

        IEnumerable<Position> QueryByName(string Keyword);
    }
}
