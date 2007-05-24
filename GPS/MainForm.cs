using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace GPS
{
	public partial class MainForm : Form
	{
		private GgaCommand _lastGga;
		private GsaCommand _lastGsa;
		private GsvCommand _lastGsv;
		private RmcCommand _lastRmc;
		private GllCommand _lastGll;

		public MainForm ()
		{
			Control.CheckForIllegalCrossThreadCalls = false;
			
			InitializeComponent();

			_port.NewLine = "\r\n";
		}

		protected override void OnLoad (EventArgs e)
		{
			portList.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
			portList.SelectedIndex = portList.Items.Count - 1;

			base.OnLoad(e);
		}

		public Satellite[] Satellites
		{
			get
			{
				if (_lastGsv == null)
					return new Satellite[0];

				List<Satellite> sats = new List<Satellite>(_lastGsv.Satellites);

				for (int i = 0; i < sats.Count; i++)
					sats[i] = sats[i] + _lastGsa;

				return sats.ToArray();
			}
		}

		private void portList_SelectedIndexChanged (object sender, EventArgs e)
		{
			_port.PortName = portList.SelectedItem as string;
		}

		private void connectButton_Click (object sender, EventArgs e)
		{
			_port.Open();
			_timer.Start();

			connectButton.Enabled = false;
			disconnectButton.Enabled = true;
			portList.Enabled = false;
		}

		private void disconnectButton_Click (object sender, EventArgs e)
		{
			_timer.Stop();
			_port.Close();

			connectButton.Enabled = true;
			disconnectButton.Enabled = false;
			portList.Enabled = true;
		}

		private void timer_Tick (object sender, EventArgs e)
		{
			try {
				string data = _port.ReadLine();

				if (String.IsNullOrEmpty(data) == false) {
					ProcessCommand(data);
				}
			} catch (Exception exc) {
				System.Diagnostics.Debug.WriteLine(exc);
			}
		}
		
		private void altitude_CheckedChanged (object sender, EventArgs e)
		{
			RefreshAltitude();
		}

		private void distance_CheckedChanged (object sender, EventArgs e)
		{
			RefreshDistance();
		}

		private void ProcessCommand (string data)
		{
			// all commands start with dollar signs
			if (data[0] != '$')
				return;

			//System.Diagnostics.Debug.WriteLine(data, "Command");

			if (data.IndexOf('*') != -1)
				data = data.Substring(0, data.IndexOf('*'));

			switch (data.Substring(3, 3)) {

				case "GGA":
					_lastGga = new GgaCommand(data);
					latitudeValueLabel.Text = _lastGga.Location.Latitude.ToString("N6");
					longitudeValueLabel.Text = _lastGga.Location.Longitude.ToString("N6");
					RefreshAltitude();
					break;

				case "GSA":
					_lastGsa = new GsaCommand(data);
					RefreshSattellites();
					break;

				case "GSV":
					_lastGsv = _lastGsv + new GsvCommand(data);
					RefreshSattellites();
					break;

				case "RMC":
					_lastRmc = new RmcCommand(data);
					directionImage.Invalidate();
					bearingValueLabel.Text = _lastRmc.DirectionalAngleInDegrees.ToString();
					RefreshDistance();
					break;

				case "GLL":
					_lastGll = new GllCommand(data);
					break;

				default:
					System.Diagnostics.Trace.WriteLine("*** " + data.Substring(3, 3) + " Not Handled", "Command");
					break;
			}
		}

		private void RefreshAltitude ()
		{
			float altitude = _lastGga.Location.AltitudeInFeet;

			if (altitudeInMetersRadio.Checked)
				altitude = _lastGga.Location.AltitudeInMeters;

			altitudeValueLabel.Text = altitude.ToString();
		}

		private void RefreshDistance ()
		{
			float speed = _lastRmc.Knots;

			if (milesPerHourRadio.Checked)
				speed = _lastRmc.MilesPerHour;
			else if (kilometersPerHourRadio.Checked)
				speed = _lastRmc.KilometersPerHour;

			speedLabel.Text = speed.ToString("N2");
		}

		private void RefreshSattellites ()
		{
			Satellite[] satellites = this.Satellites;

			for (int i = 0; i < satellites.Length; i++) {
				Satellite sat = satellites[i];
				ListViewItem item = sat.Tag as ListViewItem;

				if (item == null) {
					item = new ListViewItem(new string[] {
							sat.Prn.ToString(),
							sat.SignalQuality.ToString()
						},
						satelliteList.Groups[sat.HasFix ? "hasFixGroup" : "doesNotHaveFixGroup"]
					);

					satelliteList.Items.Add(item);
				}

				sat.Tag = item;
			}

			satilliteLocationImage.Invalidate();
		}

		private void satilliteLocationImage_Paint (object sender, PaintEventArgs e)
		{
			float centerXF = (float)satilliteLocationImage.Bounds.Width / 2F;
			int centerX = satilliteLocationImage.Bounds.Width / 2;
			float centerYF = (float)satilliteLocationImage.Bounds.Height / 2F;
			int centerY = satilliteLocationImage.Bounds.Height / 2;
			float maxRadius = (Math.Min((float)satilliteLocationImage.Bounds.Height, (float)satilliteLocationImage.Bounds.Width) - 20F) / 2F;

			using (Pen circlePen = new Pen(Color.DarkBlue, 1)) {

				double[] elevations = new double[] { 0, Math.PI / 2, Math.PI / 3, Math.PI / 6 };

				foreach (double elevation in elevations) {
					float radius = Convert.ToSingle(System.Math.Cos(elevation) * maxRadius);
					e.Graphics.DrawEllipse(circlePen, (centerXF - radius), (centerYF - radius), (2F * radius), (2F * radius));
				}

				e.Graphics.DrawLine(circlePen, new Point(centerX - 4, centerY), new Point(centerX + 4, centerY));
				e.Graphics.DrawLine(circlePen, new Point(centerX, centerY - 4), new Point(centerX, centerY + 4));
			}

			if (_lastGsv == null)
				return;

			Satellite[] satellites = this.Satellites;

			if (satellites != null && satellites.Length > 0) {

				using (Pen satellitePen = new Pen(Color.Yellow, 4F)) {
					using (Pen hasFixSatellitePen = new Pen(Color.Green, 4F)) {

						foreach (Satellite sat in satellites) {
							double h = (double)System.Math.Cos((sat.Elevation * Math.PI) / 180D) * maxRadius;

							float satX = centerXF + Convert.ToSingle(h * Math.Sin(((float)sat.Azimuth * Math.PI) / 180D));
							float satY = centerYF - Convert.ToSingle(h * Math.Cos(((float)sat.Azimuth * Math.PI) / 180D));

							e.Graphics.DrawRectangle(sat.HasFix ? hasFixSatellitePen : satellitePen, satX, satY, 4F, 4F);
							e.Graphics.DrawString(sat.Prn.ToString(), new Font("Tahoma", 8, FontStyle.Regular), new System.Drawing.SolidBrush(Color.Black), new PointF(satX + 5F, satY + 5F));
						}
					}
				}
			}
		}

		private void directionImage_Paint (object sender, PaintEventArgs e)
		{
			int centerX = directionImage.Bounds.Width / 2;
			int centerY = directionImage.Bounds.Height / 2;
			float maxWidth = Math.Min((float)directionImage.Bounds.Height, (float)directionImage.Bounds.Width) - 40F;
			float centerXF = (float)directionImage.Bounds.Width / 2F;
			float centerYF = (float)directionImage.Bounds.Height / 2F;
			float radius = maxWidth / 2F;

			e.Graphics.DrawLine(Pens.Black, new Point(0, centerY), new Point(directionImage.Bounds.Width, centerY));
			e.Graphics.DrawLine(Pens.Black, new Point(centerX, 0), new Point(centerX, directionImage.Bounds.Height));
			e.Graphics.DrawEllipse(Pens.Black, centerXF - (maxWidth / 2F), centerYF - (maxWidth / 2F), maxWidth, maxWidth);

			using (Font directionFont = new Font("Tahoma", 12F, FontStyle.Bold)) {
				e.Graphics.DrawString("N", directionFont, Brushes.Purple, centerXF, 0F);
				e.Graphics.DrawString("S", directionFont, Brushes.Purple, centerXF, directionImage.Bounds.Height - 20);
				e.Graphics.DrawString("E", directionFont, Brushes.Purple, directionImage.Bounds.Width - 20, centerYF);
				e.Graphics.DrawString("W", directionFont, Brushes.Purple, 0, centerYF);
			}

			using (Pen anglePen = new Pen(Color.Blue, 4F)) {
				e.Graphics.DrawLine(anglePen, new PointF(centerX, centerY), new PointF((radius * Convert.ToSingle(Math.Cos(_lastRmc.DirectionalAngleInRadians))) + centerXF, (radius * Convert.ToSingle(Math.Sin(_lastRmc.DirectionalAngleInRadians))) + centerYF));
			}
		}
	}
}