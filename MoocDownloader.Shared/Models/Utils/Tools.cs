using MoocDownloader.Shared.Models.Base;
using MoocDownloader.Shared.Models.Base.Attributes;
using MoocDownloader.Shared.Models.DataTransferObjects;
using MoocDownloader.Shared.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MoocDownloader.Shared.Base
{
    public static class Tools
    {
        public static IEnumerable<CrawlerInfoDto> GetCrawlers()
        {
            var crawlers = typeof(CrawlerBase).InheritedTypes().WithAttribute<CrawlerInfoAttribute>();
            return crawlers.Select(x =>
            {
                var attr = x.GetCustomAttribute<CrawlerInfoAttribute>();
                return new CrawlerInfoDto
                {
                    Title = attr.Title,
                    WebsiteUrl = attr.WebsiteUrl,
                    CrawlerType = x,
                    Index = attr.Index,
                    Implemented = attr.Implemented
                };
            });
        }
    }
}
