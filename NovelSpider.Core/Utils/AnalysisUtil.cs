using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using NovelSpider.Core.Models;

namespace NovelSpider.Core.Utils
{
    public class AnalysisUtil
    {
        private HtmlDocument doc;
        private static Regex cnSymbolReg = new Regex(@"[\u3002\uff1b\uff0c\uff1a\u201c\u201d\uff08\uff09\u3001\uff1f\u300a\u300b]", RegexOptions.Compiled);
        public string Html { get; set; }

        public AnalysisUtil(string html)
        {
            this.Html = html;
            this.doc = new HtmlDocument();
            this.doc.LoadHtml(html);
        }

        public string GetBookName()
        {
            var infoEle = doc.GetElementbyId("info");
            var h1Ele = infoEle.Element("h1");

            return h1Ele.InnerHtml.Trim();
        }

        private string GetPValue(string text)
        {
            int index = text.IndexOf("：");
            if (index > 0)
            {
                var val = text.Substring(index + 1)
                    .Trim(new[] { ' ', ',' });
                return cnSymbolReg.Replace(val, string.Empty);
            }

            return string.Empty;
        }

        public string GetCategory()
        {
            var ele = doc.DocumentNode.SelectNodes("//div[@class='con_top']/a");
            return ele.ElementAt(1).InnerText;
        }

        public string GetCoverImgUrl()
        {
            var ele = doc.GetElementbyId("fmimg")
                .Element("img");
            return ele.Attributes["src"].Value;
        }

        public string GetAuthor()
        {
            var infoEle = doc.GetElementbyId("info");
            var pEles = infoEle.Elements("p");
            var text = pEles.ElementAt(0).InnerHtml;

            return GetPValue(text);
        }

        public string GetStatus()
        {
            var infoEle = doc.GetElementbyId("info");
            var pEles = infoEle.Elements("p");
            var text = pEles.ElementAt(1).InnerText;

            return GetPValue(text);
        }

        public DateTime? GetLastUpdateTime()
        {
            var infoEle = doc.GetElementbyId("info");
            var pEles = infoEle.Elements("p");
            var text = pEles.ElementAt(2).InnerText;

            var val = GetPValue(text);

            DateTime dt;
            if (DateTime.TryParse(val, out dt))
            {
                return dt;
            }
            return null;
        }

        public string GetDescription()
        {
            var introEle = doc.GetElementbyId("intro");
            return introEle.Elements("p").ElementAt(0).InnerText
                .Replace("各位书友要是觉", "")
                .Trim(new[] { '\r', '\n', ' ', '\t' });
        }

        public string GetCapterContent()
        {
            return doc.GetElementbyId("content").InnerHtml;
        }

        private long GetSrcSiteId(string href)
        {
            int index1 = href.LastIndexOf('/'),
                index2 = href.LastIndexOf('.');

            return Convert.ToInt64(href.Substring(index1 + 1, index2 - index1 - 1));
        }

        public IEnumerable<Capter> GetCaptures()
        {
            var listEle = doc.GetElementbyId("list");
            var items = listEle.SelectNodes("//dl/dd");

            return items.Select(x =>
            {
                var a = x.Element("a");
                var href = a.Attributes["href"].Value.Trim();
                var title = a.InnerText.Trim();

                return new Models.Capter()
                {
                    Title = title,
                    CapterUrl = href,
                    SrcSiteId = GetSrcSiteId(href)
                };
            });
        }
    }
}
