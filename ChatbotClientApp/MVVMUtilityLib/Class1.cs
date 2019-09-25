using System;

namespace MVVMUtilityLib
{
        public class DelegateCommand : System.Windows.Input.ICommand
        {
            public event EventHandler CanExecuteChanged;

            private Action<object> _executeMethodAddress;
            private Func<object, bool> _canExecuteMethodAddress;

            public DelegateCommand(Action<object> executeMethodAddress, Func<object, bool> canExecuteMethodAddress)
            {
                this._executeMethodAddress = executeMethodAddress;
                this._canExecuteMethodAddress = canExecuteMethodAddress;
            }


            public bool CanExecute(object parameter)
            {
                return this._canExecuteMethodAddress.Invoke(parameter);
            }

            public void Execute(object parameter)
            {
                this._executeMethodAddress.Invoke(parameter);
            }
        }
 }
