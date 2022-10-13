
namespace Steganography
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.PreviewPictureBox = new System.Windows.Forms.PictureBox();
            this.MainGroupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.FileNameBox = new System.Windows.Forms.TextBox();
            this.SaveButt = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.RichTextBox = new System.Windows.Forms.RichTextBox();
            this.BitLabel = new System.Windows.Forms.Label();
            this.SizeLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.RevealFileButt = new System.Windows.Forms.Button();
            this.RevealTextButt = new System.Windows.Forms.Button();
            this.HideFileButt = new System.Windows.Forms.Button();
            this.HideTextButt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).BeginInit();
            this.MainGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // PreviewPictureBox
            // 
            this.PreviewPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("PreviewPictureBox.Image")));
            this.PreviewPictureBox.Location = new System.Drawing.Point(12, 12);
            this.PreviewPictureBox.Name = "PreviewPictureBox";
            this.PreviewPictureBox.Size = new System.Drawing.Size(400, 400);
            this.PreviewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PreviewPictureBox.TabIndex = 0;
            this.PreviewPictureBox.TabStop = false;
            // 
            // MainGroupBox
            // 
            this.MainGroupBox.Controls.Add(this.label4);
            this.MainGroupBox.Controls.Add(this.FileNameBox);
            this.MainGroupBox.Controls.Add(this.SaveButt);
            this.MainGroupBox.Controls.Add(this.label3);
            this.MainGroupBox.Controls.Add(this.RichTextBox);
            this.MainGroupBox.Controls.Add(this.BitLabel);
            this.MainGroupBox.Controls.Add(this.SizeLabel);
            this.MainGroupBox.Controls.Add(this.label2);
            this.MainGroupBox.Controls.Add(this.label1);
            this.MainGroupBox.Controls.Add(this.RevealFileButt);
            this.MainGroupBox.Controls.Add(this.RevealTextButt);
            this.MainGroupBox.Controls.Add(this.HideFileButt);
            this.MainGroupBox.Controls.Add(this.HideTextButt);
            this.MainGroupBox.Location = new System.Drawing.Point(418, 12);
            this.MainGroupBox.Name = "MainGroupBox";
            this.MainGroupBox.Size = new System.Drawing.Size(400, 400);
            this.MainGroupBox.TabIndex = 1;
            this.MainGroupBox.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 280);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "File name:";
            // 
            // FileNameBox
            // 
            this.FileNameBox.Enabled = false;
            this.FileNameBox.Location = new System.Drawing.Point(87, 277);
            this.FileNameBox.Name = "FileNameBox";
            this.FileNameBox.Size = new System.Drawing.Size(100, 20);
            this.FileNameBox.TabIndex = 11;
            this.FileNameBox.Text = "output.png";
            // 
            // SaveButt
            // 
            this.SaveButt.Enabled = false;
            this.SaveButt.Location = new System.Drawing.Point(193, 275);
            this.SaveButt.Name = "SaveButt";
            this.SaveButt.Size = new System.Drawing.Size(75, 23);
            this.SaveButt.TabIndex = 10;
            this.SaveButt.Text = "Save";
            this.SaveButt.UseVisualStyleBackColor = true;
            this.SaveButt.Click += new System.EventHandler(this.SaveButt_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Text:";
            // 
            // RichTextBox
            // 
            this.RichTextBox.Enabled = false;
            this.RichTextBox.Location = new System.Drawing.Point(6, 157);
            this.RichTextBox.Name = "RichTextBox";
            this.RichTextBox.Size = new System.Drawing.Size(262, 62);
            this.RichTextBox.TabIndex = 8;
            this.RichTextBox.Text = "";
            this.RichTextBox.TextChanged += new System.EventHandler(this.RichTextBox_TextChanged);
            // 
            // BitLabel
            // 
            this.BitLabel.AutoSize = true;
            this.BitLabel.Location = new System.Drawing.Point(84, 253);
            this.BitLabel.Name = "BitLabel";
            this.BitLabel.Size = new System.Drawing.Size(16, 13);
            this.BitLabel.TabIndex = 7;
            this.BitLabel.Text = "---";
            // 
            // SizeLabel
            // 
            this.SizeLabel.AutoSize = true;
            this.SizeLabel.Location = new System.Drawing.Point(84, 231);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(16, 13);
            this.SizeLabel.TabIndex = 6;
            this.SizeLabel.Text = "---";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 253);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Available bits:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Image size:";
            // 
            // RevealFileButt
            // 
            this.RevealFileButt.Enabled = false;
            this.RevealFileButt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RevealFileButt.Location = new System.Drawing.Point(148, 82);
            this.RevealFileButt.Name = "RevealFileButt";
            this.RevealFileButt.Size = new System.Drawing.Size(120, 50);
            this.RevealFileButt.TabIndex = 3;
            this.RevealFileButt.Text = "Reveal file";
            this.RevealFileButt.UseVisualStyleBackColor = true;
            // 
            // RevealTextButt
            // 
            this.RevealTextButt.Enabled = false;
            this.RevealTextButt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RevealTextButt.Location = new System.Drawing.Point(6, 82);
            this.RevealTextButt.Name = "RevealTextButt";
            this.RevealTextButt.Size = new System.Drawing.Size(120, 50);
            this.RevealTextButt.TabIndex = 2;
            this.RevealTextButt.Text = "Reveal text";
            this.RevealTextButt.UseVisualStyleBackColor = true;
            this.RevealTextButt.Click += new System.EventHandler(this.RevealTextButt_Click);
            // 
            // HideFileButt
            // 
            this.HideFileButt.Enabled = false;
            this.HideFileButt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.HideFileButt.Location = new System.Drawing.Point(148, 19);
            this.HideFileButt.Name = "HideFileButt";
            this.HideFileButt.Size = new System.Drawing.Size(120, 50);
            this.HideFileButt.TabIndex = 1;
            this.HideFileButt.Text = "Hide file";
            this.HideFileButt.UseVisualStyleBackColor = true;
            // 
            // HideTextButt
            // 
            this.HideTextButt.Enabled = false;
            this.HideTextButt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.HideTextButt.Location = new System.Drawing.Point(6, 19);
            this.HideTextButt.Name = "HideTextButt";
            this.HideTextButt.Size = new System.Drawing.Size(120, 50);
            this.HideTextButt.TabIndex = 0;
            this.HideTextButt.Text = "Hide text";
            this.HideTextButt.UseVisualStyleBackColor = true;
            this.HideTextButt.Click += new System.EventHandler(this.HideTextButt_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 423);
            this.Controls.Add(this.MainGroupBox);
            this.Controls.Add(this.PreviewPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Steganography";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).EndInit();
            this.MainGroupBox.ResumeLayout(false);
            this.MainGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PreviewPictureBox;
        private System.Windows.Forms.GroupBox MainGroupBox;
        private System.Windows.Forms.Button RevealTextButt;
        private System.Windows.Forms.Button HideFileButt;
        private System.Windows.Forms.Button HideTextButt;
        private System.Windows.Forms.Button RevealFileButt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label BitLabel;
        private System.Windows.Forms.Label SizeLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox RichTextBox;
        private System.Windows.Forms.Button SaveButt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox FileNameBox;
    }
}

