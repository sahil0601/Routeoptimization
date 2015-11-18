using System.Web;
using System.Web.Optimization;

namespace RouteOptimizationUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/libs/jquery-2.0.2.min.js",
                        "~/Scripts/libs/jquery-ui-1.10.3.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     "~/Scripts/bootstrap/bootstrap.min.js",
                     "~/Scripts/notification/SmartNotification.min.js",
                     "~/Scripts/smartwidgets/jarvis.widget.min.js",
                     "~/Scripts/plugin/easy-pie-chart/jquery.easy-pie-chart.min.js",
                     "~/Scripts/plugin/sparkline/jquery.sparkline.min.js",
                     "~/Scripts/plugin/jquery-validate/jquery.validate.min.js",
                     "~/Scripts/plugin/masked-input/jquery.maskedinput.min.js",
                     "~/Scripts/plugin/select2/select2.min.js",
                     "~/Scripts/plugin/bootstrap-slider/bootstrap-slider.min.js",
                     "~/Scripts/plugin/msie-fix/jquery.mb.browser.min.js",
                     "~/Scripts/plugin/fastclick/fastclick.min.js",
                     "~/Scripts/demo.min.js",
                     "~/Scripts/app.min.js",
                     "~/Scripts/respond.js",
                     "~/Scripts/ONE_MDC.js"
                     ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/smartadmin-production.min.css",
                      "~/Content/smartadmin-skins.min.css",
                      "~/Content/demo..min.css",
                      "~/Content/gangnam.css"));
        }
    }
}
