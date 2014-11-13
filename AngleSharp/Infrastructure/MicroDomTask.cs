namespace AngleSharp.Infrastructure
{
    using AngleSharp.DOM;
    using System;
    using System.Threading.Tasks;

    public sealed class MicroDomTask : DomTask
    {
        readonly Func<Task> _taskCreator;

        public MicroDomTask(IDocument document, Action action)
            : this(document, new Task(action))
        {
        }

        public MicroDomTask(IDocument document, Task task)
            : this(document, () => task)
        {
        }

        public MicroDomTask(IDocument document, Func<Task> taskCreator)
            : base(document)
        {
            _taskCreator = taskCreator;
        }

        public override Task Run()
        {
            var task = _taskCreator();
            task.Start();
            return task;
        }
    }
}
