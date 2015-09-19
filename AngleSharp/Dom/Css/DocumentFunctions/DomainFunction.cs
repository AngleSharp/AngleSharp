namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Take a domain.
    /// </summary>
    sealed class DomainFunction : DocumentFunction
    {
        readonly String _subdomain;

        public DomainFunction(String url)
            : base(FunctionNames.Domain, url)
        {
            _subdomain = "." + url;
        }

        public override Boolean Matches(Url url)
        {
            var domain = url.HostName;
            return domain.Isi(Data) || domain.EndsWith(_subdomain, StringComparison.OrdinalIgnoreCase);
        }
    }
}
