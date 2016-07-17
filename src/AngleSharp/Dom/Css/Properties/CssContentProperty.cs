namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using static AngleSharp.Css.Converters;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/content
    /// </summary>
    sealed class CssContentProperty : CssProperty
    {
        #region Fields

        static readonly Dictionary<String, ContentMode> ContentModes = new Dictionary<String, ContentMode>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.OpenQuote, new OpenQuoteContentMode() },
            { Keywords.NoOpenQuote, new NoOpenQuoteContentMode() },
            { Keywords.CloseQuote, new CloseQuoteContentMode() },
            { Keywords.NoCloseQuote, new NoCloseQuoteContentMode() }
        };
        static readonly ContentMode[] Default = new[] { new NormalContentMode() };

        // Way of converting for an IElement element
        /*
            var values = ContentConverter.Convert(Value);
            var parts = values.Select(m => m.Stringify(element));
            return String.Join(String.Empty, parts);
        */
        static readonly IValueConverter StyleConverter = Assign(Keywords.Normal, Default).OrNone().Or(
            ContentModes.ToConverter().Or(
            UrlConverter).Or(
            StringConverter).Or(
            AttrConverter).Or(
            CounterConverter).Many()).OrDefault();

        #endregion

        #region ctor

        internal CssContentProperty()
            : base(PropertyNames.Content)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion

        #region Helpers

        static ContentMode TransformUrl(String url)
        {
            return new UrlContentMode(url);
        }

        static ContentMode TransformString(String str)
        {
            return new TextContentMode(str);
        }

        static ContentMode TransformAttr(String attr)
        {
            return new AttributeContentMode(attr);
        }

        static ContentMode TransformCounter(Counter counter)
        {
            return new CounterContentMode(counter);
        }

        #endregion

        #region Modes

        abstract class ContentMode
        {
            public abstract String Stringify(IElement element);
        }

        /// <summary>
        /// Computes to none for the :before and :after pseudo-elements.
        /// </summary>
        sealed class NormalContentMode : ContentMode
        {
            public override String Stringify(IElement element)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// The value is replaced by the open quote string from the quotes
        /// property.
        /// </summary>
        sealed class OpenQuoteContentMode : ContentMode
        {
            public override String Stringify(IElement element)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// The value is replaced by the close string from the quotes
        /// property.
        /// </summary>
        sealed class CloseQuoteContentMode : ContentMode
        {
            public override String Stringify(IElement element)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Introduces no content, but increments the level of nesting for
        /// quotes.
        /// </summary>
        sealed class NoOpenQuoteContentMode : ContentMode
        {
            public override String Stringify(IElement element)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Introduces no content, but decrements the level of nesting for
        /// quotes.
        /// </summary>
        sealed class NoCloseQuoteContentMode : ContentMode
        {
            public override String Stringify(IElement element)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Text content.
        /// </summary>
        sealed class TextContentMode : ContentMode
        {
            readonly String _text;

            public TextContentMode(String text)
            {
                _text = text;
            }

            public override String Stringify(IElement element)
            {
                return _text;
            }
        }

        /// <summary>
        /// The generated text is the value of all counters with the given name
        /// in scope at this pseudo-element, from outermost to innermost
        /// separated by the specified string.
        /// </summary>
        sealed class CounterContentMode : ContentMode
        {
            readonly Counter _counter;

            public CounterContentMode(Counter counter)
            {
                _counter = counter;
            }

            public override String Stringify(IElement element)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Returns the value of the element's attribute X as a string. If
        /// there is no attribute X, an empty string is returned.
        /// </summary>
        sealed class AttributeContentMode : ContentMode
        {
            readonly String _attribute;

            public AttributeContentMode(String attribute)
            {
                _attribute = attribute;
            }

            public override String Stringify(IElement element)
            {
                return element.GetAttribute(_attribute) ?? String.Empty;
            }
        }

        /// <summary>
        /// The value is a URI that designates an external resource (such as an
        /// image). If the resource or image can't be displayed, it is either
        /// ignored or some placeholder shows up.
        /// </summary>
        sealed class UrlContentMode : ContentMode
        {
            readonly String _url;

            public UrlContentMode(String url)
            {
                _url = url;
            }

            public override String Stringify(IElement element)
            {
                return String.Empty;
            }
        }

        #endregion
    }
}
