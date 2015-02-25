using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace VolleyballStats
{
    public class DelegateCommand : System.Windows.Input.ICommand
    {
        private readonly Action m_Execute;
        private readonly Func<bool> m_CanExecute;
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action execute)
            : this(execute, () => true) { /* empty */ }

        public DelegateCommand(Action execute, Func<bool> canexecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            m_Execute = execute;
            m_CanExecute = canexecute;
        }

        public bool CanExecute(object p)
        {
            return m_CanExecute == null ? true : m_CanExecute();
        }

        public void Execute(object p)
        {
            if (CanExecute(null))
                m_Execute();
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }
    //public class DelegateCommand<T> : ICommand
    //{
    //    private readonly Func<T, bool> _canExecuteMethod;
    //    private readonly Action<T> _executeMethod;

    //    #region Constructors

    //    public DelegateCommand(Action<T> executeMethod) : this(executeMethod, null)
    //    {
    //    }

    //    public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
    //    {
    //        _executeMethod = executeMethod;
    //        _canExecuteMethod = canExecuteMethod;
    //    }

    //    #endregion Constructors

    //    #region ICommand Members

    //    public event EventHandler CanExecuteChanged;

    //    bool ICommand.CanExecute(object parameter)
    //    {
    //        try
    //        {
    //            return CanExecute((T)parameter);
    //        }
    //        catch { return false; }
    //    }

    //    void ICommand.Execute(object parameter)
    //    {
    //        Execute((T)parameter);
    //    }

    //    #endregion ICommand Members

    //    #region Public Methods

    //    public bool CanExecute(T parameter)
    //    {
    //        return ((_canExecuteMethod == null) || _canExecuteMethod(parameter));
    //    }

    //    public void Execute(T parameter)
    //    {
    //        if (_executeMethod != null)
    //        {
    //            _executeMethod(parameter);
    //        }
    //    }

    //    public void RaiseCanExecuteChanged()
    //    {
    //        OnCanExecuteChanged(EventArgs.Empty);
    //    }

    //    #endregion Public Methods

    //    #region Protected Methods

    //    protected virtual void OnCanExecuteChanged(EventArgs e)
    //    {
    //        var handler = CanExecuteChanged;
    //        if (handler != null)
    //        {
    //            handler(this, e);
    //        }
    //    }

    //    #endregion Protected Methods
    //}
}
