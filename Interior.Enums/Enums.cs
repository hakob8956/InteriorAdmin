using System;
using System.Collections.Generic;
using System.Text;

namespace Interior.Enums
{
    public enum ResultCode : short
    {
        Success = 0,
        Error = 1
    }
    public enum FileType : byte
    {
        Image=0,
        AndroidBundle=1,
        IosBundle=2,
        Glb=3,
    }
}
