using System;

namespace WindowsModeScheduler.BL.Time
{
    public enum TimerState : Byte
    {
        NeverActivated = 0,
        InProgress = 1,
        Stopped = 2,
        Paused = 3
    }
}
