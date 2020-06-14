using System;
using System.Collections.Generic;
using System.Text;

namespace TimeMachine
{
    public interface IHistoryTracker
    {
        void ExecuteCommand(ReversibleCommand command);
    }
}
