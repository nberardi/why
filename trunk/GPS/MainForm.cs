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

		private void RefreshSattellites ()
		{
			satilliteLocationImage.Invalidate();

			Satellite[] satellites = _gps.Satellites;
			for(int i = 0; i < satellites.Length; i++) {
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
							e.Graphics.DrawString(sat.Prn.ToString(), new Font("Verdana", 8, FontStyle.Regular), new System.Drawing.SolidBrush(Color.Black), new PointF(satX + 5F, satY + 5F));
						}
					}
				}
			}
		}

		private void refreshSatelliteTimer_Tick (object sender, EventArgs e)
		{
			RefreshSattellites();
		}
	}
}