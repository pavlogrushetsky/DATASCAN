using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading.Tasks;

namespace DATASCAN.Communication.Clients
{
    public class GprsClient : IClient, IDisposable
    {
        public List<string> Ports { get; set; }
        public int Baudrate { get; set; }
        public Parity Parity { get; set; } 
        public int DataBits { get; set; }
        public StopBits StopBits { get; set; }
        public int Timeout { get; set; }
        public int Retries { get; set; }
        public int WriteDelay { get; set; }
        public int ReadDelay { get; set; }

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

        public async Task<byte[]> GetData(byte[] request)
        {
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

            return new byte[1024];
        }

        public void Dispose()
        {
            //if (_serialPort == null || !_serialPort.IsOpen) return;

            //_serialPort.Close();
            //_serialPort.Dispose();
        }
    }
}