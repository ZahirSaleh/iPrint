using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;

//using System.Runtime.InteropServices;

namespace iPrint
{
	public partial class FormMain : Form
	{
       
        public FormMain()
		{
            InitializeComponent();            
		}

		private void buttonShowWizard_Click(object sender, EventArgs e)
		{
			WizardPagesLoader form = new WizardPagesLoader();
            
			form.ShowDialog();
		}

        private void FormMain_Load(object sender, EventArgs e)
        {
            MySQLConnection MySQLConnection1 = new MySQLConnection();

            if (CheckNetStatus())
            {
               MySQLConnection1.GetPrintingPrices();

                lablStdCost.Text = clsGlobalVariables.StandardPhotoPrice.ToString();
                lablPPCost.Text = clsGlobalVariables.PassportPhoroPrice.ToString();
            }

            else
            {
                lablStdCost.Text = "Not Available";
                lablPPCost.Text = "Not Available";
            }
        }
               

        private bool CheckNetStatus()
        {
            bool Success = false;
            try
            {
                if (new Ping().Send("www.iprintltd.com").Status == IPStatus.Success) 
                {
                    labNetStatus.Text = "ONLINE";
                    labNetStatus.BackColor = Color.Lime;
                    Success = true;
                }
                else
                {
                    labNetStatus.Text = "!! OFFLINE !!";
                    labNetStatus.BackColor = Color.Red;
                    Success = false;
                }
            }
            catch
            {
                Success = false;
                labNetStatus.Text = "!! OFFLINE !!";
                labNetStatus.BackColor = Color.Red;
                Success = false;
            }            

            return Success;
        }

        
        
	}
}
