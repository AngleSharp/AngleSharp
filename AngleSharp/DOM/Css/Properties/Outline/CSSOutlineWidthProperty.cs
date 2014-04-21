namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-width
    /// </summary>
    public sealed class CSSOutlineWidthProperty : CSSProperty
    {
        #region Fields

        static readonly Length _medium = new Length(3, Length.Unit.Px);
        static readonly Length _thin = new Length(1, Length.Unit.Px);
        static readonly Length _thick = new Length(5, Length.Unit.Px);
        Length _width;

        #endregion

        #region ctor

        internal CSSOutlineWidthProperty()
            : base(PropertyNames.OutlineWidth)
        {
            _inherited = false;
            _width = _medium;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width of the outline of an element. An outline is a
        /// line that is drawn around elements, outside the border edge,
        /// to make the element stand out:
        /// </summary>
        public Length Width
        {
            get { return _width; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var length = value.ToLength();

            if (length.HasValue)
                _width = length.Value;
            else if (value.Is("medium"))
                _width = _medium;
            else if (value.Is("thin"))
                _width = _thin;
            else if (value.Is("thick"))
                _width = _thick;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
