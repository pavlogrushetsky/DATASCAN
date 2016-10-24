using System;
using DATASCAN.Communication.Common;

namespace DATASCAN.Communication.Protocols
{
    public static class RocPlusProtocol
    {
        public static byte[] GetRequestForPeriodicData(int rocUnit, int rocGroup, int hostUnit, int hostGroup, int histSegment,
            int pointNumber, RocHistoryType historyType)
        {
            var request = new byte[15];

            request[0] = (byte)rocUnit;
            request[1] = (byte)rocGroup;
            request[2] = (byte)hostUnit;
            request[3] = (byte)hostGroup;
            request[4] = 0x88;
            request[5] = 0x07;
            request[6] = (byte)histSegment;
            request[7] = 0x00;
            request[8] = 0x00;
            request[9] = (byte)historyType;
            request[10] = (byte)(pointNumber - 1);
            request[11] = 0x01;
            request[12] = 0x1e;

            var crc = BitConverter.GetBytes(Crc16.Compute(request));

            request[13] = crc[1];
            request[14] = crc[0];

            return request;
        }

        public static byte[] GetRequestForEventData(int rocUnit, int rocGroup, int hostUnit, int hostGroup, int histSegment, int pointNumber)
        {
            var request = new byte[11];

            request[0] = (byte)rocUnit;
            request[1] = (byte)rocGroup;
            request[2] = (byte)hostUnit;
            request[3] = (byte)hostGroup;
            request[4] = 0x77;
            request[5] = 0x03;
            request[6] = 0x0a;
            request[7] = 0x01;
            request[8] = 0x00;

            var crc = BitConverter.GetBytes(Crc16.Compute(request));

            request[9] = crc[1];
            request[10] = crc[0];

            return request;
        }

        public static byte[] GetRequestForAlarmData(int rocUnit, int rocGroup, int hostUnit, int hostGroup, int histSegment, int pointNumber)
        {
            var request = new byte[11];

            request[0] = (byte)rocUnit;
            request[1] = (byte)rocGroup;
            request[2] = (byte)hostUnit;
            request[3] = (byte)hostGroup;
            request[4] = 0x76;
            request[5] = 0x03;
            request[6] = 0x0a;
            request[7] = 0x00;
            request[8] = 0x00;

            var crc = BitConverter.GetBytes(Crc16.Compute(request));

            request[9] = crc[1];
            request[10] = crc[0];

            return request;
        }
    }
}