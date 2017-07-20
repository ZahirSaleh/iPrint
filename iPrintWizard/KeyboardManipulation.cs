using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace iPrint
{
    public class KeyboardManipulation
    {
        //  private const short SWP_NOMOVE = 0X2;
        private const short SWP_NOSIZE = 1;
        private const short SWP_NOZORDER = 0X4;
        private const int SWP_SHOWWINDOW = 0x0040;
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_CLOSE = 0xF060;

       // private int kbdx;
        //private int kbdy;

        IntPtr wKB;
        string kbdPath = @"C:\Projects\iPrint\iPrint\iPrintKeyBoard.exe";

        public KeyboardManipulation()
        {
        //====Constructor=======
        }

      /*  public int kbdX
        {
            get { return kbdx; }
           // set { firstName = value; }
        }

        public int kbdY
        {
            get { return kbdy; }
            // set { firstName = value; }
        }*/

        public void OpenKbd()//int kbdX, int kbdY)
        {
            Process process;
            process = Process.Start(kbdPath);

          /*  while (process.MainWindowTitle != "Touch Keypad")
            {
                Thread.Sleep(100);
            }
            PositionKbd(kbdX, kbdY);*/
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        public void PositionKbd(int kbdX, int kbdY)
        {
            wKB = FindWindow(null, "Touch Keypad");
            SetWindowPos(wKB, 0, kbdX, kbdY, 0, 0, SWP_NOZORDER | SWP_NOSIZE | SWP_SHOWWINDOW);
        }

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, int lParam);
        public void CloseKbd()
        {
            wKB = FindWindow(null, "Touch Keypad");
            if (wKB != null)
            {
                // close the window using API        
                SendMessage(wKB, WM_SYSCOMMAND, SC_CLOSE, 0);
            }  
        }
    }
}
