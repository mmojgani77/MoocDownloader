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
    [CrawlerInfo("Edx", "https://edx.org/", 3, implemented: true, authenticationRequired: true, courseLinkRegexFormat: @"^http(s)?:\/\/(www.)?learning.edx.org\/course\/.*\/home(\/)?$")]
    public class EdxCrawler : CrawlerBase
    {
        private const string Url = "https://authn.edx.org/login";
        public EdxCrawler(string username, string password) : base(Url, username, password)
        {

        }

        protected override Queue<string> ExtractAllCoursePagesFromCourseListPage()
        {
            Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".pgn_collapsible")));
            var sections = Chrome.FindElementsByCssSelector(".pgn_collapsible");
            var result = new Queue<string>();
            foreach (var section in sections)
            {
                var sectionButton = section.FindElement(By.CssSelector(".collapsible-trigger"));
                if (sectionButton.GetAttribute("aria-expanded") != "true" || section.FindElements(By.CssSelector("ol")).Count == 0)
                {
                    sectionButton.Click();
                }
                var list = section.FindElements(By.CssSelector("ol li a"));
                if (list != null && list.Count != 0)
                {
                    var links = list.Select(x => x?.GetAttribute("href")).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                    foreach (var link in links)
                    {
                        result.Enqueue(link);
                    }
                }

            }
            return result;
        }

        protected override List<string> ExtractEachCoursePageVideoUrls()
        {
            var result = new List<string>();
            var navigationTab = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".sequence-navigation-tabs")));
            var tabsButton = navigationTab.FindElements(By.CssSelector("button"));

            var framesUrl = new Queue<string>();

            foreach (var tabButton in tabsButton)
            {
                tabButton.Click();
                var frame = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#unit-iframe")));
                var frameLink = frame?.GetAttribute("src");
                if (!string.IsNullOrWhiteSpace(frameLink))
                    framesUrl.Enqueue(frameLink);

            }

            while (framesUrl.TryDequeue(out string frameLink))
            {
                Navigator.GoToUrl(frameLink);
                try
                {
                    var downloadButton = Chrome.FindElementByCssSelector("a.video-download-button");
                    var link = downloadButton?.GetAttribute("href");
                    if (!string.IsNullOrWhiteSpace(link))
                        result.Add(link);
                }
                catch
                {

                }
            }
            return result;

        }

        protected override bool Login()
        {
            try
            {
                var usernameInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[name=emailOrUsername]")));
                usernameInput.SendKeys(Username);
                var passwordInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[name=password]")));
                passwordInput.SendKeys(Password);
                var loginButton = Chrome.FindElementByCssSelector("button[type=submit]");
                loginButton.Click();
                Waiter.Until(ExpectedConditions.ElementExists(By.CssSelector(".nav-item a[href*=dashboard]")));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
