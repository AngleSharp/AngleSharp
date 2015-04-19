namespace AngleSharp.Core.Tests
{
    using System;
    using System.IO;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;
    using AngleSharp.Parser.Html;
    using AngleSharp.Parser.Xml;
    using NUnit.Framework;

    static class TestExtensions
    {
        public static String GetTagName(this INode node)
        {
            var element = node as IElement;

            Assert.AreEqual(NodeType.Element, node.NodeType);
            Assert.IsNotNull(element);
            Assert.IsNull(element.Prefix);

            return element.LocalName;
        }

        public static IDocument ToHtmlDocument(this String sourceCode, IConfiguration configuration = null)
        {
            var parser = new HtmlParser(sourceCode, configuration);
            parser.Parse();
            return parser.Result;
        }

        public static IDocument ToXmlDocument(this String sourceCode, IConfiguration configuration = null)
        {
            var xmlParser = new XmlParser(sourceCode, configuration);
            xmlParser.Parse();
            return xmlParser.Result;
        }

        public static ICssStyleSheet ToCssStylesheet(this String sourceCode, IConfiguration configuration = null)
        {
            var parser = new CssParser(sourceCode, configuration);
            parser.Parse();
            return parser.Result;
        }

        public static INodeList ToHtmlFragment(this String sourceCode, IElement context = null, IConfiguration configuration = null)
        {
            return DocumentBuilder.HtmlFragment(sourceCode, context, configuration);
        }

        public static IDocument ToHtmlDocument(this Stream content, IConfiguration configuration = null)
        {
            var parser = new HtmlParser(content, configuration);
            parser.Parse();
            return parser.Result;
        }

        public static ICssStyleSheet ToCssStylesheet(this Stream content, IConfiguration configuration = null)
        {
            var parser = new CssParser(content, configuration);
            parser.Parse();
            return parser.Result;
        }
    }
}
