using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Timers;

namespace GPS
{
	public delegate void GpsEventHandler<T> (T command);
	public delegate void GpsCommandEventHandler (string command);

	public class Gps : IDisposable
	{
		private string _comPort;
		private SerialPort _port;

		private Timer _timer;

		private GgaCommand _lastGga;
		private GsaCommand _lastGsa;
		private GsvCommand _lastGsv;
		private RmcCommand _lastRmc;
		private GllCommand _lastGll;

		public Gps ()
		{
			_timer = new Timer();
			_timer.AutoReset = true;
			_timer.Interval = 250;
			_timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

			_port = new SerialPort();
			_port.Parity = Parity.None;
			_port.BaudRate = 4800;
			_port.StopBits = StopBits.One;
			_port.DataBits = 8;
			_port.NewLine = "\r\n";
			_port.ReadBufferSize = 256;
			_port.ReadTimeout = 0;
		}

		public event GpsEventHandler<GgaCommand> FixChanged;

		private void OnFixChanged (GgaCommand command)
		{
			if (FixChanged != null)
				FixChanged(command);
		}

		public event GpsEventHandler<GllCommand> PositionChanged;

		private void OnPositionChanged (GllCommand command)
		{
			if (PositionChanged != null)
				PositionChanged(command);
		}

		public event GpsEventHandler<GsaCommand> SatelliteStatusChanged;

		private void OnSatelliteStatusChanged (GsaCommand command)
		{
			if (SatelliteStatusChanged != null)
				SatelliteStatusChanged(command);
		}

		public event GpsEventHandler<RmcCommand> TrackingChanged;

		private void OnTrackingChanged (RmcCommand command)
		{
			if (TrackingChanged != null)
				TrackingChanged(command);
		}

		public event GpsEventHandler<GsvCommand> ViewableSatellitesChanged;

		private void OnViewableSatellitesChanged (GsvCommand command)
		{
			if (ViewableSatellitesChanged != null)
				ViewableSatellitesChanged(command);
		}

		public event GpsCommandEventHandler CommandReceived;

		private void OnCommandReceived (string command)
		{
			if (CommandReceived != null)
				CommandReceived(command);
		}

		public string ComPort
		{
			get { return _comPort; }
			set { _comPort = value; }
		}

		public float MilesPerHour
		{
			get { return _lastRmc.MilesPerHour; }
		}

		public float KilometersPerHour
		{
			get { return _lastRmc.KilometersPerHour; }
		}

		public float Knots
		{
			get { return _lastRmc.Knots; }
		}

		public float DirectionalAngle
		{
			get { return (_lastRmc.DirectionalAngle - 90F) * Convert.ToSingle(Math.PI / 180D); }
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

		public void Start ()
		{
			_port.PortName = _comPort;
			_port.Open();
			_timer.Start();
		}

		private void timer_Elapsed (object sender, ElapsedEventArgs e)
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

		public void Stop ()
		{
			_timer.Stop();
			_port.Close();
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
					OnFixChanged(_lastGga);
					break;

				case "GSA":
					_lastGsa = new GsaCommand(data);
					OnSatelliteStatusChanged(_lastGsa);
					break;

				case "GSV":
					_lastGsv = _lastGsv + new GsvCommand(data);
					OnViewableSatellitesChanged(_lastGsv);
					break;

				case "RMC":
					_lastRmc = new RmcCommand(data);
					OnTrackingChanged(_lastRmc);
					break;

				case "GLL":
					_lastGll = new GllCommand(data);
					OnPositionChanged(_lastGll);
					break;

				default:
					System.Diagnostics.Trace.WriteLine("*** " + data.Substring(3, 3) + " Not Handled", "Command");
					break;
			}

			// send notification of a new command
			OnCommandReceived(data);
		}

		#region IDisposable Members

		public void Dispose ()
		{
			if (_port != null)
				_port.Dispose();
		}

		#endregion
	}
}