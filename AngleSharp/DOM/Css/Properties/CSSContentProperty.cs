namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/content
    /// </summary>
    sealed class CSSContentProperty : CSSProperty, ICssContentProperty
    {
        #region Fields

        static readonly Dictionary<String, ContentMode> modes = new Dictionary<String,ContentMode>(StringComparer.OrdinalIgnoreCase);
        static readonly NormalContentMode _normal = new NormalContentMode();
        ContentMode _mode;

        #endregion

        #region ctor

        static CSSContentProperty()
        {
            modes.Add(Keywords.OpenQuote, new OpenQuoteContentMode());
            modes.Add(Keywords.NoOpenQuote, new NoOpenQuoteContentMode());
            modes.Add(Keywords.CloseQuote, new CloseQuoteContentMode());
            modes.Add(Keywords.NoCloseQuote, new NoCloseQuoteContentMode());
        }

        internal CSSContentProperty()
            : base(PropertyNames.Content)
        {
            _mode = _normal;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is(Keywords.Normal))
                _mode = _normal;
            else if (value.Is(Keywords.None))
                _mode = null;
            else if (value is CSSValueList)
                return Evaluate((CSSValueList)value);
            else if (value == CSSValue.Inherit)
                return true;
            else
            {
                var mode = Evaluate(value);

                if (mode == null)
                    return false;

                _mode = mode;
            }

            return true;
        }

        static ContentMode Evaluate(CSSValue value)
        {
            ContentMode mode = null;

            if (modes.TryGetValue(value, out mode))
                return mode;
            else if (value is CSSCounter)
                return new CounterContentMode((CSSCounter)value);

            var primitive = value as CSSPrimitiveValue;

            if (primitive != null)
            {
                switch (primitive.Unit)
                {
                    case UnitType.Uri:
                        return new UrlContentMode(primitive.ToUri());
                    case UnitType.String:
                        return new TextContentMode(primitive.GetString());
                    case UnitType.Attr:
                        return new AttributeContentMode(primitive.GetString());
                }
            }

            return null;
        }

        Boolean Evaluate(CSSValueList values)
        {
            var items = new List<ContentMode>();

            foreach (var value in values)
            {
                var item = Evaluate(value);

                if (item == null)
                    return false;

                items.Add(item);
            }

            if (items.Count == 0)
                return false;
            else if (items.Count == 1)
                _mode = items[0];
            else
                _mode = new MultiContentMode(items);

            return true;
        }

        #endregion

        #region Modes

        abstract class ContentMode
        {
            //TODO Add members that make sense
        }

        /// <summary>
        /// Computes to none for the :before and :after pseudo-elements.
        /// </summary>
        sealed class NormalContentMode : ContentMode
        {
        }

        /// <summary>
        /// The value is replaced by the open quote string from the quotes property.
        /// </summary>
        sealed class OpenQuoteContentMode : ContentMode
        {
        }

        /// <summary>
        /// The value is replaced by the close string from the quotes property.
        /// </summary>
        sealed class CloseQuoteContentMode : ContentMode
        {
        }

        /// <summary>
        /// Introduces no content, but increments the level of nesting for quotes.
        /// </summary>
        sealed class NoOpenQuoteContentMode : ContentMode
        {
        }

        /// <summary>
        /// Introduces no content, but decrements the level of nesting for quotes.
        /// </summary>
        sealed class NoCloseQuoteContentMode : ContentMode
        {
        }

        /// <summary>
        /// Text content.
        /// </summary>
        sealed class TextContentMode : ContentMode
        {
            String _text;

            public TextContentMode(String text)
            {
                _text = text;
            }
        }

        /// <summary>
        /// The generated text is the value of all counters with
        /// the given name in scope at this pseudo-element, from
        /// outermost to innermost separated by the specified string.
        /// </summary>
        sealed class CounterContentMode : ContentMode
        {
            CSSCounter _counter;

            public CounterContentMode(CSSCounter counter)
            {
                _counter = counter;
            }
        }

        /// <summary>
        /// Returns the value of the element's attribute X as a string.
        /// If there is no attribute X, an empty string is returned.
        /// </summary>
        sealed class AttributeContentMode : ContentMode
        {
            String _attribute;

            public AttributeContentMode(String attribute)
            {
                _attribute = attribute;
            }
        }

        /// <summary>
        /// The value is a URI that designates an external resource (such as
        /// an image). If the resource or image can't be displayed, it is
        /// either ignored or some placeholder shows up.
        /// </summary>
        sealed class UrlContentMode : ContentMode
        {
            readonly Url _url;

            public UrlContentMode(Url url)
            {
                _url = url;
            }
        }

        /// <summary>
        /// A combination of other modes which are concatenated.
        /// </summary>
        sealed class MultiContentMode : ContentMode
        {
            List<ContentMode> _contents;

            public MultiContentMode(List<ContentMode> contents)
            {
                _contents = contents;
            }

        }

        #endregion
    }
}
