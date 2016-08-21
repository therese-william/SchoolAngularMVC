using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

namespace SchoolClasses
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config) 
        {
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            config.Routes.MapHttpRoute(
                name: "ClassDetailsApi",
                routeTemplate: "api/v1/Classes/{ClassId}/Details",
                defaults: new { controller = "ClassDetails" }
                );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional}
                );
        }
    }
}