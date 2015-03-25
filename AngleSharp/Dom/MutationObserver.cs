namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// MutationObserver provides developers a way to react to changes in a
    /// DOM.
    /// </summary>
    [DomName("MutationObserver")]
    public sealed class MutationObserver
    {
        #region Fields

        readonly Queue<IMutationRecord> _records;
        readonly MutationCallback _callback;
        readonly List<MutationObserving> _observing;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new mutation observer with the provided callback.
        /// </summary>
        /// <param name="callback">The callback to trigger.</param>
        [DomConstructor]
        public MutationObserver(MutationCallback callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            _records = new Queue<IMutationRecord>();
            _callback = callback;
            _observing = new List<MutationObserving>();
        }

        #endregion

        #region Properties

        MutationObserving this[INode node]
        {
            get
            {
                foreach (var observing in _observing)
                {
                    if (Object.ReferenceEquals(observing.Target, node))
                        return observing;
                }

                return null;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Queues a record.
        /// </summary>
        /// <param name="record">The record to queue up.</param>
        internal void Enqueue(MutationRecord record)
        {
            if (_records.Count > 0)
            {
                //Here we could schedule a callback!
            }

            _records.Enqueue(record);
        }

        /// <summary>
        /// Triggers the execution if the queue is not-empty.
        /// </summary>
        internal void Trigger()
        {
            var records = _records.ToArray();
            _records.Clear();
            ClearTransients();

            if (records.Length != 0)
                TriggerWith(records);
        }

        /// <summary>
        /// Triggers the execution with the provided records.
        /// </summary>
        /// <param name="records">The records to supply as argument.</param>
        internal void TriggerWith(IMutationRecord[] records)
        {
            _callback(records, this);
        }

        /// <summary>
        /// Gets the options, if any, for the given node. If null is returned
        /// then the node is not being observed.
        /// </summary>
        /// <param name="node">The node of interest.</param>
        /// <returns>The options set for the provided node.</returns>
        internal MutationObserverInit ResolveOptions(INode node)
        {
            foreach (var observing in _observing)
            {
                if (Object.ReferenceEquals(observing.Target, node) || observing.TransientNodes.Contains(node))
                    return observing.Options;
            }

            return null;
        }

        /// <summary>
        /// Adds a transient observer for the given node with the provided
        /// ancestor, if the node's ancestor is currently observed.
        /// </summary>
        /// <param name="ancestor">
        /// The ancestor that is currently observed.
        /// </param>
        /// <param name="node">
        /// The node to observe as a transient observer.
        /// </param>
        internal void AddTransient(INode ancestor, INode node)
        {
            var obs = this[ancestor];

            if (obs != null && obs.Options.IsObservingSubtree)
                obs.TransientNodes.Add(node);
        }

        /// <summary>
        /// Clears all transient observers.
        /// </summary>
        internal void ClearTransients()
        {
            foreach (var observing in _observing)
                observing.TransientNodes.Clear();
        }

        /// <summary>
        /// Stops the MutationObserver instance from receiving
        /// notifications of DOM mutations. Until the observe()
        /// method is used again, observer's callback will not be invoked.
        /// </summary>
        [DomName("disconnect")]
        public void Disconnect()
        {
            foreach (var observing in _observing)
            {
                var node = (Node)observing.Target;
                node.Owner.Mutations.Unregister(this);
            }

            _records.Clear();
        }

        /// <summary>
        /// Registers the MutationObserver instance to receive notifications of
        /// DOM mutations on the specified node.
        /// </summary>
        /// <param name="target">
        /// The Node on which to observe DOM mutations.
        /// </param>
        /// <param name="options">
        /// Specifies which DOM mutations should be reported.
        /// </param>
        public void Connect(INode target, MutationObserverInit options)
        {
            var node = target as Node;

            if (node == null)
                return;
            else if (options == null)
                options = new MutationObserverInit();

            if (options.IsExaminingOldCharacterData.HasValue == false)
                options.IsExaminingOldCharacterData = false;

            if (options.IsExaminingOldAttributeValue.HasValue == false)
                options.IsExaminingOldAttributeValue = false;

            if (options.IsObservingAttributes.HasValue == false)
                options.IsObservingAttributes = options.IsExaminingOldAttributeValue.Value || options.AttributeFilters != null;

            if (options.IsObservingCharacterData.HasValue == false)
                options.IsObservingCharacterData = options.IsExaminingOldCharacterData.HasValue && options.IsExaminingOldCharacterData.Value;

            if (options.IsExaminingOldAttributeValue.Value && options.IsObservingAttributes.Value == false)
                throw new DomException(DomError.TypeMismatch);

            if (options.AttributeFilters != null && options.IsObservingAttributes.Value == false)
                throw new DomException(DomError.TypeMismatch);

            if (options.IsExaminingOldCharacterData.Value && options.IsObservingCharacterData.Value == false)
                throw new DomException(DomError.TypeMismatch);

            if (options.IsObservingChildNodes == false && options.IsObservingCharacterData.Value == false && options.IsObservingAttributes.Value == false)
                throw new DomException(DomError.Syntax);

            node.Owner.Mutations.Register(this);
            var existing = this[target];

            if (existing != null)
            {
                existing.TransientNodes.Clear();
                _observing.Remove(existing);
            }

            _observing.Add(new MutationObserving(target, options));
        }

        /// <summary>
        /// Registers the MutationObserver instance to receive notifications of
        /// DOM mutations on the specified node.
        /// </summary>
        /// <param name="target">
        /// The Node on which to observe DOM mutations.
        /// </param>
        /// <param name="options">A dictionary with options.</param>
        [DomName("observe")]
        public void Connect(INode target, IDictionary<String, Object> options)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            var init = new MutationObserverInit
            {
                AttributeFilters = options.TryGet("attributeFilter") as IEnumerable<String>,
                IsObservingAttributes = options.TryGet<Boolean>("attributes"),
                IsObservingChildNodes = options.TryGet<Boolean>("childList") ?? false,
                IsObservingCharacterData = options.TryGet<Boolean>("characterData"),
                IsObservingSubtree = options.TryGet<Boolean>("subtree") ?? false,
                IsExaminingOldAttributeValue = options.TryGet<Boolean>("attributeOldValue"),
                IsExaminingOldCharacterData = options.TryGet<Boolean>("characterDataOldValue")
            };

            Connect(target, init);
        }

        /// <summary>
        /// Empties the MutationObserver instance's record queue and returns
        /// what was in there.
        /// </summary>
        /// <returns>Returns an Array of MutationRecords.</returns>
        [DomName("takeRecords")]
        public IEnumerable<IMutationRecord> Flush()
        {
            while (_records.Count > 0)
            {
                yield return _records.Dequeue();
            }
        }

        #endregion

        #region Options

        sealed class MutationObserving
        {
            readonly INode _target;
            readonly MutationObserverInit _options;
            readonly List<INode> _transientNodes;

            public MutationObserving(INode target, MutationObserverInit options)
            {
                _target = target;
                _options = options;
                _transientNodes = new List<INode>();
            }

            public INode Target
            {
                get { return _target; }
            }

            public MutationObserverInit Options
            {
                get { return _options; }
            }

            public List<INode> TransientNodes
            {
                get { return _transientNodes; }
            }
        }

        #endregion
    }
}
