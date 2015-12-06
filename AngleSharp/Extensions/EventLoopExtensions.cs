namespace AngleSharp.Extensions
{
    using System;

    static class EventLoopExtensions
    {
        public static void Enqueue(this IEventLoop loop, Action action, TaskPriority priority = TaskPriority.Normal)
        {
            loop.Enqueue(c => action(), priority);
        }
    }
}
