using MoocDownloader.Shared.Models.Base;
using MoocDownloader.Shared.Models.Base.Attributes;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MoocDownloader.Shared.Models.WebsiteCrawlers
{
    [CrawlerInfo("Go to class", "https://gotoclass.ir", indexNumber: 1)]
    public class GotoClassCrawler : CrawlerBase
    {
        private const string Url = "https://dars.gotoclass.ir/login";
        public GotoClassCrawler(string username, string password) : base(Url, username, password)
        {

        }


        protected sealed override void Login()
        {
            var usernameInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[name=email]")));
            usernameInput.SendKeys(Username);
            var passwordInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[name=password]")));
            passwordInput.SendKeys(Password);
            var loginButton = Chrome.FindElementByCssSelector("button[type=submit]");
            loginButton.Click();
            Waiter.Until(ExpectedConditions.ElementExists(By.CssSelector(".nav-item a[href*=dashboard]")));
            IsLogin = true;
        }
        protected sealed override Queue<string> CrawlVideoUrls(Action<ProgressValue> progress, string coursePageLink, int? fromPage = null, int? toPage = null, CancellationToken stoppingToken = default)
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
            var pages = Chrome.FindElementsByCssSelector(".subsection-text");
            var pagesQueue = new Queue<string>(pages.Select(x => x.GetAttribute("href")));
            return pagesQueue;
        }

        private string GetVideoUrlOfPage(string pageLink)
        {
            try
            {
                Navigator.GoToUrl(pageLink);
                var videoSource = Chrome.FindElementsByCssSelector("video source[src*=mp4]").FirstOrDefault();
                var src = videoSource?.GetAttribute("src");
                if (string.IsNullOrWhiteSpace(src))
                    return null;
                var orgSrc = src.Substring(0, src.LastIndexOf('?'));
                return orgSrc;
            }
            catch
            {

            }
            return null;
        }


    }
}
