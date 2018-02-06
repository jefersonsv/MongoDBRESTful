using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MongoDBRESTful.Server
{
    public abstract class BaseController : ApiController
    {
        protected MongoClient Client { get; }

        public BaseController()
        {
            string cs = ConfigurationManager.ConnectionStrings["MONGO_DB"].ConnectionString;
            Client = new MongoClient(cs);
        }

        protected IMongoCollection<BsonDocument> Document()
        {
            var database = this.ControllerContext.RouteData.Values["database"] as string;
            var document = this.ControllerContext.RouteData.Values["document"] as string;

            return Client.GetDatabase(database).GetCollection<BsonDocument>(document);
        }
    }
}
