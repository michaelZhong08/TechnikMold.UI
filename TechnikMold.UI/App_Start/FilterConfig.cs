using System.Web;
using System.Web.Mvc;
using TechnikMold.UI.Models.Filter.ActionFilter;

namespace TechnikSys.MoldManager.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new MoldSysActionFilterAttribute());
            filters.Add(new HandleErrorFilter());
        }
    }
}
