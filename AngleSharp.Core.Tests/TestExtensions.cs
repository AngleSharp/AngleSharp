namespace AngleSharp.Core.Tests
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Dom.Html;
    using AngleSharp.Parser.Css;
    using AngleSharp.Parser.Html;
    using AngleSharp.Parser.Xml;
    using NUnit.Framework;
    using System;
    using System.IO;

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
            var parser = new HtmlParser(configuration ?? Configuration.Default);
            return parser.Parse(sourceCode);
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
            var ctx = BrowsingContext.New(configuration);
            var source = new TextSource(sourceCode);
            var document = new HtmlDocument(ctx, source);
            var parser = new HtmlDomBuilder(sourceCode, configuration);

            if (context != null)
            {
                return parser.ParseFragment(context).DocumentElement.ChildNodes;
            }
            else
            {
                return parser.Parse(default(HtmlParserOptions)).ChildNodes;
            }
        }

        public static IDocument ToHtmlDocument(this Stream content, IConfiguration configuration = null)
        {
            var parser = new HtmlParser(configuration ?? Configuration.Default);
            return parser.Parse(content);
        }

        public static ICssStyleSheet ToCssStylesheet(this Stream content, IConfiguration configuration = null)
        {
            var parser = new CssParser(content, configuration);
            parser.Parse();
            return parser.Result;
        }
    }
}
