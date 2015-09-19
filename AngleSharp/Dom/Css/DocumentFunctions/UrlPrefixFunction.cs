namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Take an url prefix.
    /// </summary>
    sealed class UrlPrefixFunction : DocumentFunction
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
