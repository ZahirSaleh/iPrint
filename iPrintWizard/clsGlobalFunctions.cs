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

        public static void ErrorLog(Exception ex)
        {
            string filePath = @"C:\Error.txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                 "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
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
