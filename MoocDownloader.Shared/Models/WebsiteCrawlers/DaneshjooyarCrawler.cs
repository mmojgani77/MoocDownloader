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
    [CrawlerInfo("Daneshjooyar", "https://www.daneshjooyar.com/", 7, implemented: true, authenticationRequired: true, courseLinkRegexFormat: @"^http(s)?:\/\/(www.)?daneshjooyar.com\/.*$")]
    public class DaneshjooyarCrawler : CrawlerBase
    {
        private const string Url = "https://www.daneshjooyar.com/";
        private const string LoginUrl = Url + "panel/login/?redirect=/";

        public DaneshjooyarCrawler(string username, string password, SupportedBrowsers supportedBrowser) : base(LoginUrl, username, password, supportedBrowser)
        {

        }
        protected override sealed Queue<string> ExtractAllCoursePagesFromCourseListPage(CancellationToken stoppingToken)
        {
            return new Queue<string>(new List<string>()
            {
                Browser.Url
            });
        }

        protected override sealed List<string> ExtractEachCoursePageVideoUrls(CancellationToken stoppingToken)
        {
            Waiter.Until(ExpectedConditions.ElementExists(By.CssSelector(".playable>a")), stoppingToken);
            var items = Browser.FindElements(By.CssSelector(".playable>a")).ToArray();
            var links = items.Select(x => x.GetAttribute("href")).ToList();
            return links;
        }

        protected override sealed void Login(CancellationToken stoppingToken)
        {
            var usernameInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#username-login")), stoppingToken);
            usernameInput.Click();
            usernameInput.SendKeys(Username);
            var passwordInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#password-login")), stoppingToken);
            passwordInput.Click();
            passwordInput.SendKeys(Password);
            var frame = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("iframe")), stoppingToken);
            Browser.SwitchTo().Frame(frame);
            var recaptcha = Waiter.Until(ExpectedConditions.ElementExists(By.CssSelector("span.recaptcha-checkbox")));
            recaptcha.Click();
            Waiter.Until(ExpectedConditions.ElementExists(By.CssSelector(".recaptcha-checkbox-checked")), stoppingToken);
            Browser.SwitchTo().ParentFrame();
            var loginButton = Browser.FindElement(By.CssSelector("button[type=submit]"));
            loginButton.Click();
            Waiter.Until(ExpectedConditions.UrlToBe(Url), stoppingToken);
        }
    }
}
