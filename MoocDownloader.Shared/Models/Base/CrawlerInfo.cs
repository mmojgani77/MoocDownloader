using MoocDownloader.Shared.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MoocDownloader.Shared.Models.Base
{
    public class CrawlerInfo
    {
        public int Index { get; private set; }
        public string Title { get; private set; }
        public string ShortName { get; }
        public string WebsiteUrl { get; private set; }
        public bool Implemented { get; private set; }
        public bool AuthenticationRequired { get; private set; }
        private Type CrawlerType { get; set; }
        private Regex CourseLinkFormat { get; set; }
        public CrawlerInfo(Type crawlerType, int index, string title, string websiteUrl, string courseLinkRegexFormat)
        {
            Index = index;
            Title = title;
            ShortName = title.ToLower().Replace(' ', '-');
            WebsiteUrl = websiteUrl;
            if (!string.IsNullOrWhiteSpace(courseLinkRegexFormat))
            {
                CourseLinkFormat = new Regex(courseLinkRegexFormat);
            }
            CrawlerType = crawlerType;
        }
        internal void SetImplementedFlag()
        {
            Implemented = true;
        }
        internal void SetAuthenticationRequiredFlag()
        {
            AuthenticationRequired = true;
        }

        internal CrawlerBase CreateInstanceOfCrawler(string username, string password, SupportedBrowsers supportedBrowser)
        {
            if (!AuthenticationRequired)
            {
                username = "";
                password = "";
            }
            var crawler = (CrawlerBase)Activator.CreateInstance(CrawlerType, new object[] { username, password, supportedBrowser });
            return crawler;
        }

        public bool IsUrlMatchToCrawler(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            if (CourseLinkFormat == null)
                return true;

            return CourseLinkFormat.IsMatch(url);
        }



    }
}
