using System;
using System.Collections.Generic;
using System.Text;

namespace NovelSpider.Core
{
    public interface ICrawl
    {
        void Execute(string baseUri);
    }
}
