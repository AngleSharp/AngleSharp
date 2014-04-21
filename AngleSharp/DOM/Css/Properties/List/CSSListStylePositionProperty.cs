namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-position
    /// </summary>
    public sealed class CSSListStylePositionProperty : CSSProperty
    {
        #region Fields

        ListPosition _position;

        #endregion

        #region ctor

        internal CSSListStylePositionProperty()
            : base(PropertyNames.ListStylePosition)
        {
            _inherited = true;
            _position = ListPosition.Outside;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected position.
        /// </summary>
        public ListPosition Position
        {
            get { return _position; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is("inside"))
                _position = ListPosition.Inside;
            else if (value.Is("outside"))
                _position = ListPosition.Outside;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
