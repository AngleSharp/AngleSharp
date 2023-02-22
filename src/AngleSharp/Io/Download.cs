namespace AngleSharp.Io
{
    using AngleSharp.Dom;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a download in progress.
    /// </summary>
    sealed class Download : IDownload
    {
        #region Fields

        private readonly CancellationTokenSource _cts;

        #endregion

        #region ctor

        public Download(Task<IResponse> task, CancellationTokenSource cts, Url target, Object? source)
        {
            Task = task;
            _cts = cts;
            Target = target;
            Source = source;
        }

        #endregion

        #region Properties

        public Object? Source { get; }

        public Url Target { get; }

        public Task<IResponse> Task { get; }

        public Boolean IsRunning => Task.Status == TaskStatus.Running;

        public Boolean IsCompleted => Task.Status == TaskStatus.Faulted ||
            Task.Status == TaskStatus.RanToCompletion ||
            Task.Status == TaskStatus.Canceled;

        #endregion

        #region Methods

        public void Cancel() => _cts.Cancel();

        #endregion
    }
}
