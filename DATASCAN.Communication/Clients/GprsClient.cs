using System;
using System.IO.Ports;
using System.Linq;
using System.Text;
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
            _readTimeout = 5000;
            _writeTimeout = 5000;
        }

        public async Task<string> TestConnection()
        {
            var response = new byte[1024];

            if (!SerialPort.GetPortNames().Contains(_port))
                return "";

            SerialPortFixer.Execute(_port);
            using (var port = new SerialPort
            {
                PortName = _port,
                BaudRate = _baudrate,
                DataBits = _dataBits,
                StopBits = _stopBits,
                Parity = _parity,
                Handshake = _handshake,
                WriteTimeout = _writeTimeout,
                ReadTimeout = _readTimeout,
                DtrEnable = true
            })
            {
                await Task.Delay(1000);

                if (!port.IsOpen)
                {
                    port.Open();
                }

                var stream = port.BaseStream;

                port.WriteLine(@"AT" + "\r\n");
                await Task.Delay(1000);
                await stream.ReadAsync(response, 0, response.Length);

                var status = Encoding.ASCII.GetString(response);
                if (!status.Contains("OK"))
                {
                    return "";
                }

                port.WriteLine(@"AT&FE0V1X1&D2&C1S0=0" + "\r\n");
                await Task.Delay(1000);
                await stream.ReadAsync(response, 0, response.Length);

                status = Encoding.ASCII.GetString(response);
                if (!status.Contains("OK"))
                {
                    return "";
                }

                port.WriteLine($@"ATDT{_phone}" + "\r\n");
                await Task.Delay(1000);
                await stream.ReadAsync(response, 0, response.Length);

                status = Encoding.ASCII.GetString(response);

                port.WriteLine(@"ATH0" + "\r\n");
                await Task.Delay(1000);

                port.Close();

                return status;
            }
        }

        public async Task<byte[]> GetData(byte[] request)
        {
            return await Task.Run(async () =>
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
                    Handshake = _handshake,
                    WriteTimeout = _writeTimeout,
                    ReadTimeout = _readTimeout,
                    DtrEnable = true
                })
                {
                    var retries = 3;
                    do
                    {
                        await Task.Delay(1000);

                        if (!port.IsOpen)
                        {
                            port.Open();
                        }

                        var stream = port.BaseStream;

                        port.WriteLine(@"AT" + "\r\n");
                        await Task.Delay(1000);
                        await stream.ReadAsync(response, 0, response.Length);

                        if (!Encoding.ASCII.GetString(response).Contains("OK"))
                        {
                            break;
                        }

                        port.WriteLine(@"AT&FE0V1X1&D2&C1S0=0" + "\r\n");
                        await Task.Delay(1000);
                        await stream.ReadAsync(response, 0, response.Length);

                        if (!Encoding.ASCII.GetString(response).Contains("OK"))
                        {
                            break;
                        }

                        port.WriteLine($@"ATDT{_phone}" + "\r\n");
                        await Task.Delay(1000);
                        await stream.ReadAsync(response, 0, response.Length);

                        if (!Encoding.ASCII.GetString(response).Contains("CONNECT"))
                        {
                            break;
                        }

                        port.DiscardInBuffer();
                        port.DiscardOutBuffer();

                        port.Write(request, 0, request.Length);
                        await Task.Delay(1000);
                        var task = stream.ReadAsync(response, 0, response.Length);
                        await Task.WhenAny(task, Task.Delay(10000));

                        if (response[0] != 0x00)
                            break;

                        port.DiscardInBuffer();

                        port.WriteLine(@"ATH0" + "\r\n");
                        await Task.Delay(1000);

                        port.Close();

                        retries--;
                    } while (retries > 0);                    
                }

                return response;
            });            
        }
    }
}