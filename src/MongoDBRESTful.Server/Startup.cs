using Newtonsoft.Json;
using Owin;
using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;

namespace MongoDBRESTful.Server
{
    public class Startup
    {

        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{database}/{document}/{id}",
                defaults: new { controller = "Restful", id = RouteParameter.Optional }
            );

            config.Filters.Add(new TraceActionFilterAttribute());

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());


            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            appBuilder.UseWebApi(config);
        }
    }
}
