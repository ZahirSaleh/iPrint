namespace iPrint
{
	partial class WizardPagePayment
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
            this.button1 = new System.Windows.Forms.Button();
            this.progBarRetry = new System.Windows.Forms.ProgressBar();
            this.webBPesaPal = new System.Windows.Forms.WebBrowser();
            this.timRetryAnim = new System.Windows.Forms.Timer(this.components);
            this.timTransactionStatus = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1688, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 88);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progBarRetry
            // 
            this.progBarRetry.Location = new System.Drawing.Point(3, 1);
            this.progBarRetry.Name = "progBarRetry";
            this.progBarRetry.Size = new System.Drawing.Size(1883, 12);
            this.progBarRetry.Step = 5;
            this.progBarRetry.TabIndex = 16;
            // 
            // webBPesaPal
            // 
            this.webBPesaPal.Location = new System.Drawing.Point(3, 21);
            this.webBPesaPal.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBPesaPal.Name = "webBPesaPal";
            this.webBPesaPal.Size = new System.Drawing.Size(1880, 937);
            this.webBPesaPal.TabIndex = 15;
            this.webBPesaPal.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBPesaPal_DocumentCompleted);
            this.webBPesaPal.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBPesaPal_Navigated);
            // 
            // timRetryAnim
            // 
            this.timRetryAnim.Interval = 250;
            this.timRetryAnim.Tick += new System.EventHandler(this.timRetryAnim_Tick);
            // 
            // timTransactionStatus
            // 
            this.timTransactionStatus.Interval = 6000;
            this.timTransactionStatus.Tick += new System.EventHandler(this.timTransactionStatus_Tick);
            // 
            // WizardPagePayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progBarRetry);
            this.Controls.Add(this.webBPesaPal);
            this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Name = "WizardPagePayment";
            this.Size = new System.Drawing.Size(1886, 961);
            this.Subtitle = "Select your preferred means of payment, make payment and proceed to printing";
            this.Title = "Step 5 - Make Payment and Print";
            this.WizardPageActivated += new WizardFormLib.WizardPageActivateHandler(this.WizardPagePayment_WizardPageActivated);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progBarRetry;
        private System.Windows.Forms.WebBrowser webBPesaPal;
        private System.Windows.Forms.Timer timRetryAnim;
        private System.Windows.Forms.Timer timTransactionStatus;

    }
}
