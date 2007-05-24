using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GPS
{
	public partial class MainForm : Form
	{
		private Gps _gps;

		public MainForm ()
		{
			Control.CheckForIllegalCrossThreadCalls = false;

			_gps = new Gps();
			_gps.TrackingChanged += new GpsEventHandler<RmcCommand>(gps_TrackingChanged);
			_gps.ViewableSatellitesChanged += new GpsEventHandler<GsvCommand>(gps_ViewableSatellitesChanged);

			InitializeComponent();
		}

		protected override void OnLoad (EventArgs e)
		{
			portList.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
			portList.SelectedIndex = portList.Items.Count - 1;

			base.OnLoad(e);
		}

		private void portList_SelectedIndexChanged (object sender, EventArgs e)
		{
			_gps.ComPort = portList.SelectedItem as string;
		}

		private void connectButton_Click (object sender, EventArgs e)
		{
			_gps.Start();
			connectButton.Enabled = false;
			disconnectButton.Enabled = true;
			portList.Enabled = false;
		}

		private void disconnectButton_Click (object sender, EventArgs e)
		{
			_gps.Stop();
			connectButton.Enabled = true;
			disconnectButton.Enabled = false;
			portList.Enabled = true;
		}

		private void gps_TrackingChanged (RmcCommand command)
		{
			directionImage.Invalidate();
			bearingValueLabel.Text = command.DirectionalAngle.ToString();
			RefreshDistance();
		}

		private void gps_ViewableSatellitesChanged (GsvCommand command)
		{
			RefreshSattellites();
		}

		private void distance_CheckedChanged (object sender, EventArgs e)
		{
			RefreshDistance();
		}

		private void RefreshDistance ()
		{
			float speed = _gps.Knots;

			if (milesPerHourRadio.Checked)
				speed = _gps.MilesPerHour;
			else if (kilometersPerHourRadio.Checked)
				speed = _gps.KilometersPerHour;

			speedLabel.Text = speed.ToString("N2");
		}

		private void RefreshSattellites ()
		{
			bool redraw = false;

			Satellite[] satellites = _gps.Satellites;
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
					redraw = true;
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

			Satellite[] satellites = _gps.Satellites;

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
				e.Graphics.DrawLine(anglePen, new PointF(centerX, centerY), new PointF((radius * Convert.ToSingle(Math.Cos(_gps.DirectionalAngle))) + centerXF, (radius * Convert.ToSingle(Math.Sin(_gps.DirectionalAngle))) + centerYF));
			}
		}
	}
}