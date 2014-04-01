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
            _creator.AddMultiple<MultiContentMode>(CssValueListSeparator.Space);
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

        sealed class NoContentMode : ContentMode
        {
        }

        sealed class NormalContentMode : ContentMode
        {
        }

        sealed class OpenQuoteContentMode : ContentMode
        {
        }

        sealed class CloseQuoteContentMode : ContentMode
        {
        }

        sealed class NoOpenQuoteContentMode : ContentMode
        {
        }

        sealed class NoCloseQuoteContentMode : ContentMode
        {
        }

        sealed class TextContentMode : ContentMode
        {
            String _text;

            public TextContentMode(String text)
            {
                _text = text;
            }
        }

        sealed class CounterContentMode : ContentMode
        {
            CSSCounter _counter;

            public CounterContentMode(CSSCounter counter)
            {
                _counter = counter;
            }
        }

        sealed class AttributeContentMode : ContentMode
        {
            CSSAttrValue _attribute;

            public AttributeContentMode(CSSAttrValue attribute)
            {
                _attribute = attribute;
            }
        }

        sealed class UrlContentMode : ContentMode
        {
            Uri _url;

            public UrlContentMode(Uri url)
            {
                _url = url;
            }
        }

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
