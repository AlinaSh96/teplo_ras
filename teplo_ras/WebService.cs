using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
//using HtmlAgilityPack;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace teplo_ras
{
    class WebService
    {
        public static string GetResponse(string url)
        {
            string str = null;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.AllowAutoRedirect = true;
            //request.Referer = "";
            //request.UserAgent =
            //    "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.75 Safari/537.36";
            //request.Headers.Set(HttpRequestHeader.Cookie, "");
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    str = streamReader.ReadToEnd();
                  
                }
                response.Close();
            }
            return str;
        }
    }
}
