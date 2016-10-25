using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DATASCAN.Communication.Clients;
using DATASCAN.Communication.Common;
using DATASCAN.Core.Entities.Rocs;

namespace DATASCAN.Communication.Protocols
{
    public class RocPlusProtocol
    {
        public List<ROC809PeriodicDataModel> GetPeriodicData(Roc809 roc, Roc809MeasurePoint point, RocHistoryType historyType, IClient client)
        {
            var request = new byte[15];

            request[0] = (byte)roc.RocUnit;
            request[1] = (byte)roc.RocGroup;
            request[2] = (byte)roc.HostUnit;
            request[3] = (byte)roc.HostGroup;
            request[4] = 0x88;
            request[5] = 0x07;
            request[6] = (byte)point.HistSegment;
            request[7] = 0x00;
            request[8] = 0x00;
            request[9] = (byte)historyType;
            request[10] = (byte)(point.Number - 1);
            request[11] = 0x01;
            request[12] = 0x1e;

            var crc = BitConverter.GetBytes(Crc16.Compute(request));

            request[13] = crc[1];
            request[14] = crc[0];

            int startIndex = request.GetInt16(7);
            int totalIndex;

            var data = new List<ROC809PeriodicDataModel>();

            do
            {
                var response = client.GetData(request);

                totalIndex = historyType == RocHistoryType.Minute ? 60 : response.GetInt16(9);

                int recordsToProcess;

                if (historyType == RocHistoryType.Minute)
                    recordsToProcess = response[11] / 2 >= 30 ? 30 : response[11] / 2;
                else
                    recordsToProcess = totalIndex - startIndex >= 30 ? 30 : totalIndex - startIndex;

                for (var j = 0; j < recordsToProcess; j++)
                {
                    var offset = j * 8;

                    var period = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    double value = 0;

                    for (var k = 0; k < 2; k++)
                    {
                        var dataOffset = k * 4 + offset;

                        if (k == 0)
                            period = period.AddSeconds(response.GetUInt32(12 + dataOffset));
                        else
                            value = response.GetSingle(12 + dataOffset);
                    }

                    data.Add(new ROC809PeriodicDataModel { DatePeriod = period, Value = value });
                }

                startIndex = BitConverter.ToInt32(BitConverter.GetBytes(startIndex + 30), 0);

                request[7] = BitConverter.GetBytes(startIndex)[0];
                request[8] = BitConverter.GetBytes(startIndex)[1];

                crc = BitConverter.GetBytes(Crc16.Compute(request));

                request[13] = crc[1];
                request[14] = crc[0];

            } while (startIndex < totalIndex);

            return data.Distinct().ToList();
        }

