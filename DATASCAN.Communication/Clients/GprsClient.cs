using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATASCAN.Communication.Common;
using DATASCAN.Core.Entities.Rocs;

namespace DATASCAN.Communication.Clients
{
    public class GprsClient : IClient, IDisposable
    {
        private List<string> _ports; 

        public List<string> Ports
        {
            get
            {
                return _ports;
            }
            set
            {
                _ports = value;
                Validate(_ports);
            }
        }
        public int Baudrate { get; set; }
        public Parity Parity { get; set; } 
        public int DataBits { get; set; }
        public StopBits StopBits { get; set; }
        public int Timeout { get; set; }
        public int Retries { get; set; }
        public int WriteDelay { get; set; }
        public int ReadDelay { get; set; }

        private Dictionary<SerialPort, ModemStatus> _statuses;

        internal class ModemStatus
        {
            public string Phone { get; set; } = "";
            public Status Status { get; set; }
        }

        internal enum Status
        {
            OK,
            CONNECTED,
            BUSY
        }

        private void Validate(List<string> ports)
        {
            _statuses = new Dictionary<SerialPort, ModemStatus>();

            ports.ForEach(port =>
            {
                Task.Run(async () =>
                {
                    if (!SerialPort.GetPortNames().Contains(port))
                    {
                        return;
                    }

                    try
                    {
                        SerialPortFixer.Execute(port);
                    }
                    catch (Exception)
                    {
                        return;
                    }                    

                    using (var serialPort = new SerialPort
                    {
                        PortName = port,
                        BaudRate = Baudrate,
                        DataBits = DataBits,
                        StopBits = StopBits,
                        Parity = Parity,
                        Handshake = Handshake.None,
                        WriteTimeout = Timeout * 1000,
                        ReadTimeout = Timeout * 1000,
                        DtrEnable = true
                    })
                    {                        
                        if (!serialPort.IsOpen)
                        {
                            serialPort.Open();
                        }

                        var response = new byte[1024];
                        var stream = serialPort.BaseStream;

                        await Task.Delay(WriteDelay * 1000);
                        serialPort.WriteLine(@"AT" + "\r\n");
                        await Task.Delay(ReadDelay * 1000);
                        var task = stream.ReadAsync(response, 0, response.Length);
                        await Task.WhenAny(task, Task.Delay(Timeout * 1000));

                        var status = Encoding.ASCII.GetString(response);
                        if (status.Contains("OK"))
                        {
                            _statuses.Add(serialPort, new ModemStatus { Status = Status.OK });
                        }
                    }
                });
            });
        }
        
        public async Task<byte[]> GetData(Roc809 roc, byte[] request)
        {
            var now = DateTime.Now;

            while (!_statuses.Any(s => s.Value.Status == Status.OK || s.Value.Status == Status.CONNECTED) &&
                   now.AddSeconds(Timeout) > DateTime.Now)
            {
                await Task.Delay(WriteDelay * 1000);
            }

            if (_statuses.Any(s => s.Value.Status == Status.OK))
            {
                var port = _statuses.First(s => s.Value.Status == Status.OK).Key;
                await Connect(port, roc.Phone);
                return await Request(port, request);
            }

            if (_statuses.All(s => s.Value.Status != Status.CONNECTED))
                throw new Exception("Помилка виділення СОМ-порту. Порти відсутні або зайняті");

            if (_statuses.Any(s => s.Value.Status == Status.CONNECTED && s.Value.Phone.Equals(roc.Phone)))
            {
                var port = _statuses.First(s => s.Value.Status == Status.CONNECTED && s.Value.Phone.Equals(roc.Phone)).Key;
                return await Request(port, request);
            }
            else
            {
                var port = _statuses.First(s => s.Value.Status == Status.CONNECTED && !s.Value.Phone.Equals(roc.Phone)).Key;
                await Disconnect(port);
                await Connect(port, roc.Phone);
                return await Request(port, request);
            }
        }

        private async Task<byte[]> Request(SerialPort port, byte[] request)
        {
            return new byte[1024];
        }

        private async Task Disconnect(SerialPort port)
        {
            
        }

