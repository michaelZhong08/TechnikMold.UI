using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Status
{
    public enum WarehouseRequestStatus
    {
        编辑 = -99, 
        待审批 = 0, 
        待领 = 1, 
        部分领料 = 2, 
        完成 = 100
    }
}
