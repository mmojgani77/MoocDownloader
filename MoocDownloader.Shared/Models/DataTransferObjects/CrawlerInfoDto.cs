using System;
using System.Collections.Generic;
using System.Text;

namespace MoocDownloader.Shared.Models.DataTransferObjects
{
    public class CrawlerInfoDto
    {
        public int Index { get; set; }
        public string Title { get; set; }
        public string WebsiteUrl { get; set; }
        public Type CrawlerType { get; set; }
        public bool Implemented { get; set; }
    }
}
