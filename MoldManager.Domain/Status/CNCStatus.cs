using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnikSys.MoldManager.Domain.Status
{
    public enum CNCStatus
    {
        任务取消 = -201,
        已销毁 = -100,
        CNC删除 = -20,
        等待中 = 2,


        无需加工 = -6,
        在EDM部门 = -5,
        返工 = 12,
        CNC结束 = 20,
        已入库 = 25,
        QC开始 = 30,

        CAM取消 = -200,
        未发布 = -99,
        暂停 = -1,
        等待 = 0,
        已接收 = 1,
        正在加工 = 10,
        外发 = 11,
        完成 = 100,
        已升版 = 200
    }
}