namespace AngleSharp.Css.DocumentFunctions
{
    using System;

    /// <summary>
    /// Take as domain.
    /// </summary>
    sealed class DomainFunction : CssDocumentFunction
    {
        public DomainFunction(String url)
            : base(FunctionNames.Domain, url)
        {
        }
    }
}
