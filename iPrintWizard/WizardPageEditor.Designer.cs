using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.IO;
namespace iPrint
{
	partial class WizardPageEditor
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelMainPic = new System.Windows.Forms.Panel();
            this.btnCrop = new System.Windows.Forms.Button();
            this.PicBoxEdit = new System.Windows.Forms.PictureBox();
            this.panelEdit = new System.Windows.Forms.Panel();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Label7 = new System.Windows.Forms.Label();
            this.lbloriginalSize = new System.Windows.Forms.Label();
            this.lblModifiedSize = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.Label3 = new System.Windows.Forms.Label();
            this.DomainUpDown1 = new System.Windows.Forms.DomainUpDown();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Label5 = new System.Windows.Forms.Label();
            this.DomainUpDownBrightness = new System.Windows.Forms.DomainUpDown();
            this.TrackBarBrightness = new System.Windows.Forms.TrackBar();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.btnRotateRight = new System.Windows.Forms.Button();
            this.btnRotateHorizantal = new System.Windows.Forms.Button();
            this.btnRotatevertical = new System.Windows.Forms.Button();
            this.btnRotateLeft = new System.Windows.Forms.Button();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tBarFrameWidth = new System.Windows.Forms.TrackBar();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.butFrameColor = new System.Windows.Forms.Button();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.btnGrayScale = new System.Windows.Forms.Button();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.btnApplyPP = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnPPSize = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radBtnX12 = new System.Windows.Forms.RadioButton();
            this.radBtnX8 = new System.Windows.Forms.RadioButton();
            this.radBtnX4 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butUndo = new System.Windows.Forms.Button();
            this.panelFilmStripMain = new System.Windows.Forms.Panel();
            this.panelFilmStrip = new System.Windows.Forms.Panel();
            this.flpThumbnails = new System.Windows.Forms.FlowLayoutPanel();
            this.butScrollL = new System.Windows.Forms.Button();
            this.butScrollR = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panelMain.SuspendLayout();
            this.panelMainPic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxEdit)).BeginInit();
            this.panelEdit.SuspendLayout();
            this.TabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarBrightness)).BeginInit();
            this.TabPage4.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tBarFrameWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabPage7.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelFilmStripMain.SuspendLayout();
            this.panelFilmStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelMainPic);
            this.panelMain.Controls.Add(this.panelEdit);
            this.panelMain.Controls.Add(this.panelFilmStripMain);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1871, 871);
            this.panelMain.TabIndex = 11;
            // 
            // panelMainPic
            // 
            this.panelMainPic.Controls.Add(this.btnCrop);
            this.panelMainPic.Controls.Add(this.PicBoxEdit);
            this.panelMainPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainPic.Location = new System.Drawing.Point(0, 0);
            this.panelMainPic.Name = "panelMainPic";
            this.panelMainPic.Size = new System.Drawing.Size(1228, 699);
            this.panelMainPic.TabIndex = 13;
            // 
            // btnCrop
            // 
            this.btnCrop.Location = new System.Drawing.Point(1135, 12);
            this.btnCrop.Name = "btnCrop";
            this.btnCrop.Size = new System.Drawing.Size(87, 79);
            this.btnCrop.TabIndex = 4;
            this.btnCrop.Tag = "8";
            this.btnCrop.Text = "Crop";
            this.btnCrop.UseVisualStyleBackColor = true;
            this.btnCrop.Click += new System.EventHandler(this.btnCrop_Click);
            // 
            // PicBoxEdit
            // 
            this.PicBoxEdit.Location = new System.Drawing.Point(2, 3);
            this.PicBoxEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PicBoxEdit.Name = "PicBoxEdit";
            this.PicBoxEdit.Size = new System.Drawing.Size(1228, 696);
            this.PicBoxEdit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicBoxEdit.TabIndex = 2;
            this.PicBoxEdit.TabStop = false;
            this.PicBoxEdit.VisibleChanged += new System.EventHandler(this.PictureBox1_VisibleChanged);
            this.PicBoxEdit.Paint += new System.Windows.Forms.PaintEventHandler(this.PicBoxEdit_Paint);
            this.PicBoxEdit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseDown);
            this.PicBoxEdit.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseMove);
            // 
            // panelEdit
            // 
            this.panelEdit.Controls.Add(this.TabControl1);
            this.panelEdit.Controls.Add(this.panel1);
            this.panelEdit.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelEdit.Location = new System.Drawing.Point(1228, 0);
            this.panelEdit.Name = "panelEdit";
            this.panelEdit.Size = new System.Drawing.Size(643, 699);
            this.panelEdit.TabIndex = 12;
            // 
            // TabControl1
            // 
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Controls.Add(this.TabPage4);
            this.TabControl1.Controls.Add(this.tabPage6);
            this.TabControl1.Controls.Add(this.tabPage7);
            this.TabControl1.Controls.Add(this.tabPage8);
            this.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabControl1.Location = new System.Drawing.Point(0, 0);
            this.TabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(643, 610);
            this.TabControl1.TabIndex = 4;
            this.TabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.Label7);
            this.TabPage1.Controls.Add(this.lbloriginalSize);
            this.TabPage1.Controls.Add(this.lblModifiedSize);
            this.TabPage1.Controls.Add(this.Label4);
            this.TabPage1.Controls.Add(this.btnOk);
            this.TabPage1.Controls.Add(this.Label3);
            this.TabPage1.Controls.Add(this.DomainUpDown1);
            this.TabPage1.Controls.Add(this.Label2);
            this.TabPage1.Controls.Add(this.Label1);
            this.TabPage1.Location = new System.Drawing.Point(4, 34);
            this.TabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabPage1.Size = new System.Drawing.Size(635, 572);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Resize";
            this.TabPage1.UseVisualStyleBackColor = true;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(16, 276);
            this.Label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(153, 29);
            this.Label7.TabIndex = 8;
            this.Label7.Text = "Original size:";
            // 
            // lbloriginalSize
            // 
            this.lbloriginalSize.AutoSize = true;
            this.lbloriginalSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbloriginalSize.Location = new System.Drawing.Point(167, 276);
            this.lbloriginalSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbloriginalSize.Name = "lbloriginalSize";
            this.lbloriginalSize.Size = new System.Drawing.Size(39, 29);
            this.lbloriginalSize.TabIndex = 7;
            this.lbloriginalSize.Text = "00";
            // 
            // lblModifiedSize
            // 
            this.lblModifiedSize.AutoSize = true;
            this.lblModifiedSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModifiedSize.Location = new System.Drawing.Point(167, 320);
            this.lblModifiedSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblModifiedSize.Name = "lblModifiedSize";
            this.lblModifiedSize.Size = new System.Drawing.Size(39, 29);
            this.lblModifiedSize.TabIndex = 6;
            this.lblModifiedSize.Text = "00";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.Location = new System.Drawing.Point(8, 316);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(162, 29);
            this.Label4.TabIndex = 5;
            this.Label4.Text = "Modified size:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(170, 92);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(140, 40);
            this.btnOk.TabIndex = 4;
            this.btnOk.Tag = "9";
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(138, 105);
            this.Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(30, 25);
            this.Label3.TabIndex = 3;
            this.Label3.Text = "%";
            // 
            // DomainUpDown1
            // 
            this.DomainUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DomainUpDown1.Location = new System.Drawing.Point(16, 97);
            this.DomainUpDown1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DomainUpDown1.Name = "DomainUpDown1";
            this.DomainUpDown1.Size = new System.Drawing.Size(112, 35);
            this.DomainUpDown1.TabIndex = 2;
            this.DomainUpDown1.Text = "100";
            this.DomainUpDown1.SelectedItemChanged += new System.EventHandler(this.DomainUpDown1_SelectedItemChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(13, 231);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(241, 29);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Size setting summery";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(12, 43);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(298, 20);
            this.Label1.TabIndex = 0;
            this.Label1.Tag = "";
            this.Label1.Text = "Percantage of original width and height";
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.Label5);
            this.TabPage2.Controls.Add(this.DomainUpDownBrightness);
            this.TabPage2.Controls.Add(this.TrackBarBrightness);
            this.TabPage2.Location = new System.Drawing.Point(4, 34);
            this.TabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabPage2.Size = new System.Drawing.Size(635, 572);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Brightness";
            this.TabPage2.UseVisualStyleBackColor = true;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(22, 34);
            this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(95, 22);
            this.Label5.TabIndex = 2;
            this.Label5.Text = "Brightness";
            // 
            // DomainUpDownBrightness
            // 
            this.DomainUpDownBrightness.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DomainUpDownBrightness.Location = new System.Drawing.Point(132, 34);
            this.DomainUpDownBrightness.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DomainUpDownBrightness.Name = "DomainUpDownBrightness";
            this.DomainUpDownBrightness.ReadOnly = true;
            this.DomainUpDownBrightness.Size = new System.Drawing.Size(133, 35);
            this.DomainUpDownBrightness.TabIndex = 1;
            this.DomainUpDownBrightness.Text = "0";
            // 
            // TrackBarBrightness
            // 
            this.TrackBarBrightness.BackColor = System.Drawing.Color.White;
            this.TrackBarBrightness.Location = new System.Drawing.Point(8, 85);
            this.TrackBarBrightness.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TrackBarBrightness.Maximum = 100;
            this.TrackBarBrightness.Minimum = -100;
            this.TrackBarBrightness.Name = "TrackBarBrightness";
            this.TrackBarBrightness.Size = new System.Drawing.Size(619, 69);
            this.TrackBarBrightness.TabIndex = 0;
            this.TrackBarBrightness.Tag = "10";
            this.TrackBarBrightness.Scroll += new System.EventHandler(this.TrackBarBrightness_Scroll);
            // 
            // TabPage4
            // 
            this.TabPage4.Controls.Add(this.btnRotateRight);
            this.TabPage4.Controls.Add(this.btnRotateHorizantal);
            this.TabPage4.Controls.Add(this.btnRotatevertical);
            this.TabPage4.Controls.Add(this.btnRotateLeft);
            this.TabPage4.Location = new System.Drawing.Point(4, 34);
            this.TabPage4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(635, 572);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Rotate";
            this.TabPage4.UseVisualStyleBackColor = true;
            // 
            // btnRotateRight
            // 
            this.btnRotateRight.Location = new System.Drawing.Point(335, 53);
            this.btnRotateRight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRotateRight.Name = "btnRotateRight";
            this.btnRotateRight.Size = new System.Drawing.Size(183, 127);
            this.btnRotateRight.TabIndex = 3;
            this.btnRotateRight.Tag = "11";
            this.btnRotateRight.Text = "Rotate right";
            this.btnRotateRight.UseVisualStyleBackColor = true;
            this.btnRotateRight.Click += new System.EventHandler(this.btnRotateRight_Click);
            // 
            // btnRotateHorizantal
            // 
            this.btnRotateHorizantal.Location = new System.Drawing.Point(53, 265);
            this.btnRotateHorizantal.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRotateHorizantal.Name = "btnRotateHorizantal";
            this.btnRotateHorizantal.Size = new System.Drawing.Size(183, 127);
            this.btnRotateHorizantal.TabIndex = 2;
            this.btnRotateHorizantal.Tag = "12";
            this.btnRotateHorizantal.Text = "Rotate horizantal";
            this.btnRotateHorizantal.UseVisualStyleBackColor = true;
            this.btnRotateHorizantal.Click += new System.EventHandler(this.btnRotateHorizantal_Click);
            // 
            // btnRotatevertical
            // 
            this.btnRotatevertical.Location = new System.Drawing.Point(335, 265);
            this.btnRotatevertical.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRotatevertical.Name = "btnRotatevertical";
            this.btnRotatevertical.Size = new System.Drawing.Size(183, 127);
            this.btnRotatevertical.TabIndex = 1;
            this.btnRotatevertical.Tag = "13";
            this.btnRotatevertical.Text = "Rotate vertical";
            this.btnRotatevertical.UseVisualStyleBackColor = true;
            this.btnRotatevertical.Click += new System.EventHandler(this.btnRotatevertical_Click);
            // 
            // btnRotateLeft
            // 
            this.btnRotateLeft.Location = new System.Drawing.Point(53, 53);
            this.btnRotateLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRotateLeft.Name = "btnRotateLeft";
            this.btnRotateLeft.Size = new System.Drawing.Size(183, 127);
            this.btnRotateLeft.TabIndex = 0;
            this.btnRotateLeft.Tag = "10";
            this.btnRotateLeft.Text = "Rotate left";
            this.btnRotateLeft.UseVisualStyleBackColor = true;
            this.btnRotateLeft.Click += new System.EventHandler(this.btnRotateLeft_Click);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.tBarFrameWidth);
            this.tabPage6.Controls.Add(this.numericUpDown1);
            this.tabPage6.Controls.Add(this.label8);
            this.tabPage6.Controls.Add(this.label6);
            this.tabPage6.Controls.Add(this.butFrameColor);
            this.tabPage6.Location = new System.Drawing.Point(4, 34);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(635, 572);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Frame";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tBarFrameWidth
            // 
            this.tBarFrameWidth.BackColor = System.Drawing.Color.White;
            this.tBarFrameWidth.Location = new System.Drawing.Point(17, 267);
            this.tBarFrameWidth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tBarFrameWidth.Maximum = 80;
            this.tBarFrameWidth.Name = "tBarFrameWidth";
            this.tBarFrameWidth.Size = new System.Drawing.Size(611, 69);
            this.tBarFrameWidth.TabIndex = 7;
            this.tBarFrameWidth.Tag = "15";
            this.tBarFrameWidth.Scroll += new System.EventHandler(this.tBarFrameWidth_Scroll);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.Location = new System.Drawing.Point(17, 338);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 35);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(17, 237);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(135, 25);
            this.label8.TabIndex = 4;
            this.label8.Text = "Frame Width";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(17, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(212, 25);
            this.label6.TabIndex = 3;
            this.label6.Text = "Choose Frame Color";
            // 
            // butFrameColor
            // 
            this.butFrameColor.BackColor = System.Drawing.Color.Black;
            this.butFrameColor.ForeColor = System.Drawing.SystemColors.Window;
            this.butFrameColor.Location = new System.Drawing.Point(17, 60);
            this.butFrameColor.Name = "butFrameColor";
            this.butFrameColor.Size = new System.Drawing.Size(213, 104);
            this.butFrameColor.TabIndex = 2;
            this.butFrameColor.Tag = "14";
            this.butFrameColor.Text = "Color";
            this.butFrameColor.UseVisualStyleBackColor = false;
            this.butFrameColor.Click += new System.EventHandler(this.butFrameColor_Click);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.btnGrayScale);
            this.tabPage7.Location = new System.Drawing.Point(4, 34);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(635, 572);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Gray Scale";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // btnGrayScale
            // 
            this.btnGrayScale.Location = new System.Drawing.Point(30, 50);
            this.btnGrayScale.Name = "btnGrayScale";
            this.btnGrayScale.Size = new System.Drawing.Size(261, 168);
            this.btnGrayScale.TabIndex = 0;
            this.btnGrayScale.Tag = "16";
            this.btnGrayScale.Text = "Convert To Black and White";
            this.btnGrayScale.UseVisualStyleBackColor = true;
            this.btnGrayScale.Click += new System.EventHandler(this.btnGrayScale_Click);
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.btnApplyPP);
            this.tabPage8.Controls.Add(this.label22);
            this.tabPage8.Controls.Add(this.label23);
            this.tabPage8.Controls.Add(this.label20);
            this.tabPage8.Controls.Add(this.label21);
            this.tabPage8.Controls.Add(this.label18);
            this.tabPage8.Controls.Add(this.label19);
            this.tabPage8.Controls.Add(this.label16);
            this.tabPage8.Controls.Add(this.label17);
            this.tabPage8.Controls.Add(this.label15);
            this.tabPage8.Controls.Add(this.label14);
            this.tabPage8.Controls.Add(this.btnPPSize);
            this.tabPage8.Controls.Add(this.groupBox1);
            this.tabPage8.Location = new System.Drawing.Point(4, 34);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(635, 572);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "Passport Picture";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // btnApplyPP
            // 
            this.btnApplyPP.Location = new System.Drawing.Point(393, 395);
            this.btnApplyPP.Name = "btnApplyPP";
            this.btnApplyPP.Size = new System.Drawing.Size(157, 90);
            this.btnApplyPP.TabIndex = 12;
            this.btnApplyPP.Tag = "21";
            this.btnApplyPP.Text = "Apply";
            this.btnApplyPP.UseVisualStyleBackColor = true;
            this.btnApplyPP.Click += new System.EventHandler(this.btnApplyPP_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(13, 246);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(404, 25);
            this.label22.TabIndex = 10;
            this.label22.Text = "Click the \"Crop\" button to apply your changes";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label23.Location = new System.Drawing.Point(13, 222);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(70, 25);
            this.label23.TabIndex = 9;
            this.label23.Text = "Step3:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(13, 427);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(124, 25);
            this.label20.TabIndex = 8;
            this.label20.Text = "Click \"Apply\"";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label21.Location = new System.Drawing.Point(13, 402);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(70, 25);
            this.label21.TabIndex = 7;
            this.label21.Text = "Step5:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(13, 303);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(329, 25);
            this.label18.TabIndex = 6;
            this.label18.Text = "Select the number of copies required";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label19.Location = new System.Drawing.Point(13, 279);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(70, 25);
            this.label19.TabIndex = 5;
            this.label19.Text = "Step4:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 132);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(398, 50);
            this.label16.TabIndex = 4;
            this.label16.Text = "Move the required section into the focus box.\r\nIf necessary, resize the picture";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label17.Location = new System.Drawing.Point(13, 106);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(70, 25);
            this.label17.TabIndex = 3;
            this.label17.Text = "Step2:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 48);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(286, 25);
            this.label15.TabIndex = 2;
            this.label15.Text = "Click the button \"Passport Size\"";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label14.Location = new System.Drawing.Point(13, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 25);
            this.label14.TabIndex = 1;
            this.label14.Text = "Step1:";
            // 
            // btnPPSize
            // 
            this.btnPPSize.Location = new System.Drawing.Point(393, 15);
            this.btnPPSize.Name = "btnPPSize";
            this.btnPPSize.Size = new System.Drawing.Size(157, 90);
            this.btnPPSize.TabIndex = 0;
            this.btnPPSize.Tag = "17";
            this.btnPPSize.Text = "Passport Size";
            this.btnPPSize.UseVisualStyleBackColor = true;
            this.btnPPSize.Click += new System.EventHandler(this.btnPPSize_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radBtnX12);
            this.groupBox1.Controls.Add(this.radBtnX8);
            this.groupBox1.Controls.Add(this.radBtnX4);
            this.groupBox1.Location = new System.Drawing.Point(20, 322);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(599, 67);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // radBtnX12
            // 
            this.radBtnX12.AutoSize = true;
            this.radBtnX12.Location = new System.Drawing.Point(488, 23);
            this.radBtnX12.Name = "radBtnX12";
            this.radBtnX12.Size = new System.Drawing.Size(73, 29);
            this.radBtnX12.TabIndex = 2;
            this.radBtnX12.TabStop = true;
            this.radBtnX12.Tag = "20";
            this.radBtnX12.Text = "X12";
            this.radBtnX12.UseVisualStyleBackColor = true;
            this.radBtnX12.CheckedChanged += new System.EventHandler(this.radBtnX12_CheckedChanged);
            // 
            // radBtnX8
            // 
            this.radBtnX8.AutoSize = true;
            this.radBtnX8.Location = new System.Drawing.Point(247, 23);
            this.radBtnX8.Name = "radBtnX8";
            this.radBtnX8.Size = new System.Drawing.Size(62, 29);
            this.radBtnX8.TabIndex = 1;
            this.radBtnX8.TabStop = true;
            this.radBtnX8.Tag = "19";
            this.radBtnX8.Text = "X8";
            this.radBtnX8.UseVisualStyleBackColor = true;
            this.radBtnX8.CheckedChanged += new System.EventHandler(this.radBtnX8_CheckedChanged);
            // 
            // radBtnX4
            // 
            this.radBtnX4.AutoSize = true;
            this.radBtnX4.Location = new System.Drawing.Point(17, 23);
            this.radBtnX4.Name = "radBtnX4";
            this.radBtnX4.Size = new System.Drawing.Size(62, 29);
            this.radBtnX4.TabIndex = 0;
            this.radBtnX4.TabStop = true;
            this.radBtnX4.Tag = "18";
            this.radBtnX4.Text = "X4";
            this.radBtnX4.UseVisualStyleBackColor = true;
            this.radBtnX4.CheckedChanged += new System.EventHandler(this.radBtnX4_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.butUndo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 610);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(643, 89);
            this.panel1.TabIndex = 2;
            // 
            // butUndo
            // 
            this.butUndo.Location = new System.Drawing.Point(20, 25);
            this.butUndo.Name = "butUndo";
            this.butUndo.Size = new System.Drawing.Size(159, 52);
            this.butUndo.TabIndex = 8;
            this.butUndo.Tag = "22";
            this.butUndo.Text = "&Undo";
            this.butUndo.UseVisualStyleBackColor = true;
            this.butUndo.Click += new System.EventHandler(this.butUndo_Click);
            // 
            // panelFilmStripMain
            // 
            this.panelFilmStripMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelFilmStripMain.Controls.Add(this.panelFilmStrip);
            this.panelFilmStripMain.Controls.Add(this.butScrollL);
            this.panelFilmStripMain.Controls.Add(this.butScrollR);
            this.panelFilmStripMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFilmStripMain.Location = new System.Drawing.Point(0, 699);
            this.panelFilmStripMain.Name = "panelFilmStripMain";
            this.panelFilmStripMain.Size = new System.Drawing.Size(1871, 172);
            this.panelFilmStripMain.TabIndex = 2;
            // 
            // panelFilmStrip
            // 
            this.panelFilmStrip.Controls.Add(this.flpThumbnails);
            this.panelFilmStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFilmStrip.Location = new System.Drawing.Point(75, 0);
            this.panelFilmStrip.Name = "panelFilmStrip";
            this.panelFilmStrip.Size = new System.Drawing.Size(1717, 168);
            this.panelFilmStrip.TabIndex = 8;
            // 
            // flpThumbnails
            // 
            this.flpThumbnails.BackColor = System.Drawing.Color.GhostWhite;
            this.flpThumbnails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.flpThumbnails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpThumbnails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpThumbnails.Location = new System.Drawing.Point(0, 0);
            this.flpThumbnails.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flpThumbnails.Name = "flpThumbnails";
            this.flpThumbnails.Size = new System.Drawing.Size(1717, 168);
            this.flpThumbnails.TabIndex = 6;
            this.flpThumbnails.WrapContents = false;
            // 
            // butScrollL
            // 
            this.butScrollL.Dock = System.Windows.Forms.DockStyle.Left;
            this.butScrollL.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butScrollL.Location = new System.Drawing.Point(0, 0);
            this.butScrollL.Name = "butScrollL";
            this.butScrollL.Size = new System.Drawing.Size(75, 168);
            this.butScrollL.TabIndex = 7;
            this.butScrollL.Text = "<";
            this.butScrollL.UseVisualStyleBackColor = true;
            this.butScrollL.Click += new System.EventHandler(this.butScrollL_Click);
            // 
            // butScrollR
            // 
            this.butScrollR.Dock = System.Windows.Forms.DockStyle.Right;
            this.butScrollR.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butScrollR.Location = new System.Drawing.Point(1792, 0);
            this.butScrollR.Name = "butScrollR";
            this.butScrollR.Size = new System.Drawing.Size(75, 168);
            this.butScrollR.TabIndex = 6;
            this.butScrollR.Text = ">";
            this.butScrollR.UseVisualStyleBackColor = true;
            this.butScrollR.Click += new System.EventHandler(this.butScrollR_Click);
            // 
            // WizardPageEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.Controls.Add(this.panelMain);
            this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Name = "WizardPageEditor";
            this.Size = new System.Drawing.Size(1871, 871);
            this.Subtitle = "Use the editor buttons on the right to do basic editing of your pictures";
            this.Title = "Step 3 - Picture editor";
            this.panelMain.ResumeLayout(false);
            this.panelMainPic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicBoxEdit)).EndInit();
            this.panelEdit.ResumeLayout(false);
            this.TabControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarBrightness)).EndInit();
            this.TabPage4.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tBarFrameWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabPage7.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panelFilmStripMain.ResumeLayout(false);
            this.panelFilmStrip.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelFilmStripMain;
        private System.Windows.Forms.Panel panelFilmStrip;
        private System.Windows.Forms.FlowLayoutPanel flpThumbnails;
        private System.Windows.Forms.Button butScrollL;
        private System.Windows.Forms.Button butScrollR;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private Color BorderColor = Color.Black;
        Bitmap EditedBitmap;
        int BorderWidth = 0;
        PictureBox SelectedPictureBox = null;
        private Panel panelMainPic;
        internal PictureBox PicBoxEdit;
        private Panel panelEdit;
        internal TabControl TabControl1;
        internal TabPage TabPage1;
        internal Label Label7;
        internal Label lbloriginalSize;
        internal Label lblModifiedSize;
        internal Label Label4;
        internal Button btnOk;
        internal Label Label3;
        internal DomainUpDown DomainUpDown1;
        internal Label Label2;
        internal Label Label1;
        internal TabPage TabPage2;
        internal Label Label5;
        internal DomainUpDown DomainUpDownBrightness;
        internal TrackBar TrackBarBrightness;
        internal TabPage TabPage4;
        internal Button btnRotateRight;
        internal Button btnRotateHorizantal;
        internal Button btnRotatevertical;
        internal Button btnRotateLeft;
        private TabPage tabPage6;
        internal TrackBar tBarFrameWidth;
        private NumericUpDown numericUpDown1;
        private Label label8;
        private Label label6;
        private Button butFrameColor;
        private TabPage tabPage7;
        private Button btnGrayScale;
        private Panel panel1;
        private Button butUndo;
        private TabPage tabPage8;
        private Button btnCrop;
        private Button btnPPSize;
        private Label label16;
        private Label label17;
        private Label label15;
        private Label label14;
        private Label label22;
        private Label label23;
        private Label label20;
        private Label label21;
        private Label label18;
        private Label label19;
        private GroupBox groupBox1;
        private RadioButton radBtnX12;
        private RadioButton radBtnX8;
        private RadioButton radBtnX4;
        private Button btnApplyPP;

    }
}
