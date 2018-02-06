using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MongoDBRESTful.Server
{
    public class TraceActionFilterAttribute : ActionFilterAttribute
    {
        public string Database { get; private set; }
        public string Document { get; private set; }

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            this.Database = filterContext.RequestContext.RouteData.Values["database"].ToString();
            this.Document = filterContext.RequestContext.RouteData.Values["document"].ToString();

            //filterContext.HttpContext.Trace.Write("(Logging Filter)Action Executing: " +
            //    filterContext.ActionDescriptor.ActionName);

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            //if (filterContext.Exception != null)
            //    filterContext.HttpContext.Trace.Write("(Logging Filter)Exception thrown");

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            var type = ((System.Net.Http.ObjectContent)filterContext.Response.Content).ObjectType;
            string str = string.Empty;
            if (type == typeof(BsonDocument))
            {
                var content = (System.Net.Http.ObjectContent<BsonDocument>)filterContext.Response.Content;
                str = ((BsonDocument)content.Value).ToJson<dynamic>(new MongoDB.Bson.IO.JsonWriterSettings()
                {
                    Indent = true,
                    OutputMode = MongoDB.Bson.IO.JsonOutputMode.Strict
                });
                var obj = Newtonsoft.Json.Linq.JObject.Parse(str);
                str = obj["_v"].ToString();
            }
            else if (type == typeof(IEnumerable<BsonDocument>))
            {
                var content = (System.Net.Http.ObjectContent<IEnumerable<BsonDocument>>)filterContext.Response.Content;
                str = ((IEnumerable<BsonDocument>)content.Value).ToJson<dynamic>(new MongoDB.Bson.IO.JsonWriterSettings()
                {
                    Indent = true,
                    OutputMode = MongoDB.Bson.IO.JsonOutputMode.Strict
                });
                var obj = Newtonsoft.Json.Linq.JObject.Parse(str);
                str = obj["_v"].ToString();
            }
            else if (type == typeof(List<BsonDocument>))
            {
                var content = (System.Net.Http.ObjectContent<List<BsonDocument>>)filterContext.Response.Content;
                str = ((List<BsonDocument>)content.Value).ToJson<dynamic>(new MongoDB.Bson.IO.JsonWriterSettings()
                {
                    Indent = true,
                    OutputMode = MongoDB.Bson.IO.JsonOutputMode.Strict
                });
                var obj = Newtonsoft.Json.Linq.JObject.Parse(str);
                str = obj["_v"].ToString();
            }

            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(str));
            writer.Flush();
            stream.Position = 0;
            //
            var c = new System.Net.Http.StreamContent(stream);
            c.Headers.Add("Content-Type", "application/json; charset=utf-8");

            filterContext.Response.Content = c;
            base.OnActionExecuted(filterContext);
        }
    }
}