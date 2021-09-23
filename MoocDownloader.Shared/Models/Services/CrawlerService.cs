using MoocDownloader.Shared.Base;
using MoocDownloader.Shared.Models.Base;
using MoocDownloader.Shared.Models.DataTransferObjects;
using MoocDownloader.Shared.Models.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MoocDownloader.Shared.Models.Services
{
    public class CrawlerService
    {
        private CrawlerFactory _crawlerFactory;
        public CrawlerService()
        {
            _crawlerFactory = Tools.GetCrawlerFactory();
        }
        public string[] GetAllCrawlersTitle()
        {
            return _crawlerFactory.GetAllCrawlersTitle();
        }
        public string[] GetAllCrawlersShortName()
        {
            return _crawlerFactory.GetAllCrawlersShortName();
        }
        public CrawlerInfo FindCrawlerWithIndex(int crawlerIndex)
        {
            return _crawlerFactory.CreateCrawlerWithIndex(crawlerIndex);
        }
        public CrawlerInfo FindCrawlerWithTitle(string title)
        {
            return _crawlerFactory.CreateCrawlerWithTitle(title);
        }
        public CrawlerInfo FindCrawlerWithShortName(string shortName)
        {
            return _crawlerFactory.CreateCrawlerWithShortName(shortName);
        }

        public async Task<CrawlResponseDto> CrawlWithCrawlerIndex(int crawlerIndex, CrawlRequestDto request)
        {
            var result = new CrawlResponseDto();
            var crawlerInfo = _crawlerFactory.CreateCrawlerWithIndex(crawlerIndex);
            var errorMessage = HasError(crawlerInfo, request);
            if (errorMessage != null)
            {
                result.ErrorText = errorMessage;
                return result;
            }
            result = await DoCrawl(crawlerInfo, request);
            return result;
        }
        public async Task<CrawlResponseDto> CrawlWithCrawlerTitle(string crawlerTitle, CrawlRequestDto request)
        {
            var result = new CrawlResponseDto();
            var crawlerInfo = _crawlerFactory.CreateCrawlerWithTitle(crawlerTitle);
            var errorMessage = HasError(crawlerInfo, request);
            if (errorMessage != null)
            {
                result.ErrorText = errorMessage;
                return result;
            }
            result = await DoCrawl(crawlerInfo, request);
            return result;
        }

        public async Task<CrawlResponseDto> CrawlWithCrawlerShortName(string crawlerShortName, CrawlRequestDto request)
        {
            var result = new CrawlResponseDto();
            var crawlerInfo = _crawlerFactory.CreateCrawlerWithShortName(crawlerShortName);
            var errorMessage = HasError(crawlerInfo, request);
            if (errorMessage != null)
            {
                result.ErrorText = errorMessage;
                return result;
            }
            result = await DoCrawl(crawlerInfo, request);
            return result;
        }

        private string HasError(CrawlerInfo crawlerInfo, CrawlRequestDto request)
        {
            if (request == null)
                return "Please fill the blank fields";

            if (crawlerInfo == null)
                return "There is no such crawler in this app";

            if (!crawlerInfo.Implemented)
                return $"You can't use {crawlerInfo?.Title} crawler.\nBecause it is not implemented yet.";

            if (crawlerInfo.AuthenticationRequired && (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password)))
            {
                return $"Authentication data is required for {crawlerInfo?.Title} crawler.\nFill the Username and Password boxes";
            }

            if (string.IsNullOrWhiteSpace(request.CourseLink))
            {
                return "You should fill the course link box";
            }

            if (!crawlerInfo.IsUrlMatchToCrawler(request.CourseLink))
            {
                return "Course link format for this crawler (website) is not correct.\nYou should provider main page url of course list.";
            }

            return null;
        }

        private async Task<CrawlResponseDto> DoCrawl(CrawlerInfo crawlerInfo, CrawlRequestDto request)
        {
            var response = new CrawlResponseDto();
            CrawlerResult crawlerResult = null;
            using (var crawler = crawlerInfo.CreateInstanceOfCrawler(request.Username, request.Password))
            {
                await Task.Run(() =>
                {
                    crawlerResult = crawler.ExtractAllVideoUrlsOfCourse(request.Progress, request.CourseLink, request.FromPage, request.ToPage, request.StoppingToken);
                });
            }

            if (crawlerResult == null || crawlerResult.HasError)
            {
                response.ErrorText = "There was a problem in crawling process.\nIt is that some of video urls is missing.";
                return response;
            }

            response.HasError = false;
            response.Result = crawlerResult.CrawledVideoUrls;
            return response;
        }
    }
}
