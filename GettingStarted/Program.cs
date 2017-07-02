using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GettingStarted
{
    class Program
    {
        static void Main(string[] args)
        {
            string uId = "762321270479102";
            string fields = "email,name,friends{email,name}";
            string accessToken = "EAACEdEose0cBAFJbUHVfJUZCoZBzQDghLmemnHyAw6PkMHO7Il3a9ztc9BWsvEQSyjPlPIuJH6aoXtdJgr6DRZBIJhFEb1nrEPWj5rriFClBUym2TdOQp1AELR2RElZBp3KKiMyowyZABDpJkzG1rZAsPI1RXbVSBAMudvZBo8ZBdAW7Cm4iGro2sd3BziSO9fbS45JAHZA8ibgZDZD";

            string linkRequest = string.Format("https://graph.facebook.com/v2.8/{0}?fields={1}&access_token={2}", uId, fields, accessToken);
            HttpWebRequest request = WebRequest.CreateHttp(linkRequest);

            var response = request.GetResponse();
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                string content = streamReader.ReadToEnd();

                var jsons = Json.JsonParser.FromJson(content);

                var parseeee = Json.JsonParser.Deserialize<AccountInfo>(content);
                var parseDynamic = Json.JsonParser.Deserialize(content);
            }
        }
    }

    class AccountInfo
    {
        public string Email { set; get; }
        
        public string Id { set; get; }

        public string Name { set; get; }

        public Dictionary<string, object> Friends { get; set; }
    }
}
