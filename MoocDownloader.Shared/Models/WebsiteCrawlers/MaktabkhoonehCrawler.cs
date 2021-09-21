using MoocDownloader.Shared.Models.Base;
using MoocDownloader.Shared.Models.Base.Attributes;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MoocDownloader.Shared.Models
{
    [CrawlerInfo("Maktabkhooneh", "https://maktabkhooneh.org")]
    public class MaktabkhoonehCrawler : CrawlerBase
    {
        private const string MaktabkhoonehUrl = "https://maktabkhooneh.com";
        public MaktabkhoonehCrawler(string username, string password) : base(MaktabkhoonehUrl, username, password)
        {

        }
        protected sealed override void Login()
        {
            if (IsLogin)
                return;

            var loginButton = Chrome.FindElementByCssSelector("button[type=submit]");
            loginButton.Click();
            var usernameInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[name=tessera]")));
            usernameInput.SendKeys(Username);
            var submitButton = Chrome.FindElementByCssSelector(".filler.js-check-active-user-form input[type=submit]");
            submitButton.Click();
            var passwordInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[name=password]")));
            passwordInput.SendKeys(Password);
            submitButton = Chrome.FindElementByCssSelector(".filler.js-login-authentication-nv-form input[type=submit]");
            submitButton.Click();
            Waiter.Until(ExpectedConditions.ElementExists(By.CssSelector(".navbar__signin a[href*=dashboard]")));
            IsLogin = true;
        }

        protected sealed override Queue<string> CrawlVideoUrls(Action<ProgressValue> progress, string coursePageLink, int? fromPage, int? toPage, CancellationToken stoppingToken = default)
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
            var pages = Chrome.FindElementsByCssSelector(".chapter__unit");
            var pagesQueue = new Queue<string>(pages.Select(x => x.GetAttribute("href")));
            return pagesQueue;
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
    }
}
