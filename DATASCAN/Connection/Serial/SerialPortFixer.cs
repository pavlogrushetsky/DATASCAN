using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace DATASCAN.Connection.Serial
{
    public class SerialPortFixer : IDisposable
    {
        public static void Execute(string portName)
        {
            using (new SerialPortFixer(portName))
            {
            }
        }
        #region IDisposable Members

        public void Dispose()
        {
            if (m_Handle == null) return;
            m_Handle.Close();
            m_Handle = null;
        }

        #endregion

        #region Implementation

        private const int DcbFlagAbortOnError = 14;
        private const int CommStateRetries = 10;
        private SafeFileHandle m_Handle;

        private SerialPortFixer(string portName)
        {
            const int dwFlagsAndAttributes = 0x40000000;
            const int dwAccess = unchecked((int)0xC0000000);

            if ((portName == null) || !portName.StartsWith("COM", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Invalid Serial Port", "portName");
            }
            var hFile = CreateFile(@"\\.\" + portName, dwAccess, 0, IntPtr.Zero, 3, dwFlagsAndAttributes, IntPtr.Zero);
            if (hFile.IsInvalid)
            {
                WinIoError();
            }
            try
            {
                var fileType = GetFileType(hFile);
                if ((fileType != 2) && (fileType != 0))
                {
                    throw new ArgumentException("Invalid Serial Port", "portName");
                }
                m_Handle = hFile;
                InitializeDcb();
            }
            catch
            {
                hFile.Close();
                m_Handle = null;
                throw;
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int FormatMessage(int dwFlags, HandleRef lpSource, int dwMessageId, int dwLanguageId,
                                                StringBuilder lpBuffer, int nSize, IntPtr arguments);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetCommState(SafeFileHandle hFile, ref Dcb lpDcb);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetCommState(SafeFileHandle hFile, ref Dcb lpDcb);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ClearCommError(SafeFileHandle hFile, ref int lpErrors, ref Comstat lpStat);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern SafeFileHandle CreateFile(string lpFileName, int dwDesiredAccess, int dwShareMode,
                                                        IntPtr securityAttrs, int dwCreationDisposition,
                                                        int dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int GetFileType(SafeFileHandle hFile);

        private void InitializeDcb()
        {
            var dcb = new Dcb();
            GetCommStateNative(ref dcb);
            dcb.Flags &= ~(1u << DcbFlagAbortOnError);
            SetCommStateNative(ref dcb);
        }

        private static string GetMessage(int errorCode)
        {
            var lpBuffer = new StringBuilder(0x200);
            return FormatMessage(0x3200, new HandleRef(null, IntPtr.Zero), errorCode, 0, lpBuffer, lpBuffer.Capacity, IntPtr.Zero) != 0 ? lpBuffer.ToString() : "Unknown Error";
        }

        private static int MakeHrFromErrorCode(int errorCode)
        {
            return (int)(0x80070000 | (uint)errorCode);
        }

        private static void WinIoError()
        {
            var errorCode = Marshal.GetLastWin32Error();
            throw new IOException(GetMessage(errorCode), MakeHrFromErrorCode(errorCode));
        }

        private void GetCommStateNative(ref Dcb lpDcb)
        {
            var commErrors = 0;
            var comStat = new Comstat();

            for (var i = 0; i < CommStateRetries; i++)
            {
                if (!ClearCommError(m_Handle, ref commErrors, ref comStat))
                {
                    WinIoError();
                }
                if (GetCommState(m_Handle, ref lpDcb))
                {
                    break;
                }
                if (i == CommStateRetries - 1)
                {
                    WinIoError();
                }
            }
        }

        private void SetCommStateNative(ref Dcb lpDcb)
        {
            var commErrors = 0;
            var comStat = new Comstat();

            for (var i = 0; i < CommStateRetries; i++)
            {
                if (!ClearCommError(m_Handle, ref commErrors, ref comStat))
                {
                    WinIoError();
                }
                if (SetCommState(m_Handle, ref lpDcb))
                {
                    break;
                }
                if (i == CommStateRetries - 1)
                {
                    WinIoError();
                }
            }
        }

        #region Nested type: COMSTAT

        [StructLayout(LayoutKind.Sequential)]
        private struct Comstat
        {
            private readonly uint Flags;
            private readonly uint cbInQue;
            private readonly uint cbOutQue;
        }

        #endregion

        #region Nested type: DCB

        [StructLayout(LayoutKind.Sequential)]
        private struct Dcb
        {
            private readonly uint DCBlength;
            private readonly uint BaudRate;
            public uint Flags;
            private readonly ushort wReserved;
            private readonly ushort XonLim;
            private readonly ushort XoffLim;
            private readonly byte ByteSize;
            private readonly byte Parity;
            private readonly byte StopBits;
            private readonly byte XonChar;
            private readonly byte XoffChar;
            private readonly byte ErrorChar;
            private readonly byte EofChar;
            private readonly byte EvtChar;
            private readonly ushort wReserved1;
        }

        #endregion

        #endregion
    }
}