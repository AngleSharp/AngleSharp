namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;
    using System;

    public class CssConstructionFunctions
    {
        internal static ICssStyleSheet ParseStyleSheet(String source)
        {
            var parser = new CssParser();
            return parser.ParseStylesheet(source);
        }

        internal static ICssStyleSheet ParseStyleSheet(String source, CssParserOptions options)
        {
            var parser = new CssParser(options);
            return parser.ParseStylesheet(source);
        }

        internal static CssRule ParseRule(String source)
        {
            var parser = new CssParser();
            return parser.ParseRule(source);
        }

        internal static CssProperty ParseDeclaration(String source)
        {
            var parser = new CssParser();
            return parser.ParseDeclaration(source);
        }

        internal static CssProperty ParseDeclaration(String source, CssParserOptions options)
        {
            var parser = new CssParser(options);
            return parser.ParseDeclaration(source);
        }

        internal static CssValue ParseValue(String source)
        {
            var parser = new CssParser();
            return parser.ParseValue(source);
        }

        internal static CssStyleDeclaration ParseDeclarations(String declarations)
        {
            var parser = new CssParser();
            var style = new CssStyleDeclaration(parser);
            style.Update(declarations);
            return style;
        }

        internal static CssKeyframeRule ParseKeyframeRule(String source)
        {
            var parser = new CssParser();
            return parser.ParseKeyframeRule(source);
        }
    }
}
