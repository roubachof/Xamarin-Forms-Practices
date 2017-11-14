// ****************************************************************************
// <author>Stephen Cleary</author>
// <date>04-2014</date>
// <web>https://msdn.microsoft.com/en-us//magazine/dn630647.aspx</web>
// ****************************************************************************

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using SillyCompany.Mobile.Practices.NotifyTask;

namespace SillyCompany.Mobile.Practices.AsyncCommand
{
    
    public static class AsyncCommand
    {
        public static AsyncCommand<object> Create(Func<Task> command, Func<object, bool> canExecute = null)
        {
            return new AsyncCommand<object>(
                async (parameter, cancellationToken) =>
                {
                    await command();
                    return null;
                }, canExecute);
        }

        public static AsyncCommand<TResult> Create<TResult>(Func<Task<TResult>> command, Func<object, bool> canExecute = null)
        {
            return new AsyncCommand<TResult>((parameter, cancellationToken) => command(), canExecute);
        }

        public static AsyncCommand<TResult> Create<TResult>(Func<object, Task<TResult>> command, Func<object, bool> canExecute = null)
        {
            return new AsyncCommand<TResult>((parameter, cancellationToken) => command(parameter), canExecute);
        }

        public static AsyncCommand<object> Create(Func<object, CancellationToken, Task> command, Func<object, bool> canExecute = null)
        {
            return new AsyncCommand<object>(
                async (parameter, token) =>
                {
                    await command(parameter, token);
                    return null;
                }, canExecute);
        }

        public static AsyncCommand<object> Create(Func<object, Task> command, Func<object, bool> canExecute = null)
        {
            return new AsyncCommand<object>(
                async (parameter, token) =>
                {
                    await command(parameter);
                    return null;
                }, canExecute);
        }

        public static AsyncCommand<TResult> Create<TResult>(Func<object, CancellationToken, Task<TResult>> command, Func<object, bool> canExecute = null)
        {
            return new AsyncCommand<TResult>(command, canExecute);
        }
    }

    public class AsyncCommand<TResult> : AsyncCommandBase, IAsyncCommand, IAsyncCommand<TResult>, INotifyPropertyChanged, IDisposable
    {
        private readonly Func<object,CancellationToken, Task<TResult>> command;
        private readonly Func<object, bool> canExecuteFunc;
        private readonly CancelAsyncCommand cancelCommand;
        private NotifyTask<TResult> execution;

        public AsyncCommand(Func<object, CancellationToken, Task<TResult>> command, Func<object, bool> canExecute = null)
        {
            this.command = command;
            this.canExecuteFunc = canExecute;
            this.cancelCommand = new CancelAsyncCommand();
        }

        public override bool CanExecute(object parameter = null)
        {
            bool isNotExecuting = this.Execution == null || this.Execution.IsCompleted;
            bool canExecute = this.canExecuteFunc == null || this.canExecuteFunc(parameter);
            return isNotExecuting && canExecute;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            this.cancelCommand.NotifyCommandStarting();
            this.Execution = NotifyTask<TResult>.Create(
                this.command(
                    parameter,
                    this.cancelCommand.Token));

            this.RaiseCanExecuteChanged();
            await this.Execution.TaskCompleted;
            this.cancelCommand.NotifyCommandFinished();
            this.RaiseCanExecuteChanged();
        }

        public ICommand CancelCommand => this.cancelCommand;

        public NotifyTask<TResult> Execution
        {
            get
            {
                return this.execution;
            }
            private set
            {
                this.execution = value;
                this.OnPropertyChanged();
            }
        }    
        
        INotifyTask IAsyncCommand.Execution => this.Execution;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private sealed class CancelAsyncCommand : ICommand, IDisposable
        {
            private CancellationTokenSource cts = new CancellationTokenSource();
            private bool commandExecuting;

            public CancellationToken Token => this.cts.Token;

            public void NotifyCommandStarting()
            {
                this.commandExecuting = true;
                if (!this.cts.IsCancellationRequested)
                {
                    return;
                }

                this.cts = new CancellationTokenSource();
                this.RaiseCanExecuteChanged();
            }

            public void NotifyCommandFinished()
            {
                this.commandExecuting = false;
                this.RaiseCanExecuteChanged();
            }

            bool ICommand.CanExecute(object parameter)
            {
                return this.commandExecuting && !this.cts.IsCancellationRequested;
            }

            void ICommand.Execute(object parameter)
            {
                this.cts.Cancel();
                this.RaiseCanExecuteChanged();
            }

            public event EventHandler CanExecuteChanged;

            private void RaiseCanExecuteChanged()
            {
                this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }

            public void Dispose()
            {
                this.cts?.Dispose();
            }
        }

        public void Dispose()
        {
            this.cancelCommand?.Dispose();
        }
    }
}