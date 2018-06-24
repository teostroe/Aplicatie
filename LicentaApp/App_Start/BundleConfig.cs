using System.Web;
using System.Web.Optimization;

namespace LicentaApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Bundles/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Bundles/Scripts/jquery.validate*"
                //"~/Bundles/Scripts/Custom/validation-overrides.js"
                ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Bundles/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/materialize").Include(
                  //"~/Bundles/Scripts/angular.min.js",
                  //"~/Bundles/Scripts/angular-animate.min.js",
                  //"~/Bundles/Scripts/angular-aria.min.js",
                  //"~/Bundles/Scripts/angular-messages.min.js",
                  //"~/Bundles/Scripts/angular-route.min.js",
                  //"~/Bundles/Scripts/angular-resource.min.js",
                  "~/Bundles/Scripts/materialize.js",
                  "~/Bundles/Scripts/fontawesome-all.js",
                  "~/Bundles/Scripts/Custom/materialize-configs.js",
                  "~/Bundles/Scripts/Custom/proj-methods.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Bundles/Content/materialize.css",
                      "~/Bundles/Content/fa-svg-with-js.css",
                      "~/Bundles/Content/Custom/materialize-overrides.css",
                      "~/Bundles/Content/Custom/site.css"));
        }
    }
}
