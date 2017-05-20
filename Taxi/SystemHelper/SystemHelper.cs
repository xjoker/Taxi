using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Taxi.SystemHelper
{
    public static class SystemHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern void Keybd_event(byte bVk, byte bScan, long dwFlags, long dwExtraInfo);
        public const byte KEYEVENTF_KEYUP = 0x0002;
        public const byte KEYEVENTF_EXTENDEDKEY = 0x0001;
        public const byte VK_LWIN = 0x5B;
        public const byte VK_RWIN = 0x5C;
        public const byte VK_CONTROL = 0x11;
        public const byte VK_LCONTROL = 0xA2;
        public const byte VK_RCONTROL = 0xA3;
        public const byte VK_SHIFT = 0x10;
        public const byte VK_LSHIFT = 0xA0;
        public const byte VK_RSHIFT = 0xA1;
        public const byte VK_ALT = 0x12;
        public const byte VK_LALT = 0xA4;
        public const byte VK_RALT = 0xA5;
    }
}
