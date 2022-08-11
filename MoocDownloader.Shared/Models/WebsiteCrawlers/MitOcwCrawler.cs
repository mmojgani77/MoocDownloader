using MoocDownloader.Shared.Models.Base;
using MoocDownloader.Shared.Models.Base.Attributes;
using MoocDownloader.Shared.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MoocDownloader.Shared.Models.WebsiteCrawlers
{
    [CrawlerInfo("Mit Open Course Ware", "https://ocw.mit.edu/", 4, implemented: false, authenticationRequired: true)]
    public class MitOcwCrawler : CrawlerBase
    {
        private const string Url = "https://ocw.mit.edu/";

        public MitOcwCrawler(string username, string password, SupportedBrowsers supportedBrowser) : base(Url, username, password, supportedBrowser)
        {

        }

        protected override Queue<string> ExtractAllCoursePagesFromCourseListPage(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

        protected override List<string> ExtractEachCoursePageVideoUrls(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

        protected override bool Login()
        {
            throw new NotImplementedException();
        }
    }
}
