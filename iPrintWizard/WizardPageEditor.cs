using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using WizardFormLib;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.IO;
using CropRect;

namespace iPrint
{
	public partial class WizardPageEditor : WizardFormLib.WizardPage
	{
        UserRect rect;
        TilePicture tilePic;
        //=====================================              
        private Size OriginalImageSize;
        private Size ModifiedImageSize;
        //private PictureBox SelectedPictureBox = null;                
        Rectangle cropRectangle = new Rectangle(0,0,270,370);
        Rectangle passportBoxRect = new Rectangle(0, 0, 270, 370);
        //============================
        // PictureBoxes we use to display thumbnails.
        private List<PictureBox> PictureBoxes = new List<PictureBox>();
        // Thumbnail sizes.
        private const int ThumbWidth = 100;
        private const int ThumbHeight = 100;
        //=======================
        private Point startingPoint = Point.Empty;
        private Point movingPoint = Point.Empty;        
        private bool newPicture = false;      
        String OriginalFilePath_Temp;
        //==========Public Variables==========
        public static bool crop = false;
        public static bool panning = false;
        public static bool passport = false;
        //========================

        public WizardPageEditor(WizardFormBase parent) 
					: base(parent)
		{
			InitPage();
		}
		public WizardPageEditor(WizardFormBase parent, WizardPageType pageType) 
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

            rect = new UserRect(clsGlobalVariables.rect64);
            rect.SetPictureBox(this.PicBoxEdit);
            // on invalidate we want to be able to draw the rectangle
           // PicBoxEdit.Paint += new PaintEventHandler(PicBoxEdit_Paint);
		}
       
        private void LoadImage(String Filename)
        {
                        
            Image Img = Image.FromFile(Filename);
            
            //Set the picture box size according to image, we can get image width and height with the help of Image.Width and Image.height properties.
            int imgWidth = Img.Width;
            int imghieght = Img.Height;
            PicBoxEdit.Width = imgWidth;
            PicBoxEdit.Height = imghieght;
            PicBoxEdit.Image = new Bitmap(Img);//loads a copy of the image
            PicBoxEdit.Tag = Filename;
            PictureBoxLocation();
            OriginalImageSize = new Size(imgWidth, imghieght);
            //Img.Dispose();
            SetResizeInfo();
            PictureBoxLocation(); //===to be checked if necessary======
            EditedBitmap = new Bitmap(Img); //new Bitmap(PicBoxEdit.Image);   
            Img.Dispose();
            GC.Collect();
        }

        private void PictureBoxLocation()
        {           
         PicBoxEdit.Location = new Point((panelMainPic.Width - PicBoxEdit.Width) / 2, 
             (panelMainPic.Height - PicBoxEdit.Height) / 2);           

        }

        private void SetResizeInfo()
        {
            lbloriginalSize.Text = OriginalImageSize.ToString();
            lblModifiedSize.Text = ModifiedImageSize.ToString();
        }     

        private void btnOk_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
            Bitmap bm_source = new Bitmap(PicBoxEdit.Tag.ToString());
            // Make a bitmap for the result.
            Bitmap bm_dest = new Bitmap(Convert.ToInt32(ModifiedImageSize.Width), Convert.ToInt32(ModifiedImageSize.Height));
            // Make a Graphics object for the result Bitmap.
            using (Graphics gr_dest = Graphics.FromImage(bm_dest))
            {
                // Copy the source image into the destination bitmap.
                gr_dest.DrawImage(bm_source, 0, 0, bm_dest.Width + 1, bm_dest.Height + 1);                
            }

