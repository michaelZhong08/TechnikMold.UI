using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Status
{
    public enum WarehouseRequestStatus
    {
        拒绝 = -200,
        编辑 = -99, 
        审核退回=-90,
        待审核 = 0,
        审核中 = 5,
        待领 = 10,
        部分领料 = 20,
        完成 = 100
    }
}
