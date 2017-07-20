using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
//using Ozeki.Media.IPCamera;
using Ozeki.Media.MediaHandlers;
using Ozeki.Media.MediaHandlers.Video;
using Ozeki.Media.Video.Controls;


using WizardFormLib;

namespace iPrint
{
	public partial class WizardPageCamera : WizardFormLib.WizardPage
	{
        private static VideoViewerWF _videoViewer;
        private static WebCamera _webCamera;
        private static DrawingImageProvider _imageProvider;
        private static MediaConnector _mediaConnector;
        private static SnapshotHandler _snapshotHandler;
        private int CountDown = 10;
        private static System.Windows.Forms.Timer timAnimation;
        
        
        public WizardPageCamera(WizardFormBase parent) 
					: base(parent)
		{
            
            InitPage();           
		}

		public WizardPageCamera(WizardFormBase parent, WizardPageType pageType) 
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
            _mediaConnector = new MediaConnector();
            _imageProvider = new DrawingImageProvider();
            _snapshotHandler = new SnapshotHandler();
            _videoViewer = new VideoViewerWF();
            timAnimation = new System.Windows.Forms.Timer();
            timAnimation.Interval = 200;
            timAnimation.Tick += new System.EventHandler(this.timAnimation_Tick);
            //================== 

            _webCamera = WebCamera.GetDefaultDevice();
            SetVideoViewer();
            base.Size = this.Size;
            this.ParentWizardForm.DiscoverPagePanelSize(this.Size);                         
            this.panelVideo.Controls.Add(_videoViewer);
            clsGlobalVariables.SnapshotFilesDirectory = @"C:\iPrint\iPrintCamera\";                                 
		}
        
        private void SetVideoViewer()
        {
            _videoViewer.Location = new Point(3, 3);
            _videoViewer.Size = new Size(panelVideo.Width-10, panelVideo.Height-10);            
            _videoViewer.BackColor = Color.Black;
            _videoViewer.TabStop = false;           
        }

        public static void ConnectUSBCamera()
        {           
            _mediaConnector.Connect(_webCamera, _imageProvider);
            _videoViewer.SetImageProvider(_imageProvider);
            _mediaConnector.Connect(_webCamera, _snapshotHandler);
            _webCamera.Start();
            _videoViewer.Start();
            timAnimation.Enabled = true;                
        }        

        public static void DisconnectUSBCamera()
        {
            timAnimation.Enabled = false;
            _videoViewer.Stop();
            _webCamera.Stop();
        }

