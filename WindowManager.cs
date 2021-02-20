using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Text;
using NespWin32Library.Layout;
using NespSdkNetFramework.Utils;

namespace NespWin32Library
{
    public sealed class WindowManager
    {

        private static readonly string TAG = "WindowManager";


        /// <summary>
        /// Minimizes a window, even if the thread that owns the window is not responding. 
        /// This flag should only be used when minimizing windows from a different thread.
        /// </summary>
        public const int SW_FORCEMINIMIZE = 11;

        /// <summary>
        /// Hides the window and activates another window.
        /// </summary>
        public const int SW_HIDE = 0;

        /// <summary>
        /// Maximizes the specified window.
        /// </summary>
        public const int SW_MAXIMIZE = 3;

        /// <summary>
        /// Minimizes the specified window and activates the next top-level window in the Z order.
        /// </summary>
        public const int SW_MINIMIZE = 6;

        /// <summary>
        /// Activates and displays the window. If the window is minimized or maximized,
        /// the system restores it to its original size and position.
        /// An application should specify this flag when restoring a minimized window.
        /// </summary>
        public const int SW_RESTORE = 9;

        /// <summary>
        /// Activates the window and displays it in its current size and position.
        /// </summary>
        public const int SW_SHOW = 5;

        /// <summary>
        /// Sets the show state based on the SW_ value specified in the STARTUPINFO 
        /// structure passed to the CreateProcess function by the program that started the application.
        /// </summary>
        public const int SW_SHOWDEFAULT = 10;

        /// <summary>
        /// Activates the window and displays it as a maximized window.
        /// </summary>
        public const int SW_SHOWMAXIMIZED = 3;

        /// <summary>
        /// Activates the window and displays it as a minimized window.
        /// </summary>
        public const int SW_SHOWMINIMIZED = 2;

        /// <summary>
        /// Displays the window as a minimized window. This value is similar to SW_SHOWMINIMIZED, 
        /// except the window is not activated.
        /// </summary>
        public const int SW_SHOWMINNOACTIVE = 7;

        /// <summary>
        /// Displays the window in its current size and position. This value is similar to SW_SHOW,
        /// except that the window is not activated.
        /// </summary>
        public const int SW_SHOWNA = 8;

        /// <summary>
        /// Displays a window in its most recent size and position. This value is similar to SW_SHOWNORMAL, 
        /// except that the window is not activated.
        /// </summary>
        public const int SW_SHOWNOACTIVATE = 4;

        /// <summary>
        /// Activates and displays a window. If the window is minimized or maximized, 
        /// the system restores it to its original size and position.
        /// An application should specify this flag when displaying the window for the first time.
        /// </summary>
        public const int SW_SHOWNORMAL = 1;


