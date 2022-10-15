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
        private readonly Header header = Header.FromJSON(Resources.DefaultHeader); // Load default header/settings 
        private readonly List<string> allowedImageExtenxsions = new List<string>() { ".jpg", ".png" };
        
        private ISteganography steganography;
        private Bitmap targetImage;

        private bool typeMissmatch;
        private int availableBits;


        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called from BaseSteganography if bad type was used to reveal image info
        /// </summary>
        public void HandleTypeMissmatch(string type, Header header)
        {
            typeMissmatch = true;

            MessageBox.Show($"Message of type {header.MsgType} found, but {type} was used. " +
                $"Try to use {(type == "TextSteganography" ? "FileSteganography" : "TextSteganography")}");
        }

        #region UI Events
        /// <summary>
        /// Copy dragged image and save it to field targetImage if its valid
        /// </summary>
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (IsDraggedFileValid(e, out string path))
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
        /// Actualize count of available bits when text is changed
        /// </summary>
        private void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            var currnet = availableBits - (RichTextBox.Text.Length * Header.BitsPerChar);
            BitLabel.Text = currnet.ToString();
            BitLabel.ForeColor = (currnet < 0) ? Color.Red : Color.Black;
        }

        /// <summary>
        /// Create instance of TextSteganography and if input is valid hide text message to image
        /// </summary>
        private void HideTextButt_Click(object sender, EventArgs e)
        {
            var text = RichTextBox.Text;
            var neededBits = text.Length * Header.BitsPerChar + Header.Size;

            if (!CheckPrerequisites(neededBits))
                return;

            steganography = new TextSteganography(header, targetImage);
            MessageBox.Show(steganography.Hide(RichTextBox.Text) ?
                "Text successfully added to image" : "Input or header was not in correct format");
        }
        /// <summary>
        /// Create instance of TextSteganography and if message is text show it to user
        /// </summary>
        private void RevealTextButt_Click(object sender, EventArgs e)
        {
            typeMissmatch = false;
            steganography = new TextSteganography(targetImage, this);

            if (typeMissmatch)
                return;

            MessageBox.Show(steganography.Reveal(targetImage, out _), "Revealed text");
        }

        /// <summary>
        /// Let user pick file, check if its file name is valid and if source image is big enough, 
        /// create instance of FileSteganography and hide file in image
        /// </summary>
        private void HideFileButt_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var fileName = dialog.FileName;
            FileInfo info = new FileInfo(fileName);

            if (!CheckPrerequisites(info.Length * 8, fileName))
                return;

            steganography = new FileSteganography(header, targetImage);
            MessageBox.Show(steganography.Hide(fileName) ?
                "File successfully added to image" : "Input or header was not in correct format");
        }
        /// <summary>
        /// Get bytes from image and save it to file
        /// </summary>
        private void RevealFileButt_Click(object sender, EventArgs e)
        {
            typeMissmatch = false;
            steganography = new FileSteganography(targetImage, this);

            if (typeMissmatch)
                return;

            var rawFileName = steganography.Reveal(targetImage, out var bytes);
            var fileName = rawFileName.Substring(0, rawFileName.IndexOf('~'));

            File.WriteAllBytes(fileName, bytes);
            
            MessageBox.Show(fileName, "Revealed file name");
            Process.Start(Application.StartupPath);
        }

        /// <summary>
        /// Check if image contains steganography
        /// </summary>
        private void CheckButt_Click(object sender, EventArgs e)
        {
            SteganographyDetection detection = new SteganographyDetection(targetImage);
            
            MessageBox.Show(detection.IsSteganography(7) ?
                "Steganography detected" : "Steganography NOT detected");
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

            targetImage.Save(filename, ImageFormat.Png);
            
            MessageBox.Show("Saved");
            Process.Start(Application.StartupPath);
        }
        private void SettingsButt_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(
                Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, Header.DefaultHeaderPath);

            MessageBox.Show("Do not forget restart program after change");
            Process.Start(path);
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Validate dropped file
        /// </summary>
        private bool IsDraggedFileValid(DragEventArgs e, out string filePath)
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
        /// <summary>
        /// Enable controls after valid image is drag & dropped
        /// </summary>
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

        /// <summary>
        /// Checks if there is enough space in image (if not asks user to resize it) 
        /// and if length of name of image to save is <= Header.MaxNameLength
        /// </summary>
        private bool CheckPrerequisites(long neededBits, string fileName = "")
        {
            if (fileName.Length > Header.MaxNameLength)
            {
                MessageBox.Show($"File name should be <= {Header.MaxNameLength}");
                return false;
            }

            if (neededBits <= availableBits)
                return true;

            var result = MessageBox.Show("Do you want to resize image?", "Image is too small", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes)
                return false;

            Utils.ResizeImage(ref targetImage, neededBits, header);
            ShowInfo();

            return true;
        }
        #endregion
    }
}
