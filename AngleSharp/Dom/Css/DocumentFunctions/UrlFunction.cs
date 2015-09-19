namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Take an url.
    /// </summary>
    sealed class UrlFunction : DocumentFunction
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
