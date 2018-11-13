using System;
using System.Windows.Input;

namespace ViewModels
{
    public class DelegateCommand: ICommand
    {
        private readonly Action action;
        private readonly Func<Boolean> canExecute;

        public DelegateCommand(Action action)
        {
            this.action = action;
        }

        public DelegateCommand(Action action, Func<Boolean> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            this.action();
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
            {
                return canExecute();
            }

            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}