using System;
using System.Collections.Generic;
using System.Text;

namespace MoocDownloader.Shared.Models.Base.Attributes
{
    public class CrawlerInfoAttribute : Attribute
    {
        private static int _indexGenerator = 0;
        public int Index { get; }
        public string Title { get; }
        public string WebsiteUrl { get; }
        public bool Implemented { get; }
        public CrawlerInfoAttribute(string title, string websiteUrl, int indexNumber = -1, bool implemented = true)
        {
            Title = title;
            WebsiteUrl = websiteUrl;
            Index = GenerateCurrentIndex(indexNumber);
            Implemented = implemented;
        }

        private int GenerateCurrentIndex(int indexNumber)
        {
            if (indexNumber < 0)
            {
                return _indexGenerator++;
            }
            else
            {
                _indexGenerator = indexNumber + 1;
                return indexNumber;
            }
        }
    }
}
