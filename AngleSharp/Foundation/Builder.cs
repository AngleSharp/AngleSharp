using AngleSharp.Css;
using AngleSharp.DOM;
using AngleSharp.DOM.Collections;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Html;
using System;
using System.IO;

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
            var parser = new CssParser(sheet, source);
            parser.Parse();
        }

        public static void StyleFromUrl(CSSStyleSheet sheet, String url)
        {
            //TODO we need the IoC container to resolve stream getters etc.
            return;

            var stream = Stream(url);
            var parser = new CssParser(sheet, stream);
            parser.Parse();
        }

        public static void Script(HTMLScriptElement script)
        {
            //TODO what kind of object to return here? nothing? just invoke?
            //script.Src
            //script.Charset
            //script.Type
            throw new NotImplementedException();
        }

        public static Stream Stream(String url)
        {
            throw new NotImplementedException();
        }

        public static Stream Stream(Uri url)
        {
            throw new NotImplementedException();
        }
    }
}
