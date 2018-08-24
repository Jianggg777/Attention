using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Attention
{
    class RelayCommand : ICommand
    {

        readonly Func<bool> _canExecute;
        readonly Action _execute;

        public RelayCommand(Action execute) : this(execute, null)
        {

        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            _execute = execute;
            _canExecute = canExecute;
        }
        
       
        // implement ICommand
        public event EventHandler CanExecuteChanged {
            add {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        // implement ICommand
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {  // return false ==> button 不能按 (灰色)
            return _canExecute == null ? true : _canExecute();
        }
        // implement ICommand
        public void Execute(object parameter)
        {
            _execute();
        }
    }


}