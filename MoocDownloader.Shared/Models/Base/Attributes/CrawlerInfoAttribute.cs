using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MoocDownloader.Shared.Models.Base.Attributes
{
    public class CrawlerInfoAttribute : Attribute
    {
        private static int _indexGenerator = 0;
        public int Index { get; }
        public string Title { get; }
        public string WebsiteUrl { get; }
        public bool Implemented { get; }
        public bool AuthenticationRequired { get; }
        public Regex CourseLinkFormat { get; set; }
        public CrawlerInfoAttribute(string title, string websiteUrl, int indexNumber = -1, bool implemented = true, bool authenticationRequired = false, string courseLinkFormat = null)
        {
            Title = title;
            WebsiteUrl = websiteUrl;
            Index = GenerateCurrentIndex(indexNumber);
            Implemented = implemented;
            AuthenticationRequired = authenticationRequired;
            CourseLinkFormat = string.IsNullOrWhiteSpace(courseLinkFormat) ? null : new Regex(courseLinkFormat);
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
