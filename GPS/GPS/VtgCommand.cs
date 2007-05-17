using System;
using System.Collections.Generic;
using System.Text;

namespace GPS
{
	/// <summary>
	/// Track made good and ground speed
	/// </summary>
	/// <seealso href="http://www.gpsinformation.org/dale/nmea.htm#GGA"/>
	public struct VtgCommand
	{
		private float _trueTrack;
		private float _magneticTrack;
		private float _speedKnots;
		private float _speedKilometersPerHour;

		public VtgCommand (string command)
		{
			string[] data = command.Split(new char[] { ',' }, StringSplitOptions.None);

			_trueTrack = Convert.ToSingle(data[1]);

			_magneticTrack = Convert.ToSingle(data[3]);

			_speedKnots = Convert.ToSingle(data[5]);

			_speedKilometersPerHour = Convert.ToSingle(data[7]);
		}
	}
}
