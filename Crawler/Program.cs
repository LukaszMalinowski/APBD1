using System;
using System.Net.Http;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
                throw new ArgumentNullException("Pass url in program argument");

            if (ArgumentInvalid(args[0]))
                throw new ArgumentException("Url not validated!");

            HttpClient client = new HttpClient();
            string htmlString = null;

            try
            {
                HttpResponseMessage message = client.GetAsync(args[0]).Result;

                htmlString = message.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd w czasue pobierania strony");
            }
            finally
            {
                client.Dispose();
            }
                        
            try
            {
                string[] emails = GetEmailsFromHtml(htmlString);

                foreach (string email in emails)
                {
                    Console.WriteLine(email);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static bool ArgumentInvalid(string url)
        {
            //regex validating url adress
            string pattern = @"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})";

            Regex regex = new Regex(pattern);

            if (regex.IsMatch(url))
                return false;

            return true;
        }

        static string[] GetEmailsFromHtml(string html)
        {
            //regex that finds email adress
            string pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

            Regex regex = new Regex(pattern);

            HashSet<string> emails = new HashSet<string>();

            foreach (Match match in regex.Matches(html))
            {
                emails.Add(match.Value);
            }

            if (emails.Count < 1)
                throw new Exception("Nie znaleziono adresów email");

            string[] returnArray = new string[emails.Count];

            emails.CopyTo(returnArray);

            return returnArray;
        }
    }
}