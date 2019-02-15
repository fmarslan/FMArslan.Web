using System.Web;
using System.Web.Optimization;

namespace FMArslan.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/resources/js").IncludeDirectory("~/content/scripts","*.js"));
            bundles.Add(new StyleBundle("~/resources/css").IncludeDirectory("~/content/styles","*.css"));
        }
    }
}
