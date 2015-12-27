namespace AngleSharp.Network
{
    using AngleSharp.Dom;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class Download : IDownload
    {
        readonly CancellationTokenSource _cts;
        readonly Task<IResponse> _task;
        readonly Url _target;
        readonly INode _originator;

        public Download(Task<IResponse> task, CancellationTokenSource cts, Url target, INode originator)
        {
            _task = task;
            _cts = cts;
            _target = target;
            _originator = originator;
        }

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

        public void Cancel()
        {
            _cts.Cancel();
        }
    }
}
