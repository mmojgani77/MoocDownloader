﻿using MoocDownloader.Shared.Models.Base;
using MoocDownloader.Shared.Models.Base.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MoocDownloader.Shared.Models.Utils;
using MoocDownloader.Shared.Base;
using MoocDownloader.Shared.Models.DataTransferObjects;
using System.Threading;
using System.IO;

namespace MoocDownloader.WinForm
{
    public partial class MainForm : Form
    {
        private List<CrawlerInfoDto> _crawlersInfo;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _started;
        public MainForm()
        {
            InitializeComponent();
            _started = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadAllCrawlers(comboCrawlers);
        }

        private void LoadAllCrawlers(ComboBox combo)
        {
            _crawlersInfo = Tools.GetCrawlers().OrderBy(x => x.Index).ToList();
            combo.Items.AddRange(_crawlersInfo.Select(x => x.Title).ToArray());
            combo.SelectedIndex = 0;
        }

        private void checkPageLimits_CheckedChanged(object sender, EventArgs e)
        {
            gpPageLimits.Visible = checkPageLimits.Checked;
            if (!checkPageLimits.Checked)
            {
                fromPageBox.Clear();
                toPageBox.Clear();
            }
        }
        private void Progress(ProgressValue progressValue)
        {
            this.Invoke(new Action(() =>
            {
                progressCrawl.Maximum = progressValue.TotalCount;
                progressCrawl.Value = progressValue.Value;
                lblProgress.Text = progressValue.ToString();
            }));
        }

        private void ApplyControlsEnableStatus(bool enabled)
        {
            foreach (Control control in this.Controls)
            {
                if (control is ProgressBar || control is Button)
                    continue;
                control.Enabled = enabled;
            }
            this.Enabled = true;
        }

        private async void btnCrawl_Click(object sender, EventArgs e)
        {
            if (!_started)
                await StartCrawl();
            else
                Stop();
        }

        private async Task StartCrawl()
        {
            try
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Text file | *.txt"
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _cancellationTokenSource = new CancellationTokenSource();
                    _started = true;
                    btnCrawl.Text = "Stop";
                    ApplyControlsEnableStatus(false);
                    var username = usernameBox.Text;
                    var password = passwordBox.Text;
                    var courseLink = courseLinkBox.Text;
                    int? fromPage = null;
                    int? toPage = null;
                    if (int.TryParse(fromPageBox.Text, out int from))
                    {
                        fromPage = from;
                    }
                    if (int.TryParse(toPageBox.Text, out int to))
                    {
                        toPage = to;
                    }
                    Queue<string> links = new Queue<string>();

                    using (CrawlerBase service = CreateSelectedCrawler(comboCrawlers, username, password))
                    {
                        await Task.Run(() =>
                        {

                            links = service.StartToCrawl(Progress, courseLink, fromPage, toPage, _cancellationTokenSource.Token);
                        });
                    }

                    File.WriteAllLines(saveFileDialog.FileName, links.ToArray());
                    ApplyControlsEnableStatus(true);
                    MessageBox.Show($"Done.\n{links.Count} videos crawled", "done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                saveFileDialog = null;
            }
            catch
            {
                MessageBox.Show("There is a problem in crawling.\nPlease contact the programmer");
            }
            _started = false;
            btnCrawl.Text = "Start to crawl links";
        }
        private void Stop()
        {
            _cancellationTokenSource.Cancel();
        }

        private CrawlerBase CreateSelectedCrawler(ComboBox comboCrawlers, string username, string password)
        {
            var index = comboCrawlers.SelectedIndex;
            var crawlerInfo = _crawlersInfo[index];
            return (CrawlerBase)Activator.CreateInstance(crawlerInfo.CrawlerType, new object[] { username, password });
        }
    }
}