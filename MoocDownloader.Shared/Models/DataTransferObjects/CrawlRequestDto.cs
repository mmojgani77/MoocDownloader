using MoocDownloader.Shared.Models.Base;
using MoocDownloader.Shared.Models.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MoocDownloader.Shared.Models.DataTransferObjects
{
    public class CrawlRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Action<ProgressValue> Progress { get; set; }
        public string CourseLink { get; set; }
        public int? FromPage { get; set; }
        public int? ToPage { get; set; }
        public SupportedBrowsers SupportedBrowser { get; set; }
        public CancellationToken StoppingToken { get; set; }
    }
}
