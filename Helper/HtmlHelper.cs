using FMArslan.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FMArslan.Web.Helper
{

    public static class CustomHtmlHelper
    {
        public static String GetPath( String path)
        {
                return String.Format("{0}/pages/{1}", MvcApplication.ContentFolder, path);
        }

        public static HtmlString PartialViewWithCheck(this HtmlHelper html, String name, PageConfig model)
        {
            var controllerContext = html.ViewContext.Controller.ControllerContext;
            var result = ViewEngines.Engines.FindPartialView(controllerContext, name);
            if (result != null)
                return html.Partial(CustomHtmlHelper.GetPath(model.Language + "/" + name), model);
            else
                return html.Partial(CustomHtmlHelper.GetPath(model.Language + "/shared/error"), model);
        }
    }
}