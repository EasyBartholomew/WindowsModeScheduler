using System;

namespace WindowsModeScheduler.BL.Management
{
    public class ModeActionChangedEventArgs : EventArgs
    {
        public ModeAction PreviousAction { get; }

        public ModeAction NewAction { get; }

        public ModeActionChangedEventArgs(ModeAction previousAction, ModeAction newAction)
        {
            this.PreviousAction = previousAction;
            this.NewAction = newAction;
        }
    }
}
