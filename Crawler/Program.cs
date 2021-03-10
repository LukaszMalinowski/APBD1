using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = client.GetAsync("https://blog.hubspot.com/marketing/professional-email-address").Result;

            string htmlString = message.Content.ReadAsStringAsync().Result;

            Console.WriteLine(htmlString);
        }
    }
}
