using MoocDownloader.Shared.Models.Base;
using MoocDownloader.Shared.Models.Base.Attributes;
using MoocDownloader.Shared.Models.DataTransferObjects;
using MoocDownloader.Shared.Models.Repository;
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
        public static CrawlerFactory GetCrawlerFactory()
        {
            var crawlers = typeof(CrawlerBase).InheritedTypes().WithAttribute<CrawlerInfoAttribute>();
            return new CrawlerFactory(crawlers.Select(x =>
               {
                   var attr = x.GetCustomAttribute<CrawlerInfoAttribute>();
                   var crawlerInfo = new CrawlerInfo(x, attr.Index, attr.Title, attr.WebsiteUrl, attr.CourseLinkRegexFormat);

                   if (attr.AuthenticationRequired)
                       crawlerInfo.SetAuthenticationRequiredFlag();

                   if (attr.Implemented)
                       crawlerInfo.SetImplementedFlag();

                   return crawlerInfo;
               }));
        }
    }
}
