using NovelSpider.Core.Models;
using NovelSpider.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelSpider.Core.Impl
{
    public class CrawlTask : ICrawlTask
    {
        public CrawlContext Context { get; set; }

        public void Do()
        {
            Console.WriteLine($"抓取: {Context.Url.AbsoluteUri}");

            var htmlHelper = new HtmlHelper();
            var html = htmlHelper.GetHtml(Context.Url).Result;
            var analysis = new AnalysisUtil(html);
            if (string.IsNullOrWhiteSpace(html))
            {
                Console.WriteLine($"地址【{Context.Url.AbsoluteUri}】没有返回内容");
                return;
            }
            string authorName = analysis.GetAuthor();
            string categoryName = analysis.GetCategory();
            string bookName = analysis.GetBookName();
            using (var storage = new Storage())
            {
                Book book = storage.GetBook(bookName, authorName);
                var capters = analysis.GetCaptures().ToList();

                if (book != null)
                {
                    Capter lastCapter = storage.GetBookLastCapter(book.Id);

                    if (lastCapter != null)
                    {
                        capters = capters.Where(x => x.SrcSiteId > lastCapter.SrcSiteId).ToList();
                        if (capters.Count == 0)
                        {
                            Console.WriteLine($"{Context.Url.AbsoluteUri} 没有有效章节，跳过处理");
                            return;
                        }
                    }
                }

                if (capters.Count > 0)
                {
                    var maxDegreeOfParallelism = Convert.ToInt32(Configuration.config["CapterTaskNum"]);
                    Console.WriteLine($"开始抓取书籍【{bookName}】的章节，并行度：{maxDegreeOfParallelism}");

                    Parallel.ForEach(capters, new ParallelOptions() { MaxDegreeOfParallelism = maxDegreeOfParallelism },
                        (c) =>
                        {
                            var baseUrl = new Uri(string.Format("{0}://{1}:{2}", Context.Url.Scheme, Context.Url.Host, Context.Url.Port));
                            var uri = new Uri(baseUrl, c.CapterUrl);
                            try
                            {
                                html = htmlHelper.GetHtml(uri).Result;
                                var capterAnalysis = new AnalysisUtil(html);
                                c.Content = capterAnalysis.GetCapterContent();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("抓取章节 {0} 出错，{1}", uri.AbsoluteUri, ex.ToString());
                            }
                        });
                    Console.WriteLine($"【{bookName}】，章节抓取完毕");
                }

                var author = storage.CreateAuthorIfNoExists(authorName);
                var category = storage.CreateCategoryIfNoExists(categoryName);

                book = new Book()
                {
                    Author = author,
                    AuthorName = authorName,
                    Category = category,
                    CategoryName = categoryName,
                    Description = analysis.GetDescription(),
                    LastCrawTime = DateTime.Now,
                    Status = analysis.GetStatus(),
                    Name = analysis.GetBookName(),
                    LastUpdateTime = analysis.GetLastUpdateTime().Value,
                    SrcSiteUrl = Context.Url.AbsoluteUri,
                    CoverImgUrl = analysis.GetCoverImgUrl(),
                    Capters = capters.Where(x => !string.IsNullOrWhiteSpace(x.Content) && !x.Content.Contains("正在手打中")).ToList()
                };
                storage.Create(book);
            }
        }
    }
}
