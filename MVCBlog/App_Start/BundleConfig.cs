using System.Web;
using System.Web.Optimization;

namespace MVCBlog
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css",
                      "~/Content/bootstrap.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datetimepicker").Include(
                     "~/Scripts/moment.js",
                     "~/Scripts/bootstrap-datetimepicker.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-datetimepicker").Include(
                     "~/Content/bootstrap-datetimepicker.min.css"
                     ));

            #region Design Bundles
            bundles.Add(new StyleBundle("~/Content/cssDesign").Include(
                   "~/Content/css/animate.css",
                   "~/Content/css/icomoon.css",
                   "~/Content/bootstrap.css",
                   "~/Content/css/flexslider.css",
                   "~/Content/css/owl.carousel.min.css",
                   "~/Content/css/owl.theme.default.min.css",
                   "~/Content/css/style.css"                 
                   ));

            bundles.Add(new ScriptBundle("~/bundles/jsDesign").Include(
                        "~/Scripts/modernizr-2.6.2.min.js",
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/jquery.easing.1.3.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/jquery.waypoints.min.js",
                        "~/Scripts/owl.carousel.min.js",
                        "~/Scripts/jquery.countTo.js",
                        "~/Scripts/jquery.flexslider-min.js",
                        "~/Scripts/notifications.js",
                        "~/Scripts/main.js"));
            #endregion
        }
    }
}
