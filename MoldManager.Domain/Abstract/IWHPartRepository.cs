using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnikSys.MoldManager.Domain.Entity;

namespace TechnikSys.MoldManager.Domain.Abstract
{
    public interface IWHPartRepository
    {
        IQueryable<WHPart> WHParts { get; }
        int Save(WHPart model);
        int Delete(WHPart model);
        WHPart GetPart(string _partNum, int PartID = 0);
        string GetPartNum(string _stockType);
        List<WHPart> GetPartsByMold(string _mold);
        List<WHPart> GetPartsByType(string ParentType);
        WHPart GetwfTaskPart(int _taskID);
        string GetwfTaskPartNum(int _taskID);
        List<WHPart> GetwfWHPartByMoldNum(string _moldNum);
        string GetwfTaskPartNum(string MooldNum);
    }
}
