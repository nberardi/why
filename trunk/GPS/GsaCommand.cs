using System;
using System.Collections.Generic;
using System.Text;

namespace GPS
{
	public enum DimensionFix
	{
		NoFix,
		Fix2D,
		Fix3D
	}

	public enum AutoSelection
	{
		Auto,
		Manual
	}

	/// <summary>
	/// Satellite status
	/// </summary>
	/// <seealso href="http://www.gpsinformation.org/dale/nmea.htm#GSA"/>
	public struct GsaCommand
	{
		private char _selection;
		private DimensionFix _dimensionFix;
		private IList<int> _fixedPrns;
		private float _pdop;
		private float _hdop;
		private float _vdop;

		public GsaCommand (string command)
		{
			string[] data = command.Split(new char[] { ',' }, StringSplitOptions.None);

			_selection = data[1][0];

			_dimensionFix = (DimensionFix)Convert.ToInt32(data[2]);

			List<int> satellites = new List<int>(12);

			for (int i = 3; i < 15; i++)
				if (!String.IsNullOrEmpty(data[i]))
					satellites.Add(Convert.ToInt32(data[i]));

			_fixedPrns = satellites.AsReadOnly();

			_pdop = Convert.ToSingle(data[15]);
			_hdop = Convert.ToSingle(data[16]);
			_vdop = Convert.ToSingle(data[17]);
		}

		public IList<int> FixedPrns
		{
			get { return _fixedPrns; }
		}
	}
}
