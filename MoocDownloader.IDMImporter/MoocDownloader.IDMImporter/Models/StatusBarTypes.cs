using System;
using System.Collections.Generic;
using System.Text;

namespace MoocDownloader.IDMImporter.Models
{
    internal enum StatusBarTypes
    {
        NotStarted,
        Importing,
        Imported,
        ValidationError,
        ImportError,
    }
}
