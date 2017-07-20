using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;


using WizardFormLib;

namespace iPrint
{
	/// <summary>
	/// Presents a basic usage example for the WizardFormLib.
	/// </summary>
	public partial class WizardPagesLoader : WizardFormLib.WizardFormBase
	{
		
        WizardPageSourceSelector    page1	= null;		
        WizardPageUSBBrowser        page2a  = null;
        WizardPageUSB               page2b  = null;
		WizardPageCamera	        page2c	= null;
        WizardPageScanner           page2d  = null;
		WizardPageEditor		    page3	= null;
		WizardPagePrintSummary	    page4	= null;
		WizardPagePayment		    page5	= null;

        

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor
		/// </summary>
		public WizardPagesLoader()
		{           

            InitializeComponent();
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the form is loaded
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void WizardPagesLoader_Load(object sender, EventArgs e)
		{
            
            // set our image panel metrics
			this.GraphicPanelImagePosition = WizardImagePosition.Right;
            this.GraphicPanelImageResource = "iPrint.iprint.png";        
			this.GraphicPanelGradientColor = Color.DarkSlateBlue;

			// if you don't need a given button, you can hide it here
			//this.ButtonHelpHide = true;
			//this.ButtonStartHide = true;

			// add handlers for the buttons
			this.buttonBack.Click	+= new System.EventHandler(this.buttonBack_Click);
			this.buttonNext.Click	+= new System.EventHandler(this.buttonNext_Click);
			this.buttonCancel.Click	+= new System.EventHandler(this.buttonCancel_Click);
			this.buttonHelp.Click	+= new System.EventHandler(this.buttonHelp_Click);
			this.buttonStart.Click	+= new System.EventHandler(this.buttonStart_Click);
            //this.buttonStart.BackColor = Color.DarkSlateBlue;

			// create the wizard pages we need
			page1	= new WizardPageSourceSelector(this, WizardPageType.Start);            
            page2a  = new WizardPageUSBBrowser(this);
            page2b  = new WizardPageUSB(this);
			page2c	= new WizardPageCamera(this);
            page2d  = new WizardPageScanner(this);
			page3	= new WizardPageEditor(this);
			page4	= new WizardPagePrintSummary(this);
			page5	= new WizardPagePayment(this, WizardPageType.Stop);

			// add a handler that lets us know when a page has been activated
			page1.WizardPageActivated	+= new WizardPageActivateHandler(WizardPageActivated);
			page2a.WizardPageActivated	+= new WizardPageActivateHandler(WizardPageActivated);
			page2b.WizardPageActivated	+= new WizardPageActivateHandler(WizardPageActivated);
            page2c.WizardPageActivated  += new WizardPageActivateHandler(WizardPageActivated);
            page2d.WizardPageActivated  += new WizardPageActivateHandler(WizardPageActivated);
			page3.WizardPageActivated	+= new WizardPageActivateHandler(WizardPageActivated);
			page4.WizardPageActivated	+= new WizardPageActivateHandler(WizardPageActivated);
			page5.WizardPageActivated	+= new WizardPageActivateHandler(WizardPageActivated);
	
			// make sure all of the necessary pages have a "next" page
			page1.AddNextPage(page2a);
			page1.AddNextPage(page2c);
            page1.AddNextPage(page2d);
			page2a.AddNextPage(page2b);
            page2b.AddNextPage(page3);
            page2b.AddNextPage(page4);
			page2c.AddNextPage(page3);
            page2d.AddNextPage(page3);
			page3.AddNextPage(page4);
			page4.AddNextPage(page5);
           
			// start the wizard
			StartWizard();
            
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when a wizard page is activated (made visible)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void WizardPageActivated(object sender, WizardPageActivateArgs e)
		{
			PaintTitle();
			this.buttonBack.Enabled = (e.ActivatedPage.WizardPageType != WizardPageType.Start);
			if (e.ActivatedPage.WizardPageType == WizardPageType.Stop)
			{
				this.buttonNext.Text = "Finished";
			}
			else
			{
				this.buttonNext.Text = "Next >";
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the back button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonBack_Click(object sender, EventArgs e)
		{
			// tell the page chain to go to the previous page
            clsGlobalVariables.WizardPagePrevious = PageChain.GetCurrentPage().Name;
            if (clsGlobalVariables.WizardPagePrevious == "WizardPageCamera")
            {
                WizardPageCamera.DisconnectUSBCamera();                
            }
           
			WizardPage currentPage = PageChain.GoBack();                  

            clsGlobalVariables.WizardPageCurrent = currentPage.Name;

            if (clsGlobalVariables.WizardPageCurrent == "WizardPageCamera")//WizardPage3
            {
                WizardPageCamera.ConnectUSBCamera();
            }
            
			// raise the page change event (this currently does nothing but lets the 
			// base class know when the active page has changed
			Raise_WizardPageChangeEvent(new WizardPageChangeArgs(currentPage, WizardStepType.Previous));
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the Next button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonNext_Click(object sender, EventArgs e)
		{
			// if the current page (before changing) is the last page in the wizard, 
			// take steps to close the wizard
            //WizardPage a = PageChain.GetCurrentPage();
            clsGlobalVariables.WizardPagePrevious = PageChain.GetCurrentPage().Name;

			if (PageChain.GetCurrentPage().WizardPageType == WizardPageType.Stop)
			{
                
                // call the central SaveData method (which calls the SaveData 
				// method in each page in the chain
				if (PageChain.SaveData() == null)
				{
					// and if everything is okay, close the wizard form
                    clsGlobalVariables.Browsed = false;
					this.Close();
				}
			}
			// otherwise, move to the next page in the chain, and let the base class know
			else
			{
				WizardPage currentPage = PageChain.GoNext(PageChain.GetCurrentPage().GetNextPage());
                clsGlobalVariables.WizardPageCurrent = currentPage.Name;
               

                if (currentPage.Name == "WizardPageCamera")
                {                    
                    WizardPageCamera.ConnectUSBCamera();                  
                }
                
                if (currentPage.Name == "WizardPageEditor")
                {
                    WizardPageCamera.DisconnectUSBCamera();
                }

                if (currentPage.Name == "WizardPageUSBBrowser")
                {
                    clsGlobalVariables.USBBrowserShow = true;
                }

                if (currentPage.Name == "WizardPagePrintSummary")
                {
                   //WizardPagePrintSummary a = new WizardPagePrintSummary();

                    //a.loadFilesToPrint();
                    clsGlobalVariables.SummaryPageAccess = true;

                }

				Raise_WizardPageChangeEvent(new WizardPageChangeArgs(currentPage, WizardStepType.Next));                
			}
 
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user clicks the Cancel button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCancel_Click(object sender, EventArgs e)
		{
            clsGlobalVariables.Browsed = false;
            WizardPageCamera.DisconnectUSBCamera();
            WizardPageCamera.DeleteAllSnapshots();
            CleanFolders();
            this.Close();
		}

        //--------------------------------------------------------------------------------
        private void CleanFolders() //Called to delete all files in the folders that were used
        {           

            System.IO.DirectoryInfo di = new DirectoryInfo(@"C:\iPrint"); //assign the iPrint main working folder
                       
            foreach (DirectoryInfo dir in di.GetDirectories()) //iterate through all the folders in the main working folder
            {
                
                    foreach (FileInfo file in dir.GetFiles()) //iterate through each file and get a handle to the file
                    {
                        file.Delete(); //delete the file
                    }
            }
        }
       	//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user clicks the Help button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonHelp_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Not implemented yet.");
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user clicks the Start button (to return to the first wizard 
		/// page).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonStart_Click(object sender, EventArgs e)
		{
            WizardPageCamera.DisconnectUSBCamera();
            WizardPage currentPage = PageChain.GoFirst();
			// raise the page change event - this currently does nothing but lets the 
			// base class know when the active page has changed
			Raise_WizardPageChangeEvent(new WizardPageChangeArgs(currentPage, WizardStepType.Previous));
		}

        private void WizardPagesLoader_FormClosed(object sender, FormClosedEventArgs e)
        {
            WizardPageCamera.DisconnectUSBCamera();
            clsGlobalVariables.Browsed = false;
        }

	}
}



