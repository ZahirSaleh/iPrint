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
using System.Linq;
using WIA;
using System.Diagnostics;

namespace iPrint
{
	public partial class WizardPageScanner : WizardFormLib.WizardPage
	{
        [DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
        public static extern Int32 StrFormatByteSize(
            long fileSize,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder buffer,int bufferSize);

        

        public WizardPageScanner(WizardFormBase parent) 
					: base(parent)
		{
			InitPage();            
		}
		public WizardPageScanner(WizardFormBase parent, WizardPageType pageType) 
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
           
			base.Size = this.Size;
			this.ParentWizardForm.DiscoverPagePanelSize(this.Size);
           // WizardPage1.
            clsGlobalVariables.ScannerFilesDirectory = @"C:\iPrint\iPrintScan\";
            
		}       

        private void btnStartScan_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
            ScanDoc();
        }
        
        private static void AdjustScannerSettings(IItem scannnerItem, int scanResolutionDPI, int scanStartLeftPixel, int scanStartTopPixel,
                    int scanWidthPixels, int scanHeightPixels, int brightnessPercents, int contrastPercents, int colorMode)
        {
            const string WIA_SCAN_COLOR_MODE = "6146";
            const string WIA_HORIZONTAL_SCAN_RESOLUTION_DPI = "6147";
            const string WIA_VERTICAL_SCAN_RESOLUTION_DPI = "6148";
            const string WIA_HORIZONTAL_SCAN_START_PIXEL = "6149";
            const string WIA_VERTICAL_SCAN_START_PIXEL = "6150";
            const string WIA_HORIZONTAL_SCAN_SIZE_PIXELS = "6151";
            const string WIA_VERTICAL_SCAN_SIZE_PIXELS = "6152";
            const string WIA_SCAN_BRIGHTNESS_PERCENTS = "6154";
            const string WIA_SCAN_CONTRAST_PERCENTS = "6155";
            SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
            SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
            SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_START_PIXEL, scanStartLeftPixel);
            SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_START_PIXEL, scanStartTopPixel);
            SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_SIZE_PIXELS, scanWidthPixels);
            SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_SIZE_PIXELS, scanHeightPixels);
            SetWIAProperty(scannnerItem.Properties, WIA_SCAN_BRIGHTNESS_PERCENTS, brightnessPercents);
            SetWIAProperty(scannnerItem.Properties, WIA_SCAN_CONTRAST_PERCENTS, contrastPercents);
            SetWIAProperty(scannnerItem.Properties, WIA_SCAN_COLOR_MODE, colorMode);

        }

        private static void SetWIAProperty(IProperties properties, object propName, object propValue)
        {
            Property prop = properties.get_Item(ref propName);
            prop.set_Value(ref propValue);
        }

        private static void SaveImageToTiff(ImageFile image, string fileName)
        {
            ImageProcess imgProcess = new ImageProcess();
            object convertFilter = "Convert";
            string convertFilterID = imgProcess.FilterInfos.get_Item(ref convertFilter).FilterID;
            imgProcess.Filters.Add(convertFilterID, 0);
            SetWIAProperty(imgProcess.Filters[imgProcess.Filters.Count].Properties, "FormatID", WIA.FormatID.wiaFormatTIFF);
            image = imgProcess.Apply(image);
            image.SaveFile(fileName);
        }
        private void ScanDoc()
        {
            try
            {
                CommonDialogClass commonDialogClass = new CommonDialogClass();
                Device scannerDevice = commonDialogClass.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, false, false);
                if (scannerDevice != null)
                {
                    Item scannnerItem = scannerDevice.Items[1];
                    AdjustScannerSettings(scannnerItem, 150, 0, 0, 1250, 1700, 0, 0, 0);

                    object scanResult = commonDialogClass.ShowTransfer(scannnerItem, WIA.FormatID.wiaFormatTIFF, false);
                    //picScan.Image = (System.Drawing.Image)scanResult;
                    if (scanResult != null)
                    {
                        ImageFile image = (ImageFile)scanResult;
                        string fileName = "";

                        var files = Directory.GetFiles(clsGlobalVariables.ScannerFilesDirectory, "*.tiff");

                        try
                        {
                            string f = ((files.Max(p1 => Int32.Parse(Path.GetFileNameWithoutExtension(p1)))) + 1).ToString();
                            fileName = clsGlobalVariables.ScannerFilesDirectory + "\\" + f + ".tiff";
                        }
                        catch (Exception ex)
                        {
                            clsGlobalFunctions.ErrorLog(ex); //Log Error
                            fileName = clsGlobalVariables.ScannerFilesDirectory + "\\" + "1.tiff";
                        }
                        SaveImageToTiff(image, fileName);
                        picScan.ImageLocation = fileName;
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobalFunctions.ErrorLog(ex); //Log Error
                MessageBox.Show("Check the Device Connection \n or \n Change the Scanner Device", "Devic Not Found!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }     

        //===========================================================
        // PictureBoxes we use to display thumbnails.
        private List<PictureBox> PictureBoxes = new List<PictureBox>();

        // Thumbnail sizes.
        private const int ThumbWidth = 200;
        private const int ThumbHeight = 200;

        // The selected PictureBox.
        private PictureBox SelectedPictureBox = null;
        //public static Boolean Browsed = false;
        // public static List<string> SelectedPics = new List<string>();
        // Display thumbnails for the selected directory.


        private void LoadThumbNails()
        {
            // Delete the old PictureBoxes.
            ClearflpScannerThumbnails();

            // If the directory doesn't exist, do nothing else.
            if (!Directory.Exists(clsGlobalVariables.ScannerFilesDirectory)) return;

            // Get the names of the files in the directory.
            List<string> filenames = new List<string>();
            string[] patterns = { "*.png", "*.gif", "*.jpg", "*.bmp", "*.tiff" };
            foreach (string pattern in patterns)
            {
                filenames.AddRange(Directory.GetFiles(clsGlobalVariables.ScannerFilesDirectory,
                    pattern, SearchOption.TopDirectoryOnly));
            }
            filenames.Sort();
            //int files = filenames.Count;           

            // Load the files.
            foreach (string filename in filenames)
            {
                // Load the picture into a PictureBox.
                PictureBox pic = new PictureBox();

                pic.ClientSize = new Size(ThumbWidth, ThumbHeight);
                Image Img = Image.FromFile(filename);
                pic.Image = new Bitmap(Img);
                Img.Dispose();
                Img = null;

                // If the image is too big, zoom.
                if ((pic.Image.Width > ThumbWidth) ||
                    (pic.Image.Height > ThumbHeight))
                {
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    pic.SizeMode = PictureBoxSizeMode.CenterImage;
                }

                // Add the Click event handler.
                pic.Click += PictureBox_Click;

                // Add a tooltip.
                FileInfo file_info = new FileInfo(filename);

                pic.Tag = file_info;
                pic.Parent = flpScannerThumbnails;

            }

            if (clsGlobalVariables.Browsed != true)
            {
                //  clsGlobalVariables.SelectedPics.Clear();
            }
        }

        private void ClearflpScannerThumbnails()
        {
            // Delete the old PictureBoxes.
            foreach (PictureBox pic in PictureBoxes)
            {
                pic.Click -= PictureBox_Click;
                pic.Dispose();
            }
            flpScannerThumbnails.Controls.Clear();
            //    PictureBoxes = new List<PictureBox>();
            SelectedPictureBox = null;
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {

            PictureBox pic = sender as PictureBox;
            SelectedPictureBox = pic;
            if (SelectedPictureBox.BorderStyle == BorderStyle.None)
            {
                SelectedPictureBox.BorderStyle = BorderStyle.Fixed3D;
                SelectedPictureBox.BackColor = Color.Pink;
                clsGlobalVariables.SelectedPics.Add(pic.Tag.ToString());
            }
            else
            {
                SelectedPictureBox.BorderStyle = BorderStyle.None;
                SelectedPictureBox.BackColor = Color.White;
                clsGlobalVariables.SelectedPics.Remove(pic.Tag.ToString());
            }
            clsGlobalVariables.Browsed = true;
        }
        
        private void butAddPic_Click(object sender, EventArgs e)
        {
            LoadThumbNails();
        }      
       
	}
}
