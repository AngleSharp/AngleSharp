namespace AngleSharp.Dom
{
    using AngleSharp.Dom.Events;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A set of extensions for EventTarget objects.
    /// </summary>
    public static class EventTargetExtensions
    {
        /// <summary>
        /// Firing a simple event means that a trusted event with a name, which
        /// does not bubble, is not cancelable and which uses the Event
        /// interface. It is created and dispatched at the given target.
        /// </summary>
        /// <param name="target">The target of the simple event.</param>
        /// <param name="eventName">The name of the event to be fired.</param>
        /// <param name="bubble">Optional parameter to enable bubbling.</param>
        /// <param name="cancelable">Should it be cancelable?</param>
        /// <returns>
        /// True if the element was cancelled, otherwise false.
        /// </returns>
        public static Boolean FireSimpleEvent(this IEventTarget target, String eventName, Boolean bubble = false, Boolean cancelable = false)
        {
            var eventData = new Event { IsTrusted = true };
            eventData.Init(eventName, bubble, cancelable);
            return eventData.Dispatch(target);
        }

        /// <summary>
        /// Fires a trusted event with the provided event data.
        /// </summary>
        /// <param name="target">The target of the event.</param>
        /// <param name="eventData">The event data to dispatch.</param>
        /// <returns>
        /// True if the element was cancelled, otherwise false.
        /// </returns>
        public static Boolean Fire(this IEventTarget target, Event eventData)
        {
            eventData.IsTrusted = true;
            return eventData.Dispatch(target);
        }

        /// <summary>
        /// Firing an event means dispatching the initialized (and trusted) 
        /// event at the specified event target.
        /// </summary>
        /// <param name="target">
        /// The target, where the event has been invoked.
        /// </param>
        /// <param name="initializer">The used initializer.</param>
        /// <param name="targetOverride">
        /// The current event target, if different to the invoked target.
        /// </param>
        /// <returns>
        /// True if the element was cancelled, otherwise false.
        /// </returns>
        public static Boolean Fire<T>(this IEventTarget target, Action<T> initializer, IEventTarget? targetOverride = null)
            where T : Event, new()
        {
            var eventData = new T { IsTrusted = true };
            initializer(eventData);
            return eventData.Dispatch(targetOverride ?? target);
        }

        /// <summary>
        /// Returns a task that is completed once the event is fired.
        /// </summary>
        /// <typeparam name="TEventTarget">The event target type.</typeparam>
        /// <param name="node">The node that fires the event.</param>
        /// <param name="eventName">The name of the event to be awaited.</param>
        /// <returns>The awaitable task returning the event arguments.</returns>
        public static Task<Event> AwaitEventAsync<TEventTarget>(this TEventTarget node, String eventName)
            where TEventTarget : IEventTarget =>
            node.AwaitEventAsync(eventName, CancellationToken.None);

        /// <summary>
        /// Returns a task that is completed once the event is fired, or
        /// cancelled once the given token is cancelled. The listener is
        /// removed in either case.
        /// </summary>
        /// <typeparam name="TEventTarget">The event target type.</typeparam>
        /// <param name="node">The node that fires the event.</param>
        /// <param name="eventName">The name of the event to be awaited.</param>
        /// <param name="cancellationToken">The token to stop waiting.</param>
        /// <returns>The awaitable task returning the event arguments.</returns>
        public static Task<Event> AwaitEventAsync<TEventTarget>(this TEventTarget node, String eventName, CancellationToken cancellationToken)
            where TEventTarget : IEventTarget
        {
            // Argument errors are reported through the task, as they were when this method was async.
            if (node is null)
            {
                return Task.FromException<Event>(new ArgumentNullException(nameof(node)));
            }

            if (eventName is null)
            {
                return Task.FromException<Event>(new ArgumentNullException(nameof(eventName)));
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<Event>(cancellationToken);
            }

            try
            {
                return new EventAwaiter(node, eventName, cancellationToken).Task;
            }
            catch (Exception ex)
            {
                return Task.FromException<Event>(ex);
            }
        }

        /// <summary>
        /// A one-shot listener. It removes itself on the dispatching thread before completing the
        /// task, so no continuation of the awaiting code ever touches the listener list - with a
        /// synchronization context that continuation runs elsewhere while the dispatch goes on.
        /// </summary>
        private sealed class EventAwaiter
        {
            private readonly IEventTarget _target;
            private readonly String _eventName;
            private readonly DomEventHandler _handler;
            private readonly CancellationToken _cancellationToken;
            private readonly TaskCompletionSource<Event> _completion;
            private readonly CancellationTokenRegistration _registration;

            public EventAwaiter(IEventTarget target, String eventName, CancellationToken cancellationToken)
            {
                _target = target;
                _eventName = eventName;
                _handler = OnEvent;
                _cancellationToken = cancellationToken;
                _completion = new TaskCompletionSource<Event>();
                _target.AddEventListener(eventName, _handler);

                // Registered after the listener is added, so a cancellation racing this constructor
                // still finds the listener to remove; an already cancelled token runs it right here.
                _registration = cancellationToken.Register(static state => ((EventAwaiter)state!).OnCanceled(), this);
            }

            public Task<Event> Task => _completion.Task;

            private void OnEvent(Object sender, Event ev)
            {
                _target.RemoveEventListener(_eventName, _handler);
                _registration.Dispose();
                _completion.TrySetResult(ev);
            }

            private void OnCanceled()
            {
                _target.RemoveEventListener(_eventName, _handler);
                _completion.TrySetCanceled(_cancellationToken);
            }
        }
    }
}
