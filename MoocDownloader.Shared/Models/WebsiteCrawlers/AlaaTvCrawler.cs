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
        protected override Queue<string> CrawlVideoUrls(Action<ProgressValue> progress, string coursePageLink, int? fromPage = null, int? toPage = null, CancellationToken stoppingToken = default)
        {
            var pagesToCrawlQueue = GetCoursePagesQueue(coursePageLink);
            var videoUrls = new Queue<string>();
            int page = 1;
            int totalCount = pagesToCrawlQueue.Count;

            if (toPage.HasValue && fromPage.HasValue)
                totalCount = toPage.GetValueOrDefault() - fromPage.GetValueOrDefault() + 1;
            else if (toPage.HasValue)
                totalCount = toPage.GetValueOrDefault();
            else if (fromPage.HasValue)
                totalCount -= fromPage.GetValueOrDefault() - 1;

            int crawledCount = 0;
            while (pagesToCrawlQueue.TryDequeue(out string pageLink) && !stoppingToken.IsCancellationRequested)
            {
                if (fromPage.HasValue && page < fromPage)
                {
                    page++;
                    continue;
                }

                if (toPage.HasValue && page > toPage)
                {
                    break;
                }

                var videoLink = GetVideoUrlOfPage(pageLink);
                if (!string.IsNullOrWhiteSpace(videoLink))
                    videoUrls.Enqueue(videoLink);
                crawledCount++;
                progress?.Invoke(new ProgressValue { Value = crawledCount, TotalCount = totalCount });
                page++;
            }
            return videoUrls;
        }

        private Queue<string> GetCoursePagesQueue(string coursePageLink)
        {
            Navigator.GoToUrl(coursePageLink);
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

        private string GetVideoUrlOfPage(string pageLink)
        {
            try
            {
                Navigator.GoToUrl(pageLink);
                var videoSource = Chrome.FindElementsByCssSelector("source").FirstOrDefault();
                return videoSource?.GetAttribute("src");
            }
            catch
            {

            }
            return null;
        }

        protected override void Login()
        {
            IsLogin = true;
        }
    }
}
