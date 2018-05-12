using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Status
{
    public enum PurchaseStatus
    {
        转入仓库 = -2,
        已经加入PO = -1,
        等待 = 0,
        询价 = 1,
        正在采购 = 2,
        采购结束 = 3,
        取消 = 4, 
    }
}
