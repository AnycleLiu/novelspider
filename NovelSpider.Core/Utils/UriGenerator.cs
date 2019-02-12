using System;

namespace NovelSpider.Core.Utils
{
    public class UriGenerator
    {
        private Uri baseUri;
        private int curIndex;

        public int CurIndex
        {
            get { return curIndex; }
        }

        public UriGenerator(Uri baseUri, int initId = 1)
        {
            this.baseUri = baseUri;
            this.curIndex = initId;
        }

        public UriGenerator(string baseUri, int initId = 1) : this(new Uri(baseUri), initId) { }

        private string BuildUrlSeg(int id)
        {
            int prefix = (int)id / 1000;
            return string.Format("{0}_{1}", prefix, id);
        }

        public Uri NextUrl()
        {
            var seg = BuildUrlSeg(curIndex);
            var uri = new Uri(baseUri, seg);

            ++curIndex;

            return uri;
        }
    }
}
