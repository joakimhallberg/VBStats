using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;

namespace VolleyballStats
{
    public class AsyncRelayCommand : IAsyncCommand
    {
        readonly Func<object, bool> _canExecute;
        bool _isExecuting;
        readonly Func<object, Task> _action;

        public event EventHandler CanExecuteChanged;

        public AsyncRelayCommand(Func<object, Task> action)
        {
            _action = action;
        }

        public AsyncRelayCommand(Func<object, Task> action, Func<object, bool> canExecute)
            : this(action)
        {
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            var canExecutResult = _canExecute == null || _canExecute(parameter);
            return !_isExecuting && canExecutResult;
        }

        async void ICommand.Execute(object parameter)
        {
            await Execute(parameter);
        }

        public async Task Execute(object parameter)
        {
            _ChangeIsExecuting(true);
            try
            {
                await _action(parameter);
            }
            finally
            {
                _ChangeIsExecuting(false);
            }
        }

        private void _ChangeIsExecuting(bool newValue)
        {
            if (newValue == _isExecuting)
            {
                return;
            }
            _isExecuting = newValue;
            _OnCanExecuteChanged();
        }

        void _OnCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }

    public interface IAsyncCommand : ICommand
    {
        Task Execute(object parameter);
    }

}
