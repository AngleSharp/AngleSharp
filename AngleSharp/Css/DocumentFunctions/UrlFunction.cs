namespace AngleSharp.Css.DocumentFunctions
{
    using System;

    /// <summary>
    /// Take as url function.
    /// </summary>
    sealed class UrlFunction : CssDocumentFunction
    {
        readonly Url _expected;

        public UrlFunction(String url)
            : base(FunctionNames.Url, url)
        {
            _expected = Url.Create(Data);
        }

        public override Boolean Matches(Url actual)
        {
            return !_expected.IsInvalid && _expected.Equals(actual);
        }
    }
}
