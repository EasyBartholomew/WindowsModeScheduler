using System;

namespace WindowsModeScheduler.BL.Management
{
    public abstract class ModeAction
    {
        public WindowsMode Mode { get; }

        public ActionForce Force { get; }

        public abstract Double Interval { get; }

        protected ModeAction(WindowsMode mode, ActionForce force)
        {
            if (!Enum.IsDefined(typeof(WindowsMode), mode))
                throw new ArgumentException("Windows mode must be defined!", nameof(mode));
            if (!Enum.IsDefined(typeof(ActionForce), force))
                throw new ArgumentException("Action force must be defined!", nameof(force));

            this.Mode = mode;
            this.Force = force;
        }

        public static ModeAction CreateFrom(WindowsMode mode, ActionForce force = ActionForce.Default)
        { return new InstantModeAction(mode, force); }

        public static ModeAction CreateFrom(TimeSpan span, WindowsMode mode, ActionForce force = ActionForce.Default)
        { return new SpanModeAction(mode, force, span); }

        public static ModeAction CreateFrom(DateTime target, WindowsMode mode, ActionForce force = ActionForce.Default)
        { return new PlannedModeAction(mode, force, target); }
    }
}
