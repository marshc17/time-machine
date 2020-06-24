using System;
using System.Collections.Generic;
using System.Text;

namespace TimeMachine
{
    public abstract class ReversibleCommand : ICommand
    {
        private IHistoryTracker historyTracker;

        public ReversibleCommand(IHistoryTracker historyTracker)
        {
            this.historyTracker = historyTracker;
        }

        public void Execute()
        {
            this.historyTracker.ExecuteCommand(this);
        }

        public abstract void ExecuteReversibly();

        public abstract void Reverse();
    }
}
