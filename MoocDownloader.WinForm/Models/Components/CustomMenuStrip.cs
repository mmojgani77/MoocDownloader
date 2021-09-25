using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MoocDownloader.WinForm.Models.Components
{
    public class CustomMenuStrip : MenuStrip
    {
        public CustomMenuStrip()
        {
            this.RenderMode = ToolStripRenderMode.Professional;
            this.Renderer = new ToolStripProfessionalRenderer(new MenuStripRenderer());
            this.BackColor = Color.FromArgb(64, 64, 64);
            this.MenuActivate += CustomMenuStrip_MenuActivate;
            this.MenuDeactivate += CustomMenuStrip_MenuDeactivate;
            this.ForeColor = Color.White;
        }

        private void CustomMenuStrip_MenuDeactivate(object sender, EventArgs e)
        {
            var menuStrip = sender as CustomMenuStrip;
            ChangeColorOfItem(menuStrip);
        }

        private void CustomMenuStrip_MenuActivate(object sender, EventArgs e)
        {
            var menuStrip = sender as CustomMenuStrip;
            ChangeColorOfItem(menuStrip);
        }
        private void ChangeColorOfItem(CustomMenuStrip menuStrip)
        {
            foreach (ToolStripItem item in menuStrip.Items)
            {
                item.ForeColor = item.Selected ? Color.Black : this.ForeColor;
            }
        }


    }
}
