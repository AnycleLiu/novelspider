using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;

namespace NovelSpider.Core.Utils
{
    public class HtmlHelper
    {
        private HttpClient httpClient = new HttpClient();
        public HtmlHelper()
        {
        }
        
        public async Task<string> HttpHead(Uri uri)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Head, uri);
                var response = await httpClient.SendAsync(request);

                return ((int)response.StatusCode).ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine("http head request error. {0}", ex.ToString());
                return "500";
            }
        }

        public async Task<string> GetHtml(Uri url)
        {
            return await GetHtml(url.AbsoluteUri);
        }
        
        public async Task<string> GetHtml(string url)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.81 Safari/537.36";
            HttpWebResponse response = (HttpWebResponse)(await request.GetResponseAsync());
            Stream stream = response.GetResponseStream();

            StringBuilder sb = new StringBuilder();
            byte[] buffer = new byte[1024];
            int readCnt = 0;
            do
            {
                readCnt = stream.Read(buffer, 0, 1024);
                if (readCnt > 0)
                {
                    sb.Append(Encoding.UTF8.GetString(buffer, 0, readCnt));
                }
            } while (readCnt > 0);

            return sb.ToString();
        }
    }
}
