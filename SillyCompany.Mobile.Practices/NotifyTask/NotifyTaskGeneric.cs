using System;
using System.ComponentModel;
using System.Threading.Tasks;

using Nito.Mvvm;

namespace SillyCompany.Mobile.Practices.NotifyTask
{
    /// <inheritdoc />
    /// <typeparam name="TResult">The type of the task result.</typeparam>
    public partial class NotifyTask<TResult> : NotifyTaskBase, INotifyTask<TResult>
    {
        public static readonly INotifyTask<TResult> NotStartedTask = new NotStartedTask<TResult>();

        /// <summary>
        /// The "result" of the task when it has not yet completed.
        /// </summary>
        private readonly TResult _defaultResult;

        /// <summary>
        /// Callback called when the task successfully completed.
        /// </summary>
        private readonly Action<INotifyTask, TResult> _whenSuccessfullyCompleted;

        /// <inheritdoc />
        internal NotifyTask(
            Task<TResult> task,
            TResult defaultResult = default(TResult),
            Action<INotifyTask> whenCanceled = null,
            Action<INotifyTask> whenFaulted = null,
            Action<INotifyTask> whenCompleted = null,
            Action<INotifyTask, TResult> whenSuccessfullyCompleted = null,
            string name = null,
            bool inNewTask = false,
            bool isHot = false)
            : base(task, whenCanceled, whenFaulted, whenCompleted, name, inNewTask, isHot)
        {
            _defaultResult = defaultResult;
            _whenSuccessfullyCompleted = whenSuccessfullyCompleted;
            Task = task;

            if (isHot)
            {
                TaskCompleted = MonitorTaskAsync(task);
            }
        }

        /// <summary>
        /// Gets the task being watched. This property never changes and is never <c>null</c>.
        /// </summary>
        public new Task<TResult> Task { get; }

        /// <summary>
        /// Gets the result of the task. Returns the "default result" value specified in the constructor if the task has not yet completed successfully. This property raises a notification when the task completes successfully.
        /// </summary>
        public TResult Result => (Task.Status == TaskStatus.RanToCompletion) ? Task.Result : _defaultResult;

        protected override bool HasCallbacks => base.HasCallbacks || _whenSuccessfullyCompleted != null;

        /// <summary>
        /// Creates a new task notifier watching the specified task.
        /// </summary>
        public static NotifyTask<TResult> Create(
            Task<TResult> task,
            Action<INotifyTask> whenFaulted = null,
            Action<INotifyTask, TResult> whenSuccessfullyCompleted = null,
            string name = null,
            TResult defaultResult = default(TResult))
        {
            return new NotifyTask<TResult>(
                task,
                whenFaulted: whenFaulted,
                whenSuccessfullyCompleted: whenSuccessfullyCompleted,
                name: name,
                defaultResult: defaultResult,
                isHot: true);
        }

        protected override void OnSuccessfullyCompleted(PropertyChangedEventHandler propertyChanged)
        {
            propertyChanged?.Invoke(this, PropertyChangedEventArgsCache.Instance.Get("Result"));
            base.OnSuccessfullyCompleted(propertyChanged);

            try
            {
                _whenSuccessfullyCompleted?.Invoke(this, Result);
            }
            catch (Exception exception)
            {
                Log.Error("Error while calling when successfully completed callback", exception);
            }
        }
    }

    public class NotStartedTask<TResult> : INotifyTask<TResult>
    {
        public TResult Result => default(TResult);

        public Task Task { get; }

        public Task TaskCompleted { get; }

        public TaskStatus Status => TaskStatus.Created;

        public bool IsNotStarted => true;

        public bool IsRunning { get; }

        public bool IsCompleted { get; }

        public bool IsNotCompleted => true;

        public bool IsSuccessfullyCompleted { get; }

        public bool IsCanceled { get; }

        public bool IsFaulted { get; }

        public string Name { get; }

        public AggregateException Exception { get; }

        public Exception InnerException { get; }

        public string ErrorMessage { get; }

        public void Start()
        {
            throw new NotSupportedException();
        }
    }
}