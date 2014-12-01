namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// More infos can be found on the W3C homepage or
    /// in condensed form at 
    /// http://css-infos.net/property/box-decoration-break
    /// </summary>
    sealed class CSSBoxDecorationBreak : CSSProperty, ICssBoxDecorationBreak
    {
        #region Fields

        internal static readonly Boolean Default = false;
        internal static readonly IValueConverter<Boolean> Converter = Toggle(Keywords.Clone, Keywords.Slice);
        Boolean _clone;

        #endregion

        #region ctor

        internal CSSBoxDecorationBreak(CSSStyleDeclaration rule)
            : base(PropertyNames.BoxDecorationBreak, rule)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if each box is independently wrapped with the border
        /// and padding. Otherwise no border and no padding are inserted
        /// at the break.
        /// </summary>
        public Boolean IsCloned
        {
            get { return _clone; }
        }

        #endregion

        #region Methods

        public void SetCloned(Boolean clone)
        {
            _clone = clone;
        }

        internal override void Reset()
        {
            _clone = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetCloned);
        }

        #endregion
    }
}
