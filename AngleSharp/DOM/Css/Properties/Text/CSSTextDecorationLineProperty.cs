namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration-line
    /// </summary>
    public sealed class CSSTextDecorationLineProperty : CSSProperty
    {
        #region Fields

        static readonly ValueConverter<TextDecorationLineMode> _weights = new ValueConverter<TextDecorationLineMode>();
        static readonly NoTextDecorationLineMode _none = new NoTextDecorationLineMode();
        TextDecorationLineMode _weight;

        #endregion

        #region ctor

        static CSSTextDecorationLineProperty()
        {
            _weights.AddStatic("none", _none, exclusive: true);
            _weights.AddStatic("underline", new UnderlineTextDecorationLineMode());
            _weights.AddStatic("overline", new OverlineTextDecorationLineMode());
            _weights.AddStatic("line-through", new LineThroughTextDecorationLineMode());
            _weights.AddStatic("blink", new BlinkTextDecorationLineMode());
            _weights.AddMultiple<MultipleTextDecorationLineMode>();
        }

        public CSSTextDecorationLineProperty()
            : base(PropertyNames.TextDecorationLine)
        {
            _weight = _none;
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            TextDecorationLineMode weight;

            if (_weights.TryCreate(value, out weight))
                _weight = weight;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Mode
        
        abstract class TextDecorationLineMode
        { }

        /// <summary>
        /// Produces no text decoration.
        /// </summary>
        sealed class NoTextDecorationLineMode : TextDecorationLineMode
        {
        }

        /// <summary>
        /// Each line of text is underlined.
        /// </summary>
        sealed class UnderlineTextDecorationLineMode : TextDecorationLineMode
        {
        }

        /// <summary>
        /// Each line of text has a line above it.
        /// </summary>
        sealed class OverlineTextDecorationLineMode : TextDecorationLineMode
        {
        }

        /// <summary>
        /// Each line of text has a line through the middle.
        /// </summary>
        sealed class LineThroughTextDecorationLineMode : TextDecorationLineMode
        {
        }

        /// <summary>
        /// The text blinks (alternates between visible and invisible). Conforming user agents may simply not blink the text.
        /// </summary>
        sealed class BlinkTextDecorationLineMode : TextDecorationLineMode
        {
        }

        /// <summary>
        /// A combination of values.
        /// </summary>
        sealed class MultipleTextDecorationLineMode : TextDecorationLineMode
        {
            List<TextDecorationLineMode> _modes;

            public MultipleTextDecorationLineMode(List<TextDecorationLineMode> modes)
            {
                _modes = modes;
            }
        }

        #endregion
    }
}
