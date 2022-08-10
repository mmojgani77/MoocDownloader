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

namespace MoocDownloader.Shared.Models.WebsiteCrawlers
{
    [CrawlerInfo("Faradars", "https://faradars.org/", 5, implemented: false, authenticationRequired: true, courseLinkRegexFormat: @"^http(s)?:\/\/(www.)?faradars.org\/courses\/.*$")]

    public class FaradarsCrawler : CrawlerBase
    {
        private const string FaradarsUrl = "https://faradars.org/login";
        public FaradarsCrawler(string username, string password, SupportedBrowsers supportedBrowser) : base(FaradarsUrl, username, password, supportedBrowser)
        {

        }
        protected sealed override Queue<string> ExtractAllCoursePagesFromCourseListPage()
        {
            var queue = new Queue<string>();
            queue.Enqueue(Browser.Url);
            return queue;
        }

        protected sealed override List<string> ExtractEachCoursePageVideoUrls()
        {
            var result = new List<string>();
            var resultHashSet = new HashSet<string>();
            Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".loadStep")));
            var loadSteps = Browser.FindElements(By.CssSelector(".loadStep")).ToArray();
            var count = loadSteps.Length / 2;
            loadSteps = loadSteps.Take(count).ToArray();
            foreach (var loadStep in loadSteps)
            {
                loadStep.Click();
                string url = null;
                do
                {
                    url = null;
                    var source = Waiter.Until(ExpectedConditions.ElementExists(By.CssSelector("source:first-child")));
                    url = source.GetAttribute("src");
                    if(url != null && !resultHashSet.Contains(url))
                        break;
                    Task.Delay(100);
                }
                while (true);
                result.Add(url);
                resultHashSet.Add(url);
            }
            return result;
        }

        protected sealed override bool Login()
        {
            try
            {
                var usernameInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#username")));
                usernameInput.Click();
                usernameInput.SendKeys(Username);
                var passwordInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#password")));
                passwordInput.Click();
                passwordInput.SendKeys(Password);
                var loginButton = Browser.FindElement(By.CssSelector("#formSubmitBtn"));
                loginButton.Click();
                Waiter.Until(ExpectedConditions.ElementExists(By.CssSelector("a[href=\"/my-account\"")));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
