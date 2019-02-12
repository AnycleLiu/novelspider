using NovelSpider.Core.Utils;
using System.Linq;
using System.Text;
using Xunit;

namespace NovelSpider.Facts.UtilsFacts
{
    public class AnalysisUtilsFact
    {
        private HtmlHelper htmlHelper = new HtmlHelper();
        private AnalysisUtil analysis;

        public AnalysisUtilsFact()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var html = htmlHelper.GetHtml("http://www.xxbiquge.com/0_999/").Result;

            analysis = new AnalysisUtil(html);
        }

        [Fact]
        public void GetBookNameFact()
        {
            Assert.Equal("鼎定星空", analysis.GetBookName());
        }

        [Fact]
        public void GetAuthorFact()
        {
            Assert.Equal("炎阳冬雪", analysis.GetAuthor());
        }

        [Fact]
        public void GetStatusFact()
        {
            Assert.NotNull(analysis.GetStatus());
        }

        [Fact]
        public void GetLastUpdateTimeFact()
        {
            Assert.NotNull(analysis.GetLastUpdateTime());
        }

        [Fact]
        public void GetDescriptionFact()
        {
            Assert.NotNull(analysis.GetDescription());
        }

        [Fact]
        public void GetCapturesFact()
        {
            var captures = analysis.GetCaptures();
            Assert.NotEmpty(captures);
            var first = captures.ElementAt(0);
            Assert.NotNull(first);
            Assert.Equal("/0_999/8243471.html", first.CapterUrl);
            Assert.Equal("第一章 星际联邦", first.Title);
        }
    }
}
