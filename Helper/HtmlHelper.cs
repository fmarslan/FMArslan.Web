using FMArslan.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FMArslan.Web.Helper
{
    public enum ContentType
    {
        STYLES, SCRIPTS, PAGES, IMAGES, OTHER
    }
    public static class CustomHtmlHelper
    {
        public static String GetPath(ContentType type, String path)
        {
            if (type == ContentType.OTHER)
                return String.Format("{0}/{1}", MvcApplication.ContentFolder, path);
            else
                return String.Format("{0}/{1}/{2}", MvcApplication.ContentFolder, type.ToString().ToLower(), path);
        }

        public static HtmlString PartialViewWithCheck(this HtmlHelper html, String name, PageConfig model)
        {
            var controllerContext = html.ViewContext.Controller.ControllerContext;
            //var result = ViewEngines.Engines.FindView(controllerContext, name, null);
            var result = ViewEngines.Engines.FindPartialView(controllerContext, name);
            if (result != null)
                return html.Partial(CustomHtmlHelper.GetPath(ContentType.PAGES, model.Language + "/" + name), model);
            else
                return html.Partial(CustomHtmlHelper.GetPath(ContentType.PAGES, model.Language + "/shared/error"), model);
        }
    }
}