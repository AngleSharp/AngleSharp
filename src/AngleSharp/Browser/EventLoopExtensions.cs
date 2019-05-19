namespace AngleSharp.Browser
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A set of useful extensions for the event loop.
    /// </summary>
    public static class EventLoopExtensions
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

        /// <summary>
        /// Enqueues another function with respecting the async nature.
        /// Exceptions will be emitted respectively.
        /// </summary>
        /// <param name="loop">The loop to extend.</param>
        /// <param name="action">The action to enqueue.</param>
        /// <param name="priority">The priority of the item.</param>
        /// <returns>A task that is completed when the action has been invoked.</returns>
        public static Task<T> EnqueueAsync<T>(this IEventLoop loop, Func<CancellationToken, T> action, TaskPriority priority = TaskPriority.Normal)
        {
            if (loop != null)
            {
                var tcs = new TaskCompletionSource<T>();

                loop.Enqueue(c =>
                {
                    try
                    {
                        tcs.SetResult(action.Invoke(c));
                    }
                    catch (Exception ex)
                    {
                        tcs.SetException(ex);
                    }
                }, priority);

                return tcs.Task;
            }
            else
            {
                try
                {
                    return Task.FromResult(action.Invoke(default));
                }
                catch (Exception ex)
                {
                    return Task.FromException<T>(ex);
                }
            }
        }
    }
}
