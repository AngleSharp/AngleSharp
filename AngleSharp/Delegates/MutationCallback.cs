namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// Defines the callback signature for a mutation event.
    /// </summary>
    /// <param name="mutations">The sequence of mutations.</param>
    /// <param name="observer">The observer.</param>
    [DomName("MutationCallback")]
    public delegate void MutationCallback(IMutationRecord[] mutations, MutationObserver observer);
}
