namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the param HTML element.
    /// </summary>
    [DomName("HTMLParamElement")]
    public interface IHtmlParamElement : IHtmlElement
    {
        [DomName("name")]
        String Name { get; set; }

        [DomName("value")]
        String Value { get; set; }
    }
}
