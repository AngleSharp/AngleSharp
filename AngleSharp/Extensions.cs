namespace AngleSharp
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Css;
    using AngleSharp.DOM.Html;
    using AngleSharp.Network;
    using AngleSharp.Parser.Css;
    using AngleSharp.Parser.Html;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A set of useful extension methods when dealing with the DOM.
    /// </summary>
    public static class Extensions
    {
        #region Generic extensions

        /// <summary>
        /// Creates an element of the given type or returns null, if there is
        /// no such type.
        /// </summary>
        /// <typeparam name="TElement">The type of element to create.</typeparam>
        /// <param name="document">The responsible document.</param>
        /// <returns>The new element, if available.</returns>
        public static TElement CreateElement<TElement>(this IDocument document)
            where TElement : IElement
        {
            var type = typeof(Extensions).GetAssembly().GetTypes()
                .Where(m => m.Implements<TElement>())
                .FirstOrDefault(m => !m.IsAbstractClass());

            if (type == null)
                return default(TElement);

            var ctor = type.GetConstructor();

            if (ctor == null)
                return default(TElement);

            var element = (TElement)ctor.Invoke(null);
            var el = element as Element;

            if (element != null)
                document.Adopt(element);

            if (el != null)
                el.Close();

            return element;
        }

        #endregion

        #region jQuery like

        /// <summary>
        /// Sets the specified attribute name to the specified value for all
        /// elements in the given collection.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <param name="attributeValue">The value of the attribute.</param>
        /// <returns>The collection itself.</returns>
        public static T Attr<T>(this T elements, String attributeName, String attributeValue)
            where T : IEnumerable<IElement>
        {
            foreach (var element in elements)
                element.SetAttribute(attributeName, attributeValue);

            return elements;
        }

        /// <summary>
        /// Empties all provided elements.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <returns>The collection itself.</returns>
        public static T Empty<T>(this T elements)
            where T : IEnumerable<IElement>
        {
            foreach (var element in elements)
                element.InnerHtml = String.Empty;

            return elements;
        }

        /// <summary>
        /// Extends the CSS of the given elements with the specified declarations.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="declarations">The declarations to apply in the inline CSS.</param>
        /// <returns>The collection itself.</returns>
        public static T Css<T>(this T elements, String declarations)
            where T : IEnumerable<IElement>
        {
            var decls = CssParser.ParseDeclarations(declarations);

            foreach (var element in elements.OfType<IHtmlElement>())
            {
                foreach (var decl in decls)
                    element.Style.SetProperty(decl.Name, decl.Value.CssText);
            }

            return elements;
        }

        /// <summary>
        /// Sets the inner HTML of the given elements.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="html">The source code of the inner HTML to set.</param>
        /// <returns>The collection itself.</returns>
        public static T Html<T>(this T elements, String html)
            where T : IEnumerable<IElement>
        {
            foreach (var element in elements)
                element.InnerHtml = html;

            return elements;
        }

        /// <summary>
        /// Sets the text content of the given elements.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="text">The text that should be set.</param>
        /// <returns>The collection itself.</returns>
        public static T Text<T>(this T elements, String text)
            where T : IEnumerable<IElement>
        {
            foreach (var element in elements)
                element.TextContent = text;

            return elements;
        }

        /// <summary>
        /// Gets the index of the given item in the list of elements.
        /// </summary>
        /// <typeparam name="T">The element type of the list of elements.</typeparam>
        /// <param name="elements">The source list of elements.</param>
        /// <param name="item">The item to search for.</param>
        /// <returns>The index of the item or -1 if not found.</returns>
        public static Int32 Index<T>(this IEnumerable<T> elements, T item)
            where T : INode
        {
            if (item != null)
            {
                int i = 0;

                foreach (var element in elements)
                {
                    if (Object.ReferenceEquals(element, item))
                        return i;

                    i++;
                }
            }

            return -1;
        }

        #endregion

        #region XPath

        /// <summary>
        /// Returns a list of the elements within the document (using depth-first pre-order traversal
        /// of the document's nodes) that match the specified XPath.
        /// </summary>
        /// <param name="document">The document to use as starting point.</param>
        /// <param name="xpath">A string containing a valid XPath query.</param>
        /// <returns>A HTMLCollection with all elements that match the selection.</returns>
        public static IHtmlCollection QueryXpath(this IDocument document, String xpath)
        {
            return document.ChildNodes.QueryXpath(xpath);
        }

        /// <summary>
        /// Returns a list of the elements within the element (using depth-first pre-order traversal
        /// of the element's nodes) that match the specified XPath.
        /// </summary>
        /// <param name="element">The element to use as starting point.</param>
        /// <param name="xpath">A string containing a valid XPath query.</param>
        /// <returns>A HTMLCollection with all elements that match the selection.</returns>
        public static IHtmlCollection QueryXpath(this IElement element, String xpath)
        {
            return element.ChildNodes.QueryXpath(xpath);
        }

        /// <summary>
        /// Returns a list of the elements for the given list of elements that match the specified XPath.
        /// </summary>
        /// <param name="nodes">The nodes to search in (first order children).</param>
        /// <param name="xpath">A string containing a valid XPath query.</param>
        /// <returns>A HTMLCollection with all elements that match the selection.</returns>
        public static IHtmlCollection QueryXpath(this INodeList nodes, String xpath)
        {
            throw new NotImplementedException("XPath queries will be supported in the future (maybe in v0.7!). Stay tuned!");
        }

        #endregion

        #region Construction helpers

        /// <summary>
        /// Interprets the string as HTML source code and returns new HTMLDocument
        /// with the DOM representation.
        /// </summary>
        /// <param name="content">The string to use as source code.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The HTML document.</returns>
        public static IDocument ParseHtml(this String content, IConfiguration configuration = null)
        {
            return DocumentBuilder.Html(content, configuration);
        }

        /// <summary>
        /// Interprets the string as CSS source code and returns new CSSStyleSheet
        /// with the CSS-OM representation.
        /// </summary>
        /// <param name="content">The string to use as source code.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The CSS stylesheet.</returns>
        public static ICssStyleSheet ParseCss(this String content, IConfiguration configuration = null)
        {
            return DocumentBuilder.Css(content, configuration);
        }

        /// <summary>
        /// Uses the URL to download the content, parse it as HTML and returning
        /// a new HTMLDocument with the DOM representation.
        /// </summary>
        /// <param name="uri">The source of the HTML content.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The HTML document.</returns>
        public static IDocument GetHtml(this Uri uri, IConfiguration configuration = null)
        {
            return DocumentBuilder.Html(uri, configuration);
        }

        /// <summary>
        /// Uses the URL to download the content, parse it as CSS and returning
        /// a new CSSStyleSheet with the CSS-OM representation.
        /// </summary>
        /// <param name="uri">The source of the CSS content.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The CSS stylesheet.</returns>
        public static ICssStyleSheet GetCss(this Uri uri, IConfiguration configuration = null)
        {
            return DocumentBuilder.Css(uri, configuration);
        }

        /// <summary>
        /// Uses the URL to download the content asynchronously, parse it as HTML and returning
        /// a new HTMLDocument with the DOM representation.
        /// </summary>
        /// <param name="uri">The source of the HTML content.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The HTML document.</returns>
        public static Task<IDocument> GetHtmlAsync(this Uri uri, IConfiguration configuration = null)
        {
            return DocumentBuilder.HtmlAsync(uri, configuration);
        }

        /// <summary>
        /// Uses the URL to download the content asynchronously, parse it as HTML and returning
        /// a new HTMLDocument with the DOM representation.
        /// </summary>
        /// <param name="uri">The source of the HTML content.</param>
        /// <param name="cancel">The cancellation token for aborting the download.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The HTML document.</returns>
        public static Task<IDocument> GetHtmlAsync(this Uri uri, CancellationToken cancel, IConfiguration configuration = null)
        {
            return DocumentBuilder.HtmlAsync(uri, cancel, configuration);
        }

        /// <summary>
        /// Uses the URL to download the content asynchronously, parse it as CSS and returning
        /// a new CSSStyleSheet with the CSS-OM representation.
        /// </summary>
        /// <param name="uri">The source of the CSS content.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The CSS stylesheet.</returns>
        public static Task<ICssStyleSheet> GetCssAsync(this Uri uri, IConfiguration configuration = null)
        {
            return DocumentBuilder.CssAsync(uri, configuration);
        }

        /// <summary>
        /// Uses the URL to download the content asynchronously, parse it as CSS and returning
        /// a new CSSStyleSheet with the CSS-OM representation.
        /// </summary>
        /// <param name="uri">The source of the CSS content.</param>
        /// <param name="cancel">The cancellation token for aborting the download.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The CSS stylesheet.</returns>
        public static Task<ICssStyleSheet> GetCssAsync(this Uri uri, CancellationToken cancel, IConfiguration configuration = null)
        {
            return DocumentBuilder.CssAsync(uri, cancel, configuration);
        }

        #endregion

        #region Browsing Context

        /// <summary>
        /// Opens a new document asynchronously in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="response">The response to examine.</param>
        /// <returns>The task that creates the document.</returns>
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, IResponse response)
        {
            return context.OpenAsync(response, CancellationToken.None);
        }

        /// <summary>
        /// Opens a new document asynchronously in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="response">The response to examine.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        public static async Task<IDocument> OpenAsync(this IBrowsingContext context, IResponse response, CancellationToken cancel)
        {
            var src = new TextSource(response.Content, context.Configuration.DefaultEncoding());
            var doc = new Document { Context = context };
            await doc.LoadAsync(response, cancel).ConfigureAwait(false);
            return doc;
        }

        /// <summary>
        /// Opens a new document asynchronously in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="url">The URL to load.</param>
        /// <returns>The task that creates the document.</returns>
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, Url url)
        {
            return context.OpenAsync(url, CancellationToken.None);
        }

        /// <summary>
        /// Opens a new document asynchronously in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="url">The URL to load.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        public static async Task<IDocument> OpenAsync(this IBrowsingContext context, Url url, CancellationToken cancel)
        {
            var response = await context.Configuration.LoadAsync(url, cancel).ConfigureAwait(false);
            return await context.OpenAsync(response, cancel).ConfigureAwait(false);
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns the HTML code representation of the given DOM element.
        /// </summary>
        /// <param name="element">The element to stringify.</param>
        /// <returns>The HTML code of the element and its children.</returns>
        public static String ToHtml(this INode element)
        {
            var htmlObject = element as IHtmlObject;

            if (htmlObject != null)
                return htmlObject.ToHtml();
            else if (element is IElement)
                return ((IElement)element).OuterHtml;

            return element.TextContent;
        }

        /// <summary>
        /// Returns the content text of the given DOM element.
        /// </summary>
        /// <param name="element">The element to stringify.</param>
        /// <returns>The text of the element and its children.</returns>
        public static String ToText(this INode element)
        {
            return element.TextContent;
        }

        #endregion
    }
}
