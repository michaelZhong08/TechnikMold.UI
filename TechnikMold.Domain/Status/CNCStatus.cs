using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnikSys.MoldManager.Domain.Status
{
    public enum CNCStatus
    {
        未发布 = -99, 
        暂停 = -1,
        等待 = 0,
        正在加工 = 1,
        CNC结束 = 2,
        QC开始 = 3,
        已入库 = 4,
        返工 = 5,
        有条件利用 = 6, 
        已升版 = 7,
        外发 = 8,
        无需加工 = 9,
        在EDM部门 = 10,
        CNC删除 = 20,
        已销毁=100,
        CAM取消 = 200,
        任务取消 = 201

    }
}