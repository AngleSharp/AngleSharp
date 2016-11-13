namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// The PseudoElement interface is used for representing CSS
    /// pseudo-elements.
    /// </summary>
    [DomName("PseudoElement")]
    [DomNoInterfaceObject]
    public interface IPseudoElement : IElement
    {
        /// <summary>
        /// Gets the assigned pseudo name (e.g., before).
        /// </summary>
        String PseudoName { get; }
    }
}
