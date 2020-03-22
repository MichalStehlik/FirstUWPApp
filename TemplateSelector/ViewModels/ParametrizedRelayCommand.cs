using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TemplateSelector.ViewModels
{
    class ParametrizedRelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> _execute;
        private readonly Func<bool> _canExecute;

        public ParametrizedRelayCommand(Action<object> execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute(parameter);         
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
