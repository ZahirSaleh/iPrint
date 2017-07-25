using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using AForge;

namespace ExtensionMethods
{
    public static class Effects
    {
        public static Bitmap ToSepia(this Bitmap bitmap)
        {
            // create filter
            AForge.Imaging.Filters.Sepia filter = new AForge.Imaging.Filters.Sepia();
            // apply the filter
            return filter.Apply(bitmap);
        }

        public static Bitmap ToPixalation(this Bitmap bitmap)
        {
            // create filter
            AForge.Imaging.Filters.Pixellate filter = new AForge.Imaging.Filters.Pixellate();
            // apply the filter
            return filter.Apply(bitmap);
        }

        public static Bitmap resize(this Bitmap bitmap, int width, int height)
        {
            // create filter
            AForge.Imaging.Filters.ResizeBicubic filter = new AForge.Imaging.Filters.ResizeBicubic(width, height);
            // apply the filter
            return filter.Apply(bitmap);
        }

        public static Bitmap rotate(this Bitmap bitmap, int degrees)
        {
            // create filter
            AForge.Imaging.Filters.RotateBicubic filter = new AForge.Imaging.Filters.RotateBicubic(degrees);
            // apply the filter
            return filter.Apply(bitmap);
        }

        public static Bitmap Mirror(this Bitmap bitmap, bool MirrorX, bool MirrorY)
        {
            // create filter
            AForge.Imaging.Filters.Mirror filter = new AForge.Imaging.Filters.Mirror(MirrorX, MirrorY);
            // apply the filter
            return filter.Apply(bitmap);
        }

        public static Bitmap ToBlackAndWhite(this Bitmap bitmap)
        {
            // create grayscale filter (BT709)
            AForge.Imaging.Filters.Grayscale filter = new AForge.Imaging.Filters.Grayscale(0.2125, 0.7154, 0.0721);
            // apply the filter
            return filter.Apply(bitmap);            
        }

        public static Bitmap Crop(this Bitmap bitmap, Rectangle cropRect)
        {
            // create filter
            AForge.Imaging.Filters.Crop filter = new AForge.Imaging.Filters.Crop(cropRect);
            // apply the filter
            return filter.Apply(bitmap);
        }

        public static Bitmap DrawFrame(this Bitmap bitmap, int boarderThickness, Color boarderColor)
        {
            // create filter
            AForge.Imaging.Filters.CanvasCrop filter = new AForge.Imaging.Filters.CanvasCrop(
                new Rectangle(boarderThickness, boarderThickness, bitmap.Width - (boarderThickness * 2), bitmap.Height - (boarderThickness * 2)), boarderColor);
            // apply the filter
            return filter.Apply(bitmap);          
        }

       
         public static Bitmap BrightnessControl(this Bitmap bitmap, int brightVal)
        {
             // create filter
             AForge.Imaging.Filters.BrightnessCorrection filter = new AForge.Imaging.Filters.BrightnessCorrection(brightVal);
             // apply the filter
             return filter.Apply(bitmap);
        }

        public static Bitmap gaussianBlur(this Bitmap bitmap, int blur)
        {
            // create filter
            AForge.Imaging.Filters.GaussianBlur filter = new AForge.Imaging.Filters.GaussianBlur(Convert.ToDouble(blur), 9);
            // apply the filter
            return filter.Apply(bitmap);
        }

        public static Bitmap ToJitter(this Bitmap bitmap)
        {
            // create filter
            AForge.Imaging.Filters.Jitter filter = new AForge.Imaging.Filters.Jitter();
            // apply the filter
            return filter.Apply(bitmap);
        }

        public static Bitmap ToInvert(this Bitmap bitmap)
        {
            // create filter
            AForge.Imaging.Filters.Invert filter = new AForge.Imaging.Filters.Invert();
            // apply the filter
            return filter.Apply(bitmap);
        }
    }
}