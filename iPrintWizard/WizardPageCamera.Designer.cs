

namespace iPrint
{
	partial class WizardPageCamera
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
            this.components = new System.ComponentModel.Container();
            this.timDelay = new System.Windows.Forms.Timer(this.components);
            this.timDisplay = new System.Windows.Forms.Timer(this.components);
            this.panelRight = new System.Windows.Forms.Panel();
            this.flpCameraThumbnails = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelVideo = new System.Windows.Forms.Panel();
            this.btnSnapshot = new System.Windows.Forms.Button();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.butDisposePic = new System.Windows.Forms.Button();
            this.butAddPic = new System.Windows.Forms.Button();
            this.picBoxSnapshot = new System.Windows.Forms.PictureBox();
            this.panelRight.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSnapshot)).BeginInit();
            this.SuspendLayout();
            // 
            // timDelay
            // 
            this.timDelay.Interval = 500;
            this.timDelay.Tick += new System.EventHandler(this.timDelay_Tick);
            // 
            // timDisplay
            // 
            this.timDisplay.Tick += new System.EventHandler(this.timDisplay_Tick);
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.flpCameraThumbnails);
            this.panelRight.Controls.Add(this.panel2);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(1025, 0);
            this.panelRight.Margin = new System.Windows.Forms.Padding(20);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(10);
            this.panelRight.Size = new System.Drawing.Size(878, 814);
            this.panelRight.TabIndex = 28;
            // 
            // flpCameraThumbnails
            // 
            this.flpCameraThumbnails.AutoScroll = true;
            this.flpCameraThumbnails.BackColor = System.Drawing.Color.GhostWhite;
            this.flpCameraThumbnails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpCameraThumbnails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCameraThumbnails.Location = new System.Drawing.Point(10, 56);
            this.flpCameraThumbnails.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flpCameraThumbnails.Name = "flpCameraThumbnails";
            this.flpCameraThumbnails.Size = new System.Drawing.Size(858, 748);
            this.flpCameraThumbnails.TabIndex = 30;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(858, 46);
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
            this.panelLeft.Controls.Add(this.panelVideo);
            this.panelLeft.Controls.Add(this.btnSnapshot);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Margin = new System.Windows.Forms.Padding(20);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(10);
            this.panelLeft.Size = new System.Drawing.Size(722, 814);
            this.panelLeft.TabIndex = 29;
            // 
            // panelVideo
            // 
            this.panelVideo.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelVideo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVideo.Location = new System.Drawing.Point(10, 10);
            this.panelVideo.Name = "panelVideo";
            this.panelVideo.Size = new System.Drawing.Size(702, 569);
            this.panelVideo.TabIndex = 22;
            // 
            // btnSnapshot
            // 
            this.btnSnapshot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSnapshot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSnapshot.Location = new System.Drawing.Point(10, 579);
            this.btnSnapshot.Name = "btnSnapshot";
            this.btnSnapshot.Size = new System.Drawing.Size(702, 225);
            this.btnSnapshot.TabIndex = 21;
            this.btnSnapshot.Tag = "6";
            this.btnSnapshot.Text = "Snapshot";
            this.btnSnapshot.UseVisualStyleBackColor = true;
            this.btnSnapshot.Click += new System.EventHandler(this.btnSnapshot_Click);
            // 
            // panelCenter
            // 
            this.panelCenter.Controls.Add(this.butDisposePic);
            this.panelCenter.Controls.Add(this.butAddPic);
            this.panelCenter.Controls.Add(this.picBoxSnapshot);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(722, 0);
            this.panelCenter.Margin = new System.Windows.Forms.Padding(20);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Padding = new System.Windows.Forms.Padding(10);
            this.panelCenter.Size = new System.Drawing.Size(303, 814);
            this.panelCenter.TabIndex = 30;
            // 
            // butDisposePic
            // 
            this.butDisposePic.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.butDisposePic.Enabled = false;
            this.butDisposePic.Location = new System.Drawing.Point(10, 579);
            this.butDisposePic.Name = "butDisposePic";
            this.butDisposePic.Size = new System.Drawing.Size(283, 225);
            this.butDisposePic.TabIndex = 25;
            this.butDisposePic.Text = "<< Dispose";
            this.butDisposePic.UseVisualStyleBackColor = true;
            this.butDisposePic.Click += new System.EventHandler(this.butDisposePic_Click);
            // 
            // butAddPic
            // 
            this.butAddPic.Location = new System.Drawing.Point(10, 312);
            this.butAddPic.Name = "butAddPic";
            this.butAddPic.Size = new System.Drawing.Size(283, 225);
            this.butAddPic.TabIndex = 24;
            this.butAddPic.Text = "Add >>";
            this.butAddPic.UseVisualStyleBackColor = true;
            this.butAddPic.Click += new System.EventHandler(this.butAddPic_Click);
            // 
            // picBoxSnapshot
            // 
            this.picBoxSnapshot.Dock = System.Windows.Forms.DockStyle.Top;
            this.picBoxSnapshot.Location = new System.Drawing.Point(10, 10);
            this.picBoxSnapshot.Name = "picBoxSnapshot";
            this.picBoxSnapshot.Size = new System.Drawing.Size(283, 258);
            this.picBoxSnapshot.TabIndex = 23;
            this.picBoxSnapshot.TabStop = false;
            // 
            // WizardPageCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelRight);
            this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Name = "WizardPageCamera";
            this.Size = new System.Drawing.Size(1903, 814);
            this.Subtitle = "Edit the existing account";
            this.Title = "Step 2B - Existing Account";
            this.panelRight.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSnapshot)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion        

        //private System.Windows.Forms.Timer timAnimation;
        private System.Windows.Forms.Timer timDelay;
        private System.Windows.Forms.Timer timDisplay;
        private string currentpath;
        //private string SnapshotFilesDirectory;
        private bool CountingDown;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.FlowLayoutPanel flpCameraThumbnails;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Button btnSnapshot;
        private System.Windows.Forms.Panel panelVideo;
        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.Button butDisposePic;
        private System.Windows.Forms.Button butAddPic;
        private System.Windows.Forms.PictureBox picBoxSnapshot;
        
	}
}
