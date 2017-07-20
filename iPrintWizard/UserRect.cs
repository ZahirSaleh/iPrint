using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using iPrint;

namespace CropRect
{
    public class UserRect 
    {        
        private PictureBox mPictureBox;
        public Rectangle rect;
        public bool allowDeformingDuringMovement=false ;
        private bool mIsClick=false;
        private bool mMove=false;        
        private int oldX;
        private int oldY;
        private int sizeNodeRect= 30;
        private Bitmap mBmp=null;
        private PosSizableRect nodeSelected = PosSizableRect.None;
        //private int angle = 30;
        bool panningTemp = false;


        private enum PosSizableRect
        {            
            UpMiddle,
            LeftMiddle,
            LeftBottom,
            LeftUp,
            RightUp,
            RightMiddle,
            RightBottom,
            BottomMiddle,
            None

        };

        public UserRect(Rectangle r)
        {
            rect = r;
            mIsClick = false;
        }

        public void Draw(Graphics g)
        {

                GraphicsUnit units = GraphicsUnit.Point;

                RectangleF imgRectangleF = mPictureBox.Image.GetBounds(ref units);
                Rectangle bigRectangle = Rectangle.Round(imgRectangleF);

                Region rgn = new Region(bigRectangle);//new Region(new Rectangle(50, 50, 200, 150));
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddRectangle(rect);
                rgn.Exclude(path);

                //param 1 = opacity, 0 = transparent, 255 = Opaque
                //param 2,3,4 = RGB, 0 = Black, 255 = White
                Brush brush = new SolidBrush(Color.FromArgb(180, 10, 10, 10));

                g.FillRegion(brush, rgn);

                Pen WhitePen = new Pen(Color.White, 2);
                g.DrawRectangle(WhitePen, rect);

                // Set the SmoothingMode property to smooth the line.
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                // e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                SolidBrush whiteBrush = new SolidBrush(Color.White);
                Pen blackPen = new Pen(Color.Black, 1);
                if (!iPrint.WizardPageEditor.passport)
                {
                    foreach (PosSizableRect pos in Enum.GetValues(typeof(PosSizableRect)))
                    {
                        g.FillEllipse(whiteBrush, GetRect(pos));
                        g.DrawEllipse(blackPen, GetRect(pos));
                    }
                }
                 
        }

        public void SetBitmapFile(string filename)
        {
            this.mBmp = new Bitmap(filename);
        }

        public void SetBitmap(Bitmap bmp)
        {
            this.mBmp = bmp;
        }

        public void SetPictureBox(PictureBox p)
        {
            this.mPictureBox = p;
            mPictureBox.MouseDown +=new MouseEventHandler(mPictureBox_MouseDown);
            mPictureBox.MouseUp += new MouseEventHandler(mPictureBox_MouseUp);
            mPictureBox.MouseMove += new MouseEventHandler(mPictureBox_MouseMove);            
            mPictureBox.Paint += new PaintEventHandler(mPictureBox_Paint);
        }

        private void mPictureBox_Paint(object sender, PaintEventArgs e)
        {
            
            try
            {
                if (iPrint.WizardPageEditor.crop)
                {
                    Draw(e.Graphics);
                }
            }
            catch (Exception exp)
            {
                System.Console.WriteLine(exp.Message);
            }

            if (mPictureBox.Image != null)
            {
                StandardRectange_6_4(new Bitmap(mPictureBox.Image), e.Graphics);
            }
            
        }

        private void mPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            mIsClick = true;

            nodeSelected = PosSizableRect.None;
            nodeSelected = GetNodeSelectable(e.Location);
                
            if (rect.Contains(new Point(e.X,e.Y)))
            {
               // mMove = true;                            
            }
            oldX = e.X;
            oldY = e.Y;
            panningTemp = iPrint.WizardPageEditor.panning;
        }

