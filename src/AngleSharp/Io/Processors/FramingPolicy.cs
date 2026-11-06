namespace AngleSharp.Io.Processors
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// What a response says about being shown in a frame: <c>frame-ancestors</c> of its
    /// Content-Security-Policy, or else its X-Frame-Options, which apply to every document around
    /// the frame up to the top, so that a page in between can't be used to get around them.
    /// </summary>
    static class FramingPolicy
    {
        private const String ContentSecurityPolicy = "Content-Security-Policy";
        private const String FrameAncestors = "frame-ancestors";
        private const String XFrameOptions = "X-Frame-Options";

        public static Boolean IsAllowed(IResponse response, IDocument? embedder)
        {
            var address = response.Address;
            var ancestors = GetAncestors(embedder).ToList();
            var policies = GetHeaderValues(response, ContentSecurityPolicy)
                .SelectMany(value => value.Split(','))
                .Select(GetFrameAncestors)
                .Where(sources => sources is not null)
                .ToList();

            // takes precedence over the older header (CSP 3, 6.3.2.3)
            if (policies.Count > 0)
            {
                return policies.All(sources => ancestors.All(ancestor => Matches(sources!, ancestor, address)));
            }

            return GetHeaderValues(response, XFrameOptions)
                .SelectMany(value => value.Split(','))
                .Select(value => value.Trim())
                .All(value => IsAllowedByFrameOptions(value, ancestors, address));
        }

        private static Boolean IsAllowedByFrameOptions(String value, List<Url> ancestors, Url address)
        {
            if (value.Isi("DENY"))
            {
                return false;
            }

            if (value.Isi("SAMEORIGIN"))
            {
                return ancestors.All(ancestor => IsSameOrigin(ancestor, address));
            }

            // anything else (including the retired ALLOW-FROM) is invalid, and ignored
            return true;
        }

        private static IEnumerable<String> GetHeaderValues(IResponse response, String name) =>
            response.Headers
                .Where(header => header.Key.Isi(name))
                .Select(header => header.Value)
                .Where(value => value is not null);

        private static String[]? GetFrameAncestors(String policy)
        {
            foreach (var directive in policy.Split(';'))
            {
                var tokens = directive.SplitSpaces();

                if (tokens.Length > 0 && tokens[0].Isi(FrameAncestors))
                {
                    return tokens.Skip(1).ToArray();
                }
            }

            return null;
        }

        // the addresses of the documents around the frame, as far as they have one
        private static IEnumerable<Url> GetAncestors(IDocument? document)
        {
            for (var current = document; current is not null; current = current.Context?.Parent?.Active)
            {
                var address = new Url(current.DocumentUri ?? String.Empty);

                if (!address.IsInvalid && address.Origin is { Length: > 0 })
                {
                    yield return address;
                }
            }
        }

        private static Boolean IsSameOrigin(Url first, Url second) =>
            first.Origin is { Length: > 0 } origin && origin.Is(second.Origin);

        private static Boolean Matches(String[] sources, Url ancestor, Url self) =>
            sources.Any(source => MatchesSource(source, ancestor, self));

        private static Boolean MatchesSource(String source, Url ancestor, Url self)
        {
            if (source.Is("*"))
            {
                return true;
            }

            if (source.Isi("'self'"))
            {
                return IsSameOrigin(ancestor, self);
            }

            // 'none' and any other keyword match nothing
            if (source.StartsWith("'", StringComparison.Ordinal))
            {
                return false;
            }

            // a scheme, e.g. https:
            if (source.EndsWith(":", StringComparison.Ordinal))
            {
                return ancestor.Scheme.Isi(source.Substring(0, source.Length - 1));
            }

            var rest = source;
            var schemeEnd = rest.IndexOf("://", StringComparison.Ordinal);

            if (schemeEnd >= 0)
            {
                if (!ancestor.Scheme.Isi(rest.Substring(0, schemeEnd)))
                {
                    return false;
                }

                rest = rest.Substring(schemeEnd + 3);
            }
            else if (!ancestor.Scheme.Isi(self.Scheme) && !(self.Scheme.Isi("http") && ancestor.Scheme.Isi("https")))
            {
                return false;
            }

            var pathStart = rest.IndexOf('/');
            rest = pathStart >= 0 ? rest.Substring(0, pathStart) : rest;
            var portStart = rest.LastIndexOf(':');
            var port = portStart >= 0 ? rest.Substring(portStart + 1) : null;
            var host = portStart >= 0 ? rest.Substring(0, portStart) : rest;

            if (port is not null && !port.Is("*") && !port.Is(ancestor.Port))
            {
                return false;
            }

            return host.StartsWith("*.", StringComparison.Ordinal)
                ? (ancestor.HostName ?? String.Empty).EndsWith(host.Substring(1), StringComparison.OrdinalIgnoreCase)
                : host.Isi(ancestor.HostName);
        }
    }
}
