using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WizardFormLib;
using System.Runtime.InteropServices;
using System.IO;

namespace iPrint
{
	public partial class WizardPageUSBBrowser : WizardFormLib.WizardPage
	{
        [DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
        public static extern Int32 StrFormatByteSize(
            long fileSize,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder buffer,int bufferSize);

        

        public WizardPageUSBBrowser(WizardFormBase parent) 
					: base(parent)
		{
			InitPage();            
		}
		public WizardPageUSBBrowser(WizardFormBase parent, WizardPageType pageType) 
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
        String drive;
		public void InitPage()
		{
			InitializeComponent();

           
           // WizardPage1.           
		}

        private void BuildTree(DirectoryInfo directoryInfo, TreeNodeCollection addInMe)
        {
            TreeNode curNode = addInMe.Add(directoryInfo.Name);

          /*  foreach (FileInfo file in directoryInfo.GetFiles())
            {
                curNode.Nodes.Add(file.FullName, file.Name);
            }*/
            foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
            {
                BuildTree(subdir, curNode.Nodes);
            }
        }

    
        private void FillExplorer()
        {
            drive = clsGlobalVariables.USBDriveLetter;
            DirectoryInfo directoryInfo = new DirectoryInfo(drive);
            if (directoryInfo.Exists)
            {
                treeView1.AfterSelect += treeView1_AfterSelect;
                treeView1.Nodes.Clear();
                BuildTree(directoryInfo, treeView1.Nodes);
            }
        }
            
        private void WizardPageUSBBrowser_Paint(object sender, PaintEventArgs e)
        {
            if (clsGlobalVariables.USBBrowserShow)
            {
                clsGlobalVariables.USBBrowserShow = false;
                label1.Text = clsGlobalVariables.USBDrivePath;
                FillExplorer();               
                clsGlobalVariables.USBDrivePath = clsGlobalVariables.USBDriveLetter;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            clsGlobalVariables.USBDrivePath = treeView1.SelectedNode.FullPath + "\\"; 
            label1.Text = clsGlobalVariables.USBDrivePath;
        }


	}
}
