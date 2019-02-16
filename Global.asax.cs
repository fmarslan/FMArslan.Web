using NLog;
using FMArslan.Web.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FMArslan.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
        public static String DefaultLanguage = "en";
        public static String ContentFolder = "~/Content";
        public static PageModel MainPage = null;
        public static String ErrorPage = "/shared/error.cshtml";
        public static List<String> Languages = new List<String>();

        private T getConfiguration<T>(String key, T defaultValue)
        {
            if (ConfigurationManager.AppSettings.Get(key)!=null)
            {
                String tmp  =   ConfigurationManager.AppSettings.Get(key);
                defaultValue = (T)Convert.ChangeType(tmp, typeof(T));
            }
            return defaultValue;
        }

        protected void Application_Start()
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            logger.Info("Filter Created.");
           
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            logger.Info("Bundles Created.");
            NavigationHelper.Init();
            logger.Info("Navigation Created.");
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            logger.Info("RouteTable Created.");

            logger.Info("Web Application Starting...");
            logger.Info("Main Page        : " + MainPage.FilePath);


            ContentFolder = getConfiguration("contentFolder", ContentFolder);
            logger.Info("ContentFolder    : " + ContentFolder);

            DefaultLanguage = getConfiguration("defaultLanguage", DefaultLanguage);
            logger.Info("Default Language : " + DefaultLanguage);
            String tmp = ConfigurationManager.AppSettings.Get("languages");
            if (String.IsNullOrEmpty(tmp))
            {
                Languages = new List<string>();
                Languages.Add("en");
            }
            else
            {
                Languages = tmp.Split(',').ToList();
            }
            logger.Info("Support Language : " + String.Join(", ", Languages.ToArray()));

            ErrorPage = getConfiguration("mainPage", ErrorPage);
            logger.Info("Error Page       : " + ErrorPage);
            logger.Info("Checked Configuration");
            logger.Info("Web Application Started.");            
        }
    }
}
