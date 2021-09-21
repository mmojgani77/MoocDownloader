using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MoocDownloader.Shared.Models.Base
{
    public abstract class CrawlerBase : IDisposable
    {
        public string BaseUrl { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool IsLogin { get; protected set; }
        protected ChromeDriver Chrome { get; private set; }
        protected WebDriverWait Waiter { get; private set; }
        protected INavigation Navigator { get; private set; }
        public CrawlerBase(string baseUrl, string username, string password)
        {
            BaseUrl = baseUrl;
            Username = username;
            Password = password;
            Chrome = new ChromeDriver(".");
            Waiter = new WebDriverWait(Chrome, TimeSpan.FromSeconds(60));
            Navigator = Chrome.Navigate();
        }
        protected void GoToWebsite()
        {
            Navigator.GoToUrl(BaseUrl);
        }
        protected abstract void Login();
        protected abstract Queue<string> CrawlVideoUrls(Action<ProgressValue> progress, string coursePageLink, int? fromPage = null, int? toPage = null, CancellationToken stoppingToken = default);
        public Queue<string> StartToCrawl(Action<ProgressValue> progress, string coursePageLink, int? fromPage = null, int? toPage = null, CancellationToken stoppingToken = default)
        {
            try
            {
                GoToWebsite();
                if (!IsLogin)
                    Login();
                return CrawlVideoUrls(progress, coursePageLink, fromPage, toPage, stoppingToken);
            }
            catch
            {

            }
            return null;
        }

        public void Dispose()
        {
            Chrome?.Close();
            Chrome?.Quit();
            Chrome?.Dispose();
            Chrome = null;
            Waiter = null;
            Navigator = null;
            GC.Collect();
        }

        ~CrawlerBase()
        {
            Dispose();
        }
    }
}
