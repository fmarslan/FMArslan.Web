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
        public static String Suffix = "";
        public static String ContentFolder = "~/Content";
        public static String MainPage = "main.cshtml";
        public static String ErrorPage = "/shared/error.cshtml";
        public static Boolean RedirectForSuffix = false;
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
            logger.Info("Web Application Starting...");
            MainPage = getConfiguration("mainPage", MainPage);
            logger.Info("Main Page        : " + MainPage);
            MainPage = getConfiguration("mainPage", ErrorPage);
            logger.Info("Error Page       : " + ErrorPage);
            Suffix = getConfiguration("suffix", Suffix);
            logger.Info("Suffix           : " + Suffix);
            Suffix = getConfiguration("contentFolder", ContentFolder);
            logger.Info("ContentFolder    : " + ContentFolder);
            RedirectForSuffix = getConfiguration("redirectForSuffix", false);
            logger.Info("Redirect Suffix  : " + RedirectForSuffix.ToString());

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

            
            logger.Info("Checked Configuration");
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            logger.Info("Filter Created.");
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            logger.Info("RouteTable Created.");
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            logger.Info("Bundles Created.");
            NavigationHelper.Init();
            logger.Info("Navigation Created.");
            

            logger.Info("Web Application Started.");            
        }
    }
}
