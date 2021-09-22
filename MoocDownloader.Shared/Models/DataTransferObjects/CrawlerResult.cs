using System;
using System.Collections.Generic;
using System.Text;

namespace MoocDownloader.Shared.Models.DataTransferObjects
{
    public class CrawlerResult
    {
        public Queue<string> CrawledVideoUrls { get; internal set; }
        public bool HasError { get; internal set; }
        public CrawlerResult()
        {
            CrawledVideoUrls = new Queue<string>();
        }
    }
}
