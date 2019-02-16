using FMArslan.Web.Helper;
using FMArslan.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FMArslan.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(String language, String id)
        {
            if (String.IsNullOrEmpty(language) || MvcApplication.Languages.IndexOf(language) < 0)
            {
                language = MvcApplication.DefaultLanguage;
            }
            if (String.IsNullOrEmpty(id))
                id = MvcApplication.MainPage.getKey(language);

            PageModel page = NavigationHelper.Navigation.Pages.Where(x => x.Key != null && x.Key.Any(y=>y.Language!=null && y.Language.Equals(language) && y.Key.Trim().Equals(id))).FirstOrDefault();
            if (page == null)
            {
                page = new PageModel();
                page.FilePath = MvcApplication.ErrorPage;
                page.Key = new List<PageKey>();
                page.Title = "Error";
                page.Key.Add(new PageKey() { Key = "error", Language = language });
            }
            if (page.FullPage)
                return View(CustomHtmlHelper.GetPath(ContentType.PAGES, language + "/" + page.FilePath), new PageConfig(page, language, id));
            else
                return View("~/Content/pages/index.cshtml", new PageConfig(page, language, id));
        }
    }
}