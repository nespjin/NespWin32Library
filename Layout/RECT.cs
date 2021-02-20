using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NespWin32Library.Layout
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        /// <summary>
        /// 最左坐标
        /// </summary>
        public int Left;
        
        /// <summary>
        /// 最上坐标
        /// </summary>
        public int Top;

        /// <summary>
        /// 最右坐标
        /// </summary>
        public int Right;

        /// <summary>
        /// 最下坐标
        /// </summary>
        public int Bottom;
    }
}
