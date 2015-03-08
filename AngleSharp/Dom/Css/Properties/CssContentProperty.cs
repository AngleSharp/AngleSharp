namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
        static readonly IValueConverter<ContentMode[]> Converter = 
            Converters.Assign(Keywords.Normal, Default).Or(Keywords.None, new ContentMode[0]).Or(
                ContentModes.ToConverter().Or(
                Converters.UrlConverter.To(TransformUrl)).Or(
                Converters.StringConverter.To(TransformString)).Or(
                Converters.AttrConverter.To(TransformAttr)).Or(
                Converters.CounterConverter.To(TransformCounter)).Many()
            );

        #endregion

        #region ctor

        internal CssContentProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Content, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            var parts = Default.Select(m => m.Stringify(element));
            return String.Join(String.Empty, parts);
        }

        protected override Object Compute(IElement element)
        {
            var values = Converter.Convert(Value);
            var parts = values.Select(m => m.Stringify(element));
            return String.Join(String.Empty, parts);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion

        #region Helpers

        static ContentMode TransformUrl(CssUrl url)
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
            readonly Url _url;

            public UrlContentMode(Url url)
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
