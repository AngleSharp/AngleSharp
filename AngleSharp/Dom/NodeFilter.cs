namespace AngleSharp.Dom
{
    /// <summary>
    /// The signature for a NodeFilter callback function.
    /// </summary>
    /// <param name="node">The node to examine.</param>
    /// <returns>The result after the examination of the node.</returns>
    public delegate FilterResult NodeFilter(INode node);
}
