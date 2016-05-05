namespace AngleSharp.Dom
{
    /// <summary>
    /// Defines the callback signature for a mutation event.
    /// </summary>
    /// <param name="mutations">The sequence of mutations.</param>
    /// <param name="observer">The observer.</param>
    public delegate void MutationCallback(IMutationRecord[] mutations, MutationObserver observer);
}
