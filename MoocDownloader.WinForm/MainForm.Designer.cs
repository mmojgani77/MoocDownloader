
using MoocDownloader.WinForm.Models.Components;

namespace MoocDownloader.WinForm
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboCrawlers = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.courseLinkBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.btnCrawl = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.toPageBox = new System.Windows.Forms.TextBox();
            this.fromPageBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.progressCrawl = new System.Windows.Forms.ProgressBar();
            this.checkPageLimits = new System.Windows.Forms.CheckBox();
            this.gpPageLimits = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new CustomMenuStrip();
            this.toolsMenuStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.idmImporterMenuStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gpPageLimits.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboCrawlers
            // 
            this.comboCrawlers.DropDownHeight = 200;
            this.comboCrawlers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCrawlers.FormattingEnabled = true;
            this.comboCrawlers.IntegralHeight = false;
            this.comboCrawlers.ItemHeight = 20;
            this.comboCrawlers.Location = new System.Drawing.Point(192, 41);
            this.comboCrawlers.Name = "comboCrawlers";
            this.comboCrawlers.Size = new System.Drawing.Size(374, 28);
            this.comboCrawlers.TabIndex = 0;
            this.comboCrawlers.SelectedIndexChanged += new System.EventHandler(this.comboCrawlers_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Crawler Type (Website) : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 20);
            this.label3.TabIndex = 20;
            this.label3.Text = "*Course link to crawl : ";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(18, 134);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(87, 20);
            this.lblPassword.TabIndex = 19;
            this.lblPassword.Text = "*Password : ";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(18, 94);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(92, 20);
            this.lblUsername.TabIndex = 18;
            this.lblUsername.Text = "*Username : ";
            // 
            // courseLinkBox
            // 
            this.courseLinkBox.Location = new System.Drawing.Point(173, 180);
            this.courseLinkBox.Name = "courseLinkBox";
            this.courseLinkBox.Size = new System.Drawing.Size(393, 27);
            this.courseLinkBox.TabIndex = 17;
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(123, 134);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(443, 27);
            this.passwordBox.TabIndex = 16;
            this.passwordBox.UseSystemPasswordChar = true;
            // 
            // usernameBox
            // 
            this.usernameBox.Location = new System.Drawing.Point(123, 91);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(443, 27);
            this.usernameBox.TabIndex = 15;
            // 
            // btnCrawl
            // 
            this.btnCrawl.Location = new System.Drawing.Point(18, 235);
            this.btnCrawl.Name = "btnCrawl";
            this.btnCrawl.Size = new System.Drawing.Size(261, 37);
            this.btnCrawl.TabIndex = 14;
            this.btnCrawl.Text = "Start to crawl links";
            this.btnCrawl.UseVisualStyleBackColor = true;
            this.btnCrawl.Click += new System.EventHandler(this.btnCrawl_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "From page number :";
            // 
            // toPageBox
            // 
            this.toPageBox.Location = new System.Drawing.Point(160, 65);
            this.toPageBox.Name = "toPageBox";
            this.toPageBox.Size = new System.Drawing.Size(89, 27);
            this.toPageBox.TabIndex = 10;
            // 
            // fromPageBox
            // 
            this.fromPageBox.Location = new System.Drawing.Point(160, 33);
            this.fromPageBox.Name = "fromPageBox";
            this.fromPageBox.Size = new System.Drawing.Size(89, 27);
            this.fromPageBox.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "To page number :";
            // 
            // lblProgress
            // 
            this.lblProgress.Location = new System.Drawing.Point(432, 337);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(134, 29);
            this.lblProgress.TabIndex = 24;
            this.lblProgress.Text = "0 from 0";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressCrawl
            // 
            this.progressCrawl.Location = new System.Drawing.Point(18, 337);
            this.progressCrawl.Name = "progressCrawl";
            this.progressCrawl.Size = new System.Drawing.Size(408, 29);
            this.progressCrawl.TabIndex = 23;
            // 
            // checkPageLimits
            // 
            this.checkPageLimits.AutoSize = true;
            this.checkPageLimits.Location = new System.Drawing.Point(18, 288);
            this.checkPageLimits.Name = "checkPageLimits";
            this.checkPageLimits.Size = new System.Drawing.Size(148, 24);
            this.checkPageLimits.TabIndex = 21;
            this.checkPageLimits.Text = "Apply page limits";
            this.checkPageLimits.UseVisualStyleBackColor = true;
            this.checkPageLimits.CheckedChanged += new System.EventHandler(this.checkPageLimits_CheckedChanged);
            // 
            // gpPageLimits
            // 
            this.gpPageLimits.Controls.Add(this.label5);
            this.gpPageLimits.Controls.Add(this.toPageBox);
            this.gpPageLimits.Controls.Add(this.fromPageBox);
            this.gpPageLimits.Controls.Add(this.label6);
            this.gpPageLimits.Location = new System.Drawing.Point(299, 224);
            this.gpPageLimits.Name = "gpPageLimits";
            this.gpPageLimits.Size = new System.Drawing.Size(267, 108);
            this.gpPageLimits.TabIndex = 22;
            this.gpPageLimits.TabStop = false;
            this.gpPageLimits.Text = "Limits";
            this.gpPageLimits.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsMenuStripItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(587, 28);
            this.menuStrip1.TabIndex = 25;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolsMenuStripItem
            // 
            this.toolsMenuStripItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.idmImporterMenuStripItem});
            this.toolsMenuStripItem.ForeColor = System.Drawing.Color.White;
            this.toolsMenuStripItem.Name = "toolsMenuStripItem";
            this.toolsMenuStripItem.Size = new System.Drawing.Size(58, 24);
            this.toolsMenuStripItem.Text = "Tools";
            // 
            // idmImporterMenuStripItem
            // 
            this.idmImporterMenuStripItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.idmImporterMenuStripItem.ForeColor = System.Drawing.Color.White;
            this.idmImporterMenuStripItem.Name = "idmImporterMenuStripItem";
            this.idmImporterMenuStripItem.Size = new System.Drawing.Size(182, 26);
            this.idmImporterMenuStripItem.Text = "IDM Importer";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 382);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.courseLinkBox);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.usernameBox);
            this.Controls.Add(this.btnCrawl);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressCrawl);
            this.Controls.Add(this.checkPageLimits);
            this.Controls.Add(this.gpPageLimits);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboCrawlers);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mooc Downloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gpPageLimits.ResumeLayout(false);
            this.gpPageLimits.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboCrawlers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox courseLinkBox;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.TextBox usernameBox;
        private System.Windows.Forms.Button btnCrawl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox toPageBox;
        private System.Windows.Forms.TextBox fromPageBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ProgressBar progressCrawl;
        private System.Windows.Forms.CheckBox checkPageLimits;
        private System.Windows.Forms.GroupBox gpPageLimits;
        private CustomMenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolsMenuStripItem;
        private System.Windows.Forms.ToolStripMenuItem idmImporterMenuStripItem;
    }
}

