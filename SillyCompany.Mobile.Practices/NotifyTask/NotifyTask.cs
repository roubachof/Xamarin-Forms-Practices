using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace SillyCompany.Mobile.Practices.NotifyTask
{
    public partial class NotifyTask : NotifyTaskBase
    {
        public static readonly INotifyTask NotStartedTask = new NotStartedTask();

        /// <summary>
        /// Callback called when the task successfully completed.
        /// </summary>
        private readonly Action<INotifyTask> _whenSuccessfullyCompleted;

        /// <inheritdoc />
        internal NotifyTask(
            Task task,
            Action<INotifyTask> whenCanceled = null,
            Action<INotifyTask> whenFaulted = null,
            Action<INotifyTask> whenCompleted = null,
            Action<INotifyTask> whenSuccessfullyCompleted = null,
            string name = null,
            bool inNewTask = false,
            bool isHot = false)
            : base(task, whenCanceled, whenFaulted, whenCompleted, name, inNewTask, isHot)
        {
            _whenSuccessfullyCompleted = whenSuccessfullyCompleted;

            if (isHot)
            {
                TaskCompleted = MonitorTaskAsync(task);
            }
        }

        protected override bool HasCallbacks => base.HasCallbacks || _whenSuccessfullyCompleted != null;

        /// <summary>
        /// Create the specified task with a faulted callback.
        /// </summary>
        public static NotifyTask Create(
            Task task,
            Action<INotifyTask> whenFaulted = null,
            Action<INotifyTask> whenSuccessfullyCompleted = null,
            string name = null)
        {
            return new NotifyTask(
                task,
                whenFaulted: whenFaulted,
                whenSuccessfullyCompleted: whenSuccessfullyCompleted,
                name: name,
                isHot: true);
        }

        /// <summary>
        /// Create the specified task with a faulted callback.
        /// </summary>
        public static NotifyTask Create(
            Func<Task> task,
            Action<INotifyTask> whenFaulted = null,
            Action<INotifyTask> whenSuccessfullyCompleted = null,
            string name = null)
        {
            return new NotifyTask(
                task(),
                whenFaulted: whenFaulted,
                whenSuccessfullyCompleted: whenSuccessfullyCompleted,
                name: name,
                isHot: true);
        }

        protected override void OnSuccessfullyCompleted(PropertyChangedEventHandler propertyChanged)
        {
            base.OnSuccessfullyCompleted(propertyChanged);

            try
            {
                _whenSuccessfullyCompleted?.Invoke(this);
            }
            catch (Exception exception)
            {
                Log.Error("Error while calling when successfully completed callback", exception);
            }
        }
    }

    public class NotStartedTask : INotifyTask
    {
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