namespace AngleSharp.Network
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
        private readonly Task<IResponse> _task;
        private readonly Url _target;
        private readonly INode _originator;

        #endregion

        #region ctor

        public Download(Task<IResponse> task, CancellationTokenSource cts, Url target, INode originator)
        {
            _task = task;
            _cts = cts;
            _target = target;
            _originator = originator;
        }

        #endregion

        #region Properties

        public INode Originator
        {
            get { return _originator; }
        }

        public Url Target
        {
            get { return _target; }
        }

        public Task<IResponse> Task
        {
            get { return _task; }
        }

        public Boolean IsRunning
        {
            get { return _task.Status == TaskStatus.Running; }
        }

        public Boolean IsCompleted
        {
            get { return _task.Status == TaskStatus.Faulted || _task.Status == TaskStatus.RanToCompletion || _task.Status == TaskStatus.Canceled; }
        }

        #endregion

        #region Methods

        public void Cancel()
        {
            _cts.Cancel();
        }

        #endregion
    }
}
