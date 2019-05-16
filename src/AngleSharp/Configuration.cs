namespace AngleSharp
{
    using AngleSharp.Browser;
    using AngleSharp.Css;
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using AngleSharp.Html;
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Parser;
    using AngleSharp.Mathml;
    using AngleSharp.Mathml.Dom;
    using AngleSharp.Svg;
    using AngleSharp.Svg.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents context configuration for the AngleSharp library. Custom
    /// configurations can be made by deriving from this class, just
    /// implementing IConfiguration or modifying an instance of this specific
    /// class.
    /// </summary>
    public class Configuration : IConfiguration
    {
        #region Fields

        private readonly IEnumerable<Object> _services;

        private static T Instance<T>(T instance) => instance;

        private static Func<IBrowsingContext, T> Creator<T>(Func<IBrowsingContext, T> creator) => creator;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new immutable configuration.
        /// </summary>
        /// <param name="services">The services to expose.</param>
        public Configuration(IEnumerable<Object> services = null)
        {
            _services = services ?? new Object[]
            {
                Instance<IElementFactory<Document, HtmlElement>>(new HtmlElementFactory()),
                Instance<IElementFactory<Document, MathElement>>(new MathElementFactory()),
                Instance<IElementFactory<Document, SvgElement>>(new SvgElementFactory()),
                Instance<IEventFactory>(new DefaultEventFactory()),
                Instance<IInputTypeFactory>(new DefaultInputTypeFactory()),
                Instance<IAttributeSelectorFactory>(new DefaultAttributeSelectorFactory()),
                Instance<IPseudoElementSelectorFactory>(new DefaultPseudoElementSelectorFactory()),
                Instance<IPseudoClassSelectorFactory>(new DefaultPseudoClassSelectorFactory()),
                Instance<ILinkRelationFactory>(new DefaultLinkRelationFactory()),
                Instance<IDocumentFactory>(new DefaultDocumentFactory()),
                Instance<IAttributeObserver>(new DefaultAttributeObserver()),
                Instance<IMetaHandler>(new EncodingMetaHandler()),
                Creator<ICssSelectorParser>(ctx => new CssSelectorParser(ctx)),
                Creator<IHtmlParser>(ctx => new HtmlParser(ctx)),
                Creator<INavigationHandler>(ctx => new DefaultNavigationHandler(ctx)),
            }; ;
        }

        #endregion

        #region Default

        /// <summary>
        /// Gets the default configuration to use. The default configuration
        /// can be overriden by calling the SetDefault method.
        /// </summary>
        public static IConfiguration Default => new Configuration();

        #endregion

        #region Properties

        /// <summary>
        /// Gets an enumeration over the registered services.
        /// </summary>
        public IEnumerable<Object> Services => _services;

        #endregion
    }
}
