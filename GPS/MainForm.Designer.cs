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
			System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Has Fix", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Does Not Have Fix", System.Windows.Forms.HorizontalAlignment.Left);
			this.connectButton = new System.Windows.Forms.Button();
			this.disconnectButton = new System.Windows.Forms.Button();
			this.mainTabControl = new System.Windows.Forms.TabControl();
			this.satellitesTabPage = new System.Windows.Forms.TabPage();
			this.satelliteList = new System.Windows.Forms.ListView();
			this.prnColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.signalQualityColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.satilliteLocationImage = new System.Windows.Forms.PictureBox();
			this.informationTabPage = new System.Windows.Forms.TabPage();
			this.directionGroup = new System.Windows.Forms.GroupBox();
			this.directionImage = new System.Windows.Forms.PictureBox();
			this.speedGroup = new System.Windows.Forms.GroupBox();
			this.speedLabel = new System.Windows.Forms.Label();
			this.knotsRadio = new System.Windows.Forms.RadioButton();
			this.milesPerHourRadio = new System.Windows.Forms.RadioButton();
			this.kilometersPerHourRadio = new System.Windows.Forms.RadioButton();
			this.portList = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.bearingLabel = new System.Windows.Forms.Label();
			this.bearingValueLabel = new System.Windows.Forms.Label();
			this.mainTabControl.SuspendLayout();
			this.satellitesTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.satilliteLocationImage)).BeginInit();
			this.informationTabPage.SuspendLayout();
			this.directionGroup.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.directionImage)).BeginInit();
			this.speedGroup.SuspendLayout();
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
			this.mainTabControl.Controls.Add(this.informationTabPage);
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
			listViewGroup1.Header = "Has Fix";
			listViewGroup1.Name = "hasFixGroup";
			listViewGroup2.Header = "Does Not Have Fix";
			listViewGroup2.Name = "doesNotHaveFixGroup";
			this.satelliteList.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
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
			// informationTabPage
			// 
			this.informationTabPage.Controls.Add(this.directionGroup);
			this.informationTabPage.Controls.Add(this.speedGroup);
			this.informationTabPage.Location = new System.Drawing.Point(4, 22);
			this.informationTabPage.Name = "informationTabPage";
			this.informationTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.informationTabPage.Size = new System.Drawing.Size(762, 495);
			this.informationTabPage.TabIndex = 1;
			this.informationTabPage.Text = "Information";
			this.informationTabPage.UseVisualStyleBackColor = true;
			// 
			// directionGroup
			// 
			this.directionGroup.Controls.Add(this.bearingValueLabel);
			this.directionGroup.Controls.Add(this.bearingLabel);
			this.directionGroup.Controls.Add(this.directionImage);
			this.directionGroup.Location = new System.Drawing.Point(6, 109);
			this.directionGroup.Name = "directionGroup";
			this.directionGroup.Size = new System.Drawing.Size(334, 380);
			this.directionGroup.TabIndex = 5;
			this.directionGroup.TabStop = false;
			this.directionGroup.Text = "Direction";
			// 
			// directionImage
			// 
			this.directionImage.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.directionImage.Location = new System.Drawing.Point(3, 47);
			this.directionImage.Name = "directionImage";
			this.directionImage.Size = new System.Drawing.Size(328, 330);
			this.directionImage.TabIndex = 0;
			this.directionImage.TabStop = false;
			this.directionImage.Paint += new System.Windows.Forms.PaintEventHandler(this.directionImage_Paint);
			// 
			// speedGroup
			// 
			this.speedGroup.Controls.Add(this.speedLabel);
			this.speedGroup.Controls.Add(this.knotsRadio);
			this.speedGroup.Controls.Add(this.milesPerHourRadio);
			this.speedGroup.Controls.Add(this.kilometersPerHourRadio);
			this.speedGroup.Location = new System.Drawing.Point(6, 6);
			this.speedGroup.Name = "speedGroup";
			this.speedGroup.Size = new System.Drawing.Size(334, 97);
			this.speedGroup.TabIndex = 4;
			this.speedGroup.TabStop = false;
			this.speedGroup.Text = "Speed";
			// 
			// speedLabel
			// 
			this.speedLabel.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.speedLabel.Location = new System.Drawing.Point(14, 28);
			this.speedLabel.Name = "speedLabel";
			this.speedLabel.Size = new System.Drawing.Size(176, 45);
			this.speedLabel.TabIndex = 0;
			this.speedLabel.Text = "0000.00";
			this.speedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// knotsRadio
			// 
			this.knotsRadio.AutoSize = true;
			this.knotsRadio.Location = new System.Drawing.Point(196, 65);
			this.knotsRadio.Name = "knotsRadio";
			this.knotsRadio.Size = new System.Drawing.Size(52, 17);
			this.knotsRadio.TabIndex = 3;
			this.knotsRadio.Text = "Knots";
			this.knotsRadio.UseVisualStyleBackColor = true;
			this.knotsRadio.CheckedChanged += new System.EventHandler(this.distance_CheckedChanged);
			// 
			// milesPerHourRadio
			// 
			this.milesPerHourRadio.AutoSize = true;
			this.milesPerHourRadio.Checked = true;
			this.milesPerHourRadio.Location = new System.Drawing.Point(196, 19);
			this.milesPerHourRadio.Name = "milesPerHourRadio";
			this.milesPerHourRadio.Size = new System.Drawing.Size(94, 17);
			this.milesPerHourRadio.TabIndex = 1;
			this.milesPerHourRadio.TabStop = true;
			this.milesPerHourRadio.Text = "Miles Per Hour";
			this.milesPerHourRadio.UseVisualStyleBackColor = true;
			this.milesPerHourRadio.CheckedChanged += new System.EventHandler(this.distance_CheckedChanged);
			// 
			// kilometersPerHourRadio
			// 
			this.kilometersPerHourRadio.AutoSize = true;
			this.kilometersPerHourRadio.Location = new System.Drawing.Point(196, 42);
			this.kilometersPerHourRadio.Name = "kilometersPerHourRadio";
			this.kilometersPerHourRadio.Size = new System.Drawing.Size(118, 17);
			this.kilometersPerHourRadio.TabIndex = 2;
			this.kilometersPerHourRadio.Text = "Kilometers Per Hour";
			this.kilometersPerHourRadio.UseVisualStyleBackColor = true;
			this.kilometersPerHourRadio.CheckedChanged += new System.EventHandler(this.distance_CheckedChanged);
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
			// bearingLabel
			// 
			this.bearingLabel.AutoSize = true;
			this.bearingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bearingLabel.Location = new System.Drawing.Point(6, 16);
			this.bearingLabel.Name = "bearingLabel";
			this.bearingLabel.Size = new System.Drawing.Size(61, 17);
			this.bearingLabel.TabIndex = 1;
			this.bearingLabel.Text = "Bearing:";
			// 
			// bearingValueLabel
			// 
			this.bearingValueLabel.AutoSize = true;
			this.bearingValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bearingValueLabel.Location = new System.Drawing.Point(73, 16);
			this.bearingValueLabel.Name = "bearingValueLabel";
			this.bearingValueLabel.Size = new System.Drawing.Size(44, 17);
			this.bearingValueLabel.TabIndex = 2;
			this.bearingValueLabel.Text = "0.000";
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
			this.informationTabPage.ResumeLayout(false);
			this.directionGroup.ResumeLayout(false);
			this.directionGroup.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.directionImage)).EndInit();
			this.speedGroup.ResumeLayout(false);
			this.speedGroup.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button connectButton;
		private System.Windows.Forms.Button disconnectButton;
		private System.Windows.Forms.TabControl mainTabControl;
		private System.Windows.Forms.TabPage satellitesTabPage;
		private System.Windows.Forms.PictureBox satilliteLocationImage;
		private System.Windows.Forms.ListView satelliteList;
		private System.Windows.Forms.ColumnHeader prnColumnHeader;
		private System.Windows.Forms.ColumnHeader signalQualityColumnHeader;
		private System.Windows.Forms.ComboBox portList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabPage informationTabPage;
		private System.Windows.Forms.Label speedLabel;
		private System.Windows.Forms.RadioButton knotsRadio;
		private System.Windows.Forms.RadioButton kilometersPerHourRadio;
		private System.Windows.Forms.RadioButton milesPerHourRadio;
		private System.Windows.Forms.GroupBox speedGroup;
		private System.Windows.Forms.GroupBox directionGroup;
		private System.Windows.Forms.PictureBox directionImage;
		private System.Windows.Forms.Label bearingValueLabel;
		private System.Windows.Forms.Label bearingLabel;

	}
}