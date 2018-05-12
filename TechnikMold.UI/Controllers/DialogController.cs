using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoldManager.WebUI.Controllers
{
    public class DialogController : Controller
    {
        // GET: Dialog
        public ActionResult MoldSelect()
        {
            return PartialView();
        }

        public ActionResult UserSelect()
        {
            return PartialView();
        }
    }
}