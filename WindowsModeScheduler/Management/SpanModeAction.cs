using System;

namespace WindowsModeScheduler.BL.Management
{
    internal class SpanModeAction : ModeAction
    {
        public override Double Interval { get; }

        public SpanModeAction(WindowsMode mode, ActionForce force, TimeSpan span) : base(mode, force)
        {
            this.Interval = span.TotalMilliseconds;
        }
    }
}
