using System;
using System.Timers;

namespace WindowsModeScheduler.BL.Time
{
    public interface ITimer : IDisposable
    {
        event ElapsedEventHandler Elapsed;

        TimerState TimerState { get; }

        Boolean IsEnabled { get; }

        Boolean AutoReset { get; set; }

        Double Interval { get; set; }

        Double TimeRemaining { get; }

        void Start();

        void Stop();

        void Pause();

        void Resume();

        void Refresh();
    }
}
