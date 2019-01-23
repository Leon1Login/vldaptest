using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace vldaptest
{
    class OSHelper
    {
        public static bool IsWindows()
        {
            return System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }
    }
}
