namespace AngleSharp.Infrastructure
{
    using AngleSharp.DOM;
    using System;
    using System.Threading.Tasks;

    public sealed class MicroDomTask : DomTask
    {
        readonly Task _task;

        public MicroDomTask(IDocument document, Action action)
            : base(document)
        {
            _task = new Task(action);
        }

        public override Task Run()
        {
            _task.Start();
            return _task;
        }
    }
}
