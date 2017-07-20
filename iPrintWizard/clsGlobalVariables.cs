using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Ozeki.Media.MediaHandlers;
//using Ozeki.Media.MediaHandlers.Video;
//using Ozeki.Media.Video.Controls;
using System.Drawing;
//using System.Drawing.Drawing2D;

namespace iPrint
{
    public class clsGlobalVariables
    {
        public static Boolean USB = false; //Stores user section USB option
        public static Boolean WEB = false; //Stores user section WEB option
        public static Boolean BLUETOOTH = false;//Stores user section Bluetooth option 
        public static Boolean CAMERA = false; //Stores user section Camera option
        public static Boolean SCANNER = false; //Stores user section Scanner Option
        public static Boolean Browsed = false;
        public static Boolean SummaryPageAccess = false;
        public static String SnapshotFilesDirectory;
        public static String DiskFilesDirectory;
        public static String ScannerFilesDirectory;
        public static String Transaction_Station_Station_Id = "01";
        public static Decimal StandardPhotoPrice;
        public static Decimal PassportPhoroPrice;
       // public static Boolean CameraOn = false;
        public static String WizardPageCurrent = "";
        public static String WizardPagePrevious = "";
        public static List<string> SelectedPics = new List<string>();
        public static List<string> EditedPics = new List<string>();
        public static List<string> PicsToExclude = new List<string>();
        public static Rectangle rect64;
        public static byte[] bytearray = new byte[4] { 0, 0, 0, 0 };
        public static String StatisticsRecord;
        public static String FirstName;
        public static String LastName;
        public static String PhoneNumber;
        public static Decimal TotalCost;
        public static int stdPhotoCount = 0;
        public static int ppPhotoCount = 0;
        public static Boolean USBBrowserShow = false;
        public static String USBDriveLetter = "";
        public static String USBDrivePath = "";
        public static Boolean AutoResize = false;
        //=========================================================

       
    }
}
