using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WizardFormLib;
using System.Diagnostics;

namespace iPrint
{
	public partial class WizardPageSourceSelector : WizardFormLib.WizardPage
	{
		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor that assumes the page type is "intermediate"
		/// </summary>
		/// <param name="parent">The parent WizardFormBase-derived form</param>
        /// 
        

		public WizardPageSourceSelector(WizardFormBase parent) 
					: base(parent)
		{
			InitPage();          
		}
        
        
		//--------------------------------------------------------------------------------
		/// <summary>
		/// Constructor that allows the programmer to specify the page type. In 
		/// the case of the sample app, we use this constructor for this object, 
		/// and specify it as the start page.
		/// </summary>
		/// <param name="parent">The parent WizardFormBase-derived form</param>
		/// <param name="pageType">The type of page this object represents (start, intermediate, or stop)</param>
		public WizardPageSourceSelector(WizardFormBase parent, WizardPageType pageType) 
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
		public void InitPage()
		{
			ButtonStateNext &= ~WizardButtonState.Enabled;
			InitializeComponent();
			base.Size = this.Size;
			//base.Title = this.Title;
			//base.Subtitle = this.Subtitle;
			this.ParentWizardForm.DiscoverPagePanelSize(this.Size);
			//this.ParentWizardForm.EnableNextButton(false);
           
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Overridden method that allows this wizard page to save page-specific data.
		/// </summary>
		/// <returns>True if the data was saved successfully</returns>
		public override bool SaveData()
		{
			return true;
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// This is an overridden method that performs special processing when 
		/// it's time to select the next page to display. In the case of our 
		/// Page 1, a different "next" page is displayed depending on which 
		/// radio button has been selected.
		/// </summary>
		/// <returns>The new current page that we will show</returns>
		public override WizardPage GetNextPage()
		{
			// some voluntary sanity checking
			if (NextPages.Count != 3)
			{
				throw new WizardFormException("Page 1 expects three \"next\" pages to be specified.");
			}
            // make a choice of the next page as per the user selection stored in the global variable
            if (clsGlobalVariables.USB)
			{             
                
                return NextPages[0];
			}

            else if (clsGlobalVariables.CAMERA)
            {               
                return NextPages[1];
            }

            else if (clsGlobalVariables.SCANNER)
            {
                return NextPages[2];
            }
			else
			{
				return NextPages[1];
			}
		}

		//--------------------------------------------------------------------------------
		/// <summary>
		/// Fired when the user selects the USB buttons. In our case the 
		/// "Next" button is disabled and the act of selecting one of the Picture sources 
		/// button allows the user to proceed in the wizard.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void butUSB_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);// collect statistics that the USB button was clicked by the user/client
            DriveLetter(); //Get the USB drive letter - the drive letter will be saved in a global variable 
            if (clsGlobalVariables.USBDriveLetter != "")//if a removable drive is found,
            {
                UpdateButtons(sender);//update the Highlight/selection of the appropriate button, Prepare to navigate next
            }
            else //if no removable drive is found, do not highlight the selection of the USB button 
            {                
                ButtonStateNext &= ~WizardButtonState.Enabled;
                ParentWizardForm.UpdateWizardForm(this);
                MessageBox.Show("Insert a USB disk first!!"); //inform the user to plug in a USB disk.
            }

            clsGlobalVariables.USB = true; //Store the selected function in preparation of the next wizard page.          
        }

        private void DriveLetter()
        {

            IList<String> fullNames = new List<String>(); //Create a string list fullNames to hold the path to the drive


            foreach (DriveInfo driveInfo in DriveInfo.GetDrives())//Collect information on each drive installed in the computer
            {
                if (driveInfo.DriveType == DriveType.Removable) //Check if the drive found is a removable drive
                {
                    fullNames.Add(driveInfo.RootDirectory.FullName); //if it is a removable drive add it to the String list fullNames
                }
            }
            clsGlobalVariables.USBDriveLetter = String.Join(Environment.NewLine, fullNames); //@"C:\C# Progs\iPrint\iPrintTemp\";//Change Here: String.Join(Environment.NewLine, fullNames); //Save the removable disk letter/path to the global variable
        }

        private void butWeb_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);// collect statistics that the USB button was clicked by the user/client
            UpdateButtons(sender); //update the Highlight/selection of the appropriate button, Prepare to navigate next

            clsGlobalVariables.WEB = true; //Store the selected function in preparation of the next wizard page.           
        }

        private void butBTooth_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);// collect statistics that the USB button was clicked by the user/client
            UpdateButtons(sender); //update the Highlight/selection of the appropriate button, Prepare to navigate next

            clsGlobalVariables.BLUETOOTH = true;  //Store the selected function in preparation of the next wizard page.          
        }

        private void btnCamera_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender); // collect statistics that the USB button was clicked by the user/client
            UpdateButtons(sender); //update the Highlight/selection of the appropriate button, Prepare to navigate next

            clsGlobalVariables.CAMERA = true; //Store the selected function in preparation of the next wizard page.

            string YouCam4Path = @"C:\Program Files (x86)\CyberLink\YouCam\YouCam.exe"; //Get the path to the YouCam software

            ProcessStartInfo startInfo = new ProcessStartInfo(YouCam4Path); //Obtain a handle to the Youcam software
            startInfo.WindowStyle = ProcessWindowStyle.Minimized; //Start the YouCamsoftware in a minimized state
            
        }

        private void btnScanner_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender); // collect statistics that the USB button was clicked by the user/client
            UpdateButtons(sender); //update the Highlight/selection of the appropriate button, Prepare to navigate next

            clsGlobalVariables.SCANNER = true; //Store the selected function in preparation of the next wizard page.
        }

        private void UpdateButtons(object sender)// Graphics update and preparation of the wizard buttons
        {
            Button SelBtn = sender as Button; //Get the ID of the clicked button
            
            //Clear selection of the buttons
            butUSB.BackColor     = Color.White;
            butWeb.BackColor     = Color.White;
            butBTooth.BackColor  = Color.White;
            btnCamera.BackColor  = Color.White;
            btnScanner.BackColor = Color.White;

            SelBtn.BackColor = Color.Pink; //Highlight the clicked button in color - Pink

            //this.ParentWizardForm.EnableNextButton(true);
            ButtonStateNext |= WizardButtonState.Enabled; // Enables the "NEXT" button
            ParentWizardForm.UpdateWizardForm(this); //Calls the update function of the wizard class
            //Clear all the global variables storing the selected function as the appropriate variable will be set in the calling function
            clsGlobalVariables.USB = false;
            clsGlobalVariables.WEB = false;
            clsGlobalVariables.BLUETOOTH = false;
            clsGlobalVariables.CAMERA = false;
            clsGlobalVariables.SCANNER = false;
        }
       

	}
}
