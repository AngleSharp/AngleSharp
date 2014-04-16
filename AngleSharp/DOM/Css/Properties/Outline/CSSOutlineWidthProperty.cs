namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-width
    /// </summary>
    sealed class CSSOutlineWidthProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, Length> _predefined = new Dictionary<String, Length>();
        Length _width;

        #endregion

        #region ctor

        static CSSOutlineWidthProperty()
        {
            _predefined.Add("medium", new Length(3, Length.Unit.Px));
            _predefined.Add("thin", new Length(1, Length.Unit.Px));
            _predefined.Add("thick", new Length(5, Length.Unit.Px));
        }

        public CSSOutlineWidthProperty()
            : base(PropertyNames.OutlineWidth)
        {
            _inherited = false;
            _width = _predefined["medium"];
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
            var width = length.HasValue ? length.Value : Length.Zero;

            if (length.HasValue)
                _width = width;
            else if (value is CSSIdentifierValue && _predefined.TryGetValue(((CSSIdentifierValue)value).Value, out width))
                _width = width;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
