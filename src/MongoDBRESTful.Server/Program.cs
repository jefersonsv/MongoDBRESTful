using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBRESTful.Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9004/";

            // Start OWIN host
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values
                HttpClient client = new HttpClient();

                var response = client.GetAsync(baseAddress + "low-price-clothes/last-search/59831a64766c3ea6f4f99e42").Result;

                Console.WriteLine(response);
                var r = response.Content.ReadAsStringAsync();
                Console.WriteLine(r.Result);
                Console.ReadLine();
            }
        }
    }
}