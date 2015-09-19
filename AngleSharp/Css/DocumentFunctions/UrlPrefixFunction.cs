namespace AngleSharp.Css.DocumentFunctions
{
    using System;

    /// <summary>
    /// Take as a url prefix function.
    /// </summary>
    sealed class UrlPrefixFunction : CssDocumentFunction
    {
        public UrlPrefixFunction(String url)
            : base(FunctionNames.UrlPrefix, url)
        {
        }

        public override Boolean Matches(Url url)
        {
            return url.Href.StartsWith(Data, StringComparison.OrdinalIgnoreCase);
        }
    }
}
