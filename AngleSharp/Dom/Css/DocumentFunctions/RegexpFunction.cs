namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Take a regular expression.
    /// </summary>
    sealed class RegexpFunction : DocumentFunction
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
