using System.Web;
using System.Web.Optimization;

namespace LMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                            "~/Scripts/jquery-{version}.js",
                            "~/Scripts/datatables/jquery.datatables.js",
                            "~/Scripts/datatables/datatables.bootstrap.js",
                            "~/Scripts/bootstrap.min.js",
                            "~/Scripts/bootbox.js",
                            "~/Scripts/typeahead.bundle.js",
                            "~/Scripts/toastr.js",
                            "~/Scripts/jquery.validate.*",
                            "~/Content/font-awesome/js/all.min.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                          "~/Content/bootstrap-lumen.min.css",
                          "~/Content/datatables/css/datatables.bootstrap.css",
                          "~/Content/typeahead.css",
                          "~/Content/font-awesome/css/all.min.css",
                          "~/Content/toastr.css",
                          "~/Content/tags.css",
                          "~/Content/site.css"
                      ));
        }
    }
}
