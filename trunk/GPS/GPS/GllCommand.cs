using System;
using System.Collections.Generic;
using System.Text;

namespace GPS
{
	/// <summary>
	/// Geographic position, Latitude and Longitude
	/// </summary>
	/// <seealso href="http://www.gpsinformation.org/dale/nmea.htm#GLL"/>
	public struct GllCommand
	{
		private DateTime _takenAt;
		private char _data;
		private Location _location;

		public GllCommand (string command)
		{
			string[] data = command.Split(new char[] { ',' }, StringSplitOptions.None);
			DateTime utcNow = DateTime.UtcNow;

			// format in HHmmss
			_takenAt = new DateTime(
				utcNow.Year,
				utcNow.Month,
				utcNow.Day,
				Convert.ToInt32(data[5].Substring(0, 2)),
				Convert.ToInt32(data[5].Substring(2, 2)),
				Convert.ToInt32(data[5].Substring(4, 2)),
				DateTimeKind.Utc
			);

			int latitudeDeg = Convert.ToInt32(data[1].Substring(0, 2));
			decimal latitudeMins = Convert.ToDecimal(data[1].Substring(2));

			decimal latitude = (((decimal)latitudeDeg) + (latitudeMins / 60M)) * (data[2].ToUpper() == "N" ? 1M : -1M);

			int longitudeDeg = Convert.ToInt32(data[3].Substring(0, 3));
			decimal longitudeMins = Convert.ToDecimal(data[3].Substring(3));

			decimal longitude = (((decimal)longitudeDeg) + (longitudeMins / 60M)) * (data[4].ToUpper() == "E" ? 1M : -1M);

			_location = new Location(latitude, longitude, 0F);

			_data = data[6][0];
		}
	}
}
