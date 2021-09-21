using System;
using System.Collections.Generic;
using System.Text;

namespace MoocDownloader.Shared.Models.Base
{
    public class ProgressValue
    {
        public int Value { get; set; }
        public int TotalCount { get; set; }
        public override string ToString()
        {
            return $"{Value} from {TotalCount}";
        }
    }
}
