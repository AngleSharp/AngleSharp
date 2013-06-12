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
        public static StyleSheet Style(HTMLLinkElement link)
        {
            //TODO Read content from ext. file
            var sheet = CssParser.ParseStyleSheet(string.Empty);
            sheet.Media.MediaText = link.Media;
            sheet.OwnerNode = link;
            return sheet;
        }

        public static StyleSheet Style(HTMLStyleElement style)
        {
            var sheet = CssParser.ParseStyleSheet(style.TextContent);
            sheet.Media.MediaText = style.Media;
            sheet.OwnerNode = style;
            return sheet;
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
