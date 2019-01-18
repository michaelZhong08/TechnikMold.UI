using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechnikSys.MoldManager.Domain.Status
{
    public enum CNCItemStatus
    {
        任务取消 = -201,//已发布任务 被删除
        CAM取消 = -200,//未发布任务 被删除(设定删除)
        CNC删除 = -100,
        未开始 = -99,
        暂停 = -1,

        备料 = 5,
        CNC加工中 = 10,
        返工 = 12,
        CNC结束 = 20,
        QC开始 = 30,
        EDM出库 = 50,
        EDM加工中 = 60,
        EDM结束 = 70,

        已入库 = 100,
        已升版 = 200
    }
}
