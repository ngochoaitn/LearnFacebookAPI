using Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    /// <summary>
    /// Lấy access token ngắn hạn và dài hạn
    /// </summary>
    class Program
    {
        static string appID = "485526955122572";
        static string appSecret = "f2c13639e9d03bef44e4311b36c33edd";
        static string redirectUrl = "https://www.facebook.com/connect/login_success.html";//Url này phải được cho phép trong ứng dụng trên facebook
        

        static void Main(string[] args)
        {
            //redirectUrl = "https://fb.com/";
            string link_MaNganHan = string.Format("https://www.facebook.com/v2.9/dialog/oauth?client_id={0}&redirect_uri={1}&response_type=token", appID, redirectUrl);
            Process.Start(link_MaNganHan);

            Console.Write("access token = ");
            string access_tokenNganHan = Console.ReadLine();

            string maDaiHan = MaDaiHan(access_tokenNganHan);
            Console.WriteLine("Ma dai han: {0}",maDaiHan);
            Console.WriteLine("Lam moi ma dai han: {0}", GiaHanMaDaiHan(maDaiHan));

            Console.ReadLine();
        }

        static string MaDaiHan(string manganhan)
        {
            string link = string.Format(@"https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token&client_id={0}&client_secret={1}&fb_exchange_token={2}", appID, appSecret, manganhan);
            HttpWebRequest request = WebRequest.CreateHttp(link);
            var response = request.GetResponse();
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                string content = streamReader.ReadToEnd();
                content = (string)Json.JsonParser.FromJson(content)["access_token"];
                return content;
            }
        }

        static string GiaHanMaDaiHan(string madaihan)
        {
            string linkGetCode = string.Format("https://graph.facebook.com/oauth/client_code?access_token={0}&client_secret={1}&redirect_uri={2}&client_id={3}",madaihan,appSecret,redirectUrl,appID);
            HttpWebRequest request = WebRequest.CreateHttp(linkGetCode);
            var response = request.GetResponse();
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                string code = streamReader.ReadToEnd();
                code = (string)Json.JsonParser.FromJson(code)["code"];

                string linkGetAccessToken = string.Format("https://graph.facebook.com/oauth/access_token?code={0}&client_id={1}&redirect_uri={2}", code,appID,redirectUrl);
                request = WebRequest.CreateHttp(linkGetAccessToken);
                response = request.GetResponse();
                using (StreamReader str = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string accessToken = str.ReadToEnd();
                    accessToken = (string)Json.JsonParser.FromJson(accessToken)["access_token"];
                    return accessToken;
                }
            }

            
        }
    }
}
