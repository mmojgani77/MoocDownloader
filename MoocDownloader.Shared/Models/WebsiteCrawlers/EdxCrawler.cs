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

namespace MoocDownloader.Shared.Models.WebsiteCrawlers
{
    [CrawlerInfo("Edx", "https://edx.org/", 3, implemented: true, authenticationRequired: true, courseLinkRegexFormat: @"^http(s)?:\/\/(www.)?learning.edx.org\/course\/.*\/home(\/)?$")]
    public class EdxCrawler : CrawlerBase
    {
        private const string Url = "https://authn.edx.org/login";
        public EdxCrawler(string username, string password, SupportedBrowsers supportedBrowser) : base(Url, username, password, supportedBrowser)
        {

        }

        protected override sealed Queue<string> ExtractAllCoursePagesFromCourseListPage(CancellationToken stoppingToken)
        {
            Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".pgn_collapsible")), stoppingToken);
            var sections = Browser.FindElements(By.CssSelector(".pgn_collapsible"));
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
                stoppingToken.ThrowIfCancellationRequested();
            }
            return result;
        }

        protected override sealed List<string> ExtractEachCoursePageVideoUrls(CancellationToken stoppingToken)
        {
            var result = new List<string>();
            var navigationTab = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".sequence-navigation-tabs")), stoppingToken);
            var tabsButton = navigationTab.FindElements(By.CssSelector("button"));

            var framesUrl = new Queue<string>();

            foreach (var tabButton in tabsButton)
            {
                if (stoppingToken.IsCancellationRequested)
                    break;

                tabButton.Click();
                var frame = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#unit-iframe")), stoppingToken);
                var frameLink = frame?.GetAttribute("src");
                if (!string.IsNullOrWhiteSpace(frameLink))
                    framesUrl.Enqueue(frameLink);
            }

            while (framesUrl.TryDequeue(out string frameLink) && !stoppingToken.IsCancellationRequested)
            {
                Navigator.GoToUrl(frameLink);
                try
                {
                    var downloadButton = Browser.FindElement(By.CssSelector("a.video-download-button"));
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

        protected override sealed void Login(CancellationToken stoppingToken)
        {
            var usernameInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[name=emailOrUsername]")),stoppingToken);
            usernameInput.SendKeys(Username);
            var passwordInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[name=password]")), stoppingToken); 
            passwordInput.SendKeys(Password);
            var loginButton = Browser.FindElement(By.CssSelector("button[type=submit]"));
            loginButton.Click();
            Waiter.Until(ExpectedConditions.ElementExists(By.CssSelector(".nav-item a[href*=dashboard]")), stoppingToken);
        }
    }
}
