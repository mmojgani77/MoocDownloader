using MoocDownloader.Shared.Models.Base;
using MoocDownloader.Shared.Models.Base.Attributes;
using MoocDownloader.Shared.Models.Enum;
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
    [CrawlerInfo("Alaa tv", "https://alaatv.com/", indexNumber: 2, authenticationRequired: false, courseLinkRegexFormat: "^http(s)?:\\/\\/(www.)?alaatv.com\\/set\\/\\d*$")]
    public class AlaaTvCrawler : CrawlerBase
    {
        private const string Url = "https://alaatv.com/";
        public AlaaTvCrawler(string username, string password, SupportedBrowsers supportedBrowser) : base(Url, username, password,supportedBrowser)
        {

        }

        protected override bool Login()
        {
            return true;
        }

        protected override Queue<string> ExtractAllCoursePagesFromCourseListPage(CancellationToken stoppingToken)
        {
            ScrollToEndOfPage();
            var pages = Browser.FindElements(By.CssSelector(".a--list1-title a"));
            var pagesQueue = new Queue<string>(pages.Select(x => x.GetAttribute("href")));
            return pagesQueue;
        }
        private void ScrollToEndOfPage()
        {
            string oldScy, newScy;
            do
            {
                oldScy = Browser.ExecuteScript("return window.scrollY;").ToString();
                Browser.ExecuteScript("window.scroll(0, document.body.scrollHeight);");
                Task.Delay(10).Wait();
                newScy = Browser.ExecuteScript("return window.scrollY;").ToString();
            }
            while (oldScy != newScy);
        }

        protected override List<string> ExtractEachCoursePageVideoUrls(CancellationToken stoppingToken)
        {
            var result = new List<string>();
            try
            {
                var videoSource = Browser.FindElements(By.CssSelector("source")).FirstOrDefault();
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
