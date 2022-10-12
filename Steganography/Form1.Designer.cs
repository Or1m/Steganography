
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
            this.label3 = new System.Windows.Forms.Label();
            this.RichTextBox = new System.Windows.Forms.RichTextBox();
            this.BitLabel = new System.Windows.Forms.Label();
            this.SizeLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UnhideFileButt = new System.Windows.Forms.Button();
            this.UnhideTextButt = new System.Windows.Forms.Button();
            this.HideFileButt = new System.Windows.Forms.Button();
            this.HiideTextButt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewPictureBox)).BeginInit();
            this.MainGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // PreviewPictureBox
            // 
            this.PreviewPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("PreviewPictureBox.Image")));
            this.PreviewPictureBox.Location = new System.Drawing.Point(12, 12);
            this.PreviewPictureBox.Name = "PreviewPictureBox";
            this.PreviewPictureBox.Size = new System.Drawing.Size(313, 297);
            this.PreviewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PreviewPictureBox.TabIndex = 0;
            this.PreviewPictureBox.TabStop = false;
            // 
            // MainGroupBox
            // 
            this.MainGroupBox.Controls.Add(this.label3);
            this.MainGroupBox.Controls.Add(this.RichTextBox);
            this.MainGroupBox.Controls.Add(this.BitLabel);
            this.MainGroupBox.Controls.Add(this.SizeLabel);
            this.MainGroupBox.Controls.Add(this.label2);
            this.MainGroupBox.Controls.Add(this.label1);
            this.MainGroupBox.Controls.Add(this.UnhideFileButt);
            this.MainGroupBox.Controls.Add(this.UnhideTextButt);
            this.MainGroupBox.Controls.Add(this.HideFileButt);
            this.MainGroupBox.Controls.Add(this.HiideTextButt);
            this.MainGroupBox.Location = new System.Drawing.Point(338, 6);
            this.MainGroupBox.Name = "MainGroupBox";
            this.MainGroupBox.Size = new System.Drawing.Size(274, 304);
            this.MainGroupBox.TabIndex = 1;
            this.MainGroupBox.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Text:";
            // 
            // RichTextBox
            // 
            this.RichTextBox.Enabled = false;
            this.RichTextBox.Location = new System.Drawing.Point(6, 168);
            this.RichTextBox.Name = "RichTextBox";
            this.RichTextBox.Size = new System.Drawing.Size(262, 62);
            this.RichTextBox.TabIndex = 8;
            this.RichTextBox.Text = "";
            this.RichTextBox.TextChanged += new System.EventHandler(this.RichTextBox_TextChanged);
            // 
            // BitLabel
            // 
            this.BitLabel.AutoSize = true;
            this.BitLabel.Location = new System.Drawing.Point(84, 279);
            this.BitLabel.Name = "BitLabel";
            this.BitLabel.Size = new System.Drawing.Size(16, 13);
            this.BitLabel.TabIndex = 7;
            this.BitLabel.Text = "---";
            // 
            // SizeLabel
            // 
            this.SizeLabel.AutoSize = true;
            this.SizeLabel.Location = new System.Drawing.Point(84, 257);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(16, 13);
            this.SizeLabel.TabIndex = 6;
            this.SizeLabel.Text = "---";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 279);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Available bits:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 257);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Image size:";
            // 
            // UnhideFileButt
            // 
            this.UnhideFileButt.Enabled = false;
            this.UnhideFileButt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.UnhideFileButt.Location = new System.Drawing.Point(148, 82);
            this.UnhideFileButt.Name = "UnhideFileButt";
            this.UnhideFileButt.Size = new System.Drawing.Size(120, 50);
            this.UnhideFileButt.TabIndex = 3;
            this.UnhideFileButt.Text = "Unhide file";
            this.UnhideFileButt.UseVisualStyleBackColor = true;
            // 
            // UnhideTextButt
            // 
            this.UnhideTextButt.Enabled = false;
            this.UnhideTextButt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.UnhideTextButt.Location = new System.Drawing.Point(148, 19);
            this.UnhideTextButt.Name = "UnhideTextButt";
            this.UnhideTextButt.Size = new System.Drawing.Size(120, 50);
            this.UnhideTextButt.TabIndex = 2;
            this.UnhideTextButt.Text = "Unhide text";
            this.UnhideTextButt.UseVisualStyleBackColor = true;
            // 
            // HideFileButt
            // 
            this.HideFileButt.Enabled = false;
            this.HideFileButt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.HideFileButt.Location = new System.Drawing.Point(6, 82);
            this.HideFileButt.Name = "HideFileButt";
            this.HideFileButt.Size = new System.Drawing.Size(120, 50);
            this.HideFileButt.TabIndex = 1;
            this.HideFileButt.Text = "Hide file";
            this.HideFileButt.UseVisualStyleBackColor = true;
            // 
            // HiideTextButt
            // 
            this.HiideTextButt.Enabled = false;
            this.HiideTextButt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.HiideTextButt.Location = new System.Drawing.Point(6, 19);
            this.HiideTextButt.Name = "HiideTextButt";
            this.HiideTextButt.Size = new System.Drawing.Size(120, 50);
            this.HiideTextButt.TabIndex = 0;
            this.HiideTextButt.Text = "Hide text";
            this.HiideTextButt.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 321);
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
        private System.Windows.Forms.Button UnhideTextButt;
        private System.Windows.Forms.Button HideFileButt;
        private System.Windows.Forms.Button HiideTextButt;
        private System.Windows.Forms.Button UnhideFileButt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label BitLabel;
        private System.Windows.Forms.Label SizeLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox RichTextBox;
    }
}

