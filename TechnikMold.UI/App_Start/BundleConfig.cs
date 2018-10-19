﻿using System.Web;
using System.Web.Optimization;

namespace TechnikSys.MoldManager.UI
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-treeview.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                      "~/Content/bootstrap-treeview.css"));
            //jqGrid stylesheet
            bundles.Add(new StyleBundle("~/Content/jqgrid").Include(
                "~/Content/ui.jqgrid-bootstrap.css"));

            //jqGrid javascript
            bundles.Add(new ScriptBundle("~/bundles/jqgrid").Include(
                "~/Scripts/jquery.jqGrid.min.js",
                "~/Scripts/i18n/grid.locale-en.js"));

            //Script file contains all grid templates
            bundles.Add(new ScriptBundle("~/bundles/GridTemplate").Include(
                "~/Scripts/JqGridTemplate.js"));
        }
    }
}
