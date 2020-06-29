using System;
using System.Timers;

namespace WindowsModeScheduler.BL.Time
{
    internal class ExtendedTimer : ITimer
    {
        private readonly Timer _timer;
        private Double _interval;
        private DateTime _startedTime;

        private Boolean _enabled;
        private TimerState _timerState;

        public TimerState TimerState => this._timerState;

        public Boolean IsEnabled => this._enabled;

        public virtual Boolean AutoReset { get => this._timer.AutoReset; set => this._timer.AutoReset = value; }

        public virtual Double Interval
        {
            get => this._interval;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Interval must be greater than zero!", nameof(value));

                this._interval = value;
            }
        }

        public virtual Double TimeRemaining
        {
            get
            {
                if (this._timerState == TimerState.InProgress)
                    return (this._startedTime - DateTime.Now)
                    .Add(TimeSpan.FromMilliseconds(this._timer.Interval))
                    .TotalMilliseconds;

                return this._timer.Interval;
            }
        }

        public event ElapsedEventHandler Elapsed;

        public ExtendedTimer(double interval, bool startImmediately)
        {
            this._timer = new Timer();
            this.Interval = interval;
            this.AutoReset = false;
            this._enabled = false;
            this._timerState = TimerState.NeverActivated;

            this.Elapsed += delegate
            {
                if (this.AutoReset)
                {
                    this._timer.Interval = this.Interval;
                    this._startedTime = DateTime.Now;
                }
            };

            this._timer.Elapsed += (sender, e) =>
            {
                this.Elapsed(this, e);
            };

            if (startImmediately)
                this.Start();
        }

        public ExtendedTimer(double interval) : this(interval, false) { }

        public ExtendedTimer() : this(1000) { }

        public virtual void Start()
        {
            this._timer.Interval = this.Interval;
            this._timer.Start();
            this._startedTime = DateTime.Now;
            this._enabled = true;
            this._timerState = TimerState.InProgress;
        }

        public virtual void Stop()
        {
            if ((this._timerState != TimerState.InProgress) && (this.TimerState != TimerState.Paused))
                return;

            this._timer.Stop();
            this._enabled = false;
            this._timerState = TimerState.Stopped;
        }

        public virtual void Pause()
        {
            if (this._timerState != TimerState.InProgress)
                return;

            this._timer.Stop();
            this._timer.Interval = this.TimeRemaining;

            this._timerState = TimerState.Paused;
        }

        public virtual void Resume()
        {
            if (this._timerState != TimerState.Paused)
                return;
            this._startedTime = DateTime.Now;
            this._timer.Start();
            this._timerState = TimerState.InProgress;
        }

        public virtual void Refresh()
        {
            this._timer.Interval = this.Interval;
            this._startedTime = DateTime.Now;
            this._timer.Stop();
            this._timer.Start();
        }

        public virtual void Dispose()
        {
            this._timer.Dispose();
        }
    }
}
