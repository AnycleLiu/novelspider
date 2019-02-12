using NovelSpider.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovelSpider.Core.Impl
{
    public class Crawl : ICrawl
    {
        private static object _lockObj = new object();

        private static volatile Crawl _instance;

        public static Crawl Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new Crawl();
                        }
                    }
                }

                return _instance;
            }
        }

        private Crawl() { }

        public void Execute(string baseUri)
        {
            int minId = Convert.ToInt32(Configuration.config["IdRange:Min"]),
                maxId = Convert.ToInt32(Configuration.config["IdRange:Max"]);

            var uriGenerator = new UriGenerator(baseUri, minId);
            var htmlHelper = new HtmlHelper();

            Console.WriteLine("开始枚举书籍地址，并加入CrawQueue....");

            do
            {
                if (uriGenerator.CurIndex > maxId) break;

                var uri = uriGenerator.NextUrl();
                var statusCode = htmlHelper.HttpHead(uri).Result;

                if (statusCode != "200")
                {
                    Console.WriteLine(string.Format("{0} response {1}", uri.AbsoluteUri, statusCode));
                    continue;
                }

                CrawlQueue.Instance.EnterTask(new CrawlTask()
                {
                    Context = new CrawlContext()
                    {
                        Url = uri
                    }
                });

            } while (true);

            Console.WriteLine("书籍地址枚举完毕，一共尝试{0}个URL", uriGenerator.CurIndex - 1);
        }
    }
}
