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

            if (currnet < 0)
                BitLabel.ForeColor = Color.Red;
            else
                BitLabel.ForeColor = Color.Black;
        }

        private void HideTextButt_Click(object sender, EventArgs e)
        {
            steganography = new TextSteganography(header, targetImage);

            MessageBox.Show(steganography.Hide(RichTextBox.Text) ?
                "Text successfully added to image" : "Input or header was not in correct format");
        }
        private void RevealTextButt_Click(object sender, EventArgs e)
        {
            steganography = new TextSteganography(targetImage);
            var revealedText = steganography.Reveal(targetImage, out _);

            Console.WriteLine(revealedText);
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
            var size = targetImage.Size;
            var rawPixelCount = (size.Width / header.StepX) * (size.Height / header.StepY);
            var availablePixelCount = rawPixelCount - header.FirstX - (header.FirstY * targetImage.Width);
            availableBits = availablePixelCount * (byte)header.ValidPixelChannels - Header.Size;
               
            SizeLabel.Text = $"W={size.Width}, H={size.Height}";
            BitLabel.Text = availableBits.ToString();
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
        }
        #endregion
    }
}
