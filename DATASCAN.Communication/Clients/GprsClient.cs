using System;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public async Task<byte[]> GetData(byte[] request)
        {
            var response = new byte[1024];
            SerialPortFixer.Execute(_port);
            using (var port = new SerialPort
            {
                PortName = _port,
                BaudRate = _baudrate,
                DataBits = _dataBits,
                StopBits = _stopBits,
                Parity = _parity,
                Handshake = Handshake.None,
                //ReadTimeout = 500000,
                //WriteTimeout = 500000,
                //WriteBufferSize = 1024,
                //ReadBufferSize = 1024
            })
            {
                if (!port.IsOpen)
                    port.Open();

                var stream = port.BaseStream;
                var req = Encoding.ASCII.GetBytes(@"ATQ0V1E0" + "\r\n");
                await stream.WriteAsync(req, 0, req.Length);

                await stream.FlushAsync();

                var resp = new byte[1024];
                var result = await stream.ReadAsync(resp, 0, resp.Length);

                //port.WriteLine(@"ATQ0V1E0" + "\r\n");
                //Thread.Sleep(5000);
                //var result = port.ReadExisting();

                await stream.FlushAsync();

                if (!Encoding.ASCII.GetString(resp).Contains("OK"))
                    return response;

                //await stream.FlushAsync();

                req = Encoding.ASCII.GetBytes($@"ATDT{_phone}" + "\r\n");
                await stream.WriteAsync(req, 0, req.Length);
                await stream.FlushAsync();
                result = await stream.ReadAsync(resp, 0, resp.Length);
                await stream.FlushAsync();

                //port.WriteLine($@"ATDT{_phone}" + "\r\n");
                //Thread.Sleep(5000);

                //var status = port.ReadExisting();

                //if (!status.Contains("CONNECT"))
                //return response;

                if (!Encoding.ASCII.GetString(resp).Contains("CONNECT"))
                    return response;

                //await stream.FlushAsync();

                

                stream.Write(request, 0, request.Length);
                await stream.FlushAsync();


                var read = await stream.ReadAsync(response, 0, 5);
                await stream.FlushAsync();

                req = Encoding.ASCII.GetBytes(@"ATH0" + "\r\n");
                await stream.WriteAsync(req, 0, req.Length);
            }

            return response;
        }
    }
}