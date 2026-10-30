namespace AngleSharp.Benchmarks
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using AngleSharp.Html.Parser;
    using BenchmarkDotNet.Attributes;
    using System;

    /// <summary>
    /// Gate for the listener list behind <see cref="EventTarget"/>: what a dispatch costs per
    /// target that has listeners, and what registering and removing a listener costs.
    /// </summary>
    /// <remarks>
    /// The list is copy-on-write, so the two sides trade against each other: a dispatch reads
    /// the current array as its snapshot instead of copying the list, while every add and remove
    /// allocates a new array. The dispatch rows reuse one event, so the event itself is not what
    /// is measured; <see cref="DispatchNoListeners"/> is the control - no target on its path has
    /// a listener list at all, so it must not move.
    ///
    /// <see cref="DispatchThroughListeningAncestors"/> is the delegation shape: a listener on the
    /// target and on each of its three ancestors. A bubbling event visits every ancestor twice,
    /// once while capturing and once while bubbling.
    ///
    /// Each row owns its element, because the add and remove rows are writes and the dispatch
    /// rows must not see a listener another row left behind.
    /// </remarks>
    [MemoryDiagnoser]
    public class EventTargetBenchmark
    {
        // Large enough that the loop, not the BenchmarkDotNet overhead, is what is being timed.
        private const Int32 Count = 1000;

        private const String Source =
            "<!doctype html><body><section><article><div><span id=target></span></div></article></section></body>";

        private static readonly DomEventHandler Listener = (_, _) => { };
        private static readonly DomEventHandler Other = (_, _) => { };

        private IElement _noListeners;
        private IElement _oneListener;
        private IElement _fourListeners;
        private IElement _listeningAncestors;
        private IElement _addRemove;
        private IElement _addRemoveAmongFour;
        private IElement _awaited;
        private Event _event;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var parser = new HtmlParser();

            _noListeners = parser.ParseDocument(Source).GetElementById("target");
            _oneListener = parser.ParseDocument(Source).GetElementById("target");
            _fourListeners = parser.ParseDocument(Source).GetElementById("target");
            _listeningAncestors = parser.ParseDocument(Source).GetElementById("target");
            _addRemove = parser.ParseDocument(Source).GetElementById("target");
            _addRemoveAmongFour = parser.ParseDocument(Source).GetElementById("target");
            _awaited = parser.ParseDocument(Source).GetElementById("target");
            _event = new Event("click", bubbles: true, cancelable: false);

            _oneListener.AddEventListener("click", Listener);

            for (var i = 0; i < 4; i++)
            {
                _fourListeners.AddEventListener("click", (_, _) => { });
                _addRemoveAmongFour.AddEventListener("click", (_, _) => { });
            }

            for (var node = (INode)_listeningAncestors; node is IElement element; node = node.Parent)
            {
                element.AddEventListener("click", Listener);

                if (element.LocalName == "section")
                {
                    break;
                }
            }

            // Warm every row's own path once, so the first measured invocation is not the one that
            // materializes the lazy per-target state behind it.
            DispatchNoListeners();
            DispatchOneListener();
            DispatchFourListeners();
            DispatchThroughListeningAncestors();
            AddRemoveListener();
            AddRemoveListenerAmongFour();
            AwaitEvent();
        }

        /// <summary>
        /// Control: a full bubbling dispatch with no listener list anywhere on the path.
        /// </summary>
        [Benchmark(Baseline = true, OperationsPerInvoke = Count)]
        public void DispatchNoListeners()
        {
            for (var i = 0; i < Count; i++)
            {
                _noListeners.Dispatch(_event);
            }
        }

        [Benchmark(OperationsPerInvoke = Count)]
        public void DispatchOneListener()
        {
            for (var i = 0; i < Count; i++)
            {
                _oneListener.Dispatch(_event);
            }
        }

        [Benchmark(OperationsPerInvoke = Count)]
        public void DispatchFourListeners()
        {
            for (var i = 0; i < Count; i++)
            {
                _fourListeners.Dispatch(_event);
            }
        }

        [Benchmark(OperationsPerInvoke = Count)]
        public void DispatchThroughListeningAncestors()
        {
            for (var i = 0; i < Count; i++)
            {
                _listeningAncestors.Dispatch(_event);
            }
        }

        [Benchmark(OperationsPerInvoke = Count)]
        public void AddRemoveListener()
        {
            for (var i = 0; i < Count; i++)
            {
                _addRemove.AddEventListener("click", Other);
                _addRemove.RemoveEventListener("click", Other);
            }
        }

        [Benchmark(OperationsPerInvoke = Count)]
        public void AddRemoveListenerAmongFour()
        {
            for (var i = 0; i < Count; i++)
            {
                _addRemoveAmongFour.AddEventListener("click", Other);
                _addRemoveAmongFour.RemoveEventListener("click", Other);
            }
        }

        /// <summary>
        /// One AwaitEventAsync round trip: register, dispatch, and the listener removing itself.
        /// </summary>
        [Benchmark(OperationsPerInvoke = Count)]
        public void AwaitEvent()
        {
            for (var i = 0; i < Count; i++)
            {
                var task = _awaited.AwaitEventAsync("click");
                _awaited.Dispatch(_event);
                _ = task.Result;
            }
        }
    }
}
