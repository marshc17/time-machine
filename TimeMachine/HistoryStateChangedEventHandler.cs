using System;
using System.Collections.Generic;
using System.Text;

namespace TimeMachine
{
    public delegate void HistoryStateChangedEventHandler(HistoryStack sender, HistoryStateChangedEventArgs e);
}
