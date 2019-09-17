using System.Web;
using System.Web.Optimization;

namespace TwitterApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/html5shiv.min.js",
                         "~/Scripts/jquery-1.8.0.min.js",
                         "~/Scripts/jquery.validate-vsdoc.js",
                         "~/Scripts/jquery.validate.min.js",
                         "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/screen.css"));
        }
    }
}
