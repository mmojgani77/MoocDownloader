using MoocDownloader.Shared.Models.Base;
using MoocDownloader.Shared.Models.Base.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MoocDownloader.Shared.Models.WebsiteCrawlers
{
    [CrawlerInfo("Mit Open Course Ware", "https://ocw.mit.edu/", 4, implemented: false)]
    public class MitOcwCrawler : CrawlerBase
    {
        private const string Url = "https://ocw.mit.edu/";

        public MitOcwCrawler(string username, string password) : base(Url, username, password)
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
