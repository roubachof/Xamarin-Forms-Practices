using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MetroLog;
using SillyCompany.Mobile.Practices.NotifyTask;

using Xamarin.Forms;

namespace SillyCompany.Mobile.Practices.ViewModels
{
    public interface IViewModelLoader
    {
        ICommand ReloadCommand { get; }

        bool IsCompleted { get; }

        bool IsNotStarted { get; }

        bool IsNotCompleted { get; }

        bool IsSuccessfullyCompleted { get; }

        bool IsCanceled { get; }

        bool IsFaulted { get; }

        string ErrorMessage { get; }
    }

    public class ViewModelLoader<TData> : BindableObject, IViewModelLoader
    {
        protected static readonly ILogger Log = LoggerFactory.GetLogger("ViewModelLoader");

        private Func<Task<TData>> _loadingTaskSource;

        private INotifyTask<TData> _currentLoadingTask = NotifyTask<TData>.NotStartedTask;

        private string _errorMessage;

        public ViewModelLoader()
        {
            ReloadCommand = new Command(() => Load(_loadingTaskSource));
        }

        public ICommand ReloadCommand { get; protected set; }

        public bool IsCompleted => _currentLoadingTask.IsCompleted;

        public bool IsNotStarted => _currentLoadingTask == null || _currentLoadingTask.IsNotStarted;

        public bool IsNotCompleted => _currentLoadingTask.IsNotCompleted;

        public bool IsSuccessfullyCompleted => _currentLoadingTask.IsSuccessfullyCompleted;

        public bool IsCanceled => _currentLoadingTask.IsCanceled;

        public bool IsFaulted => _currentLoadingTask.IsFaulted;

        public TData Result => _currentLoadingTask.Result;

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetAndRaise(ref _errorMessage, value);
        }

        public void Load(Func<Task<TData>> loadingTaskSource)
        {
            Log.Info("Load");
            _loadingTaskSource = loadingTaskSource;

            _currentLoadingTask = null;
            _currentLoadingTask = new NotifyTask<TData>.Builder(_loadingTaskSource)
                .WithWhenCompleted(
                    completedTask =>
                    {
                        Log.Info("Task completed");
                        RaisePropertyChanged(nameof(IsCompleted));
                        RaisePropertyChanged(nameof(IsNotCompleted));
                        RaisePropertyChanged(nameof(IsNotStarted));
                    })
                .WithWhenSuccessfullyCompleted(
                    (completedTask, result) =>
                    {
                        Log.Info("Task successfully completed");
                        RaisePropertyChanged(nameof(IsSuccessfullyCompleted));                      
                        RaisePropertyChanged(nameof(Result));
                    })
                .WithWhenFaulted(
                    faultedTask =>
                    {
                        Log.Info("Task completed with fault");
                        RaisePropertyChanged(nameof(IsFaulted));
                        ErrorMessage = ToErrorMessage(faultedTask.InnerException);
                    })
                .Build();

            _currentLoadingTask.Start();
            RaiseAllProperties();
        }

        public string ToErrorMessage(Exception exception)
        {
            return "Une erreur inconnue est servenue, merci de réessayer ultérieurement.";
        }

        private void RaiseAllProperties()
        {
            RaisePropertyChanged(nameof(IsCompleted));
            RaisePropertyChanged(nameof(IsNotCompleted));
            RaisePropertyChanged(nameof(IsNotStarted));
            RaisePropertyChanged(nameof(IsSuccessfullyCompleted));
            RaisePropertyChanged(nameof(IsFaulted));
            RaisePropertyChanged(nameof(Result));
        }
    }
}
