namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the fieldset HTML element.
    /// </summary>
    [DomName("HTMLFieldSetElement")]
    public interface IHtmlFieldSetElement : IHtmlFormControlElement
    {
        
        /// <summary>
        /// Gets the type of input control (fieldset).
        /// </summary>
        [DomName("type")]
        String Type { get; }

        /// <summary>
        /// Gets the elements belonging to this field set.
        /// </summary>
        [DomName("elements")]
        IHtmlFormControlsCollection Elements { get; }
    }
}
