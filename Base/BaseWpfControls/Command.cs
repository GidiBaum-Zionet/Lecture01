using System;
using System.Windows.Input;

namespace Base.Wpf
{
    public class Command : ICommand
    {
        public Command()
        {

        }

        public Command(Action<object> action)
        {
            CanExecuteFunction = p => true;
            ExecuteFunction = p => action(p);
        }

        public Command(Action action)
        {
            CanExecuteFunction = p => true;
            ExecuteFunction = p => action();
        }

        public delegate void ExecuteDelegate(object parameter);
        public delegate bool CanExecuteDelegate(object parameter);

        public ExecuteDelegate ExecuteFunction {get; set;}
        public CanExecuteDelegate CanExecuteFunction {get; set;}


        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunction != null && CanExecuteFunction(parameter);
        }


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public void Execute(object parameter)
        {
            ExecuteFunction?.Invoke(parameter);
        }

        #endregion

    }
}
