using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TimeMachine
{
    public class HistoryStack : IHistoryTracker
    {
        private Stack<ReversibleCommand> undoHistory = new Stack<ReversibleCommand>();
        private Stack<ReversibleCommand> redoHistory = new Stack<ReversibleCommand>();

        public event HistoryStateChangedEventHandler OnStateChanged;

        public void ExecuteCommand(ReversibleCommand command)
        {
            command.ExecuteReversibly();
            redoHistory.Clear();
            undoHistory.Push(command);
            ReportStateChanged();
        }

        public void Undo()
        {
            if (undoHistory.Count == 0)
            {
                return;
            }

            var commandToUndo = undoHistory.Pop();
            commandToUndo.Reverse();
            redoHistory.Push(commandToUndo);
            ReportStateChanged();
        }

        public void Redo()
        {
            if (redoHistory.Count == 0)
            {
                return;
            }

            var commandToRedo = redoHistory.Pop();
            commandToRedo.ExecuteReversibly();
            undoHistory.Push(commandToRedo);
            ReportStateChanged();
        }

        public void ClearHistory()
        {
            undoHistory.Clear();
            redoHistory.Clear();
            ReportStateChanged();
        }

        private void ReportStateChanged()
        {
            var eventArgs = new HistoryStateChangedEventArgs
            {
                CanUndo = undoHistory.Count > 0,
                CanRedo = redoHistory.Count > 0
            };

            OnStateChanged?.Invoke(this, eventArgs);
        }
    }
}
