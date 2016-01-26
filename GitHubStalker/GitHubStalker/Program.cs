using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace GitHubStalker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a username");
            string username = Console.ReadLine();

            WebClient wc = new WebClient();
            WebClient wc2 = new WebClient();
            wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            wc2.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            string json = wc.DownloadString("https://api.github.com/users/" + username);
            string json2 = wc2.DownloadString("https://api.github.com/users/" + username + "/repos");

            var o = JObject.Parse(json);
            JArray o2 = JArray.Parse(json2);

            Console.WriteLine(o["login"].ToString());
            Console.WriteLine("Name: " + o["name"].ToString());
            Console.WriteLine("URL: " + o["url"].ToString());
            Console.WriteLine("Followers: " + o["followers"].ToString());
            Console.WriteLine();
            Console.WriteLine("Repositories: " + o["public_repos"].ToString());

            for (int i = 0; i < o2.Count; i++)
            {
                var o3 = o2[i];
                Console.WriteLine("----" + o3["name"].ToString() + ", " + o3["stargazers_count"].ToString() + " stars " + o3["watchers"] + " watchers.");

                WebClient wc3 = new WebClient();
                wc3.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                string json3 = wc3.DownloadString("https://api.github.com/repos/" + username + "/" + o3["name"].ToString() + "/commits");
                JArray o4 = JArray.Parse(json3);


                for (int j = 0; j < o4.Count; j++)
                {
                    var o5 = o4[j];
                    var o6 = o5["commit"];
                    Console.WriteLine("Message: " + o6["message"]);
                }
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



                WebClient wc4 = new WebClient();
                wc4.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                string json4 = wc4.DownloadString("https://api.github.com/users/" + username + "/repos");

                JArray issuesCount = JArray.Parse(json4);
                string numberOfIssues = null;

                var x = issuesCount[i];
                numberOfIssues = x["open_issues_count"].ToString();
                Console.WriteLine("name" + "NumberOFIssues: " + numberOfIssues);
                int IssuesNumber = int.Parse(numberOfIssues);


                for (int d = 0; d < IssuesNumber; d++)
                {

                    WebClient wc5 = new WebClient();
                    wc5.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    int r = d + 1;
                    string json5 = wc5.DownloadString("https://api.github.com/repos/" + username + "/" + o3["name"].ToString() + "/issues/" + r);

                    JObject issuetitle = JObject.Parse(json5);
                    var title = issuetitle["title"].ToString();

                    Console.WriteLine(title);

                }
            }
            Console.ReadLine();
        }
    }
}
