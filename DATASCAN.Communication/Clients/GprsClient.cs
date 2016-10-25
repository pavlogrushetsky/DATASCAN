using System.IO.Ports;
using System.Threading;
using DATASCAN.Communication.Common;

namespace DATASCAN.Communication.Clients
{
    public class GprsClient : IClient
    {
        private readonly string _phone;
        private readonly string _port;
        private readonly int _baudrate;
        private readonly Parity _parity;
        private readonly int _dataBits;
        private readonly StopBits _stopBits;
        private readonly Handshake _handshake;
        private readonly int _readTimeout;
        private readonly int _writeTimeout;

        public GprsClient(string phone, string port, int baudrate, Parity parity, int dataBits, StopBits stopBits)
        {
            _phone = phone;
            _port = port;
            _baudrate = baudrate;
            _parity = parity;
            _dataBits = dataBits;
            _stopBits = stopBits;
            _handshake = Handshake.None;
            _readTimeout = 500;
            _writeTimeout = 500;
        }

        public byte[] GetData(byte[] request)
        {
            SerialPortFixer.Execute(_port);
            using (var port = new SerialPort
            {
                PortName = _port,
                BaudRate = _baudrate,
                DataBits = _dataBits,
                StopBits = _stopBits,
                Parity = _parity,
                Handshake = _handshake,
                ReadTimeout = _readTimeout,
                WriteTimeout = _writeTimeout
            })
            {
                if (!port.IsOpen)
                    port.Open();

                port.WriteLine(@"ATQ0V1E0" + "\r\n");
                Thread.Sleep(500);

                var status = port.ReadExisting();
                if (!status.Contains("OK"))
                {
                    return new byte[] { };
                }

                port.WriteLine($@"ATDT{_phone}" + "\r\n");
                Thread.Sleep(500);

                port.Write(request, 0, request.Length);
                Thread.Sleep(500);

                var response = new byte[1024];
                port.Read(response, 0, response.Length);
                Thread.Sleep(500);

                port.WriteLine(@"ATH0" + "\r\n");
                Thread.Sleep(500);
            }

            return new byte[] {};
        }
    }
}