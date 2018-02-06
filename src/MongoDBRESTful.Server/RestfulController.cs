using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MongoDBRESTful.Server
{
    public class RestfulController : BaseController
    {
        [HttpOptions]
        public IEnumerable<BsonDocument> GetByBody([FromBody] JToken value)
        {
            var filterBsonDocument = BsonDocument.Parse(value.ToString());
            return Document().Find(filterBsonDocument).ToEnumerable();
        }

        //public IEnumerable<BsonDocument> Get([FromUri] string filter)
        //{
        //    var filterDecoded = HttpUtility.UrlDecode(filter);
        //    var filterBsonDocument = BsonDocument.Parse(filterDecoded);

        // return Document().Find(filterBsonDocument).ToEnumerable();

        // //NameValueCollection query = HttpUtility.ParseQueryString(Request.RequestUri.Query);

        // //if (query.Count == 0) // return Document().Find(new
        // FilterDefinitionBuilder<BsonDocument>().Empty).ToEnumerable(); //else //{ /* BsonDocument
        // b1 = new BsonDocument { new BsonElement("score", 44) }; */

        // //var tt = b1.ToJson(new MongoDB.Bson.IO.JsonWriterSettings() { Indent = false }); //var
        // ett = HttpUtility.UrlEncode(tt);

        // //BsonDocument b = new BsonDocument.p //{ // new BsonElement("score", 44), // new
        // BsonElement("nome", "jeferson") //};

        // //var ss = b.ToJson(); //var builder = Builders<BsonDocument>.Filter.to; //builder.for

        // //FilterDefinition<BsonDocument> f = FilterDefinition<BsonDocument>;

        // ////var filter = new FilterDefinitionBuilder<BsonDocument>(); //foreach (var item in
        // query) //{ // //f = builder.Eq("score", 44);
        //    //    var t = query[item.ToString()].GetType();
        //    //    f = builder.Eq(item.ToString(), (object)query[item.ToString()]);
        //    //    //filter.And(filter.Eq(item.ToString(), item.ToString()));
        //    //}
        //    //FindOptions o = new FindOptions();
        //}

        //public BsonDocument GetById(string id)
        //{
        //    // https://stackoverflow.com/questions/8233014/how-do-i-search-for-an-object-by-its-objectid-in-the-console
        //    var filter = new FilterDefinitionBuilder<BsonDocument>()
        //        .Eq("_id", ObjectId.Parse(id.ToString()));

        //    return Document().Find(filter).FirstOrDefault();
        //}

        //// POST api/values
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}