using System;
using WindowsModeScheduler.BL.Time;
using WindowsModeScheduler.BL.Management.API;

namespace WindowsModeScheduler.BL.Management
{
    public sealed class WindowsModeManager
    {
        private readonly ITimer _timer;
        private ModeAction _action;

        public event EventHandler<ModeActionChangedEventArgs> ActionChanged;

        public ModeAction Action
        {
            get => this._action;

            set
            {
                var current = this._action;
                this._action = value ?? throw new ArgumentNullException(nameof(value), "Action can not be null!");

                if (current != this._action)
                    this.ActionChanged(this, new ModeActionChangedEventArgs(current, this._action));
            }
        }

        public TimeSpan TimeLeft => TimeSpan.FromMilliseconds(this._timer.TimeRemaining);

        private void Act(ModeAction action)
        {
            if (action.Mode == WindowsMode.Lock)
            {
                WinAPIShell.LockWorkStation();
                return;
            }

            var uFlags = default(ExitWindows);

            switch (action.Mode)
            {
                case WindowsMode.LogOff:
                    break;
                case WindowsMode.ShutDown:
                    uFlags = ExitWindows.EWX_HYBRID_SHUTDOWN;
                    break;
                case WindowsMode.Reboot:
                    uFlags = ExitWindows.EWX_REBOOT;
                    break;
                case WindowsMode.Sleep:
                    uFlags = ExitWindows.EWX_POWEROFF;
                    break;
                case WindowsMode.Hibernation:
                    uFlags = ExitWindows.EWX_HYBRID_SHUTDOWN;
                    break;
                case WindowsMode.FastHibernation:
                    uFlags = ExitWindows.EWX_HYBRID_SHUTDOWN | ExitWindows.EWX_POWEROFF;
                    break;
            }

            switch (action.Force)
            {
                case ActionForce.Default:
                    break;
                case ActionForce.Force:
                    uFlags |= ExitWindows.EWX_FORCE;
                    break;
                case ActionForce.ForceIfHung:
                    uFlags |= ExitWindows.EWX_FORCEIFHUNG;
                    break;
            }

            WinAPIShell.ExitWindowsEx(uFlags, ShutdownReason.FlagUserDefined);
        }

        public void Act()
        {
            if (this._action == null)
                throw new Exception("Action is not set!");

            if (this._action.Interval == 0)
            {
                this.Act(this._action);
                return;
            }

            this._timer.Interval = this._action.Interval;
            this._timer.Start();
        }

        public void Cancel()
        {
            this._timer.Stop();
        }

        private WindowsModeManager()
        {
            this._timer = TimerFactory.Create();
            this._timer.Elapsed += this.TimerElapsed;
            this.ActionChanged += delegate { };

            if (!WinAPIShell.GetShutdownPrivilege())
                throw new InvalidOperationException();
        }

        private void TimerElapsed(Object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Act(this._action);
        }

        public static WindowsModeManager Instance { get; }

        static WindowsModeManager()
        {
            Instance = new WindowsModeManager();
        }
    }
}
