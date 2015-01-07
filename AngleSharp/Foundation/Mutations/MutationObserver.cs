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
        readonly List<INode> _observing;

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
            _observing = new List<INode>();
        }

        #endregion

        #region Methods

        internal void Enqueue(MutationRecord record)
        {
            //TODO Mutation
            //1. Let interested observers be an initially empty set of MutationObserver objects optionally paired with a string. 
            //2. Let nodes be the inclusive ancestors of target. 
            //3. Then, for each node in nodes, and then for each registered observer (with registered observer's options as options) in node's list of registered observers: 
            //3.1 If node is not target and options's subtree is false, continue. 
            //3.2 If type is "attributes" and options's attributes is not true, continue. 
            //3.3 If type is "attributes", options's attributeFilter is present, and either options's attributeFilter does not contain name or namespace is non-null, continue. 
            //3.4 If type is "characterData" and options's characterData is not true, continue. 
            //3.5 If type is "childList" and options's childList is false, continue. 
            //3.6 If registered observer's observer is not in interested observers, append registered observer's observer to interested observers. 
            //3.7 If either type is "attributes" and options's attributeOldValue is true, or type is "characterData" and options's characterDataOldValue is true, set the paired string of registered observer's observer in interested observers to oldValue. 
            //4. Then, for each observer in interested observers: 
            //4.1 Let record be a new MutationRecord object with its type set to type and target set to target. 
            //4.2 If name and namespace are given, set record's attributeName to name, and record's attributeNamespace to namespace. 
            //4.3 If addedNodes is given, set record's addedNodes to addedNodes. 
            //4.4 If removedNodes is given, set record's removedNodes to removedNodes, 
            //4.5 If previousSibling is given, set record's previousSibling to previousSibling. 
            //4.6 If nextSibling is given, set record's nextSibling to nextSibling. 
            //4.7 If observer has a paired string, set record's oldValue to observer's paired string. 
            //4.8 Append record to observer's record queue. 
            //5. Queue a mutation observer compound microtask.
        }

        internal void TriggerWith(IMutationRecord[] records)
        {
            _callback(records, this);
        }

        /// <summary>
        /// Stops the MutationObserver instance from receiving
        /// notifications of DOM mutations. Until the observe()
        /// method is used again, observer's callback will not be invoked.
        /// </summary>
        [DomName("disconnect")]
        public void Disconnect()
        {
            //TODO Mutation
            
            //The disconnect() method must, for each node node in the context object's list of nodes,
            //remove any registered observer on node for which the context object is the observer, and
            //also empty context object's record queue. 
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
            //TODO Mutation

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

            if (_observing.Contains(target))
            {
                //6.1 Remove all transient registered observers whose source is registered. 
                //6.2 Replace registered's options with options. 
            }
            else
            {
                //7. Otherwise, add a new registered observer to target's list of registered
                //observers with the context object as the observer and options as the options,
                //and add target to context object's list of nodes on which it is registered.
                _observing.Add(target);
            }
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
