using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MoocDownloader.WinForm.Models.Components
{
    public class MenuStripRenderer : System.Windows.Forms.ProfessionalColorTable
    {
        public override Color ToolStripBorder
        {
            get { return Color.FromArgb(0, 0, 0); }
        }
        public override Color ToolStripDropDownBackground
        {
            get { return Color.FromArgb(64, 64, 64); }
        }

        public override Color MenuBorder => Color.FromArgb(64, 64, 64);
        public override Color MenuItemBorder => Color.FromArgb(64, 64, 64);

    }
}
