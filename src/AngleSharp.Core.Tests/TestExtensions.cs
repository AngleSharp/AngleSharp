namespace AngleSharp.Core.Tests
{
    using AngleSharp.Core.Tests.External;
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Network;
    using AngleSharp.Parser.Css;
    using AngleSharp.Parser.Html;
    using AngleSharp.Parser.Xml;
    using AngleSharp.Services.Scripting;
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

        public static IConfiguration WithScripts<T>(this IConfiguration config, T scripting)
            where T : IScriptEngine
        {
            var service = new MockScriptService<T>(scripting);
            return config.With(service);
        }

        public static IConfiguration WithPageRequester(this IConfiguration config, Boolean enableNavigation = true, Boolean enableResourceLoading = false)
        {
            return config.WithDefaultLoader(setup =>
            {
                setup.IsNavigationEnabled = enableNavigation;
                setup.IsResourceLoadingEnabled = enableResourceLoading;
            }, PageRequester.All);
        }

        public static IConfiguration WithMockRequester(this IConfiguration config, Action<IRequest> onRequest = null)
        {
            var mockRequester = new MockRequester();
            mockRequester.OnRequest = onRequest;
            return config.WithMockRequester(mockRequester);
        }

        public static IConfiguration WithVirtualRequester(this IConfiguration config, Func<IRequest, IResponse> onRequest = null)
        {
            var mockRequester = new VirtualRequester(onRequest);
            return config.WithMockRequester(mockRequester);
        }

        public static IConfiguration WithMockRequester(this IConfiguration config, IRequester mockRequester)
        {
            return config.WithDefaultLoader(setup => setup.IsResourceLoadingEnabled = true, new[] { mockRequester });
        }

        public static IDocument ToHtmlDocument(this String sourceCode, IConfiguration configuration = null)
        {
            var parser = new HtmlParser(configuration ?? Configuration.Default);
            return parser.Parse(sourceCode);
        }

        public static IDocument ToXmlDocument(this String sourceCode, IConfiguration configuration = null)
        {
            var xmlParser = new XmlParser(configuration);
            return xmlParser.Parse(sourceCode);
        }

        public static ICssStyleSheet ToCssStylesheet(this String sourceCode, IConfiguration configuration = null)
        {
            var parser = new CssParser(configuration);
            return parser.ParseStylesheet(sourceCode);
        }

        public static INodeList ToHtmlFragment(this String sourceCode, IElement context = null, IConfiguration configuration = null)
        {
            var parser = new HtmlParser(configuration);
            return parser.ParseFragment(sourceCode, context);
        }

        public static IDocument ToHtmlDocument(this Stream content, IConfiguration configuration = null)
        {
            var parser = new HtmlParser(configuration);
            return parser.Parse(content);
        }

        public static ICssStyleSheet ToCssStylesheet(this Stream content, IConfiguration configuration = null)
        {
            var parser = new CssParser(configuration);
            return parser.ParseStylesheet(content);
        }
    }
}
