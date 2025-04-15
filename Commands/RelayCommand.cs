using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MMIMApp.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object> ExecuteAction { get; set; }

        private Predicate<object> CanExecutePredicate { get; set; }

        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action<object> ExecuteMethod, Predicate<object> CanExecuteMethod = null)
        {
            ExecuteAction = ExecuteMethod ?? throw new ArgumentNullException(nameof(ExecuteMethod));
            CanExecutePredicate = CanExecuteMethod;
        }

        public bool CanExecute(object? parameter)
        {
            return CanExecutePredicate(parameter) || CanExecutePredicate == null;   
        }

        public void Execute(object? parameter)
        {
            ExecuteAction(parameter);
        }

        
    }
}
