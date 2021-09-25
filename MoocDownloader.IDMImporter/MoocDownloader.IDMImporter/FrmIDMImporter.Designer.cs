
namespace MoocDownloader.IDMImporter
{
    partial class FrmIDMImporter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.idmPathBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.linksPathBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.numWildcardSize = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numStartFrom = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFileNameExtension = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFileNameSuffix = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblFileNameSample = new System.Windows.Forms.Label();
            this.txtFileNamePrefix = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.destinationFolderPathBox = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.importProgress = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWildcardSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartFrom)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // idmPathBox
            // 
            this.idmPathBox.BackColor = System.Drawing.SystemColors.Control;
            this.idmPathBox.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.idmPathBox.Location = new System.Drawing.Point(125, 42);
            this.idmPathBox.Name = "idmPathBox";
            this.idmPathBox.ReadOnly = true;
            this.idmPathBox.Size = new System.Drawing.Size(428, 32);
            this.idmPathBox.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.idmPathBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(568, 93);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IDM executable location";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.linksPathBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 111);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(568, 93);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Links text file";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(16, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 33);
            this.button2.TabIndex = 0;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // linksPathBox
            // 
            this.linksPathBox.BackColor = System.Drawing.SystemColors.Control;
            this.linksPathBox.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.linksPathBox.Location = new System.Drawing.Point(125, 42);
            this.linksPathBox.Name = "linksPathBox";
            this.linksPathBox.ReadOnly = true;
            this.linksPathBox.Size = new System.Drawing.Size(428, 32);
            this.linksPathBox.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.numWildcardSize);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.numStartFrom);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtFileNameExtension);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtFileNameSuffix);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.lblFileNameSample);
            this.groupBox3.Controls.Add(this.txtFileNamePrefix);
            this.groupBox3.Location = new System.Drawing.Point(12, 219);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(568, 248);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Files name settings for import";
            // 
            // label9
            // 
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(17, 193);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(536, 49);
            this.label9.TabIndex = 15;
            this.label9.Text = "*Note : Default file extesnion is a required field and will be applied when impor" +
    "ted link has no file extension";
            // 
            // numWildcardSize
            // 
            this.numWildcardSize.Location = new System.Drawing.Point(373, 115);
            this.numWildcardSize.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numWildcardSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWildcardSize.Name = "numWildcardSize";
            this.numWildcardSize.Size = new System.Drawing.Size(70, 27);
            this.numWildcardSize.TabIndex = 14;
            this.numWildcardSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numWildcardSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWildcardSize.ValueChanged += new System.EventHandler(this.numWildcardSize_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(235, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 20);
            this.label8.TabIndex = 13;
            this.label8.Text = "with wildcard size : ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Start number from : ";
            // 
            // numStartFrom
            // 
            this.numStartFrom.Location = new System.Drawing.Point(159, 115);
            this.numStartFrom.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numStartFrom.Name = "numStartFrom";
            this.numStartFrom.Size = new System.Drawing.Size(70, 27);
            this.numStartFrom.TabIndex = 11;
            this.numStartFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numStartFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numStartFrom.ValueChanged += new System.EventHandler(this.numStartFrom_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Sample file name : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(373, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "* Default file extension";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(359, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = ".";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFileNameExtension
            // 
            this.txtFileNameExtension.BackColor = System.Drawing.Color.White;
            this.txtFileNameExtension.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtFileNameExtension.Location = new System.Drawing.Point(377, 70);
            this.txtFileNameExtension.Name = "txtFileNameExtension";
            this.txtFileNameExtension.Size = new System.Drawing.Size(176, 32);
            this.txtFileNameExtension.TabIndex = 7;
            this.txtFileNameExtension.Text = "unknown";
            this.txtFileNameExtension.TextChanged += new System.EventHandler(this.txtFileNameExtension_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(226, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "File name suffix";
            // 
            // txtFileNameSuffix
            // 
            this.txtFileNameSuffix.BackColor = System.Drawing.Color.White;
            this.txtFileNameSuffix.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtFileNameSuffix.Location = new System.Drawing.Point(231, 70);
            this.txtFileNameSuffix.Name = "txtFileNameSuffix";
            this.txtFileNameSuffix.Size = new System.Drawing.Size(122, 32);
            this.txtFileNameSuffix.TabIndex = 5;
            this.txtFileNameSuffix.TextChanged += new System.EventHandler(this.txtFileNameSuffix_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(132, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 32);
            this.label3.TabIndex = 4;
            this.label3.Text = "%NUMBER%";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "File name prefix";
            // 
            // lblFileNameSample
            // 
            this.lblFileNameSample.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblFileNameSample.Location = new System.Drawing.Point(151, 163);
            this.lblFileNameSample.Name = "lblFileNameSample";
            this.lblFileNameSample.Size = new System.Drawing.Size(403, 25);
            this.lblFileNameSample.TabIndex = 2;
            this.lblFileNameSample.Text = "File name should contain %%NUMBER%% to generate numbers automaticaly.";
            // 
            // txtFileNamePrefix
            // 
            this.txtFileNamePrefix.BackColor = System.Drawing.Color.White;
            this.txtFileNamePrefix.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtFileNamePrefix.Location = new System.Drawing.Point(16, 70);
            this.txtFileNamePrefix.Name = "txtFileNamePrefix";
            this.txtFileNamePrefix.Size = new System.Drawing.Size(111, 32);
            this.txtFileNamePrefix.TabIndex = 1;
            this.txtFileNamePrefix.TextChanged += new System.EventHandler(this.txtFileNamePrefix_TextChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Controls.Add(this.destinationFolderPathBox);
            this.groupBox4.Location = new System.Drawing.Point(12, 473);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(568, 93);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Destination folder to download files";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(16, 41);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(103, 33);
            this.button3.TabIndex = 0;
            this.button3.Text = "Browse";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // destinationFolderPathBox
            // 
            this.destinationFolderPathBox.BackColor = System.Drawing.SystemColors.Control;
            this.destinationFolderPathBox.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.destinationFolderPathBox.Location = new System.Drawing.Point(125, 42);
            this.destinationFolderPathBox.Name = "destinationFolderPathBox";
            this.destinationFolderPathBox.ReadOnly = true;
            this.destinationFolderPathBox.Size = new System.Drawing.Size(428, 32);
            this.destinationFolderPathBox.TabIndex = 1;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 575);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(568, 40);
            this.button4.TabIndex = 6;
            this.button4.Text = "Start Import Links To IDM";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // importProgress
            // 
            this.importProgress.Location = new System.Drawing.Point(12, 621);
            this.importProgress.Name = "importProgress";
            this.importProgress.Size = new System.Drawing.Size(411, 21);
            this.importProgress.TabIndex = 7;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(429, 621);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(151, 21);
            this.lblStatus.TabIndex = 16;
            this.lblStatus.Text = "Not Started";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmIDMImporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 666);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.importProgress);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmIDMImporter";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IDM Importer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWildcardSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartFrom)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox idmPathBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox linksPathBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtFileNamePrefix;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFileNameSample;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFileNameSuffix;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFileNameExtension;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numStartFrom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numWildcardSize;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox destinationFolderPathBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ProgressBar importProgress;
        private System.Windows.Forms.Label lblStatus;
    }
}