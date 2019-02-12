using NovelSpider.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovelSpider.Core
{
    public interface IBookCrawl
    {
        /// <summary>
        /// 从书的url分析数据信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Book Analysis(string url);
    }
}
