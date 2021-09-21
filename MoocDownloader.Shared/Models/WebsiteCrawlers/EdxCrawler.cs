using MoocDownloader.Shared.Models.Base;
using MoocDownloader.Shared.Models.Base.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MoocDownloader.Shared.Models.WebsiteCrawlers
{
    [CrawlerInfo("Edx", "https://edx.org/", 3, implemented: false)]
    public class EdxCrawler : CrawlerBase
    {
        private const string Url = "https://authn.edx.org/login";
        public EdxCrawler(string username, string password) : base(Url, username, password)
        {

        }

        protected override Queue<string> CrawlVideoUrls(Action<ProgressValue> progress, string coursePageLink, int? fromPage = null, int? toPage = null, CancellationToken stoppingToken = default)
        {
            throw new NotImplementedException();
        }

        protected override void Login()
        {
            throw new NotImplementedException();
        }
    }
}
