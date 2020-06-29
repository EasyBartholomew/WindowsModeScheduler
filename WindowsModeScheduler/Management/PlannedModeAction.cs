using System;

namespace WindowsModeScheduler.BL.Management
{
    internal class PlannedModeAction : ModeAction
    {
        public override Double Interval
        {
            get
            {
                var span = (this._target - DateTime.Now).TotalMilliseconds;

                if (span < 0)
                    return 0;

                return span;
            }
        }

        private readonly DateTime _target;

        public PlannedModeAction(WindowsMode mode, ActionForce force, DateTime target) : base(mode, force)
        {
            if (target < DateTime.Now)
                throw new ArgumentException($"{target} can not be earlier than now!", nameof(target));

            this._target = target;
        }
    }
}
