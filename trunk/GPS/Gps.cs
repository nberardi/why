using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Timers;

namespace GPS
{
	public delegate void GpsSatelliteEventHandler ();
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
		private VtgCommand _lastVtg;

		public Gps ()
		{
			_timer = new Timer();
			_timer.AutoReset = true;
			_timer.Interval = 500;
			_timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
		}

		public event GpsSatelliteEventHandler SatellitesChanged;

		private void OnSatellitesChanged ()
		{
			if (SatellitesChanged != null)
				SatellitesChanged();
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
			_port = new SerialPort();
			_port.PortName = _comPort;
			_port.Parity = Parity.None;
			_port.BaudRate = 4800;
			_port.StopBits = StopBits.One;
			_port.DataBits = 8;
			_port.NewLine = "\r\n";
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

			System.Diagnostics.Debug.WriteLine(data, "Command");

			if (data.IndexOf('*') != -1)
				data = data.Substring(0, data.IndexOf('*'));

			switch (data.Substring(3, 3)) {

				case "GGA":
					_lastGga = new GgaCommand(data);
					break;

				case "GSA":
					_lastGsa = new GsaCommand(data);
					OnSatellitesChanged();
					break;

				case "GSV":
					_lastGsv = _lastGsv + new GsvCommand(data);
					OnSatellitesChanged();
					break;

				case "RMC":
					_lastRmc = new RmcCommand(data);
					break;

				case "GLL":
					_lastGll = new GllCommand(data);
					break;

				case "VTG":
					_lastVtg = new VtgCommand(data);
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