using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnikSys.MoldManager.Domain.Status
{
    public enum PurchaseItemStatus
    {
        未提交 = -99,        //PR未提交
        取消 = -90,          //取消采购
        需求待审批 = 0,       //PR提交, 等待部门主管审批
        审批拒绝=5,           //PR审批不通过
        待询价 = 10,         //PR审批通过, 等待采购询价
        询价中=15,           //询价单发出
        待采购 = 20,         //采购询价完成，等待加入订单
        订单新建 = 30,      //进入订单
        订单待审批=31,       //采购提交订单，等待经理审批
        订单取消=33,         //采购取消订单
        订单审批拒绝=35,      //经理审批拒绝
        订单待发 = 40,       //经理审批通过，等待采购发布订单
        外发项待出库=45,      //外发加工件待出库
        待收货 = 50,         //订单发布，等待供应商收货
        部分收货 = 60,       //收到部分货物
        完成 = 100           //全部收货完成
    }
}
