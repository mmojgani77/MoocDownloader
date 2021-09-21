
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
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
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
            this.gpPageLimits.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboCrawlers
            // 
            this.comboCrawlers.DropDownHeight = 200;
            this.comboCrawlers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCrawlers.FormattingEnabled = true;
            this.comboCrawlers.IntegralHeight = false;
            this.comboCrawlers.ItemHeight = 20;
            this.comboCrawlers.Location = new System.Drawing.Point(193, 18);
            this.comboCrawlers.Name = "comboCrawlers";
            this.comboCrawlers.Size = new System.Drawing.Size(374, 28);
            this.comboCrawlers.TabIndex = 0;
            this.comboCrawlers.SelectedIndexChanged += new System.EventHandler(this.comboCrawlers_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Crawler Type (Website) : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 20);
            this.label3.TabIndex = 20;
            this.label3.Text = "*Course link to crawl : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 19;
            this.label2.Text = "*Password : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "*Username : ";
            // 
            // courseLinkBox
            // 
            this.courseLinkBox.Location = new System.Drawing.Point(174, 157);
            this.courseLinkBox.Name = "courseLinkBox";
            this.courseLinkBox.Size = new System.Drawing.Size(393, 27);
            this.courseLinkBox.TabIndex = 17;
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(124, 111);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(443, 27);
            this.passwordBox.TabIndex = 16;
            this.passwordBox.UseSystemPasswordChar = true;
            // 
            // usernameBox
            // 
            this.usernameBox.Location = new System.Drawing.Point(124, 68);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(443, 27);
            this.usernameBox.TabIndex = 15;
            // 
            // btnCrawl
            // 
            this.btnCrawl.Location = new System.Drawing.Point(19, 212);
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
            this.label5.Location = new System.Drawing.Point(16, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "From page number :";
            // 
            // toPageBox
            // 
            this.toPageBox.Location = new System.Drawing.Point(160, 63);
            this.toPageBox.Name = "toPageBox";
            this.toPageBox.Size = new System.Drawing.Size(89, 27);
            this.toPageBox.TabIndex = 10;
            // 
            // fromPageBox
            // 
            this.fromPageBox.Location = new System.Drawing.Point(160, 31);
            this.fromPageBox.Name = "fromPageBox";
            this.fromPageBox.Size = new System.Drawing.Size(89, 27);
            this.fromPageBox.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "To page number :";
            // 
            // lblProgress
            // 
            this.lblProgress.Location = new System.Drawing.Point(433, 314);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(134, 29);
            this.lblProgress.TabIndex = 24;
            this.lblProgress.Text = "0 from 0";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressCrawl
            // 
            this.progressCrawl.Location = new System.Drawing.Point(19, 314);
            this.progressCrawl.Name = "progressCrawl";
            this.progressCrawl.Size = new System.Drawing.Size(408, 29);
            this.progressCrawl.TabIndex = 23;
            // 
            // checkPageLimits
            // 
            this.checkPageLimits.AutoSize = true;
            this.checkPageLimits.Location = new System.Drawing.Point(19, 265);
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
            this.gpPageLimits.Location = new System.Drawing.Point(300, 201);
            this.gpPageLimits.Name = "gpPageLimits";
            this.gpPageLimits.Size = new System.Drawing.Size(267, 108);
            this.gpPageLimits.TabIndex = 22;
            this.gpPageLimits.TabStop = false;
            this.gpPageLimits.Text = "Limits";
            this.gpPageLimits.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 359);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboCrawlers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
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
    }
}