        private async Task Connect(SerialPort port, string phone)
        {
            
        }

        //var response = new byte[1024];

        //var retries = _retries;
        //var stream = _serialPort.BaseStream;
        //do
        //{
        //    await Task.Delay(_writeDelay);

        //    _serialPort.DiscardInBuffer();
        //    _serialPort.DiscardOutBuffer();

        //    _serialPort.Write(request, 0, request.Length);
        //    await Task.Delay(_readDelay);
        //    var task = stream.ReadAsync(response, 0, response.Length);
        //    await Task.WhenAny(task, Task.Delay(_timeout));

        //    if (response[0] != 0x00)
        //        break;

        //    _serialPort.DiscardInBuffer();

        //    //_serialPort.WriteLine(@"ATH0" + "\r\n");
        //    //await Task.Delay(_writeDelay);

        //    //_serialPort.Close();

        //    retries--;
        //} while (retries > 0);

        //return response;

        //public async Task<string> Connect()
        //{
        //    // Если порта нет в списке доступных - выход
        //    if (!SerialPort.GetPortNames().Contains(_port))
        //        return "";

        //    var response = new byte[1024];

        //    // Создание экземпляра последовательного порта
        //    SerialPortFixer.Execute(_port);
        //    _serialPort = new SerialPort
        //    {
        //        PortName = _port,
        //        BaudRate = _baudrate,
        //        DataBits = _dataBits,
        //        StopBits = _stopBits,
        //        Parity = _parity,
        //        Handshake = _handshake,
        //        WriteTimeout = _timeout,
        //        ReadTimeout = _timeout,
        //        DtrEnable = true
        //    };           

        //    // Если порт закрыт - открыть порт
        //    if (!_serialPort.IsOpen)
        //    {
        //        _serialPort.Open();
        //    }

        //    var retries = _retries;
        //    var stream = _serialPort.BaseStream;

        //    // Цикл опроса до исчерпания количества попыток
        //    do
        //    {
        //        // Активация модема 
        //        await Task.Delay(_writeDelay);
        //        _serialPort.WriteLine(@"AT" + "\r\n");
        //        await Task.Delay(_readDelay);
        //        var task = stream.ReadAsync(response, 0, response.Length);
        //        await Task.WhenAny(task, Task.Delay(_timeout));

        //        // Если статус не ОК - переход к следующей попытке
        //        var status = Encoding.ASCII.GetString(response);
        //        if (!status.Contains("OK"))
        //        {
        //            continue;
        //        }

        //        // Инициализация модема
        //        await Task.Delay(_writeDelay);
        //        _serialPort.WriteLine(@"AT&FE0V1X1&D2&C1S0=0" + "\r\n");
        //        await Task.Delay(_readDelay);
        //        task = stream.ReadAsync(response, 0, response.Length);
        //        await Task.WhenAny(task, Task.Delay(_timeout));

        //        // Если статус не ОК - переход к следующей попытке
        //        status = Encoding.ASCII.GetString(response);
        //        if (!status.Contains("OK"))
        //        {
        //            continue;
        //        }

        //        // Установление соединения
        //        await Task.Delay(_writeDelay);
        //        _serialPort.WriteLine($@"ATDT{_phone}" + "\r\n");
        //        await Task.Delay(_readDelay);
        //        task = stream.ReadAsync(response, 0, response.Length);
        //        await Task.WhenAny(task, Task.Delay(_timeout));

        //        // Если статус CONNECT  - выход из метода
        //        status = Encoding.ASCII.GetString(response);
        //        if (status.Contains("CONNECT"))
        //        {
        //            return "CONNECT";
        //        }

        //        // Декремент количества попыток
        //        retries--;
        //    } while (retries > 0);

        //    // Если порт открыт - закрыть порт
        //    if (_serialPort.IsOpen)
        //    {
        //        _serialPort.Close();
        //    }

        //    return "";
        //}

        public void Dispose()
        {
            foreach (var serialPort in _statuses.Keys)
            {
                if (serialPort.IsOpen)
                    serialPort.Close();

                serialPort.Dispose();
            }
        }
    }
}