using System.Web;
using System.Web.Optimization;

namespace TechnikSys.MoldManager.UI
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region js Bundles
            bundles.Add(new ScriptBundle("~/bundles/Jquery").Include(
                        "~/Scripts/jquery-1.10.2.min.js",
                        "~/Scripts/jquery.form.js",
                        "~/Scripts/jquery-ui.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/Bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-treeview.js"));

            //jqGrid javascript
            bundles.Add(new ScriptBundle("~/bundles/JqGrid").Include(
                "~/Scripts/jquery.jqGrid.min.js",
                "~/Scripts/i18n/grid.locale-en.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Plugin").Include(
                "~/Scripts/jAlert.js",
                "~/Scripts/bootstrap-switch.js",
                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/bootstrap-datetimepicker.min.js",
                "~/Scripts/bootstrap-datetimepicker.zh-CN.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/CustomJS").Include(
                "~/Scripts/JqGridTemplate.js",
                "~/Scripts/MoldManager.js",
                "~/Scripts/TimeFormat.js",
                "~/Scripts/GlobalScrip.js"
                ));
            #endregion

            #region css Bundles
            bundles.Add(new StyleBundle("~/Content/JqGrid").Include(
                "~/Content/jquery-ui.css"
                ));
            bundles.Add(new StyleBundle("~/Content/Bootstrap").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-treeview.css"
                      ));
            //jqGrid stylesheet
            bundles.Add(new StyleBundle("~/Content/JqGrid").Include(
                "~/Content/ui.jqgrid-bootstrap.css"
                ));

            bundles.Add(new StyleBundle("~/Content/Plugin").Include(
                "~/Content/jAlert.css",
                "~/Content/bootstrap-switch.css",
                "~/Content/datepicker.css",
                "~/Content/bootstrap-datetimepicker.min.css",
                "~/Content/CusPluginStyle.css"
                ));
            bundles.Add(new StyleBundle("~/Content/CustomStyle").Include(
                "~/Content/Site.css",
                "~/Content/LayoutStyle.css"
                ));
            #endregion
        }
    }
}
