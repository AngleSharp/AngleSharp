using AngleSharp.Css;
using AngleSharp.DOM;
using AngleSharp.DOM.Collections;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Html;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AngleSharp
{
    /// <summary>
    /// Represents a helper to construct objects with externally
    /// defined classes and libraries.
    /// </summary>
    static class Builder
    {
        public static void StyleFromSource(CSSStyleSheet sheet, String source)
        {
            if (sheet.Options.IsStyling)
            {
                var parser = new CssParser(sheet, source);
                parser.Parse();
            }
        }

        public static async Task StyleFromUrl(CSSStyleSheet sheet, String url, CancellationToken cancel)
        {
            if (sheet.Options.IsStyling && Configuration.UseDefaultHttpRequester)
            {
                var stream = await GetFromUrl(url, cancel);
                var parser = new CssParser(sheet, stream);
                parser.Parse();
            }
        }

        public static void Script(HTMLScriptElement script)
        {
            //TODO what kind of object to return here? nothing? just invoke?
            //script.Src
            //script.Charset
            //script.Type
            throw new NotImplementedException();
        }

        public static Task<Stream> GetFromUrl(String url)
        {
            return GetFromUrl(url, CancellationToken.None);
        }

        public static Task<Stream> GetFromUrl(Uri url)
        {
            return GetFromUrl(url, CancellationToken.None);
        }

        public static Task<Stream> GetFromUrl(String url, CancellationToken cancel)
        {
            Uri uri;

            if (Uri.TryCreate(url, UriKind.Absolute, out uri))
                return GetFromUrl(uri, cancel);

            throw new ArgumentException("The given URL is not valid as an absolute URL.");
        }

        public static async Task<Stream> GetFromUrl(Uri url, CancellationToken cancel)
        {
            var requester = Configuration.GetHttpRequester();

            if (requester == null)
                throw new NullReferenceException("No HTTP requester has been set up. Configure one with the AngleSharp.Configuration class (e.g. set UseDefaultHttpRequester = true).");

            var request = new DefaultHttpRequest
            {
                Address = url,
                Method = HttpMethod.GET
            };

            var response = await requester.RequestAsync(request, cancel);
            return response.Content;
        }
    }
}
