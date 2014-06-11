namespace AngleSharp
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Css;
    using AngleSharp.DOM.Html;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A set of useful extension methods when dealing with the DOM.
    /// </summary>
    public static class Extensions
    {
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
            where T : IEnumerable<Element>
        {
            foreach (var element in elements)
                element.SetAttribute(attributeName, attributeValue);

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
            where T : IEnumerable<Element>
        {
            var decls = CssParser.ParseDeclarations(declarations);

            foreach (var element in elements)
            {
                for (int i = 0; i < decls.Length; i++)
			    {
                    var name = decls[i];
                    var value = decls.GetPropertyValue(name);
                    element.Style.SetProperty(name, value);
			    }
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
            where T : IEnumerable<Element>
        {
            foreach (var element in elements)
                element.InnerHTML = html;

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
            where T : IEnumerable<Element>
        {
            foreach (var element in elements)
                element.TextContent = text;

            return elements;
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
        public static HTMLCollection QueryXpath(this Document document, String xpath)
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
        public static HTMLCollection QueryXpath(this Element element, String xpath)
        {
            return element.ChildNodes.QueryXpath(xpath);
        }

        /// <summary>
        /// Returns a list of the elements for the given list of elements that match the specified XPath.
        /// </summary>
        /// <param name="nodes">The nodes to search in (first order children).</param>
        /// <param name="xpath">A string containing a valid XPath query.</param>
        /// <returns>A HTMLCollection with all elements that match the selection.</returns>
        public static HTMLCollection QueryXpath(this NodeList nodes, String xpath)
        {
            throw new NotImplementedException("XPath queries will be supported in the future (maybe in v0.7!). Stay tuned!");
        }

        #endregion

        #region Construction Helpers

        /// <summary>
        /// Interprets the string as HTML source code and returns new HTMLDocument
        /// with the DOM representation.
        /// </summary>
        /// <param name="content">The string to use as source code.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The HTML document.</returns>
        public static HTMLDocument ParseHtml(this String content, IConfiguration configuration = null)
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
        public static CSSStyleSheet ParseCss(this String content, IConfiguration configuration = null)
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
        public static HTMLDocument GetHtml(this Uri uri, IConfiguration configuration = null)
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
        public static CSSStyleSheet GetCss(this Uri uri, IConfiguration configuration = null)
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
        public static Task<HTMLDocument> GetHtmlAsync(this Uri uri, IConfiguration configuration = null)
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
        public static Task<HTMLDocument> GetHtmlAsync(this Uri uri, CancellationToken cancel, IConfiguration configuration = null)
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
        public static Task<CSSStyleSheet> GetCssAsync(this Uri uri, IConfiguration configuration = null)
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
        public static Task<CSSStyleSheet> GetCssAsync(this Uri uri, CancellationToken cancel, IConfiguration configuration = null)
        {
            return DocumentBuilder.CssAsync(uri, cancel, configuration);
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns the HTML code representation of the given DOM element.
        /// </summary>
        /// <param name="element">The element to stringify.</param>
        /// <returns>The HTML code of the element and its children.</returns>
        public static String ToHtml(this IElement element)
        {
            return element.OuterHTML;
        }

        /// <summary>
        /// Returns the content text of the given DOM element.
        /// </summary>
        /// <param name="element">The element to stringify.</param>
        /// <returns>The text of the element and its children.</returns>
        public static String ToText(this IElement element)
        {
            return element.TextContent;
        }

        #endregion
    }
}
