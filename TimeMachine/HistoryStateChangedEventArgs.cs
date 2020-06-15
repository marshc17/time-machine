using System;
using System.Collections.Generic;
using System.Text;

namespace TimeMachine
{
    public class HistoryStateChangedEventArgs : EventArgs
    {
        public bool CanUndo { get; set; }
        public bool CanRedo { get; set; }
    }
}
