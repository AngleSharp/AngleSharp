namespace AngleSharp.Infrastructure
{
    using AngleSharp.DOM;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class DomTask
    {
        readonly CancellationTokenSource _cts;
        readonly IDocument _origin;

        public DomTask(IDocument origin)
        {
            _cts = new CancellationTokenSource();
            _origin = origin;
        }

        public IDocument Origin
        {
            get { return _origin; }
        }

        public abstract Task Run();

        public void Cancel()
        {
            _cts.Cancel();
        }
    }
}
