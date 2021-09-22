using MoocDownloader.Shared.Models.Base;
using MoocDownloader.Shared.Models.Base.Attributes;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MoocDownloader.Shared.Models.WebsiteCrawlers
{
    [CrawlerInfo("Alaa tv", "https://alaatv.com/", indexNumber: 3, authenticationRequired: false, courseLinkFormat: "^http(s)?:\\/\\/(www.)?alaatv.com\\/set\\/\\d*$")]
    public class AlaaTvCrawler : CrawlerBase
    {
        private const string Url = "https://alaatv.com/";
        public AlaaTvCrawler(string username, string password) : base(Url, username, password)
        {

        }

        protected override bool Login()
        {
            return true;
        }

        protected override Queue<string> ExtractAllCoursePagesFromCourseListPage()
        {
            ScrollToEndOfPage();
            var pages = Chrome.FindElementsByCssSelector(".a--list1-title a");
            var pagesQueue = new Queue<string>(pages.Select(x => x.GetAttribute("href")));
            return pagesQueue;
        }
        private void ScrollToEndOfPage()
        {
            string oldScy, newScy;
            do
            {
                oldScy = Chrome.ExecuteScript("return window.scrollY;").ToString();
                Chrome.ExecuteScript("window.scroll(0, document.body.scrollHeight);");
                Task.Delay(10).Wait();
                newScy = Chrome.ExecuteScript("return window.scrollY;").ToString();
            }
            while (oldScy != newScy);
        }

        protected override List<string> ExtractEachCoursePageVideoUrls()
        {
            var result = new List<string>();
            try
            {
                var videoSource = Chrome.FindElementsByCssSelector("source").FirstOrDefault();
                var src = videoSource?.GetAttribute("src");
                if (!string.IsNullOrWhiteSpace(src))
                {
                    result.Add(src);
                }
            }
            catch
            {

            }
            return result;
        }
    }
}
