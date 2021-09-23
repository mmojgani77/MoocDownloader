using MoocDownloader.Shared.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoocDownloader.Shared.Models.DataTransferObjects
{
    public class CrawlResponseDto
    {
        public bool HasError { get; set; }
        public string ErrorText { get; set; }
        public Queue<string> Result { get; set; }
        public CrawlResponseDto()
        {
            HasError = true;
            ErrorText = "An error occured during crawling process";
            Result = new Queue<string>();
        }
    }
}
