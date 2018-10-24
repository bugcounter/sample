using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TestWin
{
    class BCWin
    {
        public BCWin()
        {
        }

        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_SHOWWINDOW = 0x0040;

        public static List<IntPtr> BCFindWindow(string wildcard)
        {
            List<IntPtr> hList = new List<IntPtr>();
            foreach (Process p in Process.GetProcesses())
            {
                if (p.MainWindowTitle.Contains(wildcard))
                {
                    Console.WriteLine("{0} {1} {2}", p.Id, p.MainWindowHandle, p.MainWindowTitle);
                    Console.WriteLine("{0}", FindWindow(null, p.MainWindowTitle));
                    RECT rect;
                    GetWindowRect(FindWindow(null, p.MainWindowTitle), out rect);
                    Console.WriteLine("{0} {1} {2} {3}", rect.Left, rect.Upper, rect.Right, rect.Bottom);
                    hList.Add(p.MainWindowHandle);
                }
            }

            return hList;
        }

        public static void BCSetWindowPos(IntPtr hWnd, int x, int y)
        {
            SetWindowPos(hWnd, HWND_NOTOPMOST, x, y, 0, 0, SWP_SHOWWINDOW | SWP_NOSIZE);
            //SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
        }

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hWnd1, IntPtr hWnd2, string lpsz1, string lpsz2);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Upper;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }
    }
}
