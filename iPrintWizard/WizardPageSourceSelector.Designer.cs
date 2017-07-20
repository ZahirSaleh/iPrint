namespace iPrint
{
	partial class WizardPageSourceSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardPageSourceSelector));
            this.label2 = new System.Windows.Forms.Label();
            this.butUSB = new System.Windows.Forms.Button();
            this.butWeb = new System.Windows.Forms.Button();
            this.butBTooth = new System.Windows.Forms.Button();
            this.btnCamera = new System.Windows.Forms.Button();
            this.btnScanner = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label2.Location = new System.Drawing.Point(29, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(427, 29);
            this.label2.TabIndex = 4;
            this.label2.Text = "Select your Picture Source to Proceed.";
            // 
            // butUSB
            // 
            this.butUSB.BackColor = System.Drawing.SystemColors.Window;
            this.butUSB.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butUSB.Image = ((System.Drawing.Image)(resources.GetObject("butUSB.Image")));
            this.butUSB.Location = new System.Drawing.Point(34, 59);
            this.butUSB.Name = "butUSB";
            this.butUSB.Size = new System.Drawing.Size(330, 330);
            this.butUSB.TabIndex = 5;
            this.butUSB.Tag = "1";
            this.butUSB.Text = "USB";
            this.butUSB.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butUSB.UseVisualStyleBackColor = false;
            this.butUSB.Click += new System.EventHandler(this.butUSB_Click);
            // 
            // butWeb
            // 
            this.butWeb.BackColor = System.Drawing.SystemColors.Window;
            this.butWeb.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butWeb.Image = ((System.Drawing.Image)(resources.GetObject("butWeb.Image")));
            this.butWeb.Location = new System.Drawing.Point(402, 59);
            this.butWeb.Name = "butWeb";
            this.butWeb.Size = new System.Drawing.Size(330, 330);
            this.butWeb.TabIndex = 6;
            this.butWeb.Tag = "2";
            this.butWeb.Text = "WEB CODE";
            this.butWeb.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butWeb.UseVisualStyleBackColor = false;
            this.butWeb.Click += new System.EventHandler(this.butWeb_Click);
            // 
            // butBTooth
            // 
            this.butBTooth.BackColor = System.Drawing.SystemColors.Window;
            this.butBTooth.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butBTooth.Image = ((System.Drawing.Image)(resources.GetObject("butBTooth.Image")));
            this.butBTooth.Location = new System.Drawing.Point(770, 59);
            this.butBTooth.Name = "butBTooth";
            this.butBTooth.Size = new System.Drawing.Size(330, 330);
            this.butBTooth.TabIndex = 7;
            this.butBTooth.Tag = "3";
            this.butBTooth.Text = "BLUE TOOTH";
            this.butBTooth.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butBTooth.UseVisualStyleBackColor = false;
            this.butBTooth.Click += new System.EventHandler(this.butBTooth_Click);
            // 
            // btnCamera
            // 
            this.btnCamera.BackColor = System.Drawing.SystemColors.Window;
            this.btnCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCamera.Image = ((System.Drawing.Image)(resources.GetObject("btnCamera.Image")));
            this.btnCamera.Location = new System.Drawing.Point(1138, 59);
            this.btnCamera.Name = "btnCamera";
            this.btnCamera.Size = new System.Drawing.Size(330, 330);
            this.btnCamera.TabIndex = 8;
            this.btnCamera.Tag = "4";
            this.btnCamera.Text = "Camera";
            this.btnCamera.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCamera.UseVisualStyleBackColor = false;
            this.btnCamera.Click += new System.EventHandler(this.btnCamera_Click);
            // 
            // btnScanner
            // 
            this.btnScanner.BackColor = System.Drawing.SystemColors.Window;
            this.btnScanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScanner.Image = ((System.Drawing.Image)(resources.GetObject("btnScanner.Image")));
            this.btnScanner.Location = new System.Drawing.Point(1509, 59);
            this.btnScanner.Name = "btnScanner";
            this.btnScanner.Size = new System.Drawing.Size(330, 330);
            this.btnScanner.TabIndex = 9;
            this.btnScanner.Tag = "5";
            this.btnScanner.Text = "Scanner";
            this.btnScanner.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnScanner.UseVisualStyleBackColor = false;
            this.btnScanner.Click += new System.EventHandler(this.btnScanner_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1339, 372);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(571, 442);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // WizardPageSourceSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.Controls.Add(this.btnScanner);
            this.Controls.Add(this.btnCamera);
            this.Controls.Add(this.butBTooth);
            this.Controls.Add(this.butWeb);
            this.Controls.Add(this.butUSB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Name = "WizardPageSourceSelector";
            this.Size = new System.Drawing.Size(1890, 963);
            this.Subtitle = "Select the appropriate action";
            this.Title = "Step 1 - Select the Source Of your Pictures to Print";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
       
		#endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butUSB;
        private System.Windows.Forms.Button butWeb;
        private System.Windows.Forms.Button butBTooth;
        private System.Windows.Forms.Button btnCamera;
        private System.Windows.Forms.Button btnScanner;
        private System.Windows.Forms.PictureBox pictureBox1;
	}
}
