using NespWin32Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Win32Library
{
    public class WindowGdi
    {
        [DllImport(Win32DLL.GDI32, SetLastError = true)]
        public static extern bool DeleteObject(IntPtr hObject);
    }
}
