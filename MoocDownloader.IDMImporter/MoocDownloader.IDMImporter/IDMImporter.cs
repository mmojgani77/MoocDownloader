using System;
using System.Collections.Generic;
using System.Text;

namespace MoocDownloader.IDMImporter
{
    public class IDMImporter
    {
        public IDMImporter()
        {

        }
        public void ShowDialog()
        {
            var dialog = new FrmIDMImporter();
            dialog.ShowDialog();
        }
    }
}
