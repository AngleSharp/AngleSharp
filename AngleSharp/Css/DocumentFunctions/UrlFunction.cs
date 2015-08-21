namespace AngleSharp.Css.DocumentFunctions
{
    using System;

    /// <summary>
    /// Take as url function.
    /// </summary>
    sealed class UrlFunction : CssDocumentFunction
    {
        public UrlFunction(String url)
            : base(FunctionNames.Url, url)
        {
        }
    }
}
