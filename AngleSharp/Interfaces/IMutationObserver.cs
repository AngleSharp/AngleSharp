namespace AngleSharp.DOM
{
    /// <summary>
    /// MutationObserver provides developers a way to react to changes in a DOM.
    /// </summary>
    [DomName("MutationObserver")]
    interface IMutationObserver
    {
        /// <summary>
        /// Registers the MutationObserver instance to receive
        /// notifications of DOM mutations on the specified node.
        /// </summary>
        /// <param name="target">The Node on which to observe DOM mutations.</param>
        /// <param name="options">Specifies which DOM mutations should be reported.</param>
        [DomName("observe")]
        void Connect(INode target, IMutationObserverInit options);

        /// <summary>
        /// Stops the MutationObserver instance from receiving
        /// notifications of DOM mutations. Until the observe()
        /// method is used again, observer's callback will not be invoked.
        /// </summary>
        [DomName("disconnect")]
        void Disconnect();

        /// <summary>
        /// Empties the MutationObserver instance's record queue and
        /// returns what was in there.
        /// </summary>
        /// <returns>Returns an Array of MutationRecords.</returns>
        [DomName("takeRecords")]
        IMutationRecord[] Flush();
    }
}