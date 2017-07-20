using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace iPrint
{
    public class ImageResize
    {
        public static Image ResizeImage(Image image, bool preserveAspectRatio = true)
        {
            int newWidth;            
            int newHeight;

            Size size = StandardRect_6_4(new Bitmap(image));

            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        private static Size StandardRect_6_4(Bitmap sourceBitmap)
        {

            Size RectSize = new Size(0,0);

            float imageWidhtPrintPix = 6 * sourceBitmap.HorizontalResolution;
            float imageHeightPrintPix = 4 * sourceBitmap.VerticalResolution;

            RectSize.Width = Convert.ToInt32(imageWidhtPrintPix);
            RectSize.Height = Convert.ToInt32(imageHeightPrintPix);

            return RectSize;

        }

    }
}
