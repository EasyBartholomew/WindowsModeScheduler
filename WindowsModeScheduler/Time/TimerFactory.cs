using System;

namespace WindowsModeScheduler.BL.Time
{
    public static class TimerFactory
    {
        public static ITimer FromMilliseconds(double interval, bool startImmediately)
        {
            return new ExtendedTimer(interval, startImmediately);
        }

        public static ITimer FromTimeSpan(TimeSpan interval, bool startImmediately)
        {
            return new ExtendedTimer(interval.TotalSeconds, startImmediately);
        }

        public static ITimer FromMilliseconds(double interval)
        {
            return TimerFactory.FromMilliseconds(interval, false);
        }

        public static ITimer FromTimeSpan(TimeSpan interval)
        {
            return new ExtendedTimer(interval.TotalMilliseconds);
        }

        public static ITimer FromDateTimeAndStart(DateTime dateTime)
        {
            var timer = TimerFactory.FromTimeSpan(dateTime - DateTime.Now);
            timer.Start();
            return timer;
        }

        public static ITimer Create()
        {
            return new ExtendedTimer();
        }
    }
}
