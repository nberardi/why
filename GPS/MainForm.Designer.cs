namespace GPS
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Has Fix", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Does Not Have Fix", System.Windows.Forms.HorizontalAlignment.Left);
			this.connectButton = new System.Windows.Forms.Button();
			this.disconnectButton = new System.Windows.Forms.Button();
			this.mainTabControl = new System.Windows.Forms.TabControl();
			this.satellitesTabPage = new System.Windows.Forms.TabPage();
			this.satelliteList = new System.Windows.Forms.ListView();
			this.prnColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.signalQualityColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.satilliteLocationImage = new System.Windows.Forms.PictureBox();
			this.refreshSatelliteTimer = new System.Windows.Forms.Timer(this.components);
			this.portList = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.mainTabControl.SuspendLayout();
			this.satellitesTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.satilliteLocationImage)).BeginInit();
			this.SuspendLayout();
			// 
			// connectButton
			// 
			this.connectButton.Location = new System.Drawing.Point(12, 539);
			this.connectButton.Name = "connectButton";
			this.connectButton.Size = new System.Drawing.Size(75, 23);
			this.connectButton.TabIndex = 0;
			this.connectButton.Text = "Connect";
			this.connectButton.UseVisualStyleBackColor = true;
			this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
			// 
			// disconnectButton
			// 
			this.disconnectButton.Enabled = false;
			this.disconnectButton.Location = new System.Drawing.Point(93, 539);
			this.disconnectButton.Name = "disconnectButton";
			this.disconnectButton.Size = new System.Drawing.Size(75, 23);
			this.disconnectButton.TabIndex = 1;
			this.disconnectButton.Text = "Disconnect";
			this.disconnectButton.UseVisualStyleBackColor = true;
			this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
			// 
			// mainTabControl
			// 
			this.mainTabControl.Controls.Add(this.satellitesTabPage);
			this.mainTabControl.Location = new System.Drawing.Point(12, 12);
			this.mainTabControl.Name = "mainTabControl";
			this.mainTabControl.SelectedIndex = 0;
			this.mainTabControl.Size = new System.Drawing.Size(770, 521);
			this.mainTabControl.TabIndex = 2;
			// 
			// satellitesTabPage
			// 
			this.satellitesTabPage.Controls.Add(this.satelliteList);
			this.satellitesTabPage.Controls.Add(this.satilliteLocationImage);
			this.satellitesTabPage.Location = new System.Drawing.Point(4, 22);
			this.satellitesTabPage.Name = "satellitesTabPage";
			this.satellitesTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.satellitesTabPage.Size = new System.Drawing.Size(762, 495);
			this.satellitesTabPage.TabIndex = 0;
			this.satellitesTabPage.Text = "Satellites";
			this.satellitesTabPage.UseVisualStyleBackColor = true;
			// 
			// satelliteList
			// 
			this.satelliteList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.prnColumnHeader,
            this.signalQualityColumnHeader});
			this.satelliteList.FullRowSelect = true;
			listViewGroup3.Header = "Has Fix";
			listViewGroup3.Name = "hasFixGroup";
			listViewGroup4.Header = "Does Not Have Fix";
			listViewGroup4.Name = "doesNotHaveFixGroup";
			this.satelliteList.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3,
            listViewGroup4});
			this.satelliteList.Location = new System.Drawing.Point(542, 3);
			this.satelliteList.Name = "satelliteList";
			this.satelliteList.Size = new System.Drawing.Size(217, 489);
			this.satelliteList.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.satelliteList.TabIndex = 1;
			this.satelliteList.UseCompatibleStateImageBehavior = false;
			this.satelliteList.View = System.Windows.Forms.View.Details;
			// 
			// prnColumnHeader
			// 
			this.prnColumnHeader.Text = "PRN";
			this.prnColumnHeader.Width = 100;
			// 
			// signalQualityColumnHeader
			// 
			this.signalQualityColumnHeader.Text = "Quality";
			this.signalQualityColumnHeader.Width = 62;
			// 
			// satilliteLocationImage
			// 
			this.satilliteLocationImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.satilliteLocationImage.BackColor = System.Drawing.Color.PaleTurquoise;
			this.satilliteLocationImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.satilliteLocationImage.Location = new System.Drawing.Point(3, 3);
			this.satilliteLocationImage.Name = "satilliteLocationImage";
			this.satilliteLocationImage.Size = new System.Drawing.Size(533, 489);
			this.satilliteLocationImage.TabIndex = 0;
			this.satilliteLocationImage.TabStop = false;
			this.satilliteLocationImage.Paint += new System.Windows.Forms.PaintEventHandler(this.satilliteLocationImage_Paint);
			// 
			// refreshSatelliteTimer
			// 
			this.refreshSatelliteTimer.Enabled = true;
			this.refreshSatelliteTimer.Interval = 500;
			this.refreshSatelliteTimer.Tick += new System.EventHandler(this.refreshSatelliteTimer_Tick);
			// 
			// portList
			// 
			this.portList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.portList.FormattingEnabled = true;
			this.portList.Location = new System.Drawing.Point(235, 541);
			this.portList.Name = "portList";
			this.portList.Size = new System.Drawing.Size(121, 21);
			this.portList.TabIndex = 3;
			this.portList.SelectedIndexChanged += new System.EventHandler(this.portList_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(200, 544);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Port:";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(794, 574);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.portList);
			this.Controls.Add(this.mainTabControl);
			this.Controls.Add(this.disconnectButton);
			this.Controls.Add(this.connectButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.mainTabControl.ResumeLayout(false);
			this.satellitesTabPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.satilliteLocationImage)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button connectButton;
		private System.Windows.Forms.Button disconnectButton;
		private System.Windows.Forms.TabControl mainTabControl;
		private System.Windows.Forms.TabPage satellitesTabPage;
		private System.Windows.Forms.PictureBox satilliteLocationImage;
		private System.Windows.Forms.Timer refreshSatelliteTimer;
		private System.Windows.Forms.ListView satelliteList;
		private System.Windows.Forms.ColumnHeader prnColumnHeader;
		private System.Windows.Forms.ColumnHeader signalQualityColumnHeader;
		private System.Windows.Forms.ComboBox portList;
		private System.Windows.Forms.Label label1;

	}
}