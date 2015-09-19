namespace AngleSharp.Css.DocumentFunctions
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Use regular expression function.
    /// </summary>
    sealed class RegexpFunction : CssDocumentFunction
    {
        readonly Regex _regex;

        public RegexpFunction(String url)
            : base(FunctionNames.Regexp, url)
        {
            _regex = new Regex(url, RegexOptions.ECMAScript | RegexOptions.CultureInvariant);
        }

        public override Boolean Matches(Url url)
        {
            return _regex.IsMatch(url.Href);
        }
    }
}
