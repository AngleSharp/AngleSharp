namespace AngleSharp.Core.Tests
{
    using AngleSharp.Core.Tests.External;
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using AngleSharp.Io;
    using AngleSharp.Scripting;
    using AngleSharp.Xml.Parser;
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

        public static IConfiguration WithScripting(this IConfiguration config)
        {
            var service = new CallbackScriptEngine(options => { }, MimeTypeNames.DefaultJavaScript);
            return config.With(service);
        }

        public static IConfiguration WithScripts<T>(this IConfiguration config, T scripting)
            where T : IScriptingService
        {
            return config.With(scripting);
        }

        public static IConfiguration WithPageRequester(this IConfiguration config, Boolean enableNavigation = true, Boolean enableResourceLoading = false)
        {
            return config.With(new PageRequester()).WithDefaultLoader(new LoaderOptions {
                IsResourceLoadingEnabled = enableResourceLoading,
                IsNavigationDisabled = !enableNavigation
            });
        }

        public static IConfiguration WithMockRequester(this IConfiguration config, Action<Request> onRequest = null)
        {
            var mockRequester = new MockRequester { OnRequest = onRequest };
            return config.WithMockRequester(mockRequester);
        }

        public static IConfiguration WithVirtualRequester(this IConfiguration config, Func<Request, IResponse> onRequest = null)
        {
            var mockRequester = new VirtualRequester(onRequest);
            return config.WithMockRequester(mockRequester);
        }

        public static IConfiguration WithMockRequester(this IConfiguration config, IRequester mockRequester)
        {
            return config.With(mockRequester).WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true });
        }

        public static IDocument ToHtmlDocument(this String sourceCode, IConfiguration configuration = null)
        {
            var context = BrowsingContext.New(configuration ?? Configuration.Default);
            var htmlParser = context.GetService<IHtmlParser>();
            return htmlParser.ParseDocument(sourceCode);
        }

        public static IDocument ToXmlDocument(this String sourceCode, IConfiguration configuration = null)
        {
            var context = BrowsingContext.New(configuration);
            var xmlParser = context.GetService<IXmlParser>();
            return xmlParser.ParseDocument(sourceCode);
        }

        public static INodeList ToHtmlFragment(this String sourceCode, IElement contextElement = null, IConfiguration configuration = null)
        {
            var context = BrowsingContext.New(configuration);
            var htmlParser = context.GetService<IHtmlParser>();
            return htmlParser.ParseFragment(sourceCode, contextElement);
        }

        public static INodeList ToHtmlFragment(this String sourceCode, String contextElement, IConfiguration configuration = null)
        {
            var doc = String.Empty.ToHtmlDocument();
            var element = doc.CreateElement(contextElement);
            return sourceCode.ToHtmlFragment(element, configuration);
        }

        public static IDocument ToHtmlDocument(this Stream content, IConfiguration configuration = null)
        {
            var context = BrowsingContext.New(configuration);
            var htmlParser = context.GetService<IHtmlParser>();
            return htmlParser.ParseDocument(content);
        }
    }
}
