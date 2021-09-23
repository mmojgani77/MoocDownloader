using MoocDownloader.Shared.Models.Base;
using MoocDownloader.Shared.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoocDownloader.Shared.Models.Repository
{
    public class CrawlerFactory
    {
        private readonly CrawlerInfo[] _crawlers;
        public CrawlerFactory(IEnumerable<CrawlerInfo> crawlers)
        {
            _crawlers = crawlers.OrderBy(x => x.Index).ToArray();
        }

        public CrawlerInfo CreateCrawlerWithShortName(string shortName)
        {
            return _crawlers.FirstOrDefault(x => x.ShortName == shortName);
        }
        public CrawlerInfo CreateCrawlerWithTitle(string title)
        {
            return _crawlers.FirstOrDefault(x => x.Title == title);
        }
        public CrawlerInfo CreateCrawlerWithIndex(int index)
        {
            try
            {
                return _crawlers.OrderBy(x => x.Index).ToArray()[index];
            }
            catch
            {
            }
            return null;
        }
        public string[] GetAllCrawlersTitle()
        {
            return _crawlers.OrderBy(x => x.Index).Select(x => x.Title).ToArray();
        }
        public string[] GetAllCrawlersShortName()
        {
            return _crawlers.OrderBy(x => x.Index).Select(x => x.ShortName).ToArray();
        }
    }
}
