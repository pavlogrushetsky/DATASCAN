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
        public int WaitingTime { get; set; }

        public ModemLogEntry Status { get; private set; }

        private readonly Dictionary<string, ModemStatus> _statuses = new Dictionary<string,ModemStatus>();

        public event EventHandler StatusChanged;

        protected virtual void OnStatusChanged(EventArgs e)
        {
            var handler = StatusChanged;
            handler?.Invoke(this, e);
        }

        internal class ModemStatus
        {
            public SerialPort Port { get; set; }
            public string Phone { get; set; } = "";
            public PortStatus Status { get; set; }
        }

        internal enum PortStatus
        {
            OK,
            CONNECTING,
            CONNECTED
        }

        private void SetStatus(ModemLogEntry status)
        {
            Status = status;
            OnStatusChanged(EventArgs.Empty);
        }

        private void Validate(List<string> ports)
        {
            SetStatus(new ModemLogEntry { Message = "Перевірка портів", Status = Common.ModemStatus.INFO });
            ports.ForEach(port =>
            {
                Task.Run(async () =>
                {
                    if (!SerialPort.GetPortNames().Contains(port))
                    {
                        SetStatus(new ModemLogEntry { Message = "Порт відсутній у системі", Status = Common.ModemStatus.ERROR, Port = port });
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

                    if (!_statuses.ContainsKey(port))
                    {
                        _statuses.Add(port, new ModemStatus { Port = new SerialPort() });
                    }

                    var serialPort = _statuses[port].Port;

                    serialPort.PortName = port;
                    serialPort.BaudRate = Baudrate;
                    serialPort.DataBits = DataBits;
                    serialPort.StopBits = StopBits;
                    serialPort.Parity = Parity;
                    serialPort.Handshake = Handshake.None;
                    serialPort.WriteTimeout = Timeout*1000;
                    serialPort.ReadTimeout = Timeout*1000;
                    serialPort.DtrEnable = true;

                    if (!serialPort.IsOpen)
                    {
                        SetStatus(new ModemLogEntry { Message = "Відкриття порту", Status = Common.ModemStatus.INFO, Port = port });
                        serialPort.Open();

                        var response = new byte[1024];
                        var stream = serialPort.BaseStream;

                        serialPort.DiscardInBuffer();
                        serialPort.DiscardOutBuffer();

                        await Task.Delay(WriteDelay*1000);
                        SetStatus(new ModemLogEntry { Message = "AT", Status = Common.ModemStatus.SEND, Port = port });
                        serialPort.WriteLine(@"AT" + "\r\n");
                        await Task.Delay(ReadDelay*1000);
                        var task = stream.ReadAsync(response, 0, response.Length).ContinueWith(result =>
                        {
                            if (result.IsCompleted && !(result.IsFaulted || result.IsCanceled))
                                SetStatus(new ModemLogEntry { Message = $"Отримано {result.Result} байтів", Status = Common.ModemStatus.INFO, Port = port });
                        });
                        await Task.WhenAny(task, Task.Delay(Timeout*1000));

                        var status = Encoding.ASCII.GetString(response);
                        SetStatus(new ModemLogEntry { Message = $"{status}", Status = Common.ModemStatus.RECEIVE, Port = port });                       
                        if (status.Contains("OK"))
                        {
                            _statuses[port].Status = PortStatus.OK;
                        }
                    }
                    else
                    {
                        SetStatus(new ModemLogEntry { Message = "Порт відкрито", Status = Common.ModemStatus.INFO, Port = port });
                    }
                });
            });
        }

        public async Task Connect(string phone)
        {
            SetStatus(new ModemLogEntry { Message = $"Запит на встановлення зв'язку по телефону {phone}...", Status = Common.ModemStatus.INFO });
            var now = DateTime.Now;

            if (now.AddSeconds(WaitingTime) > DateTime.Now && _statuses.All(s => s.Value.Status != PortStatus.OK))
            {
                SetStatus(new ModemLogEntry { Message = $"Очікування з'єднання по телефону {phone}...", Status = Common.ModemStatus.WAIT });
            }           

            while (now.AddSeconds(WaitingTime) > DateTime.Now && _statuses.All(s => s.Value.Status != PortStatus.OK))
            {               
                await Task.Delay(1000);
            }

            var portName = _statuses.FirstOrDefault(s => s.Value.Status == PortStatus.OK).Key;

            if (string.IsNullOrEmpty(portName))
            {
                SetStatus(new ModemLogEntry { Message = $"Зв'язок по телефону {phone} не встановлено", Status = Common.ModemStatus.ERROR });
                throw new Exception("Помилка виділення СОМ-порту. Порти відсутні або зайняті");
            }

            SetStatus(new ModemLogEntry { Message = $"Встановлення зв'язку по телефону {phone}...", Status = Common.ModemStatus.CALLING, Port = portName});
            _statuses[portName].Phone = phone;
            _statuses[portName].Status = PortStatus.CONNECTING;

            var port = _statuses[portName].Port;

            if (!port.IsOpen)
            {
                SetStatus(new ModemLogEntry { Message = "Відкриття порту", Status = Common.ModemStatus.INFO, Port = port.PortName });
                port.Open();
            }

            SetStatus(new ModemLogEntry { Message = "Порт відкрито", Status = Common.ModemStatus.INFO, Port = port.PortName });

            var retries = Retries;
            var stream = port.BaseStream;

            do
            {
                var response = new byte[1024];

                port.DiscardInBuffer();
                port.DiscardOutBuffer();

                await Task.Delay(WriteDelay * 1000);
                SetStatus(new ModemLogEntry { Message = "AT&FE0V1X1&D2&C1S0=0", Status = Common.ModemStatus.SEND, Port = port.PortName });
                port.WriteLine(@"AT&FE0V1X1&D2&C1S0=0" + "\r\n");
                await Task.Delay(ReadDelay * 1000);
                var task = stream.ReadAsync(response, 0, response.Length).ContinueWith(result =>
                {
                    if (result.IsCompleted && !(result.IsFaulted || result.IsCanceled))
                        SetStatus(new ModemLogEntry { Message = $"Отримано {result.Result} байтів", Status = Common.ModemStatus.INFO, Port = port.PortName });
                });                 
                await Task.WhenAny(task, Task.Delay(Timeout * 1000));                

                var status = Encoding.ASCII.GetString(response);
                SetStatus(new ModemLogEntry { Message = $"{status}", Status = Common.ModemStatus.RECEIVE, Port = port.PortName });
                if (!status.Contains("OK"))
                {
                    continue;
                }               

                port.DiscardInBuffer();
                port.DiscardOutBuffer();

                await Task.Delay(WriteDelay * 1000);
                SetStatus(new ModemLogEntry { Message = $"ATDT{phone}", Status = Common.ModemStatus.SEND, Port = port.PortName });
                port.WriteLine($@"ATDT{phone}" + "\r\n");
                await Task.Delay(ReadDelay * 1000);
                task = stream.ReadAsync(response, 0, response.Length).ContinueWith(result =>
                {
                    if (result.IsCompleted && !(result.IsFaulted || result.IsCanceled))
                        SetStatus(new ModemLogEntry { Message = $"Отримано {result.Result} байтів", Status = Common.ModemStatus.INFO, Port = port.PortName });
                }); 
                await Task.WhenAny(task, Task.Delay(Timeout * 1000));

                status = Encoding.ASCII.GetString(response);
                SetStatus(new ModemLogEntry { Message = $"{status}", Status = Common.ModemStatus.RECEIVE, Port = port.PortName });
                if (status.Contains("CONNECT"))
                {
                    SetStatus(new ModemLogEntry { Message = $"Зв'язок по телефону {phone} встановлено", Status = Common.ModemStatus.CONNECTED });
                    _statuses[portName].Phone = phone;
                    _statuses[portName].Status = PortStatus.CONNECTED;
                    return;
                }

                retries--;
            } while (retries > 0);

            await Disconnect(phone);

            throw new Exception("Помилка виділення СОМ-порту. Таймаут встановлення з'єднання");
        }

        public async Task Disconnect(string phone)
        {
            SetStatus(new ModemLogEntry { Message = $"Завершення зв'язку по телефону {phone}...", Status = Common.ModemStatus.ENDCALL });
            var portName = _statuses.FirstOrDefault(s => s.Value.Status == PortStatus.CONNECTED && s.Value.Phone.Equals(phone)).Key;

            if (string.IsNullOrEmpty(portName))
                return;

            var port = _statuses[portName].Port;

            var stream = port.BaseStream;

            var response = new byte[1024];

            port.DiscardInBuffer();
            port.DiscardOutBuffer();

            await Task.Delay(WriteDelay * 1000);
            SetStatus(new ModemLogEntry { Message = "ATH0", Status = Common.ModemStatus.SEND, Port = port.PortName });
            port.WriteLine(@"ATH0" + "\r\n");
            await Task.Delay(ReadDelay * 1000);
            var task = stream.ReadAsync(response, 0, response.Length).ContinueWith(result =>
            {
                if (result.IsCompleted && !(result.IsFaulted || result.IsCanceled))
                    SetStatus(new ModemLogEntry { Message = $"Отримано {result.Result} байтів", Status = Common.ModemStatus.INFO, Port = port.PortName });
            }); 
            await Task.WhenAny(task, Task.Delay(Timeout * 1000));

            SetStatus(new ModemLogEntry { Message = $"Зв'язок по телефону {phone} завершено", Status = Common.ModemStatus.DISCONNECTED, Port = portName });

            if (port.IsOpen)
            {
                SetStatus(new ModemLogEntry { Message = "Порт закрито", Status = Common.ModemStatus.INFO, Port = port.PortName });
                port.Close();
            }

            _statuses[portName].Phone = "";
            _statuses[portName].Status = PortStatus.OK;
        }      

        public async Task<byte[]> GetData(Roc809 roc, byte[] request)
        {
            var portName = _statuses.FirstOrDefault(s => s.Value.Status == PortStatus.CONNECTED && s.Value.Phone.Equals(roc.Phone)).Key;

            if (string.IsNullOrEmpty(portName))
                return new byte[1024];

            var port = _statuses[portName].Port;

            var retries = Retries;
            var stream = port.BaseStream;            

            do
            {
                var response = new byte[1024];

                port.DiscardInBuffer();
                port.DiscardOutBuffer();

                await Task.Delay(WriteDelay * 1000);
                SetStatus(new ModemLogEntry { Message = BytesToString(request), Status = Common.ModemStatus.SEND, Port = port.PortName });
                port.Write(request, 0, request.Length);
                await Task.Delay(ReadDelay * 1000);
                var task = stream.ReadAsync(response, 0, response.Length).ContinueWith(result =>
                {
                    if (result.IsCompleted && !(result.IsFaulted || result.IsCanceled))
                        SetStatus(new ModemLogEntry { Message = $"Отримано {result.Result} байтів", Status = Common.ModemStatus.INFO, Port = port.PortName });
                });
                await Task.WhenAny(task, Task.Delay(Timeout * 1000));

                if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
                {
                    SetStatus(new ModemLogEntry { Message = BytesToString(response), Status = Common.ModemStatus.RECEIVE, Port = port.PortName });
                    return response;
                }

                retries--;
            } while (retries > 0);

            await Disconnect(roc.Phone);

            throw new Exception("Таймаут читання даних через GPRS");
        }

        private static string BytesToString(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", " ").ToUpper();
        }

        public void Dispose()
        {
            foreach (var port in _statuses.Keys.Select(serialPort => _statuses[serialPort].Port))
            {
                if (port.IsOpen)
                    port.Close();

                port.Dispose();
            }
        } 
    }
}