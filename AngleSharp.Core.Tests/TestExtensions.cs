using System;
using System.IO;
using AngleSharp.Dom;
using NUnit.Framework;

namespace AngleSharp.Core.Tests
{
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
            return DocumentBuilder.Html(sourceCode, configuration);
        }

        public static INodeList ToHtmlFragment(this String sourceCode, IElement context = null, IConfiguration configuration = null)
        {
            return DocumentBuilder.HtmlFragment(sourceCode, context, configuration);
        }

        public static IDocument ToHtmlDocument(this Stream content, IConfiguration configuration = null)
        {
            return DocumentBuilder.Html(content, configuration);
        }
    }
}
