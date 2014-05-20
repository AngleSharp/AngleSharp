namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-size
    /// </summary>
    public sealed class CSSFontSizeProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, FontSizeMode> _sizes = new Dictionary<String, FontSizeMode>(StringComparer.OrdinalIgnoreCase);
        FontSizeMode _size;

        #endregion

        #region ctor

        static CSSFontSizeProperty()
        {
            _sizes.Add("medium", new AbsoluteFontSizeMode(AbsoluteFontSizeMode.Size.Medium));
            _sizes.Add("xx-small", new AbsoluteFontSizeMode(AbsoluteFontSizeMode.Size.Smallest));
            _sizes.Add("x-small", new AbsoluteFontSizeMode(AbsoluteFontSizeMode.Size.Smaller));
            _sizes.Add("small", new AbsoluteFontSizeMode(AbsoluteFontSizeMode.Size.Small));
            _sizes.Add("large", new AbsoluteFontSizeMode(AbsoluteFontSizeMode.Size.Large));
            _sizes.Add("x-large", new AbsoluteFontSizeMode(AbsoluteFontSizeMode.Size.Larger));
            _sizes.Add("xx-large", new AbsoluteFontSizeMode(AbsoluteFontSizeMode.Size.Largest));
            _sizes.Add("larger", new RelativeFontSizeMode(RelativeFontSizeMode.Size.Smaller));
            _sizes.Add("smaller", new RelativeFontSizeMode(RelativeFontSizeMode.Size.Larger));
        }

        internal CSSFontSizeProperty()
            : base(PropertyNames.FontSize)
        {
            _size = _sizes["medium"];
            _inherited = true;
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
            //TODO
            //UNITLESS in QUIRKSMODE
            FontSizeMode size;
            CSSCalcValue calc = value.AsCalc();

            if (calc != null)
                _size = new CalcFontSizeMode(calc);
            else if (value is CSSIdentifierValue && _sizes.TryGetValue(((CSSIdentifierValue)value).Value, out size))
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
        /// A positive length. When the units are specified in em or ex, the
        /// size is defined relative to the size of the font on the parent element
        /// of the element in question. For example, 0.5em is half the font size of
        /// the parent of the current element.
        /// OR:
        /// A positive percentage of the parent element's font size.
        /// </summary>
        sealed class CalcFontSizeMode : FontSizeMode
        {
            CSSCalcValue _calc;

            public CalcFontSizeMode(CSSCalcValue calc)
            {
                _calc = calc;
            }
        }

        #endregion
    }
}
