using Steganography.Core;
using Steganography.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Steganography
{
    public partial class MainForm : Form
    {
        public const int BitsPerChar = 8; // ASCII only

        private readonly Header header = Header.FromJSON(Resources.DefaultHeader); // Load default header/settings 
        private readonly List<string> allowedImageExtenxsions = new List<string>() { ".jpg", ".png" };
        
        private ISteganography steganography;
        private Bitmap targetImage;
        private int availableBits;

        public MainForm()
        {
            InitializeComponent();
        }

        #region UI Events
        /// <summary>
        /// Copy dragged image and save it to field targetImage if valid
        /// </summary>
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (HasValidPath(e, out string path))
            {
                targetImage = new Bitmap(path);
                e.Effect = DragDropEffects.Copy;
            }
            else
                e.Effect = DragDropEffects.None;
        }
        /// <summary>
        /// Show image, it's info like pixel count etc., enable controls
        /// </summary>
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (targetImage == null)
                return;

            PreviewPictureBox.Image = targetImage;

            ShowInfo();
            EnableGroup(MainGroupBox);
        }
        /// <summary>
        /// Actualize count of available bits to save and show it to user as he types
        /// </summary>
        private void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            var currnet = availableBits - (RichTextBox.Text.Length * BitsPerChar);
            BitLabel.Text = currnet.ToString();
            BitLabel.ForeColor = (currnet < 0) ? Color.Red : Color.Black;
        }

        private void HideTextButt_Click(object sender, EventArgs e)
        {
            var text = RichTextBox.Text;
            var neededBits = text.Length * BitsPerChar + Header.Size;

            CheckImagePrerequisites(neededBits);

            steganography = new TextSteganography(header, targetImage);
            MessageBox.Show(steganography.Hide(RichTextBox.Text) ?
                "Text successfully added to image" : "Input or header was not in correct format");
        }
        private void RevealTextButt_Click(object sender, EventArgs e)
        {
            steganography = new TextSteganography(targetImage);
            var revealedText = steganography.Reveal(targetImage, out _);

            MessageBox.Show(revealedText, "Revealed text");
        }

        private void HideFileButt_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var fileName = dialog.FileName;
            FileInfo info = new FileInfo(fileName);

            CheckImagePrerequisites(info.Length * 8);

            steganography = new FileSteganography(header, targetImage);
            MessageBox.Show(steganography.Hide(fileName) ?
            "File successfully added to image" : "Input or header was not in correct format");
        }
        private void RevealFileButt_Click(object sender, EventArgs e)
        {
            steganography = new FileSteganography(targetImage);
            var rawFileName = steganography.Reveal(targetImage, out var bytes);
            var fileName = rawFileName.Substring(0, rawFileName.IndexOf('~'));

            File.WriteAllBytes(fileName, bytes);
            
            MessageBox.Show(fileName, "Revealed file name");
            Process.Start(Application.StartupPath);
        }

        /// <summary>
        /// Check if filename is valid, save image and open folder
        /// </summary>
        private void SaveButt_Click(object sender, EventArgs e)
        {
            var filename = FileNameBox.Text;

            if (!filename.EndsWith(".png"))
            {
                MessageBox.Show("Invalid extension, only .png allowed");
                return;
            }

            targetImage.Save(FileNameBox.Text, ImageFormat.Png);
            
            MessageBox.Show("Saved");
            Process.Start(Application.StartupPath);
        }
        private void SettingsButt_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(
                Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "Resources\\DefaultHeader.txt");

            MessageBox.Show("Do not forget restart program after change");
            Process.Start(path);
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Validate dropped file
        /// </summary>
        private bool HasValidPath(DragEventArgs e, out string filePath)
        {
            filePath = string.Empty;

            if ((e.AllowedEffect & DragDropEffects.Copy) != DragDropEffects.Copy)
                return false;

            if (e.Data.GetData("FileDrop") is Array data)
            {
                if (data.Length == 1 && data.GetValue(0) is string path)
                {
                    if (HasValidExtension(path))
                    {
                        filePath = path;
                        return true;
                    }
                }
            }

            return false;
        }
        /// <summary>
        /// Check if extenstion belongs to allowedImageExtenxsions
        /// </summary>
        private bool HasValidExtension(string filePath)
        {
            return allowedImageExtenxsions.Contains(Path.GetExtension(filePath).ToLower());
        }
        /// <summary>
        /// Show image size and available bits based on header (settings)
        /// </summary>
        private void ShowInfo()
        {
            availableBits = targetImage.AvailableBits(header);
               
            SizeLabel.Text = $"W={targetImage.Width}, H={targetImage.Height}";
            BitLabel.Text = availableBits.ToString();
            BitLabel.ForeColor = (availableBits < 0) ? Color.Red : Color.Black;
        }
        private void EnableGroup(GroupBox group)
        {
            var controls = group.Controls;

            foreach (var control in controls)
            {
                if (control is Button b)
                    b.Enabled = true;
                else if (control is RichTextBox rt)
                    rt.Enabled = true;
                else if (control is TextBox t)
                    t.Enabled = true;
            }

            WarnLabel.Visible = false;
        }

        private void CheckImagePrerequisites(long neededBits)
        {
            if (neededBits <= availableBits)
                return;

            var result = MessageBox.Show("Do you want to resize image?", "Image is too small", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes)
                return;

            Utils.ResizeImage(ref targetImage, neededBits, header);
            ShowInfo();
        }
        #endregion
    }
}
