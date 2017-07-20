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
	public partial class WizardPageUSB : WizardFormLib.WizardPage
	{
        [DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
        public static extern Int32 StrFormatByteSize(
            long fileSize,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder buffer,int bufferSize);

        

        public WizardPageUSB(WizardFormBase parent) 
					: base(parent)
		{
			InitPage();            
		}
		public WizardPageUSB(WizardFormBase parent, WizardPageType pageType) 
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
			InitializeComponent();
            clsGlobalVariables.DiskFilesDirectory = clsGlobalVariables.USBDrivePath;// @"C:\Users\Zahir\Desktop\Pigaboost pictures\";
			base.Size = this.Size;
			this.ParentWizardForm.DiscoverPagePanelSize(this.Size);            
           // WizardPage1.
            
		}
        //--------------------------------------------------------------------------------
        /// <summary>
        /// Triggered when the next page for the wizard is being called for display.
        /// </summary>
        public override WizardPage GetNextPage()
        {
            // some voluntary sanity checking
            if (NextPages.Count != 2)
            {
                throw new WizardFormException("This page expects two \"next\" pages to be specified.");
            }
            // If the user had opted to automatically resize all pictures
            if (clsGlobalVariables.AutoResize)
            {
                //Call the function that automatically resizes all pictures and then display the next page (Payment summery)
                AutoResize();
                return NextPages[1];
            }            
            else
            {   //if the user did not choose to automatically resize all the pictures
                //Load the picture editor as the next page to allow for manual editing
                return NextPages[0];
            }
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Triggered when the call to automatically resize all pictures is made.
        /// </summary>
        private void AutoResize()
        {
            String NewFilePath = @"C:\iPrint\iPrintEditor\"; //to be changed

            //Get the selected pictures form the global Variable keeping track of selections
            foreach (string filename in clsGlobalVariables.SelectedPics)
            {
                FileInfo file_info = new FileInfo(filename);
                String FileName = file_info.Name; // Get the full path of the selected files

                using (Image Img = Image.FromFile(filename)) //load the image
                {
                    Image Img2;
                    Img2 = ImageResize.ResizeImage(Img); // Call the resizing function
                    Img2.Save(NewFilePath + FileName, System.Drawing.Imaging.ImageFormat.Jpeg); //Save the resized image in the Editor folder
                    //pic.Image = new Bitmap(Img);
                }
                
            }
        }
        // PictureBoxes we use to display thumbnails.
        private List<PictureBox> PictureBoxes = new List<PictureBox>(); //declaration 1

        //Define the Thumbnail size - Dimensions.
        private const int ThumbWidth = 100;
        private const int ThumbHeight = 100;

        // The selected PictureBox.
        private PictureBox SelectedPictureBox = null;

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Loads and Display thumbnails for the pictures in the selected directory.
        /// </summary>
        private void LoadThumbNails()
        {
           // labDone.Visible = false; //Clear the message that announces "Done" when loading is completed
            // Delete the old PictureBoxes.
            labDone.Text = "";          

            foreach (PictureBox pic in PictureBoxes)
            {
                pic.Click -= PictureBox_Click;
                pic.Dispose();                
            }
            flpThumbnails.Controls.Clear();
            //    PictureBoxes = new List<PictureBox>();
            SelectedPictureBox = null;

            // If the directory doesn't exist, do nothing else.
           // if (!Directory.Exists(clsGlobalVariables.DiskFilesDirectory)) return;
            clsGlobalVariables.DiskFilesDirectory = clsGlobalVariables.USBDrivePath;

            // Get the names of the files in the directory.
            List<string> filenames = new List<string>();
            string[] patterns = { "*.png", "*.gif", "*.jpg", "*.bmp", "*.tif" }; // Create an array pattern of file extensions for all possible graphics formats
            foreach (string pattern in patterns)
            {
                filenames.AddRange(Directory.GetFiles(clsGlobalVariables.DiskFilesDirectory,
                    pattern, SearchOption.TopDirectoryOnly)); //Populate the filenames list with all files matching the patterns (graphical files)
            }
            filenames.Sort(); //Sort the file names in the list in an alphabetical order
            //int files = filenames.Count;
            progBarUSB.Maximum = filenames.Count; //Assign the maximum count level for the progress bar equal to the number of graphical files found
            progBarUSB.Value = 0; //Since loading of the files to thumbnails has not begun, reset the progress bar

            // Load the files.
            foreach (string filename in filenames)
            {
                // Load the picture into a PictureBox.
                PictureBox pic = new PictureBox(); //Create a new PictureBox object

                pic.ClientSize = new Size(ThumbWidth, ThumbHeight); //Resize the picture object to the size of the thumbnail for display

             
                using (Image image = Image.FromFile(filename))
                {
                    Image thumb = image.GetThumbnailImage(80, 80, () => false, IntPtr.Zero);
                    image.Dispose();
                    pic.Image = new Bitmap(thumb); //load the image into the PictureBox pic
                }
               
               //Center the image
                pic.SizeMode = PictureBoxSizeMode.CenterImage;
              
                // Add the Click event handler to the thumbnail.
                pic.Click += PictureBox_Click;

                // Add a tool-tip.
                FileInfo file_info = new FileInfo(filename); //FIll the file_info structure with details about the picture such as location
                
                pic.Tag = file_info; //For each thumbnail, assign the "Tag" property with pictures' storage path for easier future location recall
              
                // Add the PictureBox to the FlowLayoutPanel.
                pic.Parent = flpThumbnails; //Assign a parent to the thumbnail and display
                progBarUSB.Value = progBarUSB.Value + 1; //Increment the progress bar for each displayed thumbnail for the pictures in the folder
                
                this.Update();//update the rendering to ensure the thumbnails are well displayed

                labDone.Text = ("Loading Picture: ") + progBarUSB.Value.ToString() + (" of ") + filenames.Count;
                labDone.Update();
                
            }

            labDone.BackColor = Color.FromArgb(0xFF, 0x06, 0xB0, 0x25);
            labDone.Visible = true; //Since the loading of thumbnails is done, assign parameters to the label and announce "Done"

            progBarUSB.Update(); //interrupt processing to update the progress bar so the user can see that loading is taking place 

            // Check the Global variable clsGlobalVariables.Browsed that the user has NOT clicked on even one picture
            if (clsGlobalVariables.Browsed != true)
            {
                //Clear the  global stringlist that tracks selected thumbnails 
                clsGlobalVariables.SelectedPics.Clear();
            }
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Triggered when the user clicks a thumbnail 
        /// </summary>
        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            SelectedPictureBox = pic; // Get the clicked thumbnail

            if (SelectedPictureBox.BorderStyle == BorderStyle.None) // Check its boarder style property to know if it was not selected
            {
                //Update the selection displays to show that the user has selected the picture
                SelectedPictureBox.BorderStyle = BorderStyle.Fixed3D;
                SelectedPictureBox.BackColor = Color.Pink;
                //Add the path of the selected picture to the global stringlist that tracks selected thumbnails 
                clsGlobalVariables.SelectedPics.Add(pic.Tag.ToString()); 
            }
            else //incase the thumbnail was selected
            {
                //Clear the selection display indicators 
                SelectedPictureBox.BorderStyle = BorderStyle.None; 
                SelectedPictureBox.BackColor = Color.White;
                //Remove the path of the selected picture from the global stringlist that tracks selected thumbnails 
                clsGlobalVariables.SelectedPics.Remove(pic.Tag.ToString());
            }
            // Set the Global variable clsGlobalVariables.Browsed to True to show that the user has clicked on at least one picture
            clsGlobalVariables.Browsed = true; 
        }
        
        private void flpThumbnails_VisibleChanged(object sender, EventArgs e)
        {
            if (clsGlobalVariables.Browsed != true)
            {
                if (clsGlobalVariables.USB)
                {
                    LoadThumbNails();
                }
            }
        }

        //--------------------------------------------------------------------------------
        /// <summary>
        /// Triggered when the user clicks the auto resize all pictures checkbox 
        /// </summary>
        private void cBoxAutoResize_CheckedChanged(object sender, EventArgs e)
        {
            //Set or Reset the Global variable clsGlobalVariables.AutoResize 
            //that indicates if the user wants to automatically resize all the pictures in his selection
            clsGlobalVariables.AutoResize = cBoxAutoResize.Checked; 
        }
       
	}
}
