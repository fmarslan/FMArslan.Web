using FMArslan.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMArslan.Web.Model
{
    public class PageConfig
    {
        public PageModel Page { get; set; }
        public String URL { get; set; }
        public String Language { get; set; }
        public NavigationModel Navigation { get { return NavigationHelper.Navigation; } }

        public PageConfig(PageModel Page, String lang, String URL)
        {
            this.Page = Page;
            this.URL = URL;
            this.Language = lang;
        }
    }
}