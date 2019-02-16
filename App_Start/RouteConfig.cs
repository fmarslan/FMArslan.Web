using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing;
using System.Web.Routing;

namespace FMArslan.Web
{
    public class ConstraintForLanguage : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return MvcApplication.Languages.Contains(values[parameterName]);
        }
    }
    public class RouteConfig
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "{language}/{*id}",
                defaults: new { controller = "Home", action = "Index", language = MvcApplication.DefaultLanguage, id = UrlParameter.Optional }
            );
            var constraintsResolver = new DefaultInlineConstraintResolver();

            constraintsResolver.ConstraintMap.Add("language", typeof(ConstraintForLanguage));
            routes.MapMvcAttributeRoutes(constraintsResolver);

        }


    }
}
