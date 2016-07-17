namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
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
                throw new ArgumentNullException(nameof(callback));

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
                    {
                        return observing;
                    }
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
            {
                TriggerWith(records);
            }
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
        internal MutationOptions ResolveOptions(INode node)
        {
            foreach (var observing in _observing)
            {
                if (Object.ReferenceEquals(observing.Target, node) || observing.TransientNodes.Contains(node))
                {
                    return observing.Options;
                }
            }

            return default(MutationOptions);
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
            {
                obs.TransientNodes.Add(node);
            }
        }

        /// <summary>
        /// Clears all transient observers.
        /// </summary>
        internal void ClearTransients()
        {
            foreach (var observing in _observing)
            {
                observing.TransientNodes.Clear();
            }
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
        /// <param name="childList">
        /// If additions and removals of the target node's child elements
        /// (including text nodes) are to be observed.
        /// </param>
        /// <param name="subtree">
        /// If mutations to not just target, but also target's descendants are
        /// to be observed.
        /// </param>
        /// <param name="attributes">
        /// If mutations to target's attributes are to be observed.
        /// </param>
        /// <param name="characterData">
        /// If mutations to target's data are to be observed.
        /// </param>
        /// <param name="attributeOldValue">
        /// If attributes is set to true and target's attribute value before
        /// the mutation needs to be recorded.
        /// </param>
        /// <param name="characterDataOldValue">
        /// If characterData is set to true and target's data before the
        /// mutation needs to be recorded.
        /// </param>
        /// <param name="attributeFilter">
        /// The attributes to observe. If this is not set, then all attributes
        /// are being observed.
        /// </param>
        [DomName("observe")]
        [DomInitDict(offset: 1)]
        public void Connect(INode target, Boolean childList = false, Boolean subtree = false, Boolean? attributes = null, Boolean? characterData = null, Boolean? attributeOldValue = null, Boolean? characterDataOldValue = null, IEnumerable<String> attributeFilter = null)
        {
            var node = target as Node;

            if (node == null)
            {
                return;
            }

            var oldCharacterData = characterDataOldValue ?? false;
            var oldAttributeValue = attributeOldValue ?? false;

            var options = new MutationOptions
            {
                IsObservingChildNodes = childList,
                IsObservingSubtree = subtree,
                IsExaminingOldCharacterData = oldCharacterData,
                IsExaminingOldAttributeValue = oldAttributeValue,
                IsObservingCharacterData = characterData ?? oldCharacterData,
                IsObservingAttributes = attributes ?? (oldAttributeValue || attributeFilter != null),
                AttributeFilters = attributeFilter
            };

            if (options.IsExaminingOldAttributeValue && !options.IsObservingAttributes)
                throw new DomException(DomError.TypeMismatch);

            if (options.AttributeFilters != null && !options.IsObservingAttributes)
                throw new DomException(DomError.TypeMismatch);

            if (options.IsExaminingOldCharacterData && !options.IsObservingCharacterData)
                throw new DomException(DomError.TypeMismatch);

            if (options.IsInvalid)
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

        internal struct MutationOptions
        {
            public Boolean IsObservingChildNodes;
            public Boolean IsObservingSubtree;
            public Boolean IsObservingCharacterData;
            public Boolean IsObservingAttributes;
            public Boolean IsExaminingOldCharacterData;
            public Boolean IsExaminingOldAttributeValue;
            public IEnumerable<String> AttributeFilters;

            public Boolean IsInvalid
            {
                get { return !IsObservingAttributes && !IsObservingCharacterData && !IsObservingChildNodes; }
            }
        }

        sealed class MutationObserving
        {
            readonly INode _target;
            readonly MutationOptions _options;
            readonly List<INode> _transientNodes;

            public MutationObserving(INode target, MutationOptions options)
            {
                _target = target;
                _options = options;
                _transientNodes = new List<INode>();
            }

            public INode Target
            {
                get { return _target; }
            }

            public MutationOptions Options
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
