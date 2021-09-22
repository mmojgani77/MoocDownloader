﻿using MoocDownloader.Shared.Models.Base;
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
    [CrawlerInfo("Go to class", "https://gotoclass.ir", indexNumber: 1, authenticationRequired: true,courseLinkFormat: @"^http(s)?:\/\/(www.)?dars.gotoclass.ir\/courses\/.*\/course\/$")]
    public class GotoClassCrawler : CrawlerBase
    {
        private const string Url = "https://dars.gotoclass.ir/login";
        public GotoClassCrawler(string username, string password) : base(Url, username, password)
        {

        }

        protected sealed override bool Login()
        {
            try
            {
                var usernameInput = Waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[name=email]")));
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

        protected override Queue<string> ExtractAllCoursePagesFromCourseListPage()
        {
            var pages = Chrome.FindElementsByCssSelector(".subsection-text");
            var pagesQueue = new Queue<string>(pages.Select(x => x.GetAttribute("href")));
            return pagesQueue;
        }

        protected override List<string> ExtractEachCoursePageVideoUrls()
        {
            try
            {
                var videoUrls = new List<string>();
                var presentations = Chrome.FindElementsByCssSelector("li[role=presentation]").ToArray();
                foreach (var presentation in presentations)
                {
                    presentation.Click();
                    var videoSource = Chrome.FindElementsByCssSelector("video source[src*=mp4]").FirstOrDefault();
                    var src = videoSource?.GetAttribute("src");
                    if (!string.IsNullOrWhiteSpace(src))
                    {
                        var orgSrc = src.Substring(0, src.LastIndexOf('?'));
                        videoUrls.Add(orgSrc);
                    }
                }
                return videoUrls;
            }
            catch
            {

            }
            return null;
        }
    }
}