        /// <summary>
        /// 系统获取当前焦点窗口
        /// </summary>
        /// <returns></returns>
        [DllImport(Win32DLL.USER32, CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <param name="hWnd">要操作的窗口</param>
        /// <param name="nCmdShow">0：关闭窗口，1：正常大小显示窗口，2：最小化窗口，3：最大化窗口</param>
        /// <returns></returns>
        [DllImport(Win32DLL.USER32, CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        [DllImport(Win32DLL.USER32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);


        [DllImport(Win32DLL.USER32)]
        public static extern IntPtr FindWindowW(string lpClassName, string lpWindowName);


        [DllImport(Win32DLL.USER32)]
        public static extern IntPtr FindWindowExW(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport(Win32DLL.USER32)]
        public static extern IntPtr GetFocus();

        [DllImport(Win32DLL.USER32)]
        public static extern void SetForegroundWindow(IntPtr hWnd);

        [DllImport(Win32DLL.KERNEL32)]
        public static extern UInt32 GetCurrentThreadId();

        [DllImport(Win32DLL.USER32)]
        public static extern UInt32 GetWindowThreadProcessId(IntPtr hWnd, IntPtr lpdwProcessId);

        [DllImport(Win32DLL.USER32)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        public static extern Int32 AttachThreadInput(UInt32 idAttach, UInt32 idAttachTo, Int32 fAttach);



        public const Int32 WM_KEYDOWN = 0X100;
        public const Int32 WM_KEYUP = 0X101;
        public const Int32 WM_SYSCHAR = 0X106;
        public const Int32 WM_SYSKEYUP = 0X105;
        public const Int32 WM_SYSKEYDOWN = 0X104;
        public const Int32 WM_CHAR = 0X102;
        public const Int32 WM_IME_CHAR = 0x0286;

        public const Int32 WM_SETTEXT = 0x000C;//设置文本框内容的消息
        public const Int32 BM_CLICK = 0xF5; //鼠标点击的消息，对于各种消息的数值，你还是得去查查API手册



        [DllImport(Win32DLL.USER32)]
        public static extern int PostMessageA(IntPtr hWnd, Int32 Msg, int wParam, int lParam);

        [DllImport(Win32DLL.USER32)]
        public static extern int PostMessageA(IntPtr hWnd, Int32 Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);


        [DllImport(Win32DLL.USER32)]
        public static extern int SendMessage(IntPtr hWnd, Int32 Msg, int wParam, int lParam);

        [DllImport(Win32DLL.USER32)]
        public static extern int SendMessage(IntPtr hWnd, Int32 Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        [DllImport(Win32DLL.USER32)]
        public static extern int SendMessageW(IntPtr hWnd, Int32 Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        public static bool SendStringToFocus(string str)
        {
            Log.I(TAG, "SendStringToFocus:  str = " + str);

            IntPtr foregroundWindow = GetForegroundWindow();
            if (foregroundWindow == IntPtr.Zero)
            {
                Log.I(TAG, "SendStringToFocus: foregroundWindow is NULL return");
                return false;
            }

            Log.I(TAG, "SendStringToFocus: foregroundWindow is " + foregroundWindow);

            uint currentThreadId = GetCurrentThreadId();
            Log.I(TAG, "SendStringToFocus: CurrentThreadId = " + currentThreadId);

            uint foregroundWindowThreadId = GetWindowThreadProcessId(foregroundWindow, IntPtr.Zero);
            Log.I(TAG, "SendStringToFocus: ForegroundWindowThreadId  = " + foregroundWindowThreadId);

            int attachThreadInput = AttachThreadInput(foregroundWindowThreadId, currentThreadId, 1);
            Log.I(TAG, "SendStringToFocus: AttachThreadInput  = " + attachThreadInput);

            IntPtr focus = GetFocus();
            Log.I(TAG, "SendStringToFocus: focus  = " + focus);

            int dettachThreadInput = AttachThreadInput(foregroundWindowThreadId, currentThreadId, 0);
            Log.I(TAG, "SendStringToFocus: DettachThreadInput  = " + dettachThreadInput);

            /// Click TAB key
            //int postMessageA = PostMessageA(focus, WM_SYSKEYDOWN, 0x09, 0);
            //PostMessageA(focus, WM_SYSKEYUP, 0x09, 0);
            //Log.I(TAG, "SendStringToFocus: PostMessageA  = " + postMessageA);

            //int sendMessage = SendMessageW(focus, WM_SETTEXT, 0, str);
            //Log.I(TAG, "SendStringToFocus: SendMessageW  = " + sendMessage);

            InputStr(focus,str);
            //SendMessage(foregroundWindow,);
            return true;
        }

        /// <summary>
        /// 发送一个字符串
        /// 不支持中文
        /// </summary>
        /// <param name="myIntPtr">窗口句柄</param>
        /// <param name="Input">字符串</param>
        public static void InputStr(IntPtr myIntPtr, string Input)
        {
            byte[] ch = (Encoding.UTF8.GetBytes(Input));
            for (int i = 0; i < ch.Length; i++)
            {
                SendMessage(myIntPtr, WM_IME_CHAR, ch[i], 0);
            }
        }

    }
}
