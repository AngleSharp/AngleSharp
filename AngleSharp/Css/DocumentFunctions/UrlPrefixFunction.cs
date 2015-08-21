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
    }
}