        private void CreateSnapShot()
        {
            var date = DateTime.Now.Year + "y-" + DateTime.Now.Month + "m-" + DateTime.Now.Day + "d-" +
                       DateTime.Now.Hour + "h-" + DateTime.Now.Minute + "m-" + DateTime.Now.Second + "s";

            currentpath = clsGlobalVariables.SnapshotFilesDirectory + date + ".jpg";

            var snapShotImage = _snapshotHandler.TakeSnapshot().ToImage();
            snapShotImage.Save(currentpath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void TakeSnapshot()
        {
            CreateSnapShot();
        }       

        private void DisplaySnapshot()
        {
            timAnimation.Enabled = false;

            using (Image Img = Image.FromFile(currentpath))
            {
                picBoxSnapshot.SizeMode = PictureBoxSizeMode.Zoom;
                picBoxSnapshot.Image = new Bitmap(Img);
            }
            
            DisplayButtons();
        }

        private void DisplayButtons()
        {
            butDisposePic.Enabled = true;
            butAddPic.Enabled = true;
        }
        
        private void butDisposePic_Click(object sender, EventArgs e)
        {
            timAnimation.Enabled = true;
            File.Delete(currentpath);
            butDisposePic.Enabled = false;
            butAddPic.Enabled = false;
            picBoxSnapshot.Image.Dispose();

            using (Bitmap bmp = new Bitmap(panelVideo.Width, panelVideo.Height))
            {
                panelVideo.DrawToBitmap(bmp, panelVideo.Bounds);
                picBoxSnapshot.Image = new Bitmap(bmp);
            }
        }

        private void butAddPic_Click(object sender, EventArgs e)
        {
            timAnimation.Enabled = true;
            butDisposePic.Enabled = false;
            butAddPic.Enabled = false;
            LoadThumbNails();
        }
        private void timDelay_Tick(object sender, EventArgs e)
        {
            timDisplay.Enabled = true;
            timDelay.Enabled = false;

            if (CountDown == -1)
            {
                CountDown = 10;
                btnSnapshot.ForeColor = Color.Black;
                btnSnapshot.Text = "Snapshot";
                btnSnapshot.Font = new Font("Arial", 10, FontStyle.Bold);
                timDelay.Enabled = false;
                timDisplay.Enabled = false;
                return;
            }

        }

        private void timDisplay_Tick(object sender, EventArgs e)
        {

            String a = String.Format("{0:00}", CountDown);
            btnSnapshot.Text = a;
            timDisplay.Enabled = false;
            timDelay.Enabled = true;
            CountDown--;

            if (CountDown == 4)
            {
                btnSnapshot.ForeColor = Color.Red;
            }


            if (CountDown == -1)
            {
                TakeSnapshot();
                btnSnapshot.Font = new Font("Arial", 56, FontStyle.Bold);
                btnSnapshot.Text = "!DONE!";
                CountingDown = false;
                DisplaySnapshot();
            }

        }


        // PictureBoxes we use to display thumbnails.
        private List<PictureBox> PictureBoxes = new List<PictureBox>();

        // Thumbnail sizes.
        private const int ThumbWidth = 200;
        private const int ThumbHeight = 200;

        // The selected PictureBox.
        private PictureBox SelectedPictureBox = null;
      
        // Display thumbnails for the selected directory.
        private void LoadThumbNails()
        {
            // Delete the old PictureBoxes.
            ClearflpCameraThumbnails();

            // If the directory doesn't exist, do nothing else.
            if (!Directory.Exists(clsGlobalVariables.SnapshotFilesDirectory)) return;

            // Get the names of the files in the directory.
            List<string> filenames = new List<string>();
            string[] patterns = { "*.png", "*.gif", "*.jpg", "*.bmp", "*.tif" };
            foreach (string pattern in patterns)
            {
                filenames.AddRange(Directory.GetFiles(clsGlobalVariables.SnapshotFilesDirectory,
                    pattern, SearchOption.TopDirectoryOnly));
            }
            
            filenames.Sort();
            
            // Load the files.
            foreach (string filename in filenames)
            {
                // Load the picture into a PictureBox.
                PictureBox pic = new PictureBox();

                pic.ClientSize = new Size(ThumbWidth, ThumbHeight);
                using (Image Img = Image.FromFile(filename))
                {
                    pic.Image = new Bitmap(Img);
                }
                
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
                pic.Parent = flpCameraThumbnails;

            }

            if (clsGlobalVariables.Browsed != true)
            {
             //  clsGlobalVariables.SelectedPics.Clear();
            }
        }

        private void ClearflpCameraThumbnails()
        {
            // Delete the old PictureBoxes.
            foreach (PictureBox pic in PictureBoxes)
            {
                pic.Click -= PictureBox_Click;
                pic.Dispose();
            }
            flpCameraThumbnails.Controls.Clear();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DeleteAllSnapshots();
            Application.Exit();
        }
        
        public static void DeleteAllSnapshots()
        {            
            Array.ForEach(Directory.GetFiles(clsGlobalVariables.SnapshotFilesDirectory),
            delegate(string path) { File.Delete(path); });
        }       

        private void btnSnapshot_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
            if (!CountingDown)
            {
                btnSnapshot.Font = new Font("Arial", 100, FontStyle.Bold);
                btnSnapshot.Text = ('\u263a').ToString();
                CountingDown = true;                
                timDelay.Enabled = true;
            }
        }      

        private void timAnimation_Tick(object sender, EventArgs e)
        {
            using (Bitmap bmp = new Bitmap(panelVideo.Width, panelVideo.Height))
            {
                panelVideo.DrawToBitmap(bmp, panelVideo.Bounds);
                picBoxSnapshot.SizeMode = PictureBoxSizeMode.Zoom;
                picBoxSnapshot.Image = clsGlobalFunctions.MakeGrayscale3(bmp);
            }            
        }
          
	}
}
