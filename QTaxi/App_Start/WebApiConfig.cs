using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace QTaxi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var appJsonType = config.Formatters.JsonFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/json");
            config.Formatters.JsonFormatter.SupportedMediaTypes.Remove(appJsonType);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            
        }
    }
}
