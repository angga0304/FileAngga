using System.Web;
using System.Web.Optimization;

namespace ResidentsDuesApp
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/scripts/bootstrap-datepicker.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/scripts/jquery-1.7.1.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/Scripts/jquery-ui-1.8.20.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryuival").Include(
                "~/Scripts/jquery.validate.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                "~/scripts/bootstrap.min.js"));
            bundles.Add(new ScriptBundle("~/Content/bootstrapcss").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/bootstrap-responsive.min.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css",
                        "~/Content/jquery-ui.css"));
        }
    }
}