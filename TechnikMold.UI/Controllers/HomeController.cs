using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoldManager.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //List<int> intList = new List<int> { 0 };
            //intList[1] = 10;
            return RedirectToAction("Index", "Project");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Data()
        {
            return View();
        }
        /// <summary>
        /// 异常处理页面
        /// </summary>
        /// <param name="controllername"></param>
        /// <param name="actionname"></param>
        /// <param name="exMessage"></param>
        /// <returns></returns>
        public ViewResult ErrorHandler(string controllername,string actionname,string exMessage)
        {
            ViewBag.ErrorController = controllername;
            ViewBag.ErrorAction = actionname;
            ViewBag.ErrorMessage = exMessage;
            return View();
        }
    }
}