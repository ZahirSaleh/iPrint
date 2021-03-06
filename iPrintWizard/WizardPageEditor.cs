﻿using System;
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
using ExtensionMethods;
using System.Linq; 

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
        private System.Drawing.Point startingPoint = System.Drawing.Point.Empty;
        private System.Drawing.Point movingPoint = System.Drawing.Point.Empty;        
        private bool newPicture = false;      
        String OriginalFilePath_Temp;
        //==========Public Variables==========
        public static bool crop = false;
        public static bool panning = false;
        public static bool passport = false;
        //===========New Additions=============
        Stack<Image> UChanges = new Stack<Image>(5);
        Stack<Image> RChanges = new Stack<Image>(5);
        //====================================

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
            SetButtonProperties();
            // on invalidate we want to be able to draw the rectangle
           // PicBoxEdit.Paint += new PaintEventHandler(PicBoxEdit_Paint);
		}

        public void UCAdd(Image img)
        {
            UChanges.Push(img);
            buttUndo.Enabled = true;
            UpdatePictureBox();
        }

        public void RCAdd(Image img)
        {
            RChanges.Push(img);
            buttRedo.Enabled = true;
            UpdatePictureBox();
        }

        private void SetButtonProperties()
        {
            // Assign an image to the button.                       
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream StreamUndo = myAssembly.GetManifestResourceStream("iPrint.Undo.bmp");
            Bitmap imageUndo = new Bitmap(StreamUndo);
            buttUndo.Image = imageUndo;
            // Align the image and text on the button.
            buttUndo.ImageAlign = ContentAlignment.MiddleRight;
            buttUndo.TextAlign = ContentAlignment.MiddleLeft;
            // Give the button a flat appearance.
            buttUndo.FlatStyle = FlatStyle.Flat;

            Stream StreamRedo = myAssembly.GetManifestResourceStream("iPrint.Redo.bmp");
            Bitmap imageRedo = new Bitmap(StreamRedo);
            buttRedo.Image = imageRedo;
            // Align the image and text on the button.
            buttRedo.ImageAlign = ContentAlignment.MiddleRight;
            buttRedo.TextAlign = ContentAlignment.MiddleLeft;
            // Give the button a flat appearance.
            buttRedo.FlatStyle = FlatStyle.Flat;

            Stream StreamApply = myAssembly.GetManifestResourceStream("iPrint.Apply.png");
            Bitmap imageApply = new Bitmap(StreamApply);
            butApply.Image = imageApply;
            // Align the image and text on the button.
            butApply.ImageAlign = ContentAlignment.MiddleRight;
            butApply.TextAlign = ContentAlignment.MiddleLeft;
            // Give the button a flat appearance.
            butApply.FlatStyle = FlatStyle.Flat;           
        }

        private void LoadImage(String Filename)
        {
                        
       
            Image img = System.Drawing.Image.FromFile(Filename);
            MemoryStream imgStream = new MemoryStream();
            img.Save(imgStream, System.Drawing.Imaging.ImageFormat.Bmp);            
            int imgWidth = img.Width;
            int imghieght = img.Height;
           
            PicBoxEdit.Image = System.Drawing.Image.FromStream(imgStream);
            PicBoxEdit.Tag = Filename;
            UpdatePictureBox();
            OriginalImageSize = new Size(imgWidth, imghieght);
            SetResizeInfo();
            img.Dispose();
            GC.Collect();
            RChanges.Clear();
            UChanges.Clear();

        }

       

        private void SetResizeInfo()
        {
            lbloriginalSize.Text = OriginalImageSize.ToString();
            lblModifiedSize.Text = ModifiedImageSize.ToString();
        }     

                   
        # region "-----------------------------Crop Image------------------------------------"      
        private void cropPicture(Rectangle cropRect)
        {

            Cursor = Cursors.Default;

            try
            {              
                Rectangle rect1 = new Rectangle(cropRect.X - movingPoint.X, cropRect.Y - movingPoint.Y, cropRect.Width, cropRect.Height);
                          
                UCAdd(PicBoxEdit.Image);
                System.Drawing.Bitmap image = (Bitmap)PicBoxEdit.Image;
                PicBoxEdit.Image = image.Crop(rect1);                                 
                UpdatePictureBox();
                RChanges.Clear();              
            }
            catch //(Exception ex)
            {
            }
           
            passport = false;
            crop = false;
        }
        
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            startingPoint = new System.Drawing.Point(e.Location.X - movingPoint.X, e.Location.Y - movingPoint.Y);
                   
        }
        
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (panning && (e.Button == System.Windows.Forms.MouseButtons.Left))//Panning done here
            {
                movingPoint = new System.Drawing.Point(e.Location.X - startingPoint.X,
                                e.Location.Y - startingPoint.Y);
                PicBoxEdit.Invalidate();
            }          

        }
        # endregion

      
        private void UpdatePictureBox()
        {
            PicBoxEdit.Width = PicBoxEdit.Image.Width;
            PicBoxEdit.Height = PicBoxEdit.Image.Height;
            PicBoxEdit.SizeMode = PictureBoxSizeMode.CenterImage;

            PicBoxEdit.Location = new System.Drawing.Point((panelMainPic.Width - PicBoxEdit.Width) / 2,
             (panelMainPic.Height - PicBoxEdit.Height) / 2);

            PicBoxEdit.Refresh();
        }

        private void btnRotateLeft_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
        
            UCAdd(PicBoxEdit.Image);
            System.Drawing.Bitmap image = (Bitmap)PicBoxEdit.Image;
            PicBoxEdit.Image = image.rotate(90);
            
            UpdatePictureBox();
            RChanges.Clear();
            
        }

        private void btnRotateRight_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);

            UCAdd(PicBoxEdit.Image);
            System.Drawing.Bitmap image = (Bitmap)PicBoxEdit.Image;
            PicBoxEdit.Image = image.rotate(270);
            
            UpdatePictureBox();
            RChanges.Clear();
        }

        private void btnRotateHorizantal_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);

            UCAdd(PicBoxEdit.Image);
            System.Drawing.Bitmap image = (Bitmap)PicBoxEdit.Image;
            PicBoxEdit.Image = image.Mirror(false, true);
            
            UpdatePictureBox();
            RChanges.Clear();
        }

        private void btnRotatevertical_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);

            UCAdd(PicBoxEdit.Image);
            System.Drawing.Bitmap image = (Bitmap)PicBoxEdit.Image;
            PicBoxEdit.Image = image.Mirror(true, false);
            
            UpdatePictureBox();
            RChanges.Clear();
        }
      
        private void DrawFrame()
        {            
            System.Drawing.Bitmap image = (Bitmap)PicBoxEdit.Image;
            PicBoxEdit.Image = image.DrawFrame(BorderWidth, BorderColor);
            
            UpdatePictureBox();                   
        }      
            
              
        private void tBarFrameWidth_Scroll(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
            numericUpDown1.Text = tBarFrameWidth.Value.ToString();

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
       
                   

        private void FillFilmStrip()
        {
            this.Update();//update the rendering to ensure the thumbnails are well displayed
            // Delete the old PictureBoxes.
            foreach(PictureBox pic in PictureBoxes)
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
            UCAdd(PicBoxEdit.Image);
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
            
            RChanges.Clear();
        }

        private void PictureBox1_VisibleChanged(object sender, EventArgs e)
        {          
            FillFilmStrip();           
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
           // clsGlobalFunctions.CollectStatistics(sender);
            UCAdd(PicBoxEdit.Image);
            System.Drawing.Bitmap image = (Bitmap)PicBoxEdit.Image;
            PicBoxEdit.Image = image.ToBlackAndWhite();
            RChanges.Clear();
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
                      
                    crop = true;                         
                    rect.rect = smallerCropRectanle(imgSize);
                    newPicture = true; 
                    PicBoxEdit.Invalidate();     

            butApplyCrop.Visible = true;
            butCancelCrop.Visible = true;
            btnCrop.Visible = false;                      
          
        }

        private void btnApplyPP_Click(object sender, EventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);

            butApplyCrop_Click(sender, e);

            using(Bitmap bm_source = new Bitmap(PicBoxEdit.Image))
            {

                tilePic = new TilePicture(bm_source);

                PicBoxEdit.Image = tilePic.ArrangePicture();                
                newPicture = true;

                String FilePath = PicBoxEdit.Tag.ToString();
                clsGlobalVariables.PicsToExclude.Add(FilePath);
                FilePath = FilePath.Replace(".", "_PP_1.");
                PicBoxEdit.Tag = FilePath;
                UpdatePictureBox();
                //PicBoxEdit.Invalidate();
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
                UpdatePictureBox(); //PicBoxEdit.Invalidate();
                //btnCrop.Text = "Apply";
               // butApplyCrop.Visible = true;
                //butCancelCrop.Visible = true;
                //btnCrop.Visible = false;  
                
                btnPPSize.Text = "Cancel PP Size";
            }
            else
            {
                crop = false;
                passport = false;
                UpdatePictureBox(); //PicBoxEdit.Invalidate();
                //btnCrop.Text = "Crop";
               // butApplyCrop.Visible = false;
               // butCancelCrop.Visible = false;
               // btnCrop.Visible = true;  
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

        private void tBarResize_Scroll(object sender, EventArgs e)
        {
            int percentage = 0;
            try
            {

                percentage = tBarResize.Value;
                ModifiedImageSize = new Size((OriginalImageSize.Width * percentage) / 100, (OriginalImageSize.Height * percentage) / 100);
                
                SetResizeInfo();
               
                System.Drawing.Bitmap image = (Bitmap)PicBoxEdit.Image;
                PicBoxEdit.Width = ModifiedImageSize.Width;
                PicBoxEdit.Height = ModifiedImageSize.Height;
                PicBoxEdit.SizeMode = PictureBoxSizeMode.CenterImage;
                PicBoxEdit.Image = image.resize(ModifiedImageSize.Width, ModifiedImageSize.Height);
                UpdatePictureBox();
               

            }
            catch //(Exception ex)
            {
                MessageBox.Show("Invalid Percentage");
                return;
            }
        }

        private void butApplyCrop_Click(object sender, EventArgs e)
        {
           // clsGlobalFunctions.CollectStatistics(sender);
           

            cropPicture(rect.rect);
            newPicture = true;
            PicBoxEdit.Invalidate();
            btnCrop.Text = "Crop";
            crop = false;

            butApplyCrop.Visible = false;            
            butCancelCrop.Visible = false;
            btnCrop.Visible = true;      
        }

        private void butCancelCrop_Click(object sender, EventArgs e)
        {
            newPicture = true;
            PicBoxEdit.Invalidate();
           
            butApplyCrop.Visible = false;
            butCancelCrop.Visible = false;
            btnCrop.Visible = true; 
        }

        private void tBarResize_MouseUp(object sender, MouseEventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);               
            RChanges.Clear();              
        }
              

        private void butSepia_Click(object sender, EventArgs e)
        {
            UCAdd(PicBoxEdit.Image);
            System.Drawing.Bitmap image = (Bitmap)PicBoxEdit.Image;
            PicBoxEdit.Image = image.ToSepia();
            RChanges.Clear();
        }
        private void buttUndo_Click(object sender, EventArgs e)
        {
            if (UChanges.Count != 0)
            {
                RCAdd(PicBoxEdit.Image);
                System.Drawing.Bitmap image = (Bitmap)UChanges.Pop();               
                PicBoxEdit.Image = image;
                UpdatePictureBox();               
                if (UChanges.Count == 0)
                {
                    buttUndo.Enabled = false;
                }
                else
                {
                    buttUndo.Enabled = true;
                }
            }
        }
        private void buttRedo_Click(object sender, EventArgs e)
        {
            if (RChanges.Count != 0)
            {
                UCAdd(PicBoxEdit.Image);
                System.Drawing.Bitmap image = (Bitmap)RChanges.Pop();                
                PicBoxEdit.Image = image;
                UpdatePictureBox();
                

                if (RChanges.Count == 0)
                {
                    buttRedo.Enabled = false;
                }
                else
                {
                    buttRedo.Enabled = true;
                }
            }
        }

        private void butPixellate_Click(object sender, EventArgs e)
        {
            UCAdd(PicBoxEdit.Image);
            System.Drawing.Bitmap image = (Bitmap)PicBoxEdit.Image;
            PicBoxEdit.Image = image.ToPixalation();
            RChanges.Clear();
        }

        private void butJitter_Click(object sender, EventArgs e)
        {
            UCAdd(PicBoxEdit.Image);
            System.Drawing.Bitmap image = (Bitmap)PicBoxEdit.Image;
            PicBoxEdit.Image = image.ToJitter();
            RChanges.Clear();
        }

        private void butGaussianBlur_Click(object sender, EventArgs e)
        {           
                
                
                         
        }

        private void butInvertColour_Click(object sender, EventArgs e)
        {
            UCAdd(PicBoxEdit.Image);
            System.Drawing.Bitmap image = (Bitmap)PicBoxEdit.Image;
            PicBoxEdit.Image = image.ToInvert();
            RChanges.Clear();
        }

        private void TrackBarBrightness_MouseUp(object sender, MouseEventArgs e)
        {
            clsGlobalFunctions.CollectStatistics(sender);
            RChanges.Clear();           
        }

        private void tBarFrameWidth_MouseUp(object sender, MouseEventArgs e)
        {

            clsGlobalFunctions.CollectStatistics(sender);
            RChanges.Clear();           
        }

        private void tBarResize_MouseDown(object sender, MouseEventArgs e)
        {
            UCAdd(PicBoxEdit.Image);
        }

        private void TrackBarBrightness_MouseDown(object sender, MouseEventArgs e)
        {
            UCAdd(PicBoxEdit.Image);
        }

        private void TrackBarBrightness_Scroll(object sender, EventArgs e)
        {            
            System.Drawing.Bitmap image = (Bitmap)PicBoxEdit.Image;
            PicBoxEdit.Image = image.BrightnessControl(TrackBarBrightness.Value);            
            UpdatePictureBox();
        }

        private void tBarFrameWidth_MouseDown(object sender, MouseEventArgs e)
        {
            UCAdd(PicBoxEdit.Image);
        }

        private void tBarGaussianBlur_MouseDown(object sender, MouseEventArgs e)
        {
            UCAdd(PicBoxEdit.Image);
        }

        private void tBarGaussianBlur_Scroll(object sender, EventArgs e)
        {
            System.Drawing.Bitmap image = (Bitmap)PicBoxEdit.Image;
            PicBoxEdit.Image = image.gaussianBlur(tBarGaussianBlur.Value);
        }

        private void tBarGaussianBlur_MouseUp(object sender, MouseEventArgs e)
        {
            RChanges.Clear(); 
        }       

	}
}
