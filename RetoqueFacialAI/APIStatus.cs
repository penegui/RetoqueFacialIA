using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace RetoqueFacialAI
{
    public class APIStatus
    {
        public static string RequestAPIRetoqueFacialStatus(string nomeUpload)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://access.bgeraser.com:6708/status/{0}", nomeUpload));

            //var proxyObject = new WebProxy("http://127.0.0.1:8080");
            //request.Proxy = proxyObject;

            request.Headers = Headers();

            request.Method = "GET";            
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36";
            //request.Credentials = CredentialCache.DefaultCredentials;
            
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            var streamDados = response.GetResponseStream();
            StreamReader reader = new StreamReader(streamDados);
            object objResponse = reader.ReadToEnd();
            
            Console.WriteLine(objResponse);

            return objResponse.ToString();
        }

        public static WebHeaderCollection Headers()
        {
            var headers = new WebHeaderCollection();

            headers["sec-ch-ua"] = "\"Chromium\"; v = \"89\", \";Not A Brand\"; v = \"99\"";
            headers["Accept"] = "*/*";
            headers["sec-ch-ua-mobile"] = "?0";
            headers["Origin"] = "https://imglarger.com";
            headers["Sec-Fetch-Site"] = "cross-site";
            headers["Sec-Fetch-Mode"] = "cors";
            headers["Sec-Fetch-Dest"] = "empty";
            headers["Referer"] = "https://imglarger.com/";
            headers["Accept-Encoding"] = "gzip, deflate";
            headers["Accept-Language"] = "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7";

            return headers;
        }
    }
}
