namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a list of radio input elements.
    /// </summary>
    [DomName("RadioNodeList")]
    interface IRadioNodeList : INodeList
    {
        /// <summary>
        /// Gets or sets the currently selected value.
        /// </summary>
        [DomName("value")]
        String Value { get; set; }
    }
}
