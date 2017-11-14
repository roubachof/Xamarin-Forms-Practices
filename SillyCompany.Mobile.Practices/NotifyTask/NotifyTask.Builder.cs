using System;
using System.Threading.Tasks;

namespace SillyCompany.Mobile.Practices.NotifyTask
{
    public abstract partial class NotifyTaskBase
    {
        public abstract class BuilderBase
        {
            protected Action<INotifyTask> WhenCompleted { get; set; }

            protected Action<INotifyTask> WhenCanceled { get; set; }

            protected Action<INotifyTask> WhenFaulted { get; set; }

            protected bool InANewTask { get; set; }

            protected string Name { get; set; }
        }
    }

    public partial class NotifyTask
    {
        public class Builder : BuilderBase
        {
            public Builder(Func<Task> task)
            {
                TaskFunc = task;
            }

            protected Func<Task> TaskFunc { get; }

            protected Action<INotifyTask> WhenSuccessfullyCompleted { get; private set; }

            public Builder WithWhenCompleted(Action<INotifyTask> whenCompleted)
            {
                WhenCompleted = whenCompleted;
                return this;
            }

            public Builder WithWhenCanceled(Action<INotifyTask> whenCanceled)
            {
                WhenCanceled = whenCanceled;
                return this;
            }

            public Builder WithWhenFaulted(Action<INotifyTask> whenFaulted)
            {
                WhenFaulted = whenFaulted;
                return this;
            }

            public Builder InNewTask()
            {
                InANewTask = true;
                return this;
            }

            public Builder WithName(string name)
            {
                Name = name;
                return this;
            }

            public Builder WithWhenSuccessfullyCompleted(Action<INotifyTask> whenSuccessfullyCompleted)
            {
                WhenSuccessfullyCompleted = whenSuccessfullyCompleted;
                return this;
            }

            public NotifyTask Build()
            {
                return new NotifyTask(
                    TaskFunc(),
                    WhenCanceled,
                    WhenFaulted,
                    WhenCompleted,
                    WhenSuccessfullyCompleted,
                    Name,
                    InANewTask);
            }
        }
    }

    public partial class NotifyTask<TResult>
    {
        public class Builder : BuilderBase
        {
            public Builder(Func<Task<TResult>> taskFunc)
            {
                TaskFunc = taskFunc;
            }

            protected Func<Task<TResult>> TaskFunc { get; }

            protected Action<INotifyTask, TResult> WhenSuccessfullyCompleted { get; private set; }

            protected TResult DefaultResult { get; private set; }

            public Builder WithWhenCompleted(Action<INotifyTask> whenCompleted)
            {
                WhenCompleted = whenCompleted;
                return this;
            }

            public Builder WithWhenCanceled(Action<INotifyTask> whenCanceled)
            {
                WhenCanceled = whenCanceled;
                return this;
            }

            public Builder WithWhenFaulted(Action<INotifyTask> whenFaulted)
            {
                WhenFaulted = whenFaulted;
                return this;
            }

            public Builder InNewTask()
            {
                InANewTask = true;
                return this;
            }

            public Builder WithName(string name)
            {
                Name = name;
                return this;
            }

            public Builder WithWhenSuccessfullyCompleted(Action<INotifyTask, TResult> whenSuccessfullyCompleted)
            {
                WhenSuccessfullyCompleted = whenSuccessfullyCompleted;
                return this;
            }

            public Builder WithDefaultResult(TResult defaultResult)
            {
                DefaultResult = defaultResult;
                return this;
            }

            public NotifyTask<TResult> Build()
            {
                return new NotifyTask<TResult>(
                    TaskFunc(),
                    DefaultResult,
                    WhenCanceled,
                    WhenFaulted,
                    WhenCompleted,
                    WhenSuccessfullyCompleted,
                    Name,
                    InANewTask);
            }
        }
    }
}