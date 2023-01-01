using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace fourbot.ViewModels.ViewModel
{
    class RelayCommand : ICommand
    {
        Action<object> _execute;
        Func<object, bool> _canExecute;
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
    }
