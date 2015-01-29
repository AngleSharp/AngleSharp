namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the param HTML element.
    /// </summary>
    [DomName("HTMLParamElement")]
    public interface IHtmlParamElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        [DomName("name")]
        String Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the parameter.
        /// </summary>
        [DomName("value")]
        String Value { get; set; }
    }
}
