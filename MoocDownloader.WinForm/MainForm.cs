using MoocDownloader.Shared.Models.Base;
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
using MoocDownloader.WinForm.Assets;

namespace MoocDownloader.WinForm
{
    public partial class MainForm : Form
    {
        private List<CrawlerInfoDto> _crawlersInfo;
        private CancellationTokenSource _cancellationTokenSource;
        private CrawlerBase _currentCrawler = null;
        private bool _started;
        public MainForm()
        {
            InitializeComponent();
            _started = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            usernameBox.Text = Settings.Default.Username;
            passwordBox.Text = Settings.Default.Password;
            LoadAllCrawlers(comboCrawlers);
        }

        private void LoadAllCrawlers(ComboBox combo)
        {
            _crawlersInfo = Tools.GetCrawlers().OrderBy(x => x.Index).ToList();

            combo.Items.AddRange(_crawlersInfo.Select(x =>
            {
                string title = x.Title;
                if (!x.Implemented)
                    title += " (*Not implemented)";
                return title;

            }).ToArray());

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

            // do the combo event again to disable things if needed
            if (enabled)
                comboCrawlers_SelectedIndexChanged(comboCrawlers, new EventArgs());
        }

        private async void btnCrawl_Click(object sender, EventArgs e)
        {
            if (!_started)
            {
                if (IsCrawlerImplemented() && HasValidModel() && IsCourseLinkInCorrectFormat())
                    await StartCrawl();
            }
            else
                Stop();
        }

        private bool HasValidModel()
        {
            var crawler = GetSelectedCrawlerInfo();
            if (crawler.AuthenticationRequired)
            {
                if (string.IsNullOrWhiteSpace(usernameBox.Text) || string.IsNullOrWhiteSpace(passwordBox.Text))
                {
                    ShowError($"Authentication data is required for {crawler.Title} crawler.\nFill the Username and Password boxes", "Authentication data required");
                    return false;
                }
            }
            return true;
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
                    var crawlerResult = new CrawlerResult();
                    var links = new Queue<string>();
                    using (CrawlerBase service = CreateSelectedCrawler(comboCrawlers, username, password))
                    {
                        _currentCrawler = service;
                        await Task.Run(() =>
                        {
                            crawlerResult = service.ExtractAllVideoUrlsOfCourse(Progress, courseLink, fromPage, toPage, _cancellationTokenSource.Token);
                        });
                    }
                    _currentCrawler = null;

                    if (crawlerResult.HasError)
                    {
                        ShowError("There was a problem in crawling process.\nIt is that some of video urls is missing.");
                    }

                    if (crawlerResult.CrawledVideoUrls != null)
                    {
                        links = crawlerResult.CrawledVideoUrls;
                    }

                    File.WriteAllLines(saveFileDialog.FileName, links.ToArray());
                    MessageBox.Show($"Done.\n{links.Count} videos crawled", "done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                saveFileDialog = null;
            }
            catch
            {
                ShowError("There is a problem in crawling.\nPlease contact the programmer (mmojgani77@gmail.com)");
            }
            ApplyControlsEnableStatus(true);
            _started = false;
            btnCrawl.Text = "Start to crawl links";
        }
        private void Stop()
        {
            _cancellationTokenSource.Cancel();
        }

        private CrawlerBase CreateSelectedCrawler(ComboBox comboCrawlers, string username, string password)
        {
            var crawlerInfo = GetSelectedCrawlerInfo();
            if (!crawlerInfo.AuthenticationRequired)
            {
                username = "";
                password = "";
            }
            return (CrawlerBase)Activator.CreateInstance(crawlerInfo.CrawlerType, new object[] { username, password });
        }

        private void comboCrawlers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var crawler = GetSelectedCrawlerInfo();
            btnCrawl.Enabled = IsCrawlerImplemented();
            string prefix = crawler.AuthenticationRequired ? "*" : "";
            lblUsername.Text = $"{prefix}Username :";
            lblPassword.Text = $"{prefix}Password :";
            lblUsername.Enabled = lblPassword.Enabled = usernameBox.Enabled = passwordBox.Enabled = crawler.AuthenticationRequired;
        }

        private bool IsCrawlerImplemented()
        {
            var crawler = GetSelectedCrawlerInfo();
            if (!crawler.Implemented)
            {
                ShowError($"You can't use {crawler.Title} crawler.\nBecause it is not implemented yet.", "Not implemented error");
            }
            return crawler.Implemented;
        }
        private bool IsCourseLinkInCorrectFormat()
        {
            var courseLink = courseLinkBox.Text;
            if (string.IsNullOrWhiteSpace(courseLink))
            {
                ShowError("You should fill the course link box");
                return false;
            }
            var crawler = GetSelectedCrawlerInfo();
            if (crawler.CourseLinkFormat != null)
            {
                if (!crawler.CourseLinkFormat.IsMatch(courseLink))
                {
                    ShowError("Course link format for this crawler (website) is not correct.\nYou should provider main page url of course list.");
                    return false;
                }
            }
            return true;
        }

        private void ShowError(string message, string title = "")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Username = usernameBox.Text;
            Settings.Default.Password = passwordBox.Text;
            Settings.Default.Save();
            _currentCrawler?.Dispose();
        }

        private CrawlerInfoDto GetSelectedCrawlerInfo()
        {
            var index = comboCrawlers.SelectedIndex;
            return _crawlersInfo[index];
        }
    }
}
