namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using System;

    /// <summary>
    /// This interface marks elements that
    /// allow to have an attached label.
    /// </summary>
    public interface ILabelabelElement
    {
        /// <summary>
        /// Gets if labels are supported.
        /// </summary>
        Boolean SupportsLabels { get; }

        /// <summary>
        /// Gets the list of assigned labels.
        /// </summary>
        NodeList Labels { get; }
    }
}
