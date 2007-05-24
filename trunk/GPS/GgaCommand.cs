using System;
using System.Globalization;

namespace GPS
{
	public enum FixQuality
	{
		Invalid = 0,
		GpxFix,
		DGpsFix,
		PpsFix,
		RealTimeKinematic,
		FloatRtk,
		Estimated,
		ManualInputMode,
		SimulationMode
	}

	public struct Location
	{
		private decimal _latitude;
		private decimal _longitude;
		private float _altitude;

		public Location (decimal latitude, decimal longitude, float altitude)
		{
			_latitude = Decimal.Round(latitude, 6);
			_longitude = Decimal.Round(longitude, 6);
			_altitude = altitude;
		}

		public override string ToString ()
		{
			return String.Format("{0:N6},{1:N6}+{2}", _latitude, _longitude, _altitude);
		}
	}

	/// <summary>
	/// Global Positioning System Fix Data
	/// </summary>
	/// <seealso href="http://www.gpsinformation.org/dale/nmea.htm#GGA"/>
	public struct GgaCommand
	{
		private DateTime _takenAt;
		private Location _location;
		private FixQuality _quality;
		private byte _satelliteCount;
		private float _horizontalDilution;
		private float _meanSeaLevel;

		public GgaCommand (string command)
		{
			string[] data = command.Split(new char[] { ',' }, StringSplitOptions.None);
			DateTime utcNow = DateTime.UtcNow;

			// format in HHmmss
			_takenAt = new DateTime(
				utcNow.Year,
				utcNow.Month,
				utcNow.Day,
				Convert.ToInt32(data[1].Substring(0, 2)),
				Convert.ToInt32(data[1].Substring(2, 2)),
				Convert.ToInt32(data[1].Substring(4, 2)),
				DateTimeKind.Utc
			);

			int latitudeDeg = Convert.ToInt32(data[2].Substring(0, 2));
			decimal latitudeMins = Convert.ToDecimal(data[2].Substring(2));

			decimal latitude = (((decimal)latitudeDeg) + (latitudeMins / 60M)) * (data[3].ToUpper() == "N" ? 1M : -1M);

			int longitudeDeg = Convert.ToInt32(data[4].Substring(0, 3));
			decimal longitudeMins = Convert.ToDecimal(data[4].Substring(3));

			decimal longitude = (((decimal)longitudeDeg) + (longitudeMins / 60M)) * (data[5].ToUpper() == "E" ? 1M : -1M);

			_quality = (FixQuality)Convert.ToInt32(data[6]);

			_satelliteCount = Convert.ToByte(data[7]);

			_horizontalDilution = Convert.ToSingle(data[8]);

			float altitude = Convert.ToSingle(data[9]);

			_meanSeaLevel = Convert.ToSingle(data[11]);

			_location = new Location(latitude, longitude, altitude);
		}
	}
}
