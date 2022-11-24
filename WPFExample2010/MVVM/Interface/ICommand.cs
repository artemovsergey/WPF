using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVM.Interface
{
    public interface ICommand
    {
        event EventHandler CanExecuteChanged;
        void Execute(object parameter);
        bool CanExecute(object parameter);
    }
}
