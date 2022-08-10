using MoocDownloader.Shared.Models.Base;
using MoocDownloader.Shared.Models.Base.Attributes;
using MoocDownloader.Shared.Models.Enum;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        protected override Queue<string> ExtractAllCoursePagesFromCourseListPage()
        {
            Waiter.Until(ExpectedConditions.ElementExists(By.CssSelector(".classroom-toc-section")));
            var itemsLink = Browser.FindElements(By.CssSelector("a.classroom-toc-item__link"))
                            .Select(x => x.GetAttribute("href"))
                            .ToArray();

            return new Queue<string>(itemsLink);
        }

        protected override List<string> ExtractEachCoursePageVideoUrls()
        {
            string browserUrl = Browser.Url;
            if (browserUrl.Contains("/quiz/", StringComparison.OrdinalIgnoreCase))
                return new List<string>();

            var oldTimeOut = Waiter.Timeout;
            Waiter.Timeout = TimeSpan.FromSeconds(20);
            while (true)
            {
                try
                {
                    var videotagWaited = Waiter.Until(ExpectedConditions.ElementIsVisible(By.TagName("video")));
                    break;
                }
                catch
                {
                    Navigator.Refresh();
                }
            }
            Waiter.Timeout = oldTimeOut;
            string src;
            while (true)
            {
                src = null;
                var videoTag = Browser.FindElement(By.TagName("video"));
                src = videoTag.GetAttribute("src");
                if (!string.IsNullOrWhiteSpace(src))
                    break;
                Task.Delay(100).Wait();
            }
            var result = new List<string>()
            {
                src
            };
            return result;
        }

        protected override bool Login()
        {
            try
            {
                var usernameInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#username")));
                usernameInput.Click();
                usernameInput.SendKeys(Username);
                var passwordInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#password")));
                passwordInput.Click();
                passwordInput.SendKeys(Password);
                var loginButton = Browser.FindElement(By.CssSelector("button[type=submit]"));
                loginButton.Click();
                Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".global-nav__primary-items")));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
