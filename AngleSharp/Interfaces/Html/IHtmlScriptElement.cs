namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the script HTML element.
    /// </summary>
    [DomName("HTMLScriptElement")]
    public interface IHtmlScriptElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the source URL of the script.
        /// </summary>
        [DomName("src")]
        String Source { get; set; }

        /// <summary>
        /// Gets or sets if the script should be run asynchronously.
        /// </summary>
        [DomName("async")]
        Boolean IsAsync { get; set; }

        /// <summary>
        /// Gets or sets if script execution should be deferred.
        /// </summary>
        [DomName("defer")]
        Boolean IsDeferred { get; set; }

        /// <summary>
        /// Gets or sets the type of script.
        /// </summary>
        [DomName("type")]
        String Type { get; set; }

        /// <summary>
        /// Gets or sets the character set of the script.
        /// </summary>
        [DomName("charset")]
        String CharacterSet { get; set; }

        /// <summary>
        /// Gets or sets the cross-origin attribute.
        /// </summary>
        [DomName("crossOrigin")]
        String CrossOrigin { get; set; }

        /// <summary>
        /// Gets or sets the script's source code.
        /// </summary>
        [DomName("text")]
        String Text { get; set; }
    }
}
