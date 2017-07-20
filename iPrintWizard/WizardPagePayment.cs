using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ePayment;
using WizardFormLib;

namespace iPrint
{
	public partial class WizardPagePayment : WizardFormLib.WizardPage
	{
		public WizardPagePayment(WizardFormBase parent)
					:base(parent)
		{
			InitPage();
		}

		public WizardPagePayment(WizardFormBase parent, WizardPageType pageType) 
					: base(parent, pageType)
		{
			InitPage();
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// This method serves as a common constructor initialization location, 
		/// and serves mainly to set the desired size of the container panel in 
		/// the wizard form (see WizardFormBase for more info).  I didn't want 
		/// to do this here but it was the only way I could get the form to 
		/// resize itself appropriately - it needed to size itself according 
		/// to the size of the largest wizard page.
		/// </summary>
        PesaPal pesaPal;
        int retry = 0;
        MySQLConnection MySQLConnection1;
        String htmlPath = @"C:\Projects\iPrint\Html\";

		public void InitPage()
		{
			InitializeComponent();
			base.Size = this.Size;
			this.ParentWizardForm.DiscoverPagePanelSize(this.Size);

			// add a handler to let us know when the wizard form has been "started"
			this.ParentWizardForm.WizardFormStartedEvent += new WizardFormStartedHandler(ParentWizardForm_WizardFormStartedEvent);
            pesaPal = new PesaPal(false);//true); //
            MySQLConnection1 = new MySQLConnection();  
        }

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the wizard form has been started (and after all of the pages have 
		/// been added).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ParentWizardForm_WizardFormStartedEvent(object sender, WizardFormStartedArgs e)
		{
           //====
		}

        private void button1_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.BmpToPrint();
        }
        
        private void webBPesaPal_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            String PesaPalResponse;
            PesaPalResponse = webBPesaPal.Url.Query;

            if (PesaPalResponse.Contains("?pesapal_transaction_tracking_id"))
            {
                webBPesaPal.Stop();
                webBPesaPal.Url = new Uri(htmlPath + "verifying.html");
                pesaPal.extraxtPesaPalResponse(PesaPalResponse);

                timTransactionStatus.Enabled = true;

                timRetryAnim.Enabled = true;
                progBarRetry.Value = 0;
            }            
        }

        private void timRetryAnim_Tick(object sender, EventArgs e)
        {
            if (progBarRetry.Value >= 100)
            {
                progBarRetry.Value = 0;
            }
            progBarRetry.Value = progBarRetry.Value + 5;
        }

        private void webBPesaPal_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            String lastUrl = webBPesaPal.Url.ToString();

            if (lastUrl.Contains("_epaystatus"))
            {
                timRetryAnim.Enabled = false;
                progBarRetry.Value = 0;
            }       
        }

        private void timTransactionStatus_Tick(object sender, EventArgs e)
        {
            String Response = pesaPal.UpdateIpnTransactionStatus(pesaPal.ePay_transaction_tracking_id, pesaPal.ePay_merchant_reference);

            //COMPLETED,
            if (Response == "COMPLETED")
            {
                timTransactionStatus.Enabled = false;
                //show Completed message, PesaPal and exit
                webBPesaPal.Url = new Uri(htmlPath + "completed_epaystatus.html");

                SaveDetails(Response);

                ButtonStateNext |= WizardButtonState.Enabled;
                ButtonStateCancel &= ~WizardButtonState.Enabled;
                ParentWizardForm.UpdateWizardForm(this);

                retry = 0;
                return;
            }

            //FAILED or INVALID - COMPLETE FAILURE
            if ((Response == "FAILED") || (Response == "INVALID") || (retry == 10))
            {
                timTransactionStatus.Enabled = false;
                //show error message, PesaPal contacts and exit
                webBPesaPal.Url = new Uri(htmlPath + "completefailure_epaystatus.html");

                SaveDetails(Response);

               // btnMakePayment.Enabled = true;

                retry = 0;
                return;
            }

            //PENDING - RETRYING
            if (Response == "PENDING")
            {
                retry++;
                //show error message and wait before retry                
                webBPesaPal.Url = new Uri(htmlPath + "retry.html");
            }
        }

        private void SaveDetails(String Response)
        {
            MySQLConnection1.SaveTransactionStatus(pesaPal.ePay_transaction_tracking_id, pesaPal.ePay_merchant_reference, pesaPal.FirstName, pesaPal.LastName, pesaPal.PhoneNumber, Response);
            MySQLConnection1.SaveTransactionDetails(pesaPal.ePay_merchant_reference, clsGlobalVariables.stdPhotoCount, clsGlobalVariables.ppPhotoCount);
        }

        private void WizardPagePayment_WizardPageActivated(object sender, WizardPageActivateArgs e)
        {
            ButtonStateBack &= ~WizardButtonState.Enabled;
            ButtonStateStart &= ~WizardButtonState.Enabled;
            ButtonStateNext &= ~WizardButtonState.Enabled;
            ParentWizardForm.UpdateWizardForm(this);

            pesaPal.TotalCost = clsGlobalVariables.TotalCost;
            pesaPal.FirstName = clsGlobalVariables.FirstName;
            pesaPal.LastName = clsGlobalVariables.LastName;
            pesaPal.PhoneNumber = clsGlobalVariables.PhoneNumber;
            webBPesaPal.Navigate(pesaPal.GetPesapalUrl());
        }

	}
}
