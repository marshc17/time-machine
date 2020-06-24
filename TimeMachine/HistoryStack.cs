using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TimeMachine
{
    public class HistoryStack : IHistoryTracker
    {
        private Stack<ReversibleCommand> undoHistory = new Stack<ReversibleCommand>();
        private Stack<ReversibleCommand> redoHistory = new Stack<ReversibleCommand>();

        public bool CanUndo { get { return undoHistory.Any(); } }

        public bool CanRedo { get { return redoHistory.Any(); } }

        public event EventHandler OnStateChanged;

        public void ExecuteCommand(ReversibleCommand command)
        {
            command.ExecuteReversibly();
            redoHistory.Clear();
            undoHistory.Push(command);
            OnStateChanged?.Invoke(this, null);
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
            OnStateChanged?.Invoke(this, null);
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
            OnStateChanged?.Invoke(this, null);
        }

        public void ClearHistory()
        {
            undoHistory.Clear();
            redoHistory.Clear();
            OnStateChanged?.Invoke(this, null);
        }
    }
}
