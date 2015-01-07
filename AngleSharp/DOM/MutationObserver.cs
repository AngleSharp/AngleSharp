namespace AngleSharp.DOM
{
    using AngleSharp.Attributes;
    using System.Collections.Generic;

    /// <summary>
    /// MutationObserver provides developers a way to react to changes in a DOM.
    /// </summary>
    [DomName("MutationObserver")]
    public sealed class MutationObserver
    {
        #region Fields

        readonly Queue<IMutationRecord> _records;
        readonly MutationCallback _callback;
        readonly Dictionary<INode, MutationObserverInit> _observing;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new mutation observer with the provided callback.
        /// </summary>
        /// <param name="callback">The callback to trigger.</param>
        [DomConstructor]
        public MutationObserver(MutationCallback callback)
        {
            _records = new Queue<IMutationRecord>();
            _callback = callback;
            _observing = new Dictionary<INode, MutationObserverInit>();
        }

        #endregion

        #region Properties

        internal IEnumerable<INode> Nodes
        {
            get
            {
                foreach (var target in _observing)
                    yield return target.Key;
            }
        }

        #endregion

        #region Methods

        internal void Enqueue(MutationRecord record)
        {
            _records.Enqueue(record);
        }

        internal void TriggerWith(IMutationRecord[] records)
        {
            _callback(records, this);
        }

        internal MutationObserverInit OptionsFor(INode node)
        {
            MutationObserverInit result;
            _observing.TryGetValue(node, out result);
            return result;
        }

        /// <summary>
        /// Stops the MutationObserver instance from receiving
        /// notifications of DOM mutations. Until the observe()
        /// method is used again, observer's callback will not be invoked.
        /// </summary>
        [DomName("disconnect")]
        public void Disconnect()
        {
            foreach (var key in _observing.Keys)
            {
                var node = (Node)key;
                node.Owner.Mutations.Unregister(this);
            }

            _records.Clear();

        }

        /// <summary>
        /// Registers the MutationObserver instance to receive
        /// notifications of DOM mutations on the specified node.
        /// </summary>
        /// <param name="target">The Node on which to observe DOM mutations.</param>
        /// <param name="options">Specifies which DOM mutations should be reported.</param>
        [DomName("observe")]
        public void Connect(INode target, MutationObserverInit options)
        {
            var node = target as Node;

            if (node == null)
                return;

            node.Owner.Mutations.Register(this);

            if (options.StorePreviousDataValue.HasValue == false)
                options.StorePreviousDataValue = false;

            if (options.StorePreviousAttributeValue.HasValue == false)
                options.StorePreviousAttributeValue = false;

            if (options.ObserveTargetAttributes.HasValue == false)
                options.ObserveTargetAttributes = options.StorePreviousAttributeValue.Value || options.AttributeFilters != null;

            if (options.ObserveTargetData.HasValue == false)
                options.ObserveTargetData = options.StorePreviousDataValue.HasValue && options.StorePreviousDataValue.Value;

            if (options.StorePreviousAttributeValue.Value && options.ObserveTargetAttributes.Value == false)
                throw new DomException(ErrorCode.TypeMismatch);

            if (options.AttributeFilters != null && options.ObserveTargetAttributes.Value == false)
                throw new DomException(ErrorCode.TypeMismatch);

            if (options.StorePreviousDataValue.Value && options.ObserveTargetData.Value == false)
                throw new DomException(ErrorCode.TypeMismatch);

            if (_observing.ContainsKey(target))
            {
                //TODO Mutation
                //6.1 Remove all transient registered observers whose source is registered. 
            }

            _observing[target] = options;
        }

        /// <summary>
        /// Empties the MutationObserver instance's record queue and
        /// returns what was in there.
        /// </summary>
        /// <returns>Returns an Array of MutationRecords.</returns>
        [DomName("takeRecords")]
        public IEnumerable<IMutationRecord> Flush()
        {
            while (_records.Count != 0)
                yield return _records.Dequeue();
        }

        #endregion
    }
}
