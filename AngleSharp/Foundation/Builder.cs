using AngleSharp.DOM;
using AngleSharp.DOM.Collections;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Html;
using System;

namespace AngleSharp
{
    /// <summary>
    /// Represents a helper to construct objects with externally
    /// defined classes and libraries.
    /// </summary>
    static class Builder
    {
        public static StyleSheet Style(HTMLLinkElement link)
        {
            //TODO
            //link.Type
            //link.Media
            //link.Href
            //link.Charset
            throw new NotImplementedException();
        }

        public static StyleSheet Style(HTMLStyleElement style)
        {
            //TODO
            //style.Type
            //style.Media
            //style.TextContent
            throw new NotImplementedException();
        }

        public static void Script(HTMLScriptElement script)
        {
            //TODO what kind of object to return here? nothing? just invoke?
            //script.Src
            //script.Charset
            //script.Type
            throw new NotImplementedException();
        }
    }
}
