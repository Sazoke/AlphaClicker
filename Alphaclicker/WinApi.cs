using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace AlphaClicker
{
    public class WinApi
    {
        
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(int character);

        [DllImport("user32.dll")]
        public static extern int SetCursorPos(int x, int y);

        // Get custom coords
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            return lpPoint;
        }

        public static void DoClick(string button, bool useCustomCoords, int X, int Y, bool @long = false)
        {
            if (useCustomCoords) { SetCursorPos(X, Y); }

            var @event = (uint)MOUSEEVENTF.LEFTDOWN;
            switch (button)
            {
                case "Left":
                    @event = (uint)MOUSEEVENTF.LEFTDOWN;
                    break;
                case "Right":
                    @event = (uint)MOUSEEVENTF.RIGHTDOWN;
                    break;
                case "Middle":
                    @event = (uint)MOUSEEVENTF.MIDDLEDOWN;
                    break;
            }

            if (@long)
                mouse_event(@event, 0, 0, 0, 0);
            else
                mouse_event(@event | (@event * 2), 0, 0, 0, 0);
        }

        public static void UpClick(string button, bool useCustomCoords, int X, int Y)
        {
            if (useCustomCoords) { SetCursorPos(X, Y); }
            
            var @event = (uint)MOUSEEVENTF.LEFTDOWN;
            switch (button)
            {
                case "Left":
                    @event = (uint)MOUSEEVENTF.LEFTDOWN;
                    break;
                case "Right":
                    @event = (uint)MOUSEEVENTF.RIGHTDOWN;
                    break;
                case "Middle":
                    @event = (uint)MOUSEEVENTF.MIDDLEDOWN;
                    break;
            }

            mouse_event(@event * 2, 0, 0, 0, 0);
        }
    }
}
