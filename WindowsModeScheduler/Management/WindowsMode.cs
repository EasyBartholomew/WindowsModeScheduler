using System;

namespace WindowsModeScheduler.BL.Management
{
    public enum WindowsMode : UInt32
    {
        LogOff = 0,
        ShutDown = 0x00000001,
        Reboot = 0x00000002,
        Sleep = 0x00000008,
        Lock = 0x00000010,
        Hibernation = 0x00400000,
        FastHibernation = Hibernation | ShutDown
    }
}
