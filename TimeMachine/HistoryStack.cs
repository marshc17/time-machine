using System;
using System.Collections.Generic;
using System.Text;

namespace TimeMachine
{
    public class HistoryStack : IHistoryTracker
    {
        private Stack<ReversibleCommand> undoHistory = new Stack<ReversibleCommand>();
        private Stack<ReversibleCommand> redoHistory = new Stack<ReversibleCommand>();

        public bool StateChangedSinceLastMarked { get; private set; } = false;

        public void ExecuteCommand(ReversibleCommand command)
        {
            command.ExecuteReversibly();
            StateChangedSinceLastMarked = true;
            redoHistory.Clear();
            undoHistory.Push(command);
        }

        public void Undo()
        {
            if (undoHistory.Count == 0)
            {
                return;
            }

            var commandToUndo = undoHistory.Pop();
            commandToUndo.Reverse();
            StateChangedSinceLastMarked = true;
            redoHistory.Push(commandToUndo);
        }

        public void Redo()
        {
            if (redoHistory.Count == 0)
            {
                return;
            }

            var commandToRedo = redoHistory.Pop();
            commandToRedo.ExecuteReversibly();
            StateChangedSinceLastMarked = true;
            undoHistory.Push(commandToRedo);
        }

        public void ClearHistory()
        {
            undoHistory.Clear();
            redoHistory.Clear();
            StateChangedSinceLastMarked = false;
        }

        public void MarkCurrentState()
        {
            StateChangedSinceLastMarked = false;
        }
    }
}
