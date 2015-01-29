namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the fieldset HTML element.
    /// </summary>
    [DomName("HTMLFieldSetElement")]
    public interface IHtmlFieldSetElement : IHtmlElement, IValidation
    {
        /// <summary>
        /// Gets or sets if the element is enabled or disabled.
        /// </summary>
        [DomName("disabled")]
        Boolean IsDisabled { get; set; }

        /// <summary>
        /// Gets the associated HTML form element.
        /// </summary>
        [DomName("form")]
        IHtmlFormElement Form { get; }

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        [DomName("name")]
        String Name { get; set; }

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
