using System;
using System.Collections.Generic;
using System.Text;

namespace TimeMachine
{
    public interface ICommand
    {
        void Execute();
    }
}
