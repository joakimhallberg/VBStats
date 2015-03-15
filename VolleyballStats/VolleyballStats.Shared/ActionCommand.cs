using System;
using System.Collections.Generic;
using System.Text;

namespace VolleyballStats
{
    using System;
    using System.Windows.Input;
    public class ActionCommand : ICommand
    {
        private readonly Action action;
        private readonly Func<bool> condition;

        public ActionCommand()
        {
            this.action = delegate { };
            this.condition = () => true;
        }
        public ActionCommand(Action action)
        {
            this.action = action;
            this.condition = () => true;
        }

        public ActionCommand(Action action, Func<bool> condition)
        {
            this.action = action;
            this.condition = condition;
        }

        public event EventHandler CanExecuteChanged = delegate { };
        public void RaiseExecuteChanged()
        {
            this.CanExecuteChanged(this, EventArgs.Empty);
        }
        public bool CanExecute(object parameter)
        {
            return this.condition();
        }
        public void Execute(object parameter)
        {
            this.action();
        }
    }

}