        private void mPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            mIsClick = false;
            mMove = false;
            iPrint.WizardPageEditor.panning = panningTemp;
        }
       
        private void mPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!iPrint.WizardPageEditor.passport)
            {
            ChangeCursor(e.Location);
            if (mIsClick == false)
            {
                return;
            }

           
                Rectangle backupRect = rect;

                if (nodeSelected != PosSizableRect.None)
                {
                    iPrint.WizardPageEditor.panning = false;
                }

                switch (nodeSelected)
                {

                    case PosSizableRect.LeftUp:
                        rect.X += e.X - oldX;
                        rect.Width -= e.X - oldX;
                        rect.Y += e.Y - oldY;
                        rect.Height -= e.Y - oldY;
                        break;
                    case PosSizableRect.LeftMiddle:
                        rect.X += e.X - oldX;
                        rect.Width -= e.X - oldX;
                        break;
                    case PosSizableRect.LeftBottom:
                        rect.Width -= e.X - oldX;
                        rect.X += e.X - oldX;
                        rect.Height += e.Y - oldY;
                        break;
                    case PosSizableRect.BottomMiddle:
                        rect.Height += e.Y - oldY;
                        break;
                    case PosSizableRect.RightUp:
                        rect.Width += e.X - oldX;
                        rect.Y += e.Y - oldY;
                        rect.Height -= e.Y - oldY;
                        break;
                    case PosSizableRect.RightBottom:
                        rect.Width += e.X - oldX;
                        rect.Height += e.Y - oldY;
                        break;
                    case PosSizableRect.RightMiddle:
                        rect.Width += e.X - oldX;
                        break;

                    case PosSizableRect.UpMiddle:
                        rect.Y += e.Y - oldY;
                        rect.Height -= e.Y - oldY;
                        break;

                    default:
                        if (mMove)
                        {
                            rect.X = rect.X + e.X - oldX;
                            rect.Y = rect.Y + e.Y - oldY;
                        }
                        break;
                }
                oldX = e.X;
                oldY = e.Y;

                if (rect.Width < 5 || rect.Height < 5)
                {
                    rect = backupRect;
                }

                TestIfRectInsideArea();

                mPictureBox.Invalidate();
            }  
        }

        private void TestIfRectInsideArea()
        {
            // Test if rectangle still inside the area.
            if (rect.X < 0) rect.X = 0;
            if (rect.Y < 0) rect.Y = 0;
            if (rect.Width <= 0) rect.Width = 1;
            if (rect.Height <= 0) rect.Height = 1;

            if (rect.X + rect.Width > mPictureBox.Width)
            {
                rect.Width = mPictureBox.Width - rect.X - 1; // -1 to be still show 
                if (allowDeformingDuringMovement == false)
                {
                    mIsClick = false;
                }
            }
            if (rect.Y + rect.Height > mPictureBox.Height)
            {
                rect.Height = mPictureBox.Height - rect.Y - 1;// -1 to be still show 
                if (allowDeformingDuringMovement == false)
                {
                    mIsClick = false;
                }
            }
        }        

        private Rectangle CreateRectSizableNode(int x, int y)
        {
            return new Rectangle(x - sizeNodeRect / 2, y - sizeNodeRect / 2, sizeNodeRect, sizeNodeRect);   
        }

        private Rectangle GetRect(PosSizableRect p)
        {
            switch (p)
            {
                case PosSizableRect.LeftUp:
                    return CreateRectSizableNode(rect.X, rect.Y);
                 
                case PosSizableRect.LeftMiddle:
                    return CreateRectSizableNode(rect.X, rect.Y + +rect.Height / 2);                    

                case PosSizableRect.LeftBottom:
                    return CreateRectSizableNode(rect.X, rect.Y +rect.Height);                                   

                case PosSizableRect.BottomMiddle:
                    return CreateRectSizableNode(rect.X  + rect.Width / 2,rect.Y + rect.Height);

                case PosSizableRect.RightUp:
                    return CreateRectSizableNode(rect.X + rect.Width,rect.Y );

                case PosSizableRect.RightBottom:
                    return CreateRectSizableNode(rect.X  + rect.Width,rect.Y  + rect.Height);

                case PosSizableRect.RightMiddle:
                    return CreateRectSizableNode(rect.X  + rect.Width, rect.Y  + rect.Height / 2);

                case PosSizableRect.UpMiddle:
                    return CreateRectSizableNode(rect.X + rect.Width/2, rect.Y);
                default :
                    return new Rectangle();
            }
        }

        private PosSizableRect GetNodeSelectable(Point p)
        {
           foreach (PosSizableRect r in Enum.GetValues(typeof(PosSizableRect)))
            {
                if (GetRect(r).Contains(p))
                {
                    return r;                    
                }
            }
            return PosSizableRect.None;
        }

        private void ChangeCursor(Point p)
        {
            mPictureBox.Cursor = GetCursor(GetNodeSelectable(p));
        }

        /// <summary>
        /// Get cursor for the handle
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private Cursor GetCursor(PosSizableRect p)
        {
            switch (p)
            {
                case PosSizableRect.LeftUp:
                    return Cursors.SizeNWSE;               

                case PosSizableRect.LeftMiddle:
                    return Cursors.SizeWE;

                case PosSizableRect.LeftBottom:
                    return Cursors.SizeNESW;

                case PosSizableRect.BottomMiddle:
                    return Cursors.SizeNS;

                case PosSizableRect.RightUp:
                    return Cursors.SizeNESW;

                case PosSizableRect.RightBottom:
                    return Cursors.SizeNWSE;

                case PosSizableRect.RightMiddle:
                    return Cursors.SizeWE;

                case PosSizableRect.UpMiddle:
                    return Cursors.SizeNS;
                default:
                    return Cursors.Default;
            }
        }
        //A 6 x 4 inch photo print rectangle in red
        private void StandardRectange_6_4(Bitmap sourceBitmap, Graphics g)
        {

            float imageWidhtPrintPix = 6 * sourceBitmap.HorizontalResolution;
            float imageHeightPrintPix = 4 * sourceBitmap.VerticalResolution;

            int PixelWidth = Convert.ToInt32(imageWidhtPrintPix);
            int PixelHeight = Convert.ToInt32(imageHeightPrintPix);
                        
            //Rectangle 
            clsGlobalVariables.rect64 = new Rectangle(0, 0, PixelWidth, PixelHeight);

            //Centering the printable area box
            clsGlobalVariables.rect64.X = (sourceBitmap.Width - PixelWidth) / 2;
            clsGlobalVariables.rect64.Y = (sourceBitmap.Height - PixelHeight) / 2; 

            Pen RedPen = new Pen(Color.Red, 2);
            g.DrawRectangle(RedPen, clsGlobalVariables.rect64);

        }        

    }
}
