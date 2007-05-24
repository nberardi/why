using System;
using System.Collections.Generic;
using System.Text;

namespace GPS
{
	/// <summary>
	/// Recommended Minimum sentence C
	/// </summary>
	/// <seealso href="http://www.gpsinformation.org/dale/nmea.htm#RMC"/>
	public struct RmcCommand
	{
		private const float KnotsToMilesPerHour = 1.15077945F;
		private const float KnotsToKilometersPerHour = 1.85200F;

		private DateTime _takenAt;
		private char _status;
		private Location _location;
		private float _speedKnots;
		private float? _trackAngle;
		private decimal _magneticVariation;

		public RmcCommand (string command)
		{
			string[] data = command.Split(new char[] { ',' }, StringSplitOptions.None);

			// format in HHmmss
			_takenAt = new DateTime(
				2000 + Convert.ToInt32(data[9].Substring(4, 2)),
				Convert.ToInt32(data[9].Substring(2, 2)),
				Convert.ToInt32(data[9].Substring(0, 2)),
				Convert.ToInt32(data[1].Substring(0, 2)),
				Convert.ToInt32(data[1].Substring(2, 2)),
				Convert.ToInt32(data[1].Substring(4, 2)),
				DateTimeKind.Utc
			);

			_status = data[2][0];

			int latitudeDeg = Convert.ToInt32(data[3].Substring(0, 2));
			decimal latitudeMins = Convert.ToDecimal(data[3].Substring(2));

			decimal latitude = (((decimal)latitudeDeg) + (latitudeMins / 60M)) * (data[4].ToUpper() == "N" ? 1M : -1M);

			int longitudeDeg = Convert.ToInt32(data[5].Substring(0, 3));
			decimal longitudeMins = Convert.ToDecimal(data[5].Substring(3));

			decimal longitude = (((decimal)longitudeDeg) + (longitudeMins / 60M)) * (data[6].ToUpper() == "E" ? 1M : -1M);

			_location = new Location(latitude, longitude, 0F);

			_speedKnots = Convert.ToSingle(data[7]);

			_trackAngle = String.IsNullOrEmpty(data[8]) ? (float?)null : Convert.ToSingle(data[8]);

			_magneticVariation = Convert.ToDecimal(data[9]) * (data[10].ToUpper() == "E" ? 1M : -1M);
		}

		public float DirectionalAngleInDegrees
		{
			get { return _trackAngle ?? 0; }
		}

		public float DirectionalAngleInRadians
		{
			get { return (DirectionalAngleInDegrees - 90F) * Convert.ToSingle(Math.PI / 180D); }
		}

		public float Knots
		{
			get { return _speedKnots; }
		}

		public float MilesPerHour
		{
			get { return _speedKnots * KnotsToMilesPerHour; }
		}

		public float KilometersPerHour
		{
			get { return _speedKnots * KnotsToKilometersPerHour; }
		}
	}
}
