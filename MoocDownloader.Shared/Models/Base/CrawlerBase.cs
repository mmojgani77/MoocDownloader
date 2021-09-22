using MoocDownloader.Shared.Models.DataTransferObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MoocDownloader.Shared.Models.Base
{
    public abstract class CrawlerBase : IDisposable
    {
        public string BaseUrl { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool IsLogin { get; private set; }
        protected ChromeDriver Chrome { get; private set; }
        protected WebDriverWait Waiter { get; private protected set; }
        protected INavigation Navigator { get; private set; }
        public CrawlerBase(string baseUrl, string username, string password, byte actionsTimeOutInSec = 20)
        {
            BaseUrl = baseUrl;
            Username = username;
            Password = password;
            Chrome = new ChromeDriver(".");
            Waiter = new WebDriverWait(Chrome, TimeSpan.FromSeconds(actionsTimeOutInSec));
            Navigator = Chrome.Navigate();
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
                pagesToCrawlQueue = ExtractAllCoursePagesFromCourseListPage();
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
                        videoLinks = ExtractEachCoursePageVideoUrls();
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
        protected abstract Queue<string> ExtractAllCoursePagesFromCourseListPage();
        protected abstract List<string> ExtractEachCoursePageVideoUrls();

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
