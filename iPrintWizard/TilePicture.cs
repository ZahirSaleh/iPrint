using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace iPrint
{
    public class TilePicture
    {
        public Bitmap passportPic;

        public TilePicture(Bitmap pic)
        {
            passportPic = new Bitmap(pic);
        }
        
        public Bitmap ArrangePicture()//Bitmap tile)
        {
            //String path = @"c:\test\";

            //Bitmap tile = new Bitmap(path + "b.jpg");
            int orgWidth = passportPic.Width;
            int orgHeight = passportPic.Height;

            Bitmap dst = new Bitmap((orgWidth * 2) + 3, (passportPic.Height * 2) + 3);

            if ((orgWidth >= 260 && orgWidth <= 300) && (orgHeight >= 360 && orgHeight <= 400))
            {
                using (Graphics g = Graphics.FromImage(dst))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Rectangle destRect = new Rectangle((i % 2 * orgWidth) + (i % 2 * 3), (i / 2 * orgHeight) + (i / 2 * 3), orgWidth, orgHeight);
                        g.DrawImage(passportPic, destRect);
                    }
                }

                //dst.Save(path + "tiled.jpg");
                
            }
            else
            {
                MessageBox.Show("You must resize or crop you picture to passport size (290 X 390)");
                dst = passportPic;
            }
            return dst;
        }
    }
}