        public List<Roc809EventData> GetEventData(Roc809 roc, IClient client)
        {
            var request = new byte[11];

            request[0] = (byte)roc.RocUnit;
            request[1] = (byte)roc.RocGroup;
            request[2] = (byte)roc.HostUnit;
            request[3] = (byte)roc.HostGroup;
            request[4] = 0x77;
            request[5] = 0x03;
            request[6] = 0x0a;
            request[7] = 0x01;
            request[8] = 0x00;

            var crc = BitConverter.GetBytes(Crc16.Compute(request));

            request[9] = crc[1];
            request[10] = crc[0];

            int startIndex = request.GetInt16(7);
            int totalIndex;

            var data = new List<Roc809EventData>();

            do
            {
                var response = client.GetData(request);

                totalIndex = response.GetInt16(9);
                var eventsToProcess = totalIndex - startIndex >= 10 ? 10 : totalIndex - startIndex;

                for (var i = 0; i < eventsToProcess; i++)
                {
                    var offset = i * 22;

                    var record = new Roc809EventData();
                    var time = new DateTime(1970, 1, 1, 0, 0, 0, 0);

                    record.Type = response[11 + offset];


                    record.Time = time.AddSeconds(response.GetUInt32(12 + offset));

                    switch (record.Type)
                    {
                        case 0:
                            break;
                        case 1:
                            record.OperatorId = response.GetASCII(16 + offset, 3);

                            record.T = response[19 + offset];
                            record.L = response[20 + offset];
                            record.P = response[21 + offset];

                            switch (response[22 + offset])
                            {
                                case 0:
                                    record.NewValue = response.GetBool(23 + offset) ? "1" : "0";
                                    record.OldValue = response.GetBool(27 + offset) ? "1" : "0";
                                    break;
                                case 1:
                                    record.NewValue = response[23 + offset].ToString();
                                    record.OldValue = response[27 + offset].ToString();
                                    break;
                                case 2:
                                    record.NewValue = response.GetInt16(23 + offset).ToString();
                                    record.OldValue = response.GetInt16(27 + offset).ToString();
                                    break;
                                case 3:
                                    record.NewValue = response.GetInt32(23 + offset).ToString();
                                    record.OldValue = response.GetInt32(27 + offset).ToString();
                                    break;
                                case 4:
                                    record.NewValue = response[23 + offset].ToString();
                                    record.OldValue = response[27 + offset].ToString();
                                    break;
                                case 5:
                                    record.NewValue = response.GetUInt16(23 + offset).ToString();
                                    record.OldValue = response.GetUInt16(27 + offset).ToString();
                                    break;
                                case 6:
                                    record.NewValue = response.GetUInt32(23 + offset).ToString();
                                    record.OldValue = response.GetUInt32(27 + offset).ToString();
                                    break;
                                case 7:
                                    record.NewValue = response.GetSingle(23 + offset).ToString(CultureInfo.InvariantCulture);
                                    record.OldValue = response.GetSingle(27 + offset).ToString(CultureInfo.InvariantCulture);
                                    break;
                                case 8:
                                    record.NewValue = response.GetTLP(23 + offset);
                                    record.OldValue = response.GetTLP(27 + offset);
                                    break;
                                case 9:
                                    record.NewValue = response.GetASCII(23 + offset, 3);
                                    record.OldValue = response.GetASCII(27 + offset, 3);
                                    break;
                                case 10:
                                    record.NewValue = response.GetASCII(23 + offset, 7);
                                    break;
                                case 11:
                                    record.NewValue = response.GetASCII(23 + offset, 10);
                                    break;
                                case 12:
                                    record.NewValue = response.GetASCII(23 + offset, 10);
                                    break;
                                case 13:
                                    record.NewValue = response.GetASCII(23 + offset, 10);
                                    break;
                                case 14:
                                    record.NewValue = response.GetASCII(23 + offset, 10);
                                    break;
                                case 15:
                                    record.NewValue = response.GetASCII(23 + offset, 10);
                                    break;
                                case 16:
                                    record.NewValue = response.GetDouble(23 + offset).ToString(CultureInfo.InvariantCulture);
                                    break;
                                case 17:
                                    record.NewValue = time.AddSeconds(response.GetUInt32(23 + offset)).ToString("dd.MM.yyyy HH:mm:ss");
                                    record.OldValue = time.AddSeconds(response.GetUInt32(27 + offset)).ToString("dd.MM.yyyy HH:mm:ss");
                                    break;
                            }
                            break;
                        case 2:
                            record.Code = response[16 + offset];
                            record.Description = response.GetString(17 + offset, 16);
                            break;
                        case 3:
                            record.FST = response[16 + offset];
                            record.Value = response.GetSingle(17 + offset).ToString(CultureInfo.InvariantCulture);
                            break;
                        case 4:
                            record.OperatorId = response.GetASCII(16 + offset, 3);
                            record.Code = response[19 + offset];
                            record.Description = response.GetString(20 + offset, 13);
                            break;
                        case 5:
                            record.Value = time.AddSeconds(response.GetUInt32(16 + offset)).ToString("dd.MM.yyyy HH:mm:ss");
                            break;
                        case 6:
                            record.Value = time.AddSeconds(response.GetUInt32(16 + offset)).ToString("dd.MM.yyyy HH:mm:ss");
                            break;
                        case 7:
                            record.OperatorId = response.GetASCII(16 + offset, 3);
                            record.T = response[19 + offset];
                            record.L = response[20 + offset];
                            record.P = response[21 + offset];
                            record.RawValue = response.GetSingle(22 + offset).ToString(CultureInfo.InvariantCulture);
                            record.CalibratedValue = response.GetSingle(26 + offset).ToString(CultureInfo.InvariantCulture);
                            break;
                    }

                    data.Add(record);
                }

                startIndex = BitConverter.ToInt32(BitConverter.GetBytes(startIndex + 10), 0);

                request[7] = BitConverter.GetBytes(startIndex)[0];
                request[8] = BitConverter.GetBytes(startIndex)[1];

                crc = BitConverter.GetBytes(Crc16.Compute(request));

                request[9] = crc[1];
                request[10] = crc[0];

            } while (startIndex < totalIndex);

            return data;
        }

