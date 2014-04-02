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
            : base(PropertyNames.FONT_SIZE)
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

        sealed class AbsoluteFontSizeMode : FontSizeMode
        {
            public AbsoluteFontSizeMode(Size sizeMode)
            {
                //TODO init some values
            }

            public enum Size
            {
                Smallest,
                Smaller,
                Small,
                Medium,
                Large,
                Larger,
                Largest
            }
        }

        sealed class RelativeFontSizeMode : FontSizeMode
        {
            public RelativeFontSizeMode(Size sizeMode)
            {
                //TODO init some values
            }

            public enum Size
            {
                Larger,
                Smaller
            }
        }

        sealed class PercentFontSizeMode : FontSizeMode
        {
            Single _scale;

            public PercentFontSizeMode(CSSPercentValue percent)
            {
                _scale = percent.Value;
            }
        }

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
