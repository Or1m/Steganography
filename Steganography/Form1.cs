using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Steganography
{
    public partial class MainForm : Form
    {
        private const int numOfChannels = 3; // RGB only
        private const int charBits = 7; // ASCII

        private readonly List<string> allowedImageExtenxsions = new List<string>() { ".jpg", ".png" };
        
        private Bitmap targetImage;
        private int availableBits;

        public MainForm()
        {
            InitializeComponent();
        }

        #region UI Events
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
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (targetImage == null)
                return;

            PreviewPictureBox.Image = targetImage;

            ShowInfo();
            EnableGroup(MainGroupBox);

            Console.WriteLine(Convert.ToString((byte)'A', 2));

            // Create Instance of sth and add image to it
        }
        
        private void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            var currnet = availableBits - (RichTextBox.Text.Length * charBits);
            BitLabel.Text = currnet.ToString();

            if (currnet < 0)
                BitLabel.ForeColor = Color.Red;
            else
                BitLabel.ForeColor = Color.Black;
        }
        #endregion

        #region Helpers
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
        private bool HasValidExtension(string filePath)
        {
            return allowedImageExtenxsions.Contains(Path.GetExtension(filePath).ToLower());
        }
        private void ShowInfo()
        {
            var size = targetImage.Size;
            var pixelCount = size.Width * size.Height;
            availableBits = pixelCount * numOfChannels;

            SizeLabel.Text = $"W={size.Width}, H={size.Height} (WxH={pixelCount})";
            BitLabel.Text = availableBits.ToString();
        }
        private void EnableGroup(GroupBox group)
        {
            var controls = group.Controls;
            foreach (var control in controls)
            {
                if (control is Button b)
                    b.Enabled = true;
                else if (control is RichTextBox t)
                    t.Enabled = true;
            }
        }
        #endregion
    }
}