        public List<Roc809AlarmData> GetAlarmData(Roc809 roc, IClient client)
        {
            var request = new byte[11];
            var data = new List<Roc809AlarmData>();

            request[0] = (byte)roc.RocUnit;
            request[1] = (byte)roc.RocGroup;
            request[2] = (byte)roc.HostUnit;
            request[3] = (byte)roc.HostGroup;
            request[4] = 0x76;
            request[5] = 0x03;
            request[6] = 0x0a;
            request[7] = 0x00;
            request[8] = 0x00;

            var crc = BitConverter.GetBytes(Crc16.Compute(request));

            request[9] = crc[1];
            request[10] = crc[0];

            int startIndex = request.GetInt16(7);
            int totalIndex;
            do
            {
                var response = client.GetData(request);

                totalIndex = response.GetInt16(9);
                var alarmsToProcess = totalIndex - startIndex >= 10 ? 10 : totalIndex - startIndex;

                for (var i = 0; i < alarmsToProcess; i++)
                {
                    var offset = i * 23;

                    var record = new Roc809AlarmData();
                    var time = new DateTime(1970, 1, 1, 0, 0, 0, 0);

                    record.SRBX = (response[11 + offset] & (1 << 7)) != 0;
                    record.Condition = (response[11 + offset] & (1 << 6)) != 0;
                    record.Type = response[11 + offset] & 63;

                    record.Time = time.AddSeconds(response.GetUInt32(12 + offset));

                    switch (record.Type)
                    {
                        case 0:
                            break;
                        case 1:
                            record.Code = response[16 + offset];
                            record.T = response[17 + offset];
                            record.L = response[18 + offset];
                            record.P = response[19 + offset];
                            record.Description = response.GetString(20 + offset, 10);
                            record.Value = response.GetSingle(30 + offset).ToString(CultureInfo.InvariantCulture);
                            break;
                        case 2:
                            record.FST = response[16 + offset];
                            record.Description = response.GetString(17 + offset, 13);
                            record.Value = response.GetSingle(30 + offset).ToString(CultureInfo.InvariantCulture);
                            break;
                        case 3:
                            record.Description = response.GetString(16 + offset, 18);
                            break;
                        case 4:
                            record.Description = response.GetString(16 + offset, 14);
                            record.Value = response.GetSingle(30 + offset).ToString(CultureInfo.InvariantCulture);
                            break;
                    }

                    data.Add(record);
                }

                startIndex = BitConverter.ToInt32(BitConverter.GetBytes(startIndex + 10), 0);

                request[7] = BitConverter.GetBytes(startIndex)[0];
                request[8] = BitConverter.GetBytes(startIndex)[1];

                crc = BitConverter.GetBytes(Crc16.Compute(request));

                request[9] = crc[1];
                request[10] = crc[0];

            } while (startIndex < totalIndex);

            return data;
        }
    }
}