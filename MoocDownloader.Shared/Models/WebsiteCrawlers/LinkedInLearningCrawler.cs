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
    [CrawlerInfo("Linkedin Learning", "https://www.linkedin.com/", 6, implemented: true, authenticationRequired: true, courseLinkRegexFormat: @"^http(s)?:\/\/(www.)?linkedin.com\/learning\/.*$")]
    public class LinkedInLearningCrawler : CrawlerBase
    {
        private const string LinkedinLoginUrl = "https://www.linkedin.com/login";

        public LinkedInLearningCrawler(string username, string password, SupportedBrowsers supportedBrowser) : base(LinkedinLoginUrl, username, password, supportedBrowser)
        {

        }
        protected override sealed Queue<string> ExtractAllCoursePagesFromCourseListPage(CancellationToken stoppingToken)
        {
            Waiter.Until(ExpectedConditions.ElementExists(By.CssSelector(".classroom-toc-section")), stoppingToken);
            var collapsed = Browser.FindElements(By.CssSelector(".classroom-toc-section--collapsed>h2>button"));

            foreach (var item in collapsed)
            {
                Browser.ExecuteScript("arguments[0].click()", item);
                Task.Delay(50, stoppingToken).Wait();
                stoppingToken.ThrowIfCancellationRequested();
            }

            var itemsLink = Browser.FindElements(By.CssSelector("a.classroom-toc-item__link"))
                            .Select(x => x.GetAttribute("href"))
                            .ToArray();

            return new Queue<string>(itemsLink);
        }

        protected override sealed List<string> ExtractEachCoursePageVideoUrls(CancellationToken stoppingToken)
        {
            var result = new List<string>();
            string browserUrl = Browser.Url;
            if (browserUrl.Contains("/quiz/", StringComparison.OrdinalIgnoreCase))
                return result;

            var oldTimeOut = Waiter.Timeout;
            Waiter.Timeout = TimeSpan.FromSeconds(20);
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var videotagWaited = Waiter.Until(ExpectedConditions.ElementIsVisible(By.TagName("video")), stoppingToken);
                    break;
                }
                catch
                {
                    Navigator.Refresh();
                }
            }
            Waiter.Timeout = oldTimeOut;
            string src;
            while (!stoppingToken.IsCancellationRequested)
            {
                var videoTag = Browser.FindElement(By.TagName("video"));
                src = videoTag.GetAttribute("src");
                if (!string.IsNullOrWhiteSpace(src))
                {
                    result.Add(src);
                    break;
                }
                Task.Delay(100, stoppingToken).Wait();
            }
            return result;
        }

        protected override sealed void Login(CancellationToken stoppingToken)
        {
            var usernameInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#username")), stoppingToken);
            usernameInput.Click();
            usernameInput.SendKeys(Username);
            var passwordInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#password")), stoppingToken);
            passwordInput.Click();
            passwordInput.SendKeys(Password);
            var loginButton = Browser.FindElement(By.CssSelector("button[type=submit]"));
            loginButton.Click();
            Waiter.Until(x => x.Url.Contains("/feed", StringComparison.OrdinalIgnoreCase), stoppingToken);
        }
    }
}
