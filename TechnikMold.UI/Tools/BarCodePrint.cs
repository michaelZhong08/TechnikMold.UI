using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MoldManager.WebUI.Tools
{
    public class BarCodePrint
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct OVERLAPPED
        {
            int Internal;
            int InternalHigh;
            int Offset;
            int OffSetHigh;
            int hEvent;
        }

        [DllImport("kernel32.dll")]
        private static extern int CreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode, int lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);
        [DllImport("kernel32.dll")]
        private static extern bool WriteFile(int hFile, byte[] lpBuffer, int nNumberOfBytesToWriter, out int lpNumberOfBytesWriten, out OVERLAPPED lpOverLapped);
        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(int hObject);
        [DllImport("fnthex32.dll")]
        public static extern int GETFONTHEX(string barcodeText, string fontName, int orient, int height, int width, int isBold, int isItalic, StringBuilder returnBarcodeCMD);
        private int iHandle;
        //打开LPT 端口  
        public bool Open()
        {
            iHandle = CreateFile("lpt1", 0x40000000, 0, 0, 3, 0, 0);
            if (iHandle != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Write(string MyString)
        {
            if (iHandle != -1)
            {
                int i;
                OVERLAPPED x;
                byte[] mybyte = System.Text.Encoding.Default.GetBytes(MyString);
                return WriteFile(iHandle, mybyte, mybyte.Length, out i, out x);
            }
            else
            {
                throw new Exception("端口未打开~！");
            }
        }
        //关闭打印端口  
        public bool Close()
        {
            return CloseHandle(iHandle);
        }  
    }
}