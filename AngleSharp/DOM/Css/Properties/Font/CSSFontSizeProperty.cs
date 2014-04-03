namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-size
    /// </summary>
    public sealed class CSSFontSizeProperty : CSSProperty
    {
        #region Fields

        static readonly ValueConverter<FontSizeMode> _sizes = new ValueConverter<FontSizeMode>();
        static readonly AbsoluteFontSizeMode _medium = new AbsoluteFontSizeMode(AbsoluteFontSizeMode.Size.Medium);
        FontSizeMode _size;

        #endregion

        #region ctor

        static CSSFontSizeProperty()
        {
            _sizes.AddStatic("xx-small", new AbsoluteFontSizeMode(AbsoluteFontSizeMode.Size.Smallest));
            _sizes.AddStatic("x-small", new AbsoluteFontSizeMode(AbsoluteFontSizeMode.Size.Smaller));
            _sizes.AddStatic("small", new AbsoluteFontSizeMode(AbsoluteFontSizeMode.Size.Small));
            _sizes.AddStatic("medium", _medium);
            _sizes.AddStatic("large", new AbsoluteFontSizeMode(AbsoluteFontSizeMode.Size.Large));
            _sizes.AddStatic("x-large", new AbsoluteFontSizeMode(AbsoluteFontSizeMode.Size.Larger));
            _sizes.AddStatic("xx-large", new AbsoluteFontSizeMode(AbsoluteFontSizeMode.Size.Largest));
            _sizes.AddStatic("larger", new RelativeFontSizeMode(RelativeFontSizeMode.Size.Smaller));
            _sizes.AddStatic("smaller", new RelativeFontSizeMode(RelativeFontSizeMode.Size.Larger));
            _sizes.AddConstructed<PercentFontSizeMode>();
            _sizes.AddConstructed<LengthFontSizeMode>();
        }

        public CSSFontSizeProperty()
            : base(PropertyNames.FontSize)
        {
            _size = _medium;
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            FontSizeMode size;

            if (_sizes.TryCreate(value, out size))
                _size = size;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Mode

        abstract class FontSizeMode
        {
            //TODO add members
        }

        /// <summary>
        /// A set of absolute size keywords based on the user's default font size (which is medium). 
        /// </summary>
        sealed class AbsoluteFontSizeMode : FontSizeMode
        {
            public AbsoluteFontSizeMode(Size sizeMode)
            {
                //TODO init some values
            }

            public enum Size
            {
                /// <summary>
                /// xx-small.
                /// </summary>
                Smallest,
                /// <summary>
                /// x-small.
                /// </summary>
                Smaller,
                /// <summary>
                /// small.
                /// </summary>
                Small,
                /// <summary>
                /// medium.
                /// </summary>
                Medium,
                /// <summary>
                /// large.
                /// </summary>
                Large,
                /// <summary>
                /// x-large.
                /// </summary>
                Larger,
                /// <summary>
                /// xx-large.
                /// </summary>
                Largest
            }
        }

        /// <summary>
        /// Larger or smaller than the parent element's font size, by roughly the ratio used to
        /// separate the absolute size keywords above.
        /// </summary>
        sealed class RelativeFontSizeMode : FontSizeMode
        {
            public RelativeFontSizeMode(Size sizeMode)
            {
                //TODO init some values
            }

            public enum Size
            {
                /// <summary>
                /// Larger than the parent's font size.
                /// </summary>
                Larger,
                /// <summary>
                /// Smaller than the parent's font size.
                /// </summary>
                Smaller
            }
        }

        /// <summary>
        /// A positive percentage of the parent element's font size.
        /// </summary>
        sealed class PercentFontSizeMode : FontSizeMode
        {
            Single _scale;

            public PercentFontSizeMode(CSSPercentValue percent)
            {
                _scale = percent.Value;
            }
        }

        /// <summary>
        /// A positive length. When the units are specified in em or ex, the
        /// size is defined relative to the size of the font on the parent element
        /// of the element in question. For example, 0.5em is half the font size of
        /// the parent of the current element.
        /// </summary>
        sealed class LengthFontSizeMode : FontSizeMode
        {
            Length _length;

            public LengthFontSizeMode(Length length)
            {
                _length = length;
            }
        }

        #endregion
    }
}
