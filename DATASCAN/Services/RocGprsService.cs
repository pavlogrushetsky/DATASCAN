using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using DATASCAN.Core.Entities.Rocs;

namespace DATASCAN.Services
{
    public class RocGprsService
    {
        private readonly string _port;
        private readonly int _baudrate;
        private readonly Parity _parity;
        private readonly int _dataBits;
        private readonly StopBits _stopBits;
        private readonly Handshake _handshake;
        private readonly int _readTimeout;
        private readonly int _writeTimeout;

        public RocGprsService(string port, int baudrate, Parity parity, int dataBits, StopBits stopBits)
        {
            _port = port;
            _baudrate = baudrate;
            _parity = parity;
            _dataBits = dataBits;
            _stopBits = stopBits;
            _handshake = Handshake.None;
            _readTimeout = 500;
            _writeTimeout = 500;
        }

        public async Task GetEventData(Roc809 roc, Roc809MeasurePoint point, Action<List<Roc809EventData>> onSuccess, Action<Exception> onException)
        {
            await Task.Factory.StartNew(() =>
            {
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
                    port.Open();
                    port.WriteLine($@"ATDT{roc.Phone}" + "\r\n");
                    Thread.Sleep(500);

                    var result = port.ReadExisting();

                    

                    port.WriteLine(@"ATH0" + "\r\n");
                    Thread.Sleep(500);
                }
            });
        }
    }
}