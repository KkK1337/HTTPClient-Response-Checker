using System;
using System.IO;
using System.Threading.Tasks;

namespace HttpClientStatus
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Open File List that Contain URLs
            string path = "urls.txt";

            // Read All TXT File then Close
            //string readURLs = File.ReadAllText(path);
            List<string> lines = File.ReadLines(path).ToList();

            // Write in Console for Test

            foreach (string webURL in lines)
            {
                using var client = new HttpClient();

                try
                {
                // Get Response Code
                var result = await client.GetAsync(webURL);

                // Convert String to Integer
                var WebsiteStatusCode = (int)result.StatusCode;

                // Write Response Code
                //Console.WriteLine(WebsiteStatusCode);

                // Informational responses (100–199)
                // Successful responses(200–299)
                // Redirects(300–399)
                // Client errors(400–499)
                // Server errors(500–599)

                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Console.WriteLine(webURL + " is UP!");
                        using StreamWriter file = new("alive.txt", append: true);
                        await file.WriteLineAsync(webURL);
                    }
                    else
                    {
                        Console.WriteLine(webURL + " Unknown Status!");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(webURL + " is DOWN!");
                }
                finally
                {

                }

            }
        }
    }
}