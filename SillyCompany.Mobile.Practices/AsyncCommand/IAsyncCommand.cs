// ****************************************************************************
// <author>Stephen Cleary</author>
// <date>04-2014</date>
// <web>https://msdn.microsoft.com/en-us//magazine/dn630647.aspx</web>
// ****************************************************************************

using System.Threading.Tasks;
using System.Windows.Input;

using SillyCompany.Mobile.Practices.NotifyTask;

namespace SillyCompany.Mobile.Practices.AsyncCommand
{

    public interface IAsyncCommand : ICommand
    {
        INotifyTask Execution { get; }

        Task ExecuteAsync(object parameter);

        void RaiseCanExecuteChanged();
    }

    public interface IAsyncCommand<TResult> : ICommand
    {
        NotifyTask<TResult> Execution { get; }
    }
}