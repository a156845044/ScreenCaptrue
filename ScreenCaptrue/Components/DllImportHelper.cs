using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ScreenCaptrue
{
    public static class DllImportHelper
    {
        /// <summary>
        /// V1.0版本截图
        /// </summary>
        /// <returns></returns>

        [DllImport("PrScrn.dll", EntryPoint = "PrScrn")]
        public extern static int PrScrn();//与dll中一致   

        /// <summary>
        /// V2.0版本截图
        /// </summary>
        /// <returns></returns>
        [DllImport("PrScrnNew.dll", EntryPoint = "RunSnap")]
        public extern static int RunSnap();//与dll中一致   
    }
}
