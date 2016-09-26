namespace AngleSharp.Extensions
{
    using System;

    /// <summary>
    /// A set of useful extensions for the event loop.
    /// </summary>
    static class EventLoopExtensions
    {
        /// <summary>
        /// Enqueues another action without considering the cancellation token.
        /// </summary>
        /// <param name="loop">The loop to extend.</param>
        /// <param name="action">The action to enqueue.</param>
        /// <param name="priority">The priority of the item.</param>
        public static void Enqueue(this IEventLoop loop, Action action, TaskPriority priority = TaskPriority.Normal)
        {
            if (loop != null)
            {
                loop.Enqueue(c => action.Invoke(), priority);
            }
            else
            {
                action.Invoke();
            }
        }
    }
}
