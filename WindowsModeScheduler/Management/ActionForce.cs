using System;

namespace WindowsModeScheduler.BL.Management
{
    public enum ActionForce : UInt32
    {
        Default = 0,
        Force = 0x00000004,
        ForceIfHung = 0x00000010
    }
}
