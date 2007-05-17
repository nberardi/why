using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GPS
{
	public class Satellite
	{
		private int _prn;
		private int _elevation;
		private int _azimuth;
		private int _signalQuality;
		private bool _hasFix;
		private object _tag;

		public Satellite (string[] data)
		{
			_prn = Convert.ToInt32(data[0]);
			_elevation = Convert.ToInt32(data[1]);
			_azimuth = Convert.ToInt32(data[2]);
			_signalQuality = Convert.ToInt32(data[3]);
			_hasFix = false;
			_tag = null;
		}

		public int Prn
		{
			get { return _prn; }
			private set
			{
				if (value != _prn && Tag is ListViewItem)
					(Tag as ListViewItem).SubItems[0].Text = value.ToString();

				_prn = value;
			}
		}

		public int Elevation
		{
			get { return _elevation; }
		}

		public int Azimuth
		{
			get { return _azimuth; }
		}

		public int SignalQuality
		{
			get { return _signalQuality; }
			private set
			{
				if (value != _signalQuality && Tag is ListViewItem)
					(Tag as ListViewItem).SubItems[1].Text = value.ToString();

				_signalQuality = value;
			}
		}

		public bool HasFix
		{
			get { return _hasFix; }
			private set
			{
				if (value != _hasFix && Tag is ListViewItem)
					(Tag as ListViewItem).Group = (Tag as ListViewItem).ListView.Groups[value ? "hasFixGroup" : "doesNotHaveFixGroup"];

				_hasFix = value;
			}
		}

		public object Tag
		{
			get { return _tag; }
			set { _tag = value; }
		}

		public override bool Equals (object obj)
		{
			if (obj == null)
				return false;

			if (obj is Satellite == false)
				return false;

			return ((Satellite)obj)._prn.Equals(_prn);
		}

		public override int GetHashCode ()
		{
			return _prn.GetHashCode();
		}

		public override string ToString ()
		{
			return String.Format("PRN:{0},Quality:{1},Fixed:{2}", _prn, _signalQuality, _hasFix);
		}

		public static Satellite operator + (Satellite sat, GsaCommand command)
		{
			if (command.FixedPrns == null)
				return sat;

			sat.HasFix = command.FixedPrns.Contains(sat.Prn);

			return sat;
		}

		public static Satellite operator + (Satellite sat1, Satellite sat2)
		{
			if (sat1 == null)
				return sat2;

			if (sat2 == null)
				return sat1;

			sat1.Prn = sat2.Prn;
			sat1._elevation = sat2.Elevation;
			sat1._azimuth = sat2.Azimuth;
			sat1.SignalQuality = sat2.SignalQuality;

			return sat1;
		}
	}

	/// <summary>
	/// Satellites in view
	/// </summary>
	/// <seealso href="http://www.gpsinformation.org/dale/nmea.htm#GSV"/>
	public class GsvCommand
	{
		private Satellite[] _satellites;

		private GsvCommand ()
		{
			_satellites = new Satellite[0];
		}

		public GsvCommand (string command)
		{
			string[] data = command.Split(new char[] { ',' }, StringSplitOptions.None);

			_satellites = new Satellite[Convert.ToInt32(data[3])];

			List<Satellite> satellites = new List<Satellite>(_satellites.Length);
			for (int i = 4; i < data.Length; i = i + 4)
				satellites.Add(new Satellite(new string[] { data[i], data[i + 1], data[i + 2], data[i + 3] }));

			_satellites = satellites.ToArray();
		}

		public Satellite[] Satellites
		{
			get { return _satellites; }
		}

		public static GsvCommand operator + (GsvCommand command1, GsvCommand command2)
		{
			if (command1 == null && command2 == null)
				return new GsvCommand();

			if (command1 == null)
				return command2;

			if (command2 == null)
				return command1;

			GsvCommand command = new GsvCommand();

			List<Satellite> satellites = new List<Satellite>(command1._satellites);

			foreach (Satellite sat in command2._satellites) {
				int index = satellites.IndexOf(sat);

				if (index >= 0)
					satellites[index] = satellites[index] + sat;
				else
					satellites.Add(sat);
			}

			command._satellites = satellites.ToArray();

			return command;
		}
	}
}