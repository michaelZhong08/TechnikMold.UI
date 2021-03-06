﻿using TechnikSys.WebUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StackExchange.Profiling;

namespace TechnikSys.MoldManager.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            MvcHandler.DisableMvcResponseHeader = true;
            #region 清理视图引擎 只保留 Razor Engine
            ViewEngines.Engines.Clear(); //clear all engines
            ViewEngines.Engines.Add(new RazorViewEngine());
            #endregion
            #region 过滤器、路由、静态资源压缩 注册
            //AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            #endregion
            #region 自定义IOC控制器工厂
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            #endregion
            #region 监测EF性能
            StackExchange.Profiling.EntityFramework6.MiniProfilerEF6.Initialize();
            #endregion
        }
        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)//这里是允许本地访问启动监控,可不写
            {
                MiniProfiler.Start();

            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            #region Form验证逻辑
            //HttpApplication app = (HttpApplication)sender;
            //MoldSysPrincipal<UserPrincipal>.TrySetUserInfo(app.Context);
            #endregion
        }
    }
}
