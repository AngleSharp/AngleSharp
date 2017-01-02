#if !NET40 && !SL50
namespace System.Threading.Tasks
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Simple wrapper for static methods of Task, which are missing in older
    /// versions of the .NET-Framework.
    /// </summary>
    static class TaskEx
    {
        /// <summary>
        /// Wrapper for Task.WhenAll, but also works with .NET 4 and SL due to
        /// same naming as TaskEx in BCL.Async.
        /// </summary>
        public static Task WhenAll(params Task[] tasks)
        {
            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// Wrapper for Task.Run, but also works with .NET 4 and SL due to
        /// same naming as TaskEx in BCL.Async.
        /// </summary>
        public static Task Run(Action action, CancellationToken cancel)
        {
            return Task.Run(action, cancel);
        }

        /// <summary>
        /// Wrapper for Task.Delay, but also works with .NET 4 and SL due to
        /// same naming as TaskEx in BCL.Async.
        /// </summary>
        public static Task Delay(Int32 millisecondsDelay, CancellationToken cancel)
        {
            return Task.Delay(millisecondsDelay, cancel);
        }

        /// <summary>
        /// Wrapper for Task.WhenAll, but also works with .NET 4 and SL due to
        /// same naming as TaskEx in BCL.Async.
        /// </summary>
        public static Task WhenAll(IEnumerable<Task> tasks)
        {
            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// Wrapper for Task.FromResult, but also works with .NET 4 and SL due
        /// to same naming as TaskEx in BCL.Async.
        /// </summary>
        public static Task<TResult> FromResult<TResult>(TResult result)
        {
            return Task.FromResult(result);
        }
    }
}
#endif