namespace AngleSharp.DOM
{
    /// <summary>
    /// Defines the callback signature for a mutation event.
    /// </summary>
    /// <param name="mutations">The sequence of mutations.</param>
    /// <param name="observer">The observer.</param>
    [DomName("MutationCallback")]
    delegate void MutationCallback(IMutationRecord[] mutations, IMutationObserver observer);
}
