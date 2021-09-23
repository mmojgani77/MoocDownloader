using MoocDownloader.Shared.Models.Base;
using MoocDownloader.Shared.Models.Base.Attributes;
using MoocDownloader.Shared.Models.DataTransferObjects;
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
        public MaktabkhoonehCrawler(string username, string password) : base(MaktabkhoonehUrl, username, password)
        {

        }

        protected sealed override bool Login()
        {
            try
            {
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
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected override Queue<string> ExtractAllCoursePagesFromCourseListPage()
        {
            var pages = Chrome.FindElementsByCssSelector(".chapter__unit");
            var pagesQueue = new Queue<string>(pages.Select(x => x.GetAttribute("href")));
            return pagesQueue;
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
