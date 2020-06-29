using System;

namespace WindowsModeScheduler.BL.Management
{
    internal class InstantModeAction : ModeAction
    {
        public override Double Interval => 0f;

        public InstantModeAction(WindowsMode mode, ActionForce force) : base(mode, force)
        { }
    }
}
