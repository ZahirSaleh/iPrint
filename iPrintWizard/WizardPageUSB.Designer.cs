namespace iPrint
{
	partial class WizardPageUSB
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cBoxAutoResize = new System.Windows.Forms.CheckBox();
            this.panelProgress = new System.Windows.Forms.Panel();
            this.labDone = new System.Windows.Forms.Label();
            this.progBarUSB = new System.Windows.Forms.ProgressBar();
            this.flpThumbnails = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 669);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1692, 80);
            this.panel1.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cBoxAutoResize);
            this.groupBox1.Location = new System.Drawing.Point(4, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(270, 63);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Picture Editing";
            // 
            // cBoxAutoResize
            // 
            this.cBoxAutoResize.AutoSize = true;
            this.cBoxAutoResize.Location = new System.Drawing.Point(11, 27);
            this.cBoxAutoResize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cBoxAutoResize.Name = "cBoxAutoResize";
            this.cBoxAutoResize.Size = new System.Drawing.Size(233, 21);
            this.cBoxAutoResize.TabIndex = 9;
            this.cBoxAutoResize.Text = "Automatically Resize all Pictures";
            this.cBoxAutoResize.UseVisualStyleBackColor = true;
            this.cBoxAutoResize.CheckedChanged += new System.EventHandler(this.cBoxAutoResize_CheckedChanged);
            // 
            // panelProgress
            // 
            this.panelProgress.Controls.Add(this.labDone);
            this.panelProgress.Controls.Add(this.progBarUSB);
            this.panelProgress.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelProgress.Location = new System.Drawing.Point(0, 0);
            this.panelProgress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(1692, 31);
            this.panelProgress.TabIndex = 10;
            // 
            // labDone
            // 
            this.labDone.AutoSize = true;
            this.labDone.BackColor = System.Drawing.Color.Transparent;
            this.labDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDone.Location = new System.Drawing.Point(749, 6);
            this.labDone.Name = "labDone";
            this.labDone.Size = new System.Drawing.Size(78, 17);
            this.labDone.TabIndex = 8;
            this.labDone.Text = "!! DONE !!";
            // 
            // progBarUSB
            // 
            this.progBarUSB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progBarUSB.Location = new System.Drawing.Point(4, 3);
            this.progBarUSB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progBarUSB.Name = "progBarUSB";
            this.progBarUSB.Size = new System.Drawing.Size(1684, 22);
            this.progBarUSB.TabIndex = 7;
            // 
            // flpThumbnails
            // 
            this.flpThumbnails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpThumbnails.AutoScroll = true;
            this.flpThumbnails.BackColor = System.Drawing.Color.GhostWhite;
            this.flpThumbnails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpThumbnails.Location = new System.Drawing.Point(0, 31);
            this.flpThumbnails.Margin = new System.Windows.Forms.Padding(4);
            this.flpThumbnails.Name = "flpThumbnails";
            this.flpThumbnails.Size = new System.Drawing.Size(1692, 638);
            this.flpThumbnails.TabIndex = 11;
            this.flpThumbnails.VisibleChanged += new System.EventHandler(this.flpThumbnails_VisibleChanged);
            // 
            // WizardPageUSB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.Controls.Add(this.flpThumbnails);
            this.Controls.Add(this.panelProgress);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "WizardPageUSB";
            this.Size = new System.Drawing.Size(1692, 749);
            this.Subtitle = "Select by clicking the pictures to print";
            this.Title = "Step 2b - Select Pictures to Print";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cBoxAutoResize;
        private System.Windows.Forms.Panel panelProgress;
        private System.Windows.Forms.Label labDone;
        private System.Windows.Forms.ProgressBar progBarUSB;
        private System.Windows.Forms.FlowLayoutPanel flpThumbnails;

    }
}
