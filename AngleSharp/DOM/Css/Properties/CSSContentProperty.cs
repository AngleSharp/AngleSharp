namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/content
    /// </summary>
    sealed class CSSContentProperty : CSSProperty
    {
        #region Fields

        static readonly ValueConverter<ContentMode> _creator;
        static readonly NormalContentMode _normal = new NormalContentMode();
        ContentMode _mode;

        #endregion

        #region ctor

        static CSSContentProperty()
        {
            _creator = new ValueConverter<ContentMode>();
            _creator.AddStatic("normal", _normal, exclusive: true);
            _creator.AddStatic("none", new NoContentMode(), exclusive: true);
            _creator.AddStatic("open-quote", new OpenQuoteContentMode());
            _creator.AddStatic("no-open-quote", new NoOpenQuoteContentMode());
            _creator.AddStatic("close-quote", new CloseQuoteContentMode());
            _creator.AddStatic("no-close-quote", new NoCloseQuoteContentMode());
            _creator.AddConstructed<TextContentMode>();
            _creator.AddConstructed<UrlContentMode>();
            _creator.AddConstructed<AttributeContentMode>();
            _creator.AddConstructed<CounterContentMode>();
            _creator.AddEnumerable<MultiContentMode>();
        }

        public CSSContentProperty()
            : base(PropertyNames.CONTENT)
        {
            _mode = _normal;
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            ContentMode mode;

            if (_creator.TryCreate(value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class ContentMode
        {
            //TODO Add members that make sense
        }

        /// <summary>
        /// The pseudo-element is not generated.
        /// </summary>
        sealed class NoContentMode : ContentMode
        {
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
            CSSAttrValue _attribute;

            public AttributeContentMode(CSSAttrValue attribute)
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
            Uri _url;

            public UrlContentMode(Uri url)
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
