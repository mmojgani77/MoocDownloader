using MoocDownloader.Shared.Models.Base;
using MoocDownloader.Shared.Models.Base.Attributes;
using MoocDownloader.Shared.Models.DataTransferObjects;
using MoocDownloader.Shared.Models.Enum;
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
    [CrawlerInfo("Maktabkhooneh", "https://maktabkhooneh.org", indexNumber: 0, authenticationRequired: true, courseLinkRegexFormat: @"^http(s)?:\/\/(www.)?maktabkhooneh\.org\/course\/.*$")]
    public class MaktabkhoonehCrawler : CrawlerBase
    {
        private const string MaktabkhoonehUrl = "https://maktabkhooneh.com";
        public MaktabkhoonehCrawler(string username, string password, SupportedBrowsers supportedBrowser) : base(MaktabkhoonehUrl, username, password, supportedBrowser)
        {

        }

        protected sealed override void Login(CancellationToken stoppingToken)
        {
            var loginButton = Browser.FindElement(By.CssSelector("button[type=submit]"));
            loginButton.Click();
            var usernameInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[name=tessera]")), stoppingToken);
            usernameInput.SendKeys(Username);
            var submitButton = Browser.FindElement(By.CssSelector(".filler.js-check-active-user-form input[type=submit]"));
            submitButton.Click();
            var passwordInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[name=password]")), stoppingToken);
            passwordInput.SendKeys(Password);
            submitButton = Browser.FindElement(By.CssSelector(".filler.js-login-authentication-nv-form input[type=submit]"));
            submitButton.Click();
            Waiter.Until(ExpectedConditions.ElementExists(By.CssSelector(".navbar__signin a[href*=dashboard]")), stoppingToken);
        }

        protected override sealed Queue<string> ExtractAllCoursePagesFromCourseListPage(CancellationToken stoppingToken)
        {
            var pages = Browser.FindElements(By.CssSelector(".chapter__unit"));
            var pagesQueue = new Queue<string>(pages.Select(x => x.GetAttribute("href")));
            return pagesQueue;
        }

        protected override sealed List<string> ExtractEachCoursePageVideoUrls(CancellationToken stoppingToken)
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
