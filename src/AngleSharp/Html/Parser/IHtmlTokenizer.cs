namespace AngleSharp.Html.Parser
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom.Events;
    using AngleSharp.Html.Parser.Tokens;
    using AngleSharp.Text;
    using System;
    using Common;

    internal interface IHtmlTokenizer
    {
        /// <summary>
        /// Gets the next available token.
        /// </summary>
        /// <returns>The next available token.</returns>
        HtmlToken Get();

        /// <summary>
        /// Fired in case of a parse error.
        /// </summary>
        event EventHandler<HtmlErrorEvent>? Error;

        void RaiseErrorOccurred(HtmlParseError code, TextPosition tokenPosition);

        HtmlParseMode State { get; set; }

        Boolean IsAcceptingCharacterData { get; set; }
        Boolean IsStrictMode { get; set; }
        Boolean IsSupportingProcessingInstructions { get; set; }
        Boolean IsNotConsumingCharacterReferences { get; set; }
        bool IsPreservingAttributeNames { get; set; }
    }

}
