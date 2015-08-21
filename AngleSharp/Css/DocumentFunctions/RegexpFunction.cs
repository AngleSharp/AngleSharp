namespace AngleSharp.Css.DocumentFunctions
{
    using System;

    /// <summary>
    /// Use regular expression function.
    /// </summary>
    sealed class RegexpFunction : CssDocumentFunction
    {
        public RegexpFunction(String url)
            : base(FunctionNames.Regexp, url)
        {
        }
    }
}
