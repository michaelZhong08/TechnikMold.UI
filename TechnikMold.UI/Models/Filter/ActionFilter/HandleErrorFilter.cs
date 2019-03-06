using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechnikMold.UI.Models.Filter.ActionFilter
{
    public class HandleErrorFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            var ex = filterContext.Exception;
            string controller = filterContext.RouteData.Values["controller"].ToString();
            string action = filterContext.RouteData.Values["action"].ToString();
            #region 日志记录
            string logPath = filterContext.HttpContext.Server.MapPath("~/Log/") + "ErrorHandle_" + DateTime.Now.ToString("yyMMddHH") + ".txt";
            string _logStr=string.Format("控制器：{0}\r\n处理函数:{1}\r\n异常信息：{2}", controller, action, ex.ToString());
            Toolkits.WriteLog(logPath, _logStr);
            #endregion
            throw new HttpException(500, _logStr);
            //filterContext.Result = new RedirectResult(string.Format("/Home/ErrorHandler?controllername={0}&actionname={1}&exMessage={2}", controller, action, HttpUtility.UrlEncode(ex.Message)));
        }
    }
}