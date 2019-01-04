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
    using AngleSharp.Xml.Parser;
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

        private static T Instance<T>(T instance)
        {
            return instance;
        }

        private static Func<IBrowsingContext, T> Creator<T>(Func<IBrowsingContext, T> creator)
        {
            return creator;
        }

        private static readonly Object[] standardServices = new Object[]
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
            Creator<IXmlParser>(ctx => new XmlParser(ctx)),
            Creator<IEventLoop>(ctx => new TaskEventLoop(ctx)),
        };

        /// <summary>
        /// A fixed configuration that cannot be changed.
        /// </summary>
        private static readonly Configuration defaultConfiguration = new Configuration();

        /// <summary>
        /// A custom configuration that is user-defined.
        /// </summary>
        private static IConfiguration customConfiguration;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new immutable configuration.
        /// </summary>
        /// <param name="services">The services to expose.</param>
        public Configuration(IEnumerable<Object> services = null)
        {
            _services = services ?? standardServices;
        }

        #endregion

        #region Default

        /// <summary>
        /// Gets the default configuration to use. The default configuration
        /// can be overriden by calling the SetDefault method.
        /// </summary>
        public static IConfiguration Default => customConfiguration ?? defaultConfiguration;

        /// <summary>
        /// Sets the default configuration to use, when the configuration
        /// is omitted. Providing a null-pointer will reset the default
        /// configuration.
        /// </summary>
        /// <param name="configuration">The configuration to set.</param>
        public static void SetDefault(IConfiguration configuration)
        {
            customConfiguration = configuration;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an enumeration over the registered services.
        /// </summary>
        public IEnumerable<Object> Services => _services;

        #endregion
    }
}