            // Display the result.
            PicBoxEdit.Image = bm_dest;
            PicBoxEdit.Width = bm_dest.Width;
            PicBoxEdit.Height = bm_dest.Height;           
            PictureBoxLocation();

        }

        private void DomainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            int percentage = 0;
            try
            {
                percentage = Convert.ToInt32(DomainUpDown1.Text);
                ModifiedImageSize = new Size((OriginalImageSize.Width * percentage) / 100, (OriginalImageSize.Height * percentage) / 100);
                SetResizeInfo();
            }
            catch //(Exception ex)
            {
                MessageBox.Show("Invalid Percentage");
                return;
            }

        }
       
        # region "-----------------------------Crop Image------------------------------------"

      
        private void cropPicture(Rectangle cropRect)
        {

            Cursor = Cursors.Default;

            try
            {              
                Rectangle rect1 = new Rectangle(cropRect.X - movingPoint.X, cropRect.Y - movingPoint.Y, cropRect.Width, cropRect.Height);
                
                //First we define a rectangle with the help of already calculated points
                Bitmap OriginalImage = new Bitmap(PicBoxEdit.Image, PicBoxEdit.Width, PicBoxEdit.Height);
                //Original image
                Bitmap _img = new Bitmap(cropRect.Width, cropRect.Height);
                //for cropinf image
                using (Graphics g = Graphics.FromImage(_img))
                {
                    //create graphics
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    //set image attributes
                    g.DrawImage(OriginalImage, 0, 0, rect1, GraphicsUnit.Pixel);
                }

               
                PicBoxEdit.Image = _img;               
                PicBoxEdit.Size = _img.Size;
                PictureBoxLocation();
               
            }
            catch //(Exception ex)
            {
            }
           
            passport = false;
            crop = false;
        }
        
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {            
            startingPoint = new Point(e.Location.X - movingPoint.X, e.Location.Y - movingPoint.Y);
                   
        }
        
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (panning && (e.Button == System.Windows.Forms.MouseButtons.Left))//Panning done here
            {               
                movingPoint = new Point(e.Location.X - startingPoint.X,
                                e.Location.Y - startingPoint.Y);
                PicBoxEdit.Invalidate();
            }          

        }
        # endregion

        private void TrackBarBrightness_Scroll(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
            DomainUpDownBrightness.Text = TrackBarBrightness.Value.ToString();


            float value = TrackBarBrightness.Value * 0.01f;
            float[][] colorMatrixElements = {
	new float[] {
		1,
		0,
		0,
		0,
		0
	},
	new float[] {
		0,
		1,
		0,
		0,
		0
	},
	new float[] {
		0,
		0,
		1,
		0,
		0
	},
	new float[] {
		0,
		0,
		0,
		1,
		0
	},
	new float[] {
		value,
		value,
		value,
		0,
		1
	}
};
            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
            ImageAttributes imageAttributes = new ImageAttributes();

            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            Image _img = Image.FromFile(PicBoxEdit.Tag.ToString());
            Bitmap bm_dest = new Bitmap(Convert.ToInt32(_img.Width), Convert.ToInt32(_img.Height));
          
            using (Graphics _g = Graphics.FromImage(bm_dest))
            {               
                _g.DrawImage(_img, new Rectangle(0, 0, bm_dest.Width + 1, bm_dest.Height + 1), 0, 0, bm_dest.Width + 1, bm_dest.Height + 1, GraphicsUnit.Pixel, imageAttributes);
                PicBoxEdit.Image = bm_dest;
            }
            _img.Dispose();
            _img = null;
            
        }

        private void UpdatePictureBox()
        {             
            PicBoxEdit.Size = PicBoxEdit.Image.Size;
            PicBoxEdit.Refresh();
        }

        private void btnRotateLeft_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
            PicBoxEdit.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            UpdatePictureBox();
        }

        private void btnRotateRight_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
            PicBoxEdit.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            UpdatePictureBox();
        }

        private void btnRotateHorizantal_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
            PicBoxEdit.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            UpdatePictureBox();
        }

        private void btnRotatevertical_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
            PicBoxEdit.Image.RotateFlip(RotateFlipType.RotateNoneFlipY);
            UpdatePictureBox();
        }
      
        private void DrawFrame()
        {          
           
            Image _img = Image.FromFile(PicBoxEdit.Tag.ToString());
            Bitmap bitmap = new Bitmap(_img);
            _img.Dispose();
            _img = null;
            
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawRectangle(new Pen(BorderColor, BorderWidth), new Rectangle(0, 0, bitmap.Width, bitmap.Height));                
            }

            PicBoxEdit.Image = bitmap;
            PicBoxEdit.Size = bitmap.Size;

            PictureBoxLocation();            
        }      
            
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

            try
            {
                BorderWidth = Convert.ToInt32(numericUpDown1.Text);
                tBarFrameWidth.Value = Convert.ToInt32(numericUpDown1.Text);
                DrawFrame();
            }

            catch //(Exception ex)
            {
                MessageBox.Show("Invalid Value");
                return;
            }

        }
       
        private void tBarFrameWidth_Scroll(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
            numericUpDown1.Text = tBarFrameWidth.Value.ToString();
        }
       
        private void butUndo_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
            PicBoxEdit.Image = new Bitmap(EditedBitmap);
            PicBoxEdit.Width = EditedBitmap.Width;
            PicBoxEdit.Height = EditedBitmap.Height;
            PictureBoxLocation();
        }
              

        private void FillFilmStrip()
        {
            this.Update();//update the rendering to ensure the thumbnails are well displayed
            // Delete the old PictureBoxes.
            foreach (PictureBox pic in PictureBoxes)
            {
                pic.Click -= FilmStripBox_Click;
                pic.Image.Dispose();
                pic.Image = null;
                pic.Dispose();                
            }
            flpThumbnails.Controls.Clear();


            if (clsGlobalVariables.WizardPagePrevious != "WizardPagePrintSummary")
            {
                SelectionChecks();
            }
           
            // Load the file Thumbnails.
            foreach (string filename in clsGlobalVariables.EditedPics)
            {
                
                PictureBox pic = new PictureBox();
               
                pic.ClientSize = new Size(ThumbWidth, ThumbHeight);

                using (Image filenamex = Image.FromFile(filename))
                {
                    Image thumb = filenamex.GetThumbnailImage(80, 80, () => false, IntPtr.Zero);
                    filenamex.Dispose();
                    pic.Image = new Bitmap(thumb); //load the image into the PictureBox pic
                }
                
                
                    pic.SizeMode = PictureBoxSizeMode.CenterImage;
               
                // Add the Click event handler.
                pic.Click += FilmStripBox_Click;


                pic.Tag = filename;// file_info;
              
                // Add the PictureBox to the FlowLayoutPanel.              
                flpThumbnails.WrapContents = false;
                pic.Parent = flpThumbnails;
                this.Update();//update the rendering to ensure the thumbnails are well displayed
            }

        }

        private void butScrollR_Click(object sender, EventArgs e)
        {       
            
            flpThumbnails.AutoScroll = true;         
            flpThumbnails.HorizontalScroll.Visible = false;

            if (flpThumbnails.HorizontalScroll.Maximum - flpThumbnails.HorizontalScroll.Value >= 100)
            {
                flpThumbnails.HorizontalScroll.Value = flpThumbnails.HorizontalScroll.Value + 100;
            }
            else
            {
                flpThumbnails.HorizontalScroll.Value = flpThumbnails.HorizontalScroll.Maximum;
            }
            
        }

        private void butScrollL_Click(object sender, EventArgs e)
        {
           
            flpThumbnails.AutoScroll = true;            
            flpThumbnails.HorizontalScroll.Visible = false;

            if (flpThumbnails.HorizontalScroll.Value >= 100)
            {
                flpThumbnails.HorizontalScroll.Value = flpThumbnails.HorizontalScroll.Value - 100;
            }
            else
            {
                flpThumbnails.HorizontalScroll.Value = flpThumbnails.HorizontalScroll.Minimum;
            }
        }
        
         // Select the clicked PictureBox.
        private void FilmStripBox_Click(object sender, EventArgs e) // code cleaned
        {
            panning = true;
            crop = false;
            btnCrop.Text = "Crop";
            SavePictureEdits(); 
            PictureBox pic = sender as PictureBox;

            if (SelectedPictureBox == pic) return; //if picture already selected, return

            if (SelectedPictureBox != null) //if a different picture selected, clear former selection
               {
                   SelectedPictureBox.BorderStyle = BorderStyle.None;
                   SelectedPictureBox.BackColor = Color.White;
               }
               
            // Select the clicked PictureBox.
            SelectedPictureBox = pic; // save a reference of the newly clicked picture
            SelectedPictureBox.BorderStyle = BorderStyle.Fixed3D; // select the picture
            SelectedPictureBox.BackColor = Color.Pink;
            String file_info = pic.Tag.ToString();
            newPicture = true;            
            LoadImage(file_info);
        }

        private void butFrameColor_Click(object sender, EventArgs e)
        {
            
            clsGlobalFunctions.CollectStatistics(sender);
            // Show the color dialog.
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed OK.
            if (result == DialogResult.OK)
            {
                // Set Border to the selected color.
                BorderColor = colorDialog1.Color;
                butFrameColor.BackColor = BorderColor;
                butFrameColor.ForeColor = clsGlobalFunctions.ContrastColor(BorderColor);
            }
            DrawFrame();
        }

        private void PictureBox1_VisibleChanged(object sender, EventArgs e)
        {          
            FillFilmStrip();           
        }
        
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (Image Img = Image.FromFile(PicBoxEdit.Tag.ToString()))
            {
                EditedBitmap = new Bitmap(Img);
            }            
        }       
        
        private void SelectionChecks()
        {
            
            foreach (string OrgFilePath in clsGlobalVariables.SelectedPics)
            {
                FileInfo file_info = new FileInfo(OrgFilePath);
                String FileName = file_info.Name;
                String NewFilePath = @"C:\iPrint\iPrintEditor\" + FileName; //to be changed

              
                if (!clsGlobalVariables.EditedPics.Contains(NewFilePath))
                {
                    clsGlobalVariables.EditedPics.Add(NewFilePath);
                    //File.Copy(OrgFilePath, NewFilePath, true); 
                    //Compress image to 50%
                    jpgCompress.VaryQualityLevel(OrgFilePath, NewFilePath);
                }               
            }
            //=============================================
            //======== To be moved to NEXT button =========

            if (clsGlobalVariables.WizardPagePrevious == "WizardPageUSB")
            {
                OriginalFilePath_Temp = clsGlobalVariables.DiskFilesDirectory;
            }

            if (clsGlobalVariables.WizardPagePrevious == "WizardPageCamera")
            {
                OriginalFilePath_Temp = clsGlobalVariables.SnapshotFilesDirectory;
            }

            if (clsGlobalVariables.WizardPagePrevious == "WizardPageScanner")
            {
                OriginalFilePath_Temp = clsGlobalVariables.ScannerFilesDirectory;
            }
                        
            //=============================================
           
            int EditedPics_Count = clsGlobalVariables.EditedPics.Count;
            int b = -1;
            for (int y = 0; y < EditedPics_Count; y++) 
            {
                b = b + 1;
                string NewFilePath = clsGlobalVariables.EditedPics[b];
                
                FileInfo file_info = new FileInfo(NewFilePath);
                
                String FileName = file_info.Name;

                String OriginalFilePath;

                OriginalFilePath = OriginalFilePath_Temp + FileName;                
               
                if (!clsGlobalVariables.SelectedPics.Contains(OriginalFilePath))
                {
                    clsGlobalVariables.EditedPics.Remove(NewFilePath);
                    b = b - 1;                    
                    File.Delete(NewFilePath);                  
                }
            }
          
        }

        private void SavePictureEdits()
        {
            if (PicBoxEdit.Tag != null)
            {
                String FilePath = PicBoxEdit.Tag.ToString();                
                File.Delete(FilePath); //add try here
                PicBoxEdit.Image.Save(FilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void btnGrayScale_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
            using (Bitmap bm_source = new Bitmap(PicBoxEdit.Image))
            {
                PicBoxEdit.Image = clsGlobalFunctions.MakeGrayscale3(bm_source);
            }           
        }
        
        void DrawPassportBox()
        {
            cropRectangle = passportBoxRect;
            cropRectangle.X = (PicBoxEdit.Width - passportBoxRect.Width) / 2; 
            cropRectangle.Y = (PicBoxEdit.Height - passportBoxRect.Height) / 2;
            rect.rect = cropRectangle;           
        }
       
        private void PicBoxEdit_Paint(object sender, PaintEventArgs e)
        {            

            if (PicBoxEdit.Image != null)
            {

            using (Bitmap bm_source = new Bitmap(PicBoxEdit.Image))
              {

                if (bm_source.Width > clsGlobalVariables.rect64.Width)
                {

                    e.Graphics.Clear(Color.White);
                 

                    if (movingPoint.X > clsGlobalVariables.rect64.X)
                    {
                        movingPoint.X = clsGlobalVariables.rect64.X;
                    }

                    int xPanOverlap = (movingPoint.X + bm_source.Width) - (clsGlobalVariables.rect64.X + clsGlobalVariables.rect64.Width);
                    if (xPanOverlap < 0)
                    {
                        movingPoint.X = movingPoint.X - xPanOverlap;
                    }
                }
                else
                {
                    movingPoint.X = 0;
                }

                if (bm_source.Height > clsGlobalVariables.rect64.Height)
                {
                    e.Graphics.Clear(Color.White);

                    if (movingPoint.Y > clsGlobalVariables.rect64.Y)
                    {
                        movingPoint.Y = clsGlobalVariables.rect64.Y;
                    }

                    int yPanOverlap = (movingPoint.Y + bm_source.Height) - (clsGlobalVariables.rect64.Y + clsGlobalVariables.rect64.Height);
                    if (yPanOverlap < 0)
                    {
                        movingPoint.Y = movingPoint.Y - yPanOverlap;
                    }
                }

                else
                {
                    movingPoint.Y = 0;
                }
                
                if (newPicture)
                {
                    movingPoint.X = 0;
                    movingPoint.Y = 0;
                    newPicture = false;
                }
                
                e.Graphics.DrawImage(bm_source, movingPoint);
             }
            }
            GC.Collect();
        }
      
        private void btnCrop_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
            GraphicsUnit units = GraphicsUnit.Point;

            RectangleF imgRectangleF = PicBoxEdit.Image.GetBounds(ref units);
            Rectangle imgSize = Rectangle.Round(imgRectangleF);
                       
                if (btnCrop.Text == "Crop")
                {                   
                    crop = true;
                    btnCrop.Text = "Apply";         
                    rect.rect = smallerCropRectanle(imgSize);
                    newPicture = true; 
                    PicBoxEdit.Invalidate();                                       
                }

                else
                {                    
                    cropPicture(rect.rect);
                    newPicture = true;
                    PicBoxEdit.Invalidate();
                    btnCrop.Text = "Crop";
                    crop = false;
                }           
        }

        private void btnApplyPP_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
            using(Bitmap bm_source = new Bitmap(PicBoxEdit.Image))
            {

                tilePic = new TilePicture(bm_source);

                PicBoxEdit.Image = tilePic.ArrangePicture();                
                newPicture = true;

                String FilePath = PicBoxEdit.Tag.ToString();
                clsGlobalVariables.PicsToExclude.Add(FilePath);
                FilePath = FilePath.Replace(".", "_PP_1.");
                PicBoxEdit.Tag = FilePath;

                PicBoxEdit.Invalidate();
            }
        }

        private void btnPPSize_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
            if (btnPPSize.Text == "Passport Size")
            {
                crop = true;
                passport = true;
                DrawPassportBox();
                PicBoxEdit.Invalidate();
                btnCrop.Text = "Apply";
                btnPPSize.Text = "Cancel PP Size";
            }
            else
            {
                crop = false;
                passport = false;                
                PicBoxEdit.Invalidate();
                btnCrop.Text = "Crop";
                btnPPSize.Text = "Passport Size";
            }
        }

        private Rectangle smallerCropRectanle(Rectangle imageRect)
        {
            bool modified = false;
            int width = 0;
            int height = 0;
            int x = 0;
            int y = 0;

            if (imageRect.Width <= clsGlobalVariables.rect64.Width)
            {
                width = imageRect.Width - 30;
                x = imageRect.X + 15;
                modified = true;
            }

            else
            {
                width = clsGlobalVariables.rect64.Width - 30;
                x = clsGlobalVariables.rect64.X + 15;
            }

            if (imageRect.Height <= clsGlobalVariables.rect64.Height)
            {
                height = imageRect.Height - 30;
                y = imageRect.Y + 15;
                modified = true;
            }

            else
            {
                height = clsGlobalVariables.rect64.Height - 30;
                y = clsGlobalVariables.rect64.Y + 15;
            }

            Rectangle smallerCropRectangle = new Rectangle(x, y, width, height); 
            
            if(modified == false)
            {
                smallerCropRectangle = clsGlobalVariables.rect64;
            }

            return smallerCropRectangle;

        }

        private void radBtnX4_CheckedChanged(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
        }

        private void radBtnX8_CheckedChanged(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
        }

        private void radBtnX12_CheckedChanged(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
        }       

	}
}
