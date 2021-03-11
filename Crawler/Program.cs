using System;
using System.Net.Http;
using System.Collections;
using System.Text.RegularExpressions;


namespace Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = client.GetAsync(args[0]).Result;

            string htmlString = message.Content.ReadAsStringAsync().Result;

            string[] emails = GetEmailsFromHtml(htmlString);

            foreach (string email in emails)
            {
                Console.WriteLine(email);
            }

            Console.ReadKey();
        }

        static string[] GetEmailsFromHtml(string html)
        {
            string pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

            Regex regex = new Regex(pattern);

            ArrayList emails = new ArrayList();

            foreach (Match match in regex.Matches(html))
            {
                emails.Add(match.Value);
            }

            return (string[]) emails.ToArray( typeof(string));
        }
    }
}