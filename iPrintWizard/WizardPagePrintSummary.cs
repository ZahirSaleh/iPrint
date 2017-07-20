using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using WizardFormLib;
using System.Diagnostics;
using System.Runtime.InteropServices;
//using System.Management;

namespace iPrint
{
	
    public partial class WizardPagePrintSummary : WizardFormLib.WizardPage
    {
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;      

        public WizardPagePrintSummary(WizardFormBase parent) 
					: base(parent)
		{
			InitPage();
		}
		public WizardPagePrintSummary(WizardFormBase parent, WizardPageType pageType) 
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
            ButtonStateBack |= WizardButtonState.Enabled;

            InitializeComponent();
			base.Size = Size;
			ParentWizardForm.DiscoverPagePanelSize(Size);          
              
		}               

        private void loadFilesToPrint()
        {  
          
            flowLayoutPanelMain.Controls.Clear();

            clsGlobalVariables.stdPhotoCount = 0;
            clsGlobalVariables.ppPhotoCount = 0;

            foreach (string fileName in Directory.GetFiles(@"C:\iPrint\iPrintEditor\"))
            {

                if (Path.GetExtension(fileName) == ".jpg")
                {                  
                   flowLayoutPanelMain.FlowDirection = FlowDirection.TopDown;

                   PictureBox picBox = new PictureBox();
                   picBox.SizeMode = PictureBoxSizeMode.Zoom;
                   picBox.Width = 266;
                   picBox.Height = 200;
                   //picBox.Waitonload = false;
                   picBox.LoadAsync(fileName); 

                   Label lbl = new Label();
                   lbl.AutoSize = true;

                   String path = Path.GetFileNameWithoutExtension(fileName); 

                   if(path.Contains("_PP_"))
                   {
                       lbl.Text = "Passport Size - Cost = 2000";
                       clsGlobalVariables.ppPhotoCount++;
                   }
                   else
                   {
                       lbl.Text = "Standard Print - Cost = 50";
                       clsGlobalVariables.stdPhotoCount++;
                   }
                   flowLayoutPanelMain.Controls.Add(picBox);
                   flowLayoutPanelMain.Controls.Add(lbl);
                }
            }

            FillCost();           
        }
    
        private void FillCost()
        {
           // int ppCost = 2000;
           // int stdCost = 50;
            clsGlobalVariables.StandardPhotoPrice = 100;
            clsGlobalVariables.PassportPhoroPrice= 20;

            labPPQty.Text = clsGlobalVariables.ppPhotoCount.ToString("00");
            labStdQty.Text = clsGlobalVariables.stdPhotoCount.ToString("00");
            labQtyTotal.Text = (clsGlobalVariables.ppPhotoCount + clsGlobalVariables.stdPhotoCount).ToString();

            labPPCost.Text = clsGlobalVariables.PassportPhoroPrice.ToString("#,#");
            labStdCost.Text = clsGlobalVariables.StandardPhotoPrice.ToString("#,#");

            labPPTotal.Text = (clsGlobalVariables.PassportPhoroPrice * clsGlobalVariables.ppPhotoCount).ToString("#,#");
            labStdTotal.Text = (clsGlobalVariables.StandardPhotoPrice * clsGlobalVariables.stdPhotoCount).ToString("#,#");

            labGrandTotal.Text = ((clsGlobalVariables.PassportPhoroPrice * clsGlobalVariables.ppPhotoCount) + (clsGlobalVariables.StandardPhotoPrice * clsGlobalVariables.stdPhotoCount)).ToString("#,#");

            decimal _TotalCost;
            if (Decimal.TryParse(labGrandTotal.Text, out _TotalCost))
            {
                clsGlobalVariables.TotalCost = _TotalCost;                
            }
            else
            {                
                //to be updated
            }                
            
        }
     
        private void flowLayoutPanelMain_Paint(object sender, PaintEventArgs e)
        {
            if (clsGlobalVariables.SummaryPageAccess)
            {
                clsGlobalVariables.SummaryPageAccess = false;
                loadFilesToPrint();
            }
        }           
  
        private void tBoxPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            tBox_TextChanged();            
        }        

        private void tBoxFName_TextChanged(object sender, EventArgs e)
        {
            tBox_TextChanged();
        }

        private void tBoxLName_TextChanged(object sender, EventArgs e)
        {
            tBox_TextChanged();
        }

        private void tBox_TextChanged()
        {
            if ((tBoxPhoneNumber.Text.Length == 13) && (tBoxFName.Text != "") && (tBoxLName.Text != ""))
            {
                clsGlobalVariables.FirstName = tBoxFName.Text;
                clsGlobalVariables.LastName = tBoxLName.Text;
                clsGlobalVariables.PhoneNumber = tBoxPhoneNumber.Text;

                ButtonStateNext |= WizardButtonState.Enabled;
                //ButtonStateCancel &= ~WizardButtonState.Enabled;
                ParentWizardForm.UpdateWizardForm(this);
            }

        }

        IntPtr wKB;
        string KeypadPath = @"C:\Projects\iPrint\iPrintKeyBoard\iPrintKeyBoard\bin\Debug\iPrintKeyBoard.exe";

        private void tBoxFName_Enter(object sender, EventArgs e)
        {
            CloseKbd();
            OpenKbd(panelAlpha, " ALPHA");
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, int lParam);       

        private void tBoxPhoneNumber_Enter(object sender, EventArgs e)
        {
            CloseKbd();
            OpenKbd(panelNum, " NUM");
        }

        private void OpenKbd(Panel panelLoc, String kbdMode)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(KeypadPath);
            startInfo.WindowStyle = ProcessWindowStyle.Normal;

            Point kbdLocation;

            kbdLocation = PointToScreen(panelLoc.Location);

            String x = kbdLocation.X.ToString();
            String y = kbdLocation.Y.ToString();

            startInfo.Arguments = x + " " + y + kbdMode;
            Process.Start(startInfo);
        }

        private void CloseKbd()
        {
            wKB = FindWindow(null, "Touch Keypad");
            if (wKB != null)
            {
                // close the window using API        
                SendMessage(wKB, WM_SYSCOMMAND, SC_CLOSE, 0);
            }
        }

        private void tBoxLName_Enter(object sender, EventArgs e)
        {
            CloseKbd();
            OpenKbd(panelAlpha, " ALPHA");
        }
        
        
	}
}
