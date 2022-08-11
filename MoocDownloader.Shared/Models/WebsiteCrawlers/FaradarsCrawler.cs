using MoocDownloader.Shared.Models.Base;
using MoocDownloader.Shared.Models.Base.Attributes;
using MoocDownloader.Shared.Models.Enum;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace MoocDownloader.Shared.Models.WebsiteCrawlers
{
    [CrawlerInfo("Faradars", "https://faradars.org/", 5, implemented: false, authenticationRequired: true, courseLinkRegexFormat: @"^http(s)?:\/\/(www.)?faradars.org\/courses\/.*$")]

    public class FaradarsCrawler : CrawlerBase
    {
        private const string FaradarsUrl = "https://faradars.org/login";
        public FaradarsCrawler(string username, string password, SupportedBrowsers supportedBrowser) : base(FaradarsUrl, username, password, supportedBrowser)
        {

        }
        protected sealed override Queue<string> ExtractAllCoursePagesFromCourseListPage(CancellationToken stoppingToken)
        {
            var queue = new Queue<string>();
            queue.Enqueue(Browser.Url);
            return queue;
        }

        protected sealed override List<string> ExtractEachCoursePageVideoUrls(CancellationToken stoppingToken)
        {
            var result = new List<string>();
            var resultHashSet = new HashSet<string>();
            Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".loadStep")), stoppingToken);
            var loadSteps = Browser.FindElements(By.CssSelector(".loadStep")).ToArray();
            var count = loadSteps.Length / 2;
            loadSteps = loadSteps.Take(count).ToArray();
            foreach (var loadStep in loadSteps)
            {
                loadStep.Click();
                string url;
                do
                {
                    url = null;
                    var source = Waiter.Until(ExpectedConditions.ElementExists(By.CssSelector("source:first-child")), stoppingToken);
                    url = source.GetAttribute("src");
                    if (url != null && !resultHashSet.Contains(url))
                        break;
                    Task.Delay(50, stoppingToken).Wait();
                }
                while (!stoppingToken.IsCancellationRequested);
                
                if (string.IsNullOrWhiteSpace(url))
                {
                    result.Add(url);
                    resultHashSet.Add(url);
                }

                if (stoppingToken.IsCancellationRequested)
                    break;
            }
            return result;
        }

        protected sealed override void Login(CancellationToken stoppingToken)
        {
            var usernameInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#username")),stoppingToken);
            usernameInput.Click();
            usernameInput.SendKeys(Username);
            var passwordInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#password")), stoppingToken);
            passwordInput.Click();
            passwordInput.SendKeys(Password);
            var loginButton = Browser.FindElement(By.CssSelector("#formSubmitBtn"));
            loginButton.Click();
            Waiter.Until(ExpectedConditions.ElementExists(By.CssSelector("a[href=\"/my-account\"")), stoppingToken);
        }
    }
}
