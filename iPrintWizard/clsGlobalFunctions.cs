using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace iPrint
{
    public class clsGlobalFunctions
    {

        public static Bitmap MakeGrayscale3(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            using (Graphics g = Graphics.FromImage(newBitmap))
            {

                //create the grayscale ColorMatrix
                System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix(
                   new float[][] 
                {
                new float[] {.3f, .3f, .3f, 0, 0},
                new float[] {.59f, .59f, .59f, 0, 0},
                new float[] {.11f, .11f, .11f, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
                });

                //create some image attributes
                System.Drawing.Imaging.ImageAttributes attributes = new System.Drawing.Imaging.ImageAttributes();

                //set the color matrix attribute
                attributes.SetColorMatrix(colorMatrix);

                //draw the original image on the new image
                //using the grayscale color matrix
                g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                   0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

                //dispose the Graphics object
            }
            return newBitmap;
        }

        public static Color ContrastColor(Color color)
        {
            int d = 0;

            // Counting the perceptive luminance - human eye favors green color... 
            double a = 1 - (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;

            if (a < 0.5)
                d = 0; // bright colors - black font
            else
                d = 255; // dark colors - white font

            return Color.FromArgb(d, d, d);
        }

        public static Bitmap Make_BW(Bitmap original)
        {

            Bitmap output = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {

                for (int j = 0; j < original.Height; j++)
                {

                    Color c = original.GetPixel(i, j);

                    int average = ((c.R + c.B + c.G) / 3);

                    if (average < 200)
                        output.SetPixel(i, j, Color.Black);

                    else
                        output.SetPixel(i, j, Color.White);

                }
            }

            return output;

        }

        public static void CollectStatistics(object sender)
        {
            Control This = (Control)sender;
          
            int index = Int32.Parse(This.Tag.ToString());

            var bitArray = new BitArray(clsGlobalVariables.bytearray);

            bitArray.Set(index, true);

            bitArray.CopyTo(clsGlobalVariables.bytearray, 0);
            clsGlobalVariables.StatisticsRecord = Convert.ToString(BitConverter.ToInt32(clsGlobalVariables.bytearray, 0), 2).PadLeft(32, '0');
            //bytearray.GetValue(0).ToString();         
           
        }

        public static void ResetStatistics()
        {
            clsGlobalVariables.bytearray = new byte[4] { 0, 0, 0, 0 };
        }

        public static void BmpToPrint()
        {
            Bitmap bm_source;

            foreach (string fileName in Directory.GetFiles(@"C:\iPrint\iPrintEditor\"))
            {
                if (Path.GetExtension(fileName) == ".jpg")
                {
                    bm_source = new Bitmap(fileName);
                    PrintDoc(bm_source);
                }
            }

        }
        private static void PrintDoc(Bitmap sendPrint)
        {
            System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();//NULL, args
            pd.PrintPage += (NULL, args) =>
            {
                using (Image i = new Bitmap(sendPrint))//never tested after implementing using
                {
                    // Point p = new Point(100, 100);
                    args.Graphics.DrawImage(i, 0, 0, i.Width, i.Height);
                }
            };

            pd.Print();
        }
    }

      
}
