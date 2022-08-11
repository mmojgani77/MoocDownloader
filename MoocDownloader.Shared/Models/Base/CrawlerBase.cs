using MoocDownloader.Shared.Models.DataTransferObjects;
using MoocDownloader.Shared.Models.Enum;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace MoocDownloader.Shared.Models.Base
{
    public abstract class CrawlerBase : IDisposable
    {
        public string BaseUrl { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool IsLogin { get; private set; }
        protected WebDriver Browser { get; private set; }
        protected WebDriverWait Waiter { get; private protected set; }
        protected INavigation Navigator { get; private set; }
        public CrawlerBase(string baseUrl, string username, string password, SupportedBrowsers supportedBrowser, byte actionsTimeOutInSec = 120)
        {
            BaseUrl = baseUrl;
            Username = username;
            Password = password;
            ConfigureBrowser(supportedBrowser, actionsTimeOutInSec);
        }
        private void ConfigureBrowser(SupportedBrowsers supportedBrowser, byte actionsTimeOutInSec)
        {
            var driverManager = new WebDriverManager.DriverManager();
            switch (supportedBrowser)
            {
                case SupportedBrowsers.Chrome:
                    SetupChrome(driverManager);
                    break;
                case SupportedBrowsers.Firefox:
                    SetupFirefox(driverManager);
                    break;
                default:
                    throw new Exception("Does not support this browser");
            }

            Waiter = new WebDriverWait(Browser, TimeSpan.FromSeconds(actionsTimeOutInSec));
            Navigator = Browser.Navigate();
        }

        private void SetupChrome(WebDriverManager.DriverManager driverManager)
        {
            var path = driverManager.SetUpDriver(new ChromeConfig());
            var directory = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);
            var service = ChromeDriverService.CreateDefaultService(directory, fileName);
            service.HideCommandPromptWindow = true;
            Browser = new ChromeDriver(service);
        }
        private void SetupFirefox(WebDriverManager.DriverManager driverManager)
        {
            var path = driverManager.SetUpDriver(new FirefoxConfig());
            var directory = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);
            var service = FirefoxDriverService.CreateDefaultService(directory, fileName);
            service.HideCommandPromptWindow = true;
            Browser = new FirefoxDriver(service);
        }

        public CrawlerResult ExtractAllVideoUrlsOfCourse(Action<ProgressValue> progress, string coursePageLink, int? fromPage = null, int? toPage = null, CancellationToken stoppingToken = default)
        {
            var result = new CrawlerResult();
            try
            {
                GoToWebsite();
                try
                {
                    if (!IsLogin)
                        IsLogin = Login();
                }
                catch
                {
                    IsLogin = false;
                    result.HasError = true;
                }

                if (IsLogin)
                {
                    result = ExtractAllVideoUrls(progress, coursePageLink, fromPage, toPage, stoppingToken);
                }
            }
            catch
            {
                result.HasError = true;
            }
            return result;
        }
        private void GoToWebsite()
        {
            Navigator.GoToUrl(BaseUrl);
        }
        protected abstract bool Login();
        protected virtual CrawlerResult ExtractAllVideoUrls(Action<ProgressValue> progress, string coursePageLink, int? fromPage = null, int? toPage = null, CancellationToken stoppingToken = default)
        {
            var result = new CrawlerResult();
            Navigator.GoToUrl(coursePageLink);
            var pagesToCrawlQueue = new Queue<string>();
            var videoUrls = new Queue<string>();
            var videoLinks = new List<string>();
            int page = 1;

            try
            {
                pagesToCrawlQueue = ExtractAllCoursePagesFromCourseListPage(stoppingToken);
                if (pagesToCrawlQueue == null)
                    pagesToCrawlQueue = new Queue<string>();
            }
            catch
            {
                pagesToCrawlQueue = new Queue<string>();
                result.HasError = true;
            }

            int totalCount = pagesToCrawlQueue.Count(x => !string.IsNullOrWhiteSpace(x));

            if (toPage.HasValue && fromPage.HasValue)
                totalCount = toPage.GetValueOrDefault() - fromPage.GetValueOrDefault() + 1;
            else if (toPage.HasValue)
                totalCount = toPage.GetValueOrDefault();
            else if (fromPage.HasValue)
                totalCount -= fromPage.GetValueOrDefault() - 1;

            int crawledCount = 0;
            while (pagesToCrawlQueue.TryDequeue(out string pageLink) && !stoppingToken.IsCancellationRequested)
            {
                if (!string.IsNullOrWhiteSpace(pageLink))
                {
                    if (fromPage.HasValue && page < fromPage)
                    {
                        page++;
                        continue;
                    }

                    if (toPage.HasValue && page > toPage)
                    {
                        break;
                    }
                    Navigator.GoToUrl(pageLink);
                    try
                    {
                        videoLinks = ExtractEachCoursePageVideoUrls(stoppingToken);
                        if (videoLinks == null)
                            videoLinks = new List<string>();
                    }
                    catch
                    {
                        videoLinks = new List<string>();
                        result.HasError = true;
                    }

                    foreach (var videoLink in videoLinks)
                    {
                        if (!string.IsNullOrWhiteSpace(videoLink))
                            videoUrls.Enqueue(videoLink);
                    }
                    crawledCount++;
                    progress?.Invoke(new ProgressValue { Value = crawledCount, TotalCount = totalCount });
                    page++;
                }
            }
            result.CrawledVideoUrls = videoUrls;
            return result;
        }
        protected abstract Queue<string> ExtractAllCoursePagesFromCourseListPage(CancellationToken stoppingToken);
        protected abstract List<string> ExtractEachCoursePageVideoUrls(CancellationToken stoppingToken);

        public void Dispose()
        {
            try
            {
                Browser?.Close();
                Browser?.Quit();
                Browser?.Dispose();
                Browser = null;
                Waiter = null;
                Navigator = null;
                GC.Collect();
            }
            catch
            {

            }
        }

        ~CrawlerBase()
        {
            Dispose();
        }
    }
}
