using System;
using System.Collections.Generic;
using System.Text;

namespace NovelSpider.Core
{
    public interface ICrawlTask
    {
        CrawlContext Context { get; set; }

        void Do(); 
    }
}
