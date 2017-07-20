namespace iPrint
{
    partial class WizardPageScanner
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
            this.panelRight = new System.Windows.Forms.Panel();
            this.flpScannerThumbnails = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelScan = new System.Windows.Forms.Panel();
            this.picScan = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnStartScan = new System.Windows.Forms.Button();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.butDisposePic = new System.Windows.Forms.Button();
            this.butAddPic = new System.Windows.Forms.Button();
            this.panelRight.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelScan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picScan)).BeginInit();
            this.panel3.SuspendLayout();
            this.panelCenter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.flpScannerThumbnails);
            this.panelRight.Controls.Add(this.panel2);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(922, 0);
            this.panelRight.Margin = new System.Windows.Forms.Padding(20);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(10);
            this.panelRight.Size = new System.Drawing.Size(981, 872);
            this.panelRight.TabIndex = 29;
            // 
            // flpScannerThumbnails
            // 
            this.flpScannerThumbnails.AutoScroll = true;
            this.flpScannerThumbnails.BackColor = System.Drawing.Color.GhostWhite;
            this.flpScannerThumbnails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpScannerThumbnails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpScannerThumbnails.Location = new System.Drawing.Point(10, 56);
            this.flpScannerThumbnails.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flpScannerThumbnails.Name = "flpScannerThumbnails";
            this.flpScannerThumbnails.Size = new System.Drawing.Size(961, 806);
            this.flpScannerThumbnails.TabIndex = 30;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(961, 46);
            this.panel2.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pictures to be Printed:";
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.panelScan);
            this.panelLeft.Controls.Add(this.panel3);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(599, 872);
            this.panelLeft.TabIndex = 32;
            // 
            // panelScan
            // 
            this.panelScan.Controls.Add(this.picScan);
            this.panelScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScan.Location = new System.Drawing.Point(0, 0);
            this.panelScan.Name = "panelScan";
            this.panelScan.Size = new System.Drawing.Size(599, 693);
            this.panelScan.TabIndex = 10;
            // 
            // picScan
            // 
            this.picScan.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.picScan.Location = new System.Drawing.Point(28, 11);
            this.picScan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picScan.Name = "picScan";
            this.picScan.Size = new System.Drawing.Size(543, 654);
            this.picScan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picScan.TabIndex = 9;
            this.picScan.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnStartScan);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 693);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(599, 179);
            this.panel3.TabIndex = 9;
            // 
            // btnStartScan
            // 
            this.btnStartScan.Location = new System.Drawing.Point(28, 9);
            this.btnStartScan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStartScan.Name = "btnStartScan";
            this.btnStartScan.Size = new System.Drawing.Size(543, 161);
            this.btnStartScan.TabIndex = 8;
            this.btnStartScan.Tag = "7";
            this.btnStartScan.Text = "Scan";
            this.btnStartScan.UseVisualStyleBackColor = true;
            this.btnStartScan.Click += new System.EventHandler(this.btnStartScan_Click);
            // 
            // panelCenter
            // 
            this.panelCenter.Controls.Add(this.butDisposePic);
            this.panelCenter.Controls.Add(this.butAddPic);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(599, 0);
            this.panelCenter.Margin = new System.Windows.Forms.Padding(20);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Padding = new System.Windows.Forms.Padding(10);
            this.panelCenter.Size = new System.Drawing.Size(323, 872);
            this.panelCenter.TabIndex = 33;
            // 
            // butDisposePic
            // 
            this.butDisposePic.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.butDisposePic.Enabled = false;
            this.butDisposePic.Location = new System.Drawing.Point(10, 468);
            this.butDisposePic.Name = "butDisposePic";
            this.butDisposePic.Size = new System.Drawing.Size(303, 394);
            this.butDisposePic.TabIndex = 25;
            this.butDisposePic.Text = "<< Dispose";
            this.butDisposePic.UseVisualStyleBackColor = true;
            // 
            // butAddPic
            // 
            this.butAddPic.Dock = System.Windows.Forms.DockStyle.Top;
            this.butAddPic.Location = new System.Drawing.Point(10, 10);
            this.butAddPic.Name = "butAddPic";
            this.butAddPic.Size = new System.Drawing.Size(303, 387);
            this.butAddPic.TabIndex = 24;
            this.butAddPic.Text = "Add >>";
            this.butAddPic.UseVisualStyleBackColor = true;
            this.butAddPic.Click += new System.EventHandler(this.butAddPic_Click);
            // 
            // WizardPageScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelRight);
            this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Name = "WizardPageScanner";
            this.Size = new System.Drawing.Size(1903, 872);
            this.Subtitle = "Step 2c - Place your pictures in the Scanner and press Scan to beggin";
            this.Title = "Scan the pictures to print";
            this.panelRight.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelScan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picScan)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panelCenter.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.FlowLayoutPanel flpScannerThumbnails;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.Button butDisposePic;
        private System.Windows.Forms.Button butAddPic;
        private System.Windows.Forms.Panel panelScan;
        private System.Windows.Forms.PictureBox picScan;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnStartScan;



    }
}
