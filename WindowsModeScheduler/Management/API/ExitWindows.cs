using System;

namespace WindowsModeScheduler.BL.Management.API
{
    [Flags]
    internal enum ExitWindows : UInt32
    {
        // ONE of the following five:
        EWX_LOGOFF = 0,
        EWX_SHUTDOWN = 0x00000001,
        EWX_REBOOT = 0x00000002,
        EWX_POWEROFF = 0x00000008,
        EWX_RESTARTAPPS = 0x00000040,
        EWX_HYBRID_SHUTDOWN = 0x00400000,

        // plus AT MOST ONE of the following two:
        EWX_FORCE = 0x00000004,
        EWX_FORCEIFHUNG = 0x00000010
    }
}
