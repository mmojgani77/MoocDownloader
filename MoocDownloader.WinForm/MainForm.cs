using MoocDownloader.Shared.Models.Base;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MoocDownloader.Shared.Models.DataTransferObjects;
using System.Threading;
using System.IO;
using MoocDownloader.WinForm.Assets;
using MoocDownloader.Shared.Models.Services;
using MoocDownloader.WinForm.Models;
using MoocDownloader.WinForm.Models.Components;

namespace MoocDownloader.WinForm
{
    public partial class MainForm : Form
    {
        private CrawlerService _crawlerService;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _started;
        public MainForm()
        {
            InitializeComponent();
            (menuStrip1.Items[0] as ToolStripMenuItem).DropDownItems[0].Click += IDMImporterMenuItem_Click;
            _started = false;
        }

        private void IDMImporterMenuItem_Click(object sender, EventArgs e)
        {
            var importer = new IDMImporter.IDMImporter();
            importer.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            usernameBox.Text = Settings.Default.Username;
            passwordBox.Text = Settings.Default.Password;
            LoadAllCrawlers(comboCrawlers);
        }

        private void LoadAllCrawlers(ComboBox combo)
        {
            _crawlerService = new CrawlerService();
            var listOfTitles = _crawlerService.GetAllCrawlersTitle();
            combo.Items.AddRange(listOfTitles);
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
            if (!_started && IsCourseLinkValid())
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

                    var response = await _crawlerService.CrawlWithCrawlerIndex(comboCrawlers.SelectedIndex, new CrawlRequestDto
                    {
                        CourseLink = courseLink,
                        FromPage = fromPage,
                        ToPage = toPage,
                        Password = password,
                        Username = username,
                        Progress = Progress,
                        StoppingToken = _cancellationTokenSource.Token
                    });

                    if (response.HasError || response.Result == null)
                    {
                        ShowError(response.ErrorText);
                    }
                    else
                    {
                        File.WriteAllLines(saveFileDialog.FileName, response.Result.ToArray());
                        MessageBox.Show($"Done.\n{response.Result.Count} videos crawled", "done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                saveFileDialog = null;
            }
            catch (Exception ex)
            {
                ShowError($"There is a problem in crawling.\nPlease contact the programmer (mmojgani77@gmail.com)\n\nMessage:{ex.Message}\n\nStackTrace{ex.StackTrace}");
            }
            ApplyControlsEnableStatus(true);
            _started = false;
            btnCrawl.Text = "Start to crawl links";
        }
        private void Stop()
        {
            _cancellationTokenSource?.Cancel();
        }

        private void comboCrawlers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var crawlerInfo = GetSelectedCrawlerInfo();
            var crawlerImplemented = crawlerInfo?.Implemented ?? false;
            var crawlerAuthenticationRequired = crawlerInfo?.AuthenticationRequired ?? false;


            btnCrawl.Enabled = crawlerImplemented;
            string prefix = crawlerAuthenticationRequired ? "*" : "";
            lblUsername.Text = $"{prefix}Username :";
            lblPassword.Text = $"{prefix}Password :";
            lblUsername.Enabled = lblPassword.Enabled = usernameBox.Enabled = passwordBox.Enabled = crawlerAuthenticationRequired;

            if (!crawlerImplemented)
            {
                ShowError($"You can't use this crawler.\nBecause it is not implemented yet.", "Not implemented yet error");
            }
        }

        private void ShowError(string message, string title = "An error occured")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Username = usernameBox.Text;
            Settings.Default.Password = passwordBox.Text;
            Settings.Default.Save();
        }

        private CrawlerInfo GetSelectedCrawlerInfo()
        {
            int index = comboCrawlers.SelectedIndex;
            var crawlerInfo = _crawlerService.FindCrawlerWithIndex(index);
            return crawlerInfo;
        }

        private bool IsCourseLinkValid()
        {
            var courseLink = courseLinkBox.Text;
            if (string.IsNullOrWhiteSpace(courseLink))
            {
                ShowError("You should fill the course link box");
                return false;
            }

            bool isCourseLinkValid = GetSelectedCrawlerInfo()?.IsUrlMatchToCrawler(courseLink) ?? false;
            if (!isCourseLinkValid)
            {
                ShowError("Course link format for this crawler (website) is not correct.\nYou should provider main page url of course list.");
                return false;
            }

            return isCourseLinkValid;
        }

    }
}
