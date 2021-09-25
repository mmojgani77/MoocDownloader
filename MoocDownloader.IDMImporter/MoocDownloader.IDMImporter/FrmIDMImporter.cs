using MoocDownloader.IDMImporter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MoocDownloader.IDMImporter
{
    internal partial class FrmIDMImporter : Form
    {
        public FrmIDMImporter()
        {
            InitializeComponent();
            idmPathBox.Text = Settings.Default.IDMPath;
            UpdateSampleFileName();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "IDM Executable File|idman.exe"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                idmPathBox.Text = openFileDialog.FileName;
                Settings.Default.IDMPath = openFileDialog.FileName;
                Settings.Default.Save();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Text File Containing Links|*.txt"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                linksPathBox.Text = "";
                var fileLines = File.ReadAllLines(openFileDialog.FileName);
                if (IsMatchLinks(fileLines))
                {
                    linksPathBox.Text = openFileDialog.FileName;
                    txtFileNameExtension.Text = ExtractFileExtesnion(fileLines);
                }
                else
                {
                    ShowError("The file is not containing links for importing to IDM");
                }
            }
        }

        private bool IsMatchLinks(string[] lines)
        {
            var regex = new Regex(@"^(http|https|ftp):\/\/.+$");
            return lines.All(x => regex.IsMatch(x));
        }
        private string ExtractFileExtesnion(string[] lines)
        {
            try
            {
                foreach (var line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line) && Path.HasExtension(line))
                        return Path.GetExtension(line).Substring(1);
                }
            }
            catch
            {

            }
            return "";
        }

        private void ShowError(string message, string title = "An error occured")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtFileNamePrefix_TextChanged(object sender, EventArgs e)
        {
            UpdateSampleFileName();
        }

        private void UpdateSampleFileName()
        {
            var sampleNum = (int)Math.Max(numStartFrom.Value, 1);
            string sampleExtension = GetLinkFileExtension(null);
            lblFileNameSample.Text = txtFileNamePrefix.Text + GetNumberWithSettings(sampleNum) + txtFileNameSuffix.Text + "." + sampleExtension;
        }
        private string GetLinkFileExtension(string link)
        {
            if (!string.IsNullOrWhiteSpace(link))
            {
                var ext = Path.GetExtension(link);
                if (!string.IsNullOrWhiteSpace(ext))
                    return ext.Trim('.');
            }

            var defExt = txtFileNameExtension.Text;
            if (!string.IsNullOrWhiteSpace(defExt))
                return defExt.Trim('.');

            return "unknown";
        }

        private string CreateFileName(string link, int number)
        {
            var numberGenerated = GetNumberWithSettings(number);
            var ext = GetLinkFileExtension(link);
            return txtFileNamePrefix.Text + numberGenerated + txtFileNameSuffix.Text + "." + ext;
        }

        private string GetNumberWithSettings(int number)
        {
            int wildCardSize = (int)numWildcardSize.Value;
            string format = $"D{wildCardSize}";
            return string.Format("{0:" + format + "}", number);
        }

        private void txtFileNameSuffix_TextChanged(object sender, EventArgs e)
        {
            UpdateSampleFileName();
        }

        private void txtFileNameExtension_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFileNameExtension.Text))
            {
                ShowError("Default extension field can not be empty");
                txtFileNameExtension.Text = "unknown";
            }
            txtFileNameExtension.Text = txtFileNameExtension.Text.Trim('.');
            UpdateSampleFileName();
        }

        private void numStartFrom_ValueChanged(object sender, EventArgs e)
        {
            UpdateSampleFileName();
        }

        private void numWildcardSize_ValueChanged(object sender, EventArgs e)
        {
            UpdateSampleFileName();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                destinationFolderPathBox.Text = folderDialog.SelectedPath;
            }
        }

        private void StartTheProcess()
        {
            if (ValidateData())
            {
                ApplyControlsEnableStatus(false);
                var lines = File.ReadAllLines(linksPathBox.Text);
                var idmExecutableFilePath = idmPathBox.Text;
                int fromNumber = (int)numStartFrom.Value;
                var destinationFolder = destinationFolderPathBox.Text;
                importProgress.Maximum = lines.Length;
                foreach (var link in lines)
                {
                    if (!string.IsNullOrWhiteSpace(link))
                    {
                        var fileName = CreateFileName(link, fromNumber++);
                        if (!AddSingleFileToIDMQueue(idmExecutableFilePath, link, fileName, destinationFolder))
                        {
                            SetStatusBar(StatusBarTypes.ImportError);
                            ShowError("There was a problem in importing links to IDM");
                            return;
                        }
                        importProgress.Value++;
                    }
                }
                SetStatusBar(StatusBarTypes.Imported);
            }
            else
            {
                SetStatusBar(StatusBarTypes.ValidationError);
            }
            ApplyControlsEnableStatus(true);
        }

        private void ApplyControlsEnableStatus(bool enabled)
        {
            foreach (Control control in this.Controls)
            {
                if (control is ProgressBar)
                    continue;
                control.Enabled = enabled;
            }
            this.Enabled = true;
        }

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(idmPathBox.Text) || !File.Exists(idmPathBox.Text))
            {
                ShowError("You should select the path of \"idman.exe\" file in Internet download manager folder to start import process.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(linksPathBox.Text) || !File.Exists(linksPathBox.Text))
            {
                ShowError("You should select a text file containing download links to start import process.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFileNameExtension.Text))
            {
                ShowError("Default file extesnion is a required field and will be applied when imported link has no file extension.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(destinationFolderPathBox.Text))
            {
                ShowError("You should select the path of destination download folder to start import process.");
                return false;
            }

            if (!IsMatchLinks(File.ReadAllLines(linksPathBox.Text)))
            {
                ShowError("The selected file is not containing links for importing to IDM");
                return false;
            }

            return true;
        }

        private bool AddSingleFileToIDMQueue(string idmExecutableFile, string url, string fileName, string destinationFolder)
        {
            try
            {
                string arguments = $"/d \"{url}\" /p \"{destinationFolder}\" /f \"{fileName}\" /a";
                var process = Process.Start(new ProcessStartInfo(idmExecutableFile, arguments)
                {
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
                process.WaitForExit();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SetStatusBar(StatusBarTypes.NotStarted);
            importProgress.Value = 0;
            StartTheProcess();
        }

        private void SetStatusBar(StatusBarTypes status)
        {
            switch (status)
            {
                case StatusBarTypes.NotStarted:
                    lblStatus.Text = "Not Started";
                    lblStatus.BackColor = Color.FromArgb(64, 64, 64);
                    break;
                case StatusBarTypes.Imported:
                    lblStatus.Text = "Imported";
                    lblStatus.BackColor = Color.Green;
                    break;
                case StatusBarTypes.ValidationError:
                    lblStatus.Text = "Validation Error";
                    lblStatus.BackColor = Color.Red;
                    break;
                case StatusBarTypes.ImportError:
                    lblStatus.Text = "Import Error";
                    lblStatus.BackColor = Color.Red;
                    break;
                case StatusBarTypes.Importing:
                    lblStatus.Text = "Importing ...";
                    lblStatus.BackColor = Color.FromArgb(64, 64, 64);
                    break;
                default:
                    lblStatus.Text = "";
                    lblStatus.BackColor = Color.FromArgb(64, 64, 64);
                    break;
            }
        }
    }
}
