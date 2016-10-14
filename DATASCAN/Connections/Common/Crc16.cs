namespace DATASCAN.Connections.Common
{
    public static class Crc16
    {
        const ushort polynomial = 0xA001;

        static readonly ushort[] lookupTable = new ushort[256];

        public static ushort Compute(byte[] bytes)
        {
            ushort crc = 0;
            foreach (byte t in bytes)
            {
                byte index = (byte)(crc ^ t);
                crc = (ushort)((crc >> 8) ^ lookupTable[index]);
            }
            return crc;
        }

        static Crc16()
        {
            for (ushort i = 0; i < lookupTable.Length; ++i)
            {
                ushort value = 0;
                var temp = i;
                for (byte j = 0; j < 8; ++j)
                {
                    if (((value ^ temp) & 0x0001) != 0)
                    {
                        value = (ushort)((value >> 1) ^ polynomial);
                    }
                    else
                    {
                        value >>= 1;
                    }
                    temp >>= 1;
                }
                lookupTable[i] = value;
            }
        }
    }
}
